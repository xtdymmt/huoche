// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Components.CurvyGLRenderer
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;
using UnityEngine.Events;

namespace FluffyUnderware.Curvy.Components
{
	[HelpURL("https://curvyeditor.com/doclink/curvyglrenderer")]
	[AddComponentMenu("Curvy/Misc/Curvy GL Renderer")]
	public class CurvyGLRenderer : MonoBehaviour
	{
		private void CreateLineMaterial()
		{
			if (!this.lineMaterial)
			{
				this.lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
				this.lineMaterial.hideFlags = HideFlags.HideAndDontSave;
				this.lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
			}
		}

		private void OnPostRender()
		{
			this.sanitize();
			this.CreateLineMaterial();
			for (int i = this.Splines.Count - 1; i >= 0; i--)
			{
				this.Splines[i].Spline.OnRefresh.AddListenerOnce(new UnityAction<CurvySplineEventArgs>(this.OnSplineRefresh));
				if (this.Splines[i].VertexData.Count == 0)
				{
					this.Splines[i].GetVertexData();
				}
				this.Splines[i].Render(this.lineMaterial);
			}
		}

		private void sanitize()
		{
			for (int i = this.Splines.Count - 1; i >= 0; i--)
			{
				if (this.Splines[i] == null || this.Splines[i].Spline == null)
				{
					this.Splines.RemoveAt(i);
				}
			}
		}

		private void OnSplineRefresh(CurvySplineEventArgs e)
		{
			GLSlotData slot = this.getSlot((CurvySpline)e.Sender);
			if (slot == null)
			{
				((CurvySpline)e.Sender).OnRefresh.RemoveListener(new UnityAction<CurvySplineEventArgs>(this.OnSplineRefresh));
			}
			else
			{
				slot.VertexData.Clear();
			}
		}

		private GLSlotData getSlot(CurvySpline spline)
		{
			if (spline)
			{
				foreach (GLSlotData glslotData in this.Splines)
				{
					if (glslotData.Spline == spline)
					{
						return glslotData;
					}
				}
			}
			return null;
		}

		public void Add(CurvySpline spline)
		{
			if (spline != null)
			{
				this.Splines.Add(new GLSlotData
				{
					Spline = spline
				});
			}
		}

		public void Remove(CurvySpline spline)
		{
			for (int i = this.Splines.Count - 1; i >= 0; i--)
			{
				if (this.Splines[i].Spline == spline)
				{
					this.Splines.RemoveAt(i);
				}
			}
		}

		[ArrayEx(ShowAdd = false, Draggable = false)]
		public List<GLSlotData> Splines = new List<GLSlotData>();

		private Material lineMaterial;
	}
}
