using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace NetFocus.Components.SearchComponent
{
	/// <summary>
	/// 一个继承自DataTable类的子类,用于存放当前设置的所有组合条件的信息
	/// </summary>
	[Serializable]
	public class ComplicatedConditionDataTable : DataTable
	{
		public ComplicatedConditionDataTable()
		{
			this.Columns.Add("Condition1",typeof(string));
			this.Columns.Add("Condition2",typeof(string));
			this.Columns.Add("ConditionName",typeof(string));
			this.Columns.Add("Relation",typeof(string));
			this.PrimaryKey = new DataColumn[]{this.Columns[2]};
		}
		public ComplicatedConditionDataTable(SerializationInfo info, StreamingContext context) : base(info,context)
		{
			
		}
	}
}
