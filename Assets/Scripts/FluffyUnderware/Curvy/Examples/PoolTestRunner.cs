// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.PoolTestRunner
using System;
using FluffyUnderware.Curvy.Components;
using FluffyUnderware.DevTools;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyUnderware.Curvy.Examples
{
	public class PoolTestRunner : MonoBehaviour
	{
		private void Start()
		{
			this.checkForSpline();
		}

		private void Update()
		{
			this.PoolCountInfo.text = string.Format("Control Points in Pool: {0}", DTSingleton<CurvyGlobalManager>.Instance.ControlPointPool.Count);
		}

		private void checkForSpline()
		{
			if (this.Spline == null)
			{
				this.Spline = CurvySpline.Create();
				Camera.main.GetComponent<CurvyGLRenderer>().Add(this.Spline);
				for (int i = 0; i < 4; i++)
				{
					this.AddCP();
				}
			}
		}

		public void AddCP()
		{
			this.checkForSpline();
			this.Spline.Add(new Vector3[]
			{
				UnityEngine.Random.insideUnitCircle * 50f
			});
			this.Spline.Refresh();
		}

		public void DeleteCP()
		{
			if (this.Spline && this.Spline.ControlPointCount > 0)
			{
				int index = UnityEngine.Random.Range(0, this.Spline.ControlPointCount - 1);
				this.Spline.Delete(this.Spline.ControlPointsList[index], false);
			}
		}

		public void ClearSpline()
		{
			if (this.Spline)
			{
				this.Spline.Clear();
			}
		}

		public void DeleteSpline()
		{
			if (this.Spline)
			{
				this.Spline.Destroy();
			}
		}

		public CurvySpline Spline;

		public Text PoolCountInfo;
	}
}
