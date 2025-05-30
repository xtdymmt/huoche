// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.BuildVolumeCaps
using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy.ThirdParty.LibTessDotNet;
using FluffyUnderware.Curvy.Utils;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Build/Volume Caps", ModuleName = "Volume Caps", Description = "Build volume caps")]
	[HelpURL("https://curvyeditor.com/doclink/cgbuildvolumecaps")]
	public class BuildVolumeCaps : CGModule
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

		public CGYesNoAuto StartCap
		{
			get
			{
				return this.m_StartCap;
			}
			set
			{
				if (this.m_StartCap != value)
				{
					this.m_StartCap = value;
				}
				base.Dirty = true;
			}
		}

		public Material StartMaterial
		{
			get
			{
				return this.m_StartMaterial;
			}
			set
			{
				if (this.m_StartMaterial != value)
				{
					this.m_StartMaterial = value;
				}
				base.Dirty = true;
			}
		}

		public CGMaterialSettings StartMaterialSettings
		{
			get
			{
				return this.m_StartMaterialSettings;
			}
		}

		public CGYesNoAuto EndCap
		{
			get
			{
				return this.m_EndCap;
			}
			set
			{
				if (this.m_EndCap != value)
				{
					this.m_EndCap = value;
				}
				base.Dirty = true;
			}
		}

		public bool CloneStartCap
		{
			get
			{
				return this.m_CloneStartCap;
			}
			set
			{
				if (this.m_CloneStartCap != value)
				{
					this.m_CloneStartCap = value;
				}
				base.Dirty = true;
			}
		}

		public CGMaterialSettings EndMaterialSettings
		{
			get
			{
				return this.m_EndMaterialSettings;
			}
		}

		public Material EndMaterial
		{
			get
			{
				return this.m_EndMaterial;
			}
			set
			{
				if (this.m_EndMaterial != value)
				{
					this.m_EndMaterial = value;
				}
				base.Dirty = true;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			if (this.StartMaterial == null)
			{
				this.StartMaterial = CurvyUtility.GetDefaultMaterial();
			}
			if (this.EndMaterial == null)
			{
				this.EndMaterial = CurvyUtility.GetDefaultMaterial();
			}
		}

		public override void Reset()
		{
			base.Reset();
			this.StartCap = CGYesNoAuto.Auto;
			this.EndCap = CGYesNoAuto.Auto;
			this.ReverseTriOrder = false;
			this.GenerateUV = true;
			this.m_StartMaterialSettings = new CGMaterialSettings();
			this.m_EndMaterialSettings = new CGMaterialSettings();
		}

		public override void Refresh()
		{
			base.Refresh();
			CGVolume data = this.InVolume.GetData<CGVolume>(new CGDataRequestParameter[0]);
			List<CGVolume> allData = this.InVolumeHoles.GetAllData<CGVolume>(new CGDataRequestParameter[0]);
			if (data)
			{
				bool flag = this.StartCap == CGYesNoAuto.Yes || (this.StartCap == CGYesNoAuto.Auto && !data.Seamless);
				bool flag2 = this.EndCap == CGYesNoAuto.Yes || (this.EndCap == CGYesNoAuto.Auto && !data.Seamless);
				if (!flag && !flag2)
				{
					this.OutVMesh.SetData(null);
					return;
				}
				CGVMesh cgvmesh = new CGVMesh();
				Vector3[] array = new Vector3[0];
				Vector3[] array2 = new Vector3[0];
				cgvmesh.AddSubMesh(new CGVSubMesh((CGVSubMesh)null));
				CGVSubMesh cgvsubMesh = cgvmesh.SubMeshes[0];
				if (flag)
				{
					Tess tess = new Tess();
					tess.UsePooling = true;
					tess.AddContour(BuildVolumeCaps.make2DSegment(data, 0));
					for (int i = 0; i < allData.Count; i++)
					{
						if (allData[i].Count < 3)
						{
							this.OutVMesh.SetData(null);
							this.UIMessages.Add("Hole Cross has less than 3 Vertices: Can't create Caps!");
							return;
						}
						tess.AddContour(BuildVolumeCaps.make2DSegment(allData[i], 0));
					}
					tess.Tessellate(WindingRule.EvenOdd, ElementType.Polygons, 3);
					array = UnityLibTessUtility.FromContourVertex(tess.Vertices);
					int num = 0;
					Bounds bounds;
					cgvmesh.Vertex = BuildVolumeCaps.applyMatrix(array, BuildVolumeCaps.getMatrix(data, num, true), out bounds);
					Vector3[] array3 = new Vector3[cgvmesh.Vertex.Length];
					Vector3 vector = -data.Direction[num];
					for (int j = 0; j < array3.Length; j++)
					{
						array3[j] = vector;
					}
					cgvmesh.Normal = array3;
					cgvsubMesh.Material = this.StartMaterial;
					cgvsubMesh.Triangles = tess.Elements;
					if (this.ReverseTriOrder)
					{
						BuildVolumeCaps.flipTris(ref cgvsubMesh.Triangles, 0, cgvsubMesh.Triangles.Length);
					}
					if (this.GenerateUV)
					{
						cgvmesh.UV = new Vector2[array.Length];
						BuildVolumeCaps.applyUV(array, ref cgvmesh.UV, 0, array.Length, this.StartMaterialSettings, bounds);
					}
				}
				if (flag2)
				{
					Tess tess2 = new Tess();
					tess2.UsePooling = true;
					tess2.AddContour(BuildVolumeCaps.make2DSegment(data, 0));
					for (int k = 0; k < allData.Count; k++)
					{
						if (allData[k].Count < 3)
						{
							this.OutVMesh.SetData(null);
							this.UIMessages.Add("Hole Cross has <3 Vertices: Can't create Caps!");
							return;
						}
						tess2.AddContour(BuildVolumeCaps.make2DSegment(allData[k], 0));
					}
					tess2.Tessellate(WindingRule.EvenOdd, ElementType.Polygons, 3);
					array2 = UnityLibTessUtility.FromContourVertex(tess2.Vertices);
					int num2 = cgvmesh.Vertex.Length;
					int num3 = data.Count - 1;
					Bounds bounds2;
					cgvmesh.Vertex = cgvmesh.Vertex.AddRange(BuildVolumeCaps.applyMatrix(array2, BuildVolumeCaps.getMatrix(data, num3, true), out bounds2));
					Vector3[] array4 = new Vector3[num2];
					Vector3 vector2 = data.Direction[num3];
					for (int l = 0; l < array4.Length; l++)
					{
						array4[l] = vector2;
					}
					cgvmesh.Normal = cgvmesh.Normal.AddRange(array4);
					int[] elements = tess2.Elements;
					if (!this.ReverseTriOrder)
					{
						BuildVolumeCaps.flipTris(ref elements, 0, elements.Length);
					}
					for (int m = 0; m < elements.Length; m++)
					{
						elements[m] += num2;
					}
					if (!this.CloneStartCap && this.StartMaterial != this.EndMaterial)
					{
						cgvmesh.AddSubMesh(new CGVSubMesh(elements, this.EndMaterial));
					}
					else
					{
						cgvsubMesh.Material = this.StartMaterial;
						cgvsubMesh.Triangles = cgvsubMesh.Triangles.AddRange(elements);
					}
					if (this.GenerateUV)
					{
						Array.Resize<Vector2>(ref cgvmesh.UV, cgvmesh.UV.Length + array2.Length);
						BuildVolumeCaps.applyUV(array2, ref cgvmesh.UV, array.Length, array2.Length, (!this.CloneStartCap) ? this.EndMaterialSettings : this.StartMaterialSettings, bounds2);
					}
				}
				this.OutVMesh.SetData(new CGData[]
				{
					cgvmesh
				});
			}
		}

		private static Matrix4x4 getMatrix(CGVolume vol, int index, bool inverse)
		{
			if (inverse)
			{
				Quaternion q = Quaternion.LookRotation(vol.Direction[index], vol.Normal[index]);
				return Matrix4x4.TRS(vol.Position[index], q, Vector3.one);
			}
			Quaternion quaternion = Quaternion.Inverse(Quaternion.LookRotation(vol.Direction[index], vol.Normal[index]));
			return Matrix4x4.TRS(-(quaternion * vol.Position[index]), quaternion, Vector3.one);
		}

		private static void flipTris(ref int[] indices, int start, int end)
		{
			for (int i = start; i < end; i += 3)
			{
				int num = indices[i];
				indices[i] = indices[i + 2];
				indices[i + 2] = num;
			}
		}

		private static Vector3[] applyMatrix(Vector3[] vt, Matrix4x4 matrix, out Bounds bounds)
		{
			Vector3[] array = new Vector3[vt.Length];
			float num = float.MaxValue;
			float num2 = float.MaxValue;
			float num3 = float.MinValue;
			float num4 = float.MinValue;
			for (int i = 0; i < vt.Length; i++)
			{
				num = Mathf.Min(vt[i].x, num);
				num2 = Mathf.Min(vt[i].y, num2);
				num3 = Mathf.Max(vt[i].x, num3);
				num4 = Mathf.Max(vt[i].y, num4);
				array[i] = matrix.MultiplyPoint(vt[i]);
			}
			Vector3 size = new Vector3(Mathf.Abs(num3 - num), Mathf.Abs(num4 - num2));
			bounds = new Bounds(new Vector3(num + size.x / 2f, num2 + size.y / 2f, 0f), size);
			return array;
		}

		private static ContourVertex[] make2DSegment(CGVolume vol, int index)
		{
			Matrix4x4 matrix = BuildVolumeCaps.getMatrix(vol, index, false);
			int segmentIndex = vol.GetSegmentIndex(index);
			ContourVertex[] array = new ContourVertex[vol.CrossSize];
			for (int i = 0; i < vol.CrossSize; i++)
			{
				array[i] = matrix.MultiplyPoint(vol.Vertex[segmentIndex + i]).ContourVertex();
			}
			return array;
		}

		private static void applyUV(Vector3[] vts, ref Vector2[] uvArray, int index, int count, CGMaterialSettings mat, Bounds bounds)
		{
			float x = bounds.size.x;
			float y = bounds.size.y;
			float x2 = bounds.min.x;
			float y2 = bounds.min.y;
			float num = mat.UVScale.x;
			float num2 = mat.UVScale.y;
			CGKeepAspectMode keepAspect = mat.KeepAspect;
			if (keepAspect != CGKeepAspectMode.ScaleU)
			{
				if (keepAspect == CGKeepAspectMode.ScaleV)
				{
					float num3 = x * mat.UVScale.x;
					float num4 = y * mat.UVScale.y;
					num2 *= num4 / num3;
				}
			}
			else
			{
				float num5 = x * mat.UVScale.x;
				float num6 = y * mat.UVScale.y;
				num *= num5 / num6;
			}
			bool swapUV = mat.SwapUV;
			if (mat.UVRotation != 0f)
			{
				float f = mat.UVRotation * 0.0174532924f;
				float num7 = Mathf.Sin(f);
				float num8 = Mathf.Cos(f);
				float num9 = num * 0.5f;
				float num10 = num2 * 0.5f;
				for (int i = 0; i < count; i++)
				{
					float num11 = (vts[i].x - x2) / x * num;
					float num12 = (vts[i].y - y2) / y * num2;
					float num13 = num11 - num9;
					float num14 = num12 - num10;
					num11 = num8 * num13 - num7 * num14 + num9 + mat.UVOffset.x;
					num12 = num7 * num13 + num8 * num14 + num10 + mat.UVOffset.y;
					int num15 = i + index;
					uvArray[num15].x = ((!swapUV) ? num11 : num12);
					uvArray[num15].y = ((!swapUV) ? num12 : num11);
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					float num11 = mat.UVOffset.x + (vts[j].x - x2) / x * num;
					float num12 = mat.UVOffset.y + (vts[j].y - y2) / y * num2;
					int num16 = j + index;
					uvArray[num16].x = ((!swapUV) ? num11 : num12);
					uvArray[num16].y = ((!swapUV) ? num12 : num11);
				}
			}
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGVolume)
		})]
		public CGModuleInputSlot InVolume = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGVolume)
		}, Optional = true, Array = true)]
		public CGModuleInputSlot InVolumeHoles = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGVMesh), Array = true)]
		public CGModuleOutputSlot OutVMesh = new CGModuleOutputSlot();

		[Tab("General")]
		[SerializeField]
		private CGYesNoAuto m_StartCap = CGYesNoAuto.Auto;

		[SerializeField]
		private CGYesNoAuto m_EndCap = CGYesNoAuto.Auto;

		[SerializeField]
		[FormerlySerializedAs("m_ReverseNormals")]
		private bool m_ReverseTriOrder;

		[SerializeField]
		private bool m_GenerateUV = true;

		[Tab("Start Cap")]
		[Inline]
		[SerializeField]
		private CGMaterialSettings m_StartMaterialSettings = new CGMaterialSettings();

		[Label("Material", "")]
		[SerializeField]
		private Material m_StartMaterial;

		[Tab("End Cap")]
		[SerializeField]
		private bool m_CloneStartCap = true;

		[AsGroup(null, Invisible = true)]
		[GroupCondition("m_CloneStartCap", false, false)]
		[SerializeField]
		private CGMaterialSettings m_EndMaterialSettings = new CGMaterialSettings();

		[Group("Default/End Cap")]
		[Label("Material", "")]
		[FieldCondition("m_CloneStartCap", false, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private Material m_EndMaterial;
	}
}
