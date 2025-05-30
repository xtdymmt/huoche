// dnSpy decompiler from Assembly-CSharp.dll class: AiTrainCollision
using System;
using UnityEngine;

public class AiTrainCollision : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "TrackBusy")
		{
			this.RedLight.SetActive(false);
			this.GreenLight.SetActive(true);
			AiRaceTrain.counter = 1;
		}
		if (other.tag == "RaceTrainStop")
		{
		}
	}

	public GameObject RedLight;

	public GameObject GreenLight;
}
