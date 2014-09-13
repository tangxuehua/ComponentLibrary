
using System;

namespace NetFocus.Components.CMPServices
{
	/// <summary>
	/// 所有持久性容器的基类
	/// </summary>
	public class StdPersistenceContainer
	{
		/// <summary>
		/// 通过一个存储过程的名称就可以进行数据访问，这种方式的前提是所有的存储过程的名称都不重复，该方法主要用于执行该操作不需要任何输入参数，也不需要任何返回信息。
		/// </summary>
		/// <param name="commandName">存储过程的名称</param>
		public virtual void Execute(string commandName)
		{

		}

		/// <summary>
		/// 通过一个存储过程的名称和一个持久性对象进行数据访问，这种方式的前提是所有的存储过程的名称都不重复。
		/// </summary>
		/// <param name="commandName">存储过程的名称</param>
		/// <param name="currentObject">一个持久性对象，不能为空</param>
		public virtual void Execute(string commandName, PersistableObject currentObject)
		{

		}

		/// <summary>
		/// 提供一个持久性容器名称和该容器中的一个存储过程的名称进行数据访问，该方法主要用于执行该操作不需要任何输入参数，也不需要任何返回信息。
		/// </summary>
		/// <param name="containerMappingId">持久性容器的ID</param>
		/// <param name="commandName">存储过程的名称</param>
		public virtual void Execute(string containerMappingId, string commandName)
		{

		}

		/// <summary>
		/// 提供一个持久性容器名称和该容器中的一个存储过程以及一个持久性对象进行数据访问，三个参数都不能为空。
		/// </summary>
		/// <param name="containerMappingId">持久性容器ID</param>
		/// <param name="commandName">存储过程的名称</param>
		/// <param name="currentObject">一个持久性对象</param>
		public virtual void Execute(string containerMappingId, string commandName, PersistableObject currentObject)
		{

		}

	}
}
