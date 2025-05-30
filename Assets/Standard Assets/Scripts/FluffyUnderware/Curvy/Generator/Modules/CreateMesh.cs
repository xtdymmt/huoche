// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.CreateMesh
using System;
using System.Collections.Generic;
using System.Globalization;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.Rendering;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Create/Mesh", ModuleName = "Create Mesh")]
	[HelpURL("https://curvyeditor.com/doclink/cgcreatemesh")]
	public class CreateMesh : CGModule
	{
		public bool Combine
		{
			get
			{
				return this.m_Combine;
			}
			set
			{
				if (this.m_Combine != value)
				{
					this.m_Combine = value;
				}
				base.Dirty = true;
			}
		}

		public bool GroupMeshes
		{
			get
			{
				return this.m_GroupMeshes;
			}
			set
			{
				if (this.m_GroupMeshes != value)
				{
					this.m_GroupMeshes = value;
				}
				base.Dirty = true;
			}
		}

		public CGYesNoAuto AddNormals
		{
			get
			{
				return this.m_AddNormals;
			}
			set
			{
				if (this.m_AddNormals != value)
				{
					this.m_AddNormals = value;
				}
				base.Dirty = true;
			}
		}

		public CGYesNoAuto AddTangents
		{
			get
			{
				return this.m_AddTangents;
			}
			set
			{
				if (this.m_AddTangents != value)
				{
					this.m_AddTangents = value;
				}
				base.Dirty = true;
			}
		}

		public bool AddUV2
		{
			get
			{
				return this.m_AddUV2;
			}
			set
			{
				if (this.m_AddUV2 != value)
				{
					this.m_AddUV2 = value;
				}
				base.Dirty = true;
			}
		}

		public int Layer
		{
			get
			{
				return this.m_Layer;
			}
			set
			{
				int num = Mathf.Clamp(value, 0, 32);
				if (this.m_Layer != num)
				{
					this.m_Layer = num;
				}
				base.Dirty = true;
			}
		}

		public string Tag
		{
			get
			{
				return this.m_Tag;
			}
			set
			{
				if (this.m_Tag != value)
				{
					this.m_Tag = value;
				}
				base.Dirty = true;
			}
		}

		public bool MakeStatic
		{
			get
			{
				return this.m_MakeStatic;
			}
			set
			{
				if (this.m_MakeStatic != value)
				{
					this.m_MakeStatic = value;
				}
				base.Dirty = true;
			}
		}

		public bool RendererEnabled
		{
			get
			{
				return this.m_RendererEnabled;
			}
			set
			{
				if (this.m_RendererEnabled != value)
				{
					this.m_RendererEnabled = value;
				}
				base.Dirty = true;
			}
		}

		public ShadowCastingMode CastShadows
		{
			get
			{
				return this.m_CastShadows;
			}
			set
			{
				if (this.m_CastShadows != value)
				{
					this.m_CastShadows = value;
				}
				base.Dirty = true;
			}
		}

		public bool ReceiveShadows
		{
			get
			{
				return this.m_ReceiveShadows;
			}
			set
			{
				if (this.m_ReceiveShadows != value)
				{
					this.m_ReceiveShadows = value;
				}
				base.Dirty = true;
			}
		}

		public bool UseLightProbes
		{
			get
			{
				return this.m_UseLightProbes;
			}
			set
			{
				if (this.m_UseLightProbes != value)
				{
					this.m_UseLightProbes = value;
				}
				base.Dirty = true;
			}
		}

		public LightProbeUsage LightProbeUsage
		{
			get
			{
				return this.m_LightProbeUsage;
			}
			set
			{
				if (this.m_LightProbeUsage != value)
				{
					this.m_LightProbeUsage = value;
				}
				base.Dirty = true;
			}
		}

		public ReflectionProbeUsage ReflectionProbes
		{
			get
			{
				return this.m_ReflectionProbes;
			}
			set
			{
				if (this.m_ReflectionProbes != value)
				{
					this.m_ReflectionProbes = value;
				}
				base.Dirty = true;
			}
		}

		public Transform AnchorOverride
		{
			get
			{
				return this.m_AnchorOverride;
			}
			set
			{
				if (this.m_AnchorOverride != value)
				{
					this.m_AnchorOverride = value;
				}
				base.Dirty = true;
			}
		}

		public CGColliderEnum Collider
		{
			get
			{
				return this.m_Collider;
			}
			set
			{
				if (this.m_Collider != value)
				{
					this.m_Collider = value;
				}
				base.Dirty = true;
			}
		}

		public bool AutoUpdateColliders
		{
			get
			{
				return this.m_AutoUpdateColliders;
			}
			set
			{
				if (this.m_AutoUpdateColliders != value)
				{
					this.m_AutoUpdateColliders = value;
				}
				base.Dirty = true;
			}
		}

		public bool Convex
		{
			get
			{
				return this.m_Convex;
			}
			set
			{
				if (this.m_Convex != value)
				{
					this.m_Convex = value;
				}
				base.Dirty = true;
			}
		}

		public MeshColliderCookingOptions CookingOptions
		{
			get
			{
				return this.m_CookingOptions;
			}
			set
			{
				if (this.m_CookingOptions != value)
				{
					this.m_CookingOptions = value;
				}
				base.Dirty = true;
			}
		}

		public PhysicMaterial Material
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
				base.Dirty = true;
			}
		}

		public CGMeshResourceCollection Meshes
		{
			get
			{
				return this.m_MeshResources;
			}
		}

		public int MeshCount
		{
			get
			{
				return this.Meshes.Count;
			}
		}

		public int VertexCount { get; private set; }

		private bool canGroupMeshes
		{
			get
			{
				return this.InSpots.IsLinked && this.m_Combine;
			}
		}

		private bool canModifyStaticFlag
		{
			get
			{
				return false;
			}
		}

		private bool canUpdate
		{
			get
			{
				return !Application.isPlaying || !this.MakeStatic;
			}
		}

		public override void Reset()
		{
			base.Reset();
			this.Combine = false;
			this.GroupMeshes = true;
			this.AddNormals = CGYesNoAuto.Auto;
			this.AddTangents = CGYesNoAuto.No;
			this.MakeStatic = false;
			this.Material = null;
			this.Convex = false;
			this.Layer = 0;
			this.Tag = "Untagged";
			this.CastShadows = ShadowCastingMode.On;
			this.RendererEnabled = true;
			this.ReceiveShadows = true;
			this.UseLightProbes = true;
			this.LightProbeUsage = LightProbeUsage.BlendProbes;
			this.ReflectionProbes = ReflectionProbeUsage.BlendProbes;
			this.AnchorOverride = null;
			this.Collider = CGColliderEnum.Mesh;
			this.AutoUpdateColliders = true;
			this.Convex = false;
			this.Clear();
		}

		public override void OnTemplateCreated()
		{
			this.Clear();
		}

		public void Clear()
		{
			this.mCurrentMeshCount = 0;
			this.removeUnusedResource();
			Resources.UnloadUnusedAssets();
		}

		public override void OnStateChange()
		{
			base.OnStateChange();
			if (!this.IsConfigured)
			{
				this.Clear();
			}
		}

		public override void Refresh()
		{
			base.Refresh();
			if (this.canUpdate)
			{
				List<CGVMesh> allData = this.InVMeshArray.GetAllData<CGVMesh>(new CGDataRequestParameter[0]);
				CGSpots data = this.InSpots.GetData<CGSpots>(new CGDataRequestParameter[0]);
				this.mCurrentMeshCount = 0;
				this.VertexCount = 0;
				if (allData.Count > 0 && (!this.InSpots.IsLinked || (data != null && data.Count > 0)))
				{
					if (data != null && data.Count > 0)
					{
						this.createSpotMeshes(ref allData, ref data, this.Combine);
					}
					else
					{
						this.createMeshes(ref allData, this.Combine);
					}
				}
				this.removeUnusedResource();
				if (this.AutoUpdateColliders)
				{
					this.UpdateColliders();
				}
			}
			else
			{
				this.UIMessages.Add("In Play Mode, and when Make Static is enabled, mesh generation is stopped to avoid overriding the optimizations Unity do to static game objects'meshs.");
			}
		}

		public GameObject SaveToScene(Transform parent = null)
		{
			List<Component> list;
			List<string> list2;
			base.GetManagedResources(out list, out list2);
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count > 1)
			{
				Transform transform = new GameObject(base.ModuleName).transform;
				transform.transform.parent = ((!(parent == null)) ? parent : base.Generator.transform.parent);
				for (int i = 0; i < list.Count; i++)
				{
					MeshFilter component = list[i].GetComponent<MeshFilter>();
					GameObject gameObject = list[i].gameObject.DuplicateGameObject(transform.transform, false);
					gameObject.name = list[i].name;
					gameObject.GetComponent<CGMeshResource>().Destroy();
					gameObject.GetComponent<MeshFilter>().sharedMesh = UnityEngine.Object.Instantiate<Mesh>(component.sharedMesh);
				}
				return transform.gameObject;
			}
			MeshFilter component2 = list[0].GetComponent<MeshFilter>();
			GameObject gameObject2 = list[0].gameObject.DuplicateGameObject(parent, false);
			gameObject2.name = list[0].name;
			gameObject2.GetComponent<CGMeshResource>().Destroy();
			gameObject2.GetComponent<MeshFilter>().sharedMesh = UnityEngine.Object.Instantiate<Mesh>(component2.sharedMesh);
			return gameObject2;
		}

		public void UpdateColliders()
		{
			bool flag = true;
			for (int i = 0; i < this.m_MeshResources.Count; i++)
			{
				if (!this.m_MeshResources.Items[i].UpdateCollider(this.Collider, this.Convex, this.Material, this.CookingOptions))
				{
					flag = false;
				}
			}
			if (!flag)
			{
				this.UIMessages.Add("Error setting collider!");
			}
		}

		protected override bool UpgradeVersion(string oldVersion, string newVersion)
		{
			return true;
		}

		private void createMeshes(ref List<CGVMesh> vMeshes, bool combine)
		{
			if (combine && vMeshes.Count > 1)
			{
				int i = 0;
				while (i < vMeshes.Count)
				{
					int startIndex = i;
					int num = 0;
					while (i < vMeshes.Count && num + vMeshes[i].Count <= 65534)
					{
						num += vMeshes[i].Count;
						i++;
					}
					if (num == 0)
					{
						this.UIMessages.Add(string.Format(CultureInfo.InvariantCulture, "Mesh of index {0}, and subsequent ones, skipped because vertex count {2} > {1}", new object[]
						{
							i,
							65534,
							vMeshes[i].Count
						}));
						break;
					}
					CGVMesh cgvmesh = new CGVMesh();
					cgvmesh.MergeVMeshes(vMeshes, startIndex, i - 1);
					this.writeVMeshToMesh(ref cgvmesh);
				}
			}
			else
			{
				for (int j = 0; j < vMeshes.Count; j++)
				{
					CGVMesh cgvmesh2 = vMeshes[j];
					if (cgvmesh2.Count < 65534)
					{
						this.writeVMeshToMesh(ref cgvmesh2);
					}
					else
					{
						this.UIMessages.Add(string.Format(CultureInfo.InvariantCulture, "Mesh of index {0} skipped because vertex count {2} > {1}", new object[]
						{
							j,
							65534,
							cgvmesh2.Count
						}));
					}
				}
			}
		}

		private void createSpotMeshes(ref List<CGVMesh> vMeshes, ref CGSpots spots, bool combine)
		{
			int num = 0;
			int count = vMeshes.Count;
			if (combine)
			{
				List<CGSpot> list = new List<CGSpot>(spots.Points);
				if (this.GroupMeshes)
				{
					list.Sort((CGSpot a, CGSpot b) => a.Index.CompareTo(b.Index));
				}
				CGSpot cgspot = list[0];
				CGVMesh cgvmesh = new CGVMesh(vMeshes[cgspot.Index]);
				if (cgspot.Position != Vector3.zero || cgspot.Rotation != Quaternion.identity || cgspot.Scale != Vector3.one)
				{
					cgvmesh.TRS(cgspot.Matrix);
				}
				for (int i = 1; i < list.Count; i++)
				{
					cgspot = list[i];
					if (cgspot.Index > -1 && cgspot.Index < count)
					{
						if (cgvmesh.Count + vMeshes[cgspot.Index].Count > 65534 || (this.GroupMeshes && cgspot.Index != list[i - 1].Index))
						{
							this.writeVMeshToMesh(ref cgvmesh);
							cgvmesh = new CGVMesh(vMeshes[cgspot.Index]);
							if (!cgspot.Matrix.isIdentity)
							{
								cgvmesh.TRS(cgspot.Matrix);
							}
						}
						else if (!cgspot.Matrix.isIdentity)
						{
							cgvmesh.MergeVMesh(vMeshes[cgspot.Index], cgspot.Matrix);
						}
						else
						{
							cgvmesh.MergeVMesh(vMeshes[cgspot.Index]);
						}
					}
				}
				this.writeVMeshToMesh(ref cgvmesh);
			}
			else
			{
				for (int j = 0; j < spots.Count; j++)
				{
					CGSpot cgspot = spots.Points[j];
					if (cgspot.Index > -1 && cgspot.Index < count)
					{
						if (vMeshes[cgspot.Index].Count < 65535)
						{
							CGVMesh cgvmesh2 = vMeshes[cgspot.Index];
							CGMeshResource cgmeshResource = this.writeVMeshToMesh(ref cgvmesh2);
							if (cgspot.Position != Vector3.zero || cgspot.Rotation != Quaternion.identity || cgspot.Scale != Vector3.one)
							{
								cgspot.ToTransform(cgmeshResource.Filter.transform);
							}
						}
						else
						{
							num++;
						}
					}
				}
			}
			if (num > 0)
			{
				this.UIMessages.Add(string.Format(CultureInfo.InvariantCulture, "{0} meshes skipped (VertexCount>65534)", new object[]
				{
					num
				}));
			}
		}

		private CGMeshResource writeVMeshToMesh(ref CGVMesh vmesh)
		{
			bool flag = this.AddNormals != CGYesNoAuto.No;
			bool flag2 = this.AddTangents != CGYesNoAuto.No;
			CGMeshResource newMesh = this.getNewMesh();
			if (this.canModifyStaticFlag)
			{
				newMesh.Filter.gameObject.isStatic = false;
			}
			Mesh mesh = newMesh.Prepare();
			newMesh.gameObject.layer = this.Layer;
			newMesh.gameObject.tag = this.Tag;
			vmesh.ToMesh(ref mesh);
			this.VertexCount += vmesh.Count;
			if (this.AddUV2 && !vmesh.HasUV2)
			{
				mesh.uv2 = CGUtility.CalculateUV2(vmesh.UV);
			}
			if (flag && !vmesh.HasNormals)
			{
				mesh.RecalculateNormals();
			}
			if (flag2 && !vmesh.HasTangents)
			{
				newMesh.Filter.CalculateTangents();
			}
			newMesh.Filter.transform.localPosition = Vector3.zero;
			newMesh.Filter.transform.localRotation = Quaternion.identity;
			newMesh.Filter.transform.localScale = Vector3.one;
			if (this.canModifyStaticFlag)
			{
				newMesh.Filter.gameObject.isStatic = this.MakeStatic;
			}
			newMesh.Renderer.sharedMaterials = vmesh.GetMaterials();
			return newMesh;
		}

		private void removeUnusedResource()
		{
			for (int i = this.mCurrentMeshCount; i < this.Meshes.Count; i++)
			{
				base.DeleteManagedResource("Mesh", this.Meshes.Items[i], string.Empty, false);
			}
			this.Meshes.Items.RemoveRange(this.mCurrentMeshCount, this.Meshes.Count - this.mCurrentMeshCount);
		}

		private CGMeshResource getNewMesh()
		{
			CGMeshResource cgmeshResource;
			if (this.mCurrentMeshCount < this.Meshes.Count)
			{
				cgmeshResource = this.Meshes.Items[this.mCurrentMeshCount];
				if (cgmeshResource == null)
				{
					cgmeshResource = (CGMeshResource)base.AddManagedResource("Mesh", string.Empty, this.mCurrentMeshCount);
					this.Meshes.Items[this.mCurrentMeshCount] = cgmeshResource;
				}
			}
			else
			{
				cgmeshResource = (CGMeshResource)base.AddManagedResource("Mesh", string.Empty, this.mCurrentMeshCount);
				this.Meshes.Items.Add(cgmeshResource);
			}
			cgmeshResource.Renderer.shadowCastingMode = this.CastShadows;
			cgmeshResource.Renderer.enabled = this.RendererEnabled;
			cgmeshResource.Renderer.receiveShadows = this.ReceiveShadows;
			cgmeshResource.Renderer.lightProbeUsage = this.LightProbeUsage;
			cgmeshResource.Renderer.reflectionProbeUsage = this.ReflectionProbes;
			cgmeshResource.Renderer.probeAnchor = this.AnchorOverride;
			if (!cgmeshResource.ColliderMatches(this.Collider))
			{
				cgmeshResource.RemoveCollider();
			}
			this.mCurrentMeshCount++;
			return cgmeshResource;
		}

		private const string DefaultTag = "Untagged";

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGVMesh)
		}, Array = true, Name = "VMesh")]
		public CGModuleInputSlot InVMeshArray = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGSpots)
		}, Name = "Spots", Optional = true)]
		public CGModuleInputSlot InSpots = new CGModuleInputSlot();

		[SerializeField]
		[CGResourceCollectionManager("Mesh", ShowCount = true)]
		private CGMeshResourceCollection m_MeshResources = new CGMeshResourceCollection();

		[Tab("General")]
		[Tooltip("Merge meshes")]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		[SerializeField]
		private bool m_Combine;

		[Tooltip("Merge meshes sharing the same Index")]
		[SerializeField]
		private bool m_GroupMeshes = true;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private CGYesNoAuto m_AddNormals = CGYesNoAuto.Auto;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private CGYesNoAuto m_AddTangents = CGYesNoAuto.No;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_AddUV2 = true;

		[SerializeField]
		[Tooltip("If enabled, meshes will have the Static flag set, and will not be updated in Play Mode")]
		[FieldCondition("canModifyStaticFlag", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_MakeStatic;

		[SerializeField]
		[Tooltip("The Layer of the created game object")]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		[Layer("", "")]
		private int m_Layer;

		[SerializeField]
		[Tooltip("The Tag of the created game object")]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		[Tag("", "")]
		private string m_Tag = "Untagged";

		[Tab("Renderer")]
		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_RendererEnabled = true;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private ShadowCastingMode m_CastShadows = ShadowCastingMode.On;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_ReceiveShadows = true;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private LightProbeUsage m_LightProbeUsage = LightProbeUsage.BlendProbes;

		[HideInInspector]
		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_UseLightProbes = true;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private ReflectionProbeUsage m_ReflectionProbes = ReflectionProbeUsage.BlendProbes;

		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private Transform m_AnchorOverride;

		[Tab("Collider")]
		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private CGColliderEnum m_Collider = CGColliderEnum.Mesh;

		[FieldCondition("m_Collider", CGColliderEnum.Mesh, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private bool m_Convex;

		[Tooltip("Options used to enable or disable certain features in Collider mesh cooking. See Unity's MeshCollider.cookingOptions for more details")]
		[FieldCondition("m_Collider", CGColliderEnum.Mesh, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[EnumFlag("", "")]
		[FieldCondition("canUpdate", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		private MeshColliderCookingOptions m_CookingOptions = MeshColliderCookingOptions.CookForFasterSimulation | MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices;

		[Label("Auto Update", "")]
		[SerializeField]
		private bool m_AutoUpdateColliders = true;

		[SerializeField]
		private PhysicMaterial m_Material;

		private int mCurrentMeshCount;
	}
}
