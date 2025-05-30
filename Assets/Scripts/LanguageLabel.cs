// dnSpy decompiler from Assembly-CSharp.dll class: LanguageLabel
using System;
using UnityEngine;
using UnityEngine.UI;

public class LanguageLabel : MonoBehaviour
{
	private void OnEnable()
	{
		if (PlayerPrefs.GetInt("LanguageSet") == 0)
		{
			base.gameObject.GetComponent<Text>().text = this.ChineseText;
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 1)
		{
			base.gameObject.GetComponent<Text>().text = this.ChineseText;
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 2)
		{
			base.gameObject.GetComponent<Text>().text = this.HindiText;
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 3)
		{
			base.gameObject.GetComponent<Text>().text = this.FrenchText;
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 4)
		{
			base.gameObject.GetComponent<Text>().text = this.SpaininshText;
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 5)
		{
			base.gameObject.GetComponent<Text>().text = this.GermanText;
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 6)
		{
			base.gameObject.GetComponent<Text>().text = this.JapeneseText;
		}
	}

	public string EnglishText;

	public string ChineseText;

	public string HindiText;

	public string FrenchText;

	public string SpaininshText;

	public string GermanText;

	public string JapeneseText;
}
