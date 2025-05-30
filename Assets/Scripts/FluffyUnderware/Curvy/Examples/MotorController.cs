// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.MotorController
using System;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	public class MotorController : SplineController
	{
		protected override void Update()
		{
			float axis = UnityEngine.Input.GetAxis("Vertical");
			base.Speed = Mathf.Abs(axis) * this.MaxSpeed;
			base.MovementDirection = MovementDirectionMethods.FromInt((int)Mathf.Sign(axis));
			base.Update();
		}

		[Section("Motor", true, false, 100)]
		public float MaxSpeed = 30f;
	}
}
