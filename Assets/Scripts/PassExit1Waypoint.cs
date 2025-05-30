// dnSpy decompiler from Assembly-CSharp.dll class: PassExit1Waypoint
using System;
using UnityEngine;

public class PassExit1Waypoint : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PassangerExit1")
		{
			other.gameObject.GetComponent<Character1Exit>().counter++;
		}
	}
}
