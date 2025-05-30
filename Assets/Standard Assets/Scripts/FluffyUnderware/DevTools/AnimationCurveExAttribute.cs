// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.AnimationCurveExAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class AnimationCurveExAttribute : DTPropertyAttribute
	{
		public AnimationCurveExAttribute(string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.Options = AttributeOptionsFlags.Clipboard;
		}
	}
}
