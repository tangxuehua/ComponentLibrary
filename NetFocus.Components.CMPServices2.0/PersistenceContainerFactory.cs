using System.Runtime.Remoting;
using System;
using System.Xml;
using System.Xml.Serialization; 
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace NetFocus.Components.CMPServices
{
	public class PersistenceContainerFactory
	{
		private static PersistenceContainerFactory persistenceContainerFactory = null;

		private PersistenceContainerFactory()
		{

		}

		public static PersistenceContainerFactory Instance
		{
			get
			{
				if(persistenceContainerFactory == null)
				{
					persistenceContainerFactory = new PersistenceContainerFactory();
				}
				return persistenceContainerFactory;
			}
		}

		
		public StdPersistenceContainer CreateContainer(string assemblyName, string persistenceContainerName)
		{
			ObjectHandle objectHandle = Activator.CreateInstance(assemblyName,persistenceContainerName);
			
			if(objectHandle != null)
			{
				return objectHandle.Unwrap() as StdPersistenceContainer;
			}

			return null;

//			return Assembly.Load(assemblyName).CreateInstance(persistenceContainerName) as StdPersistenceContainer;

			
			
		}
	}
}
