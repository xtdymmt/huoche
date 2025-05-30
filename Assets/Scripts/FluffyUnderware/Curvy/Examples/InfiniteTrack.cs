// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.InfiniteTrack
using System;
using System.Collections;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.Curvy.Generator.Modules;
using FluffyUnderware.Curvy.Shapes;
using FluffyUnderware.DevTools;
using UnityEngine;
using UnityEngine.UI;
using MinAttribute = FluffyUnderware.DevTools.MinAttribute;

namespace FluffyUnderware.Curvy.Examples
{
	public class InfiniteTrack : MonoBehaviour
	{
		private void Start()
		{
			base.InvokeRepeating("updateStats", 1f, 0.25f);
		}

		private void FixedUpdate()
		{
			if (this.mInitState == 0)
			{
				base.StartCoroutine("setup");
			}
			if (this.mInitState == 2 && this.mUpdateSpline)
			{
				this.advanceTrack();
			}
		}

		private IEnumerator setup()
		{
			this.mInitState = 1;
			this.mGenerators = new CurvyGenerator[this.Sections];
			this.TrackSpline.InsertAfter(null, Vector3.zero, true);
			this.TrackSpline.InsertAfter(null, new Vector3(0f, 0f, this.CPStepSize), true);
			this.mDir = Vector3.forward;
			int num = this.TailCP + this.HeadCP + this.Sections * this.SectionCPCount - 1;
			for (int j = 0; j < num; j++)
			{
				this.addTrackCP();
			}
			this.TrackSpline.Refresh();
			for (int k = this.TailCP; k < this.TrackSpline.ControlPointCount - this.HeadCP; k += this.SectionCPCount)
			{
				this.TrackSpline.ControlPointsList[k].SerializedOrientationAnchor = true;
			}
			for (int l = 0; l < this.Sections; l++)
			{
				this.mGenerators[l] = this.buildGenerator();
				this.mGenerators[l].name = "Generator " + l;
			}
			for (int i = 0; i < this.Sections; i++)
			{
				while (!this.mGenerators[i].IsInitialized)
				{
					yield return 0;
				}
			}
			for (int m = 0; m < this.Sections; m++)
			{
				this.updateSectionGenerator(this.mGenerators[m], m * this.SectionCPCount + this.TailCP, (m + 1) * this.SectionCPCount + this.TailCP);
			}
			this.mInitState = 2;
			this.mUpdateIn = this.SectionCPCount;
			this.Controller.AbsolutePosition = this.TrackSpline.ControlPointsList[this.TailCP + 2].Distance;
			yield break;
		}

		private CurvyGenerator buildGenerator()
		{
			CurvyGenerator curvyGenerator = CurvyGenerator.Create();
			curvyGenerator.AutoRefresh = false;
			InputSplinePath inputSplinePath = curvyGenerator.AddModule<InputSplinePath>();
			InputSplineShape inputSplineShape = curvyGenerator.AddModule<InputSplineShape>();
			BuildShapeExtrusion buildShapeExtrusion = curvyGenerator.AddModule<BuildShapeExtrusion>();
			BuildVolumeMesh buildVolumeMesh = curvyGenerator.AddModule<BuildVolumeMesh>();
			CreateMesh createMesh = curvyGenerator.AddModule<CreateMesh>();
			inputSplinePath.OutputByName["Path"].LinkTo(buildShapeExtrusion.InputByName["Path"]);
			inputSplineShape.OutputByName["Shape"].LinkTo(buildShapeExtrusion.InputByName["Cross"]);
			buildShapeExtrusion.OutputByName["Volume"].LinkTo(buildVolumeMesh.InputByName["Volume"]);
			buildVolumeMesh.OutputByName["VMesh"].LinkTo(createMesh.InputByName["VMesh"]);
			inputSplinePath.Spline = this.TrackSpline;
			inputSplinePath.UseCache = true;
			CSRectangle csrectangle = inputSplineShape.SetManagedShape<CSRectangle>();
			csrectangle.Width = 20f;
			csrectangle.Height = 2f;
			buildShapeExtrusion.Optimize = false;
			buildVolumeMesh.Split = false;
			buildVolumeMesh.SetMaterial(0, this.RoadMaterial);
			buildVolumeMesh.MaterialSetttings[0].SwapUV = true;
			createMesh.Collider = CGColliderEnum.None;
			return curvyGenerator;
		}

