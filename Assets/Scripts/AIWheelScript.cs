// dnSpy decompiler from Assembly-CSharp.dll class: AIWheelScript
using System;
using UnityEngine;

public class AIWheelScript : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		this.wheelRotation();
	}

	private void wheelRotation()
	{
		base.transform.Rotate(this.mywheelCollider.rpm / 60f * 360f * Time.deltaTime, 0f, 0f);
		Vector3 localEulerAngles = base.transform.localEulerAngles;
		localEulerAngles.y = this.mywheelCollider.steerAngle - base.transform.localEulerAngles.z;
		base.transform.localEulerAngles = localEulerAngles;
	}

	private void wheelPosition()
	{
		RaycastHit raycastHit;
		Vector3 position;
		if (Physics.Raycast(this.mywheelCollider.transform.position, -this.mywheelCollider.transform.up, out raycastHit, this.mywheelCollider.radius + this.mywheelCollider.suspensionDistance))
		{
			position = raycastHit.point + this.mywheelCollider.transform.up * this.mywheelCollider.radius;
		}
		else
		{
			position = this.mywheelCollider.transform.position - this.mywheelCollider.transform.up * this.mywheelCollider.suspensionDistance;
		}
		base.transform.position = position;
	}

	public WheelCollider mywheelCollider;
}
