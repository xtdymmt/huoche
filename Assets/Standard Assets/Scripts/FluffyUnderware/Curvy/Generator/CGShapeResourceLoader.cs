// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGShapeResourceLoader
using System;
using FluffyUnderware.Curvy.Shapes;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[ResourceLoader("Shape")]
	public class CGShapeResourceLoader : ICGResourceLoader
	{
		public Component Create(CGModule cgModule, string context)
		{
			CurvySpline curvySpline = CurvySpline.Create();
			curvySpline.transform.position = Vector3.zero;
			curvySpline.RestrictTo2D = true;
			curvySpline.Closed = true;
			curvySpline.Orientation = CurvyOrientation.None;
			curvySpline.gameObject.AddComponent<CSCircle>().Refresh();
			return curvySpline;
		}

		public void Destroy(CGModule cgModule, Component obj, string context, bool kill)
		{
			if (obj != null)
			{
				UnityEngine.Object.Destroy(obj);
			}
		}
	}
}
