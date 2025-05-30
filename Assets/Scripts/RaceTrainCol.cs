// dnSpy decompiler from Assembly-CSharp.dll class: RaceTrainCol
using System;
using UnityEngine;

public class RaceTrainCol : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "RaceTrainStop")
		{
			AiRaceTrain.counter = 2;
			UnityEngine.Object.Destroy(other.gameObject);
		}
		if (other.tag == "Racewin")
		{
			UnityEngine.Debug.Log("AI Train Counter Click");
			this.AITraincounter = 1;
			UnityEngine.Object.Destroy(other.gameObject);
		}
		if (other.tag == "points")
		{
			UnityEngine.Debug.Log("Ai col");
			this.AITrainpoints++;
		}
	}

	public int AITraincounter;

	public int AITrainpoints;
}
