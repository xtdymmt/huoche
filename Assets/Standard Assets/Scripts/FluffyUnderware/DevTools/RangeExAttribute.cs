// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.RangeExAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class RangeExAttribute : DTPropertyAttribute
	{
		public RangeExAttribute(float minValue, float maxValue, string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
		}

		public RangeExAttribute(string minFieldOrProperty, float maxValue, string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.MinFieldOrPropertyName = minFieldOrProperty;
			this.MaxValue = maxValue;
		}

		public RangeExAttribute(float minValue, string maxFieldOrProperty, string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.MinValue = minValue;
			this.MaxFieldOrPropertyName = maxFieldOrProperty;
		}

		public RangeExAttribute(string minFieldOrProperty, string maxFieldOrProperty, string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.MinFieldOrPropertyName = minFieldOrProperty;
			this.MaxFieldOrPropertyName = maxFieldOrProperty;
		}

		public float MinValue;

		public string MinFieldOrPropertyName;

		public float MaxValue;

		public string MaxFieldOrPropertyName;

		public bool Slider = true;
	}
}
