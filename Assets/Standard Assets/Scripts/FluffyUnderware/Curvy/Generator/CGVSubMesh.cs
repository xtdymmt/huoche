// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGVSubMesh
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public class CGVSubMesh : CGData
	{
		public CGVSubMesh(Material material = null)
		{
			this.Material = material;
			this.Triangles = new int[0];
		}

		public CGVSubMesh(int[] triangles, Material material = null)
		{
			this.Material = material;
			this.Triangles = triangles;
		}

		public CGVSubMesh(int triangleCount, Material material = null)
		{
			this.Material = material;
			this.Triangles = new int[triangleCount];
		}

		public CGVSubMesh(CGVSubMesh source)
		{
			this.Material = source.Material;
			this.Triangles = (int[])source.Triangles.Clone();
		}

		public override int Count
		{
			get
			{
				return this.Triangles.Length;
			}
		}

		public override T Clone<T>()
		{
			return new CGVSubMesh(this) as T;
		}

		public static CGVSubMesh Get(CGVSubMesh data, int triangleCount, Material material = null)
		{
			if (data == null)
			{
				return new CGVSubMesh(triangleCount, material);
			}
			Array.Resize<int>(ref data.Triangles, triangleCount);
			data.Material = material;
			return data;
		}

		public void ShiftIndices(int offset, int startIndex = 0)
		{
			for (int i = startIndex; i < this.Triangles.Length; i++)
			{
				this.Triangles[i] += offset;
			}
		}

		public void Add(CGVSubMesh other, int shiftIndexOffset = 0)
		{
			int num = this.Triangles.Length;
			int num2 = other.Triangles.Length;
			if (num2 == 0)
			{
				return;
			}
			int[] triangles = this.Triangles;
			this.Triangles = new int[num + num2];
			Array.Copy(triangles, this.Triangles, num);
			Array.Copy(other.Triangles, 0, this.Triangles, num, num2);
			if (shiftIndexOffset != 0)
			{
				this.ShiftIndices(shiftIndexOffset, num);
			}
		}

		public int[] Triangles;

		public Material Material;
	}
}
