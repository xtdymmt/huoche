// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvySplineGizmos
using System;

namespace FluffyUnderware.Curvy
{
	[Flags]
	public enum CurvySplineGizmos
	{
		None = 0,
		Curve = 2,
		Approximation = 4,
		Tangents = 8,
		Orientation = 16,
		Labels = 32,
		Metadata = 64,
		Bounds = 128,
		All = 65535
	}
}
