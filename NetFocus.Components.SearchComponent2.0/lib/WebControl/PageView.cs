//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.ComponentModel;

    /// <summary>
    /// Represents a page in a MultiPage control.
    /// </summary>
    [
    ParseChildren(false),
    PersistChildren(true),
    ToolboxItem(false),
    ]
    public class PageView : BaseRichControl
    {
        private bool _OverrideVisible;
        private MultiPage _Parent;

        /// <summary>
        /// Initializes a new instance of a PageView.
        /// </summary>
        public PageView() : base()
        {
            _OverrideVisible = false;
        }

        /// <summary>
        /// Retrieves the parent MultiPage control.
        /// </summary>
        protected internal MultiPage ParentMultiPage
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        /// <summary>
        /// Determines the index of this page within the MultiPage's collection of pages.
        /// </summary>
        protected int Index
        {
            get
            {
                MultiPage multiPage = ParentMultiPage;
                if (multiPage == null)
                {
                    return -1;
                }

                return multiPage.Controls.IndexOf(this);
            }
        }

        /// <summary>
        /// Determines if this page is selected.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool Selected
        {
            get
            {
                MultiPage multiPage = ParentMultiPage;
                if (multiPage == null)
                {
                    return false;
                }

                return (multiPage.SelectedIndex == Index);
            }
        }

        /// <summary>
        /// Allows the MultiPage to inform the PageView to override the default
        /// behavior of the Visible property.
        /// </summary>
        internal void OverrideVisible()
        {
            bool selected = Selected;
            if (Visible != selected)
            {
                _OverrideVisible = true;
                Visible = selected;
                _OverrideVisible = false;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether a control should be rendered on the page.
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public override bool Visible
        {
            get { return base.Visible; }

            set
            {
                if (!_OverrideVisible)
                {
                    throw new Exception(Util.GetStringResource("MultiPageErrorVisible"));
                }

                base.Visible = value;
            }
        }

        /// <summary>
        /// Sets this page to be the selected page.
        /// </summary>
        public virtual void Activate()
        {
            MultiPage multiPage = ParentMultiPage;
            if (multiPage != null)
            {
                multiPage.SelectedIndex = Index;
            }
        }

        /// <summary>
        /// Overridden. Verifies certain properties.
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (ParentMultiPage == null)
            {
                throw new Exception(String.Format(GetStringResource("InvalidParentTagName"), new object[] { this.GetType().FullName, typeof(MultiPage).FullName }));
            }

            base.OnInit(e);
        }

        /// <summary>
        /// The rendering path for uplevel browsers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderUpLevelPath(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);
            writer.RenderBeginTag("MPNS:PageView");

            base.RenderUpLevelPath(writer);

            writer.RenderEndTag();  // MPNS:PageView
            writer.WriteLine();
        }

        /// <summary>
        /// The rendering path for downlevel browsers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderDownLevelPath(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            base.RenderDownLevelPath(writer);

            writer.RenderEndTag();
        }

        /// <summary>
        /// The rendering path for visual designers.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderDesignerPath(HtmlTextWriter writer)
        {
            if (Visible)
            {
                AddAttributesToRender(writer);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                base.RenderDesignerPath(writer);

                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// Loads the previous state.
        /// </summary>
        /// <param name="state">ViewState object.</param>
        protected override void LoadViewState(object state)
        {
            base.LoadViewState(state);
            if (!Visible)
            {
                _OverrideVisible = true;
                Visible = true;
                _OverrideVisible = false;
            }
        }
    }
}
