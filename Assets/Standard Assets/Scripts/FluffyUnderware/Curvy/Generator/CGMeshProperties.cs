// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGMeshProperties
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGMeshProperties
	{
		public CGMeshProperties()
		{
		}

		public CGMeshProperties(Mesh mesh)
		{
			this.Mesh = mesh;
			this.Material = ((!(mesh != null)) ? new Material[0] : new Material[mesh.subMeshCount]);
		}

		public Mesh Mesh
		{
			get
			{
				return this.m_Mesh;
			}
			set
			{
				if (this.m_Mesh != value)
				{
					this.m_Mesh = value;
				}
				if (this.m_Mesh && this.m_Mesh.subMeshCount != this.m_Material.Length)
				{
					Array.Resize<Material>(ref this.m_Material, this.m_Mesh.subMeshCount);
				}
			}
		}

		public Material[] Material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				if (this.m_Material != value)
				{
					this.m_Material = value;
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
		private Mesh m_Mesh;

		[SerializeField]
		private Material[] m_Material = new Material[0];

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
