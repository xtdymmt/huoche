// dnSpy decompiler from Assembly-CSharp.dll class: RCCTruckTrailer
using System;
using UnityEngine;

public class RCCTruckTrailer : MonoBehaviour
{
	private void Start()
	{
		this.rotationValues = new float[this.wheelColliders.Length];
		base.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
		base.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
		base.GetComponent<Rigidbody>().centerOfMass = new Vector3(this.centerOfMass.transform.localPosition.x * base.transform.localScale.x, this.centerOfMass.transform.localPosition.y * base.transform.localScale.y, this.centerOfMass.transform.localPosition.z * base.transform.localScale.z);
	}

	private void Update()
	{
		this.WheelAlign();
	}

	private void WheelAlign()
	{
		if (this.wheelColliders.Length > 0)
		{
			for (int i = 0; i < this.wheelColliders.Length; i++)
			{
				Vector3 vector = this.wheelColliders[i].transform.TransformPoint(this.wheelColliders[i].center);
				RaycastHit raycastHit;
				if (Physics.Raycast(vector, -this.wheelColliders[i].transform.up, out raycastHit, (this.wheelColliders[i].suspensionDistance + this.wheelColliders[i].radius) * base.transform.localScale.y) && !raycastHit.collider.isTrigger && raycastHit.transform != base.transform)
				{
					this.wheelTransforms[i].transform.position = raycastHit.point + this.wheelColliders[i].transform.up * this.wheelColliders[i].radius * base.transform.localScale.y;
				}
				else
				{
					this.wheelTransforms[i].transform.position = vector - this.wheelColliders[i].transform.up * this.wheelColliders[i].suspensionDistance * base.transform.localScale.y;
				}
				this.wheelTransforms[i].transform.rotation = this.wheelColliders[i].transform.rotation * Quaternion.Euler(this.rotationValues[i], 0f, this.wheelColliders[i].transform.rotation.z);
				this.rotationValues[i] += this.wheelColliders[i].rpm * 6f * Time.deltaTime;
			}
		}
	}

	private void FixedUpdate()
	{
		foreach (WheelCollider wheelCollider in this.wheelColliders)
		{
			wheelCollider.motorTorque = 200f;
		}
	}

	public Transform centerOfMass;

	public Transform[] wheelTransforms;

	public WheelCollider[] wheelColliders;

	private float[] rotationValues;
}
