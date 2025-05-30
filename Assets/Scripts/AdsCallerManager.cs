//// dnSpy decompiler from Assembly-CSharp.dll class: AdsCallerManager
//using System;
//using System.Collections;
//using GoogleMobileAds.Api;
//using UnityEngine;
//using UnityEngine.Advertisements;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class AdsCallerManager : MonoBehaviour
//{
//	public static string OutputMessage
//	{
//		set
//		{
//			AdsCallerManager.outputMessage = value;
//		}
//	}

//	public static AdsCallerManager Instance
//	{
//		get
//		{
//			if (AdsCallerManager._instance == null)
//			{
//				AdsCallerManager._instance = UnityEngine.Object.FindObjectOfType<AdsCallerManager>();
//				UnityEngine.Object.DontDestroyOnLoad(AdsCallerManager._instance.gameObject);
//			}
//			return AdsCallerManager._instance;
//		}
//	}

//	private void Awake()
//	{
//		if (Application.platform == RuntimePlatform.Android)
//		{
//			this.AdmobAppId = this.Android_AdmobAppId;
//			this.AdmobBannerAdId = this.Android_AdmobBannerAdId;
//			this.AdmobIntestritialAdId = this.Android_AdmobIntestritialAdId;
//			this.RewardedAdmobAdID = this.Android_RewardedAdmobAdID;
//			this.UnityAdId = this.Android_UnityAdId;
//		}
//		else if (Application.platform == RuntimePlatform.IPhonePlayer)
//		{
//			this.AdmobAppId = this.ios_AdmobAppId;
//			this.AdmobBannerAdId = this.ios_AdmobBannerAdId;
//			this.AdmobIntestritialAdId = this.ios_AdmobIntestritialAdId;
//			this.RewardedAdmobAdID = this.ios_RewardedAdmobAdID;
//			this.UnityAdId = this.ios_UnityAdId;
//		}
//		PlayerPrefs.SetInt("RewardWacthed", 0);
//		if (AdsCallerManager._instance == null)
//		{
//			AdsCallerManager._instance = this;
//			UnityEngine.Object.DontDestroyOnLoad(this);
//		}
//		else if (this != AdsCallerManager._instance)
//		{
//			UnityEngine.Object.Destroy(base.gameObject);
//		}
//	}

//	public void Start()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			UnityEngine.Debug.Log("Internet Available");
//			MobileAds.Initialize(this.AdmobAppId);
//			this.rewardBasedVideo = RewardBasedVideoAd.Instance;
//			this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
//			this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
//			this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
//			this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
//			this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
//			this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
//			this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;
//			Advertisement.Initialize(this.UnityAdId, false);
//			this.RequestRewardBasedVideo();
//			if (PlayerPrefs.GetInt("NoAdsPurchase") == 0)
//			{
//				this.RequestInterstitial();
//			}
//		}
//	}

//	public void Update()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable && PlayerPrefs.GetInt("PServicesDB") == 0 && PlayerPrefs.GetInt("NoAdsPurchase") == 0)
//		{
//			if (PlayerPrefs.GetInt("LoadAdCallDB") == 1)
//			{
//				this.LoadAdCall();
//				PlayerPrefs.SetInt("LoadAdCallDB", 0);
//			}
//			else if (PlayerPrefs.GetInt("PauseAdCallDB") == 1)
//			{
//				this.PauseAdCall();
//				PlayerPrefs.SetInt("PauseAdCallDB", 0);
//			}
//			else if (PlayerPrefs.GetInt("LvlCompleteAdCallDB") == 1)
//			{
//				this.LevelCompleteAdCall();
//				PlayerPrefs.SetInt("LvlCompleteAdCallDB", 0);
//			}
//			else if (PlayerPrefs.GetInt("LvlFailedAdCallDB") == 1)
//			{
//				this.LevelFailedAdCall();
//				PlayerPrefs.SetInt("LvlFailedAdCallDB", 0);
//			}
//		}
//		if (PlayerPrefs.GetInt("RewardAdCallDB") == 1)
//		{
//			this.RewardedAdShowMultiple();
//			PlayerPrefs.SetInt("RewardAdCallDB", 0);
//		}
//	}

