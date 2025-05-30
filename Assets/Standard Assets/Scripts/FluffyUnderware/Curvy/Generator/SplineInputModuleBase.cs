// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.SplineInputModuleBase
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public abstract class SplineInputModuleBase : CGModule
	{
		public bool UseCache
		{
			get
			{
				return this.m_UseCache;
			}
			set
			{
				if (this.m_UseCache != value)
				{
					this.m_UseCache = value;
				}
				base.Dirty = true;
			}
		}

		public CurvySplineSegment StartCP
		{
			get
			{
				return this.m_StartCP;
			}
			set
			{
				if (this.m_StartCP != value)
				{
					this.m_StartCP = value;
					this.ValidateStartAndEndCps();
				}
				base.Dirty = true;
			}
		}

		public CurvySplineSegment EndCP
		{
			get
			{
				return this.m_EndCP;
			}
			set
			{
				if (this.m_EndCP != value)
				{
					this.m_EndCP = value;
					this.ValidateStartAndEndCps();
				}
				base.Dirty = true;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.Properties.MinWidth = 250f;
		}

		protected abstract void ValidateStartAndEndCps();

		protected float getPathLength(CurvySpline spline)
		{
			if (!spline)
			{
				return 0f;
			}
			if (this.StartCP && this.EndCP)
			{
				return this.EndCP.Distance - this.StartCP.Distance;
			}
			return spline.Length;
		}

		protected bool getPathClosed(CurvySpline spline)
		{
			return spline && spline.Closed && this.EndCP == null;
		}

		protected CGData GetSplineData(CurvySpline spline, bool fullPath, CGDataRequestRasterization raster, CGDataRequestMetaCGOptions options)
		{
			if (spline == null || spline.Count == 0)
			{
				return null;
			}
			List<ControlPointOption> list = new List<ControlPointOption>();
			int materialID = 0;
			float maxDistance = float.MaxValue;
			CGShape cgshape = (!fullPath) ? new CGShape() : new CGPath();
			float num;
			float num2;
			if (this.StartCP)
			{
				float pathLength = this.getPathLength(spline);
				num = this.StartCP.Distance + pathLength * raster.Start;
				num2 = this.StartCP.Distance + pathLength * (raster.Start + raster.RasterizedRelativeLength);
			}
			else
			{
				num = spline.Length * raster.Start;
				num2 = spline.Length * (raster.Start + raster.RasterizedRelativeLength);
			}
			float num3 = CurvySpline.CalculateSamplingPointsPerUnit(raster.Resolution, spline.MaxPointsPerUnit);
			float num4 = (num2 - num) / (raster.SplineAbsoluteLength * raster.RasterizedRelativeLength * num3);
			cgshape.Length = num2 - num;
			float num5 = spline.DistanceToTF(num, CurvyClamping.Clamp);
			float startTF = num5;
			float num6 = (num2 <= spline.Length || !spline.Closed) ? spline.DistanceToTF(num2, CurvyClamping.Clamp) : (spline.DistanceToTF(num2 - spline.Length, CurvyClamping.Clamp) + 1f);
			cgshape.SourceIsManaged = base.IsManagedResource(spline);
			cgshape.Closed = spline.Closed;
			cgshape.Seamless = (spline.Closed && raster.RasterizedRelativeLength == 1f);
			if (cgshape.Length == 0f)
			{
				return cgshape;
			}
			if (options)
			{
				list = CGUtility.GetControlPointsWithOptions(options, spline, num, num2, raster.Mode == CGDataRequestRasterization.ModeEnum.Optimized, out materialID, out maxDistance);
			}
			List<SamplePointUData> list2 = new List<SamplePointUData>();
			List<Vector3> list3 = new List<Vector3>();
			List<float> list4 = new List<float>();
			List<float> list5 = new List<float>();
			List<Vector3> list6 = new List<Vector3>();
			List<Vector3> list7 = new List<Vector3>();
			float num7 = num;
			Vector3 tangent = Vector3.zero;
			Vector3 up = Vector3.zero;
			List<int> list8 = new List<int>();
			int num8 = 100000;
			CGDataRequestRasterization.ModeEnum mode = raster.Mode;
			if (mode != CGDataRequestRasterization.ModeEnum.Even)
			{
				if (mode == CGDataRequestRasterization.ModeEnum.Optimized)
				{
					SamplePointsMaterialGroup samplePointsMaterialGroup = new SamplePointsMaterialGroup(materialID);
					SamplePointsPatch item = new SamplePointsPatch(0);
					float stepDist = num4 / spline.Length;
					float angleThreshold = raster.AngleThreshold;
					Vector3 vector = (!this.UseCache) ? spline.Interpolate(num5) : spline.InterpolateFast(num5);
					tangent = ((!this.UseCache) ? spline.GetTangent(num5, vector) : spline.GetTangentFast(num5));
					while (num5 < num6 && num8-- > 0)
					{
						SplineInputModuleBase.AddPoint(num7 / spline.Length, (num7 - num) / cgshape.Length, fullPath, vector, tangent, spline.GetOrientationUpFast(num5 % 1f), list5, list4, list3, list6, list7);
						float stopTF = (list.Count <= 0) ? num6 : list[0].TF;
						bool flag = SplineInputModuleBase.MoveByAngleExt(spline, this.UseCache, ref num5, maxDistance, angleThreshold, out vector, out tangent, stopTF, cgshape.Closed, stepDist);
						num7 = spline.TFToDistance(num5, CurvyClamping.Clamp);
						if (Mathf.Approximately(num5, num6) || num5 > num6)
						{
							num7 = num2;
							num6 = ((!cgshape.Closed) ? Mathf.Clamp01(num6) : DTMath.Repeat(num6, 1f));
							vector = ((!this.UseCache) ? spline.Interpolate(num6) : spline.InterpolateFast(num6));
							if (fullPath)
							{
								tangent = ((!this.UseCache) ? spline.GetTangent(num6, vector) : spline.GetTangentFast(num6));
							}
							SplineInputModuleBase.AddPoint(num7 / spline.Length, (num7 - num) / cgshape.Length, fullPath, vector, tangent, spline.GetOrientationUpFast(num6), list5, list4, list3, list6, list7);
							break;
						}
						if (flag)
						{
							if (list.Count <= 0)
							{
								SplineInputModuleBase.AddPoint(num7 / spline.Length, (num7 - num) / cgshape.Length, fullPath, vector, tangent, spline.GetOrientationUpFast(num5), list5, list4, list3, list6, list7);
								break;
							}
							if (list[0].UVEdge || list[0].UVShift)
							{
								list2.Add(new SamplePointUData(list3.Count, list[0].UVEdge, list[0].FirstU, list[0].SecondU));
							}
							num7 = list[0].Distance;
							maxDistance = list[0].MaxStepDistance;
							bool flag2 = list[0].HardEdge || list[0].MaterialID != samplePointsMaterialGroup.MaterialID || (options.CheckExtendedUV && list[0].UVEdge);
							if (flag2)
							{
								item.End = list3.Count;
								samplePointsMaterialGroup.Patches.Add(item);
								if (samplePointsMaterialGroup.MaterialID != list[0].MaterialID)
								{
									cgshape.MaterialGroups.Add(samplePointsMaterialGroup);
									samplePointsMaterialGroup = new SamplePointsMaterialGroup(list[0].MaterialID);
								}
								item = new SamplePointsPatch(list3.Count + 1);
								if (!list[0].HardEdge)
								{
									list8.Add(list3.Count + 1);
								}
								if (list[0].UVEdge || list[0].UVShift)
								{
									list2.Add(new SamplePointUData(list3.Count + 1, list[0].UVEdge, list[0].FirstU, list[0].SecondU));
								}
								SplineInputModuleBase.AddPoint(num7 / spline.Length, (num7 - num) / cgshape.Length, fullPath, vector, tangent, spline.GetOrientationUpFast(num5), list5, list4, list3, list6, list7);
							}
							list.RemoveAt(0);
						}
					}
					if (num8 <= 0)
					{
						UnityEngine.Debug.LogError("[Curvy] He's dead, Jim! Deadloop in SplineInputModuleBase.GetSplineData (Optimized)! Please send a bug report.");
					}
					item.End = list3.Count - 1;
					samplePointsMaterialGroup.Patches.Add(item);
					if (list.Count > 0 && list[0].UVShift)
					{
						list2.Add(new SamplePointUData(list3.Count - 1, list[0].UVEdge, list[0].FirstU, list[0].SecondU));
					}
					if (cgshape.Closed && !spline[0].GetMetadata<MetaCGOptions>(true).HardEdge)
					{
						list8.Add(0);
					}
					SplineInputModuleBase.FillData(cgshape, samplePointsMaterialGroup, list5, list4, fullPath, list3, list6, list7, spline.Bounds);
				}
			}
			else
			{
				bool flag2 = false;
				SamplePointsMaterialGroup samplePointsMaterialGroup = new SamplePointsMaterialGroup(materialID);
				SamplePointsPatch item = new SamplePointsPatch(0);
				CurvyClamping clamping = (!cgshape.Closed) ? CurvyClamping.Clamp : CurvyClamping.Loop;
				while (num7 <= num2 && --num8 > 0)
				{
					num5 = spline.DistanceToTF(spline.ClampDistance(num7, clamping), CurvyClamping.Clamp);
					float num9 = (num7 - num) / cgshape.Length;
					if (Mathf.Approximately(1f, num9))
					{
						num9 = 1f;
					}
					float localF;
					CurvySplineSegment curvySplineSegment = spline.TFToSegment(num5, out localF, CurvyClamping.Clamp);
					Vector3 vector = (!this.UseCache) ? curvySplineSegment.Interpolate(localF, spline.Interpolation) : curvySplineSegment.InterpolateFast(localF);
					if (fullPath)
					{
						tangent = ((!this.UseCache) ? curvySplineSegment.GetTangent(localF, vector) : curvySplineSegment.GetTangentFast(localF));
						up = curvySplineSegment.GetOrientationUpFast(localF);
					}
					SplineInputModuleBase.AddPoint(num7 / spline.Length, num9, fullPath, vector, tangent, up, list5, list4, list3, list6, list7);
					if (flag2)
					{
						SplineInputModuleBase.AddPoint(num7 / spline.Length, num9, fullPath, vector, tangent, up, list5, list4, list3, list6, list7);
						flag2 = false;
					}
					num7 += num4;
					if (list.Count > 0 && num7 >= list[0].Distance)
					{
						if (list[0].UVEdge || list[0].UVShift)
						{
							list2.Add(new SamplePointUData(list3.Count, list[0].UVEdge, list[0].FirstU, list[0].SecondU));
						}
						num7 = list[0].Distance;
						flag2 = (list[0].HardEdge || list[0].MaterialID != samplePointsMaterialGroup.MaterialID || (options.CheckExtendedUV && list[0].UVEdge));
						if (flag2)
						{
							item.End = list3.Count;
							samplePointsMaterialGroup.Patches.Add(item);
							if (samplePointsMaterialGroup.MaterialID != list[0].MaterialID)
							{
								cgshape.MaterialGroups.Add(samplePointsMaterialGroup);
								samplePointsMaterialGroup = new SamplePointsMaterialGroup(list[0].MaterialID);
							}
							item = new SamplePointsPatch(list3.Count + 1);
							if (!list[0].HardEdge)
							{
								list8.Add(list3.Count + 1);
							}
							if (list[0].UVEdge || list[0].UVShift)
							{
								list2.Add(new SamplePointUData(list3.Count + 1, list[0].UVEdge, list[0].FirstU, list[0].SecondU));
							}
						}
						list.RemoveAt(0);
					}
					if (num7 > num2 && num9 < 1f)
					{
						num7 = num2;
					}
				}
				if (num8 <= 0)
				{
					UnityEngine.Debug.LogError("[Curvy] He's dead, Jim! Deadloop in SplineInputModuleBase.GetSplineData (Even)! Please send a bug report.");
				}
				item.End = list3.Count - 1;
				samplePointsMaterialGroup.Patches.Add(item);
				if (cgshape.Closed && !spline[0].GetMetadata<MetaCGOptions>(true).HardEdge)
				{
					list8.Add(0);
				}
				SplineInputModuleBase.FillData(cgshape, samplePointsMaterialGroup, list5, list4, fullPath, list3, list6, list7, spline.Bounds);
			}
			cgshape.Map = (float[])cgshape.F.Clone();
			if (!fullPath)
			{
				cgshape.RecalculateNormals(list8);
				if (options && options.CheckExtendedUV)
				{
					this.CalculateExtendedUV(spline, startTF, num6, list2, cgshape);
				}
			}
			return cgshape;
		}

		private static void FillData(CGShape dataToFill, SamplePointsMaterialGroup materialGroup, List<float> sourceFs, List<float> relativeFs, bool isFullPath, List<Vector3> positions, List<Vector3> tangents, List<Vector3> normals, Bounds bounds)
		{
			dataToFill.MaterialGroups.Add(materialGroup);
			dataToFill.SourceF = sourceFs.ToArray();
			dataToFill.F = relativeFs.ToArray();
			dataToFill.Position = positions.ToArray();
			dataToFill.Bounds = bounds;
			if (isFullPath)
			{
				((CGPath)dataToFill).Direction = tangents.ToArray();
				dataToFill.Normal = normals.ToArray();
			}
		}

		private static void AddPoint(float sourceF, float relativeF, bool isFullPath, Vector3 position, Vector3 tangent, Vector3 up, List<float> sourceFList, List<float> relativeFList, List<Vector3> positionList, List<Vector3> tangentList, List<Vector3> upList)
		{
			sourceFList.Add(sourceF);
			positionList.Add(position);
			relativeFList.Add(relativeF);
			if (isFullPath)
			{
				tangentList.Add(tangent);
				upList.Add(up);
			}
		}

		private static bool MoveByAngleExt(CurvySpline spline, bool useCache, ref float tf, float maxDistance, float maxAngle, out Vector3 pos, out Vector3 tan, float stopTF, bool loop, float stepDist)
		{
			if (!loop)
			{
				tf = Mathf.Clamp01(tf);
			}
			float tf2 = (!loop) ? tf : (tf % 1f);
			SplineInputModuleBase.GetPositionAndTangent(spline, useCache, out pos, out tan, tf2);
			Vector3 vector = pos;
			Vector3 from = tan;
			float num = 0f;
			float num2 = 0f;
			if (stopTF < tf && loop)
			{
				stopTF += 1f;
			}
			bool flag = false;
			while (tf < stopTF && !flag)
			{
				tf = Mathf.Min(stopTF, tf + stepDist);
				tf2 = ((!loop) ? tf : (tf % 1f));
				SplineInputModuleBase.GetPositionAndTangent(spline, useCache, out pos, out tan, tf2);
				Vector3 vector2;
				vector2.x = pos.x - vector.x;
				vector2.y = pos.y - vector.y;
				vector2.z = pos.z - vector.z;
				num += vector2.magnitude;
				float num3 = Vector3.Angle(from, tan);
				num2 += num3;
				if (num >= maxDistance || num2 >= maxAngle || (num3 == 0f && num2 > 0f))
				{
					flag = true;
				}
				else
				{
					vector = pos;
					from = tan;
				}
			}
			return Mathf.Approximately(tf, stopTF);
		}

		private static void GetPositionAndTangent(CurvySpline spline, bool useCache, out Vector3 position, out Vector3 tangent, float tf)
		{
			float localF;
			CurvySplineSegment curvySplineSegment = spline.TFToSegment(tf, out localF, CurvyClamping.Clamp);
			if (useCache)
			{
				position = curvySplineSegment.InterpolateFast(localF);
				tangent = curvySplineSegment.GetTangentFast(localF);
			}
			else
			{
				position = curvySplineSegment.Interpolate(localF, spline.Interpolation);
				tangent = curvySplineSegment.GetTangent(localF, position);
			}
		}

		private void CalculateExtendedUV(CurvySpline spline, float startTF, float endTF, List<SamplePointUData> ext, CGShape data)
		{
			CurvySplineSegment curvySplineSegment;
			MetaCGOptions metaCGOptions = SplineInputModuleBase.findPreviousReferenceCPOptions(spline, startTF, out curvySplineSegment);
			CurvySplineSegment curvySplineSegment2;
			MetaCGOptions metaCGOptions2 = SplineInputModuleBase.findNextReferenceCPOptions(spline, startTF, out curvySplineSegment2);
			float num;
			if (spline.FirstVisibleControlPoint == curvySplineSegment2)
			{
				num = (data.SourceF[0] * spline.Length - curvySplineSegment.Distance) / (spline.Length - curvySplineSegment.Distance);
			}
			else
			{
				num = (data.SourceF[0] * spline.Length - curvySplineSegment.Distance) / (curvySplineSegment2.Distance - curvySplineSegment.Distance);
			}
			ext.Insert(0, new SamplePointUData(0, startTF == 0f && metaCGOptions.UVEdge, num * (metaCGOptions2.FirstU - metaCGOptions.GetDefinedFirstU(0f)) + metaCGOptions.GetDefinedFirstU(0f), (startTF != 0f || !metaCGOptions.UVEdge) ? 0f : metaCGOptions.SecondU));
			if (ext[ext.Count - 1].Vertex < data.Count - 1)
			{
				metaCGOptions = SplineInputModuleBase.findPreviousReferenceCPOptions(spline, endTF, out curvySplineSegment);
				metaCGOptions2 = SplineInputModuleBase.findNextReferenceCPOptions(spline, endTF, out curvySplineSegment2);
				float num2 = metaCGOptions2.FirstU;
				float definedSecondU = metaCGOptions.GetDefinedSecondU(0f);
				if (spline.FirstVisibleControlPoint == curvySplineSegment2)
				{
					num = (data.SourceF[data.Count - 1] * spline.Length - curvySplineSegment.Distance) / (spline.Length - curvySplineSegment.Distance);
					if (metaCGOptions2.UVEdge)
					{
						num2 = metaCGOptions2.FirstU;
					}
					else if (ext.Count > 1)
					{
						num2 = (float)(Mathf.FloorToInt((!ext[ext.Count - 1].UVEdge) ? ext[ext.Count - 1].FirstU : ext[ext.Count - 1].SecondU) + 1);
					}
					else
					{
						num2 = 1f;
					}
				}
				else
				{
					num = (data.SourceF[data.Count - 1] * spline.Length - curvySplineSegment.Distance) / (curvySplineSegment2.Distance - curvySplineSegment.Distance);
				}
				ext.Add(new SamplePointUData(data.Count - 1, false, num * (num2 - definedSecondU) + definedSecondU, 0f));
			}
			float num3 = 0f;
			float num4 = (!ext[0].UVEdge) ? ext[0].FirstU : ext[0].SecondU;
			float firstU = ext[1].FirstU;
			float num5 = data.F[ext[1].Vertex] - data.F[ext[0].Vertex];
			int num6 = 1;
			for (int i = 0; i < data.Count - 1; i++)
			{
				float num7 = (data.F[i] - num3) / num5;
				data.Map[i] = (firstU - num4) * num7 + num4;
				if (ext[num6].Vertex == i)
				{
					if (ext[num6].FirstU == ext[num6 + 1].FirstU)
					{
						num4 = ((!ext[num6].UVEdge) ? ext[num6].FirstU : ext[num6].SecondU);
						num6++;
					}
					else
					{
						num4 = ext[num6].FirstU;
					}
					firstU = ext[num6 + 1].FirstU;
					num5 = data.F[ext[num6 + 1].Vertex] - data.F[ext[num6].Vertex];
					num3 = data.F[i];
					num6++;
				}
			}
			data.Map[data.Count - 1] = ext[ext.Count - 1].FirstU;
		}

		private static MetaCGOptions findPreviousReferenceCPOptions(CurvySpline spline, float tf, out CurvySplineSegment cp)
		{
			cp = spline.TFToSegment(tf);
			MetaCGOptions metadata;
			for (;;)
			{
				metadata = cp.GetMetadata<MetaCGOptions>(true);
				if (spline.FirstVisibleControlPoint == cp)
				{
					break;
				}
				cp = spline.GetPreviousSegment(cp);
				if (!cp || metadata.UVEdge || metadata.ExplicitU || metadata.HasDifferentMaterial)
				{
					return metadata;
				}
			}
			return metadata;
		}

		private static MetaCGOptions findNextReferenceCPOptions(CurvySpline spline, float tf, out CurvySplineSegment cp)
		{
			float num;
			cp = spline.TFToSegment(tf, out num);
			MetaCGOptions metadata;
			for (;;)
			{
				cp = spline.GetNextControlPoint(cp);
				metadata = cp.GetMetadata<MetaCGOptions>(true);
				if (!spline.Closed && spline.LastVisibleControlPoint == cp)
				{
					break;
				}
				if (metadata.UVEdge || metadata.ExplicitU || metadata.HasDifferentMaterial || spline.FirstSegment == cp)
				{
					return metadata;
				}
			}
			return metadata;
		}

		[Tab("General")]
		[SerializeField]
		[Tooltip("Makes this module use the cached approximations of the spline's positions and tangents")]
		private bool m_UseCache;

		[Tab("Range")]
		[SerializeField]
		protected CurvySplineSegment m_StartCP;

		[FieldCondition("m_StartCP", null, true, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		[SerializeField]
		protected CurvySplineSegment m_EndCP;
	}
}
