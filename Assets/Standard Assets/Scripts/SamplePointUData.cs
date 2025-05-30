// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: SamplePointUData
using System;
using System.Globalization;

namespace FluffyUnderware.Curvy.Generator
{
	public struct SamplePointUData : IEquatable<SamplePointUData>
	{
		public SamplePointUData(int vt, bool uvEdge, float uv0, float uv1)
		{
			this.Vertex = vt;
			this.UVEdge = uvEdge;
			this.FirstU = uv0;
			this.SecondU = uv1;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SamplePointUData (Vertex={0},Edge={1},FirstU={2},SecondU={3}", new object[]
			{
				this.Vertex,
				this.UVEdge,
				this.FirstU,
				this.SecondU
			});
		}

		public bool Equals(SamplePointUData other)
		{
			return this.Vertex == other.Vertex && this.UVEdge == other.UVEdge && this.FirstU.Equals(other.FirstU) && this.SecondU.Equals(other.SecondU);
		}

		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && obj is SamplePointUData && this.Equals((SamplePointUData)obj);
		}

		public override int GetHashCode()
		{
			int num = this.Vertex;
			num = (num * 397 ^ this.UVEdge.GetHashCode());
			num = (num * 397 ^ this.FirstU.GetHashCode());
			return num * 397 ^ this.SecondU.GetHashCode();
		}

		public static bool operator ==(SamplePointUData left, SamplePointUData right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(SamplePointUData left, SamplePointUData right)
		{
			return !left.Equals(right);
		}

		public int Vertex;

		public bool UVEdge;

		public float FirstU;

		public float SecondU;
	}
}
