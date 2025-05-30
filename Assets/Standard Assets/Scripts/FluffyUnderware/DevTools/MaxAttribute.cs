// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.MaxAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class MaxAttribute : DTPropertyAttribute
	{
		public MaxAttribute(float value, string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.MaxValue = value;
		}

		public MaxAttribute(string fieldOrProperty, string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.MaxFieldOrPropertyName = fieldOrProperty;
		}

		public float MaxValue;

		public string MaxFieldOrPropertyName;
	}
}
