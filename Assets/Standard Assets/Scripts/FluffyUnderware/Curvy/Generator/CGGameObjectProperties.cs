// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGGameObjectProperties
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGGameObjectProperties
	{
		public CGGameObjectProperties()
		{
		}

		public CGGameObjectProperties(GameObject gameObject)
		{
			this.Object = gameObject;
		}

		public GameObject Object
		{
			get
			{
				return this.m_Object;
			}
			set
			{
				if (this.m_Object != value)
				{
					this.m_Object = value;
				}
			}
		}

		public Vector3 Translation
		{
			get
			{
				return this.m_Translation;
			}
			set
			{
				if (this.m_Translation != value)
				{
					this.m_Translation = value;
				}
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
				return Matrix4x4.TRS(this.Translation, Quaternion.Euler(this.Rotation), this.Scale);
			}
		}

		[SerializeField]
		private GameObject m_Object;

		[SerializeField]
		[VectorEx("", "")]
		private Vector3 m_Translation;

		[SerializeField]
		[VectorEx("", "")]
		private Vector3 m_Rotation;

		[SerializeField]
		[VectorEx("", "")]
		private Vector3 m_Scale = Vector3.one;
	}
}
