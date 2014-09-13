
using System;
using System.Xml.Serialization;
using System.Data;
using System.Reflection;

namespace NetFocus.Components.CMPServices
{
	/// <summary>
	/// 对一个存储过程参数的映射类
	/// </summary>
	public class CommandParameter
	{
		private string classMember;                           //持久性对象类的属性名
		private string parameterName;                         //参数名称
		private string dbTypeHint;                            //参数类型
		private ParameterDirection parameterDirection;        //参数方向

		private void ConstructCommandParameter(string classMember, string initParameterName,string initDbTypeHint,string initParameterDirection)
		{
			this.classMember = classMember;
			this.parameterName = initParameterName;
			this.dbTypeHint = initDbTypeHint;
			this.ParamDirection = initParameterDirection;
		}

		
		public CommandParameter()//一个构造方法，不带任何参数
		{
			ConstructCommandParameter("Not Set", "Not Set", "Not Set", "ReturnValue");
		}

		
		public CommandParameter(string classMember, string initParameterName,string initDbTypeHint,string initParameterDirection)
		{
			ConstructCommandParameter(classMember,initParameterName,initDbTypeHint,initParameterDirection);
		}

		/// <summary>
		/// 持久性对象类的属性名
		/// </summary>
		public string ClassMember
		{
			get 
			{
				return classMember;
			}
			set 
			{
				classMember = value;
			}
		}
		
		/// <summary>
		/// 参数名称
		/// </summary>
		public string ParameterName
		{
			get 
			{
				return parameterName;
			}
			set 
			{
				parameterName = value;
			}
		}

		/// <summary>
		/// 参数类型
		/// </summary>
		public string DbTypeHint
		{
			get 
			{
				return dbTypeHint;
			}
			set 
			{
				dbTypeHint = value;
			}
		}
		
		/// <summary>
		/// 参数方向,字符串表示
		/// </summary>
		public string ParamDirection
		{
			get 
			{
				return parameterDirection.ToString();
			}
			set 
			{
				if (value == "Input")
					parameterDirection = ParameterDirection.Input;
				else if (value == "InputOutput")
					parameterDirection = ParameterDirection.InputOutput;
				else if (value == "Output" )
					parameterDirection = ParameterDirection.Output;
				else
					parameterDirection = ParameterDirection.ReturnValue;
			}
		}

		/// <summary>
		/// 参数实际方向
		/// </summary>
		public ParameterDirection RealParameterDirection
		{
			get 
			{
				return parameterDirection;
			}
		}


	}
}

