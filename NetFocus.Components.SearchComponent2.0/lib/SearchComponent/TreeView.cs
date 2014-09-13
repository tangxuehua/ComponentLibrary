
using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace NetFocus.Components.SearchComponent
{
	public class TreeView : TemplatedWebControl
	{
		private string nodeString = string.Empty;
		private string datapath;
		private string physicalpath;
		private UseType useType = UseType.ItemDesign;

		protected override void AttachChildControls()
		{
			HtmlGenericControl treeView = (HtmlGenericControl)FindControl("treeViewServer");
			datapath = Page.Request.Params["datapath"];
			string useTypeString = Page.Request.Params["usetype"];
			if(Page.Request.Params["usetype"] != null)
			{
				useType = (UseType)Enum.Parse(typeof(UseType),useTypeString);
			}
			physicalpath = Page.Request.Params["physicalpath"];
			QueryKindManager.Instance.DataPath = physicalpath;
			QueryItemManager.Instance.DataPath = physicalpath;
			InitTreeView(treeView);
		}

		
		private void SetNodeString(string queryKindIdString)
		{
			if(useType == UseType.ItemKindManage)
			{
				DataTable queryKindTable = QueryKindManager.Instance.RetrieveQueryKindByParentId(queryKindIdString);
						
				foreach(DataRow row in queryKindTable.Rows)
				{
					if(queryKindIdString == "-1")
					{
						nodeString += "tree.nodes['" + "1" + "_" + row["id"].ToString() + "'] = '";
					}
					else
					{
						nodeString += "tree.nodes['" + queryKindIdString + "_" + row["id"].ToString() + "'] = '";
					}
					nodeString += "id:" + row["id"].ToString() + ";";
					nodeString += "text:" + row["name"].ToString() + ";";
					nodeString += "hint:" + row["description"].ToString() + ";";
					nodeString += "target:mainFrame;";
					nodeString += "url:ItemKindManage.aspx?datapath=" + datapath + "&queryKindId=" + row["id"].ToString() + ";";
					nodeString += "';";
					SetNodeString(row["id"].ToString());
				}
			}
			else
			{
				DataTable queryKindTable = QueryKindManager.Instance.RetrieveQueryKindByParentId(queryKindIdString);

				foreach(DataRow row in queryKindTable.Rows)
				{
					if(queryKindIdString == "-1")
					{
						nodeString += "tree.nodes['" + "1" + "_" + row["id"].ToString() + "'] = '";
					}
					else
					{
						nodeString += "tree.nodes['" + queryKindIdString + "_" + row["id"].ToString() + "'] = '";
					}
					nodeString += "id:" + row["id"].ToString() + ";";
					nodeString += "text:" + row["name"].ToString() + ";";
					nodeString += "hint:" + row["description"].ToString() + ";";
					if(useType == UseType.ItemManage)
					{
						nodeString += "target:mainFrame;";
						nodeString += "url:ItemManage.aspx?datapath=" + datapath + "&queryKindId=" + row["id"].ToString() + ";";
					}
					nodeString += "';";
					SetNodeString(row["id"].ToString());
				}

				DataTable queryItemTable = QueryItemManager.Instance.RetrieveQueryItemByKindId(queryKindIdString);

				foreach(DataRow row in queryItemTable.Rows)
				{
					if(queryKindIdString == "-1")
					{
						nodeString += "tree.nodes['" + "1" + "_" + row["id"].ToString() + "'] = '";
					}
					else
					{
						nodeString += "tree.nodes['" + queryKindIdString + "_" + row["id"].ToString() + "'] = '";
					}
					nodeString += "id:" + row["id"].ToString() + ";";
					nodeString += "text:" + row["name"].ToString() + ";";
					nodeString += "hint:" + row["description"].ToString() + ";";
					nodeString += "target:mainFrame;";
					if(useType == UseType.ItemDesign)
					{
						nodeString += "url:itemDesign.aspx?datapath=" + datapath + "&queryItemId=" + row["id"].ToString() + ";";
					}
					else if(useType == UseType.ItemSearch)
					{
						nodeString += "url:itemSearch.aspx?datapath=" + datapath + "&queryItemId=" + row["id"].ToString() + ";";
					}
					nodeString += "';";
				}
			}
		}
		private void InitTreeView(HtmlGenericControl treeView)
		{
			string fullScript = "";
			nodeString += "tree.nodes['0_1'] = 'text:所有查询分类;";
			nodeString += "target:mainFrame;";
			if(useType == UseType.ItemKindManage)
			{
				nodeString += "url:ItemKindManage.aspx?datapath=" + datapath + "&queryKindId=-1';";
			}
			else if(useType == UseType.ItemManage)
			{
				nodeString += "url:ItemManage.aspx?datapath=" + datapath + "&queryKindId=-1';";
			}
			else if(useType == UseType.ItemDesign)
			{
				nodeString += "url:ItemDesign.aspx?datapath=" + datapath + "&queryKindId=-1';";
			}
			else if(useType == UseType.ItemSearch)
			{
				nodeString += "url:ItemSearch.aspx?datapath=" + datapath + "&queryKindId=-1';";
			}
			SetNodeString("-1");
			fullScript = 
				"<SCRIPT LANGUAGE='JavaScript'>" + 
					"var tree = new MzTreeView('tree');" + 
					"tree.setIconPath('../images/');" + nodeString + 
					"document.getElementById('treeView1').innerHTML = tree.toString();" + 
				"</SCRIPT>";

			treeView.InnerHtml = fullScript;
		}
	}
}
