// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Utils.Spline2Mesh
using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy.ThirdParty.LibTessDotNet;
using UnityEngine;

namespace FluffyUnderware.Curvy.Utils
{
	public class Spline2Mesh
	{
		public string Error { get; private set; }

		public bool Apply(out Mesh result)
		{
			this.mTess = null;
			this.mMesh = null;
			this.Error = string.Empty;
			bool flag = this.triangulate();
			if (flag)
			{
				this.mMesh = new Mesh();
				this.mMesh.name = this.MeshName;
				if (this.VertexLineOnly && this.Lines.Count > 0 && this.Lines[0] != null)
				{
					this.mMesh.vertices = this.Lines[0].GetVertices();
				}
				else
				{
					this.mMesh.vertices = UnityLibTessUtility.FromContourVertex(this.mTess.Vertices);
					this.mMesh.triangles = this.mTess.Elements;
				}
				this.mMesh.RecalculateBounds();
				this.mMesh.RecalculateNormals();
				if (!this.SuppressUVMapping && !this.VertexLineOnly)
				{
					Vector3 size = this.mMesh.bounds.size;
					Vector3 min = this.mMesh.bounds.min;
					float num = Mathf.Min(size.x, Mathf.Min(size.y, size.z));
					bool flag2 = num == size.x;
					bool flag3 = num == size.y;
					bool flag4 = num == size.z;
					Vector3[] vertices = this.mMesh.vertices;
					Vector2[] array = new Vector2[vertices.Length];
					float num2 = 0f;
					float num3 = 0f;
					for (int i = 0; i < vertices.Length; i++)
					{
						float num4;
						float num5;
						if (flag2)
						{
							num4 = this.UVOffset.x + (vertices[i].y - min.y) / size.y;
							num5 = this.UVOffset.y + (vertices[i].z - min.z) / size.z;
						}
						else if (flag3)
						{
							num4 = this.UVOffset.x + (vertices[i].z - min.z) / size.z;
							num5 = this.UVOffset.y + (vertices[i].x - min.x) / size.x;
						}
						else
						{
							if (!flag4)
							{
								throw new InvalidOperationException("Couldn't find the minimal bound dimension");
							}
							num4 = this.UVOffset.x + (vertices[i].x - min.x) / size.x;
							num5 = this.UVOffset.y + (vertices[i].y - min.y) / size.y;
						}
						num4 *= this.UVTiling.x;
						num5 *= this.UVTiling.y;
						num2 = ((num4 >= num2) ? num4 : num2);
						num3 = ((num5 >= num3) ? num5 : num3);
						array[i].x = num4;
						array[i].y = num5;
					}
					this.mMesh.uv = array;
					Vector2[] array2 = new Vector2[0];
					if (this.UV2)
					{
						array2 = new Vector2[array.Length];
						float num6 = 1f / num2;
						float num7 = 1f / num3;
						for (int j = 0; j < vertices.Length; j++)
						{
							array2[j].x = array[j].x * num6;
							array2[j].y = array[j].y * num7;
						}
					}
					this.mMesh.uv2 = array2;
				}
			}
			result = this.mMesh;
			return flag;
		}

		private bool triangulate()
		{
			if (this.Lines.Count == 0)
			{
				this.Error = "Missing splines to triangulate";
				return false;
			}
			if (this.VertexLineOnly)
			{
				return true;
			}
			this.mTess = new Tess();
			for (int i = 0; i < this.Lines.Count; i++)
			{
				if (this.Lines[i].Spline == null)
				{
					this.Error = "Missing Spline";
					return false;
				}
				if (!Spline2Mesh.polyLineIsValid(this.Lines[i]))
				{
					this.Error = this.Lines[i].Spline.name + ": Angle must be >0";
					return false;
				}
				Vector3[] vertices = this.Lines[i].GetVertices();
				if (vertices.Length < 3)
				{
					this.Error = this.Lines[i].Spline.name + ": At least 3 Vertices needed!";
					return false;
				}
				this.mTess.AddContour(UnityLibTessUtility.ToContourVertex(vertices, false), this.Lines[i].Orientation);
			}
			try
			{
				this.mTess.Tessellate(this.Winding, ElementType.Polygons, 3);
				return true;
			}
			catch (Exception ex)
			{
				this.Error = ex.Message;
			}
			return false;
		}

		private static bool polyLineIsValid(SplinePolyLine pl)
		{
			return (pl != null && pl.VertexMode == SplinePolyLine.VertexCalculation.ByApproximation) || !Mathf.Approximately(0f, pl.Angle);
		}

		public List<SplinePolyLine> Lines = new List<SplinePolyLine>();

		public WindingRule Winding;

		public Vector2 UVTiling = Vector2.one;

		public Vector2 UVOffset = Vector2.zero;

		public bool SuppressUVMapping;

		public bool UV2;

		public string MeshName = string.Empty;

		public bool VertexLineOnly;

		private Tess mTess;

		private Mesh mMesh;
	}
}
