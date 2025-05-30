// dnSpy decompiler from Assembly-CSharp.dll class: PassEnter2Waypoint
using System;
using UnityEngine;

public class PassEnter2Waypoint : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PassangerEnter2")
		{
			other.gameObject.GetComponent<Character2Enter>().counter++;
		}
	}
}
