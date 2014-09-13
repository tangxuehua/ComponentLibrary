using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Xml;
using System.Web.SessionState;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NetFocus.Components.WebControls.Design;
using NetFocus.Components.WebControls;
using System.Collections.Specialized;


namespace NetFocus.Components.SearchComponent
{
	public class ItemDesign : TemplatedWebControl
	{
		#region some private variants

		//定义一些容器控件
		private TabStrip tabStrip1 = new TabStrip();
		private MultiPage pages = null;
		private PageView[] pageViewArray = new PageView[7];

		//定义验证数据源用户控件中的一些子控件
		private DropDownList dataSourceDriverTypeDropDownList = null;
		private TextBox dataSourceStringTextBox = null;
		private Label showErrorLabel = null;
		private Button submitDataSourceButton = null;
		private Button cancelDataSourceButton = null;

		//定义选择表用户控件中的一些子控件
		private DataList sourceTableDataList = null;
		private DataList selectedDataList = null;
		private Button selectTableSubmitButton = null;
		private Button updateSelectedTableButton = null;

		//定义建立表关系用户控件中的一些子控件
		private DropDownList relationFromTable1DropDownList = null;
		private DropDownList relationFromTable2DropDownList = null;
		private DropDownList relationField1Dropdownlist = null;
		private DropDownList relationField2Dropdownlist = null;
		private Button addJoinFieldButton = null;
		private ListBox currentSelectedJoinFieldListBox = null;
		private DropDownList joinTypeDropdownlist = null;
		private Button addRelationButton = null;
		private ListBox currentTableRelationListBox = null;

		//定义选择返回字段用户控件中的一些子控件
		private DataList sourceFieldDataList = null;
		private DataList selectedFieldDataList = null;
		private Button selectFieldSubmitButton = null;
		private Button updateSelectedFieldButton = null;

		//定义设置条件用户控件中的一些子控件
		private DropDownList conditionFieldDropDownList = null;
		private Label conditionFieldDataTypeLabel = null;
		private DropDownList inputValueControlTypeDropdownlist = null;
		private TextBox conditionNameTextBox = null;
		private TextBox complicatedConditionNameTextBox = null;
		private DropDownList currentAllConditionsDropdownlist1 = null;
		private DropDownList currentAllConditionsDropdownlist2 = null;
		private DropDownList andOrRelationDropdownlist = null;
		private Button addConditionButton = null;
		private Button addComplicatedConditionButton = null;
		private Button deleteConditionButton = null;
		private Button deleteComplicatedConditionButton = null;
		private DataList conditionsDataList = null;
		private DataList complicatedConditionsDatalist = null;
		private TextBox initialValueTextBox = null;
		private TextBox fieldChineseNameTextBox = null;
		private DropDownList operatorDropDownList = null;

		//定义设置排序用户控件中的一些子控件
		private DataList sourceSortFieldDataList = null;
		private DataList selectedSortFieldDataList = null;
		private Button selectSortFieldSubmitButton = null;
		private Button updateSelectedSortFieldButton = null;

		//定义查看SQL用户控件中的一些子控件
		private Button showSQLButton = null;
		private HtmlGenericControl showSQLDiv = null;
		private Table showSQLTable = null;


		#endregion

		#region some private customize data table properties

		private SelectedTableDataTable SelectedTables 
		{
			get 
			{
				if(ViewState["SelectedTables"] == null)
				{
					ViewState["SelectedTables"] = new SelectedTableDataTable();
				}
				return ViewState["SelectedTables"] as SelectedTableDataTable;
			}
			set 
			{
				ViewState["SelectedTables"] = value;
			}
		}

		private RelationFieldDataTable RelationFields 
		{
			get 
			{
				if(ViewState["RelationFields"] == null)
				{
					ViewState["RelationFields"] = new RelationFieldDataTable();
				}
				return ViewState["RelationFields"] as RelationFieldDataTable;
			}
			set 
			{
				ViewState["RelationFields"] = value;
			}
		}

		private Hashtable RelationTableHashTable 
		{
			get
			{
				if(ViewState["RelationTables"] == null)
				{
					ViewState["RelationTables"] = new Hashtable();
				}
				return ViewState["RelationTables"] as Hashtable;
			}
			set
			{
				ViewState["RelationTables"] = value;
			}
		}
		private TableFieldDataTable SourceFields 
		{
			get 
			{
				if(ViewState["SourceFields"] == null)
				{
					ViewState["SourceFields"] = new TableFieldDataTable();
				}
				return ViewState["SourceFields"] as TableFieldDataTable;
			}
			set 
			{
				ViewState["SourceFields"] = value;
			}
		}
		private TableFieldDataTable SourceFields1 
		{
			get 
			{
				if(ViewState["SourceFields1"] == null)
				{
					ViewState["SourceFields1"] = new TableFieldDataTable();
				}
				return ViewState["SourceFields1"] as TableFieldDataTable;
			}
			set 
			{
				ViewState["SourceFields1"] = value;
			}
		}
		private TableFieldDataTable SourceSortFields 
		{
			get 
			{
				if(ViewState["SourceSortFields"] == null)
				{
					ViewState["SourceSortFields"] = new TableFieldDataTable();
				}
				return ViewState["SourceSortFields"] as TableFieldDataTable;
			}
			set 
			{
				ViewState["SourceSortFields"] = value;
			}
		}
		private TableFieldDataTable SelectedFields 
		{
			get 
			{
				if(ViewState["SelectedFields"] == null)
				{
					ViewState["SelectedFields"] = new TableFieldDataTable();
				}
				return ViewState["SelectedFields"] as TableFieldDataTable;
			}
			set 
			{
				ViewState["SelectedFields"] = value;
			}
		}

		private SelectedSortFieldDataTable SelectedSortFields 
		{
			get 
			{
				if(ViewState["SelectedSortFields"] == null)
				{
					ViewState["SelectedSortFields"] = new SelectedSortFieldDataTable();
				}
				return ViewState["SelectedSortFields"] as SelectedSortFieldDataTable;
			}
			set 
			{
				ViewState["SelectedSortFields"] = value;
			}
		}

		private AtomConditionDataTable AtomConditions 
		{
			get 
			{
				if(ViewState["AtomConditions"] == null)
				{
					ViewState["AtomConditions"] = new AtomConditionDataTable();
				}
				return ViewState["AtomConditions"] as AtomConditionDataTable;
			}
			set 
			{
				ViewState["AtomConditions"] = value;
			}
		}

		private ComplicatedConditionDataTable ComplicatedConditions 
		{
			get 
			{
				if(ViewState["ComplicatedConditions"] == null)
				{
					ViewState["ComplicatedConditions"] = new ComplicatedConditionDataTable();
				}
				return ViewState["ComplicatedConditions"] as ComplicatedConditionDataTable;
			}
			set 
			{
				ViewState["ComplicatedConditions"] = value;
			}
		}


		#endregion

