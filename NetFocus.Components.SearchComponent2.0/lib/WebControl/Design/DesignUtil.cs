//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls.Design
{
    using System;
    using System.ComponentModel;

    /// <summary>
    ///  Utility class with various useful static functions.
    /// </summary>
    internal class DesignUtil
    {
        private static System.Resources.ResourceManager _ResourceManager = null;

        internal static System.Resources.ResourceManager GetResourceManager()
        {
            if (_ResourceManager == null)
            {
                Type ourType = typeof(DesignUtil);
				_ResourceManager = new System.Resources.ResourceManager(ourType.Namespace + ".Resources.Resources.Design",ourType.Module.Assembly);
            }

            return _ResourceManager;
        }

        internal static string GetStringResource(string name)
        {
            return (string)GetResourceManager().GetObject(name);
        }

        /// <summary>
        /// Given an object and a property name, retrieves the property descriptor.
        /// </summary>
        /// <param name="obj">The source object</param>
        /// <param name="propName">The property name</param>
        /// <returns>The PropertyDescriptor of the property on the object, or null if not found.</returns>
        internal static PropertyDescriptor GetPropertyDescriptor(object obj, string propName)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);

            if (props != null)
            {
                foreach (PropertyDescriptor propDesc in props)
                {
                    if (propDesc.Name == propName)
                    {
                        return propDesc;
                    }
                }
            }

            return null;
        }
    }
}
