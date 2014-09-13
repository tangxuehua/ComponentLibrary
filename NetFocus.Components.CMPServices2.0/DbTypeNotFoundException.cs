using System;

namespace NetFocus.Components.CMPServices
{
	public class DbTypeNotFoundException : Exception
	{
		public DbTypeNotFoundException(string typeName) : base("类型名： " + typeName + " 没有找到，请检查映射文件！")
		{
		
		}
	}
}
