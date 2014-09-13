//------------------------------------------------------------------------------
// Copyright (c) 2000-2003 Microsoft Corporation. All Rights Reserved.
//------------------------------------------------------------------------------

namespace NetFocus.Components.WebControls
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Collections;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Text.RegularExpressions;

    /// <summary>
    ///  Utility class with various useful static functions.
    /// </summary>
    public class Util
    {
        private static System.Resources.ResourceManager _ResourceManager = null;

        public static System.Resources.ResourceManager GetResourceManager()
        {
            if (_ResourceManager == null)
            {
                Type ourType = typeof(Util);
                _ResourceManager = new System.Resources.ResourceManager(ourType.Namespace + ".Resources.Resources",ourType.Module.Assembly);
            }
			
            return _ResourceManager;
        }

        
		internal static string GetStringResource(string name)
        {
            return (string)GetResourceManager().GetObject(name);
        }

        /// <summary>
        ///  Converts a FontUnit to a size for the HTML FONT tag
        /// </summary>
        internal static string ConvertToHtmlFontSize(FontUnit fu)
        {
            if ((int)(fu.Type) > 3)
            {
                return ((int)(fu.Type)-3).ToString();
            }

            if (fu.Type == FontSize.AsUnit) {
                if (fu.Unit.Type == UnitType.Point) {
                    if (fu.Unit.Value <= 8)
                        return "1";
                    else if (fu.Unit.Value <= 10)
                        return "2";
                    else if (fu.Unit.Value <= 12)
                        return "3";
                    else if (fu.Unit.Value <= 14)
                        return "4";
                    else if (fu.Unit.Value <= 18)
                        return "5";
                    else if (fu.Unit.Value <= 24)
                        return "6";
                    else
                        return "7";
                }
            }

            return null;
        }

        /// <summary>
        ///  Searches for an object's parents for a Form object
        /// </summary>
        internal static Control FindForm(Control child)
        {
            Control parent = child;

            while (parent != null)
            {
                if (parent is HtmlForm)
                    break;

                parent = parent.Parent;
            }

            return parent;
        }

        /// <summary>
        /// Given a string that contains a number, extracts the substring containing the number.
        /// Returns only the first number.
        /// Example: "5px" returns "5"
        /// </summary>
        /// <param name="str">The string containing the number.</param>
        /// <returns>The extracted number or String.Empty.</returns>
        internal static string ExtractNumberString(string str)
        {
            string[] strings = ExtractNumberStrings(str);
            if (strings.Length > 0)
            {
                return strings[0];
            }

            return String.Empty;
        }

        /// <summary>
        /// Extracts all numbers from the string.
        /// </summary>
        /// <param name="str">The string containing numbers.</param>
        /// <returns>An array of the numbers as strings.</returns>
        internal static string[] ExtractNumberStrings(string str)
        {
            if (str == null)
            {
                return new string[0];
            }

            // Match the digits
            MatchCollection matches = Regex.Matches(str, "(\\d+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Move to a string array
            string[] strings = new string[matches.Count];
            int index = 0;

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    strings[index] = match.Value;
                }
                else
                {
                    strings[index] = String.Empty;
                }

                index++;
            }

            return strings;
        }

        /// <summary>
        ///  Converts a color string to a hex value string ("Green" -> "#000800")
        /// </summary>
        internal static string ColorToHexString(string color)
        {
            if (color[0] == '#')
            {
                return color;
            }

            Color c = ColorTranslator.FromHtml(color);
            return ColorToHexString(c);
        }

        /// <summary>
        ///  Converts a Color to a hex value string (Color.Green -> "#000800")
        /// </summary>
        internal static string ColorToHexString(Color c)
        {
            string r = Convert.ToString(c.R, 16);
            if (r.Length < 2)
                r = "0" + r;
            string g = Convert.ToString(c.G, 16);
            if (g.Length < 2)
                g = "0" + g;
            string b = Convert.ToString(c.B, 16);
            if (b.Length < 2)
                b = "0" + b;

            string str = "#" + r + g + b;
            return str;
        }
    }
}
