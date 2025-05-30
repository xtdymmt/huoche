// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.HeightMetadata
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	public class HeightMetadata : CurvyMetadataBase, ICurvyInterpolatableMetadata<float>, ICurvyInterpolatableMetadata, ICurvyMetadata
	{
		public object Value
		{
			get
			{
				return this.m_Height;
			}
		}

		public object InterpolateObject(ICurvyMetadata b, float f)
		{
			HeightMetadata heightMetadata = b as HeightMetadata;
			return (!(heightMetadata != null)) ? this.Value : Mathf.Lerp((float)this.Value, (float)heightMetadata.Value, f);
		}

		public float Interpolate(ICurvyMetadata b, float f)
		{
			return (float)this.InterpolateObject(b, f);
		}

		[SerializeField]
		[RangeEx(0f, 1f, "", "", Slider = true)]
		private float m_Height;
	}
}