//	public void IntestritialAdCall()
//	{
//		if (PlayerPrefs.GetInt("PServicesDB") == 0 && PlayerPrefs.GetInt("NoAdsPurchase") == 0 && this.interstitial.IsLoaded())
//		{
//			this.interstitial.Show();
//			this.RequestInterstitial();
//		}
//	}

//	public void LoadAdCall()
//	{
//		if (PlayerPrefs.GetInt("PServicesDB") == 0 && PlayerPrefs.GetInt("NoAdsPurchase") == 0 && this.interstitial.IsLoaded())
//		{
//			this.interstitial.Show();
//			this.RequestInterstitial();
//		}
//	}

//	public void LevelCompleteAdCall()
//	{
//		if (PlayerPrefs.GetInt("PServicesDB") == 0 && PlayerPrefs.GetInt("NoAdsPurchase") == 0)
//		{
//			if (PlayerPrefs.GetInt("CAdcallDB") == 0)
//			{
//				if (this.interstitial.IsLoaded())
//				{
//					this.interstitial.Show();
//					this.RequestInterstitial();
//				}
//				else if (Advertisement.IsReady("video"))
//				{
//					Advertisement.Show("video");
//				}
//				PlayerPrefs.SetInt("CAdcallDB", 1);
//			}
//			else if (PlayerPrefs.GetInt("CAdcallDB") == 1)
//			{
//				if (Advertisement.IsReady("video"))
//				{
//					Advertisement.Show("video");
//				}
//				else if (this.interstitial.IsLoaded())
//				{
//					this.interstitial.Show();
//					this.RequestInterstitial();
//				}
//				PlayerPrefs.SetInt("CAdcallDB", 0);
//			}
//		}
//	}

//	public void LevelFailedAdCall()
//	{
//		if (PlayerPrefs.GetInt("PServicesDB") == 0 && PlayerPrefs.GetInt("NoAdsPurchase") == 0)
//		{
//			if (PlayerPrefs.GetInt("FAdcallDB") == 0)
//			{
//				if (Advertisement.IsReady("video"))
//				{
//					Advertisement.Show("video");
//				}
//				else if (this.interstitial.IsLoaded())
//				{
//					this.interstitial.Show();
//					this.RequestInterstitial();
//				}
//				PlayerPrefs.SetInt("FAdcallDB", 1);
//			}
//			else if (PlayerPrefs.GetInt("FAdcallDB") == 1)
//			{
//				if (this.interstitial.IsLoaded())
//				{
//					this.interstitial.Show();
//					this.RequestInterstitial();
//				}
//				else if (Advertisement.IsReady("video"))
//				{
//					Advertisement.Show("video");
//				}
//				PlayerPrefs.SetInt("FAdcallDB", 0);
//			}
//		}
//	}

//	public void PauseAdCall()
//	{
//		if (PlayerPrefs.GetInt("PServicesDB") == 0 && PlayerPrefs.GetInt("NoAdsPurchase") == 0)
//		{
//			if (PlayerPrefs.GetInt("PAdcallDB") == 0)
//			{
//				if (this.interstitial.IsLoaded())
//				{
//					this.interstitial.Show();
//					this.RequestInterstitial();
//				}
//				else if (Advertisement.IsReady("video"))
//				{
//					Advertisement.Show("video");
//				}
//				PlayerPrefs.SetInt("PAdcallDB", 1);
//			}
//			else if (PlayerPrefs.GetInt("PAdcallDB") == 1)
//			{
//				if (Advertisement.IsReady("video"))
//				{
//					Advertisement.Show("video");
//				}
//				else if (this.interstitial.IsLoaded())
//				{
//					this.interstitial.Show();
//					this.RequestInterstitial();
//				}
//				PlayerPrefs.SetInt("PAdcallDB", 0);
//			}
//		}
//	}

//	public void RewardedAdShowMultiple()
//	{
//		if (this.rewardBasedVideo.IsLoaded())
//		{
//			this.rewardBasedVideo.Show();
//		}
//		else
//		{
//			this.ShowAd("rewardedVideo");
//		}
//	}

