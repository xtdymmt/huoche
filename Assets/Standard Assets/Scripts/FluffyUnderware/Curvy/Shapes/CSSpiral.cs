// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Shapes.CSSpiral
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Shapes
{
	[CurvyShapeInfo("3D/Spiral", false)]
	[RequireComponent(typeof(CurvySpline))]
	[AddComponentMenu("Curvy/Shape/Spiral")]
	public class CSSpiral : CurvyShape2D
	{
		public int Count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				int num = Mathf.Max(0, value);
				if (this.m_Count != num)
				{
					this.m_Count = num;
					this.Dirty = true;
				}
			}
		}

		public float Circles
		{
			get
			{
				return this.m_Circles;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_Circles != num)
				{
					this.m_Circles = num;
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

		public AnimationCurve RadiusFactor
		{
			get
			{
				return this.m_RadiusFactor;
			}
			set
			{
				if (this.m_RadiusFactor != value)
				{
					this.m_RadiusFactor = value;
					this.Dirty = true;
				}
			}
		}

		public AnimationCurve Z
		{
			get
			{
				return this.m_Z;
			}
			set
			{
				if (this.m_Z != value)
				{
					this.m_Z = value;
					this.Dirty = true;
				}
			}
		}

		protected override void Reset()
		{
			base.Reset();
			this.Count = 8;
			this.Circles = 3f;
			this.Radius = 5f;
			this.RadiusFactor = AnimationCurve.Linear(0f, 1f, 1f, 1f);
			this.Z = AnimationCurve.Linear(0f, 0f, 1f, 10f);
		}

		protected override void ApplyShape()
		{
			base.PrepareSpline(CurvyInterpolation.CatmullRom, CurvyOrientation.Dynamic, 50, false);
			base.Spline.RestrictTo2D = false;
			int num = Mathf.FloorToInt((float)this.Count * this.Circles);
			base.PrepareControlPoints(num);
			if (num == 0)
			{
				return;
			}
			float num2 = 6.28318548f / (float)this.Count;
			for (int i = 0; i < num; i++)
			{
				float time = (float)i / (float)num;
				float num3 = this.Radius * this.RadiusFactor.Evaluate(time);
				base.SetPosition(i, new Vector3(Mathf.Sin(num2 * (float)i) * num3, Mathf.Cos(num2 * (float)i) * num3, this.m_Z.Evaluate(time)));
			}
		}

		[Positive(Tooltip = "Number of Control Points per full Circle")]
		[SerializeField]
		private int m_Count = 8;

		[Positive(Tooltip = "Number of Full Circles")]
		[SerializeField]
		private float m_Circles = 3f;

		[Positive(Tooltip = "Base Radius")]
		[SerializeField]
		private float m_Radius = 5f;

		[Label(Tooltip = "Radius Multiplicator")]
		[SerializeField]
		private AnimationCurve m_RadiusFactor = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		[SerializeField]
		private AnimationCurve m_Z = AnimationCurve.Linear(0f, 0f, 1f, 10f);
	}
}
