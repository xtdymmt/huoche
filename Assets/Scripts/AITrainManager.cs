// dnSpy decompiler from Assembly-CSharp.dll class: AITrainManager
using System;
using UnityEngine;

public class AITrainManager : MonoBehaviour
{
	private void Start()
	{
		this.AITrainStartCounter = 0;
		this.AITrainCollisonCounter = 0;
	}

	private void Update()
	{
		if (this.AITrainStartCounter == 1)
		{
			this.TrainAiControllerScriptMulti[0].AiTrainStartBool = true;
			this.AITrainStartCounter = 2;
		}
		else if (this.AITrainStartCounter == 3)
		{
			this.TrainAiControllerScriptMulti[1].AiTrainStartBool = true;
			this.AITrainStartCounter = 4;
		}
		if (this.AITrainCollisonCounter == 1)
		{
			this.AITrainRagdoll[0].SetActive(true);
			this.AITrainRagdoll[0].transform.parent = null;
			this.AiTrainManagerMulti[0].SetActive(false);
			this.AITrainCollisonCounter = 2;
		}
		else if (this.AITrainCollisonCounter == 3)
		{
			this.AITrainRagdoll[1].SetActive(true);
			this.AITrainRagdoll[1].transform.parent = null;
			this.AiTrainManagerMulti[1].SetActive(false);
			this.AITrainCollisonCounter = 4;
		}
	}

	public int AITrainStartCounter;

	public TrainAiControllerzz[] TrainAiControllerScriptMulti;

	public int AITrainCollisonCounter;

	public GameObject[] AITrainRagdoll;

	public GameObject[] AiTrainManagerMulti;
}
