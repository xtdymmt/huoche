// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvyUISpline
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("Curvy/Curvy UI Spline", 2)]
	public class CurvyUISpline : CurvySpline
	{
		public static CurvyUISpline CreateUISpline(string gameObjectName = "Curvy UI Spline")
		{
			CurvyUISpline component = new GameObject(gameObjectName, new Type[]
			{
				typeof(CurvyUISpline)
			}).GetComponent<CurvyUISpline>();
			component.SetupUISpline();
			return component;
		}

		protected override void Reset()
		{
			base.Reset();
			this.SetupUISpline();
		}

		private void SetupUISpline()
		{
			base.RestrictTo2D = true;
			base.MaxPointsPerUnit = 1f;
			base.Orientation = CurvyOrientation.None;
		}
	}
}
