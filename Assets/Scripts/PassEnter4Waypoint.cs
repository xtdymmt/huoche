// dnSpy decompiler from Assembly-CSharp.dll class: PassEnter4Waypoint
using System;
using UnityEngine;

public class PassEnter4Waypoint : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PassangerEnter4")
		{
			other.gameObject.GetComponent<Character4Enter>().counter++;
		}
	}
}
