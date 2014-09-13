using System;
using System.Data;
using System.Xml;
using System.IO;



namespace NetFocus.Components.SearchComponent
{
	public class QueryItemManager
	{
		static QueryItemManager instance = null;
		private string datapath;

		private QueryItemManager()
		{}

		public static QueryItemManager Instance
		{
			get
			{
				if(instance == null)
				{
					instance = new QueryItemManager();
				}
				return instance;
			}
		}

		public string DataPath
		{
			get
			{
				return datapath;
			}
			set
			{
				datapath = value;
			}
		}

		public void DeleteQueryItems(DeleteQueryKindEventArgs e)
		{
			string queryKindId = e.QueryKindId;

			string itemsFile = DataPath + @"items\Items.xml";
			string itemFile = string.Empty;
			XmlDocument doc = new XmlDocument();

			doc.Load(itemsFile);

			XmlNode rootNode = doc.DocumentElement;

			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["kindId"].Value == queryKindId)
				{
					rootNode.RemoveChild(node);
					itemFile = DataPath + @"items\" + node.Attributes["id"].Value + ".item";
					if(File.Exists(itemFile))
					{
						File.Delete(itemFile);
					}
					doc.Save(itemsFile);

					DeleteQueryItems(e);
				}
			}

		}



		private string GetSqlString(XmlNode sqlStringNode)
		{
			return "";
		}
		private bool SetAttributes(DataRow row, string queryItemId)
		{
			string itemFile = DataPath + @"items\" + queryItemId + ".item";
			
			if(!File.Exists(itemFile))
			{
				return false;
			}

			XmlDocument doc = new XmlDocument();

			doc.Load(itemFile);

			XmlNode rootNode = doc.DocumentElement;

			row["name"] = rootNode.Attributes["name"].Value;

			XmlNode descriptionNode = rootNode.SelectSingleNode("Description");
			row["description"] = descriptionNode == null ? string.Empty : descriptionNode.InnerText;

			XmlNode connectionStringNode = rootNode.SelectSingleNode("ConnectionString");
			row["connectionString"] = connectionStringNode == null ? string.Empty : connectionStringNode.InnerText;

			XmlNode sqlStringNode = rootNode.SelectSingleNode("Body");
			if(sqlStringNode == null)
			{
				return false;
			}

			row["sqlString"] = GetSqlString(sqlStringNode);

			return true;


		}

		
		public int UpdateQueryItem(string queryItemId, string name, string description, string kindId)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode rootNode = null;
			string itemsFile = DataPath + @"items\Items.xml";
			string itemFile = DataPath + @"items\" + queryItemId + ".item";
			
			doc.Load(itemsFile);

			rootNode = doc.DocumentElement;

			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["name"].Value == name && node.Attributes["kindId"].Value == kindId)
				{
					if(node.Attributes["id"].Value != queryItemId)
					{
						return 0;
					}
				}
				
			}
			
			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["id"].Value == queryItemId)
				{
					node.Attributes["name"].Value = name;
					break;
				}
			}

			doc.Save(itemsFile);

			doc.Load(itemFile);

			rootNode = doc.DocumentElement;

			rootNode.Attributes["name"].Value = name;
			XmlNode descriptionNode = rootNode.SelectSingleNode("Description");
			if(descriptionNode != null)
			{
				descriptionNode.InnerText = description;
			}

			doc.Save(itemFile);

			return 1;


		}
		
		
		public QueryItemTable RetrieveQueryItemByKindId(string kindId)
		{
			if(kindId.Trim() == string.Empty)
			{
				return null;
			}

			QueryItemTable returnTable = new QueryItemTable();

			string itemsFile = DataPath + @"items\Items.xml";

			XmlDocument doc = new XmlDocument();

			doc.Load(itemsFile);

			XmlNode rootNode = doc.DocumentElement;

			DataRow row = null;

			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["kindId"].Value == kindId)
				{
					row = returnTable.NewRow();
					row["id"] = node.Attributes["id"].Value;
					row["kindId"] = kindId;

					if(SetAttributes(row,node.Attributes["id"].Value))
					{
						returnTable.Rows.Add(row);
					}
				}

			}

			return returnTable;

		}

		public QueryItemTable RetrieveQueryItemById(string queryItemId)
		{
			if(queryItemId.Trim() == string.Empty)
			{
				return null;
			}

			QueryItemTable returnTable = new QueryItemTable();

			string itemFile = DataPath + @"items\" + queryItemId.Trim() + ".item";

			XmlDocument doc = new XmlDocument();

			if(!File.Exists(itemFile))
			{
				return null;
			}

			doc.Load(itemFile);

			XmlNode rootNode = doc.DocumentElement;

			DataRow row = returnTable.NewRow();

			row["id"] = queryItemId;
			row["name"] = rootNode.Attributes["name"].Value;
			row["kindId"] = rootNode.Attributes["kindId"].Value;

			XmlNode descriptionNode = rootNode.SelectSingleNode("Description");
			if(descriptionNode != null)
			{
				row["description"] = descriptionNode.InnerText;
			}

			XmlNode connectionStringNode = rootNode.SelectSingleNode("ConnectionString");
			if(connectionStringNode != null)
			{
				row["connectionString"] = connectionStringNode.InnerText;
			}

			XmlNode sqlStringNode = rootNode.SelectSingleNode("Body");
			if(sqlStringNode != null)
			{
				row["sqlString"] = sqlStringNode.InnerText;
			}

			returnTable.Rows.Add(row);

			return returnTable;

		}

		
		public void DeleteQueryItem(string queryItemId)
		{
			string itemsFile = DataPath + @"items\Items.xml";
			string itemFile = DataPath + @"items\" + queryItemId + ".item";
			XmlDocument doc = new XmlDocument();

			doc.Load(itemsFile);

			XmlNode rootNode = doc.DocumentElement;

			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["id"].Value == queryItemId)
				{
					rootNode.RemoveChild(node);
					break;
				}
			}

			doc.Save(itemsFile);

			
			if(File.Exists(itemFile))
			{
				File.Delete(itemFile);
			}
		}

		
		public int CreateQueryItem(string name, string description, string kindId)
		{
			XmlDocument doc = new XmlDocument();
			XmlAttribute att = null;
			string queryItemId = Guid.NewGuid().ToString();
			string itemsFile = DataPath + @"items\Items.xml";
			string itemFile = DataPath + @"items\" + queryItemId + ".item";
			
			doc.Load(itemsFile);

			foreach(XmlNode node in doc.DocumentElement.ChildNodes)
			{
				if(node.Attributes["kindId"].Value == kindId)
				{
					if(node.Attributes["name"].Value == name)
					{
						return 0;
					}
				}
			}

			XmlNode queryItemNode = doc.CreateElement("QueryItem");

			att = doc.CreateAttribute("id");
			att.Value = queryItemId;
			queryItemNode.Attributes.Append(att);

			att = doc.CreateAttribute("name");
			att.Value = name;
			queryItemNode.Attributes.Append(att);

			att = doc.CreateAttribute("kindId");
			att.Value = kindId;
			queryItemNode.Attributes.Append(att);

			doc.DocumentElement.AppendChild(queryItemNode);

			doc.Save(itemsFile);


			//以下是新建一个查询项文件
			//
			doc.LoadXml("<?xml version=\"1.0\" encoding=\"gb2312\" ?>\n<QueryItem />");

			XmlNode rootNode = doc.DocumentElement;

			XmlNode connectionStringNode = doc.CreateElement("ConnectionString");
			XmlNode descriptionNode = doc.CreateElement("Description");

			att = doc.CreateAttribute("id");
			att.Value = queryItemId;
			rootNode.Attributes.Append(att);

			att = doc.CreateAttribute("name");
			att.Value = name;
			rootNode.Attributes.Append(att);

			att = doc.CreateAttribute("kindId");
			att.Value = kindId;
			rootNode.Attributes.Append(att);

			descriptionNode.InnerText = description;

			XmlNode bodyNode = doc.CreateElement("Body");
			XmlNode selectNode = doc.CreateElement("Select");
			XmlNode tablesNode = doc.CreateElement("Tables");
			XmlNode relationsNode = doc.CreateElement("Relations");
			XmlNode atomConditionListDataNode = doc.CreateElement("AtomConditionListData");
			XmlNode complicatedConditionListDataNode = doc.CreateElement("ComplicatedConditionListData");
			XmlNode conditionsNode = doc.CreateElement("Conditions");
			XmlNode orderNode = doc.CreateElement("Order");

			XmlNode sqlStringNode = doc.CreateElement("SQLString");
			XmlNode selectStringNode = doc.CreateElement("SelectString");
			XmlNode fromStringNode = doc.CreateElement("FromString");
			XmlNode conditionStringNode = doc.CreateElement("ConditionString");
			XmlNode orderStringNode = doc.CreateElement("OrderString");

			bodyNode.AppendChild(selectNode);
			bodyNode.AppendChild(tablesNode);
			bodyNode.AppendChild(relationsNode);
			bodyNode.AppendChild(atomConditionListDataNode);
			bodyNode.AppendChild(complicatedConditionListDataNode);
			bodyNode.AppendChild(conditionsNode);
			bodyNode.AppendChild(orderNode);

			sqlStringNode.AppendChild(selectStringNode);
			sqlStringNode.AppendChild(fromStringNode);
			sqlStringNode.AppendChild(conditionStringNode);
			sqlStringNode.AppendChild(orderStringNode);

			rootNode.AppendChild(connectionStringNode);
			rootNode.AppendChild(descriptionNode);
			rootNode.AppendChild(bodyNode);
			rootNode.AppendChild(sqlStringNode);

			doc.Save(itemFile);

			return 1;

		}


		public XmlDocument GetItemXML(string queryItemId)
		{
			XmlDocument doc = new XmlDocument();
			string itemFile = DataPath + @"items\" + queryItemId + ".item";
			doc.Load(itemFile);
			
			return doc;
		}

		public void SaveQueryItem(XmlDocument doc, string queryItemId)
		{
			string itemFile = DataPath + @"items\" + queryItemId + ".item";
			doc.Save(itemFile);
		}

	}
}
