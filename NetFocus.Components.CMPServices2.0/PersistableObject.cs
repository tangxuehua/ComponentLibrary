
using System;
using System.Data;
using System.Xml;
using System.Xml.Serialization; 
using System.Text;

namespace NetFocus.Components.CMPServices
{
	/// <summary>
	/// 所有持久性对象的基类
	/// </summary>
	public class PersistableObject
	{
		private DataSet internalData = new DataSet();
		private object returnValue = 0;
		
		/// <summary>
		/// 表示对象是否能持久化
		/// </summary>
		/// <returns></returns>
		public bool CanPersist()
		{
			return true;
		}

		
		public string ToXmlString()  //序列化该对象
		{
			try 
			{
				Type objType = this.GetType();
				StringBuilder sb = new StringBuilder();
				System.IO.StringWriter sw = new System.IO.StringWriter( sb );
				XmlSerializer xs = new XmlSerializer( objType );
				xs.Serialize( sw, this );
				return sw.GetStringBuilder().ToString();
			}
			catch 
			{
				return "Serialization Failure";
			}
		}
		
		
		/// <summary>
		/// 表示由该持久性对象所组成的一个数据集
		/// </summary>
		public DataSet ResultSet
		{
			get 
			{
				return internalData;
			}
			set 
			{
				internalData = value;
			}
		}

		/// <summary>
		/// 用于表示存储过程的返回值
		/// </summary>
		public object ReturnValue
		{
			get 
			{
				return returnValue;
			}
			set 
			{
				returnValue = value;
			}
		}

	}
}
