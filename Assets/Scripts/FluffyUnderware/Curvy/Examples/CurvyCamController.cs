// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.CurvyCamController
using System;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	public class CurvyCamController : SplineController
	{
		protected override void OnEnable()
		{
			base.OnEnable();
			base.Speed = this.MinSpeed;
		}

		protected override void Advance(float speed, float deltaTime)
		{
			base.Advance(speed, deltaTime);
			Vector3 tangent = this.GetTangent(base.RelativePosition);
			float num;
			if (tangent.y < 0f)
			{
				num = this.Down * tangent.y * this.Fric;
			}
			else
			{
				num = this.Up * -tangent.y * this.Fric;
			}
			base.Speed = Mathf.Clamp(base.Speed + this.Mass * num * deltaTime, this.MinSpeed, this.MaxSpeed);
			if (base.RelativePosition == 1f)
			{
				base.Speed = 0f;
			}
		}

		[Section("Curvy Cam", true, false, 100)]
		public float MinSpeed;

		public float MaxSpeed;

		public float Mass;

		public float Down;

		public float Up;

		public float Fric = 0.9f;
	}
}
