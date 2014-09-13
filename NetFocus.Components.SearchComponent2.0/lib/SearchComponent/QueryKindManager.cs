using System;
using System.Data;
using System.Xml;
using System.IO;



namespace NetFocus.Components.SearchComponent
{
	public delegate void DeleteQueryKindEventHandler(DeleteQueryKindEventArgs e);
	
	public class DeleteQueryKindEventArgs : EventArgs
	{
		private string queryKindId = string.Empty;

		public DeleteQueryKindEventArgs(string queryKindId)
		{
			this.queryKindId = queryKindId;
		}

		public string QueryKindId
		{
			get
			{
				return queryKindId;
			}
			set
			{
				queryKindId = value;
			}
		}

	}
	
	
	public class QueryKindManager
	{
		static QueryKindManager instance = null;

		private string datapath;

		private QueryKindManager()
		{}

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

		public static QueryKindManager Instance
		{
			get
			{
				if(instance == null)
				{
					instance = new QueryKindManager();
				}
				return instance;
			}
		}


		public QueryKindTable RetrieveQueryKindByParentId(string parentId)
		{
			if(parentId.Trim() == string.Empty)
			{
				return null;
			}

			QueryKindTable returnTable = new QueryKindTable();

			string itemKindFile = DataPath + @"items\ItemKinds.xml";

			XmlDocument doc = new XmlDocument();

			doc.Load(itemKindFile);

			XmlNode rootNode = doc.DocumentElement;

			DataRow row = null;

			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["parentId"].Value == parentId)
				{
					row = returnTable.NewRow();
					row["id"] = node.Attributes["id"].Value;
					row["name"] = node.Attributes["name"].Value;
					row["description"] = node.Attributes["description"].Value;
					row["createDate"] = node.Attributes["createDate"].Value;
					row["parentId"] = parentId;

					returnTable.Rows.Add(row);
				}

			}

			return returnTable;

		}

		
		public int CreateQueryKind(string queryKindId, string name, string description, DateTime createDate, string parentId)
		{
			string itemKindFile = DataPath + @"items\ItemKinds.xml";

			XmlDocument doc = new XmlDocument();

			doc.Load(itemKindFile);

			XmlNode rootNode = doc.DocumentElement;

			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["parentId"].Value == parentId)
				{
					if(node.Attributes["name"].Value == name)
					{
						return 0;
					}
				}
			}

			XmlNode newNode = doc.CreateElement("QueryKind");
			XmlAttribute att = null;

			att = doc.CreateAttribute("id");
			att.Value = queryKindId;
			newNode.Attributes.Append(att);

			att = doc.CreateAttribute("name");
			att.Value = name;
			newNode.Attributes.Append(att);

			att = doc.CreateAttribute("description");
			att.Value = description;
			newNode.Attributes.Append(att);

			att = doc.CreateAttribute("createDate");
			att.Value = createDate.ToShortDateString();
			newNode.Attributes.Append(att);

			att = doc.CreateAttribute("parentId");
			att.Value = parentId;
			newNode.Attributes.Append(att);

			rootNode.AppendChild(newNode);

			doc.Save(itemKindFile);

			return 1;

		}

		
		public int UpdateQueryKind(string queryKindId, string name, string description, string parentId)
		{
			string itemKindFile = DataPath + @"items\ItemKinds.xml";

			XmlDocument doc = new XmlDocument();

			doc.Load(itemKindFile);

			XmlNode rootNode = doc.DocumentElement;

			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["name"].Value == name && node.Attributes["parentId"].Value == parentId)
				{
					if(node.Attributes["id"].Value != queryKindId)
					{
						return 0;
					}
				}
			}
			
			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["id"].Value == queryKindId)
				{
					node.Attributes["name"].Value = name;
					node.Attributes["description"].Value = description;
					break;
				}
			}

			doc.Save(itemKindFile);

			return 1;

		}

		
		private int GetChildNodeCount(XmlNode rootNode, XmlNode parentNode)
		{
			int count = 0;

			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["parentId"].Value == parentNode.Attributes["id"].Value)
				{
					count++;
				}
			}

			return count;

		}

		
		public event DeleteQueryKindEventHandler DeleteEvent;

		protected virtual void OnDeleteEvent(DeleteQueryKindEventArgs e)
		{
			if(DeleteEvent != null)
			{
				DeleteEvent(e);
			}
		}
		
		private void DeleteChildQueryKinds(XmlNode rootNode, XmlNode parentNode)
		{
			foreach(XmlNode node in rootNode.ChildNodes)  //循环并递归删除其子节点
			{
				if(node.Attributes["parentId"].Value == parentNode.Attributes["id"].Value)
				{
					if(GetChildNodeCount(rootNode,node) == 0)
					{
						rootNode.RemoveChild(node);
						OnDeleteEvent(new DeleteQueryKindEventArgs(node.Attributes["id"].Value));
						DeleteChildQueryKinds(rootNode,parentNode);
					}
					else
					{
						DeleteChildQueryKinds(rootNode,node);
					}
				}
			}
			

		}

		
		public void DeleteQueryKind(string queryKindId)
		{
			string itemKindFile = DataPath + @"items\" + "ItemKinds.xml";

			XmlDocument doc = new XmlDocument();

			doc.Load(itemKindFile);

			XmlNode rootNode = doc.DocumentElement;

			foreach(XmlNode node in rootNode.ChildNodes)
			{
				if(node.Attributes["id"].Value == queryKindId)
				{
					DeleteChildQueryKinds(rootNode,node);  //先递归删除其子节点
					rootNode.RemoveChild(node);            //删除该节点
					OnDeleteEvent(new DeleteQueryKindEventArgs(queryKindId));
					break;
				}
			}

			doc.Save(itemKindFile);

		}
	


	}
}
