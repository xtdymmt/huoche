// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGVMesh
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[CGDataInfo(0.98f, 0.5f, 0f, 1f)]
	public class CGVMesh : CGBounds
	{
		public CGVMesh() : this(0, false, false, false, false)
		{
		}

		public CGVMesh(int vertexCount, bool addUV = false, bool addUV2 = false, bool addNormals = false, bool addTangents = false)
		{
			this.Vertex = new Vector3[vertexCount];
			this.UV = ((!addUV) ? new Vector2[0] : new Vector2[vertexCount]);
			this.UV2 = ((!addUV2) ? new Vector2[0] : new Vector2[vertexCount]);
			this.Normal = ((!addNormals) ? new Vector3[0] : new Vector3[vertexCount]);
			this.Tangents = ((!addTangents) ? new Vector4[0] : new Vector4[vertexCount]);
			this.SubMeshes = new CGVSubMesh[0];
		}

		public CGVMesh(CGVolume volume) : this(volume.Vertex.Length, false, false, false, false)
		{
			Array.Copy(volume.Vertex, this.Vertex, volume.Vertex.Length);
		}

		public CGVMesh(CGVolume volume, IntRegion subset) : this((subset.LengthPositive + 1) * volume.CrossSize, false, false, true, false)
		{
			int sourceIndex = subset.Low * volume.CrossSize;
			Array.Copy(volume.Vertex, sourceIndex, this.Vertex, 0, this.Vertex.Length);
			Array.Copy(volume.VertexNormal, sourceIndex, this.Normal, 0, this.Normal.Length);
		}

		public CGVMesh(CGVMesh source) : base(source)
		{
			this.Vertex = (Vector3[])source.Vertex.Clone();
			this.UV = (Vector2[])source.UV.Clone();
			this.UV2 = (Vector2[])source.UV2.Clone();
			this.Normal = (Vector3[])source.Normal.Clone();
			this.Tangents = (Vector4[])source.Tangents.Clone();
			this.SubMeshes = new CGVSubMesh[source.SubMeshes.Length];
			for (int i = 0; i < source.SubMeshes.Length; i++)
			{
				this.SubMeshes[i] = new CGVSubMesh(source.SubMeshes[i]);
			}
		}

		public CGVMesh(CGMeshProperties meshProperties) : this(meshProperties.Mesh, meshProperties.Material, meshProperties.Matrix)
		{
		}

		public CGVMesh(Mesh source, Material[] materials, Matrix4x4 trsMatrix)
		{
			this.Name = source.name;
			this.Vertex = (Vector3[])source.vertices.Clone();
			this.Normal = (Vector3[])source.normals.Clone();
			this.Tangents = (Vector4[])source.tangents.Clone();
			this.UV = (Vector2[])source.uv.Clone();
			this.UV2 = (Vector2[])source.uv2.Clone();
			this.SubMeshes = new CGVSubMesh[source.subMeshCount];
			for (int i = 0; i < source.subMeshCount; i++)
			{
				this.SubMeshes[i] = new CGVSubMesh(source.GetTriangles(i), (materials.Length <= i) ? null : materials[i]);
			}
			base.Bounds = source.bounds;
			if (!trsMatrix.isIdentity)
			{
				this.TRS(trsMatrix);
			}
		}

		public override int Count
		{
			get
			{
				return this.Vertex.Length;
			}
		}

		public bool HasUV
		{
			get
			{
				return this.UV.Length > 0;
			}
		}

		public bool HasUV2
		{
			get
			{
				return this.UV2.Length > 0;
			}
		}

		public bool HasNormals
		{
			get
			{
				return this.Normal.Length > 0;
			}
		}

		public bool HasTangents
		{
			get
			{
				return this.Tangents.Length > 0;
			}
		}

		public int TriangleCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.SubMeshes.Length; i++)
				{
					num += this.SubMeshes[i].Triangles.Length;
				}
				return num / 3;
			}
		}

		public override T Clone<T>()
		{
			return new CGVMesh(this) as T;
		}

		public static CGVMesh Get(CGVMesh data, CGVolume source, bool addUV, bool reverseNormals)
		{
			return CGVMesh.Get(data, source, new IntRegion(0, source.Count - 1), addUV, reverseNormals);
		}

		public static CGVMesh Get(CGVMesh data, CGVolume source, IntRegion subset, bool addUV, bool reverseNormals)
		{
			int sourceIndex = subset.Low * source.CrossSize;
			int num = (subset.LengthPositive + 1) * source.CrossSize;
			if (data == null)
			{
				data = new CGVMesh(num, addUV, false, true, false);
			}
			else
			{
				if (data.Vertex.Length != num)
				{
					data.Vertex = new Vector3[num];
				}
				if (data.Normal.Length != num)
				{
					data.Normal = new Vector3[num];
				}
				int num2 = (!addUV) ? 0 : source.Vertex.Length;
				if (data.UV.Length != num2)
				{
					data.UV = new Vector2[num2];
				}
				if (data.UV2.Length != 0)
				{
					data.UV2 = new Vector2[0];
				}
				if (data.Tangents.Length != 0)
				{
					data.Tangents = new Vector4[0];
				}
			}
			Array.Copy(source.Vertex, sourceIndex, data.Vertex, 0, num);
			Array.Copy(source.VertexNormal, sourceIndex, data.Normal, 0, num);
			if (reverseNormals)
			{
				for (int i = 0; i < data.Normal.Length; i++)
				{
					data.Normal[i].x = -data.Normal[i].x;
					data.Normal[i].y = -data.Normal[i].y;
					data.Normal[i].z = -data.Normal[i].z;
				}
			}
			return data;
		}

		public void SetSubMeshCount(int count)
		{
			Array.Resize<CGVSubMesh>(ref this.SubMeshes, count);
		}

		public void AddSubMesh(CGVSubMesh submesh = null)
		{
			this.SubMeshes = this.SubMeshes.Add(submesh);
		}

		public void MergeVMesh(CGVMesh source)
		{
			int count = this.Count;
			this.copyData<Vector3>(ref source.Vertex, ref this.Vertex, count, source.Count);
			this.MergeUVsNormalsAndTangents(source, count);
			for (int i = 0; i < source.SubMeshes.Length; i++)
			{
				this.GetMaterialSubMesh(source.SubMeshes[i].Material, true).Add(source.SubMeshes[i], count);
			}
			this.mBounds = null;
		}

		public void MergeVMesh(CGVMesh source, Matrix4x4 matrix)
		{
			int count = this.Count;
			Array.Resize<Vector3>(ref this.Vertex, this.Count + source.Count);
			int count2 = this.Count;
			for (int i = count; i < count2; i++)
			{
				this.Vertex[i] = matrix.MultiplyPoint3x4(source.Vertex[i - count]);
			}
			this.MergeUVsNormalsAndTangents(source, count);
			for (int j = 0; j < source.SubMeshes.Length; j++)
			{
				this.GetMaterialSubMesh(source.SubMeshes[j].Material, true).Add(source.SubMeshes[j], count);
			}
			this.mBounds = null;
		}

		public void MergeVMeshes(List<CGVMesh> vMeshes, int startIndex, int endIndex)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			Dictionary<Material, List<int[]>> dictionary = new Dictionary<Material, List<int[]>>();
			Dictionary<Material, int> dictionary2 = new Dictionary<Material, int>();
			for (int i = startIndex; i <= endIndex; i++)
			{
				CGVMesh cgvmesh = vMeshes[i];
				num += cgvmesh.Count;
				flag |= cgvmesh.HasNormals;
				flag2 |= cgvmesh.HasTangents;
				flag3 |= cgvmesh.HasUV;
				flag4 |= cgvmesh.HasUV2;
				for (int j = 0; j < cgvmesh.SubMeshes.Length; j++)
				{
					CGVSubMesh cgvsubMesh = cgvmesh.SubMeshes[j];
					if (!dictionary.ContainsKey(cgvsubMesh.Material))
					{
						dictionary[cgvsubMesh.Material] = new List<int[]>(1);
						dictionary2[cgvsubMesh.Material] = 0;
					}
					List<int[]> list = dictionary[cgvsubMesh.Material];
					list.Add(cgvsubMesh.Triangles);
				}
			}
			this.Vertex = new Vector3[num];
			if (flag)
			{
				this.Normal = new Vector3[num];
			}
			if (flag2)
			{
				this.Tangents = new Vector4[num];
			}
			if (flag3)
			{
				this.UV = new Vector2[num];
			}
			if (flag4)
			{
				this.UV2 = new Vector2[num];
			}
			foreach (KeyValuePair<Material, List<int[]>> keyValuePair in dictionary)
			{
				List<int[]> value = keyValuePair.Value;
				CGVSubMesh cgvsubMesh2 = new CGVSubMesh(keyValuePair.Key);
				int num2 = 0;
				for (int k = 0; k < keyValuePair.Value.Count; k++)
				{
					num2 += value[k].Length;
				}
				cgvsubMesh2.Triangles = new int[num2];
				this.AddSubMesh(cgvsubMesh2);
			}
			int num3 = 0;
			for (int l = startIndex; l <= endIndex; l++)
			{
				CGVMesh cgvmesh2 = vMeshes[l];
				Array.Copy(cgvmesh2.Vertex, 0, this.Vertex, num3, cgvmesh2.Vertex.Length);
				if (flag && cgvmesh2.HasNormals)
				{
					Array.Copy(cgvmesh2.Normal, 0, this.Normal, num3, cgvmesh2.Normal.Length);
				}
				if (flag2 && cgvmesh2.HasTangents)
				{
					Array.Copy(cgvmesh2.Tangents, 0, this.Tangents, num3, cgvmesh2.Tangents.Length);
				}
				if (flag3 && cgvmesh2.HasUV)
				{
					Array.Copy(cgvmesh2.UV, 0, this.UV, num3, cgvmesh2.UV.Length);
				}
				if (flag4 && cgvmesh2.HasUV2)
				{
					Array.Copy(cgvmesh2.UV2, 0, this.UV2, num3, cgvmesh2.UV2.Length);
				}
				for (int m = 0; m < cgvmesh2.SubMeshes.Length; m++)
				{
					CGVSubMesh cgvsubMesh3 = cgvmesh2.SubMeshes[m];
					Material material = cgvsubMesh3.Material;
					int[] triangles = cgvsubMesh3.Triangles;
					int num4 = triangles.Length;
					int[] triangles2 = this.GetMaterialSubMesh(material, true).Triangles;
					int num5 = dictionary2[material];
					if (num4 != 0)
					{
						if (num3 == 0)
						{
							Array.Copy(triangles, 0, triangles2, num5, num4);
						}
						else
						{
							for (int n = 0; n < num4; n++)
							{
								triangles2[num5 + n] = triangles[n] + num3;
							}
						}
						dictionary2[material] = num5 + num4;
					}
				}
				num3 += cgvmesh2.Vertex.Length;
			}
		}

		private void MergeUVsNormalsAndTangents(CGVMesh source, int preMergeVertexCount)
		{
			int count = source.Count;
			if (count == 0)
			{
				return;
			}
			int num = preMergeVertexCount + count;
			if (this.HasUV || source.HasUV)
			{
				Vector2[] uv = this.UV;
				this.UV = new Vector2[num];
				if (this.HasUV)
				{
					Array.Copy(uv, this.UV, preMergeVertexCount);
				}
				if (source.HasUV)
				{
					Array.Copy(source.UV, 0, this.UV, preMergeVertexCount, count);
				}
			}
			if (this.HasUV2 || source.HasUV2)
			{
				Vector2[] uv2 = this.UV2;
				this.UV2 = new Vector2[num];
				if (this.HasUV2)
				{
					Array.Copy(uv2, this.UV2, preMergeVertexCount);
				}
				if (source.HasUV2)
				{
					Array.Copy(source.UV2, 0, this.UV2, preMergeVertexCount, count);
				}
			}
			if (this.HasNormals || source.HasNormals)
			{
				Vector3[] normal = this.Normal;
				this.Normal = new Vector3[num];
				if (this.HasNormals)
				{
					Array.Copy(normal, this.Normal, preMergeVertexCount);
				}
				if (source.HasNormals)
				{
					Array.Copy(source.Normal, 0, this.Normal, preMergeVertexCount, count);
				}
			}
			if (this.HasTangents || source.HasTangents)
			{
				Vector4[] tangents = this.Tangents;
				this.Tangents = new Vector4[num];
				if (this.HasTangents)
				{
					Array.Copy(tangents, this.Tangents, preMergeVertexCount);
				}
				if (source.HasTangents)
				{
					Array.Copy(source.Tangents, 0, this.Tangents, preMergeVertexCount, count);
				}
			}
		}

		public CGVSubMesh GetMaterialSubMesh(Material mat, bool createIfMissing = true)
		{
			for (int i = 0; i < this.SubMeshes.Length; i++)
			{
				if (this.SubMeshes[i].Material == mat)
				{
					return this.SubMeshes[i];
				}
			}
			if (createIfMissing)
			{
				CGVSubMesh cgvsubMesh = new CGVSubMesh(mat);
				this.AddSubMesh(cgvsubMesh);
				return cgvsubMesh;
			}
			return null;
		}

		public Mesh AsMesh()
		{
			Mesh result = new Mesh();
			this.ToMesh(ref result);
			return result;
		}

		public void ToMesh(ref Mesh msh)
		{
			msh.vertices = this.Vertex;
			if (this.HasUV)
			{
				msh.uv = this.UV;
			}
			if (this.HasUV2)
			{
				msh.uv2 = this.UV2;
			}
			if (this.HasNormals)
			{
				msh.normals = this.Normal;
			}
			if (this.HasTangents)
			{
				msh.tangents = this.Tangents;
			}
			msh.subMeshCount = this.SubMeshes.Length;
			for (int i = 0; i < this.SubMeshes.Length; i++)
			{
				msh.SetTriangles(this.SubMeshes[i].Triangles, i);
			}
		}

		public Material[] GetMaterials()
		{
			List<Material> list = new List<Material>();
			for (int i = 0; i < this.SubMeshes.Length; i++)
			{
				list.Add(this.SubMeshes[i].Material);
			}
			return list.ToArray();
		}

		public override void RecalculateBounds()
		{
			if (this.Count == 0)
			{
				this.mBounds = new Bounds?(new Bounds(Vector3.zero, Vector3.zero));
			}
			else
			{
				Bounds value = new Bounds(this.Vertex[0], Vector3.zero);
				int num = this.Vertex.Length;
				for (int i = 1; i < num; i++)
				{
					value.Encapsulate(this.Vertex[i]);
				}
				this.mBounds = new Bounds?(value);
			}
		}

		public void RecalculateUV2()
		{
			this.UV2 = CGUtility.CalculateUV2(this.UV);
		}

		public void TRS(Matrix4x4 matrix)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				this.Vertex[i] = matrix.MultiplyPoint3x4(this.Vertex[i]);
			}
			this.mBounds = null;
		}

		private void copyData<T>(ref T[] src, ref T[] dst, int currentSize, int extraSize)
		{
			if (extraSize == 0)
			{
				return;
			}
			T[] sourceArray = dst;
			dst = new T[currentSize + extraSize];
			Array.Copy(sourceArray, dst, currentSize);
			Array.Copy(src, 0, dst, currentSize, extraSize);
		}

		public Vector3[] Vertex;

		public Vector2[] UV;

		public Vector2[] UV2;

		public Vector3[] Normal;

		public Vector4[] Tangents;

		public CGVSubMesh[] SubMeshes;
	}
}