//	public void ShowAd(string zone = "rewardedVideo")
//	{
//		ShowOptions showOptions = new ShowOptions();
//		showOptions.resultCallback = new Action<ShowResult>(this.AdCallbackhandler);
//		if (Advertisement.IsReady(zone))
//		{
//			Advertisement.Show(zone, showOptions);
//		}
//		else
//		{
//			if (SceneManager.GetActiveScene().buildIndex == 1)
//			{
//				this.VideoNotAvailable_Dialogue.SetActive(true);
//				this.FreeCoins_Dialogue.SetActive(false);
//			}
//			PlayerPrefs.SetInt("RewardWacthed", 2);
//		}
//	}

//	private void AdCallbackhandler(ShowResult result)
//	{
//		if (result != ShowResult.Finished)
//		{
//			if (result != ShowResult.Skipped)
//			{
//				if (result == ShowResult.Failed)
//				{
//					UnityEngine.Debug.Log("I swear this has never happened to me before");
//					PlayerPrefs.SetInt("RewardWacthed", 2);
//					if (SceneManager.GetActiveScene().buildIndex == 1)
//					{
//						this.VideoNotAvailable_Dialogue.SetActive(true);
//						this.FreeCoins_Dialogue.SetActive(false);
//					}
//					Advertisement.Initialize(this.UnityAdId, false);
//					this.RequestRewardBasedVideo();
//				}
//			}
//			else
//			{
//				UnityEngine.Debug.Log("Ad skipped. Son, I am dissapointed in you");
//				PlayerPrefs.SetInt("RewardWacthed", 2);
//				if (SceneManager.GetActiveScene().buildIndex == 1)
//				{
//					this.VideoNotAvailable_Dialogue.SetActive(true);
//					this.FreeCoins_Dialogue.SetActive(false);
//				}
//			}
//		}
//		else
//		{
//			PlayerPrefs.SetInt("RewardWacthed", 1);
//			if (SceneManager.GetActiveScene().buildIndex == 1)
//			{
//				PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 500);
//				this.ScoreText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
//				this.VideoNotAvailable_Dialogue.SetActive(false);
//				this.FreeCoins_Dialogue.SetActive(false);
//			}
//		}
//	}

//	private IEnumerator WaitForAd()
//	{
//		float currentTimeScale = Time.timeScale;
//		Time.timeScale = 0f;
//		yield return null;
//		while (Advertisement.isShowing)
//		{
//			yield return null;
//		}
//		Time.timeScale = currentTimeScale;
//		yield break;
//	}

//	private AdRequest CreateAdRequest()
//	{
//		return new AdRequest.Builder().AddTestDevice("SIMULATOR").AddTestDevice("0123456789ABCDEF0123456789ABCDEF").AddKeyword("game").SetGender(Gender.Male).SetBirthday(new DateTime(1985, 1, 1)).TagForChildDirectedTreatment(false).AddExtra("color_bg", "9B30FF").Build();
//	}

//	public void RequestBanner()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable && PlayerPrefs.GetInt("PServicesDB") == 0 && PlayerPrefs.GetInt("NoAdsPurchase") == 0)
//		{
//			if (this.BannerLoadedBool)
//			{
//				this.bannerView.Show();
//			}
//			else
//			{
//				if (this.bannerView != null)
//				{
//					this.bannerView.Destroy();
//				}
//				this.bannerView = new BannerView(this.AdmobBannerAdId, AdSize.Banner, AdPosition.TopRight);
//				this.bannerView.OnAdLoaded += this.HandleAdLoaded;
//				this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
//				this.bannerView.OnAdOpening += this.HandleAdOpened;
//				this.bannerView.OnAdClosed += this.HandleAdClosed;
//				this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;
//				this.bannerView.LoadAd(this.CreateAdRequest());
//			}
//		}
//	}

