using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace NetFocus.Components.SearchComponent
{
	/// <summary>
	/// 一个继承自DataTable类的子类,用于存放当前选择的所有的表的信息
	/// </summary>
	[Serializable]
	public class SelectedTableDataTable : DataTable
	{
		public SelectedTableDataTable()
		{
			this.Columns.Add("TableName",typeof(string));
			this.Columns.Add("AliasName",typeof(string));
			this.Columns.Add("ChineseName",typeof(string));
			this.PrimaryKey = new DataColumn[]{this.Columns[0]};
		}

		public SelectedTableDataTable(SerializationInfo info, StreamingContext context) : base(info,context)
		{
			
		}
		
	}
}
