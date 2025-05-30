// dnSpy decompiler from Assembly-CSharp.dll class: TrainMoveMenu
using System;
using UnityEngine;

public class TrainMoveMenu : MonoBehaviour
{
	private void Start()
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
		if (base.transform.position == this.Target1.position)
		{
			this.counter = 2;
		}
		if (this.counter == 2)
		{
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.TruckLook.position - this._me.position), Time.deltaTime * 9f);
		}
	}

	public Transform Target1;

	public float speed;

	private int counter;

	private Transform _me;

	public Transform TruckLook;
}
