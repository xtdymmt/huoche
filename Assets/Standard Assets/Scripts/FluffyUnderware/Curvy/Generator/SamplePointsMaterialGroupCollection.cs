// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.SamplePointsMaterialGroupCollection
using System;
using System.Collections.Generic;

namespace FluffyUnderware.Curvy.Generator
{
	public class SamplePointsMaterialGroupCollection : List<SamplePointsMaterialGroup>
	{
		public SamplePointsMaterialGroupCollection()
		{
		}

		public SamplePointsMaterialGroupCollection(int capacity) : base(capacity)
		{
		}

		public SamplePointsMaterialGroupCollection(IEnumerable<SamplePointsMaterialGroup> collection) : base(collection)
		{
		}

		public int TriangleCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < base.Count; i++)
				{
					num += base[i].TriangleCount;
				}
				return num;
			}
		}

		public void CalculateAspectCorrection(CGVolume volume, CGMaterialSettingsEx matSettings)
		{
			if (matSettings.KeepAspect == CGKeepAspectMode.Off)
			{
				this.AspectCorrection = 1f;
			}
			else
			{
				float num = 0f;
				float num2 = 0f;
				for (int i = 0; i < base.Count; i++)
				{
					float num3;
					float num4;
					base[i].GetLengths(volume, out num3, out num4);
					num += num3;
					num2 += num4;
				}
				this.AspectCorrection = volume.Length / (num / num2);
			}
		}

		public int MaterialID;

		public float AspectCorrection = 1f;
	}
}
