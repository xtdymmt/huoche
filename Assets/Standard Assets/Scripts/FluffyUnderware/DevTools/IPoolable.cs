// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.IPoolable
using System;

namespace FluffyUnderware.DevTools
{
	public interface IPoolable
	{
		void OnBeforePush();

		void OnAfterPop();
	}
}
