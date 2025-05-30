// dnSpy decompiler from Assembly-CSharp.dll class: AchivementMsg
using System;
using UnityEngine;

public class AchivementMsg : MonoBehaviour
{
	private void OnEnable()
	{
		base.Invoke("DelayMsgHide", 2.5f);
	}

	private void DelayMsgHide()
	{
		this.AchivementInformer.SetActive(false);
	}

	public GameObject AchivementInformer;
}
