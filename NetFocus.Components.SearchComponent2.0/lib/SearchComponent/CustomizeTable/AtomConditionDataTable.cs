using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace NetFocus.Components.SearchComponent
{
	/// <summary>
	/// 一个继承自DataTable类的子类,用于存放当前设置的所有原子条件的信息
	/// </summary>
	[Serializable]
	public class AtomConditionDataTable : DataTable
	{
		public AtomConditionDataTable()
		{
			this.Columns.Add("FieldFullName",typeof(string));
			this.Columns.Add("FieldDataType",typeof(string));
			this.Columns.Add("InputControlType",typeof(string));
			this.Columns.Add("ConditionName",typeof(string));
			this.Columns.Add("InitialValue",typeof(string));
			this.Columns.Add("Operator",typeof(string));
			this.Columns.Add("ChineseName",typeof(string));
			this.PrimaryKey = new DataColumn[]{this.Columns[3]};
		}
		public AtomConditionDataTable(SerializationInfo info, StreamingContext context) : base(info,context)
		{
			
		}
	}
}
