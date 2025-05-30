// dnSpy decompiler from Assembly-CSharp.dll class: JeepPushCharacter
using System;
using UnityEngine;

public class JeepPushCharacter : MonoBehaviour
{
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		if (!this._me)
		{
			this._me = base.transform;
		}
	}

	private void Update()
	{
		if (this.JeepPushScript.counter == 2)
		{
			this.anim.SetBool("IdleAnim", true);
		}
	}

	private Transform _me;

	private Animator anim;

	public JeepPush JeepPushScript;
}
