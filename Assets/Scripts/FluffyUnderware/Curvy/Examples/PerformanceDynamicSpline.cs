// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.PerformanceDynamicSpline
using System;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	public class PerformanceDynamicSpline : MonoBehaviour
	{
		private void Awake()
		{
			this.mSpline = base.GetComponent<CurvySpline>();
		}

		private void Start()
		{
			for (int i = 0; i < this.CPCount; i++)
			{
				this.addCP();
			}
			this.mSpline.Refresh();
			this.mLastUpdateTime = Time.timeSinceLevelLoad + 0.1f;
		}

		private void Update()
		{
			if (Time.timeSinceLevelLoad - (float)this.UpdateInterval * 0.001f > this.mLastUpdateTime)
			{
				this.mLastUpdateTime = Time.timeSinceLevelLoad;
				this.ExecTimes.Start();
				if (this.AlwaysClear)
				{
					this.mSpline.Clear();
				}
				while (this.mSpline.ControlPointCount > this.CPCount)
				{
					this.mSpline.Delete(this.mSpline.ControlPointsList[0], true);
				}
				while (this.mSpline.ControlPointCount <= this.CPCount)
				{
					this.addCP();
				}
				this.mSpline.Refresh();
				this.ExecTimes.Stop();
			}
		}

		private void addCP()
		{
			this.mAngleStep = 6.28318548f / ((float)this.CPCount + (float)this.CPCount * 0.25f);
			Vector3 globalPosition = base.transform.localToWorldMatrix.MultiplyPoint3x4(new Vector3(Mathf.Sin(this.mCurrentAngle) * this.Radius, Mathf.Cos(this.mCurrentAngle) * this.Radius, 0f));
			this.mSpline.InsertAfter(null, globalPosition, true);
			this.mCurrentAngle = Mathf.Repeat(this.mCurrentAngle + this.mAngleStep, 6.28318548f);
		}

		private void OnGUI()
		{
			GUILayout.BeginVertical(GUI.skin.box, new GUILayoutOption[0]);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Interval", new GUILayoutOption[]
			{
				GUILayout.Width(130f)
			});
			this.UpdateInterval = (int)GUILayout.HorizontalSlider((float)this.UpdateInterval, 0f, 5000f, new GUILayoutOption[]
			{
				GUILayout.Width(200f)
			});
			GUILayout.Label(this.UpdateInterval.ToString(), new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("# of Control Points", new GUILayoutOption[]
			{
				GUILayout.Width(130f)
			});
			this.CPCount = (int)GUILayout.HorizontalSlider((float)this.CPCount, 2f, 200f, new GUILayoutOption[]
			{
				GUILayout.Width(200f)
			});
			GUILayout.Label(this.CPCount.ToString(), new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Radius", new GUILayoutOption[]
			{
				GUILayout.Width(130f)
			});
			this.Radius = GUILayout.HorizontalSlider(this.Radius, 10f, 100f, new GUILayoutOption[]
			{
				GUILayout.Width(200f)
			});
			GUILayout.Label(this.Radius.ToString("0.00"), new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			this.AlwaysClear = GUILayout.Toggle(this.AlwaysClear, "Always clear", new GUILayoutOption[0]);
			bool updateCG = this.UpdateCG;
			this.UpdateCG = GUILayout.Toggle(this.UpdateCG, "Use Curvy Generator", new GUILayoutOption[0]);
			if (updateCG != this.UpdateCG)
			{
				this.Generator.gameObject.SetActive(this.UpdateCG);
			}
			GUILayout.Label("Avg. Execution Time (ms): " + this.ExecTimes.AverageMS.ToString("0.000"), new GUILayoutOption[0]);
			GUILayout.EndVertical();
		}

		private CurvySpline mSpline;

		public CurvyGenerator Generator;

		[Positive]
		public int UpdateInterval = 200;

		[RangeEx(2f, 2000f, "", "")]
		public int CPCount = 100;

		[Positive]
		public float Radius = 20f;

		public bool AlwaysClear;

		public bool UpdateCG;

		private float mAngleStep;

		private float mCurrentAngle;

		private float mLastUpdateTime;

		private TimeMeasure ExecTimes = new TimeMeasure(10);
	}
}
