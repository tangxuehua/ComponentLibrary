//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Collections.Specialized;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Text.RegularExpressions;
    using System.Reflection;

    /// <summary>
    /// Represents a collection of CSS attributes and their values.
    /// </summary>
    [
    TypeConverterAttribute(typeof(CssCollectionConverter)),
    Editor(typeof(NetFocus.Components.WebControls.Design.CssCollectionEditor), typeof(UITypeEditor)),
    ]
    public class CssCollection : NameObjectCollectionBase, ICloneable, IStateManager, IList
    {
        /// <summary>
        /// Event called when an item is added.
        /// </summary>
        public event CssEventHandler ItemAdded;

        /// <summary>
        /// Event called when an item is removed.
        /// </summary>
        public event CssEventHandler ItemRemoved;

        /// <summary>
        /// Event called when the collection is cleared.
        /// </summary>
        public event EventHandler Cleared;

        private bool _bRenderFontTag = false;   // Tracks if a begin FONT tag was rendered
        private bool _bRenderBoldTag = false;   // Tracks if a begin B tag was rendered
        private bool _bRenderItalicTag = false; // Trakcs if a begin I tag was rendered

        private bool _IsTrackingViewState = false;
        private bool _Dirty;

        // Taken from System.Web.UI.CssStyleCollection
        private static readonly Regex _StyleAttribRegex = new Regex(
            "\\G(\\s*" +                // any leading spaces
            "(?(stylename);\\s*)" +     // if stylename was already matched, 
            // match a semicolon and spaces
            "(?<stylename>[^:]+?)" +    // match stylename - chars up to the semicolon
            "\\s*:\\s*" +               // spaces, then the colon, then more spaces
            "(?<styleval>[^;]+?)" +     // now match styleval
            ")*\\s*;?\\s*$",            // match a trailing semicolon and trailing spaces
            RegexOptions.Singleline | 
            RegexOptions.Multiline);

        /// <summary>
        /// Initializes a new instance of a CssCollection.
        /// </summary>
        public CssCollection() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of a CssCollection.
        /// </summary>
        /// <param name="col">A collection to initialize this collection with.</param>
        public CssCollection(CssCollection col) : base()
        {
            Merge(col, true);
        }

        /// <summary>
        /// Initializes a new instance of a CssCollection with an CssStyleCollection.
        /// </summary>
        /// <param name="style">A CssStyleCollection to initialize the collection.</param>
        public CssCollection(CssStyleCollection style) : base()
        {
            Merge(style, true);
        }

        /// <summary>
        /// Initializes a new instance of a CssCollection.
        /// </summary>
        /// <param name="cssText">A CSS string to initialize this collection with.</param>
        public CssCollection(string cssText) : base()
        {
            CssText = cssText;
        }

        /// <summary>
        /// Creates a CssCollection from a string.
        /// </summary>
        /// <param name="cssText">The CSS text string.</param>
        /// <returns>A new CssCollection.</returns>
        public static CssCollection FromString(string cssText)
        {
            return new CssCollection(cssText);
        }

        /// <summary>
        /// Clones this collection.
        /// </summary>
        /// <returns>A copy of this collection.</returns>
        public virtual object Clone()
        {
            CssCollection col = (CssCollection)Activator.CreateInstance(this.GetType(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[] { this }, null);

            col._bRenderFontTag = this._bRenderFontTag;
            col._bRenderBoldTag = this._bRenderBoldTag;
            col._bRenderItalicTag = this._bRenderItalicTag;

            col._IsTrackingViewState = this._IsTrackingViewState;
            col._Dirty = this._Dirty;

            col.ItemAdded = this.ItemAdded;
            col.ItemRemoved = this.ItemRemoved;
            col.Cleared = this.Cleared;

            return col;
        }

        /// <summary>
        /// Adds a new CSS attribute/value pair.
        /// </summary>
        /// <param name="name">The CSS attribute name.</param>
        /// <param name="value">The CSS value.</param>
        public void Add(string name, string value)
        {
            name = name.ToLower().Trim();

            if (this[name] != null)
                BaseRemove(name);

            BaseAdd((string)name.Clone(), value.Clone());

            Dirty = true;

            if (ItemAdded != null)
            {
                CssEventArgs e = new CssEventArgs(name, value);
                ItemAdded(this, e);
            }
        }

        /// <summary>
        /// Clears this collection.
        /// </summary>
        public void Clear()
        {
            BaseClear();

            Dirty = true;

            if (Cleared != null)
            {
                Cleared(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Removes a CSS attribute.
        /// </summary>
        /// <param name="name">The CSS attribute name.</param>
        public void Remove(string name)
        {
            string val = this[name];
            BaseRemove(name.ToLower());

            Dirty = true;

            if (ItemRemoved != null)
            {
                CssEventArgs e = new CssEventArgs(name, val);
                ItemRemoved(this, e);
            }
        }

        /// <summary>
        /// Merges a CssCollection into this one.
        /// </summary>
        /// <param name="src">The source collection.</param>
        /// <param name="overwrite">If true, will overwrite attributes of the same name.</param>
        public void Merge(CssCollection src, bool overwrite)
        {
            foreach (string name in src)
            {
                if ((this[name] == null) || (overwrite))
                {
                    Add(name, src[name]);
                }
            }
        }

        /// <summary>
        /// Merges a CssStyleCollection into this one.
        /// </summary>
        /// <param name="src">The source collection.</param>
        /// <param name="overwrite">If true, will overwrite attributes of the same name.</param>
        public void Merge(CssStyleCollection src, bool overwrite)
        {
            foreach (string name in src.Keys)
            {
                if ((this[name] == null) || (overwrite))
                {
                    Add(name, src[name]);
                }
            }
        }

        /// <summary>
        /// Merges this collection into a CssStyleCollection.
        /// </summary>
        /// <param name="dest">The destination collection.</param>
        /// <param name="overwrite">If true, will overwrite attributes of the same name.</param>
        public void MergeInto(CssStyleCollection dest, bool overwrite)
        {
            foreach (string name in this)
            {
                if (dest[name] == null)
                {
                    dest.Add(name, this[name]);
                }
                else if (overwrite)
                {
                    dest[name] = this[name];
                }
            }
        }

        /// <summary>
        /// Indexer into this collection.
        /// </summary>
        [Browsable(false)]
        public string this[string name]
        {
            get { return (name == null) ? null : (string)BaseGet(name.ToLower()); }
            set { Add(name, value); }
        }

        /// <summary>
        /// Indexer into this collection.
        /// </summary>
        [Browsable(false)]
        public string this[int index]
        {
            get { return this[Keys[index]]; }
            set { this[Keys[index]] = value; }
        }

        /// <summary>
        /// Gets the string version of this collection.
        /// </summary>
        [Browsable(false)]
        public string CssText
        {
            get
            {
                string szCss = String.Empty;

                foreach (string name in this)
                {
                    szCss += name + ":" + this[name] + ";";
                }

                return szCss;
            }

            set
            {
                Clear();
                AppendCssText(value);
            }
        }

        /// <summary>
        /// Appends a CSS text string to this collection.
        /// </summary>
        /// <param name="cssText">A CSS text string.</param>
        public void AppendCssText(string cssText)
        {
            if (cssText != null)
            {
                Match match;
                if ((match = _StyleAttribRegex.Match(cssText, 0)).Success) 
                {
                    CaptureCollection stylenames = match.Groups["stylename"].Captures;
                    CaptureCollection stylevalues = match.Groups["styleval"].Captures;

                    for (int i = 0; i < stylenames.Count; i++) 
                    {
                        String styleName = stylenames[i].ToString();
                        String styleValue = stylevalues[i].ToString();

                        Add(styleName, styleValue);
                    }
                }
            }
        }

        /// <summary>
        /// Converts this collection to a string.
        /// </summary>
        /// <returns>The string representation of this collection.</returns>
        public override string ToString()
        {
            return CssText;
        }

        /// <summary>
        /// Returns the color attribute.
        /// </summary>
        /// <returns>Color in Hex.</returns>
        public string GetColor()
        {
            string color = this["color"];
            if (color != null)
            {
                // Translate the string to a Color then translate it back
                // to HTML to get it to convert to a hex number
                try
                {
                    return Util.ColorToHexString(color);
                }
                catch
                {
                    // ignore
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the font weight.
        /// </summary>
        /// <returns>The font weight.</returns>
        public string GetFontWeight()
        {
            string szWeight = this["font-weight"];
            if (szWeight != null)
            {
                return szWeight.ToLower();
            }
            return null;
        }

        /// <summary>
        /// Gets the font style.
        /// </summary>
        /// <returns>The font style.</returns>
        public string GetFontStyle()
        {
            string szStyle = this["font-style"];
            if (szStyle != null)
            {
                return szStyle.ToLower();
            }
            return null;        
        }

        /// <summary>
        /// Gets the font face.
        /// </summary>
        /// <returns>The font face.</returns>
        public string GetFontFace()
        {
            string szName = this["font-family"];
            if (szName != null)
            {
                return szName;
            }

            return null;
        }

        /// <summary>
        /// Gets the font size.
        /// </summary>
        /// <returns>The font size.</returns>
        public string GetFontSize()
        {
            string szSize = this["font-size"];
            if (szSize != null)
            {
                FontUnit fu = new FontUnit(szSize);
                if (fu.Type == FontSize.NotSet)
                {
                    return szSize;
                }
                else
                {
                    return Util.ConvertToHtmlFontSize(fu);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets background color.
        /// </summary>
        /// <returns>The background color.</returns>
        public string GetBackColor()
        {
            string szBackColor = this["background-color"];
            if (szBackColor == null)
                szBackColor = this["background"];

            if (szBackColor == null)
                return null;

            return Util.ColorToHexString(szBackColor);
        }

        /// <summary>
        /// Detects if the border has been set.
        /// </summary>
        /// <returns>true if the border has been set.</returns>
        public bool IsBorderSet()
        {
            foreach (string key in Keys)
            {
                // Check for border*
                if ((key.IndexOf("border") == 0) && (key != String.Empty))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Renders a font tag.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter to receive the HTML.</param>
        public void RenderBeginFontTag(HtmlTextWriter writer)
        {
            bool bAttribAdded = false;
            string color = GetColor();
            string fontFace = GetFontFace();
            string fontSize = GetFontSize();

            if (color != null)
            {
                writer.AddAttribute("COLOR", color);
                bAttribAdded = true;
            }

            if (fontFace != null)
            {
                writer.AddAttribute("FACE", fontFace);
                bAttribAdded = true;
            }

            if (fontSize != null)
            {
                writer.AddAttribute("SIZE", fontSize);
                bAttribAdded = true;
            }

            if (bAttribAdded)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Font);
                _bRenderFontTag = true;
            }
        }

        /// <summary>
        /// Renders the close tag.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter to receive the HTML.</param>
        public void RenderEndFontTag(HtmlTextWriter writer)
        {
            if (_bRenderFontTag)
                writer.RenderEndTag();
        }

        /// <summary>
        /// Renders bold and italic tags.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter to receive the HTML.</param>
        public void RenderBeginModalTags(HtmlTextWriter writer)
        {
            string fontWeight = GetFontWeight();
            string fontStyle = GetFontStyle();

            if (fontWeight == "bold" || fontWeight == "bolder" || fontWeight == "700" || fontWeight == "800" || fontWeight == "900")
            {
                writer.RenderBeginTag(HtmlTextWriterTag.B);
                _bRenderBoldTag = true;
            }

            if (fontStyle == "italic" || fontStyle == "oblique")
            {
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                _bRenderItalicTag = true;
            }
        }

        /// <summary>
        /// Closes tags.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter to receive the HTML.</param>
        public void RenderEndModalTags(HtmlTextWriter writer)
        {
            if (_bRenderItalicTag)
                writer.RenderEndTag();

            if (_bRenderBoldTag)
                writer.RenderEndTag();
        }

        /// <summary>
        /// Adds the style attributes to the HtmlTextWriter.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter that will receive the style attributes.</param>
        public void AddAttributesToRender(HtmlTextWriter writer)
        {
            foreach (string name in this)
            {
                writer.AddStyleAttribute(name, this[name]);
            }
            if (writer is Html32TextWriter)
            {
                string cssText = CssText;
                if (cssText != String.Empty)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, cssText);
                }
            }
        }

        /// <summary>
        /// Loads the collection's previously saved view state.
        /// </summary>
        /// <param name="state">An Object that contains the saved view state values for the collection.</param>
        void IStateManager.LoadViewState(object state)
        {
            if (state != null)
            {
                CssText = (string)state;
            }
        }

        /// <summary>
        /// Saves the changes to the collection's view state to an Object.
        /// </summary>
        /// <returns>The Object that contains the view state changes.</returns>
        object IStateManager.SaveViewState()
        {
            if (_Dirty)
            {
                return CssText;
            }

            return null;
        }

        /// <summary>
        /// Instructs the collection to track changes to its view state.
        /// </summary>
        void IStateManager.TrackViewState()
        {
            _IsTrackingViewState = true;
        }

        /// <summary>
        /// Gets a value indicating whether the collection is tracking its view state changes.
        /// </summary>
        bool IStateManager.IsTrackingViewState
        {
            get { return _IsTrackingViewState; }
        }

        /// <summary>
        /// Gets a value indicating whether the collection is dirty. The collection needs
        /// to be tracking view state changes in order for this value to be anything other
        /// than false.
        /// </summary>
        protected internal virtual bool Dirty
        {
            get { return _Dirty; }

            set
            {
                if (((IStateManager)this).IsTrackingViewState)
                {
                    _Dirty = value;
                }
            }
        }

        /// <summary>
        /// Determines whether the specified Object is the same instance as the current Object.
        /// </summary>
        /// <param name="obj">The Object to compare with the current Object.</param>
        /// <returns>true if the values are equal, false otherwise.</returns>
        public override bool Equals(Object obj)
        {
            if (obj is CssCollection)
            {
                return CssText == ((CssCollection)obj).CssText;
            }

            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return CssText.GetHashCode();
        }

        /// <summary>
        /// Whether the collection can be expanded.
        /// </summary>
        bool IList.IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// Whether items in the collection can be changed.
        /// </summary>
        bool IList.IsReadOnly
        {
            get { return ((CssCollection)this).IsReadOnly; }
        }

        /// <summary>
        /// Index into the array of keys.
        /// </summary>
        object IList.this[int index]
        {
            get { return ((CssCollection)this)[index]; }
            set { ((CssCollection)this)[index] = (string)value; }
        }

        /// <summary>
        /// Adds a key with an empty value.
        /// </summary>
        /// <param name="value">The name of the key.</param>
        /// <returns>Index of the key in the key collection.</returns>
        int IList.Add(object value)
        {
            ((CssCollection)this).Add((string)value, String.Empty);
            return ((IList)this).IndexOf(value);
        }

        /// <summary>
        /// Clears the collection.
        /// </summary>
        void IList.Clear()
        {
            ((CssCollection)this).Clear();
        }

        /// <summary>
        /// Whether the value is in the collection.
        /// </summary>
        /// <param name="value">The key name to test for.</param>
        /// <returns>True if the key was found.</returns>
        bool IList.Contains(object value)
        {
            return ((CssCollection)this)[(string)value] != null;
        }

        /// <summary>
        /// The index of the key.
        /// </summary>
        /// <param name="value">The key to look for.</param>
        /// <returns>The index of the key or -1 if not found.</returns>
        int IList.IndexOf(object value)
        {
            int index = 0;
            foreach (string s in Keys)
            {
                if (s == (string)value)
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Inserts the key. The index is ignored.
        /// </summary>
        /// <param name="index">The index is ignored.</param>
        /// <param name="value">The key to add.</param>
        void IList.Insert(int index, object value)
        {
            ((IList)this).Add(value);
        }

        /// <summary>
        /// Removes the key from the collection.
        /// </summary>
        /// <param name="value">The key to remove.</param>
        void IList.Remove(object value)
        {
            ((CssCollection)this).Remove((string)value);
        }

        /// <summary>
        /// Removes the key at the specified index.
        /// </summary>
        /// <param name="index">The index of the key to remove.</param>
        void IList.RemoveAt(int index)
        {
            ((IList)this).Remove(((IList)this)[index]);
        }
    }

    /// <summary>
    /// Event arguments for CssCollection events.
    /// </summary>
    public class CssEventArgs : EventArgs
    {
        /// <summary>
        /// The name of the CSS pair
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The CSS value
        /// </summary>
        public readonly string Value;

        /// <summary>
        /// Initializes a new instance of CssEventArgs
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <param name="value">The value</param>
        public CssEventArgs(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// Event handler delegate for CssCollection events.
    /// </summary>
    public delegate void CssEventHandler(CssCollection c, CssEventArgs e);
}
