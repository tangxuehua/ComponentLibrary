using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace NetFocus.Components.SearchComponent
{
	[Serializable]
	public class TableRelation
	{
		string table1 = string.Empty;
		string table2 = string.Empty;
		string joinType = string.Empty;
		RelationFieldDataTable relationFields = new RelationFieldDataTable();

		public string Table1
		{
			get
			{
				return table1;
			}
			set
			{
				table1 = value;
			}
		}
		public string Table2
		{
			get
			{
				return table2;
			}
			set
			{
				table2 = value;
			}
		}

		public string JoinType
		{
			get
			{
				return joinType;
			}
			set
			{
				joinType = value;
			}
		}

		public RelationFieldDataTable RelationFields
		{
			get
			{
				return relationFields;
			}
			set
			{
				relationFields = value;
			}
		}


	}
}
