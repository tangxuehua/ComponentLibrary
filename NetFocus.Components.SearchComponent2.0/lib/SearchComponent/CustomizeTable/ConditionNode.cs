using System;

namespace NetFocus.Components.SearchComponent
{
	/// <summary>
	/// 代表一个条件节点，该条件有可能是一个原子条件，也可能是一个组合条件
	/// </summary>
	public class ConditionNode
	{
		string conditionName = string.Empty;
		ConditionNode childConditionNode1 = null;
		ConditionNode childConditionNode2 = null;
		AtomCondition condition = null;
		string relation = string.Empty;

		public ConditionNode()
		{
		
		}
		public ConditionNode(string conditionName, ConditionNode childConditionNode1, ConditionNode childConditionNode2, string relation)
		{
			this.conditionName = conditionName;
			this.childConditionNode1 = childConditionNode1;
			this.childConditionNode2 = childConditionNode2;
			this.relation = relation;
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
		public ConditionNode ChildConditionNode1
		{
			get
			{
				return childConditionNode1;
			}
			set
			{
				childConditionNode1 = value;
			}
		}

		public ConditionNode ChildConditionNode2
		{
			get
			{
				return childConditionNode2;
			}
			set
			{
				childConditionNode2 = value;
			}
		}

		public string Relation
		{
			get
			{
				return relation;
			}
			set
			{
				relation = value;
			}
		}

		public AtomCondition Condition
		{
			get
			{
				return condition;
			}
			set
			{
				condition = value;
			}
		}

	}
}
