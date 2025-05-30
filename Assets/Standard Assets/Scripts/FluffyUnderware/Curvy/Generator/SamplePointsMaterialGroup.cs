// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.SamplePointsMaterialGroup
using System;
using System.Collections.Generic;

namespace FluffyUnderware.Curvy.Generator
{
	public class SamplePointsMaterialGroup
	{
		public SamplePointsMaterialGroup(int materialID)
		{
			this.MaterialID = materialID;
		}

		public int TriangleCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.Patches.Count; i++)
				{
					num += this.Patches[i].TriangleCount;
				}
				return num;
			}
		}

		public int StartVertex
		{
			get
			{
				return this.Patches[0].Start;
			}
		}

		public int EndVertex
		{
			get
			{
				return this.Patches[this.Patches.Count - 1].End;
			}
		}

		public int VertexCount
		{
			get
			{
				return this.EndVertex - this.StartVertex + 1;
			}
		}

		public void GetLengths(CGVolume volume, out float worldLength, out float uLength)
		{
			worldLength = 0f;
			for (int i = this.StartVertex; i < this.EndVertex; i++)
			{
				worldLength += (volume.Vertex[i + 1] - volume.Vertex[i]).magnitude;
			}
			uLength = volume.CrossMap[this.EndVertex] - volume.CrossMap[this.StartVertex];
		}

		public int MaterialID;

		public List<SamplePointsPatch> Patches = new List<SamplePointsPatch>();
	}
}
