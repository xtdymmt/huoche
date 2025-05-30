// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Components.GLSlotData
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.Curvy.Components
{
	[Serializable]
	public class GLSlotData
	{
		public void GetVertexData()
		{
			this.VertexData.Clear();
			List<CurvySpline> list = new List<CurvySpline>();
			list.Add(this.Spline);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].IsInitialized)
				{
					this.VertexData.Add(list[i].GetApproximation(Space.World));
				}
			}
		}

		public void Render(Material mat)
		{
			for (int i = 0; i < this.VertexData.Count; i++)
			{
				if (this.VertexData[i].Length > 0)
				{
					mat.SetPass(0);
					GL.Begin(1);
					GL.Color(this.LineColor);
					for (int j = 1; j < this.VertexData[i].Length; j++)
					{
						GL.Vertex(this.VertexData[i][j - 1]);
						GL.Vertex(this.VertexData[i][j]);
					}
					GL.End();
				}
			}
		}

		[SerializeField]
		public CurvySpline Spline;

		public Color LineColor = CurvyGlobalManager.DefaultGizmoColor;

		public List<Vector3[]> VertexData = new List<Vector3[]>();
	}
}
