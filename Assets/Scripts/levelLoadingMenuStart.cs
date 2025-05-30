// dnSpy decompiler from Assembly-CSharp.dll class: levelLoadingMenuStart
using System;
using UnityEngine;
using UnityEngine.UI;

public class levelLoadingMenuStart : MonoBehaviour
{
	private void a()
	{
		this.slider.value += 0.02f;
		if (this.slider.value == 1f && this.Loadinit)
		{
			this.LoadStartbool = true;
			base.Invoke("LoadAgain", 1.2f);
			this.Loadinit = false;
		}
		this.n++;
		int num = this.n;
		if (num != 3)
		{
			if (num != 6)
			{
				if (num == 9)
				{
					this.loadingTxt.text = "加载中 .    ";
					this.n = 0;
				}
			}
			else
			{
				this.loadingTxt.text = "加载中 . . .";
			}
		}
		else
		{
			this.loadingTxt.text = "加载中 . .  ";
		}
	}

	private void OnEnable()
	{
		this.Loadinit = true;
		this.enableSlider = true;
		this.LoadStartbool = false;
		base.InvokeRepeating("a", 0f, 0.07f);
	}

	private void LoadAgain()
	{
		this.LoadStartbool = false;
		this.slider.value = 0f;
		this.Loadinit = true;
	}

	public Scrollbar slider;

	public Text loadingTxt;

	private int n;

	private bool enableSlider;

	private bool LoadStartbool;

	private bool Loadinit = true;
}
