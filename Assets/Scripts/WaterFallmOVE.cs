// dnSpy decompiler from Assembly-CSharp.dll class: WaterFallmOVE
using System;
using UnityEngine;

public class WaterFallmOVE : MonoBehaviour
{
	public void FixedUpdate()
	{
		if (this.scroll)
		{
			float y = Time.time * this.verticalScrollSpeed;
			float x = Time.time * this.horizontalScrollSpeed;
			base.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, y);
		}
	}

	public void DoActivateTrigger()
	{
		this.scroll = !this.scroll;
	}

	public float horizontalScrollSpeed = 0.25f;

	public float verticalScrollSpeed = 0.25f;

	private bool scroll = true;
}
