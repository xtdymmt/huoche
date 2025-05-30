// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.TRSModuleBase
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public class TRSModuleBase : CGModule
	{
		public Vector3 Transpose
		{
			get
			{
				return this.m_Transpose;
			}
			set
			{
				if (this.m_Transpose != value)
				{
					this.m_Transpose = value;
				}
				base.Dirty = true;
			}
		}

		public Vector3 Rotation
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
				base.Dirty = true;
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
				base.Dirty = true;
			}
		}

		public Matrix4x4 Matrix
		{
			get
			{
				return Matrix4x4.TRS(this.Transpose, Quaternion.Euler(this.Rotation), this.Scale);
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.Properties.MinWidth = 200f;
			this.Properties.LabelWidth = 50f;
		}

		[SerializeField]
		[VectorEx("", "")]
		private Vector3 m_Transpose;

		[SerializeField]
		[VectorEx("", "")]
		private Vector3 m_Rotation;

		[SerializeField]
		[VectorEx("", "")]
		private Vector3 m_Scale = Vector3.one;
	}
}
