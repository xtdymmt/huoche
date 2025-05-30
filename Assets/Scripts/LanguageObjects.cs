// dnSpy decompiler from Assembly-CSharp.dll class: LanguageObjects
using System;
using UnityEngine;

public class LanguageObjects : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (PlayerPrefs.GetInt("LanguageSet") == 0)
		{
			this.English_Object.SetActive(true);
			this.Chinese_Object.SetActive(false);
		}
		else
		{
			this.English_Object.SetActive(false);
			this.Chinese_Object.SetActive(true);
		}
	}

	public GameObject English_Object;

	public GameObject Chinese_Object;
}
