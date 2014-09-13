using System;

namespace NetFocus.Components.CMPServices
{
	public class PersistableObjectNullException : Exception
	{
		public PersistableObjectNullException() : base("持久性对象为空！")
		{

		}
	}
}
