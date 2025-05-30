// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Shapes.CSRoundedRectangle
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Shapes
{
	[CurvyShapeInfo("2D/Rounded Rectangle", true)]
	[RequireComponent(typeof(CurvySpline))]
	[AddComponentMenu("Curvy/Shape/Rounded Rectangle")]
	public class CSRoundedRectangle : CurvyShape2D
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

		protected override void Reset()
		{
			base.Reset();
			this.Width = 1f;
			this.Height = 1f;
			this.Roundness = 0.5f;
		}

		protected override void ApplyShape()
		{
			base.PrepareSpline(CurvyInterpolation.Bezier, CurvyOrientation.Dynamic, 50, true);
			base.PrepareControlPoints(8);
			float num = this.Width / 2f;
			float num2 = this.Height / 2f;
			float num3 = Mathf.Min(num, num2) * this.Roundness;
			base.SetPosition(0, new Vector3(-num, -num2 + num3));
			base.SetPosition(1, new Vector3(-num, num2 - num3));
			base.SetPosition(2, new Vector3(-num + num3, num2));
			base.SetPosition(3, new Vector3(num - num3, num2));
			base.SetPosition(4, new Vector3(num, num2 - num3));
			base.SetPosition(5, new Vector3(num, -num2 + num3));
			base.SetPosition(6, new Vector3(num - num3, -num2));
			base.SetPosition(7, new Vector3(-num + num3, -num2));
			base.SetBezierHandles(0, Vector3.down * num3, Vector3.zero, Space.Self);
			base.SetBezierHandles(1, Vector3.zero, Vector3.up * num3, Space.Self);
			base.SetBezierHandles(2, Vector3.left * num3, Vector3.right * num3, Space.Self);
			base.SetBezierHandles(3, Vector3.zero, Vector3.right * num3, Space.Self);
			base.SetBezierHandles(4, Vector3.up * num3, Vector3.zero, Space.Self);
			base.SetBezierHandles(5, Vector3.zero, Vector3.down * num3, Space.Self);
			base.SetBezierHandles(6, Vector3.right * num3, Vector3.zero, Space.Self);
			base.SetBezierHandles(7, Vector3.zero, Vector3.left * num3, Space.Self);
		}

		[Positive]
		[SerializeField]
		private float m_Width = 1f;

		[Positive]
		[SerializeField]
		private float m_Height = 1f;

		[Range(0f, 1f)]
		[SerializeField]
		private float m_Roundness = 0.5f;
	}
}
