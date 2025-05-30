// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.Extensions.ColorExt
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools.Extensions
{
	public static class ColorExt
	{
		public static string ToHtml(this Color c)
		{
			Color32 color = c;
			return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
			{
				color.r,
				color.g,
				color.b,
				color.a
			});
		}
	}
}
