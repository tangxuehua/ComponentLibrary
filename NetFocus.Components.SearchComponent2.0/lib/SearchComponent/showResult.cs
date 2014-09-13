using System;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;


namespace NetFocus.Components.SearchComponent
{
	public class ShowResult : TemplatedWebControl 
	{
		private DataGrid dataGrid1;
		private string ConditionString = string.Empty;

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

		protected override void AttachChildControls()
		{
			dataGrid1 = FindControl("DataGrid1") as DataGrid;
			dataGrid1.PageIndexChanged += new DataGridPageChangedEventHandler(dataGrid1_PageIndexChanged);

			string queryItemId = Page.Request.Params["queryItemId"];

			if(queryItemId != null && queryItemId != string.Empty)
			{
				ItemFile=Page.Server.MapPath(DataPath + "items/" + queryItemId + ".item");
				ShowResultBySQLString(CreateSqlString());
			}

		}

		private string GetConnectionString()
		{
			XmlDocument doc=new XmlDocument();

			doc.Load(ItemFile);

			try
			{
				XmlNode node = doc.SelectSingleNode("QueryItem/ConnectionString");
				return node.InnerText;
				
			}
			catch
			{
				return string.Empty;
			}
	
		}

		private string GetOrderStr()
		{
			XmlDocument doc=new XmlDocument();

			doc.Load(ItemFile);

			try
			{
				XmlNode sqlStrNode = doc.SelectSingleNode("QueryItem/SQLString");
				if(sqlStrNode.HasChildNodes)
				{
					XmlNode orderNode = sqlStrNode.SelectSingleNode("OrderString");
					return orderNode.InnerText;
				}
			}
			catch{}
	
			return string.Empty;

		}
    
		private string GetSelectAndFromString()
		{
			string SelectAndFrom = string.Empty;
			string SelectString = string.Empty;
			string FromString = string.Empty;

			XmlDocument doc = new XmlDocument();
			doc.Load(ItemFile);
			try
			{
				XmlNode  sqlStrNode = doc.SelectSingleNode("QueryItem/SQLString");
				if(sqlStrNode.HasChildNodes)
				{
					XmlNode selectNode = sqlStrNode.SelectSingleNode("SelectString");
					SelectString = selectNode.InnerText;
					XmlNode fromNode = sqlStrNode.SelectSingleNode("FromString");
					FromString = fromNode.InnerText;
				}
				SelectAndFrom = "Select " + SelectString + " FROM " + FromString;
				
			}
			catch{}	
			return SelectAndFrom;			
		}

		private void GetConditionStr()
		{			
			XmlDocument doc = new XmlDocument();
			doc.Load(ItemFile);
			XmlNode ConditionsNode = doc.SelectSingleNode("//Conditions");
			if(ConditionsNode.HasChildNodes)
			{

				XmlNode Nodes = ConditionsNode.ChildNodes[ConditionsNode.ChildNodes.Count-1];
				if(Nodes.Name  == "Condition")
				{
					string fieldname = Nodes.Attributes["fieldFullname"].Value;
					string operators = Nodes.Attributes["operator"].Value;
					string fieldvalue = Nodes.Attributes["fieldvalue"].Value;
					string fieldDataType = Nodes.Attributes["fieldDataType"].Value;

					if(fieldvalue != null && fieldvalue != string.Empty)
					{
						if(operators == "LIKE")
						{
							fieldvalue = "%" + fieldvalue +"%";
						}
	
						if(fieldDataType == "Int32" || fieldDataType == "Money")
						{
							ConditionString =fieldname +" "+ operators +" "+ fieldvalue;
						}
						else
						{
							ConditionString =fieldname +" "+ operators +" '"+ fieldvalue + "'";	
						}
					}
				}
				if(Nodes.Name == "Or" || Nodes.Name == "And")
				{					    
					string Str = Condition(Nodes);
					if(Str != null && Str != string.Empty)
					{
						ConditionString = Str.Substring(1,Str.Length - 2);
					}
					else
					{
						ConditionString = string.Empty;
					}

				}

			}
		}		

		private string CreateSqlString()
		{
			string sqlString = string.Empty;
			string selectAndfrom = GetSelectAndFromString();
			string orderString = string.Empty;

			if(selectAndfrom == string.Empty)
			{
				return string.Empty;
			}
			sqlString = selectAndfrom;

			GetConditionStr();
			if(ConditionString != string.Empty)
			{
				sqlString += " " + " Where " + ConditionString; 
			}
			orderString = GetOrderStr();
			if(orderString != string.Empty)
			{
				sqlString += " Order By " + orderString;
			}

			return sqlString; 
		}


