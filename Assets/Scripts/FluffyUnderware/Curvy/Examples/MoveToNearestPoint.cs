// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.MoveToNearestPoint
using System;
using FluffyUnderware.DevTools;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyUnderware.Curvy.Examples
{
	[ExecuteInEditMode]
	public class MoveToNearestPoint : MonoBehaviour
	{
		private void Update()
		{
			if (this.Spline && this.Spline.IsInitialized && this.Lookup && !this.Spline.Dirty)
			{
				Vector3 localPosition = this.Spline.transform.InverseTransformPoint(this.Lookup.position);
				this.Timer.Start();
				float nearestPointTF = this.Spline.GetNearestPointTF(localPosition);
				this.Timer.Stop();
				base.transform.position = this.Spline.transform.TransformPoint(this.Spline.Interpolate(nearestPointTF));
				this.StatisticsText.text = string.Format("Blue Curve Cache Points: {0} \nAverage Lookup (ms): {1:0.000}", this.Spline.CacheSize, this.Timer.AverageMS);
			}
		}

		public void OnSliderChange()
		{
			this.Spline.CacheDensity = (int)this.Density.value;
		}

		public Transform Lookup;

		public CurvySpline Spline;

		public Text StatisticsText;

		public Slider Density;

		private TimeMeasure Timer = new TimeMeasure(30);
	}
}
