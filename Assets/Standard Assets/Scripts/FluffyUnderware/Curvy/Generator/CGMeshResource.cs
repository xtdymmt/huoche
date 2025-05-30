// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGMeshResource
using System;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class CGMeshResource : DuplicateEditorMesh, IPoolable
	{
		public MeshRenderer Renderer
		{
			get
			{
				if (this.mRenderer == null)
				{
					this.mRenderer = base.GetComponent<MeshRenderer>();
				}
				return this.mRenderer;
			}
		}

		public Collider Collider
		{
			get
			{
				if (this.mCollider == null)
				{
					this.mCollider = base.GetComponent<Collider>();
				}
				return this.mCollider;
			}
		}

		public Mesh Prepare()
		{
			return base.Filter.PrepareNewShared("Mesh");
		}

		public bool ColliderMatches(CGColliderEnum type)
		{
			return (this.Collider == null && type == CGColliderEnum.None) || (this.Collider is MeshCollider && type == CGColliderEnum.Mesh) || (this.Collider is BoxCollider && type == CGColliderEnum.Box) || (this.Collider is SphereCollider && type == CGColliderEnum.Sphere);
		}

		public void RemoveCollider()
		{
			if (this.Collider)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(this.mCollider);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this.mCollider);
				}
				this.mCollider = null;
			}
		}

		public bool UpdateCollider(CGColliderEnum mode, bool convex, PhysicMaterial material, MeshColliderCookingOptions meshCookingOptions = MeshColliderCookingOptions.CookForFasterSimulation | MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices)
		{
			if (this.Collider == null)
			{
				switch (mode)
				{
				case CGColliderEnum.None:
					break;
				case CGColliderEnum.Mesh:
					this.mCollider = base.gameObject.AddComponent<MeshCollider>();
					break;
				case CGColliderEnum.Box:
					this.mCollider = base.gameObject.AddComponent<BoxCollider>();
					break;
				case CGColliderEnum.Sphere:
					this.mCollider = base.gameObject.AddComponent<SphereCollider>();
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
			if (mode != CGColliderEnum.None)
			{
				switch (mode)
				{
				case CGColliderEnum.Mesh:
				{
					MeshCollider meshCollider = this.Collider as MeshCollider;
					if (meshCollider != null)
					{
						meshCollider.sharedMesh = null;
						meshCollider.convex = convex;
						meshCollider.cookingOptions = meshCookingOptions;
						try
						{
							meshCollider.sharedMesh = base.Filter.sharedMesh;
						}
						catch
						{
							return false;
						}
						break;
					}
					DTLog.LogError("[Curvy] Collider of wrong type");
					break;
				}
				case CGColliderEnum.Box:
				{
					BoxCollider boxCollider = this.Collider as BoxCollider;
					if (boxCollider != null)
					{
						boxCollider.center = base.Filter.sharedMesh.bounds.center;
						boxCollider.size = base.Filter.sharedMesh.bounds.size;
					}
					else
					{
						DTLog.LogError("[Curvy] Collider of wrong type");
					}
					break;
				}
				case CGColliderEnum.Sphere:
				{
					SphereCollider sphereCollider = this.Collider as SphereCollider;
					if (sphereCollider != null)
					{
						sphereCollider.center = base.Filter.sharedMesh.bounds.center;
						sphereCollider.radius = base.Filter.sharedMesh.bounds.extents.magnitude;
					}
					else
					{
						DTLog.LogError("[Curvy] Collider of wrong type");
					}
					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
				}
				this.Collider.material = material;
			}
			return true;
		}

		public void OnBeforePush()
		{
		}

		public void OnAfterPop()
		{
		}

		private MeshRenderer mRenderer;

		private Collider mCollider;
	}
}
