using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.Web;
using NetFocus.Components.WebControls;

namespace NetFocus.Components.SearchComponent
{
	/// <summary>
	/// 描述可使用该组件的几种情况:ItemManage表示对查询项和查询项类别的管理
	/// ItemDesign表示设计查询项的查询SQL语句,ItemSearch表示为某个查询项输入条件并进行查询
	/// </summary>
	public enum UseType
	{
		ItemManage,
		ItemKindManage,
		ItemDesign,
		ItemSearch
	}


	public class WebSearchComponent : WebControl
	{
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue("data"),
		Description("运行查询组件所必须的文件的相对目录")
		]
		public string DataPath 
		{
			get 
			{ 
				object dataPath = this.ViewState["DataPath"];
				return HttpContext.Current.Request.ApplicationPath + ((dataPath == null) ? "" : (string)dataPath);
			}
			set 
			{
				if(!value.StartsWith("/"))
				{
					value = @"/" + value;
				}
				if(!value.EndsWith("/"))
				{
					value = value + @"/";
				}
				ViewState["DataPath"] = value;
			}
		}


		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue("ItemDesign"),
		Description("通过该属性为组件指定显示的界面")
		]
		public UseType ControlUseType
		{
			get
			{
				Object state = ViewState["UseType"];
				return state == null ? UseType.ItemDesign : (UseType)state;
			}
			set
			{
				ViewState["UseType"] = value;
			}
		}


		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			QueryKindManager.Instance.DeleteEvent -= new DeleteQueryKindEventHandler(QueryItemManager.Instance.DeleteQueryItems);
			QueryKindManager.Instance.DeleteEvent += new DeleteQueryKindEventHandler(QueryItemManager.Instance.DeleteQueryItems);
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "1");

			// Render an id on the outer tag.
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.RenderBeginTag(HtmlTextWriterTag.Table);

			writer.RenderBeginTag(HtmlTextWriterTag.Tr);

			writer.RenderBeginTag(HtmlTextWriterTag.Td);

			string s ="<iframe src=\"" + DataPath + "pages/treeView.aspx?physicalpath="
				+ HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + ViewState["DataPath"])
				+ "&datapath=" + DataPath + "&usetype=" + ControlUseType + "\" marginheight=\"0\" marginwidth=\"0\" name=\"leftFrame\" "
				+ "width=\"200\" height=\"480\" frameBorder=\"0\" scrolling=\"auto\" id=\"leftFrame\"></iframe>";
			writer.Write(s);    
			writer.RenderEndTag();  // Td

			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			
			if(ControlUseType == UseType.ItemDesign)
			{
				s = "<iframe src=\"" + DataPath + "pages/itemDesign.aspx?physicalpath="
					+ HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + ViewState["DataPath"])
					+ "&datapath=" + DataPath + "\" marginheight=\"0\" marginwidth=\"0\" name=\"mainFrame\" "
					+ "width=\"580\" height=\"480\" frameBorder=\"0\" scrolling=\"auto\" id=\"mainFrame\"></iframe>";
			}
			if(ControlUseType == UseType.ItemKindManage)
			{
				s = "<iframe src=\"" + DataPath + "pages/itemKindManage.aspx?physicalpath="
					+ HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + ViewState["DataPath"])
					+ "&datapath=" + DataPath + "\" marginheight=\"0\" marginwidth=\"0\" name=\"mainFrame\" "
					+ "width=\"580\" height=\"480\" frameBorder=\"0\" scrolling=\"auto\" id=\"mainFrame\"></iframe>";
			}
			if(ControlUseType == UseType.ItemManage)
			{
				s = "<iframe src=\"" + DataPath + "pages/itemManage.aspx?physicalpath="
					+ HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + ViewState["DataPath"])
					+ "&datapath=" + DataPath + "\" marginheight=\"0\" marginwidth=\"0\" name=\"mainFrame\" "
					+ "width=\"580\" height=\"480\" frameBorder=\"0\" scrolling=\"auto\" id=\"mainFrame\"></iframe>";
			}
			if(ControlUseType == UseType.ItemSearch)
			{
				s = "<iframe src=\"" + DataPath + "pages/itemSearch.aspx?physicalpath="
					+ HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + ViewState["DataPath"])
					+ "&datapath=" + DataPath + "\" marginheight=\"0\" marginwidth=\"0\" name=\"mainFrame\" "
					+ "width=\"580\" height=\"480\" frameBorder=\"0\" scrolling=\"auto\" id=\"mainFrame\"></iframe>";
			}
			writer.Write(s);   
			writer.RenderEndTag();  // Td

			writer.RenderEndTag();  // Tr
			writer.RenderEndTag();  // Table 

			base.Render (writer);

		}


	}
}
