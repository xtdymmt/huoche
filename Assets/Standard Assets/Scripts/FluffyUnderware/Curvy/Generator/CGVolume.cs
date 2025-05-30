// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGVolume
using System;
using FluffyUnderware.Curvy.Utils;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[CGDataInfo(0.08f, 0.4f, 0.75f, 1f)]
	public class CGVolume : CGPath
	{
		public CGVolume()
		{
		}

		public CGVolume(int samplePoints, CGShape crossShape)
		{
			this.CrossF = (float[])crossShape.F.Clone();
			this.CrossMap = (float[])crossShape.Map.Clone();
			this.CrossClosed = crossShape.Closed;
			this.CrossSeamless = crossShape.Seamless;
			this.CrossMaterialGroups = new SamplePointsMaterialGroupCollection(crossShape.MaterialGroups);
			this.SegmentLength = new float[this.Count];
			this.Vertex = new Vector3[this.CrossSize * samplePoints];
			this.VertexNormal = new Vector3[this.Vertex.Length];
		}

		public CGVolume(CGPath path, CGShape crossShape) : base(path)
		{
			this.CrossF = (float[])crossShape.F.Clone();
			this.CrossMap = (float[])crossShape.Map.Clone();
			this.SegmentLength = new float[this.Count];
			this.CrossClosed = crossShape.Closed;
			this.CrossSeamless = crossShape.Seamless;
			this.CrossMaterialGroups = new SamplePointsMaterialGroupCollection(crossShape.MaterialGroups);
			this.Vertex = new Vector3[this.CrossSize * this.Count];
			this.VertexNormal = new Vector3[this.Vertex.Length];
		}

		public CGVolume(CGVolume source) : base(source)
		{
			this.Vertex = (Vector3[])source.Vertex.Clone();
			this.VertexNormal = (Vector3[])source.VertexNormal.Clone();
			this.CrossF = (float[])source.CrossF.Clone();
			this.CrossMap = (float[])source.CrossMap.Clone();
			this.SegmentLength = new float[this.Count];
			this.CrossClosed = source.Closed;
			this.CrossSeamless = source.CrossSeamless;
			this.CrossFShift = source.CrossFShift;
			this.CrossMaterialGroups = new SamplePointsMaterialGroupCollection(source.CrossMaterialGroups);
		}

		public int CrossSize
		{
			get
			{
				return this.CrossF.Length;
			}
		}

		public int VertexCount
		{
			get
			{
				return this.Vertex.Length;
			}
		}

		public static CGVolume Get(CGVolume data, CGPath path, CGShape crossShape)
		{
			if (data == null)
			{
				return new CGVolume(path, crossShape);
			}
			CGPath.Copy(data, path);
			Array.Resize<float>(ref data.SegmentLength, data.CrossF.Length);
			data.SegmentLength = new float[data.Count];
			Array.Resize<float>(ref data.CrossF, crossShape.F.Length);
			crossShape.F.CopyTo(data.CrossF, 0);
			Array.Resize<float>(ref data.CrossMap, crossShape.Map.Length);
			crossShape.Map.CopyTo(data.CrossMap, 0);
			data.CrossClosed = crossShape.Closed;
			data.CrossSeamless = crossShape.Seamless;
			data.CrossMaterialGroups = new SamplePointsMaterialGroupCollection(crossShape.MaterialGroups);
			Array.Resize<Vector3>(ref data.Vertex, data.CrossSize * data.Position.Length);
			Array.Resize<Vector3>(ref data.VertexNormal, data.Vertex.Length);
			return data;
		}

		public override T Clone<T>()
		{
			return new CGVolume(this) as T;
		}

		public void InterpolateVolume(float f, float crossF, out Vector3 pos, out Vector3 dir, out Vector3 up)
		{
			float num;
			float num2;
			int vertexIndex = this.GetVertexIndex(f, crossF, out num, out num2);
			Vector3 vector = this.Vertex[vertexIndex];
			Vector3 vector2 = this.Vertex[vertexIndex + 1];
			Vector3 vector3 = this.Vertex[vertexIndex + this.CrossSize];
			Vector3 vector4;
			Vector3 vector5;
			if (num + num2 > 1f)
			{
				Vector3 a = this.Vertex[vertexIndex + this.CrossSize + 1];
				vector4 = a - vector3;
				vector5 = a - vector2;
				pos = vector3 - vector5 * (1f - num) + vector4 * num2;
			}
			else
			{
				vector4 = vector2 - vector;
				vector5 = vector3 - vector;
				pos = vector + vector5 * num + vector4 * num2;
			}
			dir = vector5.normalized;
			up = Vector3.Cross(vector5, vector4);
		}

		public Vector3 InterpolateVolumePosition(float f, float crossF)
		{
			float num;
			float num2;
			int vertexIndex = this.GetVertexIndex(f, crossF, out num, out num2);
			Vector3 vector = this.Vertex[vertexIndex];
			Vector3 vector2 = this.Vertex[vertexIndex + 1];
			Vector3 vector3 = this.Vertex[vertexIndex + this.CrossSize];
			Vector3 a2;
			Vector3 a3;
			if (num + num2 > 1f)
			{
				Vector3 a = this.Vertex[vertexIndex + this.CrossSize + 1];
				a2 = a - vector3;
				a3 = a - vector2;
				return vector3 - a3 * (1f - num) + a2 * num2;
			}
			a2 = vector2 - vector;
			a3 = vector3 - vector;
			return vector + a3 * num + a2 * num2;
		}

		public Vector3 InterpolateVolumeDirection(float f, float crossF)
		{
			float num;
			float num2;
			int vertexIndex = this.GetVertexIndex(f, crossF, out num, out num2);
			if (num + num2 > 1f)
			{
				Vector3 b = this.Vertex[vertexIndex + 1];
				Vector3 a = this.Vertex[vertexIndex + this.CrossSize + 1];
				return (a - b).normalized;
			}
			Vector3 b2 = this.Vertex[vertexIndex];
			Vector3 a2 = this.Vertex[vertexIndex + this.CrossSize];
			return (a2 - b2).normalized;
		}

		public Vector3 InterpolateVolumeUp(float f, float crossF)
		{
			float num;
			float num2;
			int vertexIndex = this.GetVertexIndex(f, crossF, out num, out num2);
			Vector3 vector = this.Vertex[vertexIndex + 1];
			Vector3 vector2 = this.Vertex[vertexIndex + this.CrossSize];
			Vector3 rhs;
			Vector3 lhs;
			if (num + num2 > 1f)
			{
				Vector3 a = this.Vertex[vertexIndex + this.CrossSize + 1];
				rhs = a - vector2;
				lhs = a - vector;
			}
			else
			{
				Vector3 b = this.Vertex[vertexIndex];
				rhs = vector - b;
				lhs = vector2 - b;
			}
			return Vector3.Cross(lhs, rhs);
		}

		public float GetCrossLength(float pathF)
		{
			int num;
			int num2;
			float t;
			this.GetSegmentIndices(pathF, out num, out num2, out t);
			if (this.SegmentLength[num] == 0f)
			{
				this.SegmentLength[num] = this.calcSegmentLength(num);
			}
			if (this.SegmentLength[num2] == 0f)
			{
				this.SegmentLength[num2] = this.calcSegmentLength(num2);
			}
			return Mathf.LerpUnclamped(this.SegmentLength[num], this.SegmentLength[num2], t);
		}

		public float CrossFToDistance(float f, float crossF, CurvyClamping crossClamping = CurvyClamping.Clamp)
		{
			return this.GetCrossLength(f) * CurvyUtility.ClampTF(crossF, crossClamping);
		}

		public float CrossDistanceToF(float f, float distance, CurvyClamping crossClamping = CurvyClamping.Clamp)
		{
			float crossLength = this.GetCrossLength(f);
			return CurvyUtility.ClampDistance(distance, crossClamping, crossLength) / crossLength;
		}

		public void GetSegmentIndices(float pathF, out int s0Index, out int s1Index, out float frag)
		{
			s0Index = base.GetFIndex(Mathf.Repeat(pathF, 1f), out frag);
			s1Index = s0Index + 1;
		}

		public int GetSegmentIndex(int segment)
		{
			return segment * this.CrossSize;
		}

		public int GetCrossFIndex(float crossF, out float frag)
		{
			float num = crossF + this.CrossFShift;
			if (num != 1f)
			{
				return base.getGenericFIndex(ref this.CrossF, Mathf.Repeat(num, 1f), out frag);
			}
			return base.getGenericFIndex(ref this.CrossF, num, out frag);
		}

		public int GetVertexIndex(float pathF, out float pathFrag)
		{
			int findex = base.GetFIndex(pathF, out pathFrag);
			return findex * this.CrossSize;
		}

		public int GetVertexIndex(float pathF, float crossF, out float pathFrag, out float crossFrag)
		{
			int vertexIndex = this.GetVertexIndex(pathF, out pathFrag);
			int crossFIndex = this.GetCrossFIndex(crossF, out crossFrag);
			return vertexIndex + crossFIndex;
		}

		public Vector3[] GetSegmentVertices(params int[] segmentIndices)
		{
			Vector3[] array = new Vector3[this.CrossSize * segmentIndices.Length];
			for (int i = 0; i < segmentIndices.Length; i++)
			{
				Array.Copy(this.Vertex, segmentIndices[i] * this.CrossSize, array, i * this.CrossSize, this.CrossSize);
			}
			return array;
		}

		private float calcSegmentLength(int segmentIndex)
		{
			int num = segmentIndex * this.CrossSize;
			int num2 = num + this.CrossSize - 1;
			float num3 = 0f;
			for (int i = num; i < num2; i++)
			{
				num3 += (this.Vertex[i + 1] - this.Vertex[i]).magnitude;
			}
			return num3;
		}

		public Vector3[] Vertex = new Vector3[0];

		public Vector3[] VertexNormal = new Vector3[0];

		public float[] CrossF = new float[0];

		public float[] CrossMap = new float[0];

		public float[] SegmentLength;

		public bool CrossClosed;

		public bool CrossSeamless;

		public float CrossFShift;

		public SamplePointsMaterialGroupCollection CrossMaterialGroups;
	}
}
