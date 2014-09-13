
using System;
using System.Reflection;

namespace NetFocus.Components.AddIns.Attributes
{
	/// <summary>
	/// Indicates that class represents a codon.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class CodonTypeAttribute : Attribute
	{
		string codonType;
		
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		public CodonTypeAttribute(string codonType) 
		{
			this.codonType = codonType;
		}
		
		/// <summary>
		/// Returns the name of the codon.
		/// </summary>
		public string CodonType {
			get { 
				return codonType; 
			}
			set { 
				codonType = value; 
			}
		}
	}
}
