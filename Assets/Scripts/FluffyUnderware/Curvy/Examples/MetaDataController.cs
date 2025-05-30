// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.MetaDataController
using System;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	public class MetaDataController : SplineController
	{
		public float MaxHeight
		{
			get
			{
				return this.m_MaxHeight;
			}
			set
			{
				if (this.m_MaxHeight != value)
				{
					this.m_MaxHeight = value;
				}
			}
		}

		protected override void UserAfterInit()
		{
			this.setHeight();
		}

		protected override void UserAfterUpdate()
		{
			this.setHeight();
		}

		private void setHeight()
		{
			if (this.Spline.Dirty)
			{
				this.Spline.Refresh();
			}
			float num = this.Spline.InterpolateMetadata<HeightMetadata, float>(base.RelativePosition);
			base.transform.Translate(0f, num * this.MaxHeight, 0f, Space.Self);
		}

		[Section("MetaController", true, false, 100, Sort = 0)]
		[RangeEx(0f, 30f, "", "")]
		[SerializeField]
		private float m_MaxHeight = 5f;
	}
}
