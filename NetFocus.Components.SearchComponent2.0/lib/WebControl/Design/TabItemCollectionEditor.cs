//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls.Design
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Web.UI.Design;

    /// <summary>
    /// Designer editor for the ToolbarItem collection.
    /// </summary>
    public class TabItemCollectionEditor : ItemCollectionEditor
    {
        /// <summary>
        /// Initializes a new instance of the ToolbarItemCollectionEditor class.
        /// </summary>
        /// <param name="type">The type of collection this object is to edit.</param>
        public TabItemCollectionEditor(Type type) : base(type)
        {
        }

        /// <summary>
        /// Returns the type of objects the editor can create.
        /// </summary>
        /// <returns>An array of types.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[]
            {
                typeof(Tab),
                typeof(TabSeparator),
            };
        }

        /// <summary>
        /// Edits the value of the specified object using the specified service provider and context.
        /// </summary>
        /// <param name="context">A type descriptor context that can be used to gain additional context information.</param>
        /// <param name="provider">A service provider object through which editing services may be obtained.</param>
        /// <param name="value">The object to edit the value of.</param>
        /// <returns>The new value of the object. If the value of the object hasn't changed, this should return the same object it was passed.</returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            TabItemCollection col = (TabItemCollection)value;
            int oldSelectedIndex = -1;
            TabStrip parent = null;
            if (col.Count > 0)
            {
                parent = (TabStrip)col[0].Parent;
                if (parent != null)
                {
                    oldSelectedIndex = parent.SelectedIndex;
                }
            }

            object newValue = base.EditValue(context, provider, value);

            if ((parent != null) && (oldSelectedIndex > 0) && (oldSelectedIndex < col.NumTabs))
            {
                // Editing the collection clears out the selected index to its default value
                // Reset it if it is a valid non-default value
                parent.SelectedIndex = oldSelectedIndex;
                IDesignerHost host = (IDesignerHost)provider.GetService(typeof(IDesignerHost));
                if (host != null)
                {
                    IDesigner designer = host.GetDesigner(parent);
                    if ((designer != null) && (designer is ControlDesigner))
                    {
                        ((ControlDesigner)designer).UpdateDesignTimeHtml();
                    }
                }
            }

            return newValue;
        }
    }
}
