// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.VectorExAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class VectorExAttribute : DTPropertyAttribute
	{
		public VectorExAttribute(string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.Options = AttributeOptionsFlags.Full;
		}
	}
}
