//// dnSpy decompiler from Assembly-CSharp.dll class: IAPGamePlayScriptt
//using System;
//using UnityEngine;
//using UnityEngine.UI;

//public class IAPGamePlayScriptt : MonoBehaviour
//{
//	private void OnEnable()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			this.INAPPScriptt = (INAPP)UnityEngine.Object.FindObjectOfType(typeof(INAPP));
//			this.UnlockCareerLevelsPriceText.text = PlayerPrefs.GetString("CareerPriceDB").ToString();
//			this.UnlockChallengeLevelsPriceText.text = PlayerPrefs.GetString("ChallengePriceDB");
//			this.NoAdsPriceText.text = PlayerPrefs.GetString("NoAdsPriceDB");
//			this.UnlockTrain1PriceText.text = PlayerPrefs.GetString("Train1PriceDB");
//			this.UnlockTrain2PriceText.text = PlayerPrefs.GetString("Train2PriceDB");
//			this.UnlockTrain3PriceText.text = PlayerPrefs.GetString("Train3PriceDB");
//			this.UnlockTrain4PriceText.text = PlayerPrefs.GetString("Train4PriceDB");
//			this.UnlockTrain5PriceText.text = PlayerPrefs.GetString("Train5PriceDB");
//		}
//		else
//		{
//			base.gameObject.SetActive(false);
//		}
//	}

//	private void FixedUpdate()
//	{
//		if (PlayerPrefs.GetInt("NoAdsPurchase") == 1)
//		{
//			this.NoAdsStoreBtn.SetActive(false);
//		}
//		else
//		{
//			this.NoAdsStoreBtn.SetActive(true);
//		}
//		if (PlayerPrefs.GetInt("ChallengeLvlPurchased") == 1)
//		{
//			this.UnlockChallengeLevelsBtn.SetActive(false);
//		}
//		else
//		{
//			this.UnlockChallengeLevelsBtn.SetActive(true);
//		}
//		if (PlayerPrefs.GetInt("CareerLvlPurchased") == 1)
//		{
//			this.UnlockCareerLevelsBtn.SetActive(false);
//		}
//		else
//		{
//			this.UnlockCareerLevelsBtn.SetActive(true);
//		}
//		if (PlayerPrefs.GetInt("CarTwoPurcahsed") == 1)
//		{
//			this.UnlockTrain1Btn.SetActive(false);
//		}
//		else
//		{
//			this.UnlockTrain1Btn.SetActive(true);
//		}
//		if (PlayerPrefs.GetInt("CarThreePurcahsed") == 1)
//		{
//			this.UnlockTrain2Btn.SetActive(false);
//		}
//		else
//		{
//			this.UnlockTrain2Btn.SetActive(true);
//		}
//		if (PlayerPrefs.GetInt("CarFivePurcahsed") == 1)
//		{
//			this.UnlockTrain3Btn.SetActive(false);
//		}
//		else
//		{
//			this.UnlockTrain3Btn.SetActive(true);
//		}
//		if (PlayerPrefs.GetInt("CarSixPurcahsed") == 1)
//		{
//			this.UnlockTrain4Btn.SetActive(false);
//		}
//		else
//		{
//			this.UnlockTrain4Btn.SetActive(true);
//		}
//		if (PlayerPrefs.GetInt("CarSevenPurcahsed") == 1)
//		{
//			this.UnlockTrain5Btn.SetActive(false);
//		}
//		else
//		{
//			this.UnlockTrain5Btn.SetActive(true);
//		}
//	}

//	public void Train2_Buy_Btn_Click()
//	{
//		this.INAPPScriptt.UnlockTrain1_Buy_BTn_Click();
//	}

//	public void Train3_Buy_Btn_Click()
//	{
//		this.INAPPScriptt.UnlockTrain2_Buy_BTn_Click();
//	}

//	public void Train5_Buy_Btn_Click()
//	{
//		this.INAPPScriptt.UnlockTrain3_Buy_BTn_Click();
//	}

//	public void Train6_Buy_Btn_Click()
//	{
//		this.INAPPScriptt.UnlockTrain4_Buy_BTn_Click();
//	}

//	public void Train7_Buy_Btn_Click()
//	{
//		this.INAPPScriptt.UnlockTrain5_Buy_BTn_Click();
//	}

//	public void BuyNoAdsProduct_Click()
//	{
//		this.INAPPScriptt.BuyNoAdsProduct_Click();
//	}

//	public void UnlockCareerLevels_Buy_BTn_Click()
//	{
//		this.INAPPScriptt.UnlockCareerLevels_Buy_BTn_Click();
//	}

//	public void UnlockChallengeLevels_Buy_BTn_Click()
//	{
//		this.INAPPScriptt.UnlockChallengeLevels_Buy_BTn_Click();
//	}



//	[Header("Prices Text")]
//	public Text UnlockCareerLevelsPriceText;

//	public Text UnlockChallengeLevelsPriceText;

//	public Text NoAdsPriceText;

//	public Text UnlockTrain1PriceText;

//	public Text UnlockTrain2PriceText;

//	public Text UnlockTrain3PriceText;

//	public Text UnlockTrain4PriceText;

//	public Text UnlockTrain5PriceText;

//	[Header("IAP Buttons Text")]
//	public GameObject NoAdsStoreBtn;

//	public GameObject UnlockChallengeLevelsBtn;

//	public GameObject UnlockCareerLevelsBtn;

//	public GameObject UnlockTrain1Btn;

//	public GameObject UnlockTrain2Btn;

//	public GameObject UnlockTrain3Btn;

//	public GameObject UnlockTrain4Btn;

//	public GameObject UnlockTrain5Btn;
//}
