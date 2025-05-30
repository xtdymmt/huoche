// dnSpy decompiler from Assembly-CSharp.dll class: PassExit3Waypoint
using System;
using UnityEngine;

public class PassExit3Waypoint : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PassangerExit3")
		{
			other.gameObject.GetComponent<Character3Exit>().counter++;
		}
	}
}
