// dnSpy decompiler from Assembly-CSharp.dll class: TrafficController
using System;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
	private void Start()
	{
		this.AICarGo = false;
	}

	private void Update()
	{
		if (this.AICarGo)
		{
			for (int i = 0; i < this.AICarScriptArray.Length; i++)
			{
				this.AICarScriptArray[i].enabled = true;
			}
			this.AICarGo = false;
		}
		if (this.AICarStop)
		{
			for (int j = 0; j < this.AICarScriptArray.Length; j++)
			{
				this.AICarScriptArray[j].enabled = false;
			}
			this.AICarStop = false;
		}
	}

	public AICarScript[] AICarScriptArray;

	public bool AICarGo;

	public bool AICarStop;
}
