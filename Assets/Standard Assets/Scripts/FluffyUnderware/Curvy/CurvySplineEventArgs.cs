// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvySplineEventArgs
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	public class CurvySplineEventArgs : CurvyEventArgs
	{
		public CurvySplineEventArgs(MonoBehaviour sender, CurvySpline spline, object data = null) : base(sender, data)
		{
			this.Spline = spline;
		}

		public readonly CurvySpline Spline;
	}
}
