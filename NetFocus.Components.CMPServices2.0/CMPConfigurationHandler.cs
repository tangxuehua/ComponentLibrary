using System;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using System.Web;
using System.IO;
using System.Collections.Specialized;


namespace NetFocus.Components.CMPServices
{
	/// <summary>
	/// 提供对所有持久性容器的访问
	/// </summary>
	public class CMPConfigurationHandler
	{
		private static ContainerMappingSet containerMapSet;

		public static void CreateContainerMappings(StringCollection mappingFileCollection)
		{
			ContainerMappingSet cms = new ContainerMappingSet();
			XmlDocument doc = new XmlDocument();

			foreach (string fileName in mappingFileCollection)
			{
				doc.Load(fileName);
				XmlNodeList containerMappingNodeList = doc.SelectNodes("//ContainerMapping");
				ContainerMapping cm;
				foreach (XmlNode containerMappingNode in containerMappingNodeList)
				{
					cm = new ContainerMapping(containerMappingNode);
					cms[cm.ContainerMappingId] = cm;
				}
			}
			containerMapSet = cms;
			
			CMPProfile.DbTypeHints["VarChar"] = System.Data.SqlDbType.VarChar;
			CMPProfile.DbTypeHints["Int"] = System.Data.SqlDbType.Int;
			CMPProfile.DbTypeHints["DateTime"] = System.Data.SqlDbType.DateTime;
			CMPProfile.DbTypeHints["Text"] = System.Data.SqlDbType.Text;
			CMPProfile.DbTypeHints["Bit"] = System.Data.SqlDbType.Bit;
			CMPProfile.DbTypeHints["Money"] = System.Data.SqlDbType.Money;
			CMPProfile.DbTypeHints["Binary"] = System.Data.SqlDbType.Binary;
			CMPProfile.DbTypeHints["VarBinary"] = System.Data.SqlDbType.Image;
			CMPProfile.DbTypeHints["Float"] = System.Data.SqlDbType.Float;
			CMPProfile.DbTypeHints["Image"] = System.Data.SqlDbType.Image;
			CMPProfile.DbTypeHints["UniqueIdentifier"] = System.Data.SqlDbType.UniqueIdentifier;

		}
		
		public static void CreateContainerMappings(ArrayList objectList)
		{
			ContainerMappingSet cms = new ContainerMappingSet();

			foreach (XmlNode node in objectList)
			{
				ContainerMapping cm;

				cm = new ContainerMapping(node);
				cms[cm.ContainerMappingId] = cm;

			}
			containerMapSet = cms;
			
			CMPProfile.DbTypeHints["VarChar"] = System.Data.SqlDbType.VarChar;
			CMPProfile.DbTypeHints["Int"] = System.Data.SqlDbType.Int;
			CMPProfile.DbTypeHints["DateTime"] = System.Data.SqlDbType.DateTime;
			CMPProfile.DbTypeHints["Text"] = System.Data.SqlDbType.Text;
			CMPProfile.DbTypeHints["Bit"] = System.Data.SqlDbType.Bit;
			CMPProfile.DbTypeHints["Money"] = System.Data.SqlDbType.Money;
			CMPProfile.DbTypeHints["Binary"] = System.Data.SqlDbType.Binary;
			CMPProfile.DbTypeHints["VarBinary"] = System.Data.SqlDbType.Image;
			CMPProfile.DbTypeHints["Float"] = System.Data.SqlDbType.Float;
			CMPProfile.DbTypeHints["Image"] = System.Data.SqlDbType.Image;
			CMPProfile.DbTypeHints["UniqueIdentifier"] = System.Data.SqlDbType.UniqueIdentifier;

		}
		
		
		public static ContainerMappingSet ContainerMaps
		{
			get 
			{
				return containerMapSet;
			}
		}


	}
}
