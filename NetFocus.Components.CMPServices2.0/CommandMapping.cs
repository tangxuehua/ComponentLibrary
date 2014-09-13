
using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;


namespace NetFocus.Components.CMPServices
{
	/// <summary>
	/// 对一个存储过程的映射类
	/// </summary>
	public class CommandMapping
	{
		private string commandName;                  //存储过程的名称
		private ArrayList commandParameters;         //存储过程的参数集合
		private string returnValueType;              //存储过程的返回值类型

		private void ConstructCommandMapping( string initCommandName, ArrayList initCommandParameters )
		{
			commandName = initCommandName;
			commandParameters = initCommandParameters;
		}


		public CommandMapping()
		{
			ConstructCommandMapping("Not Set", new ArrayList());
		}

		
		public CommandMapping( string initCommandName )
		{
			ConstructCommandMapping(initCommandName, new ArrayList());
		}

		
		public CommandMapping( string initCommandName, ArrayList initCommandParameters )
		{
			ConstructCommandMapping( initCommandName, initCommandParameters );
		}

		
		/// <summary>
		/// 存储过程的名称
		/// </summary>
		public string CommandName
		{
			get 
			{
				return commandName;
			}
			set 
			{
				commandName = value;
			}
		}

		
		/// <summary>
		/// 存储过程的参数集合
		/// </summary>
		public ArrayList Parameters
		{
			get 
			{
				return commandParameters;
			}
			set 
			{
				commandParameters = value;
			}
		}
		
		
		/// <summary>
		/// 存储过程的返回值类型
		/// </summary>
		public string ReturnValueType
		{
			get
			{
				return returnValueType;
			}
			set
			{
				returnValueType = value;
			}
		}

		
		/// <summary>
		/// 添加一个存储过程参数
		/// </summary>
		/// <param name="newParameter"></param>
		public void AddParameter( CommandParameter newParameter )
		{
			commandParameters.Add( newParameter );
		}

	}
}
