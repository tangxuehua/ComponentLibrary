//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.Globalization;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;

    /// <summary>
    /// Allows the CssCollection to convert to/from string objects.
    /// </summary>
    public class CssCollectionConverter : TypeConverter
    {
        /// <summary>
        /// Determines if a CssCollection can be converted from a type.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns>true if possible, false otherwise.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Determines if a CssCollection can be converted to a type.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>true if possible, false otherwise.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            else if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts an object to a CssCollection.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <returns>A new CssCollection or null.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                return new CssCollection((string)value);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts a CssCollection to a new type.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value of the CssCollection.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>The new object or null.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((CssCollection)value).CssText;
            }
            else if (destinationType == typeof(InstanceDescriptor) && (value is CssCollection))
            {
                return new InstanceDescriptor(typeof(CssCollection).GetConstructor(new Type[] { typeof(string) }), new string[] { ((CssCollection)value).CssText } );
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
