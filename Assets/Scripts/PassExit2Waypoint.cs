// dnSpy decompiler from Assembly-CSharp.dll class: PassExit2Waypoint
using System;
using UnityEngine;

public class PassExit2Waypoint : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PassangerExit2")
		{
			other.gameObject.GetComponent<Character2Exit>().counter++;
		}
	}
}
