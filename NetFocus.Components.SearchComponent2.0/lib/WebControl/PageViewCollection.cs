//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.Web.UI;

    /// <summary>
    /// Represents a collection of PageView controls.
    /// </summary>
    public class PageViewCollection : ControlCollection
    {
        /// <summary>
        /// Initializes a new instance of a PageViewCollection. 
        /// </summary>
        /// <param name="owner">The parent MultiPage control.</param>
        public PageViewCollection(MultiPage owner) : base(owner)
        {
        }

        /// <summary>
        /// Verifies that a child control is a PageView.
        /// If it is, then certain properties are set.
        /// If it is not, then an exception is thrown.
        /// </summary>
        /// <param name="child">The child control.</param>
        private void VerifyChild(Control child)
        {
            if (child is PageView)
            {
                ((PageView)child).ParentMultiPage = (MultiPage)Owner;
                return;
            }

            throw new Exception(String.Format(Util.GetStringResource("InvalidChildType"), new object[] { child.GetType().FullName, typeof(PageView).FullName }));
        }

        /// <summary>
        /// Adds a control to the collection.
        /// </summary>
        /// <param name="child">The child control.</param>
        public override void Add(Control child)
        {
            VerifyChild(child);
            base.Add(child);
        }

        /// <summary>
        /// Adds a control to the collection at a specific index.
        /// </summary>
        /// <param name="index">The index where the control should be added.</param>
        /// <param name="child">The child control.</param>
        public override void AddAt(int index, Control child)
        {
            VerifyChild(child);
            base.AddAt(index, child);

            MultiPage mpParent = (MultiPage)Owner;
            int curIndex = mpParent.SelectedIndex;
            if (index <= curIndex)
            {
                curIndex++;
                if (curIndex < Count)
                {
                    mpParent.SelectedIndex = curIndex;
                }
            }
        }

        /// <summary>
        /// After removing an index, adjust the selected index.
        /// </summary>
        /// <param name="index">The index of the element that was removed.</param>
        private void RemovedIndex(int index)
        {
            MultiPage mpParent = (MultiPage)Owner;
            int curIndex = mpParent.SelectedIndex;
            if ((index >= 0) && (index < curIndex))
            {
                mpParent.SelectedIndex = curIndex - 1;
            }
            else if ((index == curIndex) && (curIndex > 0) && (Count <= curIndex))
            {
                mpParent.SelectedIndex = Count - 1;
            }
        }

        /// <summary>
        /// Removes the specified item from the collection.
        /// </summary>
        /// <param name="value">The item to remove from the collection.</param>
        public override void Remove(Control value)
        {
            int index = IndexOf(value);
            base.Remove(value);
            RemovedIndex(index);
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            RemovedIndex(index);
        }

        /// <summary>
        /// Clears the collection.
        /// </summary>
        public override void Clear()
        {
            if (Count > 0)
            {
                // This is to prepare for new items later.
                // When a new item is added later, we want the SelectedIndex to reset to 0.
                MultiPage mpParent = (MultiPage)Owner;
                mpParent.SelectedIndex = 0;
            }
            base.Clear();
        }
    }
}
