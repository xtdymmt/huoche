// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.RunnerController
using System;
using System.Collections;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	public class RunnerController : SplineController
	{
		protected override void OnDisable()
		{
			base.OnDisable();
			base.StopAllCoroutines();
		}

		protected override void InitializedApplyDeltaTime(float deltaTime)
		{
			if (Input.GetButtonDown("Fire1") && this.mMode == RunnerController.GuideMode.Guided)
			{
				base.StartCoroutine(this.Jump());
			}
			if (this.mPossibleSwitchTarget != null && this.mSwitchInProgress == 0)
			{
				float axisRaw = UnityEngine.Input.GetAxisRaw("Horizontal");
				if (this.mPossibleSwitchTarget.Options == "Right" && axisRaw > 0f)
				{
					this.Switch(1);
				}
				else if (this.mPossibleSwitchTarget.Options == "Left" && axisRaw < 0f)
				{
					this.Switch(-1);
				}
			}
			else if (this.mSwitchInProgress != 0 && !base.IsSwitching)
			{
				this.mSwitchInProgress = 0;
				this.OnCPReached(new CurvySplineMoveEventArgs(this, this.Spline, this.Spline.TFToSegment(base.RelativePosition), 0f, false, 0f, MovementDirection.Forward));
			}
			base.InitializedApplyDeltaTime(deltaTime);
			if (this.mMode == RunnerController.GuideMode.FreeFall)
			{
				this.fallingSpeed += this.Gravity * deltaTime;
				base.OffsetRadius -= this.fallingSpeed;
				if (base.OffsetRadius <= 0f)
				{
					this.mMode = RunnerController.GuideMode.Guided;
					this.fallingSpeed = 0f;
					base.OffsetRadius = 0f;
				}
			}
			if (this.mMode == RunnerController.GuideMode.Jumping)
			{
				base.OffsetRadius = this.jumpHeight;
			}
		}

		private void Switch(int dir)
		{
			this.mSwitchInProgress = dir;
			Vector3 vector = this.mPossibleSwitchTarget.Spline.transform.InverseTransformPoint(base.transform.position);
			Vector3 a;
			float nearestPointTF = this.mPossibleSwitchTarget.Spline.GetNearestPointTF(vector, out a, (int)this.mPossibleSwitchTarget.CP.Spline.GetSegmentIndex(this.mPossibleSwitchTarget.CP), -1);
			float duration = (a - vector).magnitude / base.Speed;
			this.SwitchTo(this.mPossibleSwitchTarget.Spline, nearestPointTF, duration);
		}

		private IEnumerator Jump()
		{
			this.mMode = RunnerController.GuideMode.Jumping;
			float start = Time.time;
			float f = 0f;
			while (f < 1f)
			{
				if (this.mMode != RunnerController.GuideMode.Jumping)
				{
					break;
				}
				f = (Time.time - start) / this.JumpSpeed;
				this.jumpHeight = this.JumpCurve.Evaluate(f) * this.JumpHeight;
				yield return new WaitForEndOfFrame();
			}
			if (this.mMode == RunnerController.GuideMode.Jumping)
			{
				this.mMode = RunnerController.GuideMode.Guided;
			}
			yield break;
		}

		public void OnCPReached(CurvySplineMoveEventArgs e)
		{
			this.mPossibleSwitchTarget = e.ControlPoint.GetMetadata<SplineRefMetadata>(false);
			if (this.mPossibleSwitchTarget && !this.mPossibleSwitchTarget.Spline)
			{
				this.mPossibleSwitchTarget = null;
			}
		}

		public void UseFollowUpOrFall(CurvySplineMoveEventArgs e)
		{
			CurvySplineSegment controlPoint = e.ControlPoint;
			if (controlPoint == e.Spline.FirstVisibleControlPoint && controlPoint.Connection && !controlPoint.FollowUp)
			{
				float f = controlPoint.transform.position.y - controlPoint.Connection.OtherControlPoints(controlPoint)[0].transform.position.y;
				this.mMode = RunnerController.GuideMode.FreeFall;
				this.fallingSpeed = 0f;
				base.OffsetRadius += Mathf.Abs(f);
			}
		}

		[Section("Jump", true, false, 100)]
		public float JumpHeight = 20f;

		public float JumpSpeed = 0.5f;

		public AnimationCurve JumpCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		public float Gravity = 10f;

		private RunnerController.GuideMode mMode;

		private float jumpHeight;

		private float fallingSpeed;

		private SplineRefMetadata mPossibleSwitchTarget;

		private int mSwitchInProgress;

		private enum GuideMode
		{
			Guided,
			Jumping,
			FreeFall
		}
	}
}
