using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml.XPath;
using System.Xml;
using System.Data.SqlClient;


namespace NetFocus.Components.SearchComponent
{
	public class ItemSearch : TemplatedWebControl 
	{			
		private ArrayList currentControls = null;
		private Button saveButton;
		private Button queryButton;
		private Panel p;
		private Table t;
		private Label showMessage;
		
		private String Description 
		{
			get 
			{
				Object state = ViewState["Description"];
				return state == null ? string.Empty : (string)state;
			}
			set 
			{
				ViewState["Description"] = value;
			}
		}
		private String ItemFile 
		{
			get 
			{
				Object state = ViewState["ItemFile"];
				return state == null ? string.Empty : (string)state;
			}
			set 
			{
				ViewState["ItemFile"] = value;
			}
		}
		
		private Hashtable NameList
		{
			get 
			{
				if(ViewState["NameList"] == null)
				{
					ViewState["NameList"] = new Hashtable();
				}
				return ViewState["NameList"] as Hashtable;
			}
			set 
			{
				ViewState["NameList"] = value;
			}
		}

		private Hashtable InitData
		{
			get 
			{
				if(ViewState["InitData"] == null)
				{
					ViewState["InitData"] = new Hashtable();
				}
				return ViewState["InitData"] as Hashtable;
			}
			set 
			{
				ViewState["InitData"] = value;
			}
		}

		private Hashtable InputType
		{
			get 
			{
				if(ViewState["InputType"] == null)
				{
					ViewState["InputType"] = new Hashtable();
				}
				return ViewState["InputType"] as Hashtable;
			}
			set 
			{
				ViewState["InputType"] = value;
			}
		}

		private Hashtable FieldChineseName
		{
			get 
			{
				if(ViewState["FieldChineseName"] == null)
				{
					ViewState["FieldChineseName"] = new Hashtable();
				}
				return ViewState["FieldChineseName"] as Hashtable;
			}
			set 
			{
				ViewState["FieldChineseName"] = value;
			}
		}

		private ArrayList CurrentControls
		{
			get 
			{
				if(currentControls == null)
				{
					currentControls = new ArrayList();
				}
				return currentControls;
			}
			set 
			{
				currentControls = value;
			}
		}


		protected override void AttachChildControls()
		{
			p = (Panel)FindControl("P");
			t = (Table)FindControl("T");
			showMessage = (Label)FindControl("showMessage");
			saveButton = FindControl("Save") as Button;
			queryButton = FindControl("Query") as Button;

			queryButton.Click += new EventHandler(queryButton_Click);
			saveButton.Click += new EventHandler(saveButton_Click);

			if(!Page.IsPostBack)
			{
				InitControl();
			}
			else
			{
				Label descriptionText = new Label();
				descriptionText.Text = this.Description;
				p.Controls.Add(descriptionText);	

				IEnumerator inputTypeEnumerator = InputType.GetEnumerator();
				IEnumerator fieldChineseNameEnumerator = FieldChineseName.GetEnumerator();
				IEnumerator nameListEnumerator = NameList.GetEnumerator();
				IEnumerator initDataEnumerator = InitData.GetEnumerator();

				while(inputTypeEnumerator.MoveNext() && fieldChineseNameEnumerator.MoveNext() && 
					nameListEnumerator.MoveNext() && initDataEnumerator.MoveNext())
				{
					string type = ((DictionaryEntry)inputTypeEnumerator.Current).Value.ToString();
					string chinesefieldname = ((DictionaryEntry)fieldChineseNameEnumerator.Current).Value.ToString();
					string name = ((DictionaryEntry)nameListEnumerator.Current).Value.ToString();
					string datalist = ((DictionaryEntry)initDataEnumerator.Current).Value.ToString();		
		
					Control c = CreateControl(type,chinesefieldname,name,datalist);
					CurrentControls.Add(c);
				}
			}
		}


		private void InitControl()
		{
			string queryItemId = this.Page.Request.Params["queryItemId"];
			if(queryItemId == null)
			{
				showMessage.Text = "您还没选择查询业务!";
				return;
			}
			ItemFile=Page.Server.MapPath(DataPath + "items/" + queryItemId + ".item");			
			getInputType();	
	    
			Label descriptionText = new Label();
			descriptionText.Text = this.Description;
			p.Controls.Add(descriptionText);			

			IEnumerator inputTypeEnumerator = InputType.GetEnumerator();
			IEnumerator fieldChineseNameEnumerator = FieldChineseName.GetEnumerator();
			IEnumerator nameListEnumerator = NameList.GetEnumerator();
			IEnumerator initDataEnumerator = InitData.GetEnumerator();

			while(inputTypeEnumerator.MoveNext() && fieldChineseNameEnumerator.MoveNext() && 
				nameListEnumerator.MoveNext() && initDataEnumerator.MoveNext())
			{
				string type = ((DictionaryEntry)inputTypeEnumerator.Current).Value.ToString();
				string chinesefieldname = ((DictionaryEntry)fieldChineseNameEnumerator.Current).Value.ToString();
				string name = ((DictionaryEntry)nameListEnumerator.Current).Value.ToString();
				string datalist = ((DictionaryEntry)initDataEnumerator.Current).Value.ToString();		
		
				Control c = CreateControl(type,chinesefieldname,name,datalist);
				CurrentControls.Add(c);
			}

		}


		//获取查询字段的节点信息<Condition>
		private void getInputType()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(ItemFile);			
			XmlNode descriptionNode =doc.SelectSingleNode("QueryItem/Description");
			Description = descriptionNode.InnerText;