		protected override void AttachChildControls()
		{
			this.pages = this.Page.FindControl("itemDesign1").FindControl("MultiPage1") as MultiPage;
			for(int i = 1;i <= 7;i++)
			{
				this.pageViewArray[i-1] = this.pages.FindControl("page" + i.ToString()) as PageView;
			}

			//初始化验证数据源用户控件中的子控件
			this.dataSourceDriverTypeDropDownList = this.pageViewArray[0].FindControl("ValidateDataSource1").FindControl("dataSourceDriverTypeDropDownList") as DropDownList;
			this.dataSourceStringTextBox = this.pageViewArray[0].FindControl("ValidateDataSource1").FindControl("dataSourceStringTextBox") as TextBox;
			this.showErrorLabel = this.pageViewArray[0].FindControl("ValidateDataSource1").FindControl("showErrorLabel") as Label;
			this.submitDataSourceButton = this.pageViewArray[0].FindControl("ValidateDataSource1").FindControl("submitDataSourceButton") as Button;
			this.cancelDataSourceButton = this.pageViewArray[0].FindControl("ValidateDataSource1").FindControl("cancelDataSourceButton") as Button;

			this.submitDataSourceButton.Click += new EventHandler(submitDataSourceButton_Click);
			this.cancelDataSourceButton.Click += new EventHandler(cancelDataSourceButton_Click);


			//初始化选择表用户控件中的子控件
			this.sourceTableDataList = this.pageViewArray[1].FindControl("GetTable1").FindControl("sourceTableDataList") as DataList;
			this.selectedDataList = this.pageViewArray[1].FindControl("GetTable1").FindControl("selectedTableDataList") as DataList;
			this.selectTableSubmitButton = this.pageViewArray[1].FindControl("GetTable1").FindControl("selectTableSubmitButton") as Button;
			this.updateSelectedTableButton = this.pageViewArray[1].FindControl("GetTable1").FindControl("updateSelectedTableButton") as Button;
			
			this.selectTableSubmitButton.Click += new EventHandler(selectTableSubmitButton_Click);
			this.updateSelectedTableButton.Click += new EventHandler(updateSelectedTableButton_Click);

			//初始化建立表关系用户控件中的子控件
			this.relationFromTable1DropDownList = this.pageViewArray[2].FindControl("SetTableRelation1").FindControl("relationFromTable1DropDownList") as DropDownList;
			this.relationFromTable2DropDownList = this.pageViewArray[2].FindControl("SetTableRelation1").FindControl("relationFromTable2DropDownList") as DropDownList;
			this.relationField1Dropdownlist = this.pageViewArray[2].FindControl("SetTableRelation1").FindControl("relationField1Dropdownlist") as DropDownList;
			this.relationField2Dropdownlist = this.pageViewArray[2].FindControl("SetTableRelation1").FindControl("relationField2Dropdownlist") as DropDownList;

			this.addJoinFieldButton = this.pageViewArray[2].FindControl("SetTableRelation1").FindControl("addJoinFieldButton") as Button;
			this.currentSelectedJoinFieldListBox = this.pageViewArray[2].FindControl("SetTableRelation1").FindControl("currentSelectedJoinFieldListBox") as ListBox;
			this.currentTableRelationListBox = this.pageViewArray[2].FindControl("SetTableRelation1").FindControl("currentTableRelationListBox") as ListBox;
			this.joinTypeDropdownlist = this.pageViewArray[2].FindControl("SetTableRelation1").FindControl("joinTypeDropdownlist") as DropDownList;
			this.addRelationButton = this.pageViewArray[2].FindControl("SetTableRelation1").FindControl("addRelationButton") as Button;

			this.relationFromTable1DropDownList.SelectedIndexChanged += new EventHandler(relationFromTable1DropDownList_SelectedIndexChanged);
			this.relationFromTable2DropDownList.SelectedIndexChanged += new EventHandler(relationFromTable2DropDownList_SelectedIndexChanged);

			this.addJoinFieldButton.Click += new EventHandler(addJoinFieldButton_Click);
			this.addRelationButton.Click += new EventHandler(addRelationButton_Click);

			//初始化选择返回字段用户控件中的子控件
			this.sourceFieldDataList = this.pageViewArray[3].FindControl("GetReturnField1").FindControl("sourceFieldDataList") as DataList;
			this.selectedFieldDataList = this.pageViewArray[3].FindControl("GetReturnField1").FindControl("selectedFieldDataList") as DataList;
			this.selectFieldSubmitButton = this.pageViewArray[3].FindControl("GetReturnField1").FindControl("selectFieldSubmitButton") as Button;
			this.updateSelectedFieldButton = this.pageViewArray[3].FindControl("GetReturnField1").FindControl("updateSelectedFieldButton") as Button;

			this.selectFieldSubmitButton.Click += new EventHandler(selectFieldSubmitButton_Click);
			this.updateSelectedFieldButton.Click += new EventHandler(updateSelectedFieldButton_Click);
			this.selectedFieldDataList.ItemDataBound += new DataListItemEventHandler(selectedFieldDataList_ItemDataBound);

			//初始化设置条件用户控件中的子控件
			this.conditionFieldDropDownList = this.pageViewArray[4].FindControl("SetConditions1").FindControl("conditionFieldDropDownList") as DropDownList;
			this.conditionFieldDataTypeLabel = this.pageViewArray[4].FindControl("SetConditions1").FindControl("conditionFieldDataTypeLabel") as Label;
			this.inputValueControlTypeDropdownlist = this.pageViewArray[4].FindControl("SetConditions1").FindControl("inputValueControlTypeDropdownlist") as DropDownList;
			this.conditionNameTextBox = this.pageViewArray[4].FindControl("SetConditions1").FindControl("conditionNameTextBox") as TextBox;
			this.currentAllConditionsDropdownlist1 = this.pageViewArray[4].FindControl("SetConditions1").FindControl("currentAllConditionsDropdownlist1") as DropDownList;
			this.currentAllConditionsDropdownlist2 = this.pageViewArray[4].FindControl("SetConditions1").FindControl("currentAllConditionsDropdownlist2") as DropDownList;
			this.andOrRelationDropdownlist = this.pageViewArray[4].FindControl("SetConditions1").FindControl("andOrRelationDropdownlist") as DropDownList;
			this.addConditionButton = this.pageViewArray[4].FindControl("SetConditions1").FindControl("addConditionButton") as Button;
			this.conditionsDataList = this.pageViewArray[4].FindControl("SetConditions1").FindControl("conditionsDataList") as DataList;
			this.complicatedConditionsDatalist = this.pageViewArray[4].FindControl("SetConditions1").FindControl("complicatedConditionsDatalist") as DataList;
			this.addComplicatedConditionButton = this.pageViewArray[4].FindControl("SetConditions1").FindControl("addComplicatedConditionButton") as Button;
			this.initialValueTextBox = this.pageViewArray[4].FindControl("SetConditions1").FindControl("initialValueTextBox") as TextBox;
			this.complicatedConditionNameTextBox = this.pageViewArray[4].FindControl("SetConditions1").FindControl("complicatedConditionNameTextBox") as TextBox;
			this.fieldChineseNameTextBox = this.pageViewArray[4].FindControl("SetConditions1").FindControl("fieldChineseNameTextBox") as TextBox;
			this.operatorDropDownList = this.pageViewArray[4].FindControl("SetConditions1").FindControl("operatorDropDownList") as DropDownList;
			this.deleteConditionButton = this.pageViewArray[4].FindControl("SetConditions1").FindControl("deletaAtomConditionButton") as Button;
			this.deleteComplicatedConditionButton = this.pageViewArray[4].FindControl("SetConditions1").FindControl("deletaComplicatedConditionButton") as Button;


			this.conditionFieldDropDownList.SelectedIndexChanged += new EventHandler(HandleConditionChanged);

			this.addConditionButton.Click += new EventHandler(addConditionButton_Click);
			this.addComplicatedConditionButton.Click += new EventHandler(addComplicatedConditionButton_Click);
			this.deleteConditionButton.Click += new EventHandler(deleteConditionButton_Click);
			this.deleteComplicatedConditionButton.Click += new EventHandler(deleteComplicatedConditionButton_Click);

			//初始化设置排序用户控件中的子控件
			this.sourceSortFieldDataList = this.pageViewArray[5].FindControl("SetOrder1").FindControl("sourceSortFieldDataList") as DataList;
			this.selectedSortFieldDataList = this.pageViewArray[5].FindControl("SetOrder1").FindControl("selectedSortFieldDataList") as DataList;
			this.selectSortFieldSubmitButton = this.pageViewArray[5].FindControl("SetOrder1").FindControl("selectSortFieldSubmitButton") as Button;
			this.updateSelectedSortFieldButton = this.pageViewArray[5].FindControl("SetOrder1").FindControl("updateSelectedSortFieldButton") as Button;

			this.selectedSortFieldDataList.ItemDataBound += new DataListItemEventHandler(selectedSortFieldDataList_ItemDataBound);
			this.selectSortFieldSubmitButton.Click += new EventHandler(selectSortFieldSubmitButton_Click);
			this.updateSelectedSortFieldButton.Click += new EventHandler(updateSelectedSortFieldButton_Click);

			//初始化查看SQL用户控件中的子控件
			this.showSQLButton = this.pageViewArray[6].FindControl("PreviewSQL1").FindControl("showSQLButton") as Button;
			this.showSQLDiv = this.pageViewArray[6].FindControl("PreviewSQL1").FindControl("showSQLDiv") as HtmlGenericControl;
			this.showSQLTable = this.pageViewArray[6].FindControl("PreviewSQL1").FindControl("showSQLTable") as Table;

			this.showSQLButton.Click += new EventHandler(showSQLButton_Click);


			InitValidateDataSourceControl(null,null);
			
		}
		

		private StringDictionary GetFields(string tableName)
		{
			StringDictionary fieldDictionary = new StringDictionary();

			SqlConnection conn = new SqlConnection();
			conn.ConnectionString = this.dataSourceStringTextBox.Text.Trim();
			SqlCommand command = new SqlCommand();
			command.Connection = conn;
			if(tableName.StartsWith("["))
			{
				tableName = tableName.Substring(1);
			}
			if(tableName.EndsWith("]"))
			{
				tableName = tableName.Substring(0,tableName.Length-1);
			}
			command.CommandText = "select * from [" + tableName + "]";
			SqlDataAdapter apt = new SqlDataAdapter(command);
			DataSet ds = new DataSet();
			apt.Fill(ds);
			foreach(DataColumn col in ds.Tables[0].Columns)
			{
				fieldDictionary[col.ColumnName]= col.DataType.Name;
			}
			return fieldDictionary;

		}

		private string GetAliasTableName(string realTableName)
		{
			DataRow row = SelectedTables.Rows.Find(realTableName);
			if(row == null)
			{
				return string.Empty;
			}
			return row["AliasName"] as string;

		}

		private void SelectSourceTableCheckBox(string tableName)
		{
			foreach(DataListItem item in this.sourceTableDataList.Items)
			{
				CheckBox sourceTableCheckBox = item.FindControl("sourceTableCheckBox") as CheckBox;
				if(sourceTableCheckBox.Text == tableName)
				{
					sourceTableCheckBox.Checked = true;
					break;
				}
			}
		}
		private void SelectSourceFieldCheckBox(string fieldName)
		{
			foreach(DataListItem item in this.sourceFieldDataList.Items)
			{
				CheckBox checkBox = item.FindControl("sourceFieldCheckBox") as CheckBox;
				if(checkBox.Text == fieldName)
				{
					checkBox.Checked = true;
					break;
				}
			}
		}
		
		private void SelectSourceSortFieldCheckBox(string fieldName)
		{
			foreach(DataListItem item in this.sourceSortFieldDataList.Items)
			{
				CheckBox checkBox = item.FindControl("sourceFieldCheckBox") as CheckBox;
				if(checkBox.Text == fieldName)
				{
					checkBox.Checked = true;
					break;
				}
			}
		}
		
		private void AddConditionName(string conditionName)
		{
			if(this.currentAllConditionsDropdownlist1.Items.FindByValue(conditionName) == null)
			{
				this.currentAllConditionsDropdownlist1.Items.Add(conditionName);
				this.currentAllConditionsDropdownlist2.Items.Add(conditionName);
			}
		}

		
		#region Recover all WebControls' data and internal tables' data

		private void RecoverSelectedTables()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode tablesNode = doc.SelectSingleNode("QueryItem/Body/Tables");
			if(tablesNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("Tables"));
			}

			DataRow newRow = null;

			SelectedTables.Rows.Clear();

