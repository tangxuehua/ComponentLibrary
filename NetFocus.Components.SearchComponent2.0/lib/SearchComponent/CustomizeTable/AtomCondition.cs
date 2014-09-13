using System;

namespace NetFocus.Components.SearchComponent
{

	public class AtomCondition
	{
		string fieldFullName = string.Empty;
		string fieldDataType = string.Empty;
		string inputControlType = string.Empty;
		string conditionName = string.Empty;
		string initialValue = string.Empty;
		string operator1 = string.Empty;
		string chineseName = string.Empty;

		public string FieldFullName
		{
			get
			{
				return fieldFullName;
			}
			set
			{
				fieldFullName = value;
			}
		}
		public string FieldDataType
		{
			get
			{
				return fieldDataType;
			}
			set
			{
				fieldDataType = value;
			}
		}
		public string InputControlType
		{
			get
			{
				return inputControlType;
			}
			set
			{
				inputControlType = value;
			}
		}
		public string ConditionName
		{
			get
			{
				return conditionName;
			}
			set
			{
				conditionName = value;
			}
		}
		public string InitialValue
		{
			get
			{
				return initialValue;
			}
			set
			{
				initialValue = value;
			}
		}
		public string Operator
		{
			get
			{
				return operator1;
			}
			set
			{
				operator1 = value;
			}
		}
		public string ChineseName
		{
			get
			{
				return chineseName;
			}
			set
			{
				chineseName = value;
			}
		}
	}
}
