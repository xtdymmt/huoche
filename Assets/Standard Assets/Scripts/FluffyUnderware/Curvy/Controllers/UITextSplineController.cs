// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Controllers.UITextSplineController
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FluffyUnderware.Curvy.Controllers
{
	[RequireComponent(typeof(Text))]
	[AddComponentMenu("Curvy/Controller/UI Text Spline Controller")]
	[HelpURL("https://curvyeditor.com/doclink/uitextsplinecontroller")]
	public class UITextSplineController : SplineController, IMeshModifier
	{
		public bool StaticOrientation
		{
			get
			{
				return this.staticOrientation;
			}
			set
			{
				this.staticOrientation = value;
			}
		}

		protected override bool ShowOrientationSection
		{
			get
			{
				return false;
			}
		}

		protected override bool ShowOffsetSection
		{
			get
			{
				return false;
			}
		}

		protected Text Text
		{
			get
			{
				if (this.mText == null)
				{
					this.mText = base.GetComponent<Text>();
				}
				return this.mText;
			}
		}

		protected RectTransform Rect
		{
			get
			{
				if (this.mRect == null)
				{
					this.mRect = base.GetComponent<RectTransform>();
				}
				return this.mRect;
			}
		}

		protected Graphic graphic
		{
			get
			{
				if (this.m_Graphic == null)
				{
					this.m_Graphic = base.GetComponent<Graphic>();
				}
				return this.m_Graphic;
			}
		}

		protected override void InitializedApplyDeltaTime(float deltaTime)
		{
			base.InitializedApplyDeltaTime(deltaTime);
			this.graphic.SetVerticesDirty();
		}

		public void ModifyMesh(Mesh verts)
		{
			if (base.enabled && base.gameObject.activeInHierarchy && base.isInitialized)
			{
				Vector3[] vertices = verts.vertices;
				UITextSplineController.GlyphPlain glyphPlain = new UITextSplineController.GlyphPlain();
				for (int i = 0; i < this.Text.text.Length; i++)
				{
					glyphPlain.Load(ref vertices, i * 4);
					float position = base.AbsolutePosition + glyphPlain.Rect.center.x;
					float tf = this.AbsoluteToRelative(CurvyController.GetClampedPosition(position, CurvyPositionMode.WorldUnits, base.Clamping, this.Length));
					Vector3 interpolatedSourcePosition = this.GetInterpolatedSourcePosition(tf);
					Vector3 tangent = this.GetTangent(tf);
					Vector3 v = interpolatedSourcePosition - this.Rect.position - glyphPlain.Center;
					glyphPlain.Transpose(new Vector3(0f, glyphPlain.Center.y, 0f));
					glyphPlain.Rotate(Quaternion.AngleAxis(Mathf.Atan2(tangent.x, -tangent.y) * 57.29578f - 90f, Vector3.forward));
					glyphPlain.Transpose(v);
					glyphPlain.Save(ref vertices, i * 4);
				}
				verts.vertices = vertices;
			}
		}

		public void ModifyMesh(VertexHelper vertexHelper)
		{
			if (base.enabled && base.gameObject.activeInHierarchy && base.isInitialized)
			{
				List<UIVertex> list = new List<UIVertex>();
				UITextSplineController.GlyphQuad glyphQuad = new UITextSplineController.GlyphQuad();
				vertexHelper.GetUIVertexStream(list);
				vertexHelper.Clear();
				for (int i = 0; i < this.Text.text.Length; i++)
				{
					glyphQuad.LoadTris(list, i * 6);
					float position = base.AbsolutePosition + glyphQuad.Rect.center.x;
					float tf = this.AbsoluteToRelative(CurvyController.GetClampedPosition(position, CurvyPositionMode.WorldUnits, base.Clamping, this.Length));
					Vector3 interpolatedSourcePosition = this.GetInterpolatedSourcePosition(tf);
					Vector3 tangent = this.GetTangent(tf);
					Vector3 v = interpolatedSourcePosition - this.Rect.position - glyphQuad.Center;
					glyphQuad.Transpose(new Vector3(0f, glyphQuad.Center.y, 0f));
					if (!this.StaticOrientation)
					{
						glyphQuad.Rotate(Quaternion.AngleAxis(Mathf.Atan2(tangent.x, -tangent.y) * 57.29578f - 90f, Vector3.forward));
					}
					glyphQuad.Transpose(v);
					glyphQuad.Save(vertexHelper);
				}
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
		}

		public override CurvySpline Spline
		{
			get
			{
				return this.m_Spline;
			}
			set
			{
				if (this.m_Spline != value)
				{
					if (base.isInitialized)
					{
						this.UnbindSplineRelatedEvents();
					}
					this.m_Spline = value;
					if (base.isInitialized)
					{
						this.BindSplineRelatedEvents();
					}
				}
			}
		}

		protected override void BindEvents()
		{
			base.BindEvents();
			this.BindSplineRelatedEvents();
		}

		protected override void UnbindEvents()
		{
			base.UnbindEvents();
			this.UnbindSplineRelatedEvents();
		}

		private void BindSplineRelatedEvents()
		{
			if (this.Spline)
			{
				this.UnbindSplineRelatedEvents();
				this.Spline.OnRefresh.AddListener(new UnityAction<CurvySplineEventArgs>(this.OnSplineRefreshed));
			}
		}

		private void UnbindSplineRelatedEvents()
		{
			if (this.Spline)
			{
				this.Spline.OnRefresh.RemoveListener(new UnityAction<CurvySplineEventArgs>(this.OnSplineRefreshed));
			}
		}

		private void OnSplineRefreshed(CurvySplineEventArgs e)
		{
			CurvySpline curvySpline = e.Sender as CurvySpline;
			if (curvySpline != this.Spline)
			{
				curvySpline.OnRefresh.RemoveListener(new UnityAction<CurvySplineEventArgs>(this.OnSplineRefreshed));
			}
			else
			{
				this.graphic.SetVerticesDirty();
			}
		}

		[Section("Orientation", true, false, 100)]
		[Tooltip("If true, the text characters will keep the same orientation regardless of the spline they follow")]
		[SerializeField]
		private bool staticOrientation;

		private Graphic m_Graphic;

		private RectTransform mRect;

		private Text mText;

		protected class GlyphQuad
		{
			public Vector3 Center
			{
				get
				{
					return this.Rect.center;
				}
			}

			public void Load(List<UIVertex> verts, int index)
			{
				this.V[0] = verts[index];
				this.V[1] = verts[index + 1];
				this.V[2] = verts[index + 2];
				this.V[3] = verts[index + 3];
				this.calcRect();
			}

			public void LoadTris(List<UIVertex> verts, int index)
			{
				this.V[0] = verts[index];
				this.V[1] = verts[index + 1];
				this.V[2] = verts[index + 2];
				this.V[3] = verts[index + 4];
				this.calcRect();
			}

			public void calcRect()
			{
				this.Rect = new Rect(this.V[0].position.x, this.V[2].position.y, this.V[2].position.x - this.V[0].position.x, this.V[0].position.y - this.V[2].position.y);
			}

			public void Save(List<UIVertex> verts, int index)
			{
				verts[index] = this.V[0];
				verts[index + 1] = this.V[1];
				verts[index + 2] = this.V[2];
				verts[index + 3] = this.V[3];
			}

			public void Save(VertexHelper vh)
			{
				vh.AddUIVertexQuad(this.V);
			}

			public void Transpose(Vector3 v)
			{
				for (int i = 0; i < 4; i++)
				{
					UIVertex[] v2 = this.V;
					int num = i;
					v2[num].position = v2[num].position + v;
				}
			}

			public void Rotate(Quaternion rotation)
			{
				for (int i = 0; i < 4; i++)
				{
					this.V[i].position = this.V[i].position.RotateAround(this.Center, rotation);
				}
			}

			public UIVertex[] V = new UIVertex[4];

			public Rect Rect;
		}

		protected class GlyphPlain
		{
			public Vector3 Center
			{
				get
				{
					return this.Rect.center;
				}
			}

			public void Load(ref Vector3[] verts, int index)
			{
				this.V[0] = verts[index];
				this.V[1] = verts[index + 1];
				this.V[2] = verts[index + 2];
				this.V[3] = verts[index + 3];
				this.calcRect();
			}

			public void calcRect()
			{
				this.Rect = new Rect(this.V[0].x, this.V[2].y, this.V[2].x - this.V[0].x, this.V[0].y - this.V[2].y);
			}

			public void Save(ref Vector3[] verts, int index)
			{
				verts[index] = this.V[0];
				verts[index + 1] = this.V[1];
				verts[index + 2] = this.V[2];
				verts[index + 3] = this.V[3];
			}

			public void Transpose(Vector3 v)
			{
				for (int i = 0; i < 4; i++)
				{
					this.V[i] += v;
				}
			}

			public void Rotate(Quaternion rotation)
			{
				for (int i = 0; i < 4; i++)
				{
					this.V[i] = this.V[i].RotateAround(this.Center, rotation);
				}
			}

			public Vector3[] V = new Vector3[4];

			public Rect Rect;
		}
	}
}
