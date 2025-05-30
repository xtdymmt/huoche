// dnSpy decompiler from Assembly-CSharp.dll class: NotEnoughCoinsScript
using System;
using UnityEngine;

public class NotEnoughCoinsScript : MonoBehaviour
{
	private void OnEnable()
	{
		base.Invoke("DelayMsgHide", 2.5f);
	}

	private void DelayMsgHide()
	{
		base.gameObject.SetActive(false);
	}
}
