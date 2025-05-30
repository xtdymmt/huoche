// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.SplineControllerInputRail
using System;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	public class SplineControllerInputRail : MonoBehaviour
	{
		private void Update()
		{
			float num = Mathf.Clamp(UnityEngine.Input.GetAxis("Vertical"), -1f, 1f);
			this.splineController.Speed = Mathf.Clamp(this.splineController.Speed + num * this.acceleration * Time.deltaTime, 0.001f, this.limit);
		}

		public float acceleration = 0.1f;

		public float limit = 30f;

		public SplineController splineController;
	}
}
