using System;
using System.Xml;
using System.Xml.Serialization; 
using System.Reflection;
using System.Collections;

namespace NetFocus.Components.CMPServices
{
	/// <summary>
	/// 该类用于存储所有的容器映射类的对象
	/// </summary>
	public class ContainerMappingSet
	{
		private Hashtable containerMappings;

		public ContainerMappingSet()
		{
			containerMappings = new Hashtable();
		}

		/// <summary>
		/// 提供对容器对象的键值对访问
		/// </summary>
		public ContainerMapping this[string key]
		{
			get 
			{
				if(containerMappings.ContainsKey(key))
				{
					return containerMappings[key] as ContainerMapping;
				}
				else
				{
					return null;
				}
			}
			set 
			{
				if (containerMappings.ContainsKey(key))
				{
					throw new ContainerMappingReduplicateException(key);
				}
				containerMappings.Add(key, value);
			}
		}

		/// <summary>
		/// 提供对容器对象的索引访问
		/// </summary>
		public ContainerMapping this[int index]
		{
			get 
			{
				IDictionaryEnumerator enumerator = containerMappings.GetEnumerator();
				int i = 0;
				while(enumerator.MoveNext())
				{
					if(i == index)
					{
						return enumerator.Value as ContainerMapping;
					}
					i++;
				}
				return null;
			}

		}

		
		/// <summary>
		/// 返回容器类的个数
		/// </summary>
		public int Count
		{
			get 
			{
				return containerMappings.Count;
			}
		}

	}
}

