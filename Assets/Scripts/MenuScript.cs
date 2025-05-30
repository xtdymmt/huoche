// dnSpy decompiler from Assembly-CSharp.dll class: MenuScript
using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
	private void Awake()
	{
		//if (PlayerPrefs.GetInt("DefaultLanguage") == 0)
		{
			MenuScript.SL = Application.systemLanguage;
			//if (MenuScript.SL == SystemLanguage.Chinese || MenuScript.SL == SystemLanguage.ChineseSimplified || MenuScript.SL == SystemLanguage.ChineseTraditional)
			{
				//PlayerPrefs.SetInt("LanguageSet", 1);
			}
			//else
			{
				PlayerPrefs.SetInt("LanguageSet", 0);
			}
			PlayerPrefs.SetInt("DefaultLanguage", 0);
		}
		//this.AdCallerScriptComboObj = (AdsCallerManager)UnityEngine.Object.FindObjectOfType(typeof(AdsCallerManager));
	}

	private void Start()
	{
		Time.timeScale = 1f;
		Handheld.StopActivityIndicator();
		if (Application.platform == RuntimePlatform.Android)
		{
			//PlayGamesPlatform.DebugLogEnabled = true;
			//PlayGamesPlatform.Activate();
			if (!MenuScript.LoginBool)
			{
				Social.localUser.Authenticate(delegate(bool success)
				{
					if (success)
					{
						MenuScript.LoginBool = true;
						UnityEngine.Debug.Log("Login Sucess");
					}
					else
					{
						UnityEngine.Debug.Log("Login failed");
					}
				});
			}
		}
		if (PlayerPrefs.GetInt("SelTrainDB") == 0)
		{
			this.CarCounter = 0;
		}
		else
		{
			this.CarCounter = PlayerPrefs.GetInt("SelTrainDB");
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			LeaderboardManager.AuthenticateToGameCenter();
		}
		if (!MenuScript.StartAnimBool)
		{
			this.AnimaPanel.SetActive(true);
			this.DronAnim.SetActive(true);
			this.MenuPanel.SetActive(false);
			this.MainCam.SetActive(false);
			MenuScript.StartAnimBool = true;
		}
		else
		{
			this.MenuPanel.SetActive(true);
			this.MainCam.SetActive(true);
			this.AnimaPanel.SetActive(false);
			this.DronAnim.SetActive(false);
		}
		this.AchivementPanel.SetActive(false);
		this.EasyModeLevelsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		this.PurchasePanel.SetActive(false);
		this.LevelCounter = 0;
		if (PlayerPrefs.GetInt("UnlockedLevel5") == 1)
		{
			this.ModeLock.SetActive(false);
		}
		else
		{
			this.ModeLock.SetActive(true);
		}
		if (PlayerPrefs.GetString("Vibration") == "False")
		{
			PlayerPrefs.SetString("Vibration", "False");
			PlayerPrefs.Save();
		}
		else
		{
			PlayerPrefs.SetString("Vibration", "True");
			PlayerPrefs.Save();
		}
		Color color = this.SoundSettingsBtn.color;
		color.a = 1f;
		this.SoundSettingsBtn.color = color;
		Color color2 = this.VibrationSettingsBtn.color;
		color2.a = 0f;
		this.VibrationSettingsBtn.color = color2;
		Color color3 = this.LanguageSettingsBtn.color;
		color3.a = 0f;
		this.LanguageSettingsBtn.color = color3;
		this.LanguageCheckCall();
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			LeaderboardManager.ReportScore((long)PlayerPrefs.GetInt("HighScoreDB"), this.HighScore_leaderBoardID);
			LeaderboardManager.ReportScore((long)PlayerPrefs.GetInt("PassDropDB"), this.MaxPassangerDrop_leaderBoardID);
		}
		this.ScoreText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
		if (Application.platform == RuntimePlatform.Android)
		{
			base.Invoke("ReportScore", 1f);
		}
		//this.AdCallerScriptComboObj.HideBanner();
	}

	public void ShowLeaderBoardBtn_Click()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			LeaderboardManager.ShowLeaderboard();
		}
	}

	private void ReportScore()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (Social.localUser.authenticated)
			{
				Social.ReportScore((long)PlayerPrefs.GetInt("HighScoreDB"), this.HighScoreleaderBoardIDAndroid, delegate(bool success)
				{
				});
			}
			if (Social.localUser.authenticated)
			{
				Social.ReportScore((long)PlayerPrefs.GetInt("PassDropDB"), this.MaxPassangerDropleaderBoardIDAndroid, delegate(bool success)
				{
				});
			}
			if (Social.localUser.authenticated)
			{
				Social.ReportScore((long)PlayerPrefs.GetInt("MaxFailedDB"), this.MaxLevelFailedleaderBoardIDAndroid, delegate(bool success)
				{
				});
			}
			if (PlayerPrefs.GetInt("Achive1DB") == 1)
			{
				PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 2000);
				if (Social.localUser.authenticated)
				{
					Social.ReportProgress(this.RookieAchIDAndroid, 100.0, delegate(bool success)
					{
					});
				}
				PlayerPrefs.SetInt("Achive1DB", 2);
			}
			if (PlayerPrefs.GetInt("Achive2DB") == 1)
			{
				PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 5000);
				if (Social.localUser.authenticated)
				{
					Social.ReportProgress(this.ProAchIDAndroid, 100.0, delegate(bool success)
					{
					});
				}
				PlayerPrefs.SetInt("Achive2DB", 2);
			}
			if (PlayerPrefs.GetInt("Achive3DB") == 1)
			{
				PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 8000);
				if (Social.localUser.authenticated)
				{
					Social.ReportProgress(this.EliteAchIDAndroid, 100.0, delegate(bool success)
					{
					});
				}
				PlayerPrefs.SetInt("Achive3DB", 2);
			}
		}
	}

	public void ShowLeaderBoardBtn_Android_Click()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			Social.ShowLeaderboardUI();
		}
	}

	public void ShowAchievementBtn_Android_Click()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			Social.ShowAchievementsUI();
		}
	}

	public void AchivementsBtnClick()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel15") == 1)
		{
			this.ProffesionalAchive.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel30") == 1)
		{
			this.ExpertAchive.SetActive(true);
		}
		if (PlayerPrefs.GetInt("UnlockedLevel35") == 1)
		{
			this.LegendaryAchive.SetActive(true);
		}
		this.MenuPanel.SetActive(false);
		this.AchivementPanel.SetActive(true);
	}

	public void Achivements_BackBtnClick()
	{
		this.MenuPanel.SetActive(true);
		this.AchivementPanel.SetActive(false);
	}

	private void Update()
	{
		//UnityEngine.Debug.Log("Car Counterr::" + this.CarCounter);
		this.CarSelectionCheck();
	}

	public void AnimPlay_Click()
	{
		Debug.Log("ShowOnPlay");
        ADManagerRPK.Instance.ShowYS();
        HuaWeiADManager.ShowOnPlay();
		this.AnimaPanel.SetActive(false);
		this.DronAnim.SetActive(false);
		this.MainCam.SetActive(true);
		this.MenuPanel.SetActive(true);
		MenuScript.StartAnimBool = true;
	}

	public void Play_Click()
	{
		this.MenuPanel.SetActive(false);
		this.EasyModeLevelsPanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.PurchasePanel.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Garage_Click()
	{
		Debug.Log("ShowOnGarage");
        ADManagerRPK.Instance.ShowYS();
        HuaWeiADManager.ShowOnGarage();
		this.GaragePanel.SetActive(true);
		this.MenuPanel.SetActive(false);
		this.EasyModeLevelsPanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.PurchasePanel.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Purchase_Click()
	{
		this.MenuPanel.SetActive(false);
		this.EasyModeLevelsPanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.AchivementPanel.SetActive(false);
		this.PurchasePanel.SetActive(true);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Settings_Click()
	{
		this.MenuPanel.SetActive(false);
		this.EasyModeLevelsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.AchivementPanel.SetActive(false);
		this.SettingsPanel.SetActive(true);
		this.PurchasePanel.SetActive(false);
		this.SoundBtnsPanels.SetActive(true);
		this.VibrationBtnsPanels.SetActive(false);
		this.LanguageBtnsPanels.SetActive(false);
		Color color = this.SoundSettingsBtn.color;
		color.a = 1f;
		this.SoundSettingsBtn.color = color;
		Color color2 = this.VibrationSettingsBtn.color;
		color2.a = 0f;
		this.VibrationSettingsBtn.color = color2;
		Color color3 = this.LanguageSettingsBtn.color;
		color3.a = 0f;
		this.LanguageSettingsBtn.color = color3;
		if (PlayerPrefs.GetString("Sound Status") == "False")
		{
			PlayerPrefs.SetString("Sound Status", "False");
			this.SoundYesBtn.image.overrideSprite = this.SoundUnSelectYes;
			this.SoundNoBtn.image.overrideSprite = this.SoundSelectNo;
			this.SoundYesBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.SoundNoBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
		}
		else if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			PlayerPrefs.SetString("Sound Status", "True");
			this.SoundYesBtn.image.overrideSprite = this.SoundSelectYes;
			this.SoundNoBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.SoundYesBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
			this.SoundNoBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (PlayerPrefs.GetString("Vibration") == "False")
		{
			this.VibrationYesBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.VibrationNoBtn.image.overrideSprite = this.SoundSelectYes;
			this.VibrationYesBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.VibrationNoBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
			PlayerPrefs.SetString("Vibration", "False");
			PlayerPrefs.Save();
		}
		else
		{
			this.VibrationYesBtn.image.overrideSprite = this.SoundSelectYes;
			this.VibrationNoBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.VibrationYesBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
			this.VibrationNoBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			PlayerPrefs.SetString("Vibration", "True");
			PlayerPrefs.Save();
		}
		this.LanguageCheckCall();
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void SettingsBack_Click()
	{
		this.MenuPanel.SetActive(true);
		this.EasyModeLevelsPanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		this.PurchasePanel.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

    public void ExitGame()
    {
//#if UNITY_ANDROID && !UNITY_EDITOR
//        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
//  {
//    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
//     {
//        jo.Call("U3DToAndroidExit");
//     }
//  } 
        
//#endif
Application.Quit();
    }
    public void CareerMode_Click()
	{
		Debug.Log("ShowCareer");
        ADManagerRPK.Instance.ShowYS();
        HuaWeiADManager.ShowCareer();
//#if UNITY_ANDROID && !UNITY_EDITOR
//        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
//  {
//    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
//     {
//        jo.Call("ShowPause");
//     }
//  } 
        
//#endif
        this.MenuPanel.SetActive(false);
		this.EasyModeLevelsPanel.SetActive(true);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		this.PurchasePanel.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void CareerMode_Back_Click()
	{
		this.MenuPanel.SetActive(true);
		this.EasyModeLevelsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		this.PurchasePanel.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void ChallangeMode_Click()
	{
		Debug.Log("ShowChallenge");
        ADManagerRPK.Instance.ShowYS();
        HuaWeiADManager.ShowChallenge();
		if (PlayerPrefs.GetInt("UnlockedLevel5") == 1 || PlayerPrefs.GetInt("ChallengeLvlPurchased") == 1)
		{
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(true);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PurchasePanel.SetActive(false);
			this.SettingsPanel.SetActive(false);
		}
		else
		{
			this.ModeLockDialogue.SetActive(true);
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void ChallangeMode_Back_Click()
	{
		this.MenuPanel.SetActive(true);
		this.EasyModeLevelsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.PurchasePanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void RaceMode_Click()
	{
		this.MenuPanel.SetActive(false);
		this.EasyModeLevelsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(true);
		this.PurchasePanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level1_Click()
	{
		this.LevelCounter = 2;
		this.MenuPanel.SetActive(false);
		this.EasyModeLevelsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.PlayLoadLevel_Click();
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level2_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel2") == 1)
		{
			this.LevelCounter = 3;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level3_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel3") == 1)
		{
			this.LevelCounter = 4;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level4_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel4") == 1)
		{
			this.LevelCounter = 5;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level5_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel5") == 1)
		{
			this.LevelCounter = 6;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level6_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel6") == 1)
		{
			this.LevelCounter = 7;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level7_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel7") == 1)
		{
			this.LevelCounter = 8;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level8_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel8") == 1)
		{
			this.LevelCounter = 9;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level9_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel9") == 1)
		{
			this.LevelCounter = 10;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level10_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel10") == 1)
		{
			this.LevelCounter = 11;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level11_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel11") == 1)
		{
			this.LevelCounter = 12;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level12_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel12") == 1)
		{
			this.LevelCounter = 13;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level13_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel13") == 1)
		{
			this.LevelCounter = 14;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level14_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel14") == 1)
		{
			this.LevelCounter = 15;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level15_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel15") == 1)
		{
			this.LevelCounter = 16;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level16_Click()
	{
		this.LevelCounter = 17;
		this.MenuPanel.SetActive(false);
		this.EasyModeLevelsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.PlayLoadLevel_Click();
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level17_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel17") == 1)
		{
			this.LevelCounter = 18;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level18_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel18") == 1)
		{
			this.LevelCounter = 19;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level19_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel19") == 1)
		{
			this.LevelCounter = 20;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level20_Click()
	{
		
		if (PlayerPrefs.GetInt("UnlockedLevel20") == 1)
		{
			this.LevelCounter = 21;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level21_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel21") == 1)
		{
			this.LevelCounter = 22;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level22_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel22") == 1)
		{
			this.LevelCounter = 23;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level23_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel23") == 1)
		{
			this.LevelCounter = 24;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level24_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel24") == 1)
		{
			this.LevelCounter = 25;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level25_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel25") == 1)
		{
			this.LevelCounter = 26;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level26_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel26") == 1)
		{
			this.LevelCounter = 27;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level27_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel27") == 1)
		{
			this.LevelCounter = 28;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level28_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel28") == 1)
		{
			this.LevelCounter = 29;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level29_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel29") == 1)
		{
			this.LevelCounter = 30;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level30_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel30") == 1)
		{
			this.LevelCounter = 31;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level31_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel31") == 1)
		{
			this.LevelCounter = 32;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level32_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel32") == 1)
		{
			this.LevelCounter = 33;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Level33_Click()
	{
		if (PlayerPrefs.GetInt("UnlockedLevel33") == 1)
		{
			this.LevelCounter = 34;
			this.MenuPanel.SetActive(false);
			this.EasyModeLevelsPanel.SetActive(false);
			this.ChallangeModeLevelsPanel.SetActive(false);
			this.RaceModeLevelsPanel.SetActive(false);
			this.PlayLoadLevel_Click();
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void PlayLoadLevel_Click()
	{
		this.LoadingPanel.SetActive(true);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
		base.StartCoroutine(this.Load());
		base.StartCoroutine(this.LoadNewScene());
	}

	private IEnumerator LoadNewScene()
	{
		yield return new WaitForSeconds(1f);
		AsyncOperation async = SceneManager.LoadSceneAsync(this.LevelCounter);
		while (!async.isDone)
		{
			yield return null;
		}
		yield break;
	}

	private IEnumerator Load()
	{
		Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
		Handheld.StartActivityIndicator();
		yield return new WaitForSeconds(0f);
		yield break;
	}

	public void SoundBtn_ClickYes()
	{
		PlayerPrefs.SetString("Sound Status", "True");
		this.SoundYesBtn.image.overrideSprite = this.SoundSelectYes;
		this.SoundNoBtn.image.overrideSprite = this.SoundUnSelectNo;
		this.SoundYesBtnScale.transform.localScale = new Vector3(2.4f, 2.4f, 1f);
		this.SoundNoBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void SoundBtn_ClickNO()
	{
		PlayerPrefs.SetString("Sound Status", "False");
		this.SoundYesBtn.image.overrideSprite = this.SoundUnSelectYes;
		this.SoundNoBtn.image.overrideSprite = this.SoundSelectNo;
		this.SoundYesBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		this.SoundNoBtnScale.transform.localScale = new Vector3(2.4f, 2.4f, 1f);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void VibrationBtn_ClickYes()
	{
		this.VibrationYesBtn.image.overrideSprite = this.SoundSelectYes;
		this.VibrationNoBtn.image.overrideSprite = this.SoundUnSelectNo;
		this.VibrationYesBtnScale.transform.localScale = new Vector3(2.4f, 2.4f, 1f);
		this.VibrationNoBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		PlayerPrefs.SetString("Vibration", "True");
		PlayerPrefs.Save();
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void VibrationBtn_ClickNo()
	{
		this.VibrationYesBtn.image.overrideSprite = this.SoundUnSelectNo;
		this.VibrationNoBtn.image.overrideSprite = this.SoundSelectYes;
		this.VibrationYesBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		this.VibrationNoBtnScale.transform.localScale = new Vector3(2.4f, 2.4f, 1f);
		PlayerPrefs.SetString("Vibration", "False");
		PlayerPrefs.Save();
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void EnglishLanguage()
	{
		PlayerPrefs.SetInt("LanguageSet", 0);
		this.SettingsBackground.SetActive(false);
		base.Invoke("DelayLanguageSet", 0.1f);
		base.Invoke("LanguageCheckCall", 0.1f);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void ChineseLanguage()
	{
		PlayerPrefs.SetInt("LanguageSet", 1);
		this.SettingsBackground.SetActive(false);
		base.Invoke("DelayLanguageSet", 0.1f);
		base.Invoke("LanguageCheckCall", 0.1f);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void HindiLanguage()
	{
		PlayerPrefs.SetInt("LanguageSet", 2);
		this.SettingsBackground.SetActive(false);
		base.Invoke("DelayLanguageSet", 0.1f);
		base.Invoke("LanguageCheckCall", 0.1f);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void FrenchLanguage()
	{
		PlayerPrefs.SetInt("LanguageSet", 3);
		this.SettingsBackground.SetActive(false);
		base.Invoke("DelayLanguageSet", 0.1f);
		base.Invoke("LanguageCheckCall", 0.1f);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void SpainishLanguage()
	{
		PlayerPrefs.SetInt("LanguageSet", 4);
		this.SettingsBackground.SetActive(false);
		base.Invoke("DelayLanguageSet", 0.1f);
		base.Invoke("LanguageCheckCall", 0.1f);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void GermanLanguage()
	{
		PlayerPrefs.SetInt("LanguageSet", 5);
		this.SettingsBackground.SetActive(false);
		base.Invoke("DelayLanguageSet", 0.1f);
		base.Invoke("LanguageCheckCall", 0.1f);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void JappaneseLanguage()
	{
		PlayerPrefs.SetInt("LanguageSet", 6);
		this.SettingsBackground.SetActive(false);
		base.Invoke("DelayLanguageSet", 0.1f);
		base.Invoke("LanguageCheckCall", 0.1f);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	private void DelayLanguageSet()
	{
		this.SettingsBackground.SetActive(true);
	}

	public void SoundSettingsBtnClick()
	{
		Color color = this.SoundSettingsBtn.color;
		color.a = 1f;
		this.SoundSettingsBtn.color = color;
		Color color2 = this.VibrationSettingsBtn.color;
		color2.a = 0f;
		this.VibrationSettingsBtn.color = color2;
		Color color3 = this.LanguageSettingsBtn.color;
		color3.a = 0f;
		this.LanguageSettingsBtn.color = color3;
		this.SoundBtnsPanels.SetActive(true);
		this.VibrationBtnsPanels.SetActive(false);
		this.LanguageBtnsPanels.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void VibrationSettingsBtnClick()
	{
		Color color = this.SoundSettingsBtn.color;
		color.a = 0f;
		this.SoundSettingsBtn.color = color;
		Color color2 = this.VibrationSettingsBtn.color;
		color2.a = 1f;
		this.VibrationSettingsBtn.color = color2;
		Color color3 = this.LanguageSettingsBtn.color;
		color3.a = 0f;
		this.LanguageSettingsBtn.color = color3;
		this.SoundBtnsPanels.SetActive(false);
		this.VibrationBtnsPanels.SetActive(true);
		this.LanguageBtnsPanels.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void LanguageSettingsBtnClick()
	{
		Color color = this.SoundSettingsBtn.color;
		color.a = 0f;
		this.SoundSettingsBtn.color = color;
		Color color2 = this.VibrationSettingsBtn.color;
		color2.a = 0f;
		this.VibrationSettingsBtn.color = color2;
		Color color3 = this.LanguageSettingsBtn.color;
		color3.a = 1f;
		this.LanguageSettingsBtn.color = color3;
		this.SoundBtnsPanels.SetActive(false);
		this.VibrationBtnsPanels.SetActive(false);
		this.LanguageBtnsPanels.SetActive(true);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void FBLikeBtn_Click()
	{
		Application.OpenURL("https://www.facebook.com/monstergamesproductions");
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	

	public void CarRaceBtn_Click()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.monstergamesproductions.ramp.moto.rider");
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Application.OpenURL("https://itunes.apple.com/us/app/multiplayer-fast-bike-racing/id1428273052?mt=8");
		}
	}

	public void BoatRaceBtn_Click()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.monstergamesproductions.extreme.power.boat.racing");
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Application.OpenURL("https://itunes.apple.com/us/app/3d-boat-racing-simulator-2018/id1326126127?ls=1&mt=8");
		}
	}

	public void MazeBtn_Click()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.monstergamesproductions.formula.racing");
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Application.OpenURL("https://itunes.apple.com/us/app/grand-formula-racing-pro/id1392881154?ls=1&mt=8");
		}
	}

	public void TrailerParkingBtn_Click()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.monstergamesproductions.water.car.racer.surfer");
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Application.OpenURL("https://itunes.apple.com/us/app/trailer-car-parking-challenge/id1367359094?mt=8");
		}
	}

	private void OnApplicationPause(bool isPause)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (isPause)
			{
				Time.timeScale = 0f;
			}
			else
			{
				Time.timeScale = 1f;
			}
		}
	}

	public void RightClick_Btn()
	{
		Debug.Log("ShowRight");
		HuaWeiADManager.ShowRight();
		if (this.CarCounter >= 6)
		{
			this.CarCounter = 6;
		}
		else
		{
			this.CarCounter++;
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void LeftClick_Btn()
	{
		Debug.Log("ShowRight");
		HuaWeiADManager.ShowRight();
		if (this.CarCounter <= 0)
		{
			this.CarCounter = 0;
		}
		else
		{
			this.CarCounter--;
		}
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void CarSelectGo_Btn()
	{
		PlayerPrefs.SetInt("SelTrainDB", this.CarCounter);
		this.GaragePanel.SetActive(false);
		this.MenuPanel.SetActive(true);
		this.EasyModeLevelsPanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.PurchasePanel.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void CarSelect_Back_Btn()
	{
		if (PlayerPrefs.GetInt("SelTrainDB") == 0)
		{
			this.CarCounter = 0;
		}
		else
		{
			this.CarCounter = PlayerPrefs.GetInt("SelTrainDB");
		}
		this.GaragePanel.SetActive(false);
		this.MenuPanel.SetActive(true);
		this.EasyModeLevelsPanel.SetActive(false);
		this.SettingsPanel.SetActive(false);
		this.ChallangeModeLevelsPanel.SetActive(false);
		this.RaceModeLevelsPanel.SetActive(false);
		this.PurchasePanel.SetActive(false);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void CarBuyBtn_Click()
	{
		if (this.CarCounter == 1 && PlayerPrefs.GetInt("HighScoreDB") >= 5000)
		{
			PlayerPrefs.SetInt("CarTwoPurcahsed", 1);
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") - 5000);
			this.CarSelectBtn.SetActive(true);
			this.CarBuyBtn.SetActive(false);
		}
		else if (this.CarCounter == 2 && PlayerPrefs.GetInt("HighScoreDB") >= 10000)
		{
			PlayerPrefs.SetInt("CarThreePurcahsed", 1);
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") - 10000);
			this.CarSelectBtn.SetActive(true);
			this.CarBuyBtn.SetActive(false);
		}
		else if (this.CarCounter == 3 && PlayerPrefs.GetInt("HighScoreDB") >= 30000)
		{
			PlayerPrefs.SetInt("CarFourPurcahsed", 1);
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") - 30000);
			this.CarSelectBtn.SetActive(true);
			this.CarBuyBtn.SetActive(false);
		}
		else if (this.CarCounter == 4 && PlayerPrefs.GetInt("HighScoreDB") >= 50000)
		{
			PlayerPrefs.SetInt("CarFivePurcahsed", 1);
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") - 50000);
			this.CarSelectBtn.SetActive(true);
			this.CarBuyBtn.SetActive(false);
		}
		else if (this.CarCounter == 5 && PlayerPrefs.GetInt("HighScoreDB") >= 80000)
		{
			PlayerPrefs.SetInt("CarSixPurcahsed", 1);
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") - 80000);
			this.CarSelectBtn.SetActive(true);
			this.CarBuyBtn.SetActive(false);
		}
		else if (this.CarCounter == 6 && PlayerPrefs.GetInt("HighScoreDB") >= 100000)
		{
			PlayerPrefs.SetInt("CarSevenPurcahsed", 1);
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") - 100000);
			this.CarSelectBtn.SetActive(true);
			this.CarBuyBtn.SetActive(false);
		}
		else
		{
			this.NotEnoughCash.SetActive(true);
		}
		this.ScoreText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	private void CarSelectionCheck()
	{
		if (this.CarCounter == 0)
		{
			this.Car1.SetActive(true);
			this.Car2.SetActive(false);
			this.Car3.SetActive(false);
			this.Car4.SetActive(false);
			this.Car5.SetActive(false);
			this.Car6.SetActive(false);
			this.Car7.SetActive(false);
			this.CarRightBtn.SetActive(true);
			this.CarLeftBtn.SetActive(false);
			this.CarPriceText.text = string.Empty;
			this.PriceTextObj.SetActive(false);
			this.CarSelectBtn.SetActive(true);
			this.CarBuyBtn.SetActive(false);
			this.PowerBar.fillAmount = 0.2f;
			this.SpeedBar.fillAmount = 0.5f;
			this.HandlingBar.fillAmount = 0.3f;
		}
		else if (this.CarCounter == 1)
		{
			this.Car1.SetActive(false);
			this.Car2.SetActive(true);
			this.Car3.SetActive(false);
			this.Car4.SetActive(false);
			this.Car5.SetActive(false);
			this.Car6.SetActive(false);
			this.Car7.SetActive(false);
			this.CarRightBtn.SetActive(true);
			this.CarLeftBtn.SetActive(true);
			if (PlayerPrefs.GetInt("CarTwoPurcahsed") == 1)
			{
				this.CarSelectBtn.SetActive(true);
				this.CarBuyBtn.SetActive(false);
				this.CarPriceText.text = string.Empty;
				this.PriceTextObj.SetActive(false);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			else
			{
				this.CarSelectBtn.SetActive(false);
				this.CarBuyBtn.SetActive(true);
				this.CarPriceText.text = "5000";
				this.PriceTextObj.SetActive(true);
				//this.Car2_PurchaseBtn.SetActive(true);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			this.PowerBar.fillAmount = 0.3f;
			this.SpeedBar.fillAmount = 0.2f;
			this.HandlingBar.fillAmount = 0.5f;
		}
		else if (this.CarCounter == 2)
		{
			this.Car1.SetActive(false);
			this.Car2.SetActive(false);
			this.Car3.SetActive(true);
			this.Car4.SetActive(false);
			this.Car5.SetActive(false);
			this.Car6.SetActive(false);
			this.Car7.SetActive(false);
			this.CarRightBtn.SetActive(true);
			this.CarLeftBtn.SetActive(true);
			if (PlayerPrefs.GetInt("CarThreePurcahsed") == 1)
			{
				this.CarSelectBtn.SetActive(true);
				this.CarBuyBtn.SetActive(false);
				this.CarPriceText.text = string.Empty;
				this.PriceTextObj.SetActive(false);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			else
			{
				this.CarSelectBtn.SetActive(false);
				this.CarBuyBtn.SetActive(true);
				this.CarPriceText.text = "10000";
				this.PriceTextObj.SetActive(true);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(true);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			this.PowerBar.fillAmount = 0.5f;
			this.SpeedBar.fillAmount = 0.4f;
			this.HandlingBar.fillAmount = 0.2f;
		}
		else if (this.CarCounter == 3)
		{
			this.Car1.SetActive(false);
			this.Car2.SetActive(false);
			this.Car3.SetActive(false);
			this.Car4.SetActive(true);
			this.Car5.SetActive(false);
			this.Car6.SetActive(false);
			this.Car7.SetActive(false);
			this.CarRightBtn.SetActive(true);
			this.CarLeftBtn.SetActive(true);
			if (PlayerPrefs.GetInt("CarFourPurcahsed") == 1)
			{
				this.CarSelectBtn.SetActive(true);
				this.CarBuyBtn.SetActive(false);
				this.CarPriceText.text = string.Empty;
				this.PriceTextObj.SetActive(false);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			else
			{
				this.CarSelectBtn.SetActive(false);
				this.CarBuyBtn.SetActive(true);
				this.CarPriceText.text = "30000";
				this.PriceTextObj.SetActive(true);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(true);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			this.PowerBar.fillAmount = 0.3f;
			this.SpeedBar.fillAmount = 0.6f;
			this.HandlingBar.fillAmount = 0.4f;
		}
		else if (this.CarCounter == 4)
		{
			this.Car1.SetActive(false);
			this.Car2.SetActive(false);
			this.Car3.SetActive(false);
			this.Car4.SetActive(false);
			this.Car5.SetActive(true);
			this.Car6.SetActive(false);
			this.Car7.SetActive(false);
			this.CarRightBtn.SetActive(true);
			this.CarLeftBtn.SetActive(true);
			if (PlayerPrefs.GetInt("CarFivePurcahsed") == 1)
			{
				this.CarSelectBtn.SetActive(true);
				this.CarBuyBtn.SetActive(false);
				this.CarPriceText.text = string.Empty;
				this.PriceTextObj.SetActive(false);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			else
			{
				this.CarSelectBtn.SetActive(false);
				this.CarBuyBtn.SetActive(true);
				this.CarPriceText.text = "50000";
				this.PriceTextObj.SetActive(true);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(true);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			this.PowerBar.fillAmount = 0.5f;
			this.SpeedBar.fillAmount = 0.7f;
			this.HandlingBar.fillAmount = 0.6f;
		}
		else if (this.CarCounter == 5)
		{
			this.Car1.SetActive(false);
			this.Car2.SetActive(false);
			this.Car3.SetActive(false);
			this.Car4.SetActive(false);
			this.Car5.SetActive(false);
			this.Car6.SetActive(true);
			this.Car7.SetActive(false);
			this.CarRightBtn.SetActive(true);
			this.CarLeftBtn.SetActive(true);
			if (PlayerPrefs.GetInt("CarSixPurcahsed") == 1)
			{
				this.CarSelectBtn.SetActive(true);
				this.CarBuyBtn.SetActive(false);
				this.CarPriceText.text = string.Empty;
				this.PriceTextObj.SetActive(false);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			else
			{
				this.CarSelectBtn.SetActive(false);
				this.CarBuyBtn.SetActive(true);
				this.CarPriceText.text = "80000";
				this.PriceTextObj.SetActive(true);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(true);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			this.PowerBar.fillAmount = 0.75f;
			this.SpeedBar.fillAmount = 0.8f;
			this.HandlingBar.fillAmount = 0.65f;
		}
		else if (this.CarCounter == 6)
		{
			this.Car1.SetActive(false);
			this.Car2.SetActive(false);
			this.Car3.SetActive(false);
			this.Car4.SetActive(false);
			this.Car5.SetActive(false);
			this.Car6.SetActive(false);
			this.Car7.SetActive(true);
			this.CarRightBtn.SetActive(false);
			this.CarLeftBtn.SetActive(true);
			if (PlayerPrefs.GetInt("CarSevenPurcahsed") == 1)
			{
				this.CarSelectBtn.SetActive(true);
				this.CarBuyBtn.SetActive(false);
				this.CarPriceText.text = string.Empty;
				this.PriceTextObj.SetActive(false);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(false);
			}
			else
			{
				this.CarSelectBtn.SetActive(false);
				this.CarBuyBtn.SetActive(true);
				this.CarPriceText.text = "100000";
				this.PriceTextObj.SetActive(true);
				//this.Car2_PurchaseBtn.SetActive(false);
				//this.Car3_PurchaseBtn.SetActive(false);
				//this.Car4_PurchaseBtn.SetActive(false);
				//this.Car5_PurchaseBtn.SetActive(false);
				//this.Car6_PurchaseBtn.SetActive(false);
				//this.Car7_PurchaseBtn.SetActive(true);
			}
			this.PowerBar.fillAmount = 0.8f;
			this.SpeedBar.fillAmount = 0.9f;
			this.HandlingBar.fillAmount = 0.7f;
		}
	}

	private void LanguageCheckCall()
	{
		if (PlayerPrefs.GetInt("LanguageSet") == 0)
		{
			this.EnglishLanguageBtn.image.overrideSprite = this.SoundSelectYes;
			this.ChineseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.HindiLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.FrenchLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.SpainishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.GermanLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.JapaneseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.EnglishLanguageBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
			this.ChineseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.HindiLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.FrenchLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.SpainishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.GermanLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.JapaneseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 1)
		{
			this.EnglishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.ChineseLanguageBtn.image.overrideSprite = this.SoundSelectYes;
			this.HindiLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.FrenchLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.SpainishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.GermanLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.JapaneseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.EnglishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.ChineseLanguageBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
			this.HindiLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.FrenchLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.SpainishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.GermanLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.JapaneseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 2)
		{
			this.EnglishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.ChineseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.HindiLanguageBtn.image.overrideSprite = this.SoundSelectYes;
			this.FrenchLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.SpainishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.GermanLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.JapaneseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.EnglishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.ChineseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.HindiLanguageBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
			this.FrenchLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.SpainishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.GermanLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.JapaneseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 3)
		{
			this.EnglishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.ChineseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.HindiLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.FrenchLanguageBtn.image.overrideSprite = this.SoundSelectYes;
			this.SpainishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.GermanLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.JapaneseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.EnglishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.ChineseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.HindiLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.FrenchLanguageBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
			this.SpainishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.GermanLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.JapaneseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 4)
		{
			this.EnglishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.ChineseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.HindiLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.FrenchLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.SpainishLanguageBtn.image.overrideSprite = this.SoundSelectYes;
			this.GermanLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.JapaneseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.EnglishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.ChineseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.HindiLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.FrenchLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.SpainishLanguageBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
			this.GermanLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.JapaneseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 5)
		{
			this.EnglishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.ChineseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.HindiLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.FrenchLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.SpainishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.GermanLanguageBtn.image.overrideSprite = this.SoundSelectYes;
			this.JapaneseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.EnglishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.ChineseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.HindiLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.FrenchLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.SpainishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.GermanLanguageBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
			this.JapaneseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else if (PlayerPrefs.GetInt("LanguageSet") == 6)
		{
			this.EnglishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.ChineseLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.HindiLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.FrenchLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.SpainishLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.GermanLanguageBtn.image.overrideSprite = this.SoundUnSelectNo;
			this.JapaneseLanguageBtn.image.overrideSprite = this.SoundSelectYes;
			this.EnglishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.ChineseLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.HindiLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.FrenchLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.SpainishLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.GermanLanguageBtnScale.transform.localScale = new Vector3(1f, 1f, 1f);
			this.JapaneseLanguageBtnScale.transform.localScale = new Vector3(2f, 2f, 1f);
		}
	}

	public GameObject LoadingPanel;

	public GameObject AchivementPanel;

	public GameObject AnimaPanel;

	public GameObject DronAnim;

	public GameObject MainCam;

	public GameObject MenuPanel;

	public GameObject GaragePanel;

	public GameObject EasyModeLevelsPanel;

	public GameObject ChallangeModeLevelsPanel;

	public GameObject RaceModeLevelsPanel;

	public GameObject SettingsPanel;

	public GameObject SettingsBackground;

	public GameObject PurchasePanel;

	private int LevelCounter;

	public AudioClip btn_click;

	public Sprite SoundSelectYes;

	public Sprite SoundSelectNo;

	public Sprite SoundUnSelectYes;

	public Sprite SoundUnSelectNo;

	public Button SoundYesBtn;

	public Button SoundNoBtn;

	public Button VibrationYesBtn;

	public Button VibrationNoBtn;

	public Button EnglishLanguageBtn;

	public Button ChineseLanguageBtn;

	public Button HindiLanguageBtn;

	public Button FrenchLanguageBtn;

	public Button SpainishLanguageBtn;

	public Button GermanLanguageBtn;

	public Button JapaneseLanguageBtn;

	public GameObject SoundYesBtnScale;

	public GameObject SoundNoBtnScale;

	public GameObject VibrationYesBtnScale;

	public GameObject VibrationNoBtnScale;

	public GameObject EnglishLanguageBtnScale;

	public GameObject ChineseLanguageBtnScale;

	public GameObject HindiLanguageBtnScale;

	public GameObject FrenchLanguageBtnScale;

	public GameObject SpainishLanguageBtnScale;

	public GameObject GermanLanguageBtnScale;

	public GameObject JapaneseLanguageBtnScale;

	public GameObject SoundBtnsPanels;

	public GameObject VibrationBtnsPanels;

	public GameObject LanguageBtnsPanels;

	public Image SoundSettingsBtn;

	public Image VibrationSettingsBtn;

	public Image LanguageSettingsBtn;

	public static bool LoadAd;

	public static SystemLanguage SL;

	public GameObject ProffesionalAchive;

	public GameObject ExpertAchive;

	public GameObject LegendaryAchive;

	private string HighScore_leaderBoardID = "com.monstergamesproductions.train.sim.highscore";

	private string MaxPassangerDrop_leaderBoardID = "com.monstergamesproductions.train.sim.passangerdrop";

	public Text ScoreText;

	[Header("Car  Selection")]
	private int CarCounter;

	public GameObject Car1;

	public GameObject Car2;

	public GameObject Car3;

	public GameObject Car4;

	public GameObject Car5;

	public GameObject Car6;

	public GameObject Car7;

	public GameObject Car2_PurchaseBtn;

	public GameObject Car3_PurchaseBtn;

	public GameObject Car4_PurchaseBtn;

	public GameObject Car5_PurchaseBtn;

	public GameObject Car6_PurchaseBtn;

	public GameObject Car7_PurchaseBtn;

	public GameObject CarRightBtn;

	public GameObject CarLeftBtn;

	public Image PowerBar;

	public Image SpeedBar;

	public Image HandlingBar;

	public GameObject CarSelectBtn;

	public GameObject CarBuyBtn;

	public Text CarPriceText;

	public GameObject PriceTextObj;

	public GameObject NotEnoughCash;

	public static bool LoginBool;

	private string HighScoreleaderBoardIDAndroid = "CgkIltPB_fscEAIQAA";

	private string MaxPassangerDropleaderBoardIDAndroid = "CgkIltPB_fscEAIQAQ";

	private string MaxLevelFailedleaderBoardIDAndroid = "CgkIltPB_fscEAIQAg";

	private string RookieAchIDAndroid = "CgkIltPB_fscEAIQAw";

	private string ProAchIDAndroid = "CgkIltPB_fscEAIQBA";

	private string EliteAchIDAndroid = "CgkIltPB_fscEAIQBQ";

	public GameObject ModeLock;

	public GameObject ModeLockDialogue;

	public static bool StartAnimBool;


}
