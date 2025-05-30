// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.DTRegionAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class DTRegionAttribute : DTPropertyAttribute
	{
		public DTRegionAttribute() : base(string.Empty, string.Empty)
		{
		}

		public bool RegionIsOptional;

		public string RegionOptionsPropertyName;

		public bool UseSlider = true;
	}
}
