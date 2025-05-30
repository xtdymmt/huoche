// dnSpy decompiler from Assembly-CSharp.dll class: SpeedLimitUIfALSE
using System;
using UnityEngine;

public class SpeedLimitUIfALSE : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.Invoke("UIDisable", 3f);
		}
	}

	private void UIDisable()
	{
		base.gameObject.SetActive(false);
	}
}
