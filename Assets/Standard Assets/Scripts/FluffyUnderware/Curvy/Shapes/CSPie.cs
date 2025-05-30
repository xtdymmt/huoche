// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Shapes.CSPie
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Shapes
{
	[CurvyShapeInfo("2D/Pie", true)]
	[RequireComponent(typeof(CurvySpline))]
	[AddComponentMenu("Curvy/Shape/Pie")]
	public class CSPie : CSCircle
	{
		public float Roundness
		{
			get
			{
				return this.m_Roundness;
			}
			set
			{
				float num = Mathf.Clamp01(value);
				if (this.m_Roundness != num)
				{
					this.m_Roundness = num;
					this.Dirty = true;
				}
			}
		}

		public int Empty
		{
			get
			{
				return this.m_Empty;
			}
			set
			{
				int num = Mathf.Clamp(value, 0, this.maxEmpty);
				if (this.m_Empty != num)
				{
					this.m_Empty = num;
					this.Dirty = true;
				}
			}
		}

		private int maxEmpty
		{
			get
			{
				return base.Count;
			}
		}

		public CSPie.EatModeEnum Eat
		{
			get
			{
				return this.m_Eat;
			}
			set
			{
				if (this.m_Eat != value)
				{
					this.m_Eat = value;
					this.Dirty = true;
				}
			}
		}

		protected override void Reset()
		{
			base.Reset();
			this.Roundness = 0.5f;
			this.Empty = 1;
			this.Eat = CSPie.EatModeEnum.Right;
		}

		private Vector3 cpPosition(int i, int empty, float d)
		{
			CSPie.EatModeEnum eat = this.Eat;
			if (eat == CSPie.EatModeEnum.Left)
			{
				return new Vector3(Mathf.Sin(d * (float)i) * base.Radius, Mathf.Cos(d * (float)i) * base.Radius, 0f);
			}
			if (eat != CSPie.EatModeEnum.Right)
			{
				return new Vector3(Mathf.Sin(d * ((float)i + (float)empty * 0.5f)) * base.Radius, Mathf.Cos(d * ((float)i + (float)empty * 0.5f)) * base.Radius, 0f);
			}
			return new Vector3(Mathf.Sin(d * (float)(i + empty)) * base.Radius, Mathf.Cos(d * (float)(i + empty)) * base.Radius, 0f);
		}

		protected override void ApplyShape()
		{
			base.PrepareSpline(CurvyInterpolation.Bezier, CurvyOrientation.Static, 50, true);
			base.PrepareControlPoints(base.Count - this.Empty + 2);
			float d = 6.28318548f / (float)base.Count;
			float num = this.Roundness * 0.39f;
			for (int i = 0; i < base.Spline.ControlPointCount - 1; i++)
			{
				base.Spline.ControlPointsList[i].AutoHandles = true;
				base.Spline.ControlPointsList[i].AutoHandleDistance = num;
				base.SetPosition(i, this.cpPosition(i, this.Empty, d));
				base.SetRotation(i, Quaternion.Euler(90f, 0f, 0f));
			}
			base.SetPosition(base.Spline.ControlPointCount - 1, Vector3.zero);
			base.SetRotation(base.Spline.ControlPointCount - 1, Quaternion.Euler(90f, 0f, 0f));
			base.SetBezierHandles(base.Spline.ControlPointCount - 1, 0f);
			base.Spline.ControlPointsList[0].AutoHandles = false;
			base.Spline.ControlPointsList[0].HandleIn = Vector3.zero;
			base.Spline.ControlPointsList[0].SetBezierHandles(num, this.cpPosition(base.Count - 1, this.Empty, d) - base.Spline.ControlPointsList[0].transform.localPosition, this.cpPosition(1, this.Empty, d) - base.Spline.ControlPointsList[0].transform.localPosition, false, true, false);
			base.Spline.ControlPointsList[base.Spline.ControlPointCount - 2].AutoHandles = false;
			base.Spline.ControlPointsList[base.Spline.ControlPointCount - 2].HandleOut = Vector3.zero;
			base.Spline.ControlPointsList[base.Spline.ControlPointCount - 2].SetBezierHandles(num, this.cpPosition(base.Count - 1 - this.Empty, this.Empty, d) - base.Spline.ControlPointsList[base.Spline.ControlPointCount - 2].transform.localPosition, this.cpPosition(base.Count + 1 - this.Empty, this.Empty, d) - base.Spline.ControlPointsList[base.Spline.ControlPointCount - 2].transform.localPosition, true, false, false);
		}

		[Range(0f, 1f)]
		[SerializeField]
		private float m_Roundness = 1f;

		[SerializeField]
		[RangeEx(0f, "maxEmpty", "Empty", "Number of empty slices")]
		private int m_Empty = 1;

		[Label(Tooltip = "Eat Mode")]
		[SerializeField]
		private CSPie.EatModeEnum m_Eat = CSPie.EatModeEnum.Right;

		public enum EatModeEnum
		{
			Left,
			Right,
			Center
		}
	}
}
