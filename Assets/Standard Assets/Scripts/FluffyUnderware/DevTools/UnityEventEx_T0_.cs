// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.UnityEventEx<T0>
using System;
using System.Reflection;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine.Events;

namespace FluffyUnderware.DevTools
{
	public class UnityEventEx<T0> : UnityEvent<T0>
	{
		public void AddListenerOnce(UnityAction<T0> call)
		{
			base.RemoveListener(call);
			base.AddListener(call);
			this.CheckForListeners();
		}

		public bool HasListeners()
		{
			if (this.mCallsCount == null)
			{
				FieldInfo fieldInfo = typeof(UnityEventBase).FieldByName("m_Calls", false, true);
				if (fieldInfo != null)
				{
					this.mCallerList = fieldInfo.GetValue(this);
					if (this.mCallerList != null)
					{
						this.mCallsCount = this.mCallerList.GetType().PropertyByName("Count", false, false).GetGetMethod();
					}
				}
			}
			if (this.mCount == -1)
			{
				if (this.mCallerList != null && this.mCallsCount != null)
				{
					this.mCount = (int)this.mCallsCount.Invoke(this.mCallerList, null);
				}
				this.mCount += base.GetPersistentEventCount();
			}
			return this.mCount > 0;
		}

		public void CheckForListeners()
		{
			this.mCount = -1;
		}

		private object mCallerList;

		private MethodInfo mCallsCount;

		private int mCount = -1;
	}
}
