// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGShape
using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy.Utils;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[CGDataInfo(0.73f, 0.87f, 0.98f, 1f)]
	public class CGShape : CGData
	{
		public CGShape()
		{
		}

		public CGShape(CGShape source)
		{
			this.Position = (Vector3[])source.Position.Clone();
			this.Normal = (Vector3[])source.Normal.Clone();
			this.Map = (float[])source.Map.Clone();
			this.F = (float[])source.F.Clone();
			this.SourceF = (float[])source.SourceF.Clone();
			this.MaterialGroups = new List<SamplePointsMaterialGroup>(source.MaterialGroups);
			this.Closed = source.Closed;
			this.Seamless = source.Seamless;
			this.Length = source.Length;
			this.Bounds = source.Bounds;
			this.SourceIsManaged = source.SourceIsManaged;
		}

		public override int Count
		{
			get
			{
				return this.F.Length;
			}
		}

		public override T Clone<T>()
		{
			return new CGShape(this) as T;
		}

		public static void Copy(CGShape dest, CGShape source)
		{
			Array.Resize<Vector3>(ref dest.Position, source.Position.Length);
			source.Position.CopyTo(dest.Position, 0);
			Array.Resize<Vector3>(ref dest.Normal, source.Normal.Length);
			source.Normal.CopyTo(dest.Normal, 0);
			Array.Resize<float>(ref dest.Map, source.Map.Length);
			source.Map.CopyTo(dest.Map, 0);
			Array.Resize<float>(ref dest.F, source.F.Length);
			source.F.CopyTo(dest.F, 0);
			Array.Resize<float>(ref dest.SourceF, source.SourceF.Length);
			source.SourceF.CopyTo(dest.SourceF, 0);
			dest.MaterialGroups = new List<SamplePointsMaterialGroup>(source.MaterialGroups);
			dest.Bounds = source.Bounds;
			dest.Closed = source.Closed;
			dest.Seamless = source.Seamless;
			dest.Length = source.Length;
		}

		public float DistanceToF(float distance)
		{
			return Mathf.Clamp(distance, 0f, this.Length) / this.Length;
		}

		public float FToDistance(float f)
		{
			return Mathf.Clamp01(f) * this.Length;
		}

		public int GetFIndex(float f, out float frag)
		{
			if (this.mCacheLastF != f)
			{
				this.mCacheLastF = f;
				this.mCacheLastIndex = base.getGenericFIndex(ref this.F, f, out this.mCacheLastFrag);
			}
			frag = this.mCacheLastFrag;
			return this.mCacheLastIndex;
		}

		public Vector3 InterpolatePosition(float f)
		{
			float t;
			int findex = this.GetFIndex(f, out t);
			return Vector3.LerpUnclamped(this.Position[findex], this.Position[findex + 1], t);
		}

		public Vector3 InterpolateUp(float f)
		{
			float t;
			int findex = this.GetFIndex(f, out t);
			return Vector3.SlerpUnclamped(this.Normal[findex], this.Normal[findex + 1], t);
		}

		public void Interpolate(float f, out Vector3 pos, out Vector3 up)
		{
			float t;
			int findex = this.GetFIndex(f, out t);
			pos = Vector3.LerpUnclamped(this.Position[findex], this.Position[findex + 1], t);
			up = Vector3.SlerpUnclamped(this.Normal[findex], this.Normal[findex + 1], t);
		}

		public void Move(ref float f, ref int direction, float speed, CurvyClamping clamping)
		{
			f = CurvyUtility.ClampTF(f + speed, ref direction, clamping);
		}

		public void MoveBy(ref float f, ref int direction, float speedDist, CurvyClamping clamping)
		{
			float distance = CurvyUtility.ClampDistance(this.FToDistance(f) + speedDist * (float)direction, ref direction, clamping, this.Length);
			f = this.DistanceToF(distance);
		}

		public virtual void Recalculate()
		{
			this.Length = 0f;
			float[] array = new float[this.Count];
			for (int i = 1; i < this.Count; i++)
			{
				array[i] = array[i - 1] + (this.Position[i] - this.Position[i - 1]).magnitude;
			}
			if (this.Count > 0)
			{
				this.Length = array[this.Count - 1];
				if (this.Length > 0f)
				{
					this.F[0] = 0f;
					for (int j = 1; j < this.Count - 1; j++)
					{
						this.F[j] = array[j] / this.Length;
					}
					this.F[this.Count - 1] = 1f;
				}
				else
				{
					this.F = new float[this.Count];
				}
			}
		}

		public void RecalculateNormals(List<int> softEdges)
		{
			if (this.Normal.Length != this.Position.Length)
			{
				Array.Resize<Vector3>(ref this.Normal, this.Position.Length);
			}
			for (int i = 0; i < this.MaterialGroups.Count; i++)
			{
				for (int j = 0; j < this.MaterialGroups[i].Patches.Count; j++)
				{
					SamplePointsPatch samplePointsPatch = this.MaterialGroups[i].Patches[j];
					Vector3 normalized;
					for (int k = 0; k < samplePointsPatch.Count; k++)
					{
						int num = samplePointsPatch.Start + k;
						normalized = (this.Position[num + 1] - this.Position[num]).normalized;
						this.Normal[num] = new Vector3(-normalized.y, normalized.x, 0f);
					}
					normalized = (this.Position[samplePointsPatch.End] - this.Position[samplePointsPatch.End - 1]).normalized;
					this.Normal[samplePointsPatch.End] = new Vector3(-normalized.y, normalized.x, 0f);
				}
			}
			for (int l = 0; l < softEdges.Count; l++)
			{
				int num2 = softEdges[l] - 1;
				if (num2 < 0)
				{
					num2 = this.Position.Length - 1;
				}
				int num3 = num2 - 1;
				int num4 = softEdges[l] + 1;
				if (num4 == this.Position.Length)
				{
					num4 = 0;
				}
				this.Normal[softEdges[l]] = Vector3.Slerp(this.Normal[num3], this.Normal[num4], 0.5f);
				this.Normal[num2] = this.Normal[softEdges[l]];
			}
		}

		public float[] SourceF = new float[0];

		public float[] F = new float[0];

		public Vector3[] Position = new Vector3[0];

		public Vector3[] Normal = new Vector3[0];

		public float[] Map = new float[0];

		[Obsolete("Bounds are computed but not used by Curvy. It will be removed in a future version as an optimization")]
		public Bounds Bounds;

		public List<SamplePointsMaterialGroup> MaterialGroups = new List<SamplePointsMaterialGroup>();

		public bool SourceIsManaged;

		public bool Closed;

		public bool Seamless;

		public float Length;

		private float mCacheLastF = float.MaxValue;

		private int mCacheLastIndex;

		private float mCacheLastFrag;
	}
}
