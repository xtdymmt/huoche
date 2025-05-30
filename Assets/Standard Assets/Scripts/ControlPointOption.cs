// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlPointOption
using System;

namespace FluffyUnderware.Curvy.Generator
{
	public struct ControlPointOption : IEquatable<ControlPointOption>
	{
		public ControlPointOption(float tf, float dist, bool includeAnyways, int materialID, bool hardEdge, float maxStepDistance, bool uvEdge, bool uvShift, float firstU, float secondU)
		{
			this.TF = tf;
			this.Distance = dist;
			this.Include = includeAnyways;
			this.MaterialID = materialID;
			this.HardEdge = hardEdge;
			if (maxStepDistance == 0f)
			{
				this.MaxStepDistance = float.MaxValue;
			}
			else
			{
				this.MaxStepDistance = maxStepDistance;
			}
			this.UVEdge = uvEdge;
			this.UVShift = uvShift;
			this.FirstU = firstU;
			this.SecondU = secondU;
		}

		public bool Equals(ControlPointOption other)
		{
			return this.TF.Equals(other.TF) && this.Distance.Equals(other.Distance) && this.Include == other.Include && this.MaterialID == other.MaterialID && this.HardEdge == other.HardEdge && this.MaxStepDistance.Equals(other.MaxStepDistance) && this.UVEdge == other.UVEdge && this.UVShift == other.UVShift && this.FirstU.Equals(other.FirstU) && this.SecondU.Equals(other.SecondU);
		}

		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && obj is ControlPointOption && this.Equals((ControlPointOption)obj);
		}

		public override int GetHashCode()
		{
			int num = this.TF.GetHashCode();
			num = (num * 397 ^ this.Distance.GetHashCode());
			num = (num * 397 ^ this.Include.GetHashCode());
			num = (num * 397 ^ this.MaterialID);
			num = (num * 397 ^ this.HardEdge.GetHashCode());
			num = (num * 397 ^ this.MaxStepDistance.GetHashCode());
			num = (num * 397 ^ this.UVEdge.GetHashCode());
			num = (num * 397 ^ this.UVShift.GetHashCode());
			num = (num * 397 ^ this.FirstU.GetHashCode());
			return num * 397 ^ this.SecondU.GetHashCode();
		}

		public static bool operator ==(ControlPointOption left, ControlPointOption right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(ControlPointOption left, ControlPointOption right)
		{
			return !left.Equals(right);
		}

		public float TF;

		public float Distance;

		public bool Include;

		public int MaterialID;

		public bool HardEdge;

		public float MaxStepDistance;

		public bool UVEdge;

		public bool UVShift;

		public float FirstU;

		public float SecondU;
	}
}
