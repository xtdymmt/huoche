// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Shapes.CSStar
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Shapes
{
	[CurvyShapeInfo("2D/Star", true)]
	[RequireComponent(typeof(CurvySpline))]
	[AddComponentMenu("Curvy/Shape/Star")]
	public class CSStar : CurvyShape2D
	{
		public int Sides
		{
			get
			{
				return this.m_Sides;
			}
			set
			{
				int num = Mathf.Max(0, value);
				if (this.m_Sides != num)
				{
					this.m_Sides = num;
					this.Dirty = true;
				}
			}
		}

		public float OuterRadius
		{
			get
			{
				return this.m_OuterRadius;
			}
			set
			{
				float num = Mathf.Max(this.InnerRadius, value);
				if (this.m_OuterRadius != num)
				{
					this.m_OuterRadius = num;
					this.Dirty = true;
				}
			}
		}

		public float OuterRoundness
		{
			get
			{
				return this.m_OuterRoundness;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_OuterRoundness != num)
				{
					this.m_OuterRoundness = num;
					this.Dirty = true;
				}
			}
		}

		public float InnerRadius
		{
			get
			{
				return this.m_InnerRadius;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_InnerRadius != num)
				{
					this.m_InnerRadius = num;
					this.Dirty = true;
				}
			}
		}

		public float InnerRoundness
		{
			get
			{
				return this.m_InnerRoundness;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_InnerRoundness != num)
				{
					this.m_InnerRoundness = num;
					this.Dirty = true;
				}
			}
		}

		protected override void Reset()
		{
			base.Reset();
			this.Sides = 5;
			this.OuterRadius = 2f;
			this.OuterRoundness = 0f;
			this.InnerRadius = 1f;
			this.InnerRoundness = 0f;
		}

		protected override void ApplyShape()
		{
			base.PrepareSpline(CurvyInterpolation.Bezier, CurvyOrientation.Dynamic, 50, true);
			base.PrepareControlPoints(this.Sides * 2);
			float num = 6.28318548f / (float)base.Spline.ControlPointCount;
			for (int i = 0; i < base.Spline.ControlPointCount; i += 2)
			{
				Vector3 a = new Vector3(Mathf.Sin(num * (float)i), Mathf.Cos(num * (float)i), 0f);
				base.SetPosition(i, a * this.OuterRadius);
				base.Spline.ControlPointsList[i].AutoHandleDistance = this.OuterRoundness;
				a = new Vector3(Mathf.Sin(num * (float)(i + 1)), Mathf.Cos(num * (float)(i + 1)), 0f);
				base.SetPosition(i + 1, a * this.InnerRadius);
				base.Spline.ControlPointsList[i + 1].AutoHandleDistance = this.InnerRoundness;
			}
		}

		[SerializeField]
		[Positive(Tooltip = "Number of Sides", MinValue = 2f)]
		private int m_Sides = 5;

		[SerializeField]
		[Positive]
		private float m_OuterRadius = 2f;

		[SerializeField]
		[RangeEx(0f, 1f, "", "")]
		private float m_OuterRoundness;

		[SerializeField]
		[Positive]
		private float m_InnerRadius = 1f;

		[SerializeField]
		[RangeEx(0f, 1f, "", "")]
		private float m_InnerRoundness;
	}
}