			foreach(XmlNode node in tablesNode.ChildNodes)
			{
				newRow = SelectedTables.NewRow();

				newRow["TableName"] = node.Attributes["Name"].Value;
				newRow["AliasName"] = node.Attributes["AliasName"].Value;
				newRow["ChineseName"] = node.Attributes["ChineseName"].Value;

				SelectedTables.Rows.Add(newRow);
			}

		}
		
		private void RecoverSelectedFields()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode selectNode = doc.SelectSingleNode("QueryItem/Body/Select");
			if(selectNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("Select"));
			}

			DataRow newRow = null;

			SelectedFields.Rows.Clear();

			foreach(XmlNode node in selectNode.ChildNodes)
			{
				newRow = SelectedFields.NewRow();

				newRow["OwnerTable"] = node.Attributes["OwnerTable"].Value;
				newRow["FieldFullName"] = node.Attributes["FullFieldName"].Value;
				newRow["FieldName"] = node.Attributes["FieldName"].Value;
				newRow["DataType"] = node.Attributes["DataType"].Value;
				newRow["AliasName"] = node.Attributes["AliasName"].Value;
				newRow["ChineseName"] = node.Attributes["ChineseName"].Value;

				SelectedFields.Rows.Add(newRow);
			}

		}
		
		private void RecoverSelectedSortFields()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode orderNode = doc.SelectSingleNode("QueryItem/Body/Order");
			if(orderNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("Order"));
			}

			DataRow newRow = null;

			SelectedSortFields.Rows.Clear();

			foreach(XmlNode node in orderNode.ChildNodes)
			{
				newRow = SelectedSortFields.NewRow();

				newRow["FieldFullName"] = node.Attributes["fieldFullName"].Value;
				newRow["SortType"] = node.Attributes["sortType"].Value;

				SelectedSortFields.Rows.Add(newRow);
			}

		}
		
		private void RecoverAtomConditions()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode atomConditionListDataNode = doc.SelectSingleNode("QueryItem/Body/AtomConditionListData");
			if(atomConditionListDataNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("AtomConditionListData"));
			}

			AtomConditions.Rows.Clear();

			DataRow newRow = null;

			foreach(XmlNode conditionNode in atomConditionListDataNode.ChildNodes)
			{
				newRow = AtomConditions.NewRow();

				newRow["FieldFullName"] = conditionNode.Attributes["fieldFullname"].Value;
				newRow["FieldDataType"] = conditionNode.Attributes["fieldDataType"].Value;
				newRow["ConditionName"] = conditionNode.Attributes["name"].Value;
				newRow["Operator"] = conditionNode.Attributes["operator"].Value;
				newRow["InputControlType"] = conditionNode.Attributes["inputType"].Value;
				newRow["ChineseName"] = conditionNode.Attributes["fieldChineseName"].Value;
				newRow["InitialValue"] = conditionNode.Attributes["initdata"].Value;

				AtomConditions.Rows.Add(newRow);
				
			}

		}
		private void RecoverComplicatedConditions()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode complicatedConditionListDataNode = doc.SelectSingleNode("QueryItem/Body/ComplicatedConditionListData");
			if(complicatedConditionListDataNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("ComplicatedConditionListData"));
			}

			DataRow newRow = null;

			ComplicatedConditions.Rows.Clear();

			foreach(XmlNode node in complicatedConditionListDataNode.ChildNodes)
			{
				newRow = ComplicatedConditions.NewRow();

				newRow["Condition1"] = node.Attributes["Condition1"].Value;
				newRow["Condition2"] = node.Attributes["Condition2"].Value;
				newRow["ConditionName"] = node.Attributes["Name"].Value;
				newRow["Relation"] = node.Attributes["Relation"].Value;

				ComplicatedConditions.Rows.Add(newRow);
			}

		}
		
		private void RecoverRelationTableHashTable()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode relationsNode = doc.SelectSingleNode("QueryItem/Body/Relations");
			if(relationsNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("Relations"));
			}

			XmlNode tableNode = relationsNode.SelectSingleNode("Table");

			if(tableNode != null)
			{
				//说明,数据是从一张表里选出来的,没有任何表关系,直接返回
				return;
			}

			foreach(XmlNode relationNode in relationsNode.ChildNodes)
			{
				string joinType = relationNode.Attributes["type"].Value;
				XmlNodeList twoTables = relationNode.SelectNodes("Table");
				string table1 = twoTables[0].Attributes["name"].Value;
				string table2 = twoTables[1].Attributes["name"].Value;
				XmlNodeList joinFields = relationNode.SelectNodes("JoinField");
				DataRow newRow = null;
				foreach(XmlNode joinFieldNode in joinFields)
				{
					RelationFields.Rows.Clear();
					newRow = RelationFields.NewRow();

					newRow["Table1"] = table1;
					newRow["Table2"] = table2;
					newRow["AliasTable1"] = GetAliasTableName(table1);
					newRow["AliasTable2"] = GetAliasTableName(table2);
					newRow["Field1"] = joinFieldNode.Attributes["Field1"].Value;
					newRow["Field2"] = joinFieldNode.Attributes["Field2"].Value;

					RelationFields.Rows.Add(newRow);
				}

				RelationTableHashTable[Guid.NewGuid().ToString() + "|" + joinType] = RelationFields.Copy();
			}

		}
		
		private void RecoverSqlString()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode sqlStringNode = doc.SelectSingleNode("QueryItem/SQLString");
			if(sqlStringNode == null)
			{
				doc.DocumentElement.AppendChild(doc.CreateElement("SQLString"));
			}

			XmlNode selectStringNode = sqlStringNode.SelectSingleNode("SelectString");
			XmlNode fromStringNode = sqlStringNode.SelectSingleNode("FromString");
			XmlNode conditionStringNode = sqlStringNode.SelectSingleNode("ConditionString");
			XmlNode orderStringNode = sqlStringNode.SelectSingleNode("OrderString");

			this.showSQLTable.Rows[0].Cells[0].Text = "Select";
			this.showSQLTable.Rows[1].Cells[1].Text = selectStringNode.InnerText;
			this.showSQLTable.Rows[2].Cells[0].Text = "From";
			this.showSQLTable.Rows[3].Cells[1].Text = fromStringNode.InnerText;
			if(conditionStringNode.InnerText != string.Empty)
			{
				this.showSQLTable.Rows[4].Cells[0].Text = "Where";
				this.showSQLTable.Rows[5].Cells[1].Text = conditionStringNode.InnerText;
			}
			if(orderStringNode.InnerText != string.Empty)
			{
				this.showSQLTable.Rows[6].Cells[0].Text = "Order By";
				this.showSQLTable.Rows[7].Cells[1].Text = orderStringNode.InnerText;
			}

		}


		private void InitSetTableRelationControl()
		{
			foreach(DataListItem item in this.selectedDataList.Items)
			{
				Label label = item.FindControl("selectedTableLabel") as Label;
				TextBox aliasTextBox = item.FindControl("aliasTextBox") as TextBox;

				if(aliasTextBox.Text != null && aliasTextBox.Text.Trim() != string.Empty)
				{
					relationFromTable1DropDownList.Items.Add(new ListItem(label.Text.Trim() + " " + aliasTextBox.Text.Trim()));
					relationFromTable2DropDownList.Items.Add(new ListItem(label.Text.Trim() + " " + aliasTextBox.Text.Trim()));
				}
				else
				{
					relationFromTable1DropDownList.Items.Add(new ListItem(label.Text.Trim()));
					relationFromTable2DropDownList.Items.Add(new ListItem(label.Text.Trim()));
				}
				int maxIndex = relationFromTable1DropDownList.Items.Count -1;
				relationFromTable1DropDownList.Items[maxIndex].Value = label.Text.Trim();
				relationFromTable2DropDownList.Items[maxIndex].Value = label.Text.Trim();

			}
			if(relationFromTable1DropDownList.Items.Count >= 0)
			{
				relationFromTable1DropDownList.SelectedIndex = 0;
				relationFromTable1DropDownList_SelectedIndexChanged(null,null);
			}
			if(relationFromTable2DropDownList.Items.Count >= 0)
			{
				relationFromTable2DropDownList.SelectedIndex = 0;
				relationFromTable2DropDownList_SelectedIndexChanged(null,null);
			}

			ListItem listItem = null;

			IDictionaryEnumerator enumerator = RelationTableHashTable.GetEnumerator();
			while(enumerator.MoveNext())
			{
				RelationFieldDataTable relationFieldDataTable = enumerator.Value as RelationFieldDataTable;
				string joinType = enumerator.Key.ToString().Split(new char[]{'|'})[1];
				string fullTable1 = string.Empty;
				string fullTable2 = string.Empty;
				string onString = string.Empty;

				for(int i = 0;i < relationFieldDataTable.Rows.Count;i++)
				{
					string table1 = relationFieldDataTable.Rows[i]["Table1"] as string;
					string table2 = relationFieldDataTable.Rows[i]["Table2"] as string;
					string aliasTable1 = relationFieldDataTable.Rows[i]["AliasTable1"] as string;
					string aliasTable2 = relationFieldDataTable.Rows[i]["AliasTable2"] as string;
					string field1 = relationFieldDataTable.Rows[i]["Field1"] as string;
					string field2 = relationFieldDataTable.Rows[i]["Field2"] as string;
					fullTable1 = table1;
					fullTable2 = table2;
					string correctFullField1 = string.Empty;
					string correctFullField2 = string.Empty;
					string item = string.Empty;
					
					if(aliasTable1 != null && aliasTable1 != string.Empty)
					{
						fullTable1 = fullTable1 + " " + aliasTable1;
						correctFullField1 = aliasTable1 + "." + field1;
					}
					else
					{
						correctFullField1 = table1 + "." + field1;
					}
					if(fullTable2 != null && fullTable2 != string.Empty)
					{
						fullTable2 = fullTable2 + " " + aliasTable2;
						correctFullField2 = aliasTable2 + "." + field2;
					}
					else
					{
						correctFullField2 = table2 + "." + field2;
					}
					item = correctFullField1 + "=" + correctFullField2;
					if(i == 0)
					{
						onString = onString + item;
					}
					else
					{
						onString = onString + " And " + item;
					}
				}//for
				if(this.currentTableRelationListBox.Items.Count > 0)
				{
					listItem = new ListItem(fullTable1 + " " + joinType + " " + fullTable2 + " On " + onString," " + joinType + " " + fullTable2 + " On " + onString);
				}
				else
				{
					listItem = new ListItem(fullTable1 + " " + joinType + " " + fullTable2 + " On " + onString,fullTable1 + " " + joinType + " " + fullTable2 + " On " + onString);
				}
			
				this.currentTableRelationListBox.Items.Add(listItem);

			}//while
			

		}

		private void InitGetReturnFieldControl()
		{
			//1.
			foreach(DataRow row in SelectedTables.Rows)
			{
				string aliasName = row["AliasName"] as string;
				string tableName = row["TableName"] as string;
				
				StringDictionary fields = GetFields(tableName);
				foreach(string field in fields.Keys)
				{
					DataRow rowFieldRow = SourceFields.NewRow();
					rowFieldRow["FieldFullName"] = (aliasName != null ? aliasName : tableName) + "." + field;
					rowFieldRow["FieldName"] = field;
					rowFieldRow["DataType"] = fields[field];
					rowFieldRow["OwnerTable"] = tableName;
					SourceFields.Rows.Add(rowFieldRow);

					DataRow rowFieldRow1 = SourceFields1.NewRow();
					rowFieldRow1["FieldFullName"] = (aliasName != null ? aliasName : tableName) + "." + field;
					rowFieldRow1["FieldName"] = field;
					rowFieldRow1["DataType"] = fields[field];
					rowFieldRow1["OwnerTable"] = tableName;
					SourceFields1.Rows.Add(rowFieldRow1);
				}
			}
			for(int i = SelectedTables.Rows.Count - 1;i >= 0;i--)
			{
				string aliasName = SelectedTables.Rows[i]["AliasName"] as string;
				string tableName = SelectedTables.Rows[i]["TableName"] as string;

				DataRow newRow = SourceFields.NewRow();
				newRow["FieldFullName"] = (aliasName != null ? aliasName : tableName) + ".*";
				newRow["FieldName"] = "*";
				newRow["OwnerTable"] = tableName;
				SourceFields.Rows.InsertAt(newRow,0);
			}
			this.sourceFieldDataList.DataSource = SourceFields;
			this.sourceFieldDataList.DataBind();

			//2.
			foreach(DataRow row in SelectedFields.Rows)
			{
				SelectSourceFieldCheckBox(row["FieldFullName"] as string);
			}	

			//3.
			this.selectedFieldDataList.DataSource = SelectedFields;
			this.selectedFieldDataList.DataBind();


		}

		private void InitSetConditionsControl()
		{
			foreach(DataRow row in SourceFields1.Rows)
			{
				conditionFieldDropDownList.Items.Add(row["FieldFullName"].ToString());
				int maxIndex = conditionFieldDropDownList.Items.Count -1;
				string itemValue = row["FieldFullName"].ToString() + "|" + row["DataType"].ToString();
				conditionFieldDropDownList.Items[maxIndex].Value = itemValue;
			}

			this.currentAllConditionsDropdownlist1.Items.Clear();
			this.currentAllConditionsDropdownlist2.Items.Clear();

			if(AtomConditions.Rows.Count > 0)
			{
				this.conditionsDataList.DataSource = AtomConditions;
				this.conditionsDataList.DataBind();
				AddConditionName(AtomConditions.Rows[0]["ConditionName"] as string);
			}

			if(ComplicatedConditions.Rows.Count > 0)
			{
				this.complicatedConditionsDatalist.DataSource = ComplicatedConditions;
				this.complicatedConditionsDatalist.DataBind();
				foreach(DataRow row in ComplicatedConditions.Rows)
				{
					AddConditionName(row["Condition1"] as string);
					AddConditionName(row["Condition2"] as string);
					AddConditionName(row["ConditionName"] as string);
				}
			}

		}
		private void InitGetSetOrderControl()
		{
			UpdateSourceSortFields();
			this.sourceSortFieldDataList.DataSource = SourceSortFields;
			this.sourceSortFieldDataList.DataBind();

			foreach(DataRow row in SelectedSortFields.Rows)
			{
				SelectSourceSortFieldCheckBox(row["FieldFullName"] as string);
			}

			this.selectedSortFieldDataList.DataSource = SelectedSortFields;
			this.selectedSortFieldDataList.DataBind();
		}


		#endregion

		#region Init ValidateDataSourceControl methods

		private void submitDataSourceButton_Click(object sender,EventArgs e)
		{
			if(ValidateDataSource(this.dataSourceStringTextBox.Text.Trim()) == true)
			{
				this.showErrorLabel.Text = "连接成功！";

				RecoverSelectedTables();
				RecoverSelectedFields();
				RecoverSelectedSortFields();
				RecoverAtomConditions();
				RecoverComplicatedConditions();
				RecoverRelationTableHashTable();
				RecoverSqlString();
				//到这里为止,所有和查询语句有关的信息都已经保存到了内存中的相应的表中了,
				//下面开始更新界面上控件的状态


				//1.更新GetTableControl控件
				InitGetTableControl(null,null);

				//2.更新SetTableRelationControl控件
				InitSetTableRelationControl();

				//3.更新GetReturnFieldControl控件
				InitGetReturnFieldControl();

				//4.更新SetConditionsControl控件
				InitSetConditionsControl();

				//5.更新SetOrderControl控件
				InitGetSetOrderControl();

			}
			else
			{
				this.showErrorLabel.Text = "连接失败！";
				
			}
			
		}
		private void cancelDataSourceButton_Click(object sender,EventArgs e)
		{
			this.dataSourceStringTextBox.Text = string.Empty;
		}
		private bool ValidateDataSource(string connectionString)
		{
			try
			{
				new SqlConnection(connectionString).Open();
			}
			catch
			{
				return false;
			}
			return true;

		}

		private void InitValidateDataSourceControl(object sender, EventArgs e)
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			if(queryItemId == null || queryItemId == string.Empty)
			{
				return;
			}

			DataTable tbl = QueryItemManager.Instance.RetrieveQueryItemById(queryItemId);
			if(tbl.Rows.Count == 0)
			{
				return;
			}

			this.dataSourceStringTextBox.Text = tbl.Rows[0]["connectionString"].ToString();

			this.dataSourceDriverTypeDropDownList.Items.Clear();
			this.dataSourceDriverTypeDropDownList.Items.Add("Microsoft SQL Server");

			
		}


		#endregion
		
		#region Init GetTableControl methods

		private void updateSelectedTableButton_Click(object sender, EventArgs e)
		{
			foreach(DataListItem item in this.selectedDataList.Items)
			{
				Label selectedTableLabel = item.FindControl("selectedTableLabel") as Label;
				TextBox aliasTextBox = item.FindControl("aliasTextBox") as TextBox;
				TextBox chineseNameTextBox = item.FindControl("chineseNameTextBox") as TextBox;
				DataRow row = SelectedTables.Rows.Find(selectedTableLabel.Text);
				if(row != null)
				{
					row["AliasName"] = aliasTextBox.Text.Trim();
					row["ChineseName"] = chineseNameTextBox.Text.Trim();
				}
			}
			InitSetTableRelationControl(null,null);
			InitGetReturnFieldControl(null,null);
			InitSetConditionsControl(null,null);
			InitSetConditionsControl(null,null);
			InitGetSetOrderControl(null,null);
			ResetSQLString();
		}
		private void selectTableSubmitButton_Click(object sender, EventArgs e)
		{
			foreach(DataListItem item in this.sourceTableDataList.Items)
			{
				CheckBox checkBox = item.FindControl("sourceTableCheckBox") as CheckBox;
				if(checkBox.Checked == true)
				{
					DataRow row = SelectedTables.Rows.Find(checkBox.Text);
					if(row == null)
					{
						DataRow newRow = SelectedTables.NewRow();
						newRow["TableName"] = checkBox.Text;
						SelectedTables.Rows.Add(newRow);
					}
				}
				else
				{
					if(SelectedTables.Rows.Find(checkBox.Text) != null)
					{
						SelectedTables.Rows.Remove(SelectedTables.Rows.Find(checkBox.Text));
					}
				}
			}
			this.selectedDataList.DataSource = SelectedTables;
			this.selectedDataList.DataBind();

			InitSetTableRelationControl(null,null);
			InitGetReturnFieldControl(null,null);
			InitSetConditionsControl(null,null);
			InitGetSetOrderControl(null,null);
			ResetSQLString();
		}

		private void InitGetTableControl(object sender, EventArgs e)
		{
			//1.得到数据库中的所有表
			if(this.dataSourceStringTextBox.Text.Trim() == string.Empty)
			{
				return;
			}
			SqlConnection conn = new SqlConnection(this.dataSourceStringTextBox.Text.Trim());
			string selectString = "select name as tableName from sysobjects where xtype='u' order by tableName";
			SqlDataAdapter apt = new SqlDataAdapter(selectString,conn);
			DataSet ds = new DataSet();
			apt.Fill(ds);
			if(ds.Tables.Count > 0)
			{
				foreach(DataRow row in ds.Tables[0].Rows)
				{
					if(row["tableName"].ToString().IndexOf(" ") >= 0)
					{
						row["tableName"] = "[" + row["tableName"].ToString() + "]";
					}
				}
				this.sourceTableDataList.DataSource = ds.Tables[0];
				this.sourceTableDataList.DataBind();
			}

			//2.从源DataList中选择一些和查询语句相关的表
			foreach(DataRow row in SelectedTables.Rows)
			{
				SelectSourceTableCheckBox(row["TableName"] as string);
			}

			//3.将SelectedTables绑定到selectedDataList
			this.selectedDataList.DataSource = SelectedTables;
			this.selectedDataList.DataBind();

		}


		#endregion

		#region Init SetTableRelationControl methods

		private void relationFromTable1DropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(this.relationFromTable1DropDownList.SelectedIndex >= 0)
			{
				int currentIndex = this.relationFromTable1DropDownList.SelectedIndex;
				string tableName = this.relationFromTable1DropDownList.Items[currentIndex].Value;
				StringDictionary fields = GetFields(tableName);
				this.relationField1Dropdownlist.Items.Clear();
				foreach(string field in fields.Keys)
				{
					this.relationField1Dropdownlist.Items.Add(field);
				}
			}
		}
		private void relationFromTable2DropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(this.relationFromTable2DropDownList.SelectedIndex >= 0)
			{
				int currentIndex = this.relationFromTable2DropDownList.SelectedIndex;
				string tableName = this.relationFromTable2DropDownList.Items[currentIndex].Value;
				StringDictionary fields = GetFields(tableName);
				this.relationField2Dropdownlist.Items.Clear();
				foreach(string field in fields.Keys)
				{
					this.relationField2Dropdownlist.Items.Add(field);
				}
			}
		}

		private void addJoinFieldButton_Click(object sender, EventArgs e)
		{
			string table1 = this.relationFromTable1DropDownList.SelectedValue;
			string table2 = this.relationFromTable2DropDownList.SelectedValue;
			string aliasTable1 = GetAliasTableName(table1);
			string aliasTable2 = GetAliasTableName(table2);
			string field1 = this.relationField1Dropdownlist.SelectedItem.Text;
			string field2 = this.relationField2Dropdownlist.SelectedItem.Text;
			string fullField1 = string.Empty;
			string fullField2 = string.Empty;

			DataRow row = RelationFields.NewRow();
			row["Table1"] = table1;
			row["Table2"] = table2;
			row["AliasTable1"] = aliasTable1;
			row["AliasTable2"] = aliasTable2;
			row["Field1"] = field1;
			row["Field2"] = field2;
			RelationFields.Rows.Add(row);
			
			if(aliasTable1 != null && aliasTable1 != string.Empty)
			{
				fullField1 = aliasTable1 + "." + field1;
			}
			else
			{
				fullField1 = table1 + "." + field1;
			}

			if(aliasTable2 != null && aliasTable2 != string.Empty)
			{
				fullField2 = aliasTable2 + "." + field2;
			}
			else
			{
				fullField2 = table2 + "." + field2;
			}

			this.currentSelectedJoinFieldListBox.Items.Add(fullField1 + " = " + fullField2);

		}

		private void addRelationButton_Click(object sender, EventArgs e)
		{
			if(this.currentSelectedJoinFieldListBox.Items.Count <= 0)
			{
				this.Page.Response.Write("<script language='javascript'>alert('请至少添加一对关联的字段！');</script>");
				return;
			}
			string table1 = this.relationFromTable1DropDownList.SelectedItem.Text;
			string table2 = this.relationFromTable2DropDownList.SelectedItem.Text;
			string joinType = this.joinTypeDropdownlist.SelectedItem.Value;
			string onString = string.Empty;
			foreach(ListItem item in this.currentSelectedJoinFieldListBox.Items)
			{
				if(onString == string.Empty)
				{
					onString = onString + item;
				}
				else
				{
					onString = onString + " And " + item;
				}
			}
			ListItem listItem = null;

			if(this.currentTableRelationListBox.Items.Count > 0)
			{
				listItem = new ListItem(table1 + " " + joinType + " " + table2 + " On " + onString," " + joinType + " " + table2 + " On " + onString);
			}
			else
			{
				listItem = new ListItem(table1 + " " + joinType + " " + table2 + " On " + onString,table1 + " " + joinType + " " + table2 + " On " + onString);
			}
			
			this.currentTableRelationListBox.Items.Add(listItem);
//			TableRelation tableRelation = new TableRelation();
//			tableRelation.Table1 = table1;
//			tableRelation.Table2 = table2;
//			tableRelation.JoinType = joinType;
//			tableRelation.RelationFields = RelationFields.Copy() as RelationFieldDataTable;
			RelationTableHashTable[Guid.NewGuid().ToString() + "|" + joinType] = RelationFields.Copy();
			this.currentSelectedJoinFieldListBox.Items.Clear();
			RelationFields.Rows.Clear();
		}

		private void InitSetTableRelationControl(object sender, EventArgs e)
		{
			relationFromTable1DropDownList.Items.Clear();
			relationFromTable2DropDownList.Items.Clear();
			RelationFields.Rows.Clear();
			RelationTableHashTable.Clear();
			this.currentSelectedJoinFieldListBox.Items.Clear();
			this.currentTableRelationListBox.Items.Clear();


			foreach(DataListItem item in this.selectedDataList.Items)
			{
				Label label = item.FindControl("selectedTableLabel") as Label;
				TextBox aliasTextBox = item.FindControl("aliasTextBox") as TextBox;

				if(aliasTextBox.Text != null && aliasTextBox.Text.Trim() != string.Empty)
				{
					relationFromTable1DropDownList.Items.Add(new ListItem(label.Text.Trim() + " " + aliasTextBox.Text.Trim()));
					relationFromTable2DropDownList.Items.Add(new ListItem(label.Text.Trim() + " " + aliasTextBox.Text.Trim()));
				}
				else
				{
					relationFromTable1DropDownList.Items.Add(new ListItem(label.Text.Trim()));
					relationFromTable2DropDownList.Items.Add(new ListItem(label.Text.Trim()));
				}
				int maxIndex = relationFromTable1DropDownList.Items.Count -1;
				relationFromTable1DropDownList.Items[maxIndex].Value = label.Text.Trim();
				relationFromTable2DropDownList.Items[maxIndex].Value = label.Text.Trim();

			}
			if(relationFromTable1DropDownList.Items.Count >= 0)
			{
				relationFromTable1DropDownList.SelectedIndex = 0;
				relationFromTable1DropDownList_SelectedIndexChanged(null,null);
			}
			if(relationFromTable2DropDownList.Items.Count >= 0)
			{
				relationFromTable2DropDownList.SelectedIndex = 0;
				relationFromTable2DropDownList_SelectedIndexChanged(null,null);
			}
		}


		#endregion
		
		#region Init GetReturnFieldControl methods

		private void UpdateSourceSortFields()
		{
			SourceSortFields.Rows.Clear();

			foreach(DataRow row in SelectedFields.Rows)
			{
				string tableName = row["OwnerTable"] as string;
				string fieldName = row["FieldName"] as string;
				string aliasName = row["AliasName"] as string;
				string chineseName = row["ChineseName"] as string;
				string dataType = row["DataType"] as string;

				if(fieldName == "*")
				{
					StringDictionary fields = GetFields(tableName);
					
					string currentTableName = GetAliasTableName(tableName);
					if(currentTableName == null || currentTableName == string.Empty)
					{
						currentTableName = tableName;
					}
					
					foreach(DictionaryEntry entry in fields)
					{
						if(SourceSortFields.Rows.Find(currentTableName + "." + entry.Key.ToString()) == null)
						{
							DataRow r = SourceSortFields.NewRow();
							r["FieldFullName"] = currentTableName + "." + entry.Key.ToString();
							r["FieldName"] = entry.Key.ToString();
							r["AliasName"] = aliasName;
							r["ChineseName"] = chineseName;
							r["DataType"] = dataType;
							r["OwnerTable"] = tableName;
							SourceSortFields.Rows.Add(r);
						}
					}
				}
				else
				{
					string currentTableName = GetAliasTableName(tableName);
					if(currentTableName == null || currentTableName == string.Empty)
					{
						currentTableName = tableName;
					}
					if(SourceSortFields.Rows.Find(currentTableName + "." + fieldName) == null)
					{
						DataRow r = SourceSortFields.NewRow();
						r["FieldFullName"] = currentTableName + "." + fieldName;
						r["FieldName"] = fieldName;
						r["AliasName"] = aliasName;
						r["ChineseName"] = chineseName;
						r["DataType"] = dataType;
						r["OwnerTable"] = tableName;
						SourceSortFields.Rows.Add(r);
					}
				}
			}
		}
		private void updateSelectedFieldButton_Click(object sender, EventArgs e)
		{
			foreach(DataListItem item in this.selectedFieldDataList.Items)
			{
				Label selectedFieldLabel = item.FindControl("selectedFieldLabel") as Label;
				TextBox aliasFieldTextBox = item.FindControl("aliasFieldTextBox") as TextBox;
				TextBox chineseNameFieldTextbox = item.FindControl("chineseNameFieldTextbox") as TextBox;
				DataRow row = SelectedFields.Rows.Find(selectedFieldLabel.Text);
				if(row != null)
				{
					row["AliasName"] = aliasFieldTextBox.Text.Trim();
					row["ChineseName"] = chineseNameFieldTextbox.Text.Trim();
				}
			}

			UpdateSourceSortFields();
			InitGetSetOrderControl(null,null);

		}
		private void selectFieldSubmitButton_Click(object sender, EventArgs e)
		{
			foreach(DataListItem item in this.sourceFieldDataList.Items)
			{
				CheckBox checkBox = item.FindControl("sourceFieldCheckBox") as CheckBox;
				if(checkBox.Checked == true)
				{
					if(SelectedFields.Rows.Find(checkBox.Text) == null)
					{
						DataRow sourceRow = SourceFields.Rows.Find(checkBox.Text);
			
						bool flag = false;
						foreach(DataRow row in SelectedFields.Rows)
						{
							if(row["OwnerTable"].ToString() == sourceRow["OwnerTable"].ToString())
							{
								if(row["FieldName"].ToString() == "*")
								{
									flag = true;
									break;
								}
							}
						}
						if(flag == false)
						{
							DataRow row = SelectedFields.NewRow();
							row["FieldFullName"] = checkBox.Text;
							row["FieldName"] = sourceRow["FieldName"];
							row["DataType"] = sourceRow["DataType"];
							row["OwnerTable"] = sourceRow["OwnerTable"];
							SelectedFields.Rows.Add(row);
						}
						
					}
				}
				else
				{
					if(SelectedFields.Rows.Find(checkBox.Text) != null)
					{
						SelectedFields.Rows.Remove(SelectedFields.Rows.Find(checkBox.Text));
					}
				}
			}

			TableFieldDataTable tempTable = SelectedFields.Copy() as TableFieldDataTable;

			foreach(DataRow row in tempTable.Rows)
			{
				if(row["FieldName"].ToString() == "*")
				{
					string ownerTable = row["OwnerTable"].ToString();
					foreach(DataRow r in tempTable.Rows)
					{
						if(r["OwnerTable"].ToString() == ownerTable && r["FieldName"].ToString() != "*")
						{
							DataRow currentRow = SelectedFields.Rows.Find(r["FieldFullName"].ToString());
							SelectedFields.Rows.Remove(currentRow);
						}
					}
				}
			}
			this.selectedFieldDataList.DataSource = SelectedFields;
			this.selectedFieldDataList.DataBind();

			UpdateSourceSortFields();
			InitGetSetOrderControl(null,null);

		}
		private void selectedFieldDataList_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label selectedFieldLabel = e.Item.FindControl("selectedFieldLabel") as Label;
				if(selectedFieldLabel.Text.IndexOf("*") >= 0)
				{
					TextBox aliasFieldTextBox = e.Item.FindControl("aliasFieldTextBox") as TextBox;
					TextBox chineseNameFieldTextbox = e.Item.FindControl("chineseNameFieldTextbox") as TextBox;
					chineseNameFieldTextbox.Visible = false;
					aliasFieldTextBox.Visible = false;
				}
			}
		}
		private void InitGetReturnFieldControl(object sender, EventArgs e)
		{
			SourceFields.Rows.Clear();
			SourceFields1.Rows.Clear();
			SelectedFields.Rows.Clear();

			foreach(DataRow row in SelectedTables.Rows)
			{
				string aliasName = row["AliasName"] as string;
				string tableName = row["TableName"] as string;
				string realName = tableName;
				
				StringDictionary fields = GetFields(tableName);
				foreach(string field in fields.Keys)
				{
					DataRow rowFieldRow = SourceFields.NewRow();
					if(aliasName != null && aliasName != string.Empty)
					{
						realName = aliasName;
					}
					rowFieldRow["FieldFullName"] = realName + "." + field;
					rowFieldRow["FieldName"] = field;
					rowFieldRow["DataType"] = fields[field];
					rowFieldRow["OwnerTable"] = tableName;
					SourceFields.Rows.Add(rowFieldRow);

					DataRow rowFieldRow1 = SourceFields1.NewRow();
					rowFieldRow1["FieldFullName"] = realName + "." + field;
					rowFieldRow1["FieldName"] = field;
					rowFieldRow1["DataType"] = fields[field];
					rowFieldRow1["OwnerTable"] = tableName;
					SourceFields1.Rows.Add(rowFieldRow1);
				}
			}
			for(int i = SelectedTables.Rows.Count - 1;i >= 0;i--)
			{
				string aliasName = SelectedTables.Rows[i]["AliasName"] as string;
				string tableName = SelectedTables.Rows[i]["TableName"] as string;
				string realName1 = tableName;

				DataRow newRow = SourceFields.NewRow();
				if(aliasName != null && aliasName != string.Empty)
				{
					realName1 = aliasName;
				}
				newRow["FieldFullName"] = realName1 + ".*";
				newRow["FieldName"] = "*";
				newRow["OwnerTable"] = tableName;
				SourceFields.Rows.InsertAt(newRow,0);
			}
			this.sourceFieldDataList.DataSource = SourceFields;
			this.sourceFieldDataList.DataBind();
		
			selectFieldSubmitButton_Click(null,null);

		}

		
		#endregion
		
		#region Init SetConditionsControl methods

		private void addComplicatedConditionButton_Click(object sender, EventArgs e)
		{
			if(this.complicatedConditionNameTextBox.Text.Trim() == string.Empty)
			{
				Page.Response.Write("<script language='javascript'>alert('组合条件的名称不能为空！');</script>");
				return;
			}
			string condition1 = this.currentAllConditionsDropdownlist1.SelectedItem == null ? 
				string.Empty : this.currentAllConditionsDropdownlist1.SelectedItem.Text;
			string condition2 = this.currentAllConditionsDropdownlist2.SelectedItem == null ? 
				string.Empty : this.currentAllConditionsDropdownlist2.SelectedItem.Text;
			string relation = this.andOrRelationDropdownlist.SelectedItem == null ? 
				string.Empty : this.andOrRelationDropdownlist.SelectedItem.Value;
			string conditionName = this.complicatedConditionNameTextBox.Text.Trim();

			if(ComplicatedConditions.Rows.Find(conditionName) != null)
			{
				Page.Response.Write("<script language='javascript'>alert('组合条件的名称不能重复！');</script>");
				return;	
			}
			if(AtomConditions.Rows.Find(conditionName) != null)
			{
				Page.Response.Write("<script language='javascript'>alert('组合条件的名称和原子条件名称不能重复！');</script>");
				return;	
			}
			if(condition1 == condition2)
			{
				Page.Response.Write("<script language='javascript'>alert('被组合的两个条件不能相同！');</script>");
				return;	
			}

			DataRow row = ComplicatedConditions.NewRow();

			row["Condition1"] = condition1;
			row["Condition2"] = condition2;
			row["Relation"] = relation;
			row["ConditionName"] = conditionName;

			ComplicatedConditions.Rows.Add(row);

			this.currentAllConditionsDropdownlist1.Items.Add(conditionName);
			this.currentAllConditionsDropdownlist2.Items.Add(conditionName);
			this.complicatedConditionNameTextBox.Text = string.Empty;

			this.complicatedConditionsDatalist.DataSource = ComplicatedConditions;
			this.complicatedConditionsDatalist.DataBind();


		}
		private void HandleConditionChanged(object sender, EventArgs e)
		{
			if(this.conditionFieldDropDownList.SelectedIndex >= 0)
			{
				string fieldType = this.conditionFieldDropDownList.SelectedItem.Value.Split(new char[]{'|'})[1];
				this.conditionFieldDataTypeLabel.Text = fieldType;
			}
		}
		private void addConditionButton_Click(object sender, EventArgs e)
		{
			if(this.conditionFieldDropDownList.SelectedItem == null)
			{
				Page.Response.Write("<script language='javascript'>alert('请选择一个字段！');</script>");
				return;
			}
			if(this.conditionNameTextBox.Text.Trim() == string.Empty)
			{
				Page.Response.Write("<script language='javascript'>alert('条件名称不能为空！');</script>");
				return;
			}
			if(this.fieldChineseNameTextBox.Text.Trim() == string.Empty)
			{
				Page.Response.Write("<script language='javascript'>alert('条件中文名称不能为空！');</script>");
				return;
			}
			string fieldFullName = this.conditionFieldDropDownList.SelectedItem.Text;
			string fieldDataType = this.conditionFieldDataTypeLabel.Text;
			string inputControlType = this.inputValueControlTypeDropdownlist.SelectedItem.Value;
			string conditionName = this.conditionNameTextBox.Text.Trim();
			string initialValue = this.initialValueTextBox.Text.Trim();
			string fieldChineseName = this.fieldChineseNameTextBox.Text.Trim();
			string operatorString = this.operatorDropDownList.SelectedItem.Value;

			if(AtomConditions.Rows.Find(conditionName) != null)
			{
				Page.Response.Write("<script language='javascript'>alert('条件名称不能重复！');</script>");
				return;	
			}

			DataRow row = AtomConditions.NewRow();

			row["FieldFullName"] = fieldFullName;
			row["FieldDataType"] = fieldDataType;
			row["InputControlType"] = inputControlType;
			row["ConditionName"] = conditionName;
			row["InitialValue"] = initialValue;
			row["ChineseName"] = fieldChineseName;
			row["Operator"] = operatorString;

			AtomConditions.Rows.Add(row);

			foreach(DataRow row1 in AtomConditions.Rows)
			{
				AddConditionName(row1["ConditionName"] as string);
			}

			this.conditionNameTextBox.Text = string.Empty;

			this.conditionsDataList.DataSource = AtomConditions;
			this.conditionsDataList.DataBind();

		}
		private void deleteConditionButton_Click(object sender, EventArgs e)
		{
			if(AtomConditions.Rows.Count > 0)
			{
				AtomConditionDataTable tempTable = AtomConditions.Copy() as AtomConditionDataTable;
				AtomConditions.Rows.Clear();
				for(int i = 0;i < tempTable.Rows.Count - 1;i++)
				{
					DataRow row = AtomConditions.NewRow();
					for(int j = 0;j < AtomConditions.Columns.Count;j++)
					{
						row[j] = tempTable.Rows[i][j];
					}
					AtomConditions.Rows.Add(row);
				}
				ComplicatedConditions.Rows.Clear();
				this.currentAllConditionsDropdownlist1.Items.Clear();
				this.currentAllConditionsDropdownlist2.Items.Clear();

				foreach(DataRow row1 in AtomConditions.Rows)
				{
					AddConditionName(row1["ConditionName"] as string);
				}

				if(AtomConditions.Rows.Count == 0)
				{
					this.conditionsDataList.DataSource = null;
				}
				else
				{
					this.conditionsDataList.DataSource = AtomConditions;
				}

				this.complicatedConditionsDatalist.DataSource = null;

				this.conditionsDataList.DataBind();
				this.complicatedConditionsDatalist.DataBind();

			}
		}
		private void deleteComplicatedConditionButton_Click(object sender, EventArgs e)
		{
			if(ComplicatedConditions.Rows.Count > 0)
			{
				ComplicatedConditionDataTable tempTable = ComplicatedConditions.Copy() as ComplicatedConditionDataTable;
				ComplicatedConditions.Rows.Clear();
				for(int i = 0;i < tempTable.Rows.Count - 1;i++)
				{
					DataRow row = ComplicatedConditions.NewRow();
					for(int j = 0;j < ComplicatedConditions.Columns.Count;j++)
					{
						row[j] = tempTable.Rows[i][j];
					}
					ComplicatedConditions.Rows.Add(row);
				}
				string conditionName = tempTable.Rows[tempTable.Rows.Count-1]["ConditionName"] as string;
				ListItem item = this.currentAllConditionsDropdownlist1.Items.FindByValue(conditionName);
				if(item != null)
				{
					this.currentAllConditionsDropdownlist1.Items.Remove(item);
				}

				string conditionName1 = tempTable.Rows[tempTable.Rows.Count-1]["ConditionName"] as string;
				ListItem item1 = this.currentAllConditionsDropdownlist2.Items.FindByValue(conditionName1);
				if(item1 != null)
				{
					this.currentAllConditionsDropdownlist2.Items.Remove(item1);
				}

				if(ComplicatedConditions.Rows.Count == 0)
				{
					this.complicatedConditionsDatalist.DataSource = null;
				}
				else
				{
					this.complicatedConditionsDatalist.DataSource = ComplicatedConditions;
				}
				this.complicatedConditionsDatalist.DataBind();

			}
		}
		private void InitSetConditionsControl(object sender, EventArgs e)
		{
			conditionFieldDropDownList.Items.Clear();
			AtomConditions.Rows.Clear();
			ComplicatedConditions.Rows.Clear();
			conditionsDataList.DataSource = null;
			complicatedConditionsDatalist.DataSource = null;
			conditionsDataList.DataBind();
			complicatedConditionsDatalist.DataBind();

			foreach(DataRow row in SourceFields1.Rows)
			{
				conditionFieldDropDownList.Items.Add(row["FieldFullName"].ToString());
				int maxIndex = conditionFieldDropDownList.Items.Count -1;
				string itemValue = row["FieldFullName"].ToString() + "|" + row["DataType"].ToString();
				conditionFieldDropDownList.Items[maxIndex].Value = itemValue;
			}

			this.currentAllConditionsDropdownlist1.Items.Clear();
			this.currentAllConditionsDropdownlist2.Items.Clear();

			if(this.conditionFieldDropDownList.Items.Count > 0)
			{
				this.conditionFieldDropDownList.SelectedIndex = 0;
				HandleConditionChanged(null,null);
			}

		}

		
		#endregion
		
		#region Init SetOrderControl methods

		private void selectedSortFieldDataList_ItemDataBound(object sender,DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataListItem item = e.Item;
				Label sortLabel = item.FindControl("sortLabel") as Label;
				DropDownList sortDropDownList = item.FindControl("sortDropDownList") as DropDownList;

				if(sortLabel.Text == "Desc")
				{
					sortDropDownList.SelectedIndex = 1;
				}
				else
				{
					sortDropDownList.SelectedIndex = 0;
				}
			}

		}

		private void updateSelectedSortFieldButton_Click(object sender, EventArgs e)
		{
			foreach(DataListItem item in this.selectedSortFieldDataList.Items)
			{
				Label selectedFieldLabel = item.FindControl("selectedFieldLabel") as Label;
				DropDownList sortDropDownList = item.FindControl("sortDropDownList") as DropDownList;
				DataRow row = SelectedSortFields.Rows.Find(selectedFieldLabel.Text);
				if(row != null)
				{
					row["SortType"] = sortDropDownList.SelectedItem.Value.Trim();
				}
			}
		}
		private void selectSortFieldSubmitButton_Click(object sender, EventArgs e)
		{
			foreach(DataListItem item in this.sourceSortFieldDataList.Items)
			{
				CheckBox checkBox = item.FindControl("sourceFieldCheckBox") as CheckBox;
				if(checkBox.Checked == true)
				{
					if(SelectedSortFields.Rows.Find(checkBox.Text) == null)
					{
						DataRow row = SelectedSortFields.NewRow();
						row["FieldFullName"] = checkBox.Text;
						row["SortType"] = "Asc";
						SelectedSortFields.Rows.Add(row);
					}
				}
				else
				{
					if(SelectedSortFields.Rows.Find(checkBox.Text) != null)
					{
						SelectedSortFields.Rows.Remove(SelectedSortFields.Rows.Find(checkBox.Text));
					}
				}
			}
			this.selectedSortFieldDataList.DataSource = SelectedSortFields;
			this.selectedSortFieldDataList.DataBind();
		}
		private void InitGetSetOrderControl(object sender, EventArgs e)
		{
			this.sourceSortFieldDataList.DataSource = SourceSortFields;
			this.sourceSortFieldDataList.DataBind();

			SelectedSortFields.Rows.Clear();
			this.selectedSortFieldDataList.DataSource = SelectedSortFields;
			this.selectedSortFieldDataList.DataBind();

		}

		
		#endregion

		#region Init PreviewSQLControl methods
		
		private bool generateSQLSuccess = true;
		string conditionString = string.Empty;

		private void ResetSQLString()
		{
			this.showSQLTable.Rows[0].Cells[0].Text = "";
			this.showSQLTable.Rows[1].Cells[1].Text = "";
			this.showSQLTable.Rows[2].Cells[0].Text = "";
			this.showSQLTable.Rows[3].Cells[1].Text = "";
			this.showSQLTable.Rows[4].Cells[0].Text = "";
			this.showSQLTable.Rows[5].Cells[1].Text = "";
			this.showSQLTable.Rows[6].Cells[0].Text = "";
			this.showSQLTable.Rows[7].Cells[1].Text = "";
			
		}

		private XmlNode CreateAtomConditionNode(DataRow row, XmlDocument doc)
		{
			XmlNode conditionNode = null;
			XmlAttribute att = null;

			conditionNode = doc.CreateElement("Condition");

			att = doc.CreateAttribute("name");
			att.Value = row["ConditionName"] as string;
			conditionNode.Attributes.Append(att);

			att = doc.CreateAttribute("fieldFullname");
			att.Value = row["FieldFullName"] as string;
			conditionNode.Attributes.Append(att);

			att = doc.CreateAttribute("fieldDataType");
			att.Value = row["fieldDataType"] as string;
			conditionNode.Attributes.Append(att);

			att = doc.CreateAttribute("operator");
			att.Value = row["Operator"] as string;
			conditionNode.Attributes.Append(att);

			att = doc.CreateAttribute("inputType");
			att.Value = row["InputControlType"] as string;
			conditionNode.Attributes.Append(att);

			att = doc.CreateAttribute("fieldChineseName");
			att.Value = row["ChineseName"] as string;
			conditionNode.Attributes.Append(att);

			att = doc.CreateAttribute("initdata");
			att.Value = row["InitialValue"] as string;
			conditionNode.Attributes.Append(att);

			att = doc.CreateAttribute("fieldvalue");
			att.Value = string.Empty;
			conditionNode.Attributes.Append(att);

			return conditionNode;

		}

		private void SaveConnectionString()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode connectionStringNode = doc.SelectSingleNode("QueryItem/ConnectionString");
			if(connectionStringNode == null)
			{
				doc.DocumentElement.AppendChild(doc.CreateElement("ConnectionString"));
			}
			connectionStringNode.InnerText = this.dataSourceStringTextBox.Text.Trim();

			QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);

		}

		
		#region get condition string

		private DataRow GetComplicatedConditionRow(string conditionName)
		{
			foreach(DataRow row in ComplicatedConditions.Rows)
			{
				if(row["ConditionName"].ToString() == conditionName)
				{
					return row;
				}
			}
			return null;
		}
		private ConditionNode CreateComplicatedConditionNode(DataRow complicatedConditonRow)
		{
			ConditionNode node = new ConditionNode();
			
			node.ConditionName = complicatedConditonRow["ConditionName"].ToString();
			node.Relation = complicatedConditonRow["Relation"].ToString();

			return node;
		}
		private AtomCondition CreateAtomCondition(string atomConditionName)
		{
			AtomCondition condition = null;
			
			DataRow row = AtomConditions.Rows.Find(atomConditionName);
			
			if(row != null)
			{
				condition = new AtomCondition();
				condition.ConditionName = atomConditionName;
				condition.ChineseName = row["ChineseName"] as String;
				condition.FieldDataType = row["FieldDataType"] as String;
				condition.FieldFullName = row["FieldFullName"] as String;
				condition.InitialValue = row["InitialValue"] as String;
				condition.InputControlType = row["InputControlType"] as String;
				condition.Operator = row["Operator"] as String;
			}

			return condition;
		}
		
		private void CreateConditionTree(ConditionNode parentNode)
		{
			DataRow row = ComplicatedConditions.Rows.Find(parentNode.ConditionName);

			if(row != null)
			{
				DataRow complicatedChildRow1 = GetComplicatedConditionRow(row["Condition1"].ToString());
				DataRow complicatedChildRow2 = GetComplicatedConditionRow(row["Condition2"].ToString());

				if(complicatedChildRow1 == null)         //说明rows[i]["Condition1"]是一个原子条件
				{
					parentNode.ChildConditionNode1 = new ConditionNode();
					parentNode.ChildConditionNode1.Condition = CreateAtomCondition(row["Condition1"].ToString());
				}
				else
				{
					ConditionNode childNode1 = CreateComplicatedConditionNode(complicatedChildRow1);
					parentNode.ChildConditionNode1 = childNode1;
					CreateConditionTree(childNode1);
				}
				if(complicatedChildRow2 == null)         //说明rows[i]["Condition2"]是一个原子条件
				{
					parentNode.ChildConditionNode2 = new ConditionNode();
					parentNode.ChildConditionNode2.Condition = CreateAtomCondition(row["Condition2"].ToString());
				}
				else
				{
					ConditionNode childNode2 = CreateComplicatedConditionNode(complicatedChildRow2);
					parentNode.ChildConditionNode2 = childNode2;
					CreateConditionTree(childNode2);
				}
			}

		}

		private void AddLeftBracketToMostLeftChildNode(ConditionNode currentNode)
		{
			ConditionNode node = currentNode;
			if(node == null)
			{
				return;
			}
			while(node.ChildConditionNode1 != null)
			{
				node = node.ChildConditionNode1;
			}
			node.Condition.FieldFullName = "(" + node.Condition.FieldFullName;

		}
		private void AddRightBracketToMostRightChildNode(ConditionNode currentNode)
		{
			ConditionNode node = currentNode;
			if(node == null)
			{
				return;
			}
			while(node.ChildConditionNode2 != null)
			{
				node = node.ChildConditionNode2;
			}
			node.Condition.Operator = node.Condition.Operator  + ")";

		}
		private void ModifyConditionTree(ConditionNode currentNode)
		{
			if(currentNode.ChildConditionNode1 != null)
			{
				ModifyConditionTree(currentNode.ChildConditionNode1);
			}
			if(currentNode.Condition == null)   //当当前节点是一个组合条件时,要为被组合的两个子条件两边添加括号
			{
				AddLeftBracketToMostLeftChildNode(currentNode.ChildConditionNode1);
				AddRightBracketToMostRightChildNode(currentNode.ChildConditionNode2);
			}
			if(currentNode.ChildConditionNode2 != null)
			{
				ModifyConditionTree(currentNode.ChildConditionNode2);
			}
		}
		private string GetRealFieldFullName(string fieldFullName)
		{
			string realFieldFullName = fieldFullName;
			while(realFieldFullName.StartsWith("("))
			{
				realFieldFullName = realFieldFullName.Substring(1,realFieldFullName.Length-1);
			}
			return realFieldFullName;

		}
		private string GetRealOperator(string operator1)
		{
			string realOperator = operator1;
			while(realOperator.EndsWith(")"))
			{
				realOperator = realOperator.Substring(0,realOperator.Length-1);
			}
			return realOperator;

		}
		private string GetRightBrackets(string operator1)
		{
			string realOperator = operator1;
			int index = operator1.IndexOf(")");
			if(index >= 0)
			{
				return realOperator.Substring(index,operator1.Length - index);
			}
			return ")";
		}
		private void GenerateConditionString(ConditionNode currentNode)
		{
			//以下对rootNode为根节点的二叉树进行中序遍历生成条件字符串

			if(currentNode.ChildConditionNode1 != null)
			{
				GenerateConditionString(currentNode.ChildConditionNode1);
			}
			if(currentNode.Condition != null)   //说明当前节点是一个原子条件
			{
				if(currentNode.Condition.Operator.EndsWith(")"))
				{
					conditionString += currentNode.Condition.FieldFullName + " " + GetRealOperator(currentNode.Condition.Operator) + " @" + GetRealFieldFullName(currentNode.Condition.FieldFullName) + GetRightBrackets(currentNode.Condition.Operator);
				}
				else
				{
					conditionString += currentNode.Condition.FieldFullName + " " + currentNode.Condition.Operator + " @" + GetRealFieldFullName(currentNode.Condition.FieldFullName);
				}
			}
			else                                //说明当前节点是一个组合条件
			{
				conditionString += " " + currentNode.Relation + " ";
			}
			if(currentNode.ChildConditionNode2 != null)
			{
				GenerateConditionString(currentNode.ChildConditionNode2);
			}
			
		}
		private void SaveConditionTreeToXml(XmlDocument doc, ConditionNode parentNode, XmlNode parentXmlNode)
		{
			XmlNode relationNode = null;
			XmlNode conditionNode = null;

			ConditionNode childNode1 = parentNode.ChildConditionNode1;
			ConditionNode childNode2 = parentNode.ChildConditionNode2;

			if(childNode1 != null)
			{
				if(childNode1.Relation != null && childNode1.Relation != string.Empty)
				{
					relationNode = doc.CreateElement(childNode1.Relation);
					parentXmlNode.AppendChild(relationNode);
					SaveConditionTreeToXml(doc,childNode1,relationNode);
				}
				else
				{
					conditionNode = CreateAtomConditionNode(AtomConditions.Rows.Find(childNode1.Condition.ConditionName),doc);
					parentXmlNode.AppendChild(conditionNode);
				}
			}
			if(childNode2 != null)
			{
				if(childNode2.Relation != null && childNode2.Relation != string.Empty)
				{
					relationNode = doc.CreateElement(childNode2.Relation);
					parentXmlNode.AppendChild(relationNode);
					SaveConditionTreeToXml(doc,childNode2,relationNode);
				}
				else
				{
					conditionNode = CreateAtomConditionNode(AtomConditions.Rows.Find(childNode2.Condition.ConditionName),doc);
					parentXmlNode.AppendChild(conditionNode);
				}
			}
			
		}

		
		#endregion
		
		private void SaveSelectedTables()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode tablesNode = doc.SelectSingleNode("QueryItem/Body/Tables");
			if(tablesNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("Tables"));
			}
			tablesNode.RemoveAll();

			XmlNode tableNode = null;
			XmlAttribute att = null;

			foreach(DataRow row in SelectedTables.Rows)
			{
				tableNode = doc.CreateElement("Table");

				att = doc.CreateAttribute("Name");
				att.Value = row["TableName"] as string;
				tableNode.Attributes.Append(att);

				att = doc.CreateAttribute("AliasName");
				att.Value = row["AliasName"] as string;
				tableNode.Attributes.Append(att);

				att = doc.CreateAttribute("ChineseName");
				att.Value = row["ChineseName"] as string;
				tableNode.Attributes.Append(att);

				tablesNode.AppendChild(tableNode);
			}
			QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);

		}
		private void SaveAtomConditionListData()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode conditionListDataNode = doc.SelectSingleNode("QueryItem/Body/AtomConditionListData");
			if(conditionListDataNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("AtomConditionListData"));
			}
			conditionListDataNode.RemoveAll();

			XmlNode conditionNode = null;
			XmlAttribute att = null;

			foreach(DataRow row in AtomConditions.Rows)
			{
				conditionNode = doc.CreateElement("AtomCondition");

				att = doc.CreateAttribute("name");
				att.Value = row["ConditionName"] as string;
				conditionNode.Attributes.Append(att);

				att = doc.CreateAttribute("fieldFullname");
				att.Value = row["FieldFullName"] as string;
				conditionNode.Attributes.Append(att);

				att = doc.CreateAttribute("fieldDataType");
				att.Value = row["fieldDataType"] as string;
				conditionNode.Attributes.Append(att);

				att = doc.CreateAttribute("operator");
				att.Value = row["Operator"] as string;
				conditionNode.Attributes.Append(att);

				att = doc.CreateAttribute("inputType");
				att.Value = row["InputControlType"] as string;
				conditionNode.Attributes.Append(att);

				att = doc.CreateAttribute("fieldChineseName");
				att.Value = row["ChineseName"] as string;
				conditionNode.Attributes.Append(att);

				att = doc.CreateAttribute("initdata");
				att.Value = row["InitialValue"] as string;
				conditionNode.Attributes.Append(att);

				att = doc.CreateAttribute("fieldvalue");
				att.Value = string.Empty;
				conditionNode.Attributes.Append(att);

				conditionListDataNode.AppendChild(conditionNode);
			}

			QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);


		}
		private void SaveComplicatedConditionListData()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode conditionListDataNode = doc.SelectSingleNode("QueryItem/Body/ComplicatedConditionListData");
			if(conditionListDataNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("ComplicatedConditionListData"));
			}
			conditionListDataNode.RemoveAll();

			XmlNode conditionNode = null;
			XmlAttribute att = null;

			foreach(DataRow row in ComplicatedConditions.Rows)
			{
				conditionNode = doc.CreateElement("ComplicatedCondition");

				att = doc.CreateAttribute("Condition1");
				att.Value = row["Condition1"] as string;
				conditionNode.Attributes.Append(att);

				att = doc.CreateAttribute("Condition2");
				att.Value = row["Condition2"] as string;
				conditionNode.Attributes.Append(att);

				att = doc.CreateAttribute("Name");
				att.Value = row["ConditionName"] as string;
				conditionNode.Attributes.Append(att);

				att = doc.CreateAttribute("Relation");
				att.Value = row["Relation"] as string;
				conditionNode.Attributes.Append(att);

				conditionListDataNode.AppendChild(conditionNode);
			}

			QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);


		}
		private string GetSelectedFieldsString()
		{
			if(SelectedFields.Rows.Count == 0)
			{
				this.Page.Response.Write("<script language='javascript'>alert('请至少选择一个要返回的字段！');</script>");
				generateSQLSuccess = false;
				return string.Empty;
			}

			string returnString = string.Empty;
			string fieldFullName = string.Empty;
			string fieldName = string.Empty;
			string dataType = string.Empty;
			string aliasName = string.Empty;
			string chineseName = string.Empty;
			string ownerTable = string.Empty;

			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode selectNode = doc.SelectSingleNode("QueryItem/Body/Select");
			if(selectNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("Select"));
			}
			selectNode.RemoveAll();
			XmlNode fieldNode = null;
			XmlAttribute att = null;
			foreach(DataRow row in SelectedFields.Rows)
			{
				fieldFullName = row["FieldFullName"] as string;
				fieldName = row["FieldName"] as string;
				dataType = row["DataType"] as string;
				aliasName = row["AliasName"] as string;
				chineseName = row["ChineseName"] as string;
				ownerTable = row["OwnerTable"] as string;

				if(returnString == string.Empty)
				{
					returnString += fieldFullName;
				}
				else
				{
					returnString += ",<br>" + fieldFullName;
				}
				if(aliasName != null && aliasName != string.Empty)
				{
					returnString += " AS " + aliasName;
				}

				fieldNode = doc.CreateElement("Field");

				att = doc.CreateAttribute("OwnerTable");
				att.Value = ownerTable;
				fieldNode.Attributes.Append(att);

				att = doc.CreateAttribute("FullFieldName");
				att.Value = fieldFullName;
				fieldNode.Attributes.Append(att);

				
				att = doc.CreateAttribute("FieldName");
				att.Value = fieldName;
				fieldNode.Attributes.Append(att);

				
				att = doc.CreateAttribute("DataType");
				att.Value = dataType;
				fieldNode.Attributes.Append(att);

				
				att = doc.CreateAttribute("AliasName");
				att.Value = aliasName;
				fieldNode.Attributes.Append(att);

				
				att = doc.CreateAttribute("ChineseName");
				att.Value = chineseName;
				fieldNode.Attributes.Append(att);

				selectNode.AppendChild(fieldNode);

			}

			QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);

			return returnString;

		}
		
		private string GetSelectedTablesWithRelationString()
		{
			XmlNode relationNode = null;   //节点<Relation>
			XmlNode table1Node = null;     //节点<Relation>下的第一个<Table>节点
			XmlNode table2Node = null;     //节点<Relation>下的第二个<Table>节点
			XmlNode andNode = null;        //节点<And>
			XmlNode joinFieldsNode = null; //节点<JoinFields>
			XmlNode tableNode = null;      //节点<Source>下的<Table>节点
			XmlAttribute att = null;       //用于添加某个节点下的属性
			string queryItemId = Page.Request.Params["queryItemId"];
			string tableString = string.Empty;
			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);
			XmlNode relationsNode = doc.SelectSingleNode("QueryItem/Body/Relations");
			if(relationsNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("Relations"));
			}
			relationsNode.RemoveAll();        //先移除<Relations>节点所有子节点和内容

			if(RelationTableHashTable.Count == 0)
			{
				if(SelectedTables.Rows.Count == 0)
				{
					Page.Response.Write("<script language='javascript'>alert('请至少选择一个表！');</script>");
					generateSQLSuccess = false;
					return string.Empty;
				}
				else if(SelectedTables.Rows.Count > 1)
				{
					Page.Response.Write("<script language='javascript'>alert('选择的表多于两个时，必须指定它们的关系！');</script>");
					generateSQLSuccess = false;
					return string.Empty;
				}
				
				tableNode = doc.CreateElement("Table");

				att = doc.CreateAttribute("TableName");
				att.Value = SelectedTables.Rows[0]["TableName"] as string;
				tableString = att.Value;
				tableNode.Attributes.Append(att);

				att = doc.CreateAttribute("AliasName");
				att.Value = SelectedTables.Rows[0]["AliasName"] as string;
				if(att.Value != null && att.Value != string.Empty)
				{
					tableString = tableString + " " + att.Value;
				}
				tableNode.Attributes.Append(att);

				att = doc.CreateAttribute("ChineseName");
				att.Value = SelectedTables.Rows[0]["ChineseName"] as string;
				tableNode.Attributes.Append(att);

				relationsNode.AppendChild(tableNode);

				QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);

			}
			else
			{
				foreach(string key in RelationTableHashTable.Keys)
				{
					string[] members = key.Split(new char[]{'|'});
					relationNode = doc.CreateElement("Relation");
					table1Node = doc.CreateElement("Table");
					table2Node = doc.CreateElement("Table");
					andNode = doc.CreateElement("And");

					att = doc.CreateAttribute("type");
					att.Value = members[1];
					relationNode.Attributes.Append(att);

					RelationFieldDataTable relationFields = RelationTableHashTable[key] as RelationFieldDataTable;
					
					att = doc.CreateAttribute("name");
					att.Value = relationFields.Rows[0]["Table1"] as string;
					table1Node.Attributes.Append(att);

					att = doc.CreateAttribute("name");
					att.Value = relationFields.Rows[0]["Table2"] as string;
					table2Node.Attributes.Append(att);

					if(relationFields.Rows.Count > 1)
					{
						foreach(DataRow row in relationFields.Rows)
						{
							joinFieldsNode = doc.CreateElement("JoinField");

							att = doc.CreateAttribute("Field1");
							att.Value = row["Field1"] as string;
							joinFieldsNode.Attributes.Append(att);

							att = doc.CreateAttribute("Field2");
							att.Value = row["Field2"] as string;
							joinFieldsNode.Attributes.Append(att);

							andNode.AppendChild(joinFieldsNode);
						}
						relationNode.AppendChild(table1Node);
						relationNode.AppendChild(table2Node);
						relationNode.AppendChild(andNode);
					}
					else
					{
						joinFieldsNode = doc.CreateElement("JoinField");

						att = doc.CreateAttribute("Field1");
						att.Value = relationFields.Rows[0]["Field1"] as string;
						joinFieldsNode.Attributes.Append(att);

						att = doc.CreateAttribute("Field2");
						att.Value = relationFields.Rows[0]["Field2"] as string;
						joinFieldsNode.Attributes.Append(att);
						
						relationNode.AppendChild(table1Node);
						relationNode.AppendChild(table2Node);
						relationNode.AppendChild(joinFieldsNode);
					}

					relationsNode.AppendChild(relationNode);

				}
				QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);
				
				foreach(ListItem item in this.currentTableRelationListBox.Items)
				{
					tableString += item.Value;
				}
			}
			
			return tableString;

		}

		private string GetConditionsString()
		{
			if(ComplicatedConditions.Rows.Count == 0)
			{
				if(AtomConditions.Rows.Count == 0)
				{
					string queryItemId = this.Page.Request.Params["queryItemId"];

					XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

					XmlNode conditionsNode = doc.SelectSingleNode("QueryItem/Body/Conditions");
					if(conditionsNode == null)
					{
						XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
						bodyNode.AppendChild(doc.CreateElement("Conditions"));
					}
					conditionsNode.RemoveAll();

					QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);

					return string.Empty;
				}
				else if(AtomConditions.Rows.Count >= 2)
				{
					Page.Response.Write("<script language='javascript'>alert('如果指定的条件个数大于两个,必须指定它们的关系！');</script>");
					generateSQLSuccess = false;
					return string.Empty;
				}
				else
				{
					DataRow row = AtomConditions.Rows[0];

					string queryItemId = this.Page.Request.Params["queryItemId"];

					XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

					XmlNode conditionsNode = doc.SelectSingleNode("QueryItem/Body/Conditions");
					if(conditionsNode == null)
					{
						XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
						bodyNode.AppendChild(doc.CreateElement("Conditions"));
					}
					conditionsNode.RemoveAll();
					
					conditionsNode.AppendChild(CreateAtomConditionNode(row,doc));

					QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);

					return row["FieldFullName"].ToString() + " " + row["Operator"].ToString() + " @" + row["FieldFullName"].ToString();
				}
			}
			ConditionNode rootNode = CreateComplicatedConditionNode(ComplicatedConditions.Rows[ComplicatedConditions.Rows.Count-1]);
			
			CreateConditionTree(rootNode);

			string queryItemId1 = this.Page.Request.Params["queryItemId"];

			XmlDocument doc1 = QueryItemManager.Instance.GetItemXML(queryItemId1);

			XmlNode conditionsNode1 = doc1.SelectSingleNode("QueryItem/Body/Conditions");
			if(conditionsNode1 == null)
			{
				XmlNode bodyNode = doc1.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc1.CreateElement("Conditions"));
			}
			conditionsNode1.RemoveAll();

			XmlNode rootRelationNode = doc1.CreateElement(rootNode.Relation);

			SaveConditionTreeToXml(doc1,rootNode,rootRelationNode);

			conditionsNode1.AppendChild(rootRelationNode);

			QueryItemManager.Instance.SaveQueryItem(doc1,queryItemId1);

			ModifyConditionTree(rootNode);

			GenerateConditionString(rootNode);

			if(conditionString.StartsWith("(") && conditionString.EndsWith(")"))
			{
				conditionString = conditionString.Substring(1,conditionString.Length - 2);
			}

			return conditionString;

		}
		
		private string GetSortFieldsString()
		{
			string returnString = string.Empty;
			int index = 0;

			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode orderNode = doc.SelectSingleNode("QueryItem/Body/Order");
			if(orderNode == null)
			{
				XmlNode bodyNode = doc.CreateElement("QueryItem/Body");
				bodyNode.AppendChild(doc.CreateElement("Order"));
			}
			orderNode.RemoveAll();
			XmlNode fieldNode = null;
			XmlAttribute att = null;

			foreach(DataRow row in SelectedSortFields.Rows)
			{
				if(index == 0)
				{
					returnString += row["FieldFullName"].ToString() + " " + row["SortType"].ToString();
				}
				else
				{
					returnString += ",<br>" + row["FieldFullName"].ToString() + " " + row["SortType"].ToString();	
				}

				fieldNode = doc.CreateElement("Field");

				att = doc.CreateAttribute("fieldFullName");
				att.Value = row["FieldFullName"].ToString();
				fieldNode.Attributes.Append(att);

				att = doc.CreateAttribute("sortType");
				att.Value = row["SortType"].ToString();
				fieldNode.Attributes.Append(att);

				orderNode.AppendChild(fieldNode);

				index++;

			}

			QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);

			return returnString;

		}

		
		private void showSQLButton_Click(object sender, EventArgs e)
		{
			SaveConnectionString();

			string selectedFieldString = GetSelectedFieldsString();
			if(generateSQLSuccess == false)
			{
				return;
			}
			string selectedTableWithRelationString = GetSelectedTablesWithRelationString();
			if(generateSQLSuccess == false)
			{
				return;
			}
			string conditionString = GetConditionsString();
			if(generateSQLSuccess == false)
			{
				return;
			}
			string sortFieldsString = GetSortFieldsString();
			if(generateSQLSuccess == false)
			{
				return;
			}

			SaveSelectedTables();
			SaveAtomConditionListData();
			SaveComplicatedConditionListData();

			this.showSQLTable.Rows[0].Cells[0].Text = "Select";
			this.showSQLTable.Rows[1].Cells[1].Text = selectedFieldString;
			this.showSQLTable.Rows[2].Cells[0].Text = "From";
			this.showSQLTable.Rows[3].Cells[1].Text = selectedTableWithRelationString;
			if(conditionString != string.Empty)
			{
				this.showSQLTable.Rows[4].Cells[0].Text = "Where";
			}
			else
			{
				this.showSQLTable.Rows[4].Cells[0].Text = "";
			}
			this.showSQLTable.Rows[5].Cells[1].Text = conditionString;

			if(sortFieldsString != string.Empty)
			{
				this.showSQLTable.Rows[6].Cells[0].Text = "Order By";
			}
			else
			{
				this.showSQLTable.Rows[6].Cells[0].Text = "";
			}
			this.showSQLTable.Rows[7].Cells[1].Text = sortFieldsString;

			string queryItemId = this.Page.Request.Params["queryItemId"];

			XmlDocument doc = QueryItemManager.Instance.GetItemXML(queryItemId);

			XmlNode sqlStringNode = doc.SelectSingleNode("QueryItem/SQLString");
			if(sqlStringNode == null)
			{
				sqlStringNode = doc.CreateElement("SQLString");
				doc.DocumentElement.AppendChild(sqlStringNode);
			}
			sqlStringNode.RemoveAll();
			XmlNode node = null;

			node = doc.CreateElement("SelectString");
			node.InnerText = selectedFieldString;
			sqlStringNode.AppendChild(node);

			node = doc.CreateElement("FromString");
			node.InnerText = selectedTableWithRelationString;
			sqlStringNode.AppendChild(node);

			node =doc.CreateElement("ConditionString");
			node.InnerText = conditionString;
			sqlStringNode.AppendChild(node);
			
			node = doc.CreateElement("OrderString");
			node.InnerText = sortFieldsString;
			sqlStringNode.AppendChild(node);
			

			QueryItemManager.Instance.SaveQueryItem(doc,queryItemId);

		}
		
		
		private void InitPreviewSQLControl(object sender, EventArgs e)
		{

		}

		
		#endregion
		
	}
}
