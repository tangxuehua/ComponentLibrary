//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Represents a tab within a TabStrip.
    /// </summary>
    public class Tab : TabItem
    {
        /// <summary>
        /// Gets or sets the ID of the PageView to activate when the tab is activated.
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        PersistenceMode(PersistenceMode.Attribute),
        ResDescription("TabTargetID"),
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
        /// Retrieves the PageView object to activate when the tab is activated.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PageView Target
        {
            get
            {
                TabStrip parent = ParentTabStrip;
                if (parent == null)
                {
                    return null;
                }

                MultiPage multiPage = parent.Target;
                if (multiPage == null)
                {
                    return null;
                }

                // Search for the TargetID, if one is specified
                string id = TargetID;
                if (id != String.Empty)
                {
                    Control ctrl = multiPage.FindControl(id);
                    if ((ctrl != null) && (ctrl is PageView))
                    {
                        return (PageView)ctrl;
                    }
                    else
                    {
                        // Target not found, return null
                        return null;
                    }
                }

                // No TargetID, so use index
                int index = Index;
                if ((index >= 0) && (index < multiPage.Controls.Count))
                {
                    Control ctrl = multiPage.Controls[index];
                    if (ctrl is PageView)
                    {
                        return (PageView)ctrl;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// The tab ordered index of this tab.
        /// </summary>
        private int Index
        {
            get { return (ParentTabStrip == null) ? -1 : ParentTabStrip.Items.TabIndexOf(this); }
        }

        /// <summary>
        /// true if this tab is the selected tab.
        /// </summary>
        private bool Selected
        {
            get { return (ParentTabStrip == null) ? false : (ParentTabStrip.SelectedIndex == Index); }
        }

        /// <summary>
        /// The downlevel postback anchor string.
        /// </summary>
        private string AnchorHref
        {
            get
            {
                TabStrip parent = ParentTabStrip;
                if (parent == null)
                {
                    return String.Empty;
                }

                int index = Index;
                return parent.ClientHelperID + ".value='" + index.ToString() + "';" + 
                    parent.Page.GetPostBackEventReference(ParentTabStrip, index.ToString());
            }
        }

        /// <summary>
        /// Tabs are active when they are selected.
        /// </summary>
        internal override bool Active
        {
            get { return Selected; }
        }

        /// <summary>
        /// The uplevel tag name for the tab item.
        /// </summary>
        protected override string UpLevelTag
        {
            get { return TabStrip.TabTagName; }
        }

        /// <summary>
        /// Writes out TabItem attributes.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter that receives the markup.</param>
        protected override void WriteItemAttributes(HtmlTextWriter writer)
        {
            base.WriteItemAttributes(writer);

            if (TargetID != String.Empty)
            {
                writer.WriteAttribute("targetID", Target.ClientID);
            }
        }

        /// <summary>
        /// Returns true if this tab should render an anchor.
        /// An anchor is only rendered when the tab is not selected and
        /// the parent TabStrip is enabled.
        /// </summary>
        private bool RenderAnchor
        {
            get
            {
                bool renderAnchor = true;

                TabStrip parent = ParentTabStrip;
                if (parent != null)
                {
                    renderAnchor = parent.Enabled;
                }

                return Enabled && renderAnchor && !Selected;
            }
        }

        /// <summary>
        /// Opens the anchor tag.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        private void RenderBeginAnchor(HtmlTextWriter writer)
        {
            if (RenderAnchor)
            {
                // If not selected, then render an anchor
                writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:" + AnchorHref);

                if (AccessKey != String.Empty)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey);
                }

                if (TabIndex != 0)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, TabIndex.ToString());
                }

                string style = String.Empty;

                // Custom underline
                if (CurrentStyle["text-decoration"] != null)
                {
                    style += "text-decoration:" + CurrentStyle["text-decoration"] + ";";
                }

                // Custom cursor
                if (CurrentStyle["cursor"] != null)
                {
                    style += "cursor:" + CurrentStyle["cursor"] + ";";
                }

                // Re-apply the current color;
                string currentColor = CurrentStyle["color"];
                if ((currentColor != null) && (currentColor != String.Empty))
                {
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Color, currentColor);
                }

                writer.RenderBeginTag(HtmlTextWriterTag.A);
            }
        }

        /// <summary>
        /// Closes the anchor tag.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        private void RenderEndAnchor(HtmlTextWriter writer)
        {
            if (RenderAnchor)
            {
                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// Renders the image tag.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        /// <param name="imageUrl">The url of the image.</param>
        protected override void RenderImage(HtmlTextWriter writer, string imageUrl)
        {
            RenderBeginAnchor(writer);
            base.RenderImage(writer, imageUrl);
            RenderEndAnchor(writer);
        }

        /// <summary>
        /// Renders the text property.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the content.</param>
        /// <param name="text">The text to render.</param>
        protected override void RenderText(HtmlTextWriter writer, string text)
        {
            RenderBeginAnchor(writer);
            base.RenderText(writer, text);
            RenderEndAnchor(writer);
        }
    }
}
