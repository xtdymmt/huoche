//// dnSpy decompiler from Assembly-CSharp.dll class: PurchaseCheck
//using System;
//using UnityEngine;
//using UnityEngine.UI;

//public class PurchaseCheck : MonoBehaviour
//{
//	private void Start()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			this.LeadrboardBtnn.SetActive(true);
//			this.SocialBtnns.SetActive(true);
//			this.StoreObj.SetActive(true);
//			this.IAP_Gmbobj.SetActive(true);
//			this.StoreNotAvailbleObj.SetActive(false);
//			this.FreeCoinsBTn.SetActive(true);
//			if (PlayerPrefs.GetInt("CarFourPurcahsed") == 0 && PlayerPrefs.GetInt("NoAdsPurchase") == 0)
//			{
//				this.StarterKitMenuBtn.SetActive(true);
//			}
//			if (PlayerPrefs.GetInt("CoinsOfferPurchase") == 0)
//			{
//				this.CoinsOfferPackMenuBtn.SetActive(true);
//			}
//			if (PlayerPrefs.GetInt("CareerLvlPurchased") == 0)
//			{
//				this.UnlockCareerLevelsMenuBtn.SetActive(true);
//			}
//			if (PlayerPrefs.GetInt("ChallengeLvlPurchased") == 0)
//			{
//				this.UnlockChallengeLevelsMenuBtn.SetActive(true);
//			}
//		}
//		else
//		{
//			this.LeadrboardBtnn.SetActive(false);
//			this.SocialBtnns.SetActive(false);
//			this.StoreNotAvailbleObj.SetActive(true);
//			this.StoreObj.SetActive(false);
//			this.FreeCoinsBTn.SetActive(false);
//			this.IAP_Gmbobj.SetActive(false);
//			this.StarterKitMenuBtn.SetActive(false);
//			this.CoinsOfferPackMenuBtn.SetActive(false);
//			this.UnlockCareerLevelsMenuBtn.SetActive(false);
//			this.UnlockChallengeLevelsMenuBtn.SetActive(false);
//		}
//	}

//	private void LateUpdate()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			if (PlayerPrefs.GetInt("CarFourPurcahsed") == 1 && PlayerPrefs.GetInt("NoAdsPurchase") == 1)
//			{
//				this.StarterKitMenuBtn.SetActive(false);
//			}
//			if (PlayerPrefs.GetInt("CoinsOfferPurchase") == 1)
//			{
//				this.CoinsOfferPackMenuBtn.SetActive(false);
//			}
//			if (PlayerPrefs.GetInt("CareerLvlPurchased") == 1)
//			{
//				this.UnlockCareerLevelsMenuBtn.SetActive(false);
//			}
//			if (PlayerPrefs.GetInt("ChallengeLvlPurchased") == 1)
//			{
//				this.UnlockChallengeLevelsMenuBtn.SetActive(false);
//			}
//			if (PlayerPrefs.GetInt("CarFourPurcahsed") == 0 && PlayerPrefs.GetInt("NoAdsPurchase") == 0)
//			{
//				this.StarterKitStoreBtn.interactable = true;
//			}
//			else if (PlayerPrefs.GetInt("CarFourPurcahsed") == 1 && PlayerPrefs.GetInt("NoAdsPurchase") == 1)
//			{
//				this.StarterKitStoreBtn.interactable = false;
//			}
//			if (PlayerPrefs.GetInt("CoinsOfferPurchase") == 1)
//			{
//				this.CoinsOfferPackStoreBtn.interactable = false;
//			}
//			else
//			{
//				this.CoinsOfferPackStoreBtn.interactable = true;
//			}
//			if (PlayerPrefs.GetInt("NoAdsPurchase") == 1)
//			{
//				this.NoAdsStoreBtn.interactable = false;
//			}
//			else
//			{
//				this.NoAdsStoreBtn.interactable = true;
//			}
//			if (PlayerPrefs.GetInt("ChallengeLvlPurchased") == 1)
//			{
//				this.UnlockChallengeLevelsBtn.interactable = false;
//			}
//			else
//			{
//				this.UnlockChallengeLevelsBtn.interactable = true;
//			}
//			if (PlayerPrefs.GetInt("CareerLvlPurchased") == 1)
//			{
//				this.UnlockCareerLevelsBtn.interactable = false;
//			}
//			else
//			{
//				this.UnlockCareerLevelsBtn.interactable = true;
//			}
//			if (PlayerPrefs.GetInt("CarTwoPurcahsed") == 1)
//			{
//				this.UnlockTrain1Btn.interactable = false;
//			}
//			else
//			{
//				this.UnlockTrain1Btn.interactable = true;
//			}
//			if (PlayerPrefs.GetInt("CarThreePurcahsed") == 1)
//			{
//				this.UnlockTrain2Btn.interactable = false;
//			}
//			else
//			{
//				this.UnlockTrain2Btn.interactable = true;
//			}
//			if (PlayerPrefs.GetInt("CarFivePurcahsed") == 1)
//			{
//				this.UnlockTrain3Btn.interactable = false;
//			}
//			else
//			{
//				this.UnlockTrain3Btn.interactable = true;
//			}
//			if (PlayerPrefs.GetInt("CarSixPurcahsed") == 1)
//			{
//				this.UnlockTrain4Btn.interactable = false;
//			}
//			else
//			{
//				this.UnlockTrain4Btn.interactable = true;
//			}
//			if (PlayerPrefs.GetInt("CarSevenPurcahsed") == 1)
//			{
//				this.UnlockTrain5Btn.interactable = false;
//			}
//			else
//			{
//				this.UnlockTrain5Btn.interactable = true;
//			}
//		}
//	}

