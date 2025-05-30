// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Shapes.CSRectangle
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Shapes
{
	[CurvyShapeInfo("2D/Rectangle", true)]
	[RequireComponent(typeof(CurvySpline))]
	[AddComponentMenu("Curvy/Shape/Rectangle")]
	public class CSRectangle : CurvyShape2D
	{
		public float Width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_Width != num)
				{
					this.m_Width = num;
					this.Dirty = true;
				}
			}
		}

		public float Height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_Height != num)
				{
					this.m_Height = num;
					this.Dirty = true;
				}
			}
		}

		protected override void Reset()
		{
			base.Reset();
			this.Width = 1f;
			this.Height = 1f;
		}

		protected override void ApplyShape()
		{
			base.ApplyShape();
			base.PrepareSpline(CurvyInterpolation.Linear, CurvyOrientation.Static, 1, true);
			base.PrepareControlPoints(4);
			float num = this.Width / 2f;
			float num2 = this.Height / 2f;
			base.SetCGHardEdges(new int[0]);
			base.SetPosition(0, new Vector3(-num, -num2));
			base.SetPosition(1, new Vector3(-num, num2));
			base.SetPosition(2, new Vector3(num, num2));
			base.SetPosition(3, new Vector3(num, -num2));
		}

		[Positive]
		[SerializeField]
		private float m_Width = 1f;

		[Positive]
		[SerializeField]
		private float m_Height = 1f;
	}
}
