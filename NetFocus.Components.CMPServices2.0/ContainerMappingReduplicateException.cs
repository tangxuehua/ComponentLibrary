using System;

namespace NetFocus.Components.CMPServices
{

	public class ContainerMappingReduplicateException : Exception
	{
		public ContainerMappingReduplicateException(string containerMappingId) : base("已存在一个名为 " + containerMappingId + " 的持久性容器！")
		{
		
		}
	}
}