//	public void FreeCoins_Btn_Click()
//	{
//		this.FreeCoins_Dialogue.SetActive(true);
//	}

//	public void FreeCoins_Cross_Btn_Click()
//	{
//		this.FreeCoins_Dialogue.SetActive(false);
//	}

//	public void StarterKit_Btn_Click()
//	{
//		this.StarterKit_Dialogue.SetActive(true);
//	}

//	public void StarterKit_Cross_Btn_Click()
//	{
//		this.StarterKit_Dialogue.SetActive(false);
//	}

//	public void CoinsOffer_Btn_Click()
//	{
//		this.CoinsOffer_Dialogue.SetActive(true);
//	}

//	public void CoinsOffer_Cross_Btn_Click()
//	{
//		this.CoinsOffer_Dialogue.SetActive(false);
//	}

//	public void Train2_Buy_Btn_Click()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			this.INAPPScriptt.UnlockTrain1_Buy_BTn_Click();
//		}
//		else
//		{
//			this.InternetDialogueaaaa.SetActive(true);
//		}
//	}

//	public void Train3_Buy_Btn_Click()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			this.INAPPScriptt.UnlockTrain2_Buy_BTn_Click();
//		}
//		else
//		{
//			this.InternetDialogueaaaa.SetActive(true);
//		}
//	}

//	public void Train4_Buy_Btn_Click()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			this.INAPPScriptt.StarterKit_Buy_BTn_Click();
//		}
//		else
//		{
//			this.InternetDialogueaaaa.SetActive(true);
//		}
//	}

//	public void Train5_Buy_Btn_Click()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			this.INAPPScriptt.UnlockTrain3_Buy_BTn_Click();
//		}
//		else
//		{
//			this.InternetDialogueaaaa.SetActive(true);
//		}
//	}

//	public void Train6_Buy_Btn_Click()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			this.INAPPScriptt.UnlockTrain4_Buy_BTn_Click();
//		}
//		else
//		{
//			this.InternetDialogueaaaa.SetActive(true);
//		}
//	}

//	public void Train7_Buy_Btn_Click()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			this.INAPPScriptt.UnlockTrain5_Buy_BTn_Click();
//		}
//		else
//		{
//			this.InternetDialogueaaaa.SetActive(true);
//		}
//	}

//	public void Social_Btn_Click()
//	{
//		this.SocialPanel.SetActive(true);
//	}

//	public void Social_Back_Btn_Click()
//	{
//		this.SocialPanel.SetActive(false);
//	}

//	public GameObject SocialPanel;

//	[Header("Store Purchase Buttons")]
//	public Button NoAdsStoreBtn;

//	public Button UnlockChallengeLevelsBtn;

//	public Button UnlockCareerLevelsBtn;

//	public Button UnlockTrain1Btn;

//	public Button UnlockTrain2Btn;

//	public Button UnlockTrain3Btn;

//	public Button UnlockTrain4Btn;

//	public Button UnlockTrain5Btn;

//	public Button StarterKitStoreBtn;

//	public Button CoinsOfferPackStoreBtn;

//	[Header("IAP Menu BUttons")]
//	public GameObject StarterKitMenuBtn;

//	public GameObject CoinsOfferPackMenuBtn;

//	public GameObject UnlockCareerLevelsMenuBtn;

//	public GameObject UnlockChallengeLevelsMenuBtn;

//	public GameObject StoreObj;

//	public GameObject IAP_Gmbobj;

//	public GameObject StoreNotAvailbleObj;

//	public GameObject FreeCoinsBTn;

//	[Header("Dialogues Text")]
//	public GameObject FreeCoins_Dialogue;

//	public GameObject StarterKit_Dialogue;

//	public GameObject CoinsOffer_Dialogue;

//	[Header("Leaderboard Button")]
//	public GameObject LeadrboardBtnn;

//	public GameObject SocialBtnns;

	

//	public GameObject InternetDialogueaaaa;
//}
