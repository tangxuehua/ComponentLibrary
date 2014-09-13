//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.Collections;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;

    /// <summary>
    /// Represents a collection of pages.
    /// </summary>
    [
    ParseChildren(true, "Controls"),
    ControlBuilder(typeof(MultiPageControlBuilder)),
    DefaultEvent("SelectedIndexChange"),
    Designer(typeof(NetFocus.Components.WebControls.Design.MultiPageDesigner)),
    ToolboxBitmap(typeof(NetFocus.Components.WebControls.MultiPage)),
    ToolboxData("<{0}:MultiPage runat=server></{0}:MultiPage>"),
    ]
    public class MultiPage : BasePostBackControl
    {
        /// <summary>
        /// Event fires when the SelectedIndex property changes.
        /// </summary>
        public event EventHandler SelectedIndexChange;

        private int _CachedSelectedIndex;

        /// <summary>
        /// Initializes a new instance of a MultiPage control.
        /// </summary>
        public MultiPage() : base()
        {
            _CachedSelectedIndex = -1;
        }

        /// <summary>
        /// Creates a new collection of child controls for the current control.
        /// </summary>
        /// <returns>A PageViewCollection object that contains the currents control's children.</returns>
        protected override ControlCollection CreateControlCollection()
        {
            return new PageViewCollection(this);
        }

        /// <summary>
        /// Gets a ControlCollection object that represents the child controls for a specified server control in the UI hierarchy.
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        PersistenceMode(PersistenceMode.InnerDefaultProperty)
        ]
        public override ControlCollection Controls 
        {
            get { return base.Controls; }
        }

        /// <summary>
        /// Gets or sets the zero-based index of the currently viewable page.
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(0),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("MultiPageSelectedIndex"),
        ]
        public int SelectedIndex
        {
            get
            {
                // If there are no controls in the collection, then return -1.
                if (Controls.Count == 0)
                {
                    return -1;
                }

                // Pull the index out of ViewState, if there is nothing in ViewState,
                // then index defaults to 0.
                int index = 0;
                object obj = ViewState["SelectedIndex"];
                if (obj != null)
                {
                    index = (int)obj;
                }

                // Verify that the index is valid. If not, then return 0.
                if ((index < 0) || (index >= Controls.Count))
                {
                    if (obj != null)
                    {
                        ViewState["SelectedIndex"] = 0;
                    }
                    return 0;
                }

                return index;
            }

            set
            {
                if (Controls.Count == 0)
                {
                    _CachedSelectedIndex = value;
                }
                else if ((value >= 0) && (value < Controls.Count))
                {
                    ViewState["SelectedIndex"] = value;

                    // If the page is not visible, then we want to avoid this situation
                    // by setting the selected index to -1
                    PageView page = SelectedPage;
                    if ((page != null) && (!page.Visible))
                    {
                        ViewState["SelectedIndex"] = -1;
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
        /// Retrieves the PageView object that is currently viewable.
        /// </summary>
        [Browsable(false)]
        private PageView SelectedPage
        {
            get
            {
                int index = SelectedIndex;
                if ((index >= 0) && (index < Controls.Count))
                {
                    return (PageView)Controls[SelectedIndex];
                }

                return null;
            }
        }

        /// <summary>
        /// Overridden. Filters out all objects except PageView objects.
        /// </summary>
        /// <param name="obj">The parsed element.</param>
        protected override void AddParsedSubObject(object obj)
        {
            if (obj is PageView)
            {
                base.AddParsedSubObject(obj);
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
            FireSelectedIndexChangeEvent();
        }

        /// <summary>
        /// Fires the SelectedIndexChange event.
        /// </summary>
        internal void FireSelectedIndexChangeEvent()
        {
            OnSelectedIndexChange(new EventArgs());
        }

        /// <summary>
        /// Overridden. Verifies certain properties.
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            // It may have been possible for a SelectedIndex to be assigned
            // before our pages were added. In that case, we cached the
            // SelectedIndex, and now we're going to do the assignment.

            if (_CachedSelectedIndex >= 0)
            {
                SelectedIndex = _CachedSelectedIndex;
            }
            else if (Controls.Count > 0)
            {
                // Activate the first visible PageView
                SelectedIndex = 0;
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Initializes the pages and the hidden helper.
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            // OnPreRender is not called in the designer, and is not needed either
            if (!IsUpLevelBrowser)
            {
                foreach (Control ctrl in Controls)
                {
                    ((PageView)ctrl).OverrideVisible();
                }
            }

            if (NeedHelper)
            {
                HelperData = SelectedIndex.ToString();
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// The rendering path for uplevel browsers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderUpLevelPath(HtmlTextWriter writer)
        {
            writer.Write("<?XML:NAMESPACE PREFIX=\"MPNS\" /><?IMPORT NAMESPACE=\"MPNS\" IMPLEMENTATION=\""
                + AddPathToFilename("multipage.htc") + "\" />");
            writer.WriteLine();

            AddAttributesToRender(writer);

            writer.AddAttribute("selectedIndex", SelectedIndex.ToString());
            writer.AddAttribute("onSelectedIndexChange", "JScript:" + ClientHelperID + ".value=event.selectedIndex");

            writer.RenderBeginTag("MPNS:MultiPage");

            base.RenderUpLevelPath(writer);

            writer.RenderEndTag();  // MPNS:MultiPage
        }

        /// <summary>
        /// The rendering path for downlevel browsers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderDownLevelPath(HtmlTextWriter writer)
        {
            if (Width == Unit.Empty)
            {
                Width = Unit.Percentage(100);
            }

            AddAttributesToRender(writer);

            string padding = Util.ExtractNumberString(Style["padding"]);
            if (padding != String.Empty)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, padding);
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            // Only render the page that is visible
            PageView page = SelectedPage;
            if (page != null)
            {
                page.RenderControl(writer);
            }
            // base.RenderDownLevelPath(writer);

            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        /// <summary>
        /// The rendering path for visual designers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderDesignerPath(HtmlTextWriter writer)
        {
            if (Width == Unit.Empty)
            {
                Width = Unit.Percentage(100);
            }

            AddAttributesToRender(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // Only render the page that is visible
            PageView page = SelectedPage;
            if (page != null)
            {
                page.RenderControl(writer);
            }

            writer.RenderEndTag();
        }
    }

    /// <summary>
    /// Filters out all child types except for PageView controls.
    /// </summary>
    internal class MultiPageControlBuilder : FilterControlBuilder
    {
        protected override void FillTagTypeTable()
        {
            Add("PageView", typeof(PageView));
        }
    }
}
