// dnSpy decompiler from Assembly-CSharp.dll class: TypeWritter
using System;
using UnityEngine;
using UnityEngine.UI;

public class TypeWritter : MonoBehaviour
{
	private void Update()
	{
		if (Time.time >= this.currentTime + 0.05f && this.msg01 != this.output)
		{
			this.pos++;
			this.currentTime = Time.time;
			this.msg01 = this.output.Substring(0, this.pos);
		}
		this.ShowText.text = this.msg01;
	}

	private string msg01;

	public string output;

	private int pos;

	public float currentTime;

	public Text ShowText;
}
