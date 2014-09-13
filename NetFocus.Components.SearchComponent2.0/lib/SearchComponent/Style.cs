using System;
using System.Web;
using System.Web.UI;
using System.ComponentModel;


namespace NetFocus.Components.SearchComponent
{
	public class Style : LiteralControl 
	{
		const string linkFormat = "<link rel=\"stylesheet\" href=\"{0}\" type=\"text/css\" media=\"{1}\" />";

		[DefaultValue( "screen" )]
		public virtual String Media {
			get {
				Object state = ViewState["Media"];
				if ( state != null ) {
					return (String)state;
				}
				return "screen";
			}
			set {
				ViewState["Media"] = value;
			}
		}

		public virtual String Href {
			get {
				Object state = ViewState["Href"];
				return state == null ? string.Empty : (string)state;
			}
			set {
				ViewState["Href"] = value;
			}
		}
        

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(linkFormat,Href,Media);
        }

	}
}