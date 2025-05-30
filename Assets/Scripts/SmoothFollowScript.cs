// dnSpy decompiler from Assembly-CSharp.dll class: SmoothFollowScript
using System;
using UnityEngine;

public class SmoothFollowScript : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.target)
		{
			float y = this.target.eulerAngles.y;
			float b = this.target.position.y + this.height;
			float num = base.transform.eulerAngles.y;
			float num2 = base.transform.position.y;
			num = Mathf.LerpAngle(num, y, this.rotationDamping * Time.deltaTime);
			num2 = Mathf.Lerp(num2, b, this.heightDamping * Time.deltaTime);
			Quaternion rotation = Quaternion.Euler(0f, num, 0f);
			Vector3 vector = this.target.position;
			vector -= rotation * Vector3.forward * this.distance;
			vector.y = num2;
			base.transform.position = vector;
			base.transform.LookAt(this.target);
		}
	}

	public Transform target;

	public float distance = 15f;

	public float height = 5f;

	public float heightDamping = 3f;

	public float rotationDamping = 3f;
}
