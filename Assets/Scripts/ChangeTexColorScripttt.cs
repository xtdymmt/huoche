// dnSpy decompiler from Assembly-CSharp.dll class: ChangeTexColorScripttt
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTexColorScripttt : MonoBehaviour
{
	private void Start()
	{
		this.text = base.GetComponent<Text>();
	}

	private void LateUpdate()
	{
		if (this.i == 0)
		{
			this.text.color = Color.white;
			base.Invoke("ColorChangecall1", this.AnimTime);
		}
		else if (this.i == 1)
		{
			this.text.color = Color.black;
			base.Invoke("ColorChangecall2", this.AnimTime);
		}
	}

	private void ColorChangecall1()
	{
		this.i = 1;
	}

	private void ColorChangecall2()
	{
		this.i = 0;
	}

	private void OnDisable()
	{
		base.CancelInvoke("ColorChangecall1");
		base.CancelInvoke("ColorChangecall2");
	}

	private Text text;

	private int i;

	public float AnimTime = 0.2f;
}
