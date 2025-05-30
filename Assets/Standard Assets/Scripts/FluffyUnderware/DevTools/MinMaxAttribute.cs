// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.MinMaxAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class MinMaxAttribute : DTPropertyAttribute
	{
		public MinMaxAttribute(string maxValueField, string label = "", string tooltip = "") : base(label, tooltip)
		{
			this.MaxValueField = maxValueField;
			this.Min = 0f;
			this.Max = 1f;
		}

		public readonly string MaxValueField;

		public float Min;

		public string MinBoundFieldOrPropertyName;

		public float Max;

		public string MaxBoundFieldOrPropertyName;
	}
}
