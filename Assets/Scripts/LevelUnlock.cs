// dnSpy decompiler from Assembly-CSharp.dll class: LevelUnlock
using System;
using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
	private void Awake()
	{
		PlayerPrefs.SetInt("UnlockedLevel13", 1);
	}

	private void Update()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel2") == 1)
		{
			this.LockLevel2.SetActive(false);
			this.LevelStars_1.SetActive(true);
		}
		else
		{
			this.LockLevel2.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel3") == 1)
		{
			this.LockLevel3.SetActive(false);
			this.LevelStars_2.SetActive(true);
		}
		else
		{
			this.LockLevel3.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel4") == 1)
		{
			this.LockLevel4.SetActive(false);
			this.LevelStars_3.SetActive(true);
		}
		else
		{
			this.LockLevel4.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel5") == 1)
		{
			this.LockLevel5.SetActive(false);
			this.LevelStars_4.SetActive(true);
		}
		else
		{
			this.LockLevel5.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel6") == 1)
		{
			this.LockLevel6.SetActive(false);
			this.LevelStars_5.SetActive(true);
		}
		else
		{
			this.LockLevel6.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel7") == 1)
		{
			this.LockLevel7.SetActive(false);
			this.LevelStars_6.SetActive(true);
		}
		else
		{
			this.LockLevel7.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel8") == 1)
		{
			this.LockLevel8.SetActive(false);
			this.LevelStars_7.SetActive(true);
		}
		else
		{
			this.LockLevel8.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel9") == 1)
		{
			this.LockLevel9.SetActive(false);
			this.LevelStars_8.SetActive(true);
		}
		else
		{
			this.LockLevel9.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel10") == 1)
		{
			this.LockLevel10.SetActive(false);
			this.LevelStars_9.SetActive(true);
		}
		else
		{
			this.LockLevel10.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel11") == 1)
		{
			this.LockLevel11.SetActive(false);
			this.LevelStars_10.SetActive(true);
		}
		else
		{
			this.LockLevel11.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel12") == 1)
		{
			this.LockLevel12.SetActive(false);
			this.LevelStars_11.SetActive(true);
		}
		else
		{
			this.LockLevel12.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel13Star") == 1)
		{
			this.LevelStars_12.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel14") == 1)
		{
			this.LockLevel14.SetActive(false);
			this.LevelStars_13.SetActive(true);
		}
		else
		{
			this.LockLevel14.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel15") == 1)
		{
			this.LockLevel15.SetActive(false);
			this.LevelStars_14.SetActive(true);
		}
		else
		{
			this.LockLevel15.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel16") == 1)
		{
			this.LockLevel16.SetActive(false);
			this.LevelStars_15.SetActive(true);
		}
		else
		{
			this.LockLevel16.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel17") == 1)
		{
			this.LockLevel17.SetActive(false);
			this.LevelStars_16.SetActive(true);
		}
		else
		{
			this.LockLevel17.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel18") == 1)
		{
			this.LockLevel18.SetActive(false);
			this.LevelStars_17.SetActive(true);
		}
		else
		{
			this.LockLevel18.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel19") == 1)
		{
			this.LockLevel19.SetActive(false);
			this.LevelStars_18.SetActive(true);
		}
		else
		{
			this.LockLevel19.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel20") == 1)
		{
			this.LockLevel20.SetActive(false);
			this.LevelStars_19.SetActive(true);
		}
		else
		{
			this.LockLevel20.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel21") == 1)
		{
			this.LockLevel21.SetActive(false);
			this.LevelStars_20.SetActive(true);
		}
		else
		{
			this.LockLevel21.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel22") == 1)
		{
			this.LockLevel22.SetActive(false);
			this.LevelStars_21.SetActive(true);
		}
		else
		{
			this.LockLevel22.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel23") == 1)
		{
			this.LockLevel23.SetActive(false);
			this.LevelStars_22.SetActive(true);
		}
		else
		{
			this.LockLevel23.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel24") == 1)
		{
			this.LockLevel24.SetActive(false);
			this.LevelStars_23.SetActive(true);
		}
		else
		{
			this.LockLevel24.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel25") == 1)
		{
			this.LockLevel25.SetActive(false);
			this.LevelStars_24.SetActive(true);
		}
		else
		{
			this.LockLevel25.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel26") == 1)
		{
			this.LockLevel26.SetActive(false);
			this.LevelStars_25.SetActive(true);
		}
		else
		{
			this.LockLevel26.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel27") == 1)
		{
			this.LockLevel27.SetActive(false);
			this.LevelStars_26.SetActive(true);
		}
		else
		{
			this.LockLevel27.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel28") == 1)
		{
			this.LevelStars_27.SetActive(true);
		}
	}

	[Header("Career Levels")]
	public GameObject LockLevel2;

	public GameObject LockLevel3;

	public GameObject LockLevel4;

	public GameObject LockLevel5;

	public GameObject LockLevel6;

	public GameObject LockLevel7;

	public GameObject LockLevel8;

	public GameObject LockLevel9;

	public GameObject LockLevel10;

	public GameObject LockLevel11;

	public GameObject LockLevel12;

	[Header("Challange Levels")]
	public GameObject LockLevel14;

	public GameObject LockLevel15;

	public GameObject LockLevel16;

	public GameObject LockLevel17;

	public GameObject LockLevel18;

	public GameObject LockLevel19;

	public GameObject LockLevel20;

	public GameObject LockLevel21;

	public GameObject LockLevel22;

	public GameObject LockLevel23;

	public GameObject LockLevel24;

	public GameObject LockLevel25;

	public GameObject LockLevel26;

	public GameObject LockLevel27;

	[Header("Career Levels Stars")]
	public GameObject LevelStars_1;

	public GameObject LevelStars_2;

	public GameObject LevelStars_3;

	public GameObject LevelStars_4;

	public GameObject LevelStars_5;

	public GameObject LevelStars_6;

	public GameObject LevelStars_7;

	public GameObject LevelStars_8;

	public GameObject LevelStars_9;

	public GameObject LevelStars_10;

	public GameObject LevelStars_11;

	public GameObject LevelStars_12;

	[Header("Challange Levels Stars")]
	public GameObject LevelStars_13;

	public GameObject LevelStars_14;

	public GameObject LevelStars_15;

	public GameObject LevelStars_16;

	public GameObject LevelStars_17;

	public GameObject LevelStars_18;

	public GameObject LevelStars_19;

	public GameObject LevelStars_20;

	public GameObject LevelStars_21;

	public GameObject LevelStars_22;

	public GameObject LevelStars_23;

	public GameObject LevelStars_24;

	public GameObject LevelStars_25;

	public GameObject LevelStars_26;

	public GameObject LevelStars_27;
}
