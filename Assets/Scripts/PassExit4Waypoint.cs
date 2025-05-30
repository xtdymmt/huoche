// dnSpy decompiler from Assembly-CSharp.dll class: PassExit4Waypoint
using System;
using UnityEngine;

public class PassExit4Waypoint : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PassangerExit4")
		{
			other.gameObject.GetComponent<Character4Exit>().counter++;
		}
	}
}
