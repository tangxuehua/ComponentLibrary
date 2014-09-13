using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;


namespace NetFocus.Components.SearchComponent
{
	public class ItemManage : TemplatedWebControl 
	{
		DataList queryItemDataList = null;
		DropDownList queryItemDropDownList = null;
		TextBox queryItemTextBox = null;
		TextBox queryItemDescriptionTextbox = null;
		Button createQueryItemButton = null;
		Button updateQueryItemButton = null;
		Button deleteQueryItemButton = null;

		public virtual String KindId 
		{
			get 
			{
				Object state = ViewState["KindId"];
				return state == null ? string.Empty : (string)state;
			}
			set 
			{
				ViewState["KindId"] = value;
			}
		}

		protected override void AttachChildControls()
		{
			this.queryItemDataList = FindControl("queryItemDataList") as DataList;
			this.queryItemDropDownList = FindControl("queryItemDropDownList") as DropDownList;
			this.queryItemDescriptionTextbox = FindControl("queryItemDescriptionTextbox") as TextBox;
			this.queryItemTextBox = FindControl("queryItemTextBox") as TextBox;
			this.createQueryItemButton = FindControl("createQueryItemButton") as Button;
			this.updateQueryItemButton = FindControl("updateQueryItemButton") as Button;
			this.deleteQueryItemButton = FindControl("deleteQueryItemButton") as Button;

			this.createQueryItemButton.Click += new EventHandler(createQueryItemButton_Click);
			this.updateQueryItemButton.Click += new EventHandler(updateQueryItemButton_Click);
			this.deleteQueryItemButton.Click += new EventHandler(deleteQueryItemButton_Click);

			this.queryItemDataList.ItemDataBound += new DataListItemEventHandler(queryItemDataList_ItemDataBound);

			BindData();
			
		}

		private void queryItemDataList_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Image queryItemImage = e.Item.FindControl("queryItemImage") as Image;
				queryItemImage.ImageUrl = DataPath + "images/queryItemIcon.jpg";

			}
		}

		private void BindData()
		{
			string queryKindId = Page.Request.Params["queryKindId"];
			if(queryKindId == null || queryKindId == string.Empty)
			{
				queryKindId = "-1";
			}
			KindId = queryKindId;
			
			DataTable queryItemTable = QueryItemManager.Instance.RetrieveQueryItemByKindId(queryKindId);
			this.queryItemDataList.DataSource = queryItemTable;
			this.queryItemDataList.DataBind();

			DataTable queryItemTable1 = QueryItemManager.Instance.RetrieveQueryItemByKindId(queryKindId);
			this.queryItemDropDownList.DataSource = queryItemTable1;
			this.queryItemDropDownList.DataTextField = "name";
			this.queryItemDropDownList.DataValueField = "id";
			this.queryItemDropDownList.DataBind();
		}


		private void createQueryItemButton_Click(object sender, System.EventArgs e)
		{
			if(this.queryItemTextBox.Text.Trim() == "")
			{
				Page.Response.Write("<script language='javascript'>alert('查询项名称不能为空！');</script>");
				return;
			}

			string name = this.queryItemTextBox.Text.Trim();
			string description = this.queryItemDescriptionTextbox.Text.Trim();

			int result = QueryItemManager.Instance.CreateQueryItem(name,description,KindId);

			if(result == 0)
			{
				Page.Response.Write("<script language='javascript'>alert('已经存在该查询项！');</script>");
				return;
			}

			BindData();
		}

		private void updateQueryItemButton_Click(object sender, System.EventArgs e)
		{
			if(this.queryItemTextBox.Text.Trim() == "")
			{
				Page.Response.Write("<script language='javascript'>alert('查询项名称不能为空！');</script>");
				return;
			}

			if(this.queryItemDropDownList.SelectedValue == null)
			{
				Page.Response.Write("<script language='javascript'>alert('请选择一个要更新的查询项！');</script>");
			}

			string queryItemId = this.queryItemDropDownList.SelectedValue;
			string name = this.queryItemTextBox.Text.Trim();
			string description = this.queryItemDescriptionTextbox.Text.Trim();

			int result = QueryItemManager.Instance.UpdateQueryItem(queryItemId,name,description,KindId);

			if(result == 0)
			{
				Page.Response.Write("<script language='javascript'>alert('已经存在该查询项！');</script>");
				return;
			}

			BindData();
		}

		private void deleteQueryItemButton_Click(object sender, System.EventArgs e)
		{
			string queryItemId = this.queryItemDropDownList.SelectedValue;
			if(queryItemId != null)
			{
				QueryItemManager.Instance.DeleteQueryItem(queryItemId);

				BindData();
			}
			else
			{
				Page.Response.Write("<script language='javascript'>alert('请选择一个要删除的查询项！');</script>");
			}
		}


	}
}