//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Reflection;

    /// <summary>
    /// Base class for child nodes of a TabStrip.
    /// </summary>
    [ToolboxItem(false)]
    public abstract class TabItem : BaseChildNode
    {
        private TabStrip _Parent;
        private CssCollection _DefaultStyle;
        private CssCollection _HoverStyle;
        private CssCollection _SelectedStyle;

        /// <summary>
        /// Initializes a new instance of a TabItem.
        /// </summary>
        public TabItem() : base()
        {
            _DefaultStyle = new CssCollection();
            _HoverStyle = new CssCollection();
            _SelectedStyle = new CssCollection();
        }

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object.</returns>
        public override string ToString()
        {
            string name = this.GetType().Name;

            if (ID != String.Empty)
            {
                name += " - " + ID;
            }

            return name;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            TabItem copy = (TabItem)base.Clone();

            copy._DefaultStyle = (CssCollection)this._DefaultStyle.Clone();
            copy._HoverStyle = (CssCollection)this._HoverStyle.Clone();
            copy._SelectedStyle = (CssCollection)this._SelectedStyle.Clone();

            return copy;
        }

        /// <summary>
        /// The TabStrip control that contains this item.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabStrip ParentTabStrip
        {
            get { return _Parent; }
        }

        /// <summary>
        /// Returns the parent object.
        /// </summary>
        public override object Parent
        {
            get { return ParentTabStrip; }
        }

        /// <summary>
        /// Sets the parent of this item.
        /// </summary>
        /// <param name="parent">The parent TabStrip.</param>
        internal void SetParentTabStrip(TabStrip parent)
        {
            _Parent = parent;
        }

        /// <summary>
        /// Sets all items within the StateBag to be dirty
        /// </summary>
        protected internal override void SetViewStateDirty()
        {
            base.SetViewStateDirty();

            DefaultStyle.Dirty = true;
            HoverStyle.Dirty = true;
            SelectedStyle.Dirty = true;
        }

        /// <summary>
        /// Sets all items within the StateBag to be clean
        /// </summary>
        protected internal override void SetViewStateClean()
        {
            base.SetViewStateClean();

            DefaultStyle.Dirty = false;
            HoverStyle.Dirty = false;
            SelectedStyle.Dirty = false;
        }

        /// <summary>
        /// Gets or sets the keyboard shortcut key (AccessKey) for setting focus to the item.
        /// </summary>
        [DefaultValue("")]
        [Category("Behavior")]
        [ResDescription("BaseAccessKey")]
        public virtual string AccessKey
        {
            get
            {
                object obj = ViewState["AccessKey"];
                return (obj == null) ? String.Empty : (string)obj;
            }

            set { ViewState["AccessKey"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is enabled.
        /// </summary>
        [DefaultValue(true)]
        [Category("Behavior")]
        [ResDescription("BaseEnabled")]
        public virtual bool Enabled
        {
            get
            {
                object obj = ViewState["Enabled"];
                return (obj == null) ? true : (bool)obj;
            }

            set { ViewState["Enabled"] = value; }
        }

        /// <summary>
        /// Gets or sets the tab index of the item.
        /// </summary>
        [DefaultValue((short)0)]
        [Category("Behavior")]
        [ResDescription("BaseTabIndex")]
        public virtual short TabIndex
        {
            get
            {
                object obj = ViewState["TabIndex"];
                return (obj == null) ? (short)0 : (short)obj;
            }

            set { ViewState["TabIndex"] = value; }
        }

        /// <summary>
        /// Gets or sets the tool tip for the item to be displayed when the mouse cursor is over the control.
        /// </summary>
        [DefaultValue("")]
        [Category("Appearance")]
        [ResDescription("BaseToolTip")]
        public virtual string ToolTip
        {
            get
            {
                object obj = ViewState["ToolTip"];
                return (obj == null) ? String.Empty : (string)obj;
            }

            set { ViewState["ToolTip"] = value; }
        }
        
        /// <summary>
        /// Retrieves the orientation from the ParentTabStrip.
        /// </summary>
        protected Orientation Orientation
        {
            get { return (ParentTabStrip == null) ? Orientation.Horizontal : ParentTabStrip.Orientation; }
        }

        /// <summary>
        /// The text string that will appear within a tab item.
        /// </summary>
        [
        Category("Appearance"),
        DefaultValue(""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("ItemText"),
        ]
        public string Text
        {
            get
            {
                string szText = (string)ViewState["Text"];
                return (szText == null) ? String.Empty : szText;
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        /// <summary>
        /// Url of the image to display within the tab item.
        /// </summary>
        [
        Category("Appearance"),
        DefaultValue(""),
        Editor(typeof(NetFocus.Components.WebControls.Design.ObjectImageUrlEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("DefaultImageUrl"),
        ]
        public string DefaultImageUrl
        {
            get
            {
                Object obj = ViewState["DefaultImageUrl"];
                return (obj == null) ? String.Empty : (string)obj;
            }

            set
            {
                ViewState["DefaultImageUrl"] = value;
            }
        }

        /// <summary>
        /// Url of the image to display within the tab item when in a hover state.
        /// </summary>
        [
        Category("Appearance"),
        DefaultValue(""),
        Editor(typeof(NetFocus.Components.WebControls.Design.ObjectImageUrlEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("HoverImageUrl"),
        ]
        public string HoverImageUrl
        {
            get
            {
                Object obj = ViewState["HoverImageUrl"];
                return (obj == null) ? String.Empty : (string)obj;
            }

            set
            {
                ViewState["HoverImageUrl"] = value;
            }
        }

        /// <summary>
        /// Url of the image to display within the tab item when in a selected state.
        /// </summary>
        [
        Category("Appearance"),
        DefaultValue(""),
        Editor(typeof(NetFocus.Components.WebControls.Design.ObjectImageUrlEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("SelectedImageUrl"),
        ]
        public string SelectedImageUrl
        {
            get
            {
                Object obj = ViewState["SelectedImageUrl"];
                return (obj == null) ? String.Empty : (string)obj;
            }

            set
            {
                ViewState["SelectedImageUrl"] = value;
            }
        }

        /// <summary>
        /// The style of the tab item.
        /// </summary>
        [
        Category("Appearance"),
        DefaultValue(typeof(CssCollection), ""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("ChildDefaultStyle"),
        ]
        public CssCollection DefaultStyle
        {
            get { return _DefaultStyle; }
            set
            {
                _DefaultStyle = value;
                if (((IStateManager)this).IsTrackingViewState)
                {
                    ((IStateManager)_DefaultStyle).TrackViewState();
                    _DefaultStyle.Dirty = true;
                }
            }
        }

        /// <summary>
        /// The style of the tab item when in a hover state.
        /// </summary>
        [
        Category("Appearance"),
        DefaultValue(typeof(CssCollection), ""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("ChildHoverStyle"),
        ]
        public CssCollection HoverStyle
        {
            get { return _HoverStyle; }
            set
            {
                _HoverStyle = value;
                if (((IStateManager)this).IsTrackingViewState)
                {
                    ((IStateManager)_HoverStyle).TrackViewState();
                    _HoverStyle.Dirty = true;
                }
            }
        }

        /// <summary>
        /// The style of the tab item when in a selected state.
        /// </summary>
        [
        Category("Appearance"),
        DefaultValue(typeof(CssCollection), ""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("ChildSelectedStyle"),
        ]
        public CssCollection SelectedStyle
        {
            get { return _SelectedStyle; }
            set
            {
                _SelectedStyle = value;
                if (((IStateManager)this).IsTrackingViewState)
                {
                    ((IStateManager)_SelectedStyle).TrackViewState();
                    _SelectedStyle.Dirty = true;
                }
            }
        }

        /// <summary>
        /// The product of merging local and global image urls.
        /// </summary>
        protected virtual string CurrentImageUrl
        {
            get
            {
                TabStrip parent = ParentTabStrip;
                bool isSep = this is TabSeparator;

                if (Active && (SelectedImageUrl != String.Empty))
                {
                    return SelectedImageUrl;
                }
                else if (Active && isSep && (parent != null) && (parent.SepSelectedImageUrl != String.Empty))
                {
                    return parent.SepSelectedImageUrl;
                }
                else if (DefaultImageUrl != String.Empty)
                {
                    return DefaultImageUrl;
                }
                else if (isSep && (parent != null) && (parent.SepDefaultImageUrl != String.Empty))
                {
                    return parent.SepDefaultImageUrl;
                }

                return String.Empty;
            }
        }

        /// <summary>
        /// The product of merging local and global styles.
        /// </summary>
        protected CssCollection CurrentStyle
        {
            get
            {
                CssCollection style = new CssCollection();
                bool isTab = (this is Tab);
                bool isSep = !isTab && (this is TabSeparator);

                CssCollection globalDefault = null, globalSelected = null, 
                    builtinDefault = null, builtinSelected = null;
                TabStrip parent = ParentTabStrip;

                if (isTab)
                {
                    builtinDefault = new CssCollection();
                    builtinDefault["background-color"] = "#D0D0D0";
                    if ((parent != null) && (parent.Style["color"] != null))
                    {
                        builtinDefault["color"] = parent.Style["color"];
                    }
                    else if ((parent != null) && (parent.ForeColor != Color.Empty))
                    {
                        builtinDefault["color"] = ColorTranslator.ToHtml(parent.ForeColor);
                    }
                    else
                    {
                        builtinDefault["color"] = "#000000";
                    }

                    builtinSelected = new CssCollection();
                    builtinSelected["background-color"] = "#FFFFFF";

                }

                if (parent != null)
                {
                    if (isTab)
                    {
                        globalDefault = parent.TabDefaultStyle;
                        globalSelected = parent.TabSelectedStyle;
                    }
                    else if (isSep)
                    {
                        globalDefault = parent.SepDefaultStyle;
                        globalSelected = parent.SepSelectedStyle;
                    }
                }

                if (builtinDefault != null)
                {
                    style.Merge(builtinDefault, true);
                }
                if (globalDefault != null)
                {
                    style.Merge(globalDefault, true);
                }
                style.Merge(DefaultStyle, true);

                if (Active)
                {
                    if (builtinSelected != null)
                    {
                        style.Merge(builtinSelected, true);
                    }
                    if (globalSelected != null)
                    {
                        style.Merge(globalSelected, true);
                    }
                    style.Merge(SelectedStyle, true);
                }

                return style;
            }
        }

        /// <summary>
        /// true if the tab item is in an active state.
        /// </summary>
        internal virtual bool Active
        {
            get { return false; }
        }

        /// <summary>
        /// The uplevel tag name for the tab item.
        /// </summary>
        protected abstract string UpLevelTag
        {
            get;
        }

        /// <summary>
        /// Adds attributes to the HtmlTextWriter.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (AccessKey != String.Empty)
            {
                writer.AddAttribute("accesskey", AccessKey);
            }

            if (!Enabled)
            {
                writer.AddAttribute("disabled", "true");
            }

            if (TabIndex != 0)
            {
                writer.AddAttribute("tabindex", TabIndex.ToString());
            }

            if (ToolTip != String.Empty)
            {
                writer.AddAttribute("title", ToolTip);
            }
        }

        /// <summary>
        /// Writes attributes to the HtmlTextWriter.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        protected override void WriteAttributes(HtmlTextWriter writer)
        {
            base.WriteAttributes(writer);

            if (AccessKey != String.Empty)
            {
                writer.WriteAttribute("accesskey", AccessKey);
            }

            if (!Enabled)
            {
                writer.WriteAttribute("disabled", "true");
            }

            if (TabIndex != 0)
            {
                writer.WriteAttribute("tabindex", TabIndex.ToString());
            }

            if (ToolTip != String.Empty)
            {
                writer.WriteAttribute("title", ToolTip);
            }
        }

        /// <summary>
        /// Writes out TabItem attributes.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter that receives the markup.</param>
        protected virtual void WriteItemAttributes(HtmlTextWriter writer)
        {
            if (DefaultImageUrl != String.Empty)
                writer.WriteAttribute("defaultImageUrl", DefaultImageUrl);
            if (HoverImageUrl != String.Empty)
                writer.WriteAttribute("hoverImageUrl", HoverImageUrl);
            if (SelectedImageUrl != String.Empty)
                writer.WriteAttribute("selectedImageUrl", SelectedImageUrl);

            string style = DefaultStyle.CssText;
            if (style != String.Empty)
                writer.WriteAttribute("defaultStyle", style);
            style = HoverStyle.CssText;
            if (style != String.Empty)
                writer.WriteAttribute("hoverStyle", style);
            style = SelectedStyle.CssText;
            if (style != String.Empty)
                writer.WriteAttribute("selectedStyle", style);
        }

        /// <summary>
        /// Renders the item for uplevel browsers.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        protected override void RenderUpLevelPath(HtmlTextWriter writer)
        {
            writer.WriteBeginTag(TabStrip.TagNamespace + ":" + UpLevelTag);

            WriteAttributes(writer);
            WriteItemAttributes(writer);

            if (Text != String.Empty)
            {
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write(Text);
                writer.WriteEndTag(TabStrip.TagNamespace + ":" + UpLevelTag);
            }
            else
            {
                writer.Write(HtmlTextWriter.SelfClosingChars + HtmlTextWriter.TagRightChar);
            }
            writer.WriteLine();
        }

        /// <summary>
        /// Renders the image tag.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        /// <param name="imageUrl">The url of the image.</param>
        protected virtual void RenderImage(HtmlTextWriter writer, string imageUrl)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Src, imageUrl);
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "absmiddle");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
        }

        /// <summary>
        /// Renders the text property.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        /// <param name="text">The text to render.</param>
        protected virtual void RenderText(HtmlTextWriter writer, string text)
        {
            writer.Write(text);
        }

        /// <summary>
        /// Renders contents for downlevel and visual designers.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        private void RenderContents(HtmlTextWriter writer)
        {
            string imageUrl = CurrentImageUrl;
            bool renderTable = ((imageUrl != String.Empty) && (Text != String.Empty));

            if (renderTable)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, null);
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
            }

            if (imageUrl != String.Empty)
            {
                RenderImage(writer, imageUrl);
            }

            if (renderTable)
            {
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, null);
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
            }

            if (Text != String.Empty)
            {
                RenderText(writer, Text);
            }

            if (renderTable)
            {
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            if ((imageUrl == String.Empty) && (Text == String.Empty))
            {
                // Neither text nor image was rendered, so render a blank
                RenderText(writer, "&nbsp;");
            }
        }

        /// <summary>
        /// Renders content for downlevel browsers.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        protected virtual void DownLevelContent(HtmlTextWriter writer)
        {
            RenderContents(writer);
        }

        /// <summary>
        /// Renders the item for downlevel browsers.
        /// </summary>
        /// <param name="htmlWriter">The HtmlTextWriter object that receives the content.</param>
        protected override void RenderDownLevelPath(HtmlTextWriter htmlWriter)
        {
            HtmlInlineWriter writer = new HtmlInlineWriter(htmlWriter);
            if (Orientation == Orientation.Vertical)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            }

            CurrentStyle.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, null);

            if (!Enabled)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
            }

            if (ToolTip != String.Empty)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Title, ToolTip);
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            DownLevelContent(writer);

            writer.RenderEndTag();

            writer.AllowNewLine = true;
            writer.WriteLine();

            if (Orientation == Orientation.Vertical)
            {
                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// Renders content for visual designers.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        protected virtual void DesignerContent(HtmlTextWriter writer)
        {
            RenderContents(writer);
        }

        /// <summary>
        /// Renders the item for visual designers.
        /// </summary>
        /// <param name="htmlWriter">The HtmlTextWriter object that receives the content.</param>
        protected override void RenderDesignerPath(HtmlTextWriter htmlWriter)
        {
            HtmlInlineWriter writer = new HtmlInlineWriter(htmlWriter);
            if (Orientation == Orientation.Vertical)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            }

            CurrentStyle.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, null);

            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            DesignerContent(writer);

            writer.RenderEndTag();

            writer.AllowNewLine = true;
            writer.WriteLine();

            if (Orientation == Orientation.Vertical)
            {
                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// Loads the item's previously saved view state.
        /// </summary>
        /// <param name="savedState">An Object that contains the saved view state values for the item.</param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] state = (object[])savedState;

                base.LoadViewState(state[0]);
                ((IStateManager)DefaultStyle).LoadViewState(state[1]);
                ((IStateManager)HoverStyle).LoadViewState(state[2]);
                ((IStateManager)SelectedStyle).LoadViewState(state[3]);
            }
        }

        /// <summary>
        /// Saves the changes to the item's view state to an object.
        /// </summary>
        /// <returns>The object that contains the view state changes.</returns>
        protected override object SaveViewState()
        {
            object[] state = new object[]
            {
                base.SaveViewState(),
                ((IStateManager)DefaultStyle).SaveViewState(),
                ((IStateManager)HoverStyle).SaveViewState(),
                ((IStateManager)SelectedStyle).SaveViewState(),
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
        /// Instructs the item to track changes to its view state.
        /// </summary>
        protected override void TrackViewState()
        {
            base.TrackViewState();

            ((IStateManager)DefaultStyle).TrackViewState();
            ((IStateManager)HoverStyle).TrackViewState();
            ((IStateManager)SelectedStyle).TrackViewState();
        }
    }
}
