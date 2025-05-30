// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Controllers.ConnectedControlPointsSelector
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Controllers
{
	public abstract class ConnectedControlPointsSelector : MonoBehaviour
	{
		public abstract CurvySplineSegment SelectConnectedControlPoint(SplineController caller, CurvyConnection connection, CurvySplineSegment currentControlPoint);
	}
}
