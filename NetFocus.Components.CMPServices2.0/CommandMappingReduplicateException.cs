using System;

namespace NetFocus.Components.CMPServices
{

	public class CommandMappingReduplicateException : Exception
	{
		public CommandMappingReduplicateException(string containerMappingId, string commandName) : base("在持久性容器： " + containerMappingId + " 中已经存在一个名为 " + commandName + " 的命令！")
		{
		
		}
	}
}
