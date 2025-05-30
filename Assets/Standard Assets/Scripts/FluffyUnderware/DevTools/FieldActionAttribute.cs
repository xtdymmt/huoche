// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.FieldActionAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class FieldActionAttribute : ActionAttribute, IDTFieldRenderAttribute
	{
		public FieldActionAttribute(string actionData, ActionAttribute.ActionEnum action = ActionAttribute.ActionEnum.Callback) : base(actionData, action)
		{
		}
	}
}
