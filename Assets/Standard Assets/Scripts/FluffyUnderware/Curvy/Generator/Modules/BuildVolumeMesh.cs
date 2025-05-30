// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.BuildVolumeMesh
using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy.Utils;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Build/Volume Mesh", ModuleName = "Volume Mesh", Description = "Build a volume mesh")]
	[HelpURL("https://curvyeditor.com/doclink/cgbuildvolumemesh")]
	public class BuildVolumeMesh : CGModule
	{
		public bool GenerateUV
		{
			get
			{
				return this.m_GenerateUV;
			}
			set
			{
				if (this.m_GenerateUV != value)
				{
					this.m_GenerateUV = value;
				}
				base.Dirty = true;
			}
		}

		public bool ReverseTriOrder
		{
			get
			{
				return this.m_ReverseTriOrder;
			}
			set
			{
				if (this.m_ReverseTriOrder != value)
				{
					this.m_ReverseTriOrder = value;
				}
				base.Dirty = true;
			}
		}

		public bool Split
		{
			get
			{
				return this.m_Split;
			}
			set
			{
				if (this.m_Split != value)
				{
					this.m_Split = value;
				}
				base.Dirty = true;
			}
		}

		public float SplitLength
		{
			get
			{
				return this.m_SplitLength;
			}
			set
			{
				float num = Mathf.Max(1f, value);
				if (this.m_SplitLength != num)
				{
					this.m_SplitLength = num;
				}
				base.Dirty = true;
			}
		}

		public List<CGMaterialSettingsEx> MaterialSetttings
		{
			get
			{
				return this.m_MaterialSettings;
			}
		}

		public int MaterialCount
		{
			get
			{
				return this.m_MaterialSettings.Count;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			if (this.MaterialCount == 0)
			{
				this.AddMaterial();
			}
		}

		public override void Reset()
		{
			base.Reset();
			this.GenerateUV = true;
			this.Split = false;
			this.SplitLength = 100f;
			this.ReverseTriOrder = false;
			this.m_MaterialSettings = new List<CGMaterialSettingsEx>(new CGMaterialSettingsEx[]
			{
				new CGMaterialSettingsEx()
			});
			this.m_Material = new Material[]
			{
				CurvyUtility.GetDefaultMaterial()
			};
		}

		public override void Refresh()
		{
			base.Refresh();
			CGVolume data = this.InVolume.GetData<CGVolume>(new CGDataRequestParameter[0]);
			if (data && data.Count > 0 && data.CrossSize > 0 && data.CrossMaterialGroups.Count > 0)
			{
				List<IntRegion> list = new List<IntRegion>();
				if (this.Split)
				{
					float num = 0f;
					int num2 = 0;
					for (int i = 0; i < data.Count; i++)
					{
						float num3 = data.FToDistance(data.F[i]);
						if (num3 - num >= this.SplitLength)
						{
							list.Add(new IntRegion(num2, i));
							num = num3;
							num2 = i;
						}
					}
					if (num2 < data.Count - 1)
					{
						list.Add(new IntRegion(num2, data.Count - 1));
					}
				}
				else
				{
					list.Add(new IntRegion(0, data.Count - 1));
				}
				CGVMesh[] allData = this.OutVMesh.GetAllData<CGVMesh>();
				Array.Resize<CGVMesh>(ref allData, list.Count);
				this.prepare(data);
				for (int j = 0; j < list.Count; j++)
				{
					allData[j] = CGVMesh.Get(allData[j], data, list[j], this.GenerateUV, this.ReverseTriOrder);
					this.build(allData[j], data, list[j]);
				}
				this.OutVMesh.SetData(allData);
			}
			else
			{
				this.OutVMesh.SetData(null);
			}
		}

		public int AddMaterial()
		{
			this.m_MaterialSettings.Add(new CGMaterialSettingsEx());
			this.m_Material = this.m_Material.Add(CurvyUtility.GetDefaultMaterial());
			base.Dirty = true;
			return this.MaterialCount;
		}

		public void RemoveMaterial(int index)
		{
			if (!this.validateMaterialIndex(index))
			{
				return;
			}
			this.m_MaterialSettings.RemoveAt(index);
			this.m_Material = this.m_Material.RemoveAt(index);
			base.Dirty = true;
		}

		public void SetMaterial(int index, Material mat)
		{
			if (!this.validateMaterialIndex(index) || mat == this.m_Material[index])
			{
				return;
			}
			if (this.m_Material[index] != mat)
			{
				this.m_Material[index] = mat;
				base.Dirty = true;
			}
		}

		public Material GetMaterial(int index)
		{
			if (!this.validateMaterialIndex(index))
			{
				return null;
			}
			return this.m_Material[index];
		}

		private void prepare(CGVolume vol)
		{
			this.groupsByMatID = this.getMaterialIDGroups(vol);
		}

		private void build(CGVMesh vmesh, CGVolume vol, IntRegion subset)
		{
			if (this.GenerateUV)
			{
				Array.Resize<Vector2>(ref vmesh.UV, vmesh.Count);
			}
			BuildVolumeMesh.prepareSubMeshes(vmesh, this.groupsByMatID, subset.Length, ref this.m_Material);
			int num = 0;
			int[] array = new int[this.groupsByMatID.Count];
			for (int i = subset.From; i < subset.To; i++)
			{
				for (int j = 0; j < this.groupsByMatID.Count; j++)
				{
					SamplePointsMaterialGroupCollection samplePointsMaterialGroupCollection = this.groupsByMatID[j];
					for (int k = 0; k < samplePointsMaterialGroupCollection.Count; k++)
					{
						SamplePointsMaterialGroup samplePointsMaterialGroup = samplePointsMaterialGroupCollection[k];
						if (this.GenerateUV)
						{
							this.createMaterialGroupUV(ref vmesh, ref vol, ref samplePointsMaterialGroup, samplePointsMaterialGroupCollection.MaterialID, samplePointsMaterialGroupCollection.AspectCorrection, i, num);
						}
						for (int l = 0; l < samplePointsMaterialGroup.Patches.Count; l++)
						{
							BuildVolumeMesh.createPatchTriangles(ref vmesh.SubMeshes[j].Triangles, ref array[j], num + samplePointsMaterialGroup.Patches[l].Start, samplePointsMaterialGroup.Patches[l].Count, vol.CrossSize, this.ReverseTriOrder);
						}
					}
				}
				num += vol.CrossSize;
			}
			if (this.GenerateUV)
			{
				for (int m = 0; m < this.groupsByMatID.Count; m++)
				{
					SamplePointsMaterialGroupCollection samplePointsMaterialGroupCollection = this.groupsByMatID[m];
					for (int n = 0; n < samplePointsMaterialGroupCollection.Count; n++)
					{
						SamplePointsMaterialGroup samplePointsMaterialGroup = samplePointsMaterialGroupCollection[n];
						this.createMaterialGroupUV(ref vmesh, ref vol, ref samplePointsMaterialGroup, samplePointsMaterialGroupCollection.MaterialID, samplePointsMaterialGroupCollection.AspectCorrection, subset.To, num);
					}
				}
			}
		}

		private static void prepareSubMeshes(CGVMesh vmesh, List<SamplePointsMaterialGroupCollection> groupsBySubMeshes, int extrusions, ref Material[] materials)
		{
			vmesh.SetSubMeshCount(groupsBySubMeshes.Count);
			for (int i = 0; i < groupsBySubMeshes.Count; i++)
			{
				CGVSubMesh data = vmesh.SubMeshes[i];
				vmesh.SubMeshes[i] = CGVSubMesh.Get(data, groupsBySubMeshes[i].TriangleCount * extrusions * 3, materials[Mathf.Min(groupsBySubMeshes[i].MaterialID, materials.Length - 1)]);
			}
		}

		private void createMaterialGroupUV(ref CGVMesh vmesh, ref CGVolume vol, ref SamplePointsMaterialGroup grp, int matIndex, float grpAspectCorrection, int sample, int baseVertex)
		{
			CGMaterialSettingsEx cgmaterialSettingsEx = this.m_MaterialSettings[matIndex];
			float num = cgmaterialSettingsEx.UVOffset.y + vol.F[sample] * cgmaterialSettingsEx.UVScale.y * grpAspectCorrection;
			int endVertex = grp.EndVertex;
			bool swapUV = cgmaterialSettingsEx.SwapUV;
			Vector2[] uv = vmesh.UV;
			for (int i = grp.StartVertex; i <= endVertex; i++)
			{
				float num2 = cgmaterialSettingsEx.UVOffset.x + vol.CrossMap[i] * cgmaterialSettingsEx.UVScale.x;
				uv[baseVertex + i].x = ((!swapUV) ? num2 : num);
				uv[baseVertex + i].y = ((!swapUV) ? num : num2);
			}
		}

		private static int createPatchTriangles(ref int[] triangles, ref int triIdx, int curVTIndex, int patchSize, int crossSize, bool reverse)
		{
			int num = (!reverse) ? 0 : 1;
			int num2 = 1 - num;
			int num3 = curVTIndex + crossSize;
			for (int i = 0; i < patchSize; i++)
			{
				triangles[triIdx + num] = curVTIndex + i;
				triangles[triIdx + num2] = num3 + i;
				triangles[triIdx + 2] = curVTIndex + i + 1;
				triangles[triIdx + num + 3] = curVTIndex + i + 1;
				triangles[triIdx + num2 + 3] = num3 + i;
				triangles[triIdx + 5] = num3 + i + 1;
				triIdx += 6;
			}
			return curVTIndex + patchSize + 1;
		}

		private List<SamplePointsMaterialGroupCollection> getMaterialIDGroups(CGVolume volume)
		{
			Dictionary<int, SamplePointsMaterialGroupCollection> dictionary = new Dictionary<int, SamplePointsMaterialGroupCollection>();
			for (int i = 0; i < volume.CrossMaterialGroups.Count; i++)
			{
				int num = Mathf.Min(volume.CrossMaterialGroups[i].MaterialID, this.MaterialCount - 1);
				SamplePointsMaterialGroupCollection samplePointsMaterialGroupCollection;
				if (!dictionary.TryGetValue(num, out samplePointsMaterialGroupCollection))
				{
					samplePointsMaterialGroupCollection = new SamplePointsMaterialGroupCollection();
					samplePointsMaterialGroupCollection.MaterialID = num;
					dictionary.Add(num, samplePointsMaterialGroupCollection);
				}
				samplePointsMaterialGroupCollection.Add(volume.CrossMaterialGroups[i]);
			}
			List<SamplePointsMaterialGroupCollection> list = new List<SamplePointsMaterialGroupCollection>();
			foreach (SamplePointsMaterialGroupCollection samplePointsMaterialGroupCollection2 in dictionary.Values)
			{
				samplePointsMaterialGroupCollection2.CalculateAspectCorrection(volume, this.MaterialSetttings[samplePointsMaterialGroupCollection2.MaterialID]);
				list.Add(samplePointsMaterialGroupCollection2);
			}
			return list;
		}

		private bool validateMaterialIndex(int index)
		{
			if (index < 0 || index >= this.m_MaterialSettings.Count)
			{
				UnityEngine.Debug.LogError("TriangulateTube: Invalid Material Index!");
				return false;
			}
			return true;
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGVolume)
		})]
		public CGModuleInputSlot InVolume = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGVMesh), Array = true)]
		public CGModuleOutputSlot OutVMesh = new CGModuleOutputSlot();

		[Tab("General")]
		[SerializeField]
		private bool m_GenerateUV = true;

		[SerializeField]
		private bool m_Split;

		[Positive(MinValue = 1f)]
		[FieldCondition("m_Split", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private float m_SplitLength = 100f;

		[FieldAction("CBAddMaterial", ActionAttribute.ActionEnum.Callback)]
		[SerializeField]
		[FormerlySerializedAs("m_ReverseNormals")]
		private bool m_ReverseTriOrder;

		[SerializeField]
		[HideInInspector]
		private List<CGMaterialSettingsEx> m_MaterialSettings = new List<CGMaterialSettingsEx>();

		[SerializeField]
		[HideInInspector]
		private Material[] m_Material = new Material[0];

		private List<SamplePointsMaterialGroupCollection> groupsByMatID;
	}
}
