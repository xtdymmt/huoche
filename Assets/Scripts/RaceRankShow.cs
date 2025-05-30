// dnSpy decompiler from Assembly-CSharp.dll class: RaceRankShow
using System;
using UnityEngine;
using UnityEngine.UI;

public class RaceRankShow : MonoBehaviour
{
	private void Start()
	{
		this.Rankshowtext.text = string.Empty;
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
		if (this.RaceTrainColScript.AITrainpoints > this.TrainCollisionScriptScript.PlayerTrainPoints)
		{
			this.Rankshowtext.text = "2nd";
		}
		else if (this.RaceTrainColScript.AITrainpoints < this.TrainCollisionScriptScript.PlayerTrainPoints)
		{
			this.Rankshowtext.text = "1st";
		}
	}

	public RaceTrainCol RaceTrainColScript;

	public TrainCollisionScript TrainCollisionScriptScript;

	public Text Rankshowtext;
}
