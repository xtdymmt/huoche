// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Controllers.CurvySplineMoveEventArgs
using System;
using System.ComponentModel;

namespace FluffyUnderware.Curvy.Controllers
{
	public class CurvySplineMoveEventArgs : CancelEventArgs
	{
		public CurvySplineMoveEventArgs(SplineController sender, CurvySpline spline, CurvySplineSegment controlPoint, float position, bool usingWorldUnits, float delta, MovementDirection direction)
		{
			this.Set_INTERNAL(sender, spline, controlPoint, position, delta, direction, usingWorldUnits);
		}

		public SplineController Sender { get; private set; }

		public CurvySpline Spline { get; private set; }

		public CurvySplineSegment ControlPoint { get; private set; }

		public bool WorldUnits { get; private set; }

		public MovementDirection MovementDirection { get; private set; }

		public float Delta { get; private set; }

		public float Position { get; private set; }

		internal void Set_INTERNAL(SplineController sender, CurvySpline spline, CurvySplineSegment controlPoint, float position, float delta, MovementDirection direction, bool usingWorldUnits)
		{
			this.Sender = sender;
			this.Spline = spline;
			this.ControlPoint = controlPoint;
			this.MovementDirection = direction;
			this.Delta = delta;
			this.Position = position;
			this.WorldUnits = usingWorldUnits;
			base.Cancel = false;
		}
	}
}
