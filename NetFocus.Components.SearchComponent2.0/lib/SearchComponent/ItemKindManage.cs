using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;


namespace NetFocus.Components.SearchComponent
{
	public class ItemKindManage : TemplatedWebControl 
	{
		DataList queryKindDataList = null;
		DropDownList queryKindDropDownList = null;
		TextBox queryKindTextBox = null;
		TextBox queryKindDescriptionTextbox = null;
		Button createQueryKindButton = null;
		Button updateQueryKindButton = null;
		Button deleteQueryKindButton = null;

		public virtual String ParentId 
		{
			get 
			{
				Object state = ViewState["ParentId"];
				return state == null ? string.Empty : (string)state;
			}
			set 
			{
				ViewState["ParentId"] = value;
			}
		}

		protected override void AttachChildControls()
		{
			this.queryKindDataList = FindControl("queryKindDataList") as DataList;
			this.queryKindDropDownList = FindControl("queryKindDropDownList") as DropDownList;
			this.queryKindDescriptionTextbox = FindControl("queryKindDescriptionTextbox") as TextBox;
			this.queryKindTextBox = FindControl("queryKindTextBox") as TextBox;
			this.createQueryKindButton = FindControl("createQueryKindButton") as Button;
			this.updateQueryKindButton = FindControl("updateQueryKindButton") as Button;
			this.deleteQueryKindButton = FindControl("deleteQueryKindButton") as Button;

			this.createQueryKindButton.Click += new EventHandler(createQueryKindButton_Click);
			this.updateQueryKindButton.Click += new EventHandler(updateQueryKindButton_Click);
			this.deleteQueryKindButton.Click += new EventHandler(deleteQueryKindButton_Click);

			this.queryKindDataList.ItemDataBound += new DataListItemEventHandler(queryKindDataList_ItemDataBound);

			BindData();			
		}

		private void queryKindDataList_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Image queryKindImage = e.Item.FindControl("queryKindImage") as Image;
				HyperLink queryKindHyperLink = e.Item.FindControl("queryKindHyperLink") as HyperLink;
				Label queryKindIdLabel = e.Item.FindControl("queryKindIdLabel") as Label;

				queryKindImage.ImageUrl = DataPath + "images/queryKindIcon.jpg";
				queryKindHyperLink.NavigateUrl = DataPath + "pages/itemKindManage.aspx?datapath=" + 
					DataPath + "&queryKindId=" + queryKindIdLabel.Text;
			}
		}

		private void BindData()
		{
			string queryKindId = Page.Request.Params["queryKindId"];
			if(queryKindId == null || queryKindId == string.Empty)
			{
				queryKindId = "-1";
			}
			ParentId = queryKindId;

			DataTable queryKindTable = QueryKindManager.Instance.RetrieveQueryKindByParentId(queryKindId);
			this.queryKindDataList.DataSource = queryKindTable;
			this.queryKindDataList.DataBind();

			DataTable queryKindTable1 = QueryKindManager.Instance.RetrieveQueryKindByParentId(queryKindId);
			this.queryKindDropDownList.DataSource = queryKindTable1;
			this.queryKindDropDownList.DataTextField = "name";
			this.queryKindDropDownList.DataValueField = "id";
			this.queryKindDropDownList.DataBind();

		}


		private void createQueryKindButton_Click(object sender, System.EventArgs e)
		{
			if(this.queryKindTextBox.Text.Trim() == "")
			{
				Page.Response.Write("<script language='javascript'>alert('类别名称不能为空！');</script>");
				return;
			}

			string queryKindId = Guid.NewGuid().ToString();
			string name = this.queryKindTextBox.Text.Trim();
			string description = this.queryKindDescriptionTextbox.Text.Trim();
			DateTime createTime = DateTime.Now;

			int result = QueryKindManager.Instance.CreateQueryKind(queryKindId,name,description,createTime,ParentId);

			if(result == 0)
			{
				Page.Response.Write("<script language='javascript'>alert('已经存在该类别！');</script>");
				return;
			}

			BindData();

		}

		private void deleteQueryKindButton_Click(object sender, System.EventArgs e)
		{
			string queryKindId = this.queryKindDropDownList.SelectedValue;
			if(queryKindId != null)
			{
				QueryKindManager.Instance.DeleteQueryKind(queryKindId);

				BindData();
			}
			else
			{
				Page.Response.Write("<script language='javascript'>alert('请选择一个要删除的类别！');</script>");
			}
		}

		private void updateQueryKindButton_Click(object sender, System.EventArgs e)
		{
			if(this.queryKindTextBox.Text.Trim() == "")
			{
				Page.Response.Write("<script language='javascript'>alert('类别名称不能为空！');</script>");
				return;
			}

			if(this.queryKindDropDownList.SelectedValue == null)
			{
				Page.Response.Write("<script language='javascript'>alert('请选择一个要更新的类别！');</script>");
			}

			string queryKindId = this.queryKindDropDownList.SelectedValue;
			string name = this.queryKindTextBox.Text.Trim();
			string description = this.queryKindDescriptionTextbox.Text.Trim();

			int reuslt = QueryKindManager.Instance.UpdateQueryKind(queryKindId,name,description,ParentId);

			if(reuslt == 0)
			{
				Page.Response.Write("<script language='javascript'>alert('已经存在该类别！');</script>");
				return;
			}

			BindData();
		}


	}
}