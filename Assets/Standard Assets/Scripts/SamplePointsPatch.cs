// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: SamplePointsPatch
using System;
using System.Globalization;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public struct SamplePointsPatch : IEquatable<SamplePointsPatch>
	{
		public SamplePointsPatch(int start)
		{
			this.Start = start;
			this.Count = 0;
		}

		public int End
		{
			get
			{
				return this.Start + this.Count;
			}
			set
			{
				this.Count = Mathf.Max(0, value - this.Start);
			}
		}

		public int TriangleCount
		{
			get
			{
				return this.Count * 2;
			}
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Size={0} ({1}-{2}, {3} Tris)", new object[]
			{
				this.Count,
				this.Start,
				this.End,
				this.TriangleCount
			});
		}

		public bool Equals(SamplePointsPatch other)
		{
			return this.Start == other.Start && this.Count == other.Count;
		}

		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && obj is SamplePointsPatch && this.Equals((SamplePointsPatch)obj);
		}

		public override int GetHashCode()
		{
			return this.Start * 397 ^ this.Count;
		}

		public static bool operator ==(SamplePointsPatch left, SamplePointsPatch right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(SamplePointsPatch left, SamplePointsPatch right)
		{
			return !left.Equals(right);
		}

		public int Start;

		public int Count;
	}
}
