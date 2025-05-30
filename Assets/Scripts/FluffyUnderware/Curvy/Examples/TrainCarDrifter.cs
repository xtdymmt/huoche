// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.TrainCarDrifter
using System;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	[ExecuteInEditMode]
	public class TrainCarDrifter : MonoBehaviour
	{
		private void Start()
		{
			this.controllerWheelLeading.Speed = this.speed;
		}

		private void Update()
		{
			if (this.controllerWheelLeading && this.controllerWheelTrailing && this.controllerWheelLeading.Spline && this.controllerWheelTrailing.Spline && this.controllerWheelLeading.Spline != this.controllerWheelTrailing.Spline && this.trainCar)
			{
				Vector3 localPosition = this.controllerWheelTrailing.Spline.transform.InverseTransformPoint(this.controllerWheelLeading.transform.position);
				Vector3 b;
				float nearestPointTF = this.controllerWheelTrailing.Spline.GetNearestPointTF(localPosition, out b);
				this.controllerWheelTrailing.RelativePosition = nearestPointTF;
				float num = Vector3.Distance(this.controllerWheelLeading.transform.position, b);
				float num2 = Mathf.Clamp(Mathf.Sqrt(this.wheelSpacing * this.wheelSpacing - num * num), 0f, 20f);
				this.controllerWheelTrailing.AbsolutePosition -= num2;
				this.trainCar.position = (this.controllerWheelLeading.transform.position + this.controllerWheelTrailing.transform.position) / 2f + this.bodyOffset;
				Vector3 worldPosition = new Vector3(this.controllerWheelLeading.transform.position.x, this.trainCar.transform.position.y, this.controllerWheelLeading.transform.position.z);
				this.trainCar.LookAt(worldPosition, this.controllerWheelLeading.transform.up);
			}
		}

		public float speed = 30f;

		public float wheelSpacing = 9.72f;

		public Vector3 bodyOffset = new Vector3(0f, 1f, 0f);

		public SplineController controllerWheelLeading;

		public SplineController controllerWheelTrailing;

		public Transform trainCar;
	}
}
