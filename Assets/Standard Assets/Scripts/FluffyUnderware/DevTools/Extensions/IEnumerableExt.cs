// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.Extensions.IEnumerableExt
using System;
using System.Collections.Generic;

namespace FluffyUnderware.DevTools.Extensions
{
	public static class IEnumerableExt
	{
		public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
		{
			foreach (T obj in ie)
			{
				action(obj);
			}
		}
	}
}
