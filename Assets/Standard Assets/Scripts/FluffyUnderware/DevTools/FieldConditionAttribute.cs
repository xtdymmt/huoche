// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.FieldConditionAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class FieldConditionAttribute : ConditionalAttribute, IDTFieldRenderAttribute
	{
		public FieldConditionAttribute(string fieldOrProperty, object compareTo, bool compareFalse = false, ActionAttribute.ActionEnum action = ActionAttribute.ActionEnum.Show, object actionData = null, ActionAttribute.ActionPositionEnum position = ActionAttribute.ActionPositionEnum.Below) : base(fieldOrProperty, compareTo, compareFalse)
		{
			this.Action = action;
			this.ActionData = actionData;
			this.Position = position;
		}

		public FieldConditionAttribute(string fieldOrProperty, object compareTo, bool compareFalse, ConditionalAttribute.OperatorEnum op, string fieldOrProperty2, object compareTo2, bool compareFalse2) : base(fieldOrProperty, compareTo, compareFalse, op, fieldOrProperty2, compareTo2, compareFalse2)
		{
		}

		public FieldConditionAttribute(string fieldOrProperty, object compareTo, bool compareFalse, ConditionalAttribute.OperatorEnum op, string fieldOrProperty2, object compareTo2, bool compareFalse2, string fieldOrProperty3, object compareTo3, bool compareFalse3) : base(fieldOrProperty, compareTo, compareFalse, op, fieldOrProperty2, compareTo2, compareFalse2, fieldOrProperty3, compareTo3, compareFalse3)
		{
		}

		public FieldConditionAttribute(string methodToQuery) : base(methodToQuery)
		{
		}
	}
}
