using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace NetFocus.Components.SearchComponent
{
	/// <summary>
	/// 一个继承自DataTable类的子类,用于存放当前设置的和表关系相关的一些关联字段信息
	/// </summary>
	[Serializable]
	public class RelationFieldDataTable : DataTable
	{
		public RelationFieldDataTable()
		{
			this.Columns.Add("Table1",typeof(string));
			this.Columns.Add("Table2",typeof(string));
			this.Columns.Add("AliasTable1",typeof(string));
			this.Columns.Add("AliasTable2",typeof(string));
			this.Columns.Add("Field1",typeof(string));
			this.Columns.Add("Field2",typeof(string));
		}

		public RelationFieldDataTable(SerializationInfo info, StreamingContext context) : base(info,context)
		{
			
		}
		
	}
}
