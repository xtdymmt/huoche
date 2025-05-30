// dnSpy decompiler from Assembly-CSharp.dll class: TrainScriptFalseMenu
using System;
using UnityEngine;

public class TrainScriptFalseMenu : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.Invoke("UIDisable", 7.5f);
		}
	}

	private void UIDisable()
	{
		this.TrainMoveMenuScript.enabled = false;
	}

	public TrainMoveMenu TrainMoveMenuScript;
}
