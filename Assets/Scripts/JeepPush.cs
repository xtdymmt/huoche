// dnSpy decompiler from Assembly-CSharp.dll class: JeepPush
using System;
using UnityEngine;

public class JeepPush : MonoBehaviour
{
	private void Start()
	{
		this.counter = 1;
		this.anim = base.GetComponent<Animator>();
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
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target1.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Traffic")
		{
			this.counter = 2;
			this.RedLight.SetActive(false);
			this.GreenLight.SetActive(true);
		}
	}

	public Transform Target1;

	public float speed;

	public int counter;

	private Transform _me;

	private Animator anim;

	public GameObject RedLight;

	public GameObject GreenLight;
}
