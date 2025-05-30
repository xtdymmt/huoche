// dnSpy decompiler from Assembly-CSharp.dll class: SteamEngine
using System;
using UnityEngine;
using UnityEngine.UI;

public class SteamEngine : MonoBehaviour
{
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		if (this.TrainMoveScript != null)
		{
			this.ScrollbarSize = this.TrainMoveScript.ScrollbarSize;
		}
	}

	private void Update()
	{
		if (this.TrainMoveScript != null)
		{
			if (this.ScrollbarSize.value > 0f && !this.TrainMoveScript.ApplyBrakesBool)
			{
				this.anim.SetInteger("EngineGo", 1);
				if (this.ScrollbarSize.value > 0f && (double)this.ScrollbarSize.value <= 0.2)
				{
					this.anim.speed = 1f;
				}
				else if ((double)this.ScrollbarSize.value >= 0.2 && (double)this.ScrollbarSize.value <= 0.4)
				{
					this.anim.speed = 1.4f;
				}
				else if ((double)this.ScrollbarSize.value >= 0.4 && (double)this.ScrollbarSize.value <= 0.6)
				{
					this.anim.speed = 2f;
				}
				else if ((double)this.ScrollbarSize.value >= 0.6 && (double)this.ScrollbarSize.value <= 0.8)
				{
					this.anim.speed = 2.5f;
				}
				else if ((double)this.ScrollbarSize.value >= 0.8 && (double)this.ScrollbarSize.value <= 1.0)
				{
					this.anim.speed = 3f;
				}
			}
			else
			{
				this.anim.SetInteger("EngineGo", 0);
			}
		}
	}

	public Animator anim;

	public TrainMove TrainMoveScript;

	public Scrollbar ScrollbarSize;
}
