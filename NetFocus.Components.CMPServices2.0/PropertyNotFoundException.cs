using System;

namespace NetFocus.Components.CMPServices
{
	public class PropertyNotFoundException : Exception
	{
		public PropertyNotFoundException(string propertyName) : base("映射文件中定义的属性成员 " + propertyName + " 在持久性类中不存在！") 
		{
			
		}
	}
}
