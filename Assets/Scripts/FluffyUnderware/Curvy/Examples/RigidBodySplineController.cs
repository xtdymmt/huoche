// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.RigidBodySplineController
using System;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	[RequireComponent(typeof(Rigidbody))]
	public class RigidBodySplineController : MonoBehaviour
	{
		private void Start()
		{
			this.mRigidBody = base.GetComponent<Rigidbody>();
		}

		private void LateUpdate()
		{
			if (this.CameraController)
			{
				float target = this.Spline.TFToDistance(this.mTF, CurvyClamping.Clamp) - 5f;
				this.CameraController.AbsolutePosition = Mathf.SmoothDamp(this.CameraController.AbsolutePosition, target, ref this.velocity, 0.5f);
			}
		}

		private void FixedUpdate()
		{
			if (this.Spline)
			{
				float num = UnityEngine.Input.GetAxis("Vertical") * this.VSpeed;
				float num2 = UnityEngine.Input.GetAxis("Horizontal") * this.HSpeed;
				Vector3 a;
				this.mTF = this.Spline.GetNearestPointTF(base.transform.localPosition, out a);
				if (num != 0f)
				{
					this.mRigidBody.AddForce(this.Spline.GetTangentFast(this.mTF) * num, ForceMode.Force);
				}
				if (num2 != 0f)
				{
					Vector3 b = this.Spline.InterpolateFast(this.mTF) + Quaternion.AngleAxis(90f, this.Spline.GetTangentFast(this.mTF)) * this.Spline.GetOrientationUpFast(this.mTF);
					Vector3 a2 = a - b;
					this.mRigidBody.AddForce(a2 * num2, ForceMode.Force);
				}
				if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
				{
					this.mRigidBody.AddForce(Vector3.up * this.JumpForce, ForceMode.Impulse);
				}
				this.mRigidBody.AddForce((this.Spline.Interpolate(this.mTF) - base.transform.localPosition) * this.CenterDrag, ForceMode.VelocityChange);
			}
		}

		public CurvySpline Spline;

		public SplineController CameraController;

		public float VSpeed = 10f;

		public float HSpeed = 0.5f;

		public float CenterDrag = 0.5f;

		public float JumpForce = 10f;

		private Rigidbody mRigidBody;

		private float mTF;

		private float velocity;
	}
}
