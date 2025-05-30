// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvyControlPointEventArgs
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	public class CurvyControlPointEventArgs : CurvySplineEventArgs
	{
		public CurvyControlPointEventArgs(MonoBehaviour sender, CurvySpline spline, CurvySplineSegment cp, CurvyControlPointEventArgs.ModeEnum mode = CurvyControlPointEventArgs.ModeEnum.None, object data = null) : base(sender, spline, data)
		{
			this.ControlPoint = cp;
			this.Mode = mode;
		}

		public readonly CurvyControlPointEventArgs.ModeEnum Mode;

		public readonly CurvySplineSegment ControlPoint;

		public enum ModeEnum
		{
			None,
			AddBefore,
			AddAfter,
			Delete
		}
	}
}
