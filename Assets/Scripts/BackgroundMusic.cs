// dnSpy decompiler from Assembly-CSharp.dll class: BackgroundMusic
using System;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (PlayerPrefs.GetString("Sound Status") == "False")
		{
			this.BackMusic.SetActive(false);
			PlayerPrefs.SetString("Sound Status", "False");
		}
		else
		{
			this.BackMusic.SetActive(true);
			PlayerPrefs.SetString("Sound Status", "True");
		}
	}

	public GameObject BackMusic;
}
