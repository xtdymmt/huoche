// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: CGSpot
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public struct CGSpot : IEquatable<CGSpot>
	{
		public CGSpot(int index)
		{
			this = new CGSpot(index, Vector3.zero, Quaternion.identity, Vector3.one);
		}

		public CGSpot(int index, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			this.m_Index = index;
			this.m_Position = position;
			this.m_Rotation = rotation;
			this.m_Scale = scale;
		}

		public int Index
		{
			get
			{
				return this.m_Index;
			}
		}

		public Vector3 Position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				if (this.m_Position != value)
				{
					this.m_Position = value;
				}
			}
		}

		public Quaternion Rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				if (this.m_Rotation != value)
				{
					this.m_Rotation = value;
				}
			}
		}

		public Vector3 Scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				if (this.m_Scale != value)
				{
					this.m_Scale = value;
				}
			}
		}

		public Matrix4x4 Matrix
		{
			get
			{
				return Matrix4x4.TRS(this.m_Position, this.m_Rotation, this.m_Scale);
			}
		}

		public void ToTransform(Transform transform)
		{
			transform.localPosition = this.Position;
			transform.localRotation = this.Rotation;
			transform.localScale = this.Scale;
		}

		public bool Equals(CGSpot other)
		{
			return this.m_Index == other.m_Index && this.m_Position.Equals(other.m_Position) && this.m_Rotation.Equals(other.m_Rotation) && this.m_Scale.Equals(other.m_Scale);
		}

		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && obj is CGSpot && this.Equals((CGSpot)obj);
		}

		public override int GetHashCode()
		{
			int num = this.m_Index;
			num = (num * 397 ^ this.m_Position.GetHashCode());
			num = (num * 397 ^ this.m_Rotation.GetHashCode());
			return num * 397 ^ this.m_Scale.GetHashCode();
		}

		public static bool operator ==(CGSpot left, CGSpot right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(CGSpot left, CGSpot right)
		{
			return !left.Equals(right);
		}

		[SerializeField]
		[Label("Idx", "")]
		private int m_Index;

		[SerializeField]
		[VectorEx("Pos", "", Options = AttributeOptionsFlags.Compact, Precision = 4)]
		private Vector3 m_Position;

		[SerializeField]
		[VectorEx("Rot", "", Options = AttributeOptionsFlags.Compact, Precision = 4)]
		private Quaternion m_Rotation;

		[SerializeField]
		[VectorEx("Scl", "", Options = AttributeOptionsFlags.Compact, Precision = 4)]
		private Vector3 m_Scale;
	}
}
