// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: IntRegion
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	[Serializable]
	public struct IntRegion
	{
		public IntRegion(int value)
		{
			this.From = value;
			this.To = value;
			this.SimpleValue = true;
		}

		public IntRegion(int A, int B)
		{
			this.From = A;
			this.To = B;
			this.SimpleValue = false;
		}

		public static IntRegion ZeroOne
		{
			get
			{
				return new IntRegion(0, 1);
			}
		}

		public void MakePositive()
		{
			if (this.To < this.From)
			{
				int to = this.To;
				this.To = this.From;
				this.From = to;
			}
		}

		public void Clamp(int low, int high)
		{
			this.Low = Mathf.Clamp(this.Low, low, high);
			this.High = Mathf.Clamp(this.High, low, high);
		}

		public bool Positive
		{
			get
			{
				return this.From <= this.To;
			}
		}

		public int Low
		{
			get
			{
				return (!this.Positive) ? this.To : this.From;
			}
			set
			{
				if (this.Positive)
				{
					this.From = value;
				}
				else
				{
					this.To = value;
				}
			}
		}

		public int High
		{
			get
			{
				return (!this.Positive) ? this.From : this.To;
			}
			set
			{
				if (this.Positive)
				{
					this.To = value;
				}
				else
				{
					this.From = value;
				}
			}
		}

		public int Random
		{
			get
			{
				return UnityEngine.Random.Range(this.From, this.To);
			}
		}

		public int Length
		{
			get
			{
				return this.To - this.From;
			}
		}

		public int LengthPositive
		{
			get
			{
				return (!this.Positive) ? (this.From - this.To) : (this.To - this.From);
			}
		}

		public override string ToString()
		{
			return string.Format("({0}-{1})", this.From, this.To);
		}

		public override int GetHashCode()
		{
			return this.From.GetHashCode() ^ this.To.GetHashCode() << 2;
		}

		public override bool Equals(object other)
		{
			if (!(other is IntRegion))
			{
				return false;
			}
			IntRegion intRegion = (IntRegion)other;
			return this.From.Equals(intRegion.From) && this.To.Equals(intRegion.To);
		}

		public static IntRegion operator +(IntRegion a, IntRegion b)
		{
			return new IntRegion(a.From + b.From, a.To + b.To);
		}

		public static IntRegion operator -(IntRegion a, IntRegion b)
		{
			return new IntRegion(a.From - b.From, a.To - b.To);
		}

		public static IntRegion operator -(IntRegion a)
		{
			return new IntRegion(-a.From, -a.To);
		}

		public static IntRegion operator *(IntRegion a, int v)
		{
			return new IntRegion(a.From * v, a.To * v);
		}

		public static IntRegion operator *(int v, IntRegion a)
		{
			return new IntRegion(a.From * v, a.To * v);
		}

		public static IntRegion operator /(IntRegion a, int v)
		{
			return new IntRegion(a.From / v, a.To / v);
		}

		public static bool operator ==(IntRegion lhs, IntRegion rhs)
		{
			return lhs.From == rhs.From && lhs.To == rhs.To && lhs.SimpleValue != rhs.SimpleValue;
		}

		public static bool operator !=(IntRegion lhs, IntRegion rhs)
		{
			return lhs.From != rhs.From || lhs.To != rhs.To || lhs.SimpleValue != rhs.SimpleValue;
		}

		public int From;

		public int To;

		public bool SimpleValue;
	}
}
