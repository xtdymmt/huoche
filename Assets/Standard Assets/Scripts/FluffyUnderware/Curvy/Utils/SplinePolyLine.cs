// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Utils.SplinePolyLine
using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy.ThirdParty.LibTessDotNet;
using UnityEngine;

namespace FluffyUnderware.Curvy.Utils
{
	[Serializable]
	public class SplinePolyLine
	{
		public SplinePolyLine(CurvySpline spline) : this(spline, SplinePolyLine.VertexCalculation.ByApproximation, 0f, 0f, Space.World)
		{
		}

		public SplinePolyLine(CurvySpline spline, float angle, float distance) : this(spline, SplinePolyLine.VertexCalculation.ByAngle, angle, distance, Space.World)
		{
		}

		private SplinePolyLine(CurvySpline spline, SplinePolyLine.VertexCalculation vertexMode, float angle, float distance, Space space = Space.World)
		{
			this.Spline = spline;
			this.VertexMode = vertexMode;
			this.Angle = angle;
			this.Distance = distance;
			this.Space = space;
		}

		public bool IsClosed
		{
			get
			{
				return this.Spline && this.Spline.Closed;
			}
		}

		public Vector3[] GetVertices()
		{
			Vector3[] array = new Vector3[0];
			SplinePolyLine.VertexCalculation vertexMode = this.VertexMode;
			if (vertexMode != SplinePolyLine.VertexCalculation.ByAngle)
			{
				array = this.Spline.GetApproximation(Space.Self);
			}
			else
			{
				List<float> list;
				List<Vector3> list2;
				array = SplinePolyLine.GetPolygon(this.Spline, 0f, 1f, this.Angle, this.Distance, -1f, out list, out list2, false, 0.01f);
			}
			if (this.Space == Space.World)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.Spline.transform.TransformPoint(array[i]);
				}
			}
			return array;
		}

		private static Vector3[] GetPolygon(CurvySpline spline, float fromTF, float toTF, float maxAngle, float minDistance, float maxDistance, out List<float> vertexTF, out List<Vector3> vertexTangents, bool includeEndPoint = true, float stepSize = 0.01f)
		{
			stepSize = Mathf.Clamp(stepSize, 0.002f, 1f);
			maxDistance = ((maxDistance != -1f) ? Mathf.Clamp(maxDistance, 0f, spline.Length) : spline.Length);
			minDistance = Mathf.Clamp(minDistance, 0f, maxDistance);
			if (!spline.Closed)
			{
				toTF = Mathf.Clamp01(toTF);
				fromTF = Mathf.Clamp(fromTF, 0f, toTF);
			}
			List<Vector3> vPos = new List<Vector3>();
			List<Vector3> vTan = new List<Vector3>();
			List<float> vTF = new List<float>();
			int linearSteps = 0;
			float angleFromLast = 0f;
			float distAccu = 0f;
			Vector3 curPos = spline.Interpolate(fromTF);
			Vector3 curTangent = spline.GetTangent(fromTF);
			Vector3 curPos2 = curPos;
			Vector3 curTangent2 = curTangent;
			Action<float> action = delegate(float f)
			{
				vPos.Add(curPos);
				vTan.Add(curTangent);
				vTF.Add(f);
				angleFromLast = 0f;
				distAccu = 0f;
				linearSteps = 0;
			};
			action(fromTF);
			float num = fromTF + stepSize;
			while (num < toTF)
			{
				float num2 = num % 1f;
				curPos = spline.Interpolate(num2);
				curTangent = spline.GetTangent(num2);
				if (curTangent == Vector3.zero)
				{
					UnityEngine.Debug.Log("zero Tangent! Oh no!");
				}
				distAccu += (curPos - curPos2).magnitude;
				if (curTangent == curTangent2)
				{
					linearSteps++;
				}
				if (distAccu >= minDistance)
				{
					if (distAccu >= maxDistance)
					{
						action(num2);
					}
					else
					{
						angleFromLast += Vector3.Angle(curTangent2, curTangent);
						if (angleFromLast >= maxAngle || (linearSteps > 0 && angleFromLast > 0f))
						{
							action(num2);
						}
					}
				}
				num += stepSize;
				curPos2 = curPos;
				curTangent2 = curTangent;
			}
			if (includeEndPoint)
			{
				vTF.Add(toTF % 1f);
				curPos = spline.Interpolate(toTF % 1f);
				vPos.Add(curPos);
				vTan.Add(spline.GetTangent(toTF % 1f, curPos));
			}
			vertexTF = vTF;
			vertexTangents = vTan;
			return vPos.ToArray();
		}

		public ContourOrientation Orientation;

		public CurvySpline Spline;

		public SplinePolyLine.VertexCalculation VertexMode;

		public float Angle;

		public float Distance;

		public Space Space;

		public enum VertexCalculation
		{
			ByApproximation,
			ByAngle
		}
	}
}
