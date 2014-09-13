
using System;
using System.Xml;
using System.Xml.Serialization; 
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace NetFocus.Components.CMPServices
{
	/// <summary>
	/// 持久性容器的Sql Server实现
	/// </summary>
	public class SqlPersistenceContainer : StdPersistenceContainer  
	{
		private void AssignReturnValue(SqlCommand currentCmd,PersistableObject persistObject)
		{
			foreach(SqlParameter parameter in currentCmd.Parameters)
			{
				if(parameter.Direction == ParameterDirection.ReturnValue)
				{
					persistObject.ReturnValue = parameter.Value;
				}
			}
		}
		public override void Execute(string containerMappingId, string commandName,PersistableObject currentObject)
		{
			if(currentObject == null)
			{
				throw new PersistableObjectNullException();
			}
			
			ContainerMapping containerMapping = CMPConfigurationHandler.ContainerMaps[containerMappingId];
			if(containerMapping == null)
			{
				throw new ContainerMappingNotFoundException(containerMappingId);
			}
			
			CommandMapping commandMapping = containerMapping.CommandMappingList[commandName] as CommandMapping;
			if(commandMapping == null)
			{
				throw new CommandMappingNotFoundException(containerMappingId,commandName);
			}
			
			SqlCommand currentCommand = BuildCommandFromMapping(commandMapping);
			AssignValuesToParameters(commandMapping, ref currentCommand, currentObject);
			currentCommand.Connection.Open();

			AssignReturnValueToDataSet(currentCommand, ref currentObject);

			AssignOutputValuesToInstance(commandMapping, currentCommand, ref currentObject);

			AssignReturnValue(currentCommand,currentObject);

			currentCommand.Connection.Close();
			currentCommand.Connection.Dispose();
			currentCommand.Dispose();

		}

		
		public override void Execute(string containerMappingId, string commandName)
		{

			ContainerMapping containerMapping = CMPConfigurationHandler.ContainerMaps[containerMappingId];
			if(containerMapping == null)
			{
				throw new ContainerMappingNotFoundException(containerMappingId);
			}
			
			CommandMapping commandMapping = containerMapping.CommandMappingList[commandName] as CommandMapping;
			if(commandMapping == null)
			{
				throw new CommandMappingNotFoundException(containerMappingId,commandName);
			}
			
			SqlCommand currentCommand = BuildCommandFromMapping(commandMapping);

			currentCommand.Connection.Open();

			currentCommand.ExecuteNonQuery();

			currentCommand.Connection.Close();
			currentCommand.Connection.Dispose();
			currentCommand.Dispose();

		}


		public override void Execute(string commandName,PersistableObject currentObject)
		{
			if(currentObject == null)
			{
				throw new PersistableObjectNullException();
			}
			CommandMapping commandMapping = null;

			for(int i = 0; i < CMPConfigurationHandler.ContainerMaps.Count; i++)
			{
				commandMapping = CMPConfigurationHandler.ContainerMaps[i].CommandMappingList[commandName] as CommandMapping;
				
				if(commandMapping != null)
				{
					break;
				}
			}
			
			if(commandMapping == null)
			{
				throw new CommandMappingWithoutContainerMappingIdNotFoundException(commandName);
			}
			
			SqlCommand currentCommand = BuildCommandFromMapping(commandMapping);
			AssignValuesToParameters(commandMapping, ref currentCommand, currentObject);
			currentCommand.Connection.Open();

			AssignReturnValueToDataSet(currentCommand, ref currentObject);

			AssignOutputValuesToInstance( commandMapping, currentCommand, ref currentObject );

			AssignReturnValue(currentCommand,currentObject);

			currentCommand.Connection.Close();
			currentCommand.Connection.Dispose();
			currentCommand.Dispose();

		}

		
		public override void Execute(string commandName)
		{
			CommandMapping commandMapping = null;

			for(int i = 0; i < CMPConfigurationHandler.ContainerMaps.Count; i++)
			{
				commandMapping = CMPConfigurationHandler.ContainerMaps[i].CommandMappingList[commandName] as CommandMapping;
				
				if(commandMapping != null)
				{
					break;
				}
			}
			
			if(commandMapping == null)
			{
				throw new CommandMappingWithoutContainerMappingIdNotFoundException(commandName);
			}
			
			SqlCommand currentCommand = BuildCommandFromMapping(commandMapping);

			currentCommand.Connection.Open();

			currentCommand.ExecuteNonQuery();

			currentCommand.Connection.Close();
			currentCommand.Connection.Dispose();
			currentCommand.Dispose();

		}

	
		private void AssignReturnValueToDataSet(SqlCommand currentCmd,ref PersistableObject persistObject )
		{
			SqlDataAdapter sqlDa = new SqlDataAdapter(currentCmd);

			sqlDa.Fill(persistObject.ResultSet);

		}

		
		private void AssignOutputValuesToInstance( CommandMapping cmdMap,SqlCommand currentCmd,ref PersistableObject persistObject )
		{
			SqlParameter curParam;
			ParameterDirection parameterDirection;

			foreach (CommandParameter cmdParameter in cmdMap.Parameters)
			{
				parameterDirection = cmdParameter.RealParameterDirection;

				if (parameterDirection == ParameterDirection.Output)
				{
					curParam = currentCmd.Parameters[cmdParameter.ParameterName];

					if (curParam.Value != DBNull.Value)
					{
						PropertyInfo propertyInfo = persistObject.GetType().GetProperty(cmdParameter.ClassMember);
						if(propertyInfo == null)
						{
							throw new PropertyNotFoundException(cmdParameter.ClassMember);
						}
						propertyInfo.SetValue(persistObject, curParam.Value, null);
					} 

				}
			}

		}

		
		private void AssignValuesToParameters( CommandMapping cmdMap,ref SqlCommand currentCmd,PersistableObject persistObject )
		{
			ParameterDirection parameterDirection;

			foreach (CommandParameter cmdParameter in cmdMap.Parameters)
			{
				parameterDirection = cmdParameter.RealParameterDirection;

				if (parameterDirection == ParameterDirection.Input)
				{
					PropertyInfo propertyInfo = persistObject.GetType().GetProperty(cmdParameter.ClassMember);
					if(propertyInfo == null)
					{
						throw new PropertyNotFoundException(cmdParameter.ClassMember);
					}
					object propertyValue =  propertyInfo.GetValue(persistObject,null);
					currentCmd.Parameters[cmdParameter.ParameterName].Value = propertyValue;
				}
			}
		}

		
		private SqlCommand BuildCommandFromMapping( CommandMapping cmdMap )
		{
			SqlConnection conn = new SqlConnection(CMPProfile.DefaultDataSource);
			
			SqlCommand sqlCommand = conn.CreateCommand();
			sqlCommand.CommandText = cmdMap.CommandName;
			sqlCommand.CommandType = CommandType.StoredProcedure;
			foreach (CommandParameter cmdParameter in cmdMap.Parameters)
			{
				SqlParameter newParam = new SqlParameter();
				newParam.ParameterName = cmdParameter.ParameterName;
				newParam.Direction = cmdParameter.RealParameterDirection;
				object dbType = CMPProfile.DbTypeHints[cmdParameter.DbTypeHint];
				if(dbType == null)
				{
					throw new DbTypeNotFoundException(cmdParameter.DbTypeHint);
				}
				newParam.SqlDbType = (SqlDbType)dbType;

				sqlCommand.Parameters.Add(newParam);
			}

			//新增一个返回值参数
			//
			SqlParameter returnParam = new SqlParameter();
			returnParam.Direction = ParameterDirection.ReturnValue;

			sqlCommand.Parameters.Add(returnParam);

			return sqlCommand;
		}


	}
}
