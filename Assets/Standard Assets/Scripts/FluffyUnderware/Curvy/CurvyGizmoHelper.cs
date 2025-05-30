// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvyGizmoHelper
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	public static class CurvyGizmoHelper
	{
		public static void SegmentCurveGizmo(CurvySplineSegment seg, Color col, float stepSize = 0.05f)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = CurvyGizmoHelper.Matrix;
			Gizmos.color = col;
			if (seg.Spline.Interpolation == CurvyInterpolation.Linear)
			{
				Gizmos.DrawLine(seg.Interpolate(0f), seg.Interpolate(1f));
				return;
			}
			Vector3 from = seg.Interpolate(0f);
			for (float num = stepSize; num < 1f; num += stepSize)
			{
				Vector3 vector = seg.Interpolate(num);
				Gizmos.DrawLine(from, vector);
				from = vector;
			}
			Gizmos.DrawLine(from, seg.Interpolate(1f));
			Gizmos.matrix = matrix;
		}

		public static void SegmentApproximationGizmo(CurvySplineSegment seg, Color col)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = CurvyGizmoHelper.Matrix;
			Gizmos.color = col;
			Vector3 a = new Vector3(0.1f / seg.Spline.transform.localScale.x, 0.1f / seg.Spline.transform.localScale.y, 0.1f / seg.Spline.transform.localScale.z);
			for (int i = 0; i < seg.Approximation.Length; i++)
			{
				Vector3 vector = seg.Approximation[i];
				Gizmos.DrawCube(vector, DTUtility.GetHandleSize(vector) * a);
			}
			Gizmos.matrix = matrix;
		}

		public static void SegmentOrientationAnchorGizmo(CurvySplineSegment seg, Color col)
		{
			if (seg.ApproximationUp.Length == 0)
			{
				return;
			}
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = CurvyGizmoHelper.Matrix;
			Gizmos.color = col;
			Vector3 vector = new Vector3(1f / seg.Spline.transform.localScale.x, 1f / seg.Spline.transform.localScale.y, 1f / seg.Spline.transform.localScale.z);
			Vector3 a = seg.ApproximationUp[0];
			a.Set(a.x * vector.x, a.y * vector.y, a.z * vector.z);
			Gizmos.DrawRay(seg.Approximation[0], a * CurvyGlobalManager.GizmoOrientationLength * 1.75f);
			Gizmos.matrix = matrix;
		}

		public static void SegmentOrientationGizmo(CurvySplineSegment seg, Color col)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = CurvyGizmoHelper.Matrix;
			Gizmos.color = col;
			Vector3 vector = new Vector3(1f / seg.Spline.transform.localScale.x, 1f / seg.Spline.transform.localScale.y, 1f / seg.Spline.transform.localScale.z);
			for (int i = 0; i < seg.ApproximationUp.Length; i++)
			{
				Vector3 a = seg.ApproximationUp[i];
				a.Set(a.x * vector.x, a.y * vector.y, a.z * vector.z);
				Gizmos.DrawRay(seg.Approximation[i], a * CurvyGlobalManager.GizmoOrientationLength);
			}
			Gizmos.matrix = matrix;
		}

		public static void SegmentTangentGizmo(CurvySplineSegment seg, Color col)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = CurvyGizmoHelper.Matrix;
			Gizmos.color = col;
			for (int i = 0; i < seg.ApproximationT.Length; i++)
			{
				Gizmos.color = ((i != seg.CacheSize) ? ((i != 0) ? col : Color.blue) : Color.black);
				Vector3 from = seg.Approximation[i];
				Vector3 normalized = seg.ApproximationT[i].normalized;
				Gizmos.DrawRay(from, normalized * CurvyGlobalManager.GizmoOrientationLength);
			}
			Gizmos.matrix = matrix;
		}

		public static void ControlPointGizmo(CurvySplineSegment cp, bool selected, Color col)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = Matrix4x4.identity;
			Gizmos.color = col;
			Vector3 vector = CurvyGizmoHelper.Matrix.MultiplyPoint(cp.transform.localPosition);
			float num = (!selected) ? 0.7f : 1f;
			if (cp.Spline && cp.Spline.RestrictTo2D)
			{
				Gizmos.DrawCube(vector, Vector3.one * DTUtility.GetHandleSize(vector) * num * CurvyGlobalManager.GizmoControlPointSize);
			}
			else
			{
				Gizmos.DrawSphere(vector, DTUtility.GetHandleSize(vector) * num * CurvyGlobalManager.GizmoControlPointSize);
			}
			Gizmos.matrix = matrix;
		}

		public static void BoundsGizmo(CurvySplineSegment cp, Color col)
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = CurvyGizmoHelper.Matrix;
			Gizmos.color = col;
			Gizmos.DrawWireCube(cp.Bounds.center, cp.Bounds.size);
			Gizmos.matrix = matrix;
		}

		public static Matrix4x4 Matrix = Matrix4x4.identity;
	}
}