			XmlNode ConditionsNode = doc.SelectSingleNode("QueryItem/Body/Conditions");			
			if(ConditionsNode.HasChildNodes)
			{
				XmlNodeList items = ConditionsNode.SelectNodes("//Condition");
				int index = 0;
				foreach(XmlNode item in items)
				{
					if(item.Name == "Condition")
					{
						this.FieldChineseName.Add(index.ToString(),item.Attributes["fieldChineseName"].Value);
						this.InputType.Add(index.ToString(),item.Attributes["inputType"].Value);
						this.NameList.Add(index.ToString(),item.Attributes["name"].Value);
						this.InitData.Add(index.ToString(),item.Attributes["initdata"].Value);					
						index += 1;
					}

				}
			}			
		}
		
		
		//自动生成查询界面，包括查询名称，输入框类型，对应查询字段ID，输入框的初始值；
		private Control CreateControl(string inputType,string chinesefieldname,string name,string datalist)
		{			    
			//在界面上显示查询框，并为每个查询输入框指定ID，这里ID就是查询字段名；
			Control c=typeof(Button).BaseType.Assembly.CreateInstance("System.Web.UI.WebControls."+inputType) as Control;
			c.ID=name;	
			Label lb=new Label();
			lb.Text=chinesefieldname+":";
			
			TableRow row = null;
			TableCell cell = null;
			TableCell cell2 = null;

			if(t.Rows.Count <= 0)
			{
				row = new TableRow();
				cell = new TableCell();				
				cell2 = new TableCell();

				row.Cells.Add(cell);
				row.Cells.Add(cell2);
				cell.Controls.Add(lb);
				cell2.Controls.Add(c);
				t.Rows.Add(row);
			}
			else
			{
				if(t.Rows[t.Rows.Count-1].Cells.Count == 2)
				{
					cell = new TableCell();
					cell2 =new TableCell();
					cell.Controls.Add(lb);
					cell2.Controls.Add(c);
					t.Rows[t.Rows.Count-1].Cells.Add(cell);
					t.Rows[t.Rows.Count-1].Cells.Add(cell2);
				}
				else
				{
					row = new TableRow();
					cell = new TableCell();
					cell2 = new TableCell();
		
					row.Cells.Add(cell);
					row.Cells.Add(cell2);
					cell.Controls.Add(lb);
					cell2.Controls.Add(c);
					t.Rows.Add(row);
				}
			}

			//为输入框设置初始值；									  
			if(c.GetType().ToString()=="System.Web.UI.WebControls.TextBox")
			{
				TextBox tb=new TextBox();
				tb=(TextBox)c;
				tb.Text=datalist;				
			}
			if(c.GetType().ToString()=="System.Web.UI.WebControls.DropDownList")
			{
				DropDownList dp=new DropDownList();
				dp=(DropDownList)c;
				dp.DataSource=datalist.Split('|');
				dp.DataBind();
			}
			if(c.GetType().ToString()=="System.Web.UI.WebControls.RadioButtonList")
			{
				RadioButtonList radio=new RadioButtonList();
				radio=(RadioButtonList)c;
				radio.DataSource=datalist.Split('|');
				radio.DataBind();
			}
			return c;
		}

		//根据不同的输入控件类型，得到查询值；这里指定了三种类型
		private string GetFieldValue(Control obj)
		{
			string fieldValue=null;
			if(obj.GetType().ToString()=="System.Web.UI.WebControls.TextBox")
			{
				TextBox tb=new TextBox();
				tb=(TextBox)obj;
				fieldValue=tb.Text;
									
			}
			if(obj.GetType().ToString()=="System.Web.UI.WebControls.DropDownList")
			{
				DropDownList dp=new DropDownList();
				dp=(DropDownList)obj;
				fieldValue=dp.SelectedItem.Value;
			
			}
			if(obj.GetType().ToString() == "System.Web.UI.WebControls.RadioButtonList")
			{
				RadioButtonList Radio=new RadioButtonList();
				Radio=(RadioButtonList)obj;
				fieldValue=Radio.SelectedValue;				
			}
			return fieldValue;
		}

		//设置或更改查询条件值；
		private void setFieldValue(XmlNodeList xn,string fieldvalue,string name)
		{		
			foreach(XmlNode item in xn)
			{
				if(item.Attributes["name"].Value==name)
					item.Attributes["fieldvalue"].Value=fieldvalue;									
			}					
		}


		private void saveButton_Click(object sender, EventArgs e)
		{
			try
			{
				XmlDocument doc=new XmlDocument();
				doc.Load(ItemFile);
				XmlNodeList xn = doc.SelectNodes("//Condition");
				if(xn.Count != 0)
				{
					foreach(Control obj in CurrentControls)
					{
						string name=obj.ID;
						string fieldvalue=GetFieldValue(obj);
						setFieldValue(xn,fieldvalue,name);
					}
					doc.Save(ItemFile);	
					showMessage.Text = "成功将查询条件保存到查询项文件！您可以直接查询或查看查询条件!";		
				}				
			}
			catch{}
		}


		private void queryButton_Click(object sender, EventArgs e)
		{
			if(this.Page.Request.Params["queryItemId"] == null || this.Page.Request.Params["queryItemId"] == string.Empty)
			{
				return;
			}
			string showResultPage = DataPath + "pages/showResult.aspx?queryItemId=" + this.Page.Request.Params["queryItemId"] + "&datapath=" + DataPath;
			Page.Response.Write("<script language='javascript'>window.open('" + showResultPage + "');</script>");
		}


	}
}