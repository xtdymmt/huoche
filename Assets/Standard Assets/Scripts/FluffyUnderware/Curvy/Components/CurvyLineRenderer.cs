// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Components.CurvyLineRenderer
using System;
using UnityEngine;
using UnityEngine.Events;

namespace FluffyUnderware.Curvy.Components
{
	[AddComponentMenu("Curvy/Misc/Curvy Line Renderer")]
	[RequireComponent(typeof(LineRenderer))]
	[ExecuteInEditMode]
	[HelpURL("https://curvyeditor.com/doclink/curvylinerenderer")]
	public class CurvyLineRenderer : MonoBehaviour
	{
		public CurvySpline Spline
		{
			get
			{
				return this.m_Spline;
			}
			set
			{
				if (this.m_Spline != value)
				{
					this.unbindEvents();
					this.m_Spline = value;
					this.bindEvents();
					this.Refresh();
				}
			}
		}

		private void Awake()
		{
			this.mRenderer = base.GetComponent<LineRenderer>();
			if (this.m_Spline == null)
			{
				this.m_Spline = base.GetComponent<CurvySpline>();
			}
		}

		private void OnEnable()
		{
			this.mRenderer = base.GetComponent<LineRenderer>();
			this.bindEvents();
		}

		private void OnDisable()
		{
			this.unbindEvents();
		}

		private void Start()
		{
			this.Refresh();
		}

		public void Refresh()
		{
			if (this.Spline && this.Spline.IsInitialized)
			{
				Vector3[] approximation = this.Spline.GetApproximation(Space.Self);
				this.mRenderer.positionCount = approximation.Length;
				this.mRenderer.SetPositions(approximation);
			}
			else if (this.mRenderer != null)
			{
				this.mRenderer.positionCount = 0;
			}
		}

		private void OnSplineRefresh(CurvySplineEventArgs e)
		{
			this.Refresh();
		}

		private void bindEvents()
		{
			if (this.Spline)
			{
				this.Spline.OnRefresh.AddListenerOnce(new UnityAction<CurvySplineEventArgs>(this.OnSplineRefresh));
			}
		}

		private void unbindEvents()
		{
			if (this.Spline)
			{
				this.Spline.OnRefresh.RemoveListener(new UnityAction<CurvySplineEventArgs>(this.OnSplineRefresh));
			}
		}

		public CurvySpline m_Spline;

		private LineRenderer mRenderer;
	}
}
