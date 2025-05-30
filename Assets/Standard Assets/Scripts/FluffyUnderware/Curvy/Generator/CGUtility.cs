// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGUtility
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public static class CGUtility
	{
		public static Vector2[] CalculateUV2(Vector2[] uv)
		{
			Vector2[] array = new Vector2[uv.Length];
			float num = 1f;
			float num2 = 1f;
			for (int i = 0; i < uv.Length; i++)
			{
				num = ((num >= uv[i].x) ? num : uv[i].x);
				num2 = ((num2 >= uv[i].y) ? num2 : uv[i].y);
			}
			float num3 = 1f / num;
			float num4 = 1f / num2;
			for (int j = 0; j < uv.Length; j++)
			{
				array[j].x = uv[j].x * num3;
				array[j].y = uv[j].y * num4;
			}
			return array;
		}

		public static List<ControlPointOption> GetControlPointsWithOptions(CGDataRequestMetaCGOptions options, CurvySpline shape, float startDist, float endDist, bool optimize, out int initialMaterialID, out float initialMaxStep)
		{
			List<ControlPointOption> list = new List<ControlPointOption>();
			initialMaterialID = 0;
			initialMaxStep = float.MaxValue;
			CurvySplineSegment curvySplineSegment = shape.DistanceToSegment(startDist, CurvyClamping.Clamp);
			float num = shape.ClampDistance(endDist, (!shape.Closed) ? CurvyClamping.Clamp : CurvyClamping.Loop);
			if (num == 0f)
			{
				num = endDist;
			}
			CurvySplineSegment curvySplineSegment2 = (num != shape.Length) ? shape.DistanceToSegment(num, CurvyClamping.Clamp) : shape.LastVisibleControlPoint;
			if (endDist != shape.Length && endDist > curvySplineSegment2.Distance)
			{
				curvySplineSegment2 = shape.GetNextControlPoint(curvySplineSegment2);
			}
			float num2 = 0f;
			if (curvySplineSegment)
			{
				MetaCGOptions metadata = curvySplineSegment.GetMetadata<MetaCGOptions>(true);
				initialMaxStep = ((metadata.MaxStepDistance != 0f) ? metadata.MaxStepDistance : float.MaxValue);
				if (options.CheckMaterialID)
				{
					initialMaterialID = metadata.MaterialID;
				}
				int num3 = initialMaterialID;
				float num4 = metadata.MaxStepDistance;
				CurvySplineSegment curvySplineSegment3 = shape.GetNextSegment(curvySplineSegment) ?? shape.GetNextControlPoint(curvySplineSegment);
				do
				{
					metadata = curvySplineSegment3.GetMetadata<MetaCGOptions>(true);
					if (shape.GetControlPointIndex(curvySplineSegment3) < shape.GetControlPointIndex(curvySplineSegment))
					{
						num2 = shape.Length;
					}
					if (options.IncludeControlPoints || (options.CheckHardEdges && metadata.HardEdge) || (options.CheckMaterialID && metadata.MaterialID != num3) || (optimize && metadata.MaxStepDistance != num4) || (options.CheckExtendedUV && (metadata.UVEdge || metadata.ExplicitU)))
					{
						bool flag = metadata.MaterialID != num3;
						num4 = ((metadata.MaxStepDistance != 0f) ? metadata.MaxStepDistance : float.MaxValue);
						num3 = ((!options.CheckMaterialID) ? initialMaterialID : metadata.MaterialID);
						list.Add(new ControlPointOption(curvySplineSegment3.LocalFToTF(0f) + (float)Mathf.FloorToInt(num2 / shape.Length), curvySplineSegment3.Distance + num2, options.IncludeControlPoints, num3, options.CheckHardEdges && metadata.HardEdge, metadata.MaxStepDistance, (options.CheckExtendedUV && metadata.UVEdge) || flag, options.CheckExtendedUV && metadata.ExplicitU, metadata.FirstU, metadata.SecondU));
					}
					curvySplineSegment3 = shape.GetNextSegment(curvySplineSegment3);
				}
				while (curvySplineSegment3 && curvySplineSegment3 != curvySplineSegment2);
				if (options.CheckExtendedUV && !curvySplineSegment3 && shape.LastVisibleControlPoint == curvySplineSegment2)
				{
					metadata = curvySplineSegment2.GetMetadata<MetaCGOptions>(true);
					if (metadata.ExplicitU)
					{
						list.Add(new ControlPointOption(1f, curvySplineSegment2.Distance + num2, options.IncludeControlPoints, num3, options.CheckHardEdges && metadata.HardEdge, metadata.MaxStepDistance, (options.CheckExtendedUV && metadata.UVEdge) || (options.CheckMaterialID && metadata.MaterialID != num3), options.CheckExtendedUV && metadata.ExplicitU, metadata.FirstU, metadata.SecondU));
					}
				}
			}
			return list;
		}
	}
}
