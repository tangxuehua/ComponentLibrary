//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls.Design
{
    using System;
    using System.Web.UI.Design;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Web.UI.WebControls;
    using NetFocus.Components.WebControls;

    /// <summary>
    /// Designer class for NetFocus.Components.WebControls.MultiPage
    /// </summary>
    public class MultiPageDesigner : ControlDesigner
    {
        private int _SelectedIndex = -1;
        private DesignerVerbCollection _Verbs;

        /// <summary>
        /// The index of the currently visible page
        /// </summary>
        protected int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
                UpdateDesignTimeHtml();
            }
        }

        /// <summary>
        /// Called when switching to the next page.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        public virtual void OnNextPage(object sender, EventArgs e)
        {
            MultiPage multiPage = (MultiPage)Component;
            if (SelectedIndex < (multiPage.Controls.Count - 1))
            {
                SelectedIndex++;
            }
        }

        /// <summary>
        /// Called when switching to the previous page.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        public virtual void OnPrevPage(object sender, EventArgs e)
        {
            MultiPage multiPage = (MultiPage)Component;
            if ((SelectedIndex > 0) && (multiPage.Controls.Count > 0))
            {
                SelectedIndex--;
            }
        }

        /// <summary>
        /// Called when a part of the component is changing.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="ce">Event arguments</param>
        public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
        {
            base.OnComponentChanged(sender, ce);

            PropertyDescriptor selIndexDesc = DesignUtil.GetPropertyDescriptor(Component, "SelectedIndex");
            if ((selIndexDesc != null) && (ce.Member == selIndexDesc))
            {
                SelectedIndex = (int)ce.NewValue;
                UpdateDesignTimeHtml();
            }
        }

        /// <summary>
        /// Collection of actions that can be performed on the component.
        /// </summary>
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_Verbs == null)
                {
                    string prevPage = DesignUtil.GetStringResource("MultiPageShowPrevPage");
                    string nextPage = DesignUtil.GetStringResource("MultiPageShowNextPage");

                    _Verbs = new DesignerVerbCollection(
                        new DesignerVerb[]
                        {
                            new DesignerVerb(prevPage, new EventHandler(OnPrevPage)),
                            new DesignerVerb(nextPage, new EventHandler(OnNextPage)),
                        }
                    );
                }

                return _Verbs;
            }
        }

        /// <summary>
        /// Retrieves the HTML to display in the designer.
        /// </summary>
        /// <returns>HTML for the designer.</returns>
        public override string GetDesignTimeHtml()
        {
            string html;
            MultiPage multiPage = (MultiPage)Component;
            int realIndex = 0;

            if (multiPage.Controls.Count == 0)
            {
                // Add a message if the MultiPage is empty
                return CreatePlaceHolderDesignTimeHtml(DesignUtil.GetStringResource("MultiPageNoItems"));
            }

            realIndex = multiPage.SelectedIndex;
            if (_SelectedIndex < 0)
            {
                _SelectedIndex = realIndex;
            }
            multiPage.SelectedIndex = _SelectedIndex;

            try
            {
                html = base.GetDesignTimeHtml();
            }
            finally
            {
                // Restore the component
                multiPage.SelectedIndex = realIndex;
            }

            return html;
        }
    }
}

