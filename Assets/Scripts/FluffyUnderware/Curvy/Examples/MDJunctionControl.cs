// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.MDJunctionControl
using System;

namespace FluffyUnderware.Curvy.Examples
{
	public class MDJunctionControl : CurvyMetadataBase, ICurvyMetadata
	{
		public void Toggle()
		{
			this.UseJunction = !this.UseJunction;
		}

		public bool UseJunction;
	}
}
