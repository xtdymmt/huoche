// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.ChaseCam
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	[ExecuteInEditMode]
	public class ChaseCam : MonoBehaviour
	{
		private void LateUpdate()
		{
			if (this.MoveTo)
			{
				base.transform.position = Vector3.SmoothDamp(base.transform.position, this.MoveTo.position, ref this.mVelocity, this.ChaseTime);
			}
			if (this.LookAt)
			{
				if (!this.RollTo)
				{
					base.transform.LookAt(this.LookAt);
				}
				else
				{
					base.transform.LookAt(this.LookAt, Vector3.SmoothDamp(base.transform.up, this.RollTo.up, ref this.mRollVelocity, this.ChaseTime));
				}
			}
		}

		public Transform LookAt;

		public Transform MoveTo;

		public Transform RollTo;

		[Positive]
		public float ChaseTime = 0.5f;

		private Vector3 mLastPos;

		private Vector3 mVelocity;

		private Vector3 mRollVelocity;
	}
}
