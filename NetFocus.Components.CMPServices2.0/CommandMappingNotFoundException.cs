using System;

namespace NetFocus.Components.CMPServices
{

	public class CommandMappingNotFoundException : Exception
	{
		public CommandMappingNotFoundException(string containerId, string commandName) : base("持久性容器 " + containerId + " 中命令： " + commandName + " 没有找到！")
		{
		
		}
	}

	public class CommandMappingWithoutContainerMappingIdNotFoundException : Exception
	{
		public CommandMappingWithoutContainerMappingIdNotFoundException(string commandName) : base("命令： " + commandName + " 没有找到！")
		{
		
		}
	}
}
