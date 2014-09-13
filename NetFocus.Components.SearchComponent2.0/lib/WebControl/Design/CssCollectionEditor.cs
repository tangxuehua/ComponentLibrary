//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls.Design
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using NetFocus.Components.WebControls;

    /// <summary>
    /// Edits a CssCollection object.
    /// </summary>
    public class CssCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// Initializes a new instance of the CssCollectionEditor class.
        /// </summary>
        /// <param name="type">The type to edit.</param>
        public CssCollectionEditor(Type type) : base(type)
        {
        }

        /// <summary>
        /// Returns the type of objects the editor can create.
        /// </summary>
        /// <returns>An array of types.</returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(CssAttribute) };
        }

        /// <summary>
        /// Gets the data type that this collection contains.
        /// </summary>
        /// <returns>The Type.</returns>
        protected override Type CreateCollectionItemType()
        {
            return typeof(CssAttribute);
        }

        /// <summary>
        /// Converts the specified collection into an array of values.
        /// </summary>
        /// <param name="editValue">The collection to edit.</param>
        /// <returns>An array of values.</returns>
        protected override object[] GetItems(object editValue) 
        {
            if (editValue is CssCollection)
            {
                CssCollection col = (CssCollection)editValue;
                object[] values = new object[col.Keys.Count];

                for (int i = 0; i < col.Keys.Count; i++)
                {
                    string key = col.Keys[i];
                    values[i] = new CssAttribute(key, col[key]);
                }

                return values;
            }

            return new object[0];
        }

        /// <summary>
        /// Converts an array of items back into a collection.
        /// </summary>
        /// <param name="editValue">The collection to edit.</param>
        /// <param name="value">The array of values.</param>
        /// <returns>The editted collection.</returns>
        protected override object SetItems(object editValue, object[] value) 
        {
            if (editValue is CssCollection)
            {
                CssCollection col = (CssCollection)editValue;
                col.Clear();

                for (int i = 0; i < value.Length; i++)
                {
                    CssAttribute attrib = (CssAttribute)value[i];

                    if ((attrib.Attribute != String.Empty) && (attrib.Value != String.Empty))
                    {
                        col.Add(attrib.Attribute, attrib.Value);
                    }
                }
            }

            return editValue;
        }
    }

    /// <summary>
    /// For editting a CSS attribute.
    /// </summary>
    public class CssAttribute
    {
        private string _Name;
        private string _Value;

        /// <summary>
        /// Initializes a new instance of a CSS Attribute.
        /// </summary>
        public CssAttribute()
        {
            _Name = String.Empty;
            _Value = String.Empty;
        }

        /// <summary>
        /// Provides a string representation of the attribute
        /// </summary>
        /// <returns>The string representation</returns>
        public override string ToString()
        {
            if (Attribute == String.Empty)
            {
                return this.GetType().Name;
            }

            if (Value == String.Empty)
            {
                return Attribute;
            }

            return Attribute + ":" + Value;
        }

        /// <summary>
        /// Initializes a new instance of a CSS Attribute.
        /// </summary>
        /// <param name="name">The CSS attribute name.</param>
        /// <param name="value">The CSS value.</param>
        public CssAttribute(string name, string value)
        {
            _Name = (string)((ICloneable)name).Clone();
            _Value = (string)((ICloneable)value).Clone();
        }

        /// <summary>
        /// The CSS attribute name.
        /// </summary>
        [Browsable(true)]
        [Category("CSS")]
        public string Attribute
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// The CSS value.
        /// </summary>
        [Browsable(true)]
        [Category("CSS")]
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
}
