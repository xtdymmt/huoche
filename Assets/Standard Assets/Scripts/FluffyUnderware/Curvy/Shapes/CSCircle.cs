// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Shapes.CSCircle
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Shapes
{
	[CurvyShapeInfo("2D/Circle", true)]
	[RequireComponent(typeof(CurvySpline))]
	[AddComponentMenu("Curvy/Shape/Circle")]
	public class CSCircle : CurvyShape2D
	{
		public int Count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				int num = Mathf.Max(2, value);
				if (this.m_Count != num)
				{
					this.m_Count = num;
					this.Dirty = true;
				}
			}
		}

		public float Radius
		{
			get
			{
				return this.m_Radius;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_Radius != num)
				{
					this.m_Radius = num;
					this.Dirty = true;
				}
			}
		}

		protected override void Reset()
		{
			base.Reset();
			this.Count = 4;
			this.Radius = 1f;
		}

		protected override void ApplyShape()
		{
			base.PrepareSpline(CurvyInterpolation.Bezier, CurvyOrientation.Dynamic, 50, true);
			base.PrepareControlPoints(this.Count);
			float num = 6.28318548f / (float)this.Count;
			for (int i = 0; i < this.Count; i++)
			{
				base.Spline.ControlPointsList[i].transform.localPosition = new Vector3(Mathf.Sin(num * (float)i) * this.Radius, Mathf.Cos(num * (float)i) * this.Radius, 0f);
			}
		}

		[Positive(Tooltip = "Number of Control Points")]
		[SerializeField]
		private int m_Count = 4;

		[SerializeField]
		private float m_Radius = 1f;
	}
}
