// dnSpy decompiler from Assembly-CSharp.dll class: SmoothFollowMenu
using System;
using UnityEngine;

public class SmoothFollowMenu : MonoBehaviour
{
	private void Start()
	{
	}

	private void FixedUpdate()
	{
		this.wantedRotationAngle = this.target.eulerAngles.y;
		this.wantedHeight = this.target.position.y + this.height;
	}

	private void LateUpdate()
	{
		if (!this.target)
		{
			return;
		}
		float num = base.transform.eulerAngles.y;
		float num2 = base.transform.position.y;
		num = Mathf.LerpAngle(num, this.wantedRotationAngle, this.rotationDamping * Time.deltaTime);
		num2 = Mathf.Lerp(num2, this.wantedHeight, this.heightDamping * Time.deltaTime);
		Quaternion rotation = Quaternion.Euler(0f, num, 0f);
		base.transform.position = this.target.position;
		base.transform.position -= rotation * Vector3.forward * this.distance;
		base.transform.position = new Vector3(base.transform.position.x, num2, base.transform.position.z);
		base.transform.LookAt(this.target);
	}

	public Transform target;

	[SerializeField]
	public float distance = 10f;

	[SerializeField]
	public float height = 5f;

	public float rotationDamping;

	public float heightDamping;

	private float wantedRotationAngle;

	private float wantedHeight;
}
