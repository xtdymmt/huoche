// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.MinAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class MinAttribute : DTPropertyAttribute
	{
		public MinAttribute(float value, string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.MinValue = value;
		}

		public MinAttribute(string fieldOrProperty, string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.MinFieldOrPropertyName = fieldOrProperty;
		}

		public float MinValue;

		public string MinFieldOrPropertyName;
	}
}
