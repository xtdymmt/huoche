// dnSpy decompiler from Assembly-CSharp.dll class: WagonAnim
using System;
using UnityEngine;

public class WagonAnim : MonoBehaviour
{
	private void OnEnable()
	{
		this.counter = 1;
		if (!this._me)
		{
			this._me = base.transform;
		}
		base.Invoke("StopCall", 1.5f);
	}

	private void Update()
	{
		if (this.counter == 1)
		{
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target1.position, maxDistanceDelta);
		}
	}

	private void StopCall()
	{
		this.counter = 2;
	}

	public Transform Target1;

	public float speed;

	private int counter;

	private Transform _me;
}
