// dnSpy decompiler from Assembly-CSharp.dll class: CameraAnimation
using System;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
	private void OnEnable()
	{
		this.counter = 1;
		if (!this._me)
		{
			this._me = base.transform;
		}
	}

	private void Update()
	{
		if (this.counter == 1)
		{
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target1.position, maxDistanceDelta);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.TruckLook.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
		}
		if (this.MoveBool && base.transform.position == this.Target1.position)
		{
			this.counter = 2;
			base.Invoke("Target2Cam", 4.5f);
			this.MoveBool = false;
		}
	}

	private void Target2Cam()
	{
		base.gameObject.transform.position = this.Target2.transform.position;
		base.gameObject.transform.rotation = this.Target2.transform.rotation;
		base.Invoke("Target3Cam", 6.5f);
	}

	private void Target3Cam()
	{
		base.gameObject.transform.position = this.Target3.transform.position;
		base.gameObject.transform.rotation = this.Target3.transform.rotation;
	}

	public Transform Target1;

	public float speed;

	private int counter;

	private Transform _me;

	public Transform TruckLook;

	public GameObject Target2;

	public GameObject Target3;

	private bool MoveBool = true;
}
