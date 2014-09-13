
using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace NetFocus.Components.SearchComponent
{
	[
	ParseChildren( true ),
	PersistChildren( false ),
	]
	public abstract class TemplatedWebControl : WebControl, INamingContainer 
	{
		private string controlFileName;

		public string ControlFileName 
		{
			get 
			{
				return controlFileName;
			}
			set 
			{
				controlFileName = value;
			}
		}

		public override ControlCollection Controls 
		{
			get 
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		public override void DataBind() 
		{
			this.EnsureChildControls();
			base.DataBind ();
		}


		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			//we don't need a span tag
		}
		   
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			//we don't need a span tag
		}
		
		public override Control FindControl(string id) 
		{
			Control ctrl = base.FindControl(id);
			if (ctrl == null && this.Controls.Count == 1) 
			{
				ctrl = this.Controls[0].FindControl(id);
			}
			return ctrl;

		}

		public virtual String DataPath 
		{
			get 
			{
				Object state = ViewState["DataPath"];
				return state == null ? string.Empty : (string)state;
			}
			set 
			{
				ViewState["DataPath"] = value;
			}
		}
		
		protected override void CreateChildControls() 
		{
			DataPath = Page.Request.Params["datapath"];
			Control control = Page.LoadControl(DataPath + @"controls/" + ControlFileName);
			if(control != null)
			{
				this.Controls.Add(control);
			}
			AttachChildControls();

		}

		
		protected abstract void AttachChildControls();

	}
}
