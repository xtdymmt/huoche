// dnSpy decompiler from Assembly-CSharp.dll class: SurroundClouds
using System;
using UnityEngine;

public class SurroundClouds : MonoBehaviour
{
	private void Start()
	{
		this.theGod = GameObject.Find("God");
	}

	private void Update()
	{
		base.transform.position = new Vector3(this.theGod.transform.position.x, base.transform.position.y, this.theGod.transform.position.z);
	}

	private GameObject theGod;
}
