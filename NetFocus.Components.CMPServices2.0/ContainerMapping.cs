using System;
using System.Xml;
using System.Xml.Serialization; 
using System.Reflection;
using System.Collections;


namespace NetFocus.Components.CMPServices
{
	/// <summary>
	/// 描述存储过程集合的一个容器映射类
	/// </summary>
	public class ContainerMapping  //这个类代表一个容器的映射
	{
		private string containerMappingId;
		private Hashtable commandMappingList = new Hashtable();
		private string currentCommandName;

		/// <summary>
		/// 通过一个ContainerMapping节点来创建一个容器类对象
		/// </summary>
		/// <param name="xmlNode"></param>
		public ContainerMapping(XmlNode xmlNode)
		{
			containerMappingId = xmlNode.Attributes.GetNamedItem("Id").Value;

			XmlNodeList commandMappingNodeList = xmlNode.SelectNodes("Command");
			
			foreach(XmlNode commandMappingNode in commandMappingNodeList)
			{
				string commandName = commandMappingNode.Attributes.GetNamedItem("CommandName").Value;
				if(commandMappingList.Contains(commandName))
				{
					throw new CommandMappingReduplicateException(containerMappingId,commandName);
				}
				commandMappingList[commandName] = CreateCommandMappingFromNode(commandName, commandMappingNode );
				
			}
			
		}

		
		private static CommandMapping CreateCommandMappingFromNode(string commandName, XmlNode cmdNode)
		{
			CommandMapping newCmdMap = new CommandMapping(commandName);

			CommandParameter newParam;
			XmlNodeList parameterList = cmdNode.SelectNodes("Parameter");

			foreach (XmlNode cmdParamNode in parameterList)
			{
				newParam = new CommandParameter();
				newParam.ClassMember = cmdParamNode.Attributes.GetNamedItem("ClassMember").Value;
				newParam.ParameterName = cmdParamNode.Attributes.GetNamedItem("ParameterName").Value;
				newParam.DbTypeHint = cmdParamNode.Attributes.GetNamedItem("DbTypeHint").Value;
				newParam.ParamDirection = cmdParamNode.Attributes.GetNamedItem("ParamDirection").Value;
				
				newCmdMap.AddParameter(newParam);
			}

			return newCmdMap;
		}

		
		/// <summary>
		/// 容器类ID
		/// </summary>
		public string ContainerMappingId
		{
			get 
			{
				return containerMappingId;
			}
			set 
			{
				containerMappingId = value;
			}
		}


		/// <summary>
		/// 存储过程集合
		/// </summary>
		public Hashtable CommandMappingList
		{
			get
			{
				return commandMappingList;
			}
			set
			{
				commandMappingList = value;
			}
		}
		
		
		/// <summary>
		/// 当前存储过程名字
		/// </summary>
		public string CurrentCommandName
		{
			get
			{
				return currentCommandName;
			}
			set
			{
				currentCommandName = value;
			}
		}


	}
}
