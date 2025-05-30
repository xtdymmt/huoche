// dnSpy decompiler from Assembly-CSharp.dll class: AiWayPointIncrement
using System;
using UnityEngine;

public class AiWayPointIncrement : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
		}
		if (other.tag == "PBuggy1")
		{
			other.gameObject.GetComponent<FollowNav>().i++;
		}
		if (other.tag == "PBuggy2")
		{
			other.gameObject.GetComponent<FollowNav>().i++;
		}
		if (other.tag == "PBuggy3")
		{
			other.gameObject.GetComponent<FollowNav>().i++;
		}
		if (other.tag == "PBuggy4")
		{
			other.gameObject.GetComponent<FollowNav>().i++;
		}
	}
}
