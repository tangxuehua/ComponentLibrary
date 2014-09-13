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
    /// Represents a separator within a TabStrip.
    /// </summary>
    public class TabSeparator : TabItem
    {
        /// <summary>
        /// Separators are active when they are next to a selected tab.
        /// </summary>
        internal override bool Active
        {
            get
            {
                TabStrip parent = ParentTabStrip;
                if (parent == null)
                {
                    return false;
                }

                int nIndex = parent.Items.IndexOf(this);
                if (nIndex > 0)
                {
                    // Look at the item to the left
                    TabItem item = parent.Items[nIndex - 1];
                    if ((item != null) && (item is Tab) && ((Tab)item).Active)
                    {
                        // If the item is a tab and is active, then the separator is active
                        return true;
                    }
                }

                if (nIndex < (parent.Items.Count - 1))
                {
                    // Look at the item to the right
                    TabItem item = parent.Items[nIndex + 1];
                    if ((item != null) && (item is Tab) && ((Tab)item).Active)
                    {
                        // If the item is a tab and is active, then the separator is active
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// The uplevel tag name for the tab item.
        /// </summary>
        protected override string UpLevelTag
        {
            get { return TabStrip.TabSeparatorTagName; }
        }
    }
}
