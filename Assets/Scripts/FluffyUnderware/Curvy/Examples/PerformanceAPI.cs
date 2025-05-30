// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.PerformanceAPI
using System;
using System.Collections.Generic;
using System.Reflection;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	public class PerformanceAPI : MonoBehaviour
	{
		private void Awake()
		{
			this.mTests.Add("Interpolate");
			this.mTests.Add("Refresh");
		}

		private void OnGUI()
		{
			GUILayout.BeginVertical(GUI.skin.box, new GUILayoutOption[0]);
			GUILayout.Label("Curvy offers various options to fine-tune performance vs. precision balance:", new GUILayoutOption[0]);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Interpolation: ", new GUILayoutOption[0]);
			if (GUILayout.Toggle(this.mInterpolation == CurvyInterpolation.Linear, "Linear", GUI.skin.button, new GUILayoutOption[0]))
			{
				this.mInterpolation = CurvyInterpolation.Linear;
			}
			if (GUILayout.Toggle(this.mInterpolation == CurvyInterpolation.Bezier, "Bezier", GUI.skin.button, new GUILayoutOption[0]))
			{
				this.mInterpolation = CurvyInterpolation.Bezier;
			}
			if (GUILayout.Toggle(this.mInterpolation == CurvyInterpolation.CatmullRom, "CatmullRom", GUI.skin.button, new GUILayoutOption[0]))
			{
				this.mInterpolation = CurvyInterpolation.CatmullRom;
			}
			if (GUILayout.Toggle(this.mInterpolation == CurvyInterpolation.TCB, "TCB", GUI.skin.button, new GUILayoutOption[0]))
			{
				this.mInterpolation = CurvyInterpolation.TCB;
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Orientation: ", new GUILayoutOption[0]);
			if (GUILayout.Toggle(this.mOrientation == CurvyOrientation.None, "None", GUI.skin.button, new GUILayoutOption[0]))
			{
				this.mOrientation = CurvyOrientation.None;
			}
			if (GUILayout.Toggle(this.mOrientation == CurvyOrientation.Static, "Static", GUI.skin.button, new GUILayoutOption[0]))
			{
				this.mOrientation = CurvyOrientation.Static;
			}
			if (GUILayout.Toggle(this.mOrientation == CurvyOrientation.Dynamic, "Dynamic", GUI.skin.button, new GUILayoutOption[0]))
			{
				this.mOrientation = CurvyOrientation.Dynamic;
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Control Points (max): " + this.mControlPointCount.ToString(), new GUILayoutOption[0]);
			this.mControlPointCount = (int)GUILayout.HorizontalSlider((float)this.mControlPointCount, 2f, 1000f, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Total spline length: " + this.mTotalSplineLength.ToString(), new GUILayoutOption[0]);
			this.mTotalSplineLength = (int)GUILayout.HorizontalSlider((float)this.mTotalSplineLength, 5f, 10000f, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Cache Density: " + this.mCacheSize.ToString(), new GUILayoutOption[0]);
			this.mCacheSize = (int)GUILayout.HorizontalSlider((float)this.mCacheSize, 1f, 100f, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			this.mUseCache = GUILayout.Toggle(this.mUseCache, "Use Cache (where applicable)", new GUILayoutOption[0]);
			this.mUseMultiThreads = GUILayout.Toggle(this.mUseMultiThreads, "Use Multiple Threads (where applicable)", new GUILayoutOption[0]);
			GUILayout.Label("Select Test:", new GUILayoutOption[0]);
			int num = GUILayout.SelectionGrid(Mathf.Max(0, this.mCurrentTest), this.mTests.ToArray(), 4, new GUILayoutOption[0]);
			if (num != this.mCurrentTest)
			{
				this.mCurrentTest = num;
				this.Timer.Clear();
				this.mTestResults.Clear();
				this.mGUIMethod = base.GetType().MethodByName("GUI_" + this.mTests[this.mCurrentTest], false, true);
				this.mRunMethod = base.GetType().MethodByName("Test_" + this.mTests[this.mCurrentTest], false, true);
			}
			GUILayout.Space(5f);
			if (this.mGUIMethod != null)
			{
				this.mGUIMethod.Invoke(this, null);
			}
			GUI.enabled = (!this.mExecuting && this.mRunMethod != null);
			string text = (!this.mExecuting) ? ("Run (" + 20 + " times)") : "Please wait...";
			if (GUILayout.Button(text, new GUILayoutOption[0]))
			{
				this.mExecuting = true;
				this.Timer.Clear();
				this.mTestResults.Clear();
				base.Invoke("runTest", 0.5f);
			}
			GUI.enabled = true;
			if (this.Timer.Count > 0)
			{
				foreach (string text2 in this.mTestResults)
				{
					GUILayout.Label(text2, new GUILayoutOption[0]);
				}
				GUILayout.Label(string.Format("Average (ms): {0:0.0000}", this.Timer.AverageMS), new GUILayoutOption[0]);
				GUILayout.Label(string.Format("Minimum (ms): {0:0.0000}", this.Timer.MinimumMS), new GUILayoutOption[0]);
				GUILayout.Label(string.Format("Maximum (ms): {0:0.0000}", this.Timer.MaximumMS), new GUILayoutOption[0]);
			}
			GUILayout.EndVertical();
		}

		private void GUI_Interpolate()
		{
			GUILayout.Label("Interpolates position", new GUILayoutOption[0]);
			this.mInterpolate_UseDistance = GUILayout.Toggle(this.mInterpolate_UseDistance, "By Distance", new GUILayoutOption[0]);
		}

		private void Test_Interpolate()
		{
			CurvySpline spline = this.getSpline();
			this.addRandomCP(ref spline, this.mControlPointCount, this.mTotalSplineLength);
			this.mTestResults.Add("Cache Points: " + spline.CacheSize);
			this.mTestResults.Add(string.Format("Cache Point Distance: {0:0.000}", (float)this.mTotalSplineLength / (float)spline.CacheSize));
			Vector3 vector = Vector3.zero;
			if (this.mInterpolate_UseDistance)
			{
				for (int i = 0; i < 20; i++)
				{
					float distance = UnityEngine.Random.Range(0f, spline.Length);
					if (this.mUseCache)
					{
						this.Timer.Start();
						vector = spline.InterpolateByDistanceFast(distance);
						this.Timer.Stop();
					}
					else
					{
						this.Timer.Start();
						vector = spline.InterpolateByDistance(distance);
						this.Timer.Stop();
					}
				}
			}
			else
			{
				for (int j = 0; j < 20; j++)
				{
					float tf = (float)UnityEngine.Random.Range(0, 1);
					if (this.mUseCache)
					{
						this.Timer.Start();
						vector = spline.InterpolateFast(tf);
						this.Timer.Stop();
					}
					else
					{
						this.Timer.Start();
						vector = spline.Interpolate(tf);
						this.Timer.Stop();
					}
				}
			}
			UnityEngine.Object.Destroy(spline.gameObject);
			vector.Set(0f, 0f, 0f);
		}

		private void GUI_Refresh()
		{
			GUILayout.Label("Refresh Spline or Single segment!", new GUILayoutOption[0]);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label("Mode:", new GUILayoutOption[0]);
			this.mRefresh_Mode = GUILayout.SelectionGrid(this.mRefresh_Mode, new string[]
			{
				"All",
				"Single random segment"
			}, 2, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
		}

		private void Work()
		{
			for (int i = 0; i < 1000; i++)
			{
				Vector3 vector = new Vector3(1f, 2f, 3f);
				vector.Normalize();
			}
		}

		private void work()
		{
			for (int i = 0; i < 1000; i++)
			{
				Vector3 vector = new Vector3(1f, 2f, 3f);
				vector.Normalize();
			}
		}

		private void Test_Refresh()
		{
			CurvySpline spline = this.getSpline();
			this.addRandomCP(ref spline, this.mControlPointCount, this.mTotalSplineLength);
			this.mTestResults.Add("Cache Points: " + spline.CacheSize);
			this.mTestResults.Add(string.Format("Cache Point Distance: {0:0.000}", (float)this.mTotalSplineLength / (float)spline.CacheSize));
			for (int i = 0; i < 20; i++)
			{
				int idx = UnityEngine.Random.Range(0, spline.Count - 1);
				if (this.mRefresh_Mode == 0)
				{
					this.Timer.Start();
					spline.SetDirtyAll(SplineDirtyingType.Everything, true);
					spline.Refresh();
					this.Timer.Stop();
				}
				else
				{
					this.Timer.Start();
					spline.SetDirty(spline[idx], SplineDirtyingType.Everything);
					spline.Refresh();
					this.Timer.Stop();
				}
			}
			UnityEngine.Object.Destroy(spline.gameObject);
		}

		private CurvySpline getSpline()
		{
			CurvySpline curvySpline = CurvySpline.Create();
			curvySpline.Interpolation = this.mInterpolation;
			curvySpline.Orientation = this.mOrientation;
			curvySpline.CacheDensity = this.mCacheSize;
			curvySpline.UseThreading = this.mUseMultiThreads;
			curvySpline.Refresh();
			return curvySpline;
		}

		private void addRandomCP(ref CurvySpline spline, int count, int totalLength)
		{
			Vector3[] array = new Vector3[count];
			float num = (float)totalLength / (float)(count - 1);
			array[0] = Vector3.zero;
			for (int i = 1; i < count; i++)
			{
				int num2 = UnityEngine.Random.Range(0, 2);
				int num3 = (UnityEngine.Random.Range(0f, 1f) <= 0.5f) ? -1 : 1;
				if (num2 != 0)
				{
					if (num2 != 1)
					{
						if (num2 == 2)
						{
							array[i] = array[i - 1] + new Vector3(0f, 0f, num * (float)num3);
						}
					}
					else
					{
						array[i] = array[i - 1] + new Vector3(0f, num * (float)num3, 0f);
					}
				}
				else
				{
					array[i] = array[i - 1] + new Vector3(num * (float)num3, 0f, 0f);
				}
			}
			spline.Add(array);
			spline.Refresh();
		}

		private void runTest()
		{
			this.mRunMethod.Invoke(this, null);
			this.mExecuting = false;
		}

		private const int LOOPS = 20;

		private List<string> mTests = new List<string>();

		private List<string> mTestResults = new List<string>();

		private CurvyInterpolation mInterpolation = CurvyInterpolation.CatmullRom;

		private CurvyOrientation mOrientation = CurvyOrientation.Dynamic;

		private int mCacheSize = 50;

		private int mControlPointCount = 20;

		private int mTotalSplineLength = 100;

		private bool mUseCache;

		private bool mUseMultiThreads = true;

		private int mCurrentTest = -1;

		private bool mExecuting;

		private TimeMeasure Timer = new TimeMeasure(20);

		private MethodInfo mGUIMethod;

		private MethodInfo mRunMethod;

		private bool mInterpolate_UseDistance;

		private int mRefresh_Mode;
	}
}
