// dnSpy decompiler from Assembly-CSharp.dll class: ArrowBlink
using System;
using UnityEngine;

public class ArrowBlink : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.BlinkingArrow.activeInHierarchy)
		{
			base.Invoke("ArrowDisplayOff", 0.5f);
		}
	}

	private void ArrowDisplayOff()
	{
		this.BlinkingArrow.SetActive(false);
		base.Invoke("ArrowDisplayON", 0.5f);
	}

	private void ArrowDisplayON()
	{
		this.BlinkingArrow.SetActive(true);
	}

	public GameObject BlinkingArrow;
}
