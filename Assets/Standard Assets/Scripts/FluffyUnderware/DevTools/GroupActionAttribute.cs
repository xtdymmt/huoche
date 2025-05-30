// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.GroupActionAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class GroupActionAttribute : ActionAttribute, IDTGroupRenderAttribute
	{
		public GroupActionAttribute(string actionData, ActionAttribute.ActionEnum action = ActionAttribute.ActionEnum.Callback) : base(actionData, action)
		{
		}
	}
}
