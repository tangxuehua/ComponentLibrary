//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Collections.Specialized;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Wraps a CssStyleCollection in a CssCollection interface
    /// </summary>
    internal class CssWrapper : CssCollection
    {
        private WebControl _Control;

        /// <summary>
        /// Initializes a new instance of a CssCollection.
        /// </summary>
        public CssWrapper(WebControl ctrl) : base()
        {
            _Control = ctrl;
            Merge(Style, true);

            ItemAdded += new CssEventHandler(OnAdd);
            ItemRemoved += new CssEventHandler(OnRemove);
            Cleared += new EventHandler(OnClear);
        }

        /// <summary>
        /// Retrieves the CssStyleCollection.
        /// </summary>
        protected CssStyleCollection Style
        {
            get { return _Control.Style; }
        }

        /// <summary>
        /// Clones this collection.
        /// </summary>
        /// <returns>A copy of this collection.</returns>
        public override object Clone()
        {
            CssWrapper copy = (CssWrapper)base.Clone();

            copy._Control = this._Control;

            return copy;
        }

        /// <summary>
        /// Called when an item is added.
        /// </summary>
        /// <param name="c">The collection</param>
        /// <param name="e">Event arguments</param>
        private void OnAdd(CssCollection c, CssEventArgs e)
        {
            Style.Add(e.Name, e.Value);
        }

        /// <summary>
        /// Called when an item is removed.
        /// </summary>
        /// <param name="c">The collection</param>
        /// <param name="e">Event arguments</param>
        private void OnRemove(CssCollection c, CssEventArgs e)
        {
            Style.Remove(e.Name);
        }

        /// <summary>
        /// Called when the collection is cleared.
        /// </summary>
        /// <param name="s">The collection</param>
        /// <param name="e">Event arguments</param>
        private void OnClear(object s, EventArgs e)
        {
            Style.Clear();
        }
    }
}
