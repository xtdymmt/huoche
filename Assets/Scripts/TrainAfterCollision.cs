// dnSpy decompiler from Assembly-CSharp.dll class: TrainAfterCollision
using System;
using UnityEngine;

public class TrainAfterCollision : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy && this.GoBool)
		{
			base.gameObject.GetComponent<Rigidbody>().AddForce(0f, 0f, 100f);
			this.GoBool = false;
		}
	}

	private bool GoBool = true;
}
