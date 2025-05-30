// dnSpy decompiler from Assembly-CSharp.dll class: PassEnter3Waypoint
using System;
using UnityEngine;

public class PassEnter3Waypoint : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PassangerEnter3")
		{
			other.gameObject.GetComponent<Character3Enter>().counter++;
		}
	}
}
