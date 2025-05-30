// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.DTPropertyAttribute
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public class DTPropertyAttribute : PropertyAttribute
	{
		public DTPropertyAttribute(string label = "", string tooltip = "")
		{
			this.Label = label;
			this.Tooltip = tooltip;
		}

		public string Label;

		public string Tooltip;

		public string Color;

		public AttributeOptionsFlags Options;

		public int Precision = -1;
	}
}
