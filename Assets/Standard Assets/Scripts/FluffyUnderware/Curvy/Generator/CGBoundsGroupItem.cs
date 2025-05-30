// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGBoundsGroupItem
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGBoundsGroupItem
	{
		public float Weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				float num = Mathf.Clamp01(value);
				if (this.m_Weight != num)
				{
					this.m_Weight = num;
				}
			}
		}

		public int Index;

		[RangeEx(0f, 1f, "", "", Slider = true, Precision = 1)]
		[SerializeField]
		private float m_Weight = 0.5f;
	}
}
