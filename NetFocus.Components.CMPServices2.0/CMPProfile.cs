
using System;
using System.Collections;

namespace NetFocus.Components.CMPServices
{
	public class CMPProfile
	{
		private static Hashtable dbTypeHints;
		private static string defaultDataSource;
		
		static CMPProfile()
		{
			dbTypeHints = new Hashtable();
		}

		public static Hashtable DbTypeHints
		{
			get 
			{
				return dbTypeHints;
			}
			set 
			{
				dbTypeHints = value;
			}
		}

		public static string DefaultDataSource
		{
			get 
			{
				return defaultDataSource;
			}
			set 
			{
				defaultDataSource = value;
			}
		}
	}
}
