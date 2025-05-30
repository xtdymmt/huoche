// dnSpy decompiler from Assembly-CSharp.dll class: AiRaceTrain
using System;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

public class AiRaceTrain : MonoBehaviour
{
	private void Start()
	{
		AiRaceTrain.counter = 0;
		for (int i = 0; i < this.splineController.Length; i++)
		{
			this.splineController[i].Speed = 0f;
		}
	}

	private void Update()
	{
		if (AiRaceTrain.counter == 1)
		{
			for (int i = 0; i < this.splineController.Length; i++)
			{
				if (this.Speed > 5f && this.Speed <= 18f)
				{
					this.splineController[i].Speed = this.Speed;
				}
				else if (this.Speed > 16f && this.Speed <= 20f)
				{
					UnityEngine.Debug.Log("Max Speed Player");
					this.splineController[i].Speed = 18f;
				}
				else
				{
					this.splineController[i].Speed = this.TrainSlowSpeed;
				}
			}
		}
		else if (AiRaceTrain.counter == 2)
		{
			for (int j = 0; j < this.splineController.Length; j++)
			{
				this.splineController[j].Speed = 0f;
			}
		}
	}

	public SplineController[] splineController;

	public float Speed;

	public float TrainSlowSpeed;

	public static int counter;
}
