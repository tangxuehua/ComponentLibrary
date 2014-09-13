using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
namespace NetFocus.Components.SearchComponent
{
	/// <summary>
	/// 一个继承自DataTable类的子类,用于存放当前选择的一些排序字段的信息
	/// </summary>
	[Serializable]
	public class SelectedSortFieldDataTable : DataTable
	{
		public SelectedSortFieldDataTable()
		{
			this.Columns.Add("FieldFullName",typeof(string));
			this.Columns.Add("SortType",typeof(string));
			this.PrimaryKey = new DataColumn[]{this.Columns[0]};
		}
		public SelectedSortFieldDataTable(SerializationInfo info, StreamingContext context) : base(info,context)
		{
			
		}
	}
}