//	public void HideBanner()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable && PlayerPrefs.GetInt("PServicesDB") == 0 && PlayerPrefs.GetInt("NoAdsPurchase") == 0)
//		{
//			if (this.BannerLoadedBool)
//			{
//				if (this.bannerView != null)
//				{
//					this.bannerView.Hide();
//				}
//			}
//			else if (this.bannerView != null)
//			{
//				this.bannerView.Destroy();
//			}
//		}
//	}

//	private void RequestInterstitial()
//	{
//		if (this.interstitial != null)
//		{
//			this.interstitial.Destroy();
//		}
//		this.interstitial = new InterstitialAd(this.AdmobIntestritialAdId);
//		this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
//		this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
//		this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
//		this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
//		this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;
//		this.interstitial.LoadAd(this.CreateAdRequest());
//	}

//	private void RequestRewardBasedVideo()
//	{
//		this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), this.RewardedAdmobAdID);
//	}

//	private void ShowInterstitial()
//	{
//		if (this.interstitial.IsLoaded())
//		{
//			this.interstitial.Show();
//		}
//		else
//		{
//			MonoBehaviour.print("Interstitial is not ready yet");
//		}
//	}

//	private void ShowRewardBasedVideo()
//	{
//		if (this.rewardBasedVideo.IsLoaded())
//		{
//			this.rewardBasedVideo.Show();
//		}
//		else
//		{
//			MonoBehaviour.print("Reward based video ad is not ready yet");
//		}
//	}

//	public void HandleAdLoaded(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleAdLoaded event received");
//		this.BannerLoadedBool = true;
//	}

//	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//	{
//		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
//		this.BannerLoadedBool = false;
//	}

//	public void HandleAdOpened(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleAdOpened event received");
//	}

//	public void HandleAdClosed(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleAdClosed event received");
//	}

//	public void HandleAdLeftApplication(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleAdLeftApplication event received");
//	}

//	public void HandleInterstitialLoaded(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialLoaded event received");
//	}

//	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
//	}

//	public void HandleInterstitialOpened(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialOpened event received");
//	}

//	public void HandleInterstitialClosed(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialClosed event received");
//	}

//	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleInterstitialLeftApplication event received");
//	}

//	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
//	}

//	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
//	}

//	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
//	}

//	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
//	}

//	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
//	}

//	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
//	{
//		string type = args.Type;
//		MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + args.Amount.ToString() + " " + type);
//		PlayerPrefs.SetInt("RewardWacthed", 1);
//		if (SceneManager.GetActiveScene().buildIndex == 1)
//		{
//			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 500);
//			this.ScoreText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
//			this.VideoNotAvailable_Dialogue.SetActive(false);
//			this.FreeCoins_Dialogue.SetActive(false);
//		}
//	}

//	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
//	{
//		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
//	}

//	private static AdsCallerManager _instance;

//	private BannerView bannerView;

//	private InterstitialAd interstitial;

//	private RewardBasedVideoAd rewardBasedVideo;

//	private float deltaTime;

//	private static string outputMessage = string.Empty;

//	private string AdmobAppId;

//	private string AdmobBannerAdId;

//	private string AdmobIntestritialAdId;

//	private string RewardedAdmobAdID;

//	private string UnityAdId;

//	private bool BannerLoadedBool;

//	[Header("Android Ad id's")]
//	[SerializeField]
//	public string Android_AdmobAppId;

//	[SerializeField]
//	public string Android_AdmobBannerAdId;

//	[SerializeField]
//	public string Android_AdmobIntestritialAdId;

//	[SerializeField]
//	public string Android_RewardedAdmobAdID;

//	[SerializeField]
//	public string Android_UnityAdId;

//	[Header("ios Ad id's")]
//	[SerializeField]
//	public string ios_AdmobAppId;

//	[SerializeField]
//	public string ios_AdmobBannerAdId;

//	[SerializeField]
//	public string ios_AdmobIntestritialAdId;

//	[SerializeField]
//	public string ios_RewardedAdmobAdID;

//	[SerializeField]
//	public string ios_UnityAdId;

//	[Header("Dialogues Text")]
//	public GameObject FreeCoins_Dialogue;

//	public GameObject VideoNotAvailable_Dialogue;

//	public Text ScoreText;
//}
