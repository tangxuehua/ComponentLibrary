using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using NetFocus.Components.SearchComponent;

namespace test
{
	public class _default : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button Button4;
		protected System.Web.UI.WebControls.Button button1;
		protected NetFocus.Components.SearchComponent.WebSearchComponent c1;

	
		private void Page_Load(object sender, System.EventArgs e)
		{

		}


		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.button1.Click += new System.EventHandler(this.button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button4.Click += new System.EventHandler(this.Button4_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.c1.ControlUseType = UseType.ItemKindManage;
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			this.c1.ControlUseType = UseType.ItemManage;
		}

		private void Button4_Click(object sender, System.EventArgs e)
		{
			this.c1.ControlUseType = UseType.ItemDesign;
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			this.c1.ControlUseType = UseType.ItemSearch;
		}



	}
}
