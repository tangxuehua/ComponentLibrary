//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Web.UI;

    /// <summary>
    /// Collection of TabItems within a TabStrip.
    /// </summary>
    [Editor(typeof(NetFocus.Components.WebControls.Design.TabItemCollectionEditor), typeof(UITypeEditor))]
    public class TabItemCollection : BaseChildNodeCollection
    {
        private TabStrip _Parent;
        private int _NumTabs = 0;

        /// <summary>
        /// Initializes a new instance of a TabItemCollection.
        /// </summary>
        public TabItemCollection() : base()
        {
            _Parent = null;
        }

        /// <summary>
        /// Initializes a new instance of a TabItemCollection.
        /// </summary>
        /// <param name="parent">The parent TabStrip of this collection.</param>
        public TabItemCollection(TabStrip parent) : base()
        {
            _Parent = parent;
        }

        /// <summary>
        /// Creates a new deep copy of the current collection.
        /// </summary>
        /// <returns>A new object that is a deep copy of this instance.</returns>
        public override object Clone()
        {
            TabItemCollection copy = (TabItemCollection)base.Clone();

            copy._Parent = this._Parent;
            copy._NumTabs = this._NumTabs;

            return copy;
        }

        /// <summary>
        /// The parent TabStrip containing this collection of items.
        /// </summary>
        private TabStrip ParentTabStrip
        {
            get { return _Parent; }
        }

        /// <summary>
        /// The number of tabs within this collection.
        /// </summary>
        public int NumTabs
        {
            get { return _NumTabs; }
            set { _NumTabs = value; }
        }

        /// <summary>
        /// Tracks the number of tabs after a clear operation.
        /// </summary>
        protected override void OnClear()
        {
            base.OnClear();

            NumTabs = 0;
            if (!Reloading && (ParentTabStrip != null))
            {
                ParentTabStrip.ResetSelectedIndex();
            }

            foreach (TabItem item in List)
            {
                item.SetParentTabStrip(null);
            }
        }

        /// <summary>
        /// Tracks the number of tabs after a remove operation.
        /// </summary>
        /// <param name="index">The index of the item being removed.</param>
        /// <param name="value">The item being removed.</param>
        protected override void OnRemove(int index, object value)
        {
            if (value is Tab)
            {
                if (!Reloading && (ParentTabStrip != null))
                {
                    int selIndex = ParentTabStrip.SelectedIndex;
                    int tabIndex = ToTabIndex(index);
                    if ((tabIndex < selIndex) || ((tabIndex == selIndex) && (tabIndex == (NumTabs - 1)) && (NumTabs > 1)))
                    {
                        // Decrease the SelectedIndex if the tab index is less than the selected index
                        // or if the tab index is equal to the selected index and the selected index
                        // is the last tab and there is another tab in the TabStrip
                        ParentTabStrip.SelectedIndex = selIndex - 1;
                    }
                }

                NumTabs--;
            }

            base.OnRemove(index, value);
        }

        /// <summary>
        /// Cleans up SelectedIndex property
        /// </summary>
        /// <param name="index">The index of the object that was removed.</param>
        /// <param name="value">The object that was removed.</param>
        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);

            ((TabItem)value).SetParentTabStrip(null);
        }

        /// <summary>
        /// Tracks the number of tabs after an insert operation.
        /// </summary>
        /// <param name="index">The index of the item being inserted.</param>
        /// <param name="value">The item being inserted.</param>
        protected override void OnInsert(int index, object value)
        {
            TabItem item = (TabItem)value;

            if (item.ParentTabStrip != null)
            {
                item.ParentTabStrip.Items.Remove(item);
            }

            SetItemProperties(item);

            if (item is Tab)
            {
                NumTabs++;
            }

            base.OnInsert(index, item);

        }

        /// <summary>
        /// Adjusts the SelectedIndex after inserting an item.
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <param name="value">The item inserted</param>
        protected override void OnInsertComplete(int index, object value)
        {
            base.OnInsertComplete(index, value);

            if (!Reloading &&
                (ParentTabStrip != null) && (NumTabs > 1) && (value is Tab) &&
                (ToTabIndex(index) <= ParentTabStrip.SelectedIndex))
            {
                ParentTabStrip.SelectedIndex++;
            }
        }

        /// <summary>
        /// Tracks the number of tabs after a set operation.
        /// </summary>
        /// <param name="index">The index of the item being changed.</param>
        /// <param name="oldValue">The old item.</param>
        /// <param name="newValue">The new item.</param>
        protected override void OnSet(int index, object oldValue, object newValue)
        {
            SetItemProperties((TabItem)newValue);

            base.OnSet(index, oldValue, newValue);

            if (oldValue is Tab)
            {
                NumTabs--;
            }

            if (newValue is Tab)
            {
                NumTabs++;
            }
        }

        /// <summary>
        /// Given a tab-based index, converts it to an index in this collection.
        /// Example:
        ///   [0]=Tab1; [1]=TabSeparator; [2]=Tab2;
        ///   ToArrayIndex(0) = 0
        ///   ToArrayIndex(1) = 2
        ///   ToArrayIndex(2) = -1
        /// </summary>
        /// <param name="tabIndex">The tab-based index.</param>
        /// <returns>The zero-based index into this collection.</returns>
        public int ToArrayIndex(int tabIndex)
        {
            if ((tabIndex >= 0) && (List.Count > 0))
            {
                int arrayIndex = 0;

                foreach (TabItem item in List)
                {
                    if (item is Tab)
                    {
                        tabIndex--;
                    }

                    if (tabIndex < 0)
                    {
                        return arrayIndex;
                    }

                    arrayIndex++;
                }
            }

            return -1;
        }

        /// <summary>
        /// Given a zero-based index into the collection, converts it to a tab-based index.
        /// Example:
        ///   [0]=Tab1; [1]=TabSeparator; [2]=Tab2;
        ///   ToTabIndex(0) = 0
        ///   ToTabIndex(1) = -1
        ///   ToTabIndex(2) = 1
        /// </summary>
        /// <param name="arrayIndex"></param>
        /// <returns></returns>
        public int ToTabIndex(int arrayIndex)
        {
            int tabIndex = -1;

            foreach (TabItem item in List)
            {
                if (item is Tab)
                {
                    tabIndex++;
                }

                arrayIndex--;
                if (arrayIndex < 0)
                {
                    // Only return the converted index if it was a tab
                    if (item is Tab)
                    {
                        return tabIndex;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Sets properties of the TabItem before being added.
        /// </summary>
        /// <param name="item">The TabItem to be set.</param>
        private void SetItemProperties(TabItem item)
        {
            item.SetParentTabStrip(ParentTabStrip);
        }

        /// <summary>
        /// Adds a TabItem to the collection.
        /// </summary>
        /// <param name="item">The TabItem to add.</param>
        public void Add(TabItem item)
        {
            if (!Contains(item))
            {
                List.Add(item);
            }
        }

        /// <summary>
        /// Adds a TabItem to the collection at a specific index.
        /// </summary>
        /// <param name="index">The index at which to add the item.</param>
        /// <param name="item">The TabItem to add.</param>
        public void AddAt(int index, TabItem item)
        {
            if (!Contains(item))
            {
                List.Insert(index, item);
            }
        }

        /// <summary>
        /// Determines if a TabItem is in the collection.
        /// </summary>
        /// <param name="item">The TabItem to search for.</param>
        /// <returns>true if the TabItem exists within the collection. false otherwise.</returns>
        public bool Contains(TabItem item)
        {
            return List.Contains(item);
        }

        /// <summary>
        /// Determines zero-based index of a TabItem within the collection.
        /// </summary>
        /// <param name="item">The TabItem to locate within the collection.</param>
        /// <returns>The zero-based index.</returns>
        public int IndexOf(TabItem item)
        {
            return List.IndexOf(item);
        }

        /// <summary>
        /// Determines the tab-based index of a Tab within the collection.
        /// </summary>
        /// <param name="tab">The Tab to locate within the collection.</param>
        /// <returns>The tab-based index.</returns>
        public int TabIndexOf(Tab tab)
        {
            return ToTabIndex(IndexOf(tab));
        }

        /// <summary>
        /// Removes a TabItem from the collection.
        /// </summary>
        /// <param name="item">The TabItem to remove.</param>
        public void Remove(TabItem item)
        {
            List.Remove(item);
        }

        /// <summary>
        /// Indexer into the collection.
        /// </summary>
        public TabItem this[int index]
        {
            get { return (TabItem)List[index]; }
        }
    }
}
