// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.Extensions.ObjectExt
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools.Extensions
{
	public static class ObjectExt
	{
		public static void Destroy(this UnityEngine.Object c)
		{
			UnityEngine.Object.Destroy(c);
		}

		public static string ToDumpString(this object o)
		{
			return new DTObjectDump(o, 0).ToString();
		}
	}
}
