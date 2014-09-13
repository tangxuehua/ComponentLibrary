using System;
using System.Web;
using System.Web.UI;

namespace NetFocus.Components.SearchComponent
{
	public class Script : LiteralControl {

		public virtual String Src {
			get {
				Object state = ViewState["Src"];
				return state == null ? string.Empty : (string)state;
			}
			set {
				ViewState["Src"] = value;
			}
		}
        
        const string srcFormat = "<script src=\"{0}\" type=\"text/javascript\"></script>";

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(srcFormat, Src);
        }


	}
}