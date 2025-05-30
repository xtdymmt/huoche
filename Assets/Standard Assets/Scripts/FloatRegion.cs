// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FloatRegion
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	[Serializable]
	public struct FloatRegion
	{
		public FloatRegion(float value)
		{
			this.From = value;
			this.To = value;
			this.SimpleValue = true;
		}

		public FloatRegion(float A, float B)
		{
			this.From = A;
			this.To = B;
			this.SimpleValue = false;
		}

		public static FloatRegion ZeroOne
		{
			get
			{
				return new FloatRegion(0f, 1f);
			}
		}

		public void MakePositive()
		{
			if (this.To < this.From)
			{
				float to = this.To;
				this.To = this.From;
				this.From = to;
			}
		}

		public void Clamp(float low, float high)
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

		public float Low
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

		public float High
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

		public float Random
		{
			get
			{
				return UnityEngine.Random.Range(this.From, this.To);
			}
		}

		public float Next
		{
			get
			{
				if (this.SimpleValue)
				{
					return this.From;
				}
				return this.Random;
			}
		}

		public float Length
		{
			get
			{
				return this.To - this.From;
			}
		}

		public float LengthPositive
		{
			get
			{
				return (!this.Positive) ? (this.From - this.To) : (this.To - this.From);
			}
		}

		public override string ToString()
		{
			return string.Format("({0:F1}-{1:F1})", this.From, this.To);
		}

		public override int GetHashCode()
		{
			return this.From.GetHashCode() ^ this.To.GetHashCode() << 2;
		}

		public override bool Equals(object other)
		{
			if (!(other is FloatRegion))
			{
				return false;
			}
			FloatRegion floatRegion = (FloatRegion)other;
			return this.From.Equals(floatRegion.From) && this.To.Equals(floatRegion.To);
		}

		public static FloatRegion operator +(FloatRegion a, FloatRegion b)
		{
			return new FloatRegion(a.From + b.From, a.To + b.To);
		}

		public static FloatRegion operator -(FloatRegion a, FloatRegion b)
		{
			return new FloatRegion(a.From - b.From, a.To - b.To);
		}

		public static FloatRegion operator -(FloatRegion a)
		{
			return new FloatRegion(-a.From, -a.To);
		}

		public static FloatRegion operator *(FloatRegion a, float v)
		{
			return new FloatRegion(a.From * v, a.To * v);
		}

		public static FloatRegion operator *(float v, FloatRegion a)
		{
			return new FloatRegion(a.From * v, a.To * v);
		}

		public static FloatRegion operator /(FloatRegion a, float v)
		{
			return new FloatRegion(a.From / v, a.To / v);
		}

		public static bool operator ==(FloatRegion lhs, FloatRegion rhs)
		{
			return lhs.SimpleValue == rhs.SimpleValue && Mathf.Approximately(lhs.From, rhs.From) && Mathf.Approximately(lhs.To, rhs.To);
		}

		public static bool operator !=(FloatRegion lhs, FloatRegion rhs)
		{
			return lhs.SimpleValue != rhs.SimpleValue || !Mathf.Approximately(lhs.From, rhs.From) || !Mathf.Approximately(lhs.To, rhs.To);
		}

		public float From;

		public float To;

		public bool SimpleValue;
	}
}
