// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.TrainManager
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	[ExecuteInEditMode]
	public class TrainManager : MonoBehaviour
	{
		private void Start()
		{
			this.Spline = GameObject.FindGameObjectWithTag("StartSpline").GetComponent<CurvySpline>();
			this.TrainStartPositionScriptTTT = (TrainStartPosScripttt)UnityEngine.Object.FindObjectOfType(typeof(TrainStartPosScripttt));
			this.GameMasterScript = (GameMaster)UnityEngine.Object.FindObjectOfType(typeof(GameMaster));
			if (this.TrainStartPositionScriptTTT != null)
			{
				this.Position = this.TrainStartPositionScriptTTT.Position;
			}
			this.setup();
		}

		private void OnDisable()
		{
			this.isSetup = false;
		}

		private void Update()
		{
			if (this.GameMasterScript.MissionCompleteBool)
			{
				if (this.TrainStartPositionScriptTTT != null)
				{
					this.Position = this.TrainStartPositionScriptTTT.StopPosition;
				}
				this.GameMasterScript.MissionCompleteBool = false;
			}
		}

		private void LateUpdate()
		{
			if (!this.isSetup)
			{
				this.setup();
			}
			if (this.Cars.Length > 1)
			{
				TrainCarManager trainCarManager = this.Cars[0];
				TrainCarManager trainCarManager2 = this.Cars[this.Cars.Length - 1];
				if (trainCarManager.FrontAxis.Spline == trainCarManager2.BackAxis.Spline && trainCarManager.FrontAxis.RelativePosition > trainCarManager2.BackAxis.RelativePosition)
				{
					for (int i = 1; i < this.Cars.Length; i++)
					{
						float num = this.Cars[i - 1].Position - this.Cars[i].Position - this.CarSize - this.CarGap;
						if (Mathf.Abs(num) >= this.Limit)
						{
							this.Cars[i].Position += num;
						}
					}
				}
			}
		}

		private void setup()
		{
			if (this.Spline.Dirty)
			{
				this.Spline.Refresh();
			}
			this.Cars = base.GetComponentsInChildren<TrainCarManager>();
			float num = this.Position - this.CarSize / 2f;
			for (int i = 0; i < this.Cars.Length; i++)
			{
				this.Cars[i].setup();
				if (this.Cars[i].BackAxis && this.Cars[i].FrontAxis && this.Cars[i].Waggon)
				{
					this.Cars[i].Position = num;
				}
				num -= this.CarSize + this.CarGap;
			}
			this.isSetup = true;
		}

		public CurvySpline Spline;

		public float Speed;

		public float Position;

		public float CarSize = 10f;

		public float AxisDistance = 8f;

		public float CarGap = 1f;

		public float Limit = 0.2f;

		private bool isSetup;

		private TrainCarManager[] Cars;

		public TrainStartPosScripttt TrainStartPositionScriptTTT;

		public GameMaster GameMasterScript;
	}
}