		private string Condition(XmlNode Node)
		{
			string leftString = string.Empty;
			string rightString = string.Empty;
			string ConditionStrings = string.Empty;
			string fieldname = string.Empty ;
			string operators = string.Empty ;
			string fieldvalue = string.Empty ;
			string fieldDataType = string.Empty;

			//获得根节点的两个子节点
			XmlNode leftChildNode = Node.ChildNodes[Node.ChildNodes.Count - 2];
			XmlNode rightChildNode = Node.ChildNodes[Node.ChildNodes.Count - 1]; 
			
			//判断左边的节点是否有子节点,如果没有leftChildNode肯定是<Condition>节点
			if(leftChildNode.HasChildNodes)
			{
				if(Condition(leftChildNode) != string.Empty)
					leftString = "(" + Condition(leftChildNode);
				else 
					leftString = string.Empty ;
			}
			else
			{
				fieldname = leftChildNode.Attributes["fieldFullname"].Value;
				operators = leftChildNode.Attributes["operator"].Value;
				fieldvalue = leftChildNode.Attributes["fieldvalue"].Value;
				fieldDataType = leftChildNode.Attributes["fieldDataType"].Value;
				
				if(fieldvalue == string.Empty)
				{
					leftString = string.Empty;
				}
				else
				{
					if(operators == "LIKE")
					{
						fieldvalue = "%" + fieldvalue +"%";
					}
					if(fieldDataType == "Int32" || fieldDataType == "Money")
					{
						leftString ="(" + fieldname +" "+ operators +" "+ fieldvalue;
					}
					else
					{
						leftString ="(" + fieldname +" "+ operators +" '"+ fieldvalue + "'";	
					}
				}				
			}

			//判断右边的节点是否有子节点,如果没有rightChildNode肯定是<Condition>节点
			if(rightChildNode.HasChildNodes)
			{
				if(Condition(rightChildNode) != string.Empty)
					rightString =  Condition(rightChildNode) + ")";	
				else
					rightString = string.Empty ;
			}
			else
			{			
				fieldname = rightChildNode.Attributes["fieldFullname"].Value;
				operators = rightChildNode.Attributes["operator"].Value;
				fieldvalue = rightChildNode.Attributes["fieldvalue"].Value;
				fieldDataType = rightChildNode.Attributes["fieldDataType"].Value;
				
				if(fieldvalue == string.Empty)
				{
					rightString = string.Empty;
				}
				else 
				{
					if(operators == "LIKE")
					{
						fieldvalue = "%" + fieldvalue +"%";
					}
					if(fieldDataType == "Int32" || fieldDataType == "Money")
					{
						rightString =fieldname +" "+ operators +" "+ fieldvalue + ")";	
					}
					else
					{
						rightString =fieldname +" "+ operators +" '"+ fieldvalue + "')";	
					}
				}
			}
			//连接左子节点和右子节点
				
			if(leftString != string.Empty && rightString != string.Empty)
				ConditionStrings = leftString + " " + Node.Name+" " + rightString;	
            		
			if(leftString == string.Empty && rightString == string.Empty)
				ConditionStrings = string.Empty;
            		
			if(leftString == string.Empty && rightString != string.Empty)
				ConditionStrings = "("+rightString;

			if(leftString != string.Empty && rightString == string.Empty)
				ConditionStrings = leftString+")";
				
			return ConditionStrings;
		}

		private string RemoveNewLineMark(string sourceString)
		{
			return sourceString.Replace("<br>","");
		}
		private void ShowResultBySQLString(string sqlString)
		{
			SqlConnection conn = new SqlConnection();
			conn.ConnectionString = GetConnectionString();
			if(conn.ConnectionString != string.Empty)
			{
				SqlCommand command = conn.CreateCommand();
				command.CommandText = RemoveNewLineMark(sqlString);
				SqlDataAdapter apt = new SqlDataAdapter();
				apt.SelectCommand = command;
				DataSet ds = new DataSet();
				try
				{
					apt.Fill(ds);
				}
				catch
				{
					Page.Response.Write("<script language='javascript'>alert('执行查询项时发生错误！');</script>");
					return;
				}
				this.dataGrid1.DataSource = ds.Tables[0];
				this.dataGrid1.DataBind();
			}
		}
	

		private void dataGrid1_PageIndexChanged(object sender,DataGridPageChangedEventArgs e)
		{
			this.dataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.dataGrid1.DataBind();
		}



	}


}