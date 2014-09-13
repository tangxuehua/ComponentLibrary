//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Collections;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Reflection;

    /// <summary>
    /// Specifies the orientation that a control can be in.
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// Indicates a horizontal orientation.
        /// </summary>
        Horizontal,

        /// <summary>
        /// Indicates a vertical orientation.
        /// </summary>
        Vertical
    };

    /// <summary>
    /// Represents a control that contains a row of tabs and separators.
    /// </summary>
    [
    ParseChildren(true, "Items"),
    DefaultEvent("SelectedIndexChange"),
    Designer(typeof(NetFocus.Components.WebControls.Design.TabStripDesigner)),
    ToolboxBitmap(typeof(NetFocus.Components.WebControls.TabStrip)),
    ToolboxData(@"<{0}:TabStrip runat=""server"" 
                      TabDefaultStyle=""background-color:#000000;font-family:verdana;font-weight:bold;font-size:8pt;color:#ffffff;width:79;height:21;text-align:center""
                      TabHoverStyle=""background-color:#777777""
                      TabSelectedStyle=""background-color:#ffffff;color:#000000"">
                    <{0}:Tab Text=""Tab 1"" />
                    <{0}:Tab Text=""Tab 2"" />
                    <{0}:Tab Text=""Tab 3"" />
                  </{0}:TabStrip>"),
    ]
    public class TabStrip : BasePostBackControl
    {
        /// <summary>
        /// The namespace for the TabStrip and its children.
        /// </summary>
        public const string TagNamespace = "TSNS";

        /// <summary>
        /// The TabStrip's tag name.
        /// </summary>
        public const string TabStripTagName = "TabStrip";

        /// <summary>
        /// The Tab's tag name.
        /// </summary>
        public const string TabTagName = "Tab";

        /// <summary>
        /// The TabSeparator's tag name.
        /// </summary>
        public const string TabSeparatorTagName = "TabSeparator";

        /// <summary>
        /// Event fired when the SelectedIndex property changes.
        /// </summary>
        [ResDescription("TabStripSelectedIndexChange")]
        public event EventHandler SelectedIndexChange;

        private TabItemCollection _Items;
        private int _CachedSelectedIndex;
        private int _OldMultiPageIndex;
        private CssCollection _TabDefaultStyle;
        private CssCollection _TabHoverStyle;
        private CssCollection _TabSelectedStyle;
        private CssCollection _SepDefaultStyle;
        private CssCollection _SepHoverStyle;
        private CssCollection _SepSelectedStyle;

        // Constant to indicate that there is no selection
        private const int NoSelection = -1;

        // Constant to indicate that the value is not set
        private const int NotSet = -2;

        /// <summary>
        /// Initializes a new instance of a TabStrip.
        /// </summary>
        public TabStrip() : base()
        {
            _Items = new TabItemCollection(this);
            _CachedSelectedIndex = NotSet;
            _OldMultiPageIndex = -1;
            _TabDefaultStyle = new CssCollection();
            _TabHoverStyle = new CssCollection();
            _TabSelectedStyle = new CssCollection();
            _SepDefaultStyle = new CssCollection();
            _SepHoverStyle = new CssCollection();
            _SepSelectedStyle = new CssCollection();
        }

        /// <summary>
        /// Gets the collection of items in the control.
        /// </summary>
        [
        Category("Data"),
        DefaultValue(null),
        MergableProperty(false),
        PersistenceMode(PersistenceMode.InnerDefaultProperty),
        ResDescription("TabStripItems"),
        ]
        public virtual TabItemCollection Items
        {
            get { return _Items; }
        }

        /// <summary>
        /// The ID of the MultiPage whose pages will change when the SelectedIndex changes.
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("TabStripTargetID"),
        ]
        public string TargetID
        {
            get
            {
                object obj = ViewState["TargetID"];
                return (obj == null) ? String.Empty : (string)obj;
            }

            set { ViewState["TargetID"] = value; }
        }

        /// <summary>
        /// The target MultiPage whose pages will change when the SelectedIndex changes.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MultiPage Target
        {
            get
            {
                string id = TargetID;
                if (id == String.Empty)
                {
                    return null;
                }

                Control ctrl = null;
                Control container = NamingContainer;
                Control page = Page;

                if (container != null)
                {
                    ctrl = container.FindControl(id);
                }
                if ((ctrl == null) && (page != null))
                {
                    ctrl = page.FindControl(id);
                }

                if ((ctrl != null) && (ctrl is MultiPage))
                {
                    return (MultiPage)ctrl;
                }

                return null;
            }
        }

        /// <summary>
        /// The default image url for separators.
        /// </summary>
        [
        Category("Separator Defaults"),
        DefaultValue(""),
        Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("SepDefaultImageUrl"),
        ]
        public string SepDefaultImageUrl
        {
            get
            {
                Object obj = ViewState["SepDefaultImageUrl"];
                return (obj == null) ? String.Empty : (string)obj;
            }

            set { ViewState["SepDefaultImageUrl"] = value; }
        }

        /// <summary>
        /// The default image url for separators when they are next to
        /// a hovered tab but not next to a selected tab.
        /// </summary>
        [
        Category("Separator Defaults"),
        DefaultValue(""),
        Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("SepHoverImageUrl"),
        ]
        public string SepHoverImageUrl
        {
            get
            {
                Object obj = ViewState["SepHoverImageUrl"];
                return (obj == null) ? String.Empty : (string)obj;
            }

            set { ViewState["SepHoverImageUrl"] = value; }
        }

        /// <summary>
        /// The default image url for separators when they are next to a selected tab.
        /// </summary>
        [
        Category("Separator Defaults"),
        DefaultValue(""),
        Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("SepSelectedImageUrl"),
        ]
        public string SepSelectedImageUrl
        {
            get
            {
                Object obj = ViewState["SepSelectedImageUrl"];
                return (obj == null) ? String.Empty : (string)obj;
            }

            set { ViewState["SepSelectedImageUrl"] = value; }
        }

        /// <summary>
        /// The default style for tabs.
        /// </summary>
        [
        Category("Tab Defaults"),
        DefaultValue(typeof(CssCollection), ""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("TabDefaultStyle"),
        ]
        public CssCollection TabDefaultStyle
        {
            get { return _TabDefaultStyle; }
            set
            {
                _TabDefaultStyle = value;
                if (IsTrackingViewState)
                {
                    ((IStateManager)_TabDefaultStyle).TrackViewState();
                    _TabDefaultStyle.Dirty = true;
                }
            }
        }

        /// <summary>
        /// The default style for tabs when they are hovered.
        /// </summary>
        [
        Category("Tab Defaults"),
        DefaultValue(typeof(CssCollection), ""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("TabHoverStyle"),
        ]
        public CssCollection TabHoverStyle
        {
            get { return _TabHoverStyle; }
            set
            {
                _TabHoverStyle = value;
                if (IsTrackingViewState)
                {
                    ((IStateManager)_TabHoverStyle).TrackViewState();
                    _TabHoverStyle.Dirty = true;
                }
            }
        }

        /// <summary>
        /// The default style for tabs when they are selected.
        /// </summary>
        [
        Category("Tab Defaults"),
        DefaultValue(typeof(CssCollection), ""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("TabSelectedStyle"),
        ]
        public CssCollection TabSelectedStyle
        {
            get { return _TabSelectedStyle; }
            set
            {
                _TabSelectedStyle = value;
                if (IsTrackingViewState)
                {
                    ((IStateManager)_TabSelectedStyle).TrackViewState();
                    _TabSelectedStyle.Dirty = true;
                }
            }
        }

        /// <summary>
        /// The default style for separators.
        /// </summary>
        [
        Category("Separator Defaults"),
        DefaultValue(typeof(CssCollection), ""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("SepDefaultStyle"),
        ]
        public CssCollection SepDefaultStyle
        {
            get { return _SepDefaultStyle; }
            set
            {
                _SepDefaultStyle = value;
                if (IsTrackingViewState)
                {
                    ((IStateManager)_SepDefaultStyle).TrackViewState();
                    _SepDefaultStyle.Dirty = true;
                }
            }
        }

        /// <summary>
        /// The default style for separators when they are next
        /// to a hovered tab but not next to a selected tab.
        /// </summary>
        [
        Category("Separator Defaults"),
        DefaultValue(typeof(CssCollection), ""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("SepHoverStyle"),
        ]
        public CssCollection SepHoverStyle
        {
            get { return _SepHoverStyle; }
            set
            {
                _SepHoverStyle = value;
                if (IsTrackingViewState)
                {
                    ((IStateManager)_SepHoverStyle).TrackViewState();
                    _SepHoverStyle.Dirty = true;
                }
            }
        }

        /// <summary>
        /// The default style for separators when they are next to a selected tab.
        /// </summary>
        [
        Category("Separator Defaults"),
        DefaultValue(typeof(CssCollection), ""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("SepSelectedStyle"),
        ]
        public CssCollection SepSelectedStyle
        {
            get { return _SepSelectedStyle; }
            set
            {
                _SepSelectedStyle = value;
                if (IsTrackingViewState)
                {
                    ((IStateManager)_SepSelectedStyle).TrackViewState();
                    _SepSelectedStyle.Dirty = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an automatic postback to the server 
        /// will occur whenever the user changes the selected index.
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(false),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("AutoPostBack"),
        ]
        public bool AutoPostBack
        {
            get
            {
                Object obj = ViewState["AutoPostBack"];
                return ((obj == null) ? false : (bool)obj);
            }
            set { ViewState["AutoPostBack"] = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal or vertical orientation of the TabStrip.
        /// </summary>
        [
        Category("Appearance"),
        DefaultValue(Orientation.Horizontal),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("TabStripOrientation"),
        ]
        public Orientation Orientation
        {
            get
            {
                object obj = ViewState["Orientation"];
                return (obj == null) ? Orientation.Horizontal : (Orientation)obj;
            }

            set { ViewState["Orientation"] = value; }
        }

        /// <summary>
        /// Gets or sets the zero-based index of the selected tab.
        /// The index is based on the order of tabs and excludes separators.
        /// Example: A TabStrip has a Tab, a TabSeparator, and a Tab.
        ///   When the first Tab is selected, SelectedIndex is 0.
        ///   When the second Tab is selected, SelectedIndex is 1.
        /// The default is -1, which indicates that nothing is selected.
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(0),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("TabStripSelectedIndex"),
        ]
        public int SelectedIndex
        {
            get
            {
                // If the collection is empty, then return NoSelection
                int numTabs = Items.NumTabs;
                if (numTabs == 0)
                {
                    return NoSelection;
                }

                // Pull the index out of ViewState, if there is nothing in ViewState,
                // then index defaults to 0.
                int index = 0;
                object obj = ViewState["SelectedIndex"];
                if (obj != null)
                {
                    index = (int)obj;
                }
                else if (_CachedSelectedIndex == NoSelection)
                {
                    // There is no set value in ViewState, but the cached
                    // value has been set to NoSelection.
                    return NoSelection;
                }

                // Verify that the index is valid. If not, then return NoSelection.
                if ((index < 0) || (index >= numTabs))
                {
                    return NoSelection;
                }

                return index;
            }

            set
            {
                if ((Items.NumTabs == 0) && (value > NotSet))
                {
                    _CachedSelectedIndex = value;
                }
                else if ((value > NotSet) && (value < Items.NumTabs))
                {
                    ViewState["SelectedIndex"] = value;
                    if (value >= 0)
                    {
                        _OldMultiPageIndex = -1;
                        SetTargetSelectedIndex();
                    }
                }
                else
                {
                    // Invalid value
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Allows the collection to reset the selected index when cleared.
        /// </summary>
        internal void ResetSelectedIndex()
        {
            if (ViewState["SelectedIndex"] != null)
            {
                ViewState.Remove("SelectedIndex");
            }
        }

        /// <summary>
        /// Activates the targetted PageView within a MultiPage control.
        /// </summary>
        private void SetTargetSelectedIndex()
        {
            int arrayIndex = Items.ToArrayIndex(SelectedIndex);
            if (arrayIndex >= 0)
            {
                Tab tab = (Tab)Items[arrayIndex];

                MultiPage multiPage = Target;
                if (multiPage != null)
                {
                    PageView page = (tab == null) ? null : tab.Target;
                    if ((page != null) && !page.Selected)
                    {
                        if (_OldMultiPageIndex < 0)
                        {
                            _OldMultiPageIndex = multiPage.SelectedIndex;
                        }

                        // Activate the PageView
                        page.Activate();
                    }
                }
            }
        }

        /// <summary>
        /// Raises the SelectedIndexChange event.
        /// </summary>
        /// <param name="e">Contains the event data</param>
        protected virtual void OnSelectedIndexChange(EventArgs e)
        {
            if (SelectedIndexChange != null)
            {
                SelectedIndexChange(this, e);
            }
        }

        /// <summary>
        /// This control always needs a hidden helper.
        /// </summary>
        protected override bool NeedHelper
        {
            get { return true; }
        }

        /// <summary>
        /// Processes post back data for the server control given the data from the hidden helper.
        /// </summary>
        /// <param name="szData">The data from the hidden helper</param>
        /// <returns>true if the server control's state changes as a result of the post back; otherwise false.</returns>
        protected override bool ProcessData(string szData)
        {
            try
            {
                int newIndex = Convert.ToInt32(szData);

                if (SelectedIndex != newIndex)
                {
                    SelectedIndex = newIndex;
                    return true;
                }
            }
            catch
            {
                // Ignore
            }

            return false;
        }

        /// <summary>
        /// Signals the server control object to notify the ASP.NET application that the state of the control has changed.
        /// </summary>
        protected override void RaisePostDataChangedEvent()
        {
            OnSelectedIndexChange(new EventArgs());

            MultiPage target = Target;
            if (target != null)
            {
                if (_OldMultiPageIndex < 0)
                {
                    SetTargetSelectedIndex();
                }

                if ((_OldMultiPageIndex >= 0) && (target.SelectedIndex != _OldMultiPageIndex))
                {
                    target.FireSelectedIndexChangeEvent();
                }
            }
        }

        /// <summary>
        /// Overridden. Creates an EmptyControlCollection to prevent controls from
        /// being added to the ControlCollection.
        /// </summary>
        /// <returns>An EmptyControlCollection object.</returns>
        protected override ControlCollection CreateControlCollection()
        {
            return new EmptyControlCollection(this);
        }

        /// <summary>
        /// Overridden. Verifies certain properties.
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            // It may have been possible for a SelectedIndex to be assigned
            // before our Items were added. In that case, we cached the
            // SelectedIndex, and now we're going to do the assignment.
            if (_CachedSelectedIndex != NotSet)
            {
                SelectedIndex = _CachedSelectedIndex;
                _CachedSelectedIndex = NotSet;
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Overriden. Verifies certain properties.
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            if ((Page != null) && !Page.IsPostBack)
            {
                if ((TargetID != String.Empty) && (Target == null))
                {
                    // TargetID cannot be found
                    throw new Exception(GetStringResource("TabStripInvalidTargetID"));
                }

                foreach (TabItem item in Items)
                {
                    if (item is Tab)
                    {
                        Tab tab = (Tab)item;
                        PageView target = tab.Target;

                        if ((tab.TargetID != String.Empty) && (target == null))
                        {
                            // TargetID cannot be found
                            throw new Exception(GetStringResource("TabInvalidTargetID"));
                        }
                    }
                }

                SetTargetSelectedIndex();
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// Overridden. Updates the hidden input's data.
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            HelperData = SelectedIndex.ToString();

            base.OnPreRender(e);
        }

        /// <summary>
        /// Overridden. Renders the TabItems within the Items collection.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            foreach (TabItem item in Items)
            {
                item.Render(writer, RenderPath);
            }
        }

        /// <summary>
        /// The rendering path for uplevel browsers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderUpLevelPath(HtmlTextWriter writer)
        {
				writer.Write("<?XML:NAMESPACE PREFIX=\"" + TagNamespace
					+ "\" /><?IMPORT NAMESPACE=\"" + TagNamespace + "\" IMPLEMENTATION=\""
					+ AddPathToFilename("tabstrip.htc") + "\" />");
            writer.WriteLine();

            AddAttributesToRender(writer);

            writer.AddAttribute("selectedIndex", SelectedIndex.ToString());

            if (Orientation == Orientation.Vertical)
                writer.AddAttribute("orientation", "vertical");
            if (TargetID != String.Empty)
                writer.AddAttribute("targetID", Target.ClientID);

            if (SepDefaultImageUrl != String.Empty)
                writer.AddAttribute("sepDefaultImageUrl", SepDefaultImageUrl);
            if (SepHoverImageUrl != String.Empty)
                writer.AddAttribute("sepHoverImageUrl", SepHoverImageUrl);
            if (SepSelectedImageUrl != String.Empty)
                writer.AddAttribute("sepSelectedImageUrl", SepSelectedImageUrl);

            string style = TabDefaultStyle.CssText;
            if (style != String.Empty)
                writer.AddAttribute("tabDefaultStyle", style);
            style = TabHoverStyle.CssText;
            if (style != String.Empty)
                writer.AddAttribute("tabHoverStyle", style);
            style = TabSelectedStyle.CssText;
            if (style != String.Empty)
                writer.AddAttribute("tabSelectedStyle", style);
            style = SepDefaultStyle.CssText;
            if (style != String.Empty)
                writer.AddAttribute("sepDefaultStyle", style);
            style = SepHoverStyle.CssText;
            if (style != String.Empty)
                writer.AddAttribute("sepHoverStyle", style);
            style = SepSelectedStyle.CssText;
            if (style != String.Empty)
                writer.AddAttribute("sepSelectedStyle", style);

            if (Page != null)
            {
                string script = ClientHelperID + ".value" + "=event.index";

                if (AutoPostBack)
                {
                    script += ";if (getAttribute('_submitting') != 'true'){setAttribute('_submitting','true');try{" + Page.GetPostBackEventReference(this, String.Empty) + ";}catch(e){setAttribute('_submitting','false');}}";
                }

                writer.AddAttribute("onSelectedIndexChange", "JScript:" + script);
                writer.AddAttribute("onwcready", "JScript:try{" + ClientHelperID + ".value=selectedIndex}catch(e){}");
            }

            writer.RenderBeginTag(TagNamespace + ":" + TabStripTagName);
            writer.WriteLine();

            base.RenderUpLevelPath(writer);

            writer.RenderEndTag();
        }

        /// <summary>
        /// The rendering path for downlevel browsers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderDownLevelPath(HtmlTextWriter writer)
        {
            writer.WriteLine("<script language=\"javascript\">" + ClientHelperID + ".value=" + SelectedIndex.ToString() + ";</script>");

            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");

            AddAttributesToRender(writer);

            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            if (Orientation == Orientation.Horizontal)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            }
            
            base.RenderDownLevelPath(writer);

            if (Orientation == Orientation.Horizontal)
            {
                writer.RenderEndTag();  // TR
            }
            writer.RenderEndTag();  // TABLE
        }

        /// <summary>
        /// The rendering path for visual designers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderDesignerPath(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");

            AddAttributesToRender(writer);

            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            if (Orientation == Orientation.Horizontal)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            }
            
            base.RenderDesignerPath(writer);

            if (Orientation == Orientation.Horizontal)
            {
                writer.RenderEndTag();  // TR
            }
            writer.RenderEndTag();  // TABLE
        }

        /// <summary>
        /// Loads the control's previously saved view state.
        /// </summary>
        /// <param name="savedState">An object that contains the saved view state values for the control.</param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] state = (object[])savedState;

                base.LoadViewState(state[0]);
                ((IStateManager)Items).LoadViewState(state[1]);
                ((IStateManager)TabDefaultStyle).LoadViewState(state[2]);
                ((IStateManager)TabHoverStyle).LoadViewState(state[3]);
                ((IStateManager)TabSelectedStyle).LoadViewState(state[4]);
                ((IStateManager)SepDefaultStyle).LoadViewState(state[5]);
                ((IStateManager)SepHoverStyle).LoadViewState(state[6]);
                ((IStateManager)SepSelectedStyle).LoadViewState(state[7]);
            }
        }

        /// <summary>
        /// Saves the changes to the control's view state to an Object.
        /// </summary>
        /// <returns>The object that contains the view state changes.</returns>
        protected override object SaveViewState()
        {
            object[] state = new object[]
            {
                base.SaveViewState(),
                ((IStateManager)Items).SaveViewState(),
                ((IStateManager)TabDefaultStyle).SaveViewState(),
                ((IStateManager)TabHoverStyle).SaveViewState(),
                ((IStateManager)TabSelectedStyle).SaveViewState(),
                ((IStateManager)SepDefaultStyle).SaveViewState(),
                ((IStateManager)SepHoverStyle).SaveViewState(),
                ((IStateManager)SepSelectedStyle).SaveViewState(),
            };

            // Check to see if we're really saving anything
            foreach (object obj in state)
            {
                if (obj != null)
                {
                    return state;
                }
            }

            return null;
        }

        /// <summary>
        /// Instructs the control to track changes to its view state.
        /// </summary>
        protected override void TrackViewState()
        {
            base.TrackViewState();

            ((IStateManager)TabDefaultStyle).TrackViewState();
            ((IStateManager)TabHoverStyle).TrackViewState();
            ((IStateManager)TabSelectedStyle).TrackViewState();
            ((IStateManager)SepDefaultStyle).TrackViewState();
            ((IStateManager)SepHoverStyle).TrackViewState();
            ((IStateManager)SepSelectedStyle).TrackViewState();
            ((IStateManager)Items).TrackViewState();
        }
    }
}
