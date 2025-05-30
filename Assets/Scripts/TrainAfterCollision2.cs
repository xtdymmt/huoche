// dnSpy decompiler from Assembly-CSharp.dll class: TrainAfterCollision2
using System;
using UnityEngine;

public class TrainAfterCollision2 : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy && this.GoBool)
		{
			base.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 10f, 2f));
			this.GoBool = false;
		}
	}

	private bool GoBool = true;
}
