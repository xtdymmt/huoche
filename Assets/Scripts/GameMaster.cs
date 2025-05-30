// dnSpy decompiler from Assembly-CSharp.dll class: GameMaster
using System;
using System.Collections;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.Curvy.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
	private void Awake()
	{
		Time.timeScale = 1f;
		this.MissionCompleteBool = false;
		AudioListener.pause = false;
		this.RaceBtnPanel.SetActive(true);
		this.RankShowText = GameObject.Find("Canvas/PanelRaceBtnsPanel/RankShow");
		this.DirectionCounter = 3;
		this.RightIndicator = GameObject.Find("Canvas/PanelRaceBtnsPanel/ArrowDirections/ArrowIndicators/RightArrow").GetComponent<Button>();
		this.LeftIndicator = GameObject.Find("Canvas/PanelRaceBtnsPanel/ArrowDirections/ArrowIndicators/LeftArrow").GetComponent<Button>();
		this.BtnRightIndicatorImg = this.RightIndicator.GetComponent<Image>();
		this.BtnLeftIndicatorImg = this.LeftIndicator.GetComponent<Image>();
		this.RightIndicatorImg = GameObject.Find("Canvas/PanelRaceBtnsPanel/ArrowDirections/RightImgg").GetComponent<Image>();
		this.LeftIndicatorImg = GameObject.Find("Canvas/PanelRaceBtnsPanel/ArrowDirections/LeftImgg").GetComponent<Image>();
		this.RightIndicatorImgAnim = this.RightIndicatorImg.GetComponent<Animator>();
		this.LeftIndicatorImgAnim = this.LeftIndicatorImg.GetComponent<Animator>();
		this.RightIndicatorImgObjjj = this.RightIndicatorImg.gameObject;
		this.LeftIndicatorImgObjjj = this.LeftIndicatorImg.gameObject;
		this.PlayerTrainPrefab = this.PlayerTrainArray[PlayerPrefs.GetInt("SelTrainDB")];
		this.PassangerTrain = UnityEngine.Object.Instantiate<GameObject>(this.PlayerTrainPrefab, this.PlayerTrainPrefab.transform.position, this.PlayerTrainPrefab.transform.rotation);
		//this.AdCallerScriptComboObj = (AdsCallerManager)UnityEngine.Object.FindObjectOfType(typeof(AdsCallerManager));
	}

	private void Start()
	{
		Time.timeScale = 1f;
		this.Levelcounter = SceneManager.GetActiveScene().buildIndex;
		Handheld.StopActivityIndicator();
		this.TrainMoveScript = (TrainMove)UnityEngine.Object.FindObjectOfType(typeof(TrainMove));
		this.TrainCollisionScriptScript = (TrainCollisionScript)UnityEngine.Object.FindObjectOfType(typeof(TrainCollisionScript));
		this.RightIndicator.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.TrainMoveRight();
		});
		this.LeftIndicator.GetComponent<Button>().onClick.AddListener(delegate()
		{
			this.TrainMoveLeft();
		});
		this.TargetObj = this.TrainMoveScript.TargetObj;
		this.ExitCamera0 = this.TrainMoveScript.ExitCamera0;
		this.PassangerEnter = this.TrainMoveScript.PassangerEnter;
		this.PassangerExit = this.TrainMoveScript.PassangerExit;
		this.PassangerEnter0 = this.TrainMoveScript.PassangerEnter0;
		this.StaticPassanger0 = this.TrainMoveScript.StaticPassanger0;
		this.PassangerEnterAgian0 = this.TrainMoveScript.PassangerEnterAgian0;
		this.PassangerExit0 = this.TrainMoveScript.PassangerExit0;
		this.TrainEngineController = this.TargetObj.GetComponent<SplineController>();
		if (SceneManager.GetActiveScene().buildIndex > 28 && SceneManager.GetActiveScene().buildIndex <= 33)
		{
			if (this.RankShowText != null)
			{
				this.RankShowText.SetActive(true);
			}
		}
		else if (this.RankShowText != null)
		{
			this.RankShowText.SetActive(false);
		}
		this.RaceBtnPanel.SetActive(false);
		this.DoorsOpenAll();
		this.TrainCollisionScriptScript.MissionFailedBool = false;
		PlayerPrefs.SetInt("Next", 0);
		this.PassangerEnter.SetActive(false);
		this.PassangerExit.SetActive(false);
		this.PassangerEnterAgian0.SetActive(false);
		this.PassangerExit0.SetActive(false);
		this.ExitCamera0.SetActive(false);
		base.Invoke("DelayLoadingFalse", 1f);
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			//this.AdCallerScriptComboObj.HideBanner();
			if (Application.platform == RuntimePlatform.Android)
			{
				if (SystemInfo.systemMemorySize > 1024 && !GameMaster.LoadAd)
				{
					PlayerPrefs.SetInt("LoadAdCallDB", 1);
				}
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer && SystemInfo.systemMemorySize > 512 && !GameMaster.LoadAd)
			{
				PlayerPrefs.SetInt("LoadAdCallDB", 1);
			}
		}
		this.InitBool = true;
	}

	private void DelayLoadingFalse()
	{
		this.LoadingPanel.SetActive(false);
	}

	private void FixedUpdate()
	{
		if (this.InitBool)
		{
			this.kph = this.TrainEngineController.Speed * 2.5f * 3.6f;
			this.KPH.text = string.Format("{0:000}", this.kph);
			this.DistnaceinKM = (float)((int)this.Distance) / 100f;
			this.StationDistance.text = string.Format("{0:00}", this.DistnaceinKM);
			this.FillKPH.fillAmount = this.TrainEngineController.Speed * 100f * 5f / 210f * Time.deltaTime;
		}
	}

	private void Update()
	{
		if (this.InitBool)
		{
			if (this.DirectionCounter == 0)
			{
				this.RightIndicatorImg.color = Color.red;
				this.LeftIndicatorImg.color = Color.green;
				this.BtnRightIndicatorImg.color = Color.red;
				this.BtnLeftIndicatorImg.color = Color.green;
				this.RightIndicatorImgAnim.enabled = false;
				this.LeftIndicatorImgAnim.enabled = true;
				this.RightIndicatorImgObjjj.transform.localScale = new Vector3(1f, 1f, 1f);
				if (this.TrainCollisionScriptScript.TrackChangeLefttBool)
				{
					this.TrackChangeObject[this.TrainCollisionScriptScript.i - 1].GetComponent<MDJunctionControl>().Toggle();
					this.TrainCollisionScriptScript.TrackChangeLefttBool = false;
				}
			}
			else if (this.DirectionCounter == 1)
			{
				this.RightIndicatorImg.color = Color.green;
				this.LeftIndicatorImg.color = Color.red;
				this.BtnRightIndicatorImg.color = Color.green;
				this.BtnLeftIndicatorImg.color = Color.red;
				this.RightIndicatorImgAnim.enabled = true;
				this.LeftIndicatorImgAnim.enabled = false;
				this.LeftIndicatorImgObjjj.transform.localScale = new Vector3(1f, 1f, 1f);
				if (this.TrainCollisionScriptScript.TrackChangeRightBool)
				{
					this.TrackChangeObject[this.TrainCollisionScriptScript.i - 1].GetComponent<MDJunctionControl>().Toggle();
					this.TrainCollisionScriptScript.TrackChangeRightBool = false;
				}
			}
			else if (this.DirectionCounter == 3)
			{
				this.RightIndicatorImg.color = Color.red;
				this.LeftIndicatorImg.color = Color.red;
				this.BtnRightIndicatorImg.color = Color.red;
				this.BtnLeftIndicatorImg.color = Color.red;
				this.RightIndicatorImgAnim.enabled = false;
				this.LeftIndicatorImgAnim.enabled = false;
				this.LeftIndicatorImgObjjj.transform.localScale = new Vector3(1f, 1f, 1f);
				this.RightIndicatorImgObjjj.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			if (this.RaceTrainColScript != null && this.TrainCollisionScriptScript != null)
			{
				if (this.RaceTrainColScript.AITraincounter == 1 && this.TrainCollisionScriptScript.PlayerTraincounter == 0)
				{
					UnityEngine.Debug.Log("Player Lost");
					this.PlayerWinCounter = 2;
				}
				else if (this.TrainCollisionScriptScript.PlayerTraincounter == 1 && this.RaceTrainColScript.AITraincounter == 0)
				{
					UnityEngine.Debug.Log("Player Wins");
					this.PlayerWinCounter = 1;
				}
			}
			if (this.TrainCollisionScriptScript.TrainStopBool && this.TrainCollisionScriptScript.MissionCompleteBool)
			{
				this.OpenDoorBtn.SetActive(true);
			}
			if (this.TrainCollisionScriptScript.TrainStopBoolMulti && this.TrainCollisionScriptScript.TrainStopCounter == 1)
			{
				this.OpenDoorBtn.SetActive(true);
			}
			if (this.TrainCollisionScriptScript.MissionFailedBool)
			{
				if (this.LevelFailedAd)
				{
					
						//this.AdCallerScriptComboObj.RequestBanner();
						AudioListener.pause = true;
						PlayerPrefs.SetInt("MaxFailedDB", PlayerPrefs.GetInt("MaxFailedDB") + 1);
						PlayerPrefs.SetInt("LvlFailedAdCallDB", 1);
						this.LevelFailedAd = false;
					Debug.Log("ShowLose");
					HuaWeiADManager.ShowLose();
				}
				this.RaceBtnPanel.SetActive(false);
				//#if UNITY_ANDROID && !UNITY_EDITOR
				//        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				//  {
				//    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
				//     {
				//        jo.Call("ShowLose");
				//     }
				//  } 

				//#endif
				
                this.MissionFailed.SetActive(true);
				Time.timeScale = 0f;
			}
			if (TrafficCarCollision.MissionFailedBool)
			{
				if (this.LevelFailedAd)
				{
					//this.AdCallerScriptComboObj.RequestBanner();
					AudioListener.pause = true;
					PlayerPrefs.SetInt("MaxFailedDB", PlayerPrefs.GetInt("MaxFailedDB") + 1);
					PlayerPrefs.SetInt("LvlFailedAdCallDB", 1);
					this.LevelFailedAd = false;
				}
				this.RaceBtnPanel.SetActive(false);
//#if UNITY_ANDROID && !UNITY_EDITOR
//        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
//  {
//    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
//     {
//        jo.Call("ShowLose");
//     }
//  } 
        
//#endif

                this.MissionFailed.SetActive(true);
				Time.timeScale = 0f;
			}
			if (SceneManager.GetActiveScene().buildIndex > 28 && SceneManager.GetActiveScene().buildIndex <= 33 && this.PlayerWinCounter == 2 && AiRaceTrain.counter == 2)
			{
				if (this.LevelFailedAd)
				{
					//this.AdCallerScriptComboObj.RequestBanner();
					AudioListener.pause = true;
					PlayerPrefs.SetInt("MaxFailedDB", PlayerPrefs.GetInt("MaxFailedDB") + 1);
					PlayerPrefs.SetInt("LvlFailedAdCallDB", 1);
					this.LevelFailedAd = false;
				}
				this.RaceBtnPanel.SetActive(false);
				//#if UNITY_ANDROID && !UNITY_EDITOR
				//        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				//  {
				//    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
				//     {
				//        jo.Call("ShowLose");
				//     }
				//  } 

				//#endif
				
				this.MissionFailed.SetActive(true);
				Time.timeScale = 0f;
			}
		}
	}

	public void DoorsOpenAll()
	{
		Debug.Log("ShowOpen");
		HuaWeiADManager.ShowOpen();
		this.TrainMoveScript.DoorOpenCall();
		if (this.TrainCollisionScriptScript.TrainStopBoolMulti && this.TrainCollisionScriptScript.TrainStopCounter == 1)
		{
			this.ExitCamera0.SetActive(true);
			this.MainStaticCamera.SetActive(false);
			this.RaceBtnPanel.SetActive(false);
			this.StationDistinationPointMulti.SetActive(false);
			this.MissionFailedColliderMulti.SetActive(false);
			this.PassangerEnter0.SetActive(false);
			this.StaticPassanger0.SetActive(false);
			this.PassangerExit0.SetActive(true);
			this.PassangerEnterAgian0.SetActive(true);
			this.PassangerEnter.SetActive(true);
			base.Invoke("TrainMoveAgain", 14f);
			this.TrainCollisionScriptScript.TrainStopCounter = 0;
		}
		else if (this.TrainCollisionScriptScript.TrainStopBool && SceneManager.GetActiveScene().buildIndex > 1 && SceneManager.GetActiveScene().buildIndex <= 28)
		{
			this.ExitCamBool = true;
			this.RaceBtnPanel.SetActive(false);
			this.StationDistinationPoint.SetActive(false);
			this.PassangerEnter.SetActive(false);
			this.PassangerExit.SetActive(true);
			this.MissionCompleteBool = true;
			base.Invoke("MissionCompleteMsg", 9f);
			this.TrainCollisionScriptScript.MissionCompleteBool = false;
		}
		else if (this.TrainCollisionScriptScript.TrainStopBool && SceneManager.GetActiveScene().buildIndex > 28 && SceneManager.GetActiveScene().buildIndex <= 33 && this.PlayerWinCounter == 1)
		{
			this.ExitCamBool = true;
			this.RaceBtnPanel.SetActive(false);
			this.StationDistinationPoint.SetActive(false);
			this.PassangerEnter.SetActive(false);
			this.PassangerExit.SetActive(true);
			this.MissionCompleteBool = true;
			base.Invoke("MissionCompleteMsg", 9f);
			this.TrainCollisionScriptScript.MissionCompleteBool = false;
		}
		this.OpenDoorBtn.SetActive(false);
	}

	private void MissionCompleteMsg()
	{
		//#if UNITY_ANDROID && !UNITY_EDITOR
		//        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//  {
		//    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
		//     {
		//        jo.Call("ShowWin");
		//     }
		//  } 

		//#endif
		Debug.Log("ShowWin");
		HuaWeiADManager.ShowWin();
        this.RaceBtnPanel.SetActive(false);
			this.MissionComplete.SetActive(true);
			if (this.LevelClearAd)
			{
				//this.AdCallerScriptComboObj.RequestBanner();

				AudioListener.pause = true;
				PlayerPrefs.SetInt("LvlCompleteAdCallDB", 1);
				if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex <= 12)
				{
					PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 500);
					this.ScoreText.text = "500";
				}
				else if (SceneManager.GetActiveScene().buildIndex > 12)
				{
					PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 1000);
					this.ScoreText.text = "1000";
				}
				PlayerPrefs.SetInt("PassDropDB", PlayerPrefs.GetInt("PassDropDB") + 12);
				if (SceneManager.GetActiveScene().buildIndex == 2)
				{
					PlayerPrefs.SetInt("UnlockedLevel2", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 3)
				{
					PlayerPrefs.SetInt("UnlockedLevel3", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 4)
				{
					PlayerPrefs.SetInt("UnlockedLevel4", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 5)
				{
					PlayerPrefs.SetInt("UnlockedLevel5", 1);
					if (PlayerPrefs.GetInt("Achive1DB") == 0)
					{
						PlayerPrefs.SetInt("Achive1DB", 1);
					}
				}
				else if (SceneManager.GetActiveScene().buildIndex == 6)
				{
					PlayerPrefs.SetInt("UnlockedLevel6", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 7)
				{
					PlayerPrefs.SetInt("UnlockedLevel7", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 8)
				{
					PlayerPrefs.SetInt("UnlockedLevel8", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 9)
				{
					PlayerPrefs.SetInt("UnlockedLevel9", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 10)
				{
					PlayerPrefs.SetInt("UnlockedLevel10", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 11)
				{
					PlayerPrefs.SetInt("UnlockedLevel11", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 12)
				{
					PlayerPrefs.SetInt("UnlockedLevel12", 1);
					if (PlayerPrefs.GetInt("Achive2DB") == 0)
					{
						PlayerPrefs.SetInt("Achive2DB", 1);
					}
				}
				else if (SceneManager.GetActiveScene().buildIndex == 13)
				{
					PlayerPrefs.SetInt("UnlockedLevel13", 1);
					PlayerPrefs.SetInt("UnlockedLevel13Star", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 14)
				{
					PlayerPrefs.SetInt("UnlockedLevel14", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 15)
				{
					PlayerPrefs.SetInt("UnlockedLevel15", 1);
					PlayerPrefs.SetInt("AchivementUnlocked", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 16)
				{
					PlayerPrefs.SetInt("UnlockedLevel16", 1);
				}
				if (SceneManager.GetActiveScene().buildIndex == 17)
				{
					PlayerPrefs.SetInt("UnlockedLevel17", 1);
					if (PlayerPrefs.GetInt("Achive3DB") == 0)
					{
						PlayerPrefs.SetInt("Achive3DB", 1);
					}
				}
				else if (SceneManager.GetActiveScene().buildIndex == 18)
				{
					PlayerPrefs.SetInt("UnlockedLevel18", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 19)
				{
					PlayerPrefs.SetInt("UnlockedLevel19", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 20)
				{
					PlayerPrefs.SetInt("UnlockedLevel20", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 21)
				{
					PlayerPrefs.SetInt("UnlockedLevel21", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 22)
				{
					PlayerPrefs.SetInt("UnlockedLevel22", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 23)
				{
					PlayerPrefs.SetInt("UnlockedLevel23", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 24)
				{
					PlayerPrefs.SetInt("UnlockedLevel24", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 25)
				{
					PlayerPrefs.SetInt("UnlockedLevel25", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 26)
				{
					PlayerPrefs.SetInt("UnlockedLevel26", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 27)
				{
					PlayerPrefs.SetInt("UnlockedLevel27", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 28)
				{
					PlayerPrefs.SetInt("UnlockedLevel28", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 29)
				{
					PlayerPrefs.SetInt("UnlockedLevel29", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 30)
				{
					PlayerPrefs.SetInt("UnlockedLevel30", 1);
					PlayerPrefs.SetInt("AchivementUnlocked", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 31)
				{
					PlayerPrefs.SetInt("UnlockedLevel31", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 32)
				{
					PlayerPrefs.SetInt("UnlockedLevel32", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 33)
				{
					PlayerPrefs.SetInt("UnlockedLevel33", 1);
				}
				else if (SceneManager.GetActiveScene().buildIndex == 34)
				{
					PlayerPrefs.SetInt("UnlockedLevel34", 1);
					PlayerPrefs.SetInt("AchivementUnlocked", 1);
				}
				this.LevelClearAd = false;
			}
		
	}

	private void TrainMoveAgain()
	{
		this.CloseDoorBtn.SetActive(true);
		this.MainStaticCamera.SetActive(true);
		this.ExitCamera0.SetActive(false);
	}

	public void DoorsClosedAll()
	{
		Debug.Log("ShowClose");
		HuaWeiADManager.ShowClose();
		if (this.TrainCollisionScriptScript.TrainStopBoolMulti)
		{
			AiRaceTrain.counter = 1;
			this.TargetObj = this.TargetObjMulti;
			this.PassangerExit0.SetActive(false);
			this.TrainMoveScript.ApplyBrakesBool = false;
			this.TrainCollisionScriptScript.TrainStopBoolMulti = false;
		}
		this.RaceBtnPanel.SetActive(true);
		this.TrainMoveScript.DoorCloseCall();
		this.CloseDoorBtn.SetActive(false);
	}

	public void Pause_Click()
	{
		Debug.Log("ShowPause");
        ADManagerRPK.Instance.ShowYS();
        HuaWeiADManager.ShowPause();
//#if UNITY_ANDROID && !UNITY_EDITOR
//        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
//  {
//    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
//     {
//        jo.Call("ShowPause");
//     }
//  } 
        
//#endif
        //this.AdCallerScriptComboObj.RequestBanner();
        PlayerPrefs.SetInt("PauseAdCallDB", 1);
		AudioListener.pause = true;
		this.RaceBtnPanel.SetActive(false);
		this.PausePanel.SetActive(true);
		Time.timeScale = 0f;
	}
	//public void ShowRestartButton_Click()
	//{
 //       Debug.Log("ShowRestart");
 //       MFADManager.ShowVideo = delegate
 //       {
	//		Restart_Click();
 //       };
 //   }
	public void Restart_Click()
	{
        HuaWeiADManager.ShowRestart();
        Debug.Log("ShowRestart");
        MFADManager.ShowVideo = delegate
		{
			RestarScript();
        };
	}
	//public void testClick()
	//{
 //       RestarScript();
 //   }
	public void RestarScript()
	{
        this.LoadingPanel.SetActive(true);
        AudioListener.pause = false;
        this.TrainCollisionScriptScript.MissionFailedBool = false;
        base.StartCoroutine(this.Load());
        if (PlayerPrefs.GetString("Sound Status") == "True")
        {
            base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
	public void Menu_Click()
	{
		Debug.Log("ShowMenu");
        ADManagerRPK.Instance.ShowYS();
        HuaWeiADManager.ShowMenu();
		GameMaster.LoadAd = false;
		this.LoadingPanel.SetActive(true);
		AudioListener.pause = false;
		this.TrainCollisionScriptScript.MissionFailedBool = false;
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
		base.StartCoroutine(this.Load());
		SceneManager.LoadScene(1);
	}

	public void Resume_Click()
	{
		//this.AdCallerScriptComboObj.HideBanner();
		AudioListener.pause = false;
		this.RaceBtnPanel.SetActive(true);
		this.PausePanel.SetActive(false);
		Time.timeScale = 1f;
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
	}

	public void Next()
	{
		Debug.Log("ShowNextLevel");
		HuaWeiADManager.ShowNextLevel();
		GameMaster.LoadAd = false;
		this.LoadingPanel.SetActive(true);
		if (PlayerPrefs.GetString("Sound Status") == "True")
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.btn_click, 1f);
		}
		base.StartCoroutine(this.Load());
		if (SceneManager.GetActiveScene().buildIndex >= 2 && SceneManager.GetActiveScene().buildIndex <= 12)
		{
			SceneManager.LoadScene(this.Levelcounter + 1);
		}
		else if (SceneManager.GetActiveScene().buildIndex == 13)
		{
			SceneManager.LoadScene(1);
		}
		else if (SceneManager.GetActiveScene().buildIndex >= 14 && SceneManager.GetActiveScene().buildIndex <= 24)
		{
			SceneManager.LoadScene(this.Levelcounter + 1);
		}
		else if (SceneManager.GetActiveScene().buildIndex == 25)
		{
			SceneManager.LoadScene(1);
		}
	}

	public void TrackSlider_PointerDownClick()
	{
	}

	public void TrackSlider_PointerUpClick()
	{
	}

	private IEnumerator Load()
	{
		Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
		Handheld.StartActivityIndicator();
		yield return new WaitForSeconds(0f);
		yield break;
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

	public void BrakesApply_On_Click_PoniterDown()
	{
		this.TrainMoveScript.BrakesApply_Click();
	}

	public void BrakesApplyOFF_Click_PointerUP()
	{
		this.TrainMoveScript.BrakesApplyOFF_Click();
	}

	public void Horn_Click()
	{
		this.TrainMoveScript.Horn_Click();
	}

	public void Horn_UnClick()
	{
		this.TrainMoveScript.Horn_UnClick();
	}

	public void RateUS_Click_Btn()
	{
		
	}

	public void RateUS_Cross_Btn()
	{
	
	}

	public void TrainMoveRight()
	{
		if (this.DirectionCounter == 1)
		{
			this.DirectionCounter = 3;
		}
		else
		{
			this.DirectionCounter = 1;
		}
	}

	public void TrainMoveLeft()
	{
		if (this.DirectionCounter == 0)
		{
			this.DirectionCounter = 3;
		}
		else
		{
			this.DirectionCounter = 0;
		}
	}

	[Header("Train Prefab")]
	public GameObject PlayerTrainPrefab;

	public GameObject[] PlayerTrainArray;

	[Header("Passanger Train")]
	public GameObject PassangerTrain;

	public GameObject PassangerTrain_BrakeBtn;

	public SplineController TrainEngineController;

	public TrainCollisionScript TrainCollisionScriptScript;

	public GameObject ExitCamera0;

	public GameObject PassangerTrain_Stations;

	public GameObject PassangerTrain_HornBtn;

	[Header("OverAll Settings")]
	public Text KPH;

	public Image FillKPH;

	private float kph;

	public GameObject[] TrackChangeObject;

	private float Distance;

	private float DistnaceinKM;

	private float TotalDistnace;

	public Transform TargetObj;

	public Text StationDistance;

	public GameObject RaceBtnPanel;

	public GameObject CloseDoorBtn;

	public GameObject OpenDoorBtn;

	public RaceTrainCol RaceTrainColScript;

	public GameObject PassangerEnter;

	public GameObject PassangerExit;

	public GameObject PassangerEnter0;

	public GameObject StaticPassanger0;

	public GameObject PassangerEnterAgian0;

	public GameObject PassangerExit0;

	public GameObject MainStaticCamera;

	public GameObject MissionFailed;

	public GameObject MissionComplete;

	public GameObject PausePanel;

	public GameObject StationDistinationPoint;

	public GameObject StationDistinationPointMulti;

	public GameObject MissionFailedColliderMulti;

	public Transform TargetObjMulti;

	public bool ExitCamBool;

	private bool LevelClearAd = true;

	public bool MissionCompleteBool;

	public AudioClip btn_click;

	public bool LevelFailedAd = true;

	private int PlayerWinCounter;

	public GameObject RankShowText;

	public TrainMove TrainMoveScript;

	private bool InitBool;

	[Header("Train Turn Buttons")]
	public Button RightIndicator;

	public Button LeftIndicator;

	public Image BtnRightIndicatorImg;

	public Image BtnLeftIndicatorImg;

	public Image RightIndicatorImg;

	public Image LeftIndicatorImg;

	public Animator RightIndicatorImgAnim;

	public Animator LeftIndicatorImgAnim;

	public GameObject RightIndicatorImgObjjj;

	public GameObject LeftIndicatorImgObjjj;

	private int DirectionCounter = 3;

	[Header("加载中")]
	public GameObject LoadingPanel;

	//public GameObject RateUsPanel;

	public Text ScoreText;

	private int Levelcounter;



	public static bool LoadAd;

	//public Text testText;
}
