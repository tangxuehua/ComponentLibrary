using System;
using System.Data;

namespace NetFocus.Components.SearchComponent
{
    public class QueryKindTable : DataTable
    {
		public QueryKindTable()
		{
			this.Columns.Add("id",typeof(string));
			this.Columns.Add("name",typeof(string));
			this.Columns.Add("description",typeof(string));
			this.Columns.Add("createDate",typeof(string));
			this.Columns.Add("parentId",typeof(string));
		}
    }
}
