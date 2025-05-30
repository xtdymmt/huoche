// dnSpy decompiler from Assembly-CSharp.dll class: TrainAiControllerzz
using System;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

public class TrainAiControllerzz : MonoBehaviour
{
	private void Start()
	{
		this.AiTrainStartBool = false;
		for (int i = 0; i < this.splineController.Length; i++)
		{
			this.splineController[i].Speed = 0f;
		}
	}

	private void Update()
	{
		if (this.AiTrainStartBool && this.GoBool)
		{
			for (int i = 0; i < this.splineController.Length; i++)
			{
				this.splineController[i].Speed = 17f;
			}
			if (this.BlowHornBool)
			{
				base.Invoke("TraiNHornn", 4.5f);
				this.BlowHornBool = false;
			}
			this.GoBool = false;
		}
	}

	private void TraiNHornn()
	{
		base.GetComponent<AudioSource>().PlayOneShot(this.TrainHorn, 1f);
	}

	public SplineController[] splineController;

	public bool AiTrainStartBool;

	private bool GoBool = true;

	public AudioClip TrainHorn;

	private bool BlowHornBool = true;
}
