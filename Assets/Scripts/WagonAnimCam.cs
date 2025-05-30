// dnSpy decompiler from Assembly-CSharp.dll class: WagonAnimCam
using System;
using UnityEngine;

public class WagonAnimCam : MonoBehaviour
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
			base.transform.LookAt(this.Target1);
		}
	}

	private void StopCall()
	{
		this.counter = 2;
	}

	public Transform Target1;

	private int counter;

	private Transform _me;
}
