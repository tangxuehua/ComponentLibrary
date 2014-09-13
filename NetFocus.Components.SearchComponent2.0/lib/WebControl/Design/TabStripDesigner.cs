//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls.Design
{
    using System;
    using System.Web.UI.Design;
    using System.Drawing.Design;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using NetFocus.Components.WebControls;

    /// <summary>
    /// Designer class for NetFocus.Components.WebControls.TabStrip
    /// </summary>
    public class TabStripDesigner : ControlDesigner
    {
        private DesignerVerbCollection _Verbs;

        /// <summary>
        /// Gets or sets a value indicating whether or not the control can be resized.
        /// </summary>
        public override bool AllowResize
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the design time verbs supported by the component associated
        /// with the designer.
        /// </summary>
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_Verbs == null)
                {
                    string addTab = DesignUtil.GetStringResource("TabStripAddTab");
                    string addSep = DesignUtil.GetStringResource("TabStripAddSep");

                    _Verbs = new DesignerVerbCollection(
                        new DesignerVerb[]
                        {
                            new DesignerVerb(addTab, new EventHandler(OnAddTab)),
                            new DesignerVerb(addSep, new EventHandler(OnAddSep)),
                        });
                }

                return _Verbs;
            }
        }

        /// <summary>
        /// Called when the Add Tab menu item is clicked.
        /// </summary>
        /// <param name="sender">The source object</param>
        /// <param name="e">Event arguments</param>
        private void OnAddTab(object sender, EventArgs e)
        {
            TabStrip strip = (TabStrip)Component;
            PropertyDescriptor itemsDesc = DesignUtil.GetPropertyDescriptor(strip, "Items");
            if (itemsDesc != null)
            {
                // Tell the designer that we're changing the property
                RaiseComponentChanging(itemsDesc);

                // Do the change
                Tab tab = new Tab();
                tab.Text = "Tab";
                strip.Items.Add(tab);

                // Tell the designer that we've changed the property
                RaiseComponentChanged(itemsDesc, null, null);
                UpdateDesignTimeHtml();
            }
        }

        /// <summary>
        /// Called when the Add Separator menu item is clicked.
        /// </summary>
        /// <param name="sender">The source object</param>
        /// <param name="e">Event arguments</param>
        private void OnAddSep(object sender, EventArgs e)
        {
            TabStrip strip = (TabStrip)Component;
            PropertyDescriptor itemsDesc = DesignUtil.GetPropertyDescriptor(strip, "Items");
            if (itemsDesc != null)
            {
                // Tell the designer that we're changing the property
                RaiseComponentChanging(itemsDesc);

                // Do the change
                TabSeparator sep  = new TabSeparator();
                strip.Items.Add(sep);

                // Tell the designer that we've changed the property
                RaiseComponentChanged(itemsDesc, null, null);
                UpdateDesignTimeHtml();
            }
        }

        /// <summary>
        /// Retrieves the HTML to display in the designer.
        /// </summary>
        /// <returns>The design-time HTML.</returns>
        public override string GetDesignTimeHtml()
        {
            TabStrip strip = (TabStrip)Component;

            // If the tabstrip is empty, then add a label with instructions
            if (strip.Items.Count == 0)
            {
                return CreatePlaceHolderDesignTimeHtml(DesignUtil.GetStringResource("TabStripNoItems"));
            }
            
            return base.GetDesignTimeHtml();
        }
    }
}