		private void advanceTrack()
		{
			this.timeSpline.Start();
			float num = this.Controller.AbsolutePosition;
			for (int i = 0; i < this.SectionCPCount; i++)
			{
				num -= this.TrackSpline.ControlPointsList[0].Length;
				this.TrackSpline.Delete(this.TrackSpline.ControlPointsList[0], true);
			}
			for (int j = 0; j < this.SectionCPCount; j++)
			{
				this.addTrackCP();
			}
			this.TrackSpline.Refresh();
			this.Controller.AbsolutePosition = num;
			this.mUpdateSpline = false;
			this.timeSpline.Stop();
			base.Invoke("advanceSections", 0.2f);
		}

		private void advanceSections()
		{
			CurvyGenerator gen = this.mGenerators[this.mCurrentGen++];
			int num = this.TrackSpline.ControlPointCount - this.HeadCP - 1;
			this.updateSectionGenerator(gen, num - this.SectionCPCount, num);
			if (this.mCurrentGen == this.Sections)
			{
				this.mCurrentGen = 0;
			}
		}

		private void updateStats()
		{
			this.TxtStats.text = string.Format("Spline Update: {0:0.00} ms\nGenerator Update: {1:0.00} ms", this.timeSpline.AverageMS, this.timeCG.AverageMS);
		}

		private void updateSectionGenerator(CurvyGenerator gen, int startCP, int endCP)
		{
			InputSplinePath inputSplinePath = gen.FindModules<InputSplinePath>(true)[0];
			inputSplinePath.EndCP = null;
			inputSplinePath.StartCP = this.TrackSpline.ControlPointsList[startCP];
			inputSplinePath.EndCP = this.TrackSpline.ControlPointsList[endCP];
			BuildVolumeMesh buildVolumeMesh = gen.FindModules<BuildVolumeMesh>(false)[0];
			buildVolumeMesh.MaterialSetttings[0].UVOffset.y = this.lastSectionEndV % 1f;
			this.timeCG.Start();
			gen.Refresh(false);
			this.timeCG.Stop();
			CGVMesh data = buildVolumeMesh.OutVMesh.GetData<CGVMesh>();
			this.lastSectionEndV = data.UV[data.Count - 1].y;
		}

		public void Track_OnControlPointReached(CurvySplineMoveEventArgs e)
		{
			if (--this.mUpdateIn == 0)
			{
				this.mUpdateSpline = true;
				this.mUpdateIn = this.SectionCPCount;
			}
		}

		private void addTrackCP()
		{
			float x = UnityEngine.Random.value * this.CurvationX * DTUtility.RandomSign();
			float y = UnityEngine.Random.value * this.CurvationY * DTUtility.RandomSign();
			Vector3 localPosition = this.TrackSpline.ControlPointsList[this.TrackSpline.ControlPointCount - 1].transform.localPosition;
			Vector3 globalPosition = this.TrackSpline.transform.localToWorldMatrix.MultiplyPoint3x4(localPosition + this.mDir * this.CPStepSize);
			this.mDir = Quaternion.Euler(x, y, 0f) * this.mDir;
			this.TrackSpline.InsertAfter(null, globalPosition, true);
		}

		public CurvySpline TrackSpline;

		public CurvyController Controller;

		public Material RoadMaterial;

		public Text TxtStats;

		[Positive]
		public float CurvationX = 10f;

		[Positive]
		public float CurvationY = 10f;

		[Positive]
		public float CPStepSize = 20f;

		[Positive]
		public int HeadCP = 3;

		[Positive]
		public int TailCP = 2;

		[Min(3f, "", "")]
		public int Sections = 6;

		[Min(1f, "", "")]
		public int SectionCPCount = 2;

		private int mInitState;

		private bool mUpdateSpline;

		private int mUpdateIn;

		private CurvyGenerator[] mGenerators;

		private int mCurrentGen;

		private float lastSectionEndV;

		private Vector3 mDir;

		private TimeMeasure timeSpline = new TimeMeasure(30);

		private TimeMeasure timeCG = new TimeMeasure(1);
	}
}
