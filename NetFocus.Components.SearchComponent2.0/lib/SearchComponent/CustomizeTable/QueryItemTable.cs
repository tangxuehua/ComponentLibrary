using System;
using System.Data;

namespace NetFocus.Components.SearchComponent
{
	public class QueryItemTable : DataTable
	{
		public QueryItemTable()
		{
			this.Columns.Add("id",typeof(string));
			this.Columns.Add("name",typeof(string));
			this.Columns.Add("description",typeof(string));
			this.Columns.Add("connectionString",typeof(string));
			this.Columns.Add("sqlString",typeof(string));
			this.Columns.Add("kindId",typeof(string));
		}

	}
}
