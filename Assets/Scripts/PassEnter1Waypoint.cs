// dnSpy decompiler from Assembly-CSharp.dll class: PassEnter1Waypoint
using System;
using UnityEngine;

public class PassEnter1Waypoint : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PassangerEnter1")
		{
			other.gameObject.GetComponent<Character1Enter>().counter++;
		}
	}
}
