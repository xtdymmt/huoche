// dnSpy decompiler from Assembly-CSharp.dll class: DeerRun
using System;
using UnityEngine;

public class DeerRun : MonoBehaviour
{
	private void Start()
	{
		this.counter = 1;
		if (!this._me)
		{
			this._me = base.transform;
		}
		this.model.GetComponent<Animation>()[this.Walkcarry.name].wrapMode = WrapMode.Loop;
		this.model.GetComponent<Animation>()[this.IdleAnim.name].wrapMode = WrapMode.Loop;
	}

	private void Update()
	{
		if (this.counter == 1)
		{
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target1.position, maxDistanceDelta);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target1.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
			this.model.GetComponent<Animation>().Play(this.Walkcarry.name);
		}
		if (base.transform.position == this.Target1.position)
		{
			this.counter = 2;
		}
		if (this.counter == 2)
		{
			float maxDistanceDelta2 = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target2.position, maxDistanceDelta2);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target2.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
			this.model.GetComponent<Animation>().Play(this.Walkcarry.name);
		}
		if (base.transform.position == this.Target2.position)
		{
			this.counter = 3;
		}
		if (this.counter == 3)
		{
			float maxDistanceDelta3 = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target3.position, maxDistanceDelta3);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target3.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
			this.model.GetComponent<Animation>().Play(this.Walkcarry.name);
		}
		if (base.transform.position == this.Target3.position)
		{
			this.counter = 4;
		}
		if (this.counter == 4)
		{
			this.model.GetComponent<Animation>().Play(this.IdleAnim.name);
		}
	}

	public Transform Target1;

	public Transform Target2;

	public Transform Target3;

	public float speed;

	private int counter;

	public AnimationClip Walkcarry;

	public AnimationClip IdleAnim;

	public Transform model;

	private Transform _me;
}
