// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.ActionAttribute
using System;
using System.Reflection;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public class ActionAttribute : DTAttribute
	{
		protected ActionAttribute(string actionData, ActionAttribute.ActionEnum action = ActionAttribute.ActionEnum.Callback) : base(50, false)
		{
			this.ActionData = actionData;
			this.Action = action;
		}

		public void Callback(object classInstance)
		{
			string text = this.ActionData as string;
			if (!string.IsNullOrEmpty(text))
			{
				if (this.mCallback == null)
				{
					this.mCallback = classInstance.GetType().MethodByName(text, true, true);
				}
				if (this.mCallback != null)
				{
					this.mCallback.Invoke(classInstance, null);
				}
				else
				{
					UnityEngine.Debug.LogWarningFormat("[DevTools] Unable to find method '{0}' at class '{1}' !", new object[]
					{
						text,
						classInstance.GetType().Name
					});
				}
			}
		}

		public ActionAttribute.ActionEnum Action = ActionAttribute.ActionEnum.Callback;

		public ActionAttribute.ActionPositionEnum Position = ActionAttribute.ActionPositionEnum.Below;

		public object ActionData;

		private MethodInfo mCallback;

		public enum ActionEnum
		{
			Show,
			Hide,
			Enable,
			Disable,
			ShowInfo,
			ShowWarning,
			ShowError,
			Callback
		}

		public enum ActionPositionEnum
		{
			Above,
			Below
		}
	}
}
