//// dnSpy decompiler from Assembly-CSharp.dll class: INAPP
//using System;
//using UnityEngine;
//using UnityEngine.Purchasing;
//using UnityEngine.Purchasing.Extension;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class INAPP : MonoBehaviour, IStoreListener
//{
//	private void Start()
//	{
//		if (INAPP.m_StoreController == null)
//		{
//			this.InitializePurchasing();
//		}
//	}

//	public void InitializePurchasing()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			UnityEngine.Debug.Log("Internet Available");
//			if (this.IsInitialized())
//			{
//				return;
//			}
//			ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
//			configurationBuilder.AddProduct(INAPP.UnlockCareerLevels, ProductType.NonConsumable);
//			configurationBuilder.AddProduct(INAPP.UnlockChallengeLevels, ProductType.NonConsumable);
//			configurationBuilder.AddProduct(INAPP.UnlockTrain1, ProductType.NonConsumable);
//			configurationBuilder.AddProduct(INAPP.UnlockTrain2, ProductType.NonConsumable);
//			configurationBuilder.AddProduct(INAPP.UnlockTrain3, ProductType.NonConsumable);
//			configurationBuilder.AddProduct(INAPP.UnlockTrain4, ProductType.NonConsumable);
//			configurationBuilder.AddProduct(INAPP.UnlockTrain5, ProductType.NonConsumable);
//			configurationBuilder.AddProduct(INAPP.NoAdsid, ProductType.NonConsumable);
//			configurationBuilder.AddProduct(INAPP.StarterKit, ProductType.NonConsumable);
//			configurationBuilder.AddProduct(INAPP.CoinOfferPack, ProductType.NonConsumable);
//			configurationBuilder.AddProduct(INAPP.CashSet1, ProductType.Consumable);
//			configurationBuilder.AddProduct(INAPP.CashSet2, ProductType.Consumable);
//			configurationBuilder.AddProduct(INAPP.CashSet3, ProductType.Consumable);
//			UnityPurchasing.Initialize(this, configurationBuilder);
//		}
//	}

//	private bool IsInitialized()
//	{
//		return INAPP.m_StoreController != null && INAPP.m_StoreExtensionProvider != null;
//	}

//	public void BuyNoAdsProduct_Click()
//	{
//		this.BuyProductID(INAPP.NoAdsid);
//	}

//	public void BuyCashSetOne_Click()
//	{
//		this.BuyProductID(INAPP.CashSet1);
//	}

//	public void BuyCashSetTwo_Click()
//	{
//		this.BuyProductID(INAPP.CashSet2);
//	}

//	public void BuyCashSetThree_Click()
//	{
//		this.BuyProductID(INAPP.CashSet3);
//	}

//	public void StarterKit_Buy_BTn_Click()
//	{
//		this.BuyProductID(INAPP.StarterKit);
//	}

//	public void CoinOfferPack_Buy_BTn_Click()
//	{
//		this.BuyProductID(INAPP.CoinOfferPack);
//	}

//	public void UnlockCareerLevels_Buy_BTn_Click()
//	{
//		this.BuyProductID(INAPP.UnlockCareerLevels);
//	}

//	public void UnlockChallengeLevels_Buy_BTn_Click()
//	{
//		this.BuyProductID(INAPP.UnlockChallengeLevels);
//	}

//	public void UnlockTrain1_Buy_BTn_Click()
//	{
//		this.BuyProductID(INAPP.UnlockTrain1);
//	}

//	public void UnlockTrain2_Buy_BTn_Click()
//	{
//		this.BuyProductID(INAPP.UnlockTrain2);
//	}

//	public void UnlockTrain3_Buy_BTn_Click()
//	{
//		this.BuyProductID(INAPP.UnlockTrain3);
//	}

//	public void UnlockTrain4_Buy_BTn_Click()
//	{
//		this.BuyProductID(INAPP.UnlockTrain4);
//	}

//	public void UnlockTrain5_Buy_BTn_Click()
//	{
//		this.BuyProductID(INAPP.UnlockTrain5);
//	}

//	private void BuyProductID(string productId)
//	{
//		if (this.IsInitialized())
//		{
//			Product product = INAPP.m_StoreController.products.WithID(productId);
//			if (product != null && product.availableToPurchase)
//			{
//				UnityEngine.Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
//				INAPP.m_StoreController.InitiatePurchase(product);
//			}
//			else
//			{
//				UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//			}
//		}
//		else
//		{
//			UnityEngine.Debug.Log("BuyProductID FAIL. Not initialized.");
//		}
//	}

//	public void RestorePurchases()
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			if (!this.IsInitialized())
//			{
//				UnityEngine.Debug.Log("RestorePurchases FAIL. Not initialized.");
//				return;
//			}
//			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
//			{
//				UnityEngine.Debug.Log("RestorePurchases started ...");
//				IAppleExtensions extension = INAPP.m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
//				extension.RestoreTransactions(delegate(bool result)
//				{
//					UnityEngine.Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//				});
//			}
//			else
//			{
//				UnityEngine.Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
//			}
//		}
//	}

//	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//	{
//		UnityEngine.Debug.Log("OnInitialized: PASS");
//		INAPP.m_StoreController = controller;
//		INAPP.m_StoreExtensionProvider = extensions;
//		if (SceneManager.GetActiveScene().buildIndex == 1)
//		{
//			string localizedPriceString = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.UnlockCareerLevels).metadata.localizedPriceString;
//			this.UnlockCareerLevelsPriceText.text = localizedPriceString;
//			string localizedPriceString2 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.UnlockChallengeLevels).metadata.localizedPriceString;
//			this.UnlockChallengeLevelsPriceText.text = localizedPriceString2;
//			string localizedPriceString3 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.UnlockTrain1).metadata.localizedPriceString;
//			this.UnlockTrain1PriceText.text = localizedPriceString3;
//			string localizedPriceString4 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.UnlockTrain2).metadata.localizedPriceString;
//			this.UnlockTrain2PriceText.text = localizedPriceString4;
//			string localizedPriceString5 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.UnlockTrain3).metadata.localizedPriceString;
//			this.UnlockTrain3PriceText.text = localizedPriceString5;
//			string localizedPriceString6 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.UnlockTrain4).metadata.localizedPriceString;
//			this.UnlockTrain4PriceText.text = localizedPriceString6;
//			string localizedPriceString7 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.UnlockTrain5).metadata.localizedPriceString;
//			this.UnlockTrain5PriceText.text = localizedPriceString7;
//			string localizedPriceString8 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.NoAdsid).metadata.localizedPriceString;
//			this.NoAdsPriceText.text = localizedPriceString8;
//			string localizedPriceString9 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.StarterKit).metadata.localizedPriceString;
//			this.StarterKitPriceText.text = localizedPriceString9;
//			this.StarterKitPriceText_Dialogue.text = localizedPriceString9;
//			string localizedPriceString10 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.CoinOfferPack).metadata.localizedPriceString;
//			this.CoinOfferPackPriceText.text = localizedPriceString10;
//			this.CoinOfferPackPriceText_Dialogue.text = localizedPriceString10;
//			string localizedPriceString11 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.CashSet1).metadata.localizedPriceString;
//			this.CashSet1PriceText.text = localizedPriceString11;
//			string localizedPriceString12 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.CashSet2).metadata.localizedPriceString;
//			this.CashSet2PriceText.text = localizedPriceString12;
//			string localizedPriceString13 = INAPP.m_StoreController.products.WithStoreSpecificID(INAPP.CashSet3).metadata.localizedPriceString;
//			this.CashSet3PriceText.text = localizedPriceString13;
//			this.Train2PriceText.text = localizedPriceString3;
//			this.Train3PriceText.text = localizedPriceString4;
//			this.Train4PriceText.text = localizedPriceString9;
//			this.Train5PriceText.text = localizedPriceString5;
//			this.Train6PriceText.text = localizedPriceString6;
//			this.Train7PriceText.text = localizedPriceString7;
//			PlayerPrefs.SetString("Train1PriceDB", localizedPriceString3);
//			PlayerPrefs.SetString("Train2PriceDB", localizedPriceString4);
//			PlayerPrefs.SetString("Train3PriceDB", localizedPriceString5);
//			PlayerPrefs.SetString("Train4PriceDB", localizedPriceString6);
//			PlayerPrefs.SetString("Train5PriceDB", localizedPriceString7);
//			PlayerPrefs.SetString("CareerPriceDB", localizedPriceString);
//			PlayerPrefs.SetString("ChallengePriceDB", localizedPriceString2);
//			PlayerPrefs.SetString("NoAdsPriceDB", localizedPriceString8);
//		}
//	}

//	public void OnInitializeFailed(InitializationFailureReason error)
//	{
//		UnityEngine.Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
//	}

//	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//	{
//		if (Application.internetReachability != NetworkReachability.NotReachable)
//		{
//			if (string.Equals(args.purchasedProduct.definition.id, INAPP.NoAdsid, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("NoAdsPurchase", 1);
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.CashSet1, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 10000);
//				this.CashText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.CashSet2, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 20000);
//				this.CashText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.CashSet3, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 40000);
//				this.CashText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.StarterKit, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 30000);
//				this.CashText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
//				PlayerPrefs.SetInt("CarFourPurcahsed", 1);
//				PlayerPrefs.SetInt("NoAdsPurchase", 1);
//				PlayerPrefs.SetInt("StarterKitPurchase", 1);
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.CoinOfferPack, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 50000);
//				this.CashText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
//				PlayerPrefs.SetInt("CoinsOfferPurchase", 1);
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.UnlockCareerLevels, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("UnlockedLevel2", 1);
//				PlayerPrefs.SetInt("UnlockedLevel3", 1);
//				PlayerPrefs.SetInt("UnlockedLevel4", 1);
//				PlayerPrefs.SetInt("UnlockedLevel5", 1);
//				PlayerPrefs.SetInt("UnlockedLevel6", 1);
//				PlayerPrefs.SetInt("UnlockedLevel7", 1);
//				PlayerPrefs.SetInt("UnlockedLevel8", 1);
//				PlayerPrefs.SetInt("UnlockedLevel9", 1);
//				PlayerPrefs.SetInt("UnlockedLevel10", 1);
//				PlayerPrefs.SetInt("UnlockedLevel11", 1);
//				PlayerPrefs.SetInt("UnlockedLevel12", 1);
//				PlayerPrefs.SetInt("CareerLvlPurchased", 1);
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.UnlockChallengeLevels, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("UnlockedLevel13", 1);
//				PlayerPrefs.SetInt("UnlockedLevel14", 1);
//				PlayerPrefs.SetInt("UnlockedLevel15", 1);
//				PlayerPrefs.SetInt("UnlockedLevel16", 1);
//				PlayerPrefs.SetInt("UnlockedLevel17", 1);
//				PlayerPrefs.SetInt("UnlockedLevel18", 1);
//				PlayerPrefs.SetInt("UnlockedLevel19", 1);
//				PlayerPrefs.SetInt("UnlockedLevel20", 1);
//				PlayerPrefs.SetInt("UnlockedLevel21", 1);
//				PlayerPrefs.SetInt("UnlockedLevel22", 1);
//				PlayerPrefs.SetInt("UnlockedLevel23", 1);
//				PlayerPrefs.SetInt("UnlockedLevel24", 1);
//				PlayerPrefs.SetInt("UnlockedLevel25", 1);
//				PlayerPrefs.SetInt("UnlockedLevel26", 1);
//				PlayerPrefs.SetInt("UnlockedLevel27", 1);
//				PlayerPrefs.SetInt("UnlockedLevel28", 1);
//				PlayerPrefs.SetInt("ChallengeLvlPurchased", 1);
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.UnlockTrain1, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("CarTwoPurcahsed", 1);
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.UnlockTrain2, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("CarThreePurcahsed", 1);
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.UnlockTrain3, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("CarFivePurcahsed", 1);
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.UnlockTrain4, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("CarSixPurcahsed", 1);
//			}
//			else if (string.Equals(args.purchasedProduct.definition.id, INAPP.UnlockTrain5, StringComparison.Ordinal))
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				PlayerPrefs.SetInt("CarSevenPurcahsed", 1);
//			}
//			else
//			{
//				UnityEngine.Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
//			}
//		}
//		return PurchaseProcessingResult.Complete;
//	}

//	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//	{
//		UnityEngine.Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//	}

//	private static IStoreController m_StoreController;

//	private static IExtensionProvider m_StoreExtensionProvider;

//	public static string UnlockCareerLevels = "com.monstergamesproductions.modern.train.driving.inap.careerlevels";

//	public static string UnlockChallengeLevels = "com.monstergamesproductions.modern.train.driving.inap.challengelevels";

//	public static string UnlockTrain1 = "com.monstergamesproductions.modern.train.driving.inap.unlocktrain1";

//	public static string UnlockTrain2 = "com.monstergamesproductions.modern.train.driving.inap.unlocktrain2";

//	public static string UnlockTrain3 = "com.monstergamesproductions.modern.train.driving.inap.unlocktrain3";

//	public static string UnlockTrain4 = "com.monstergamesproductions.modern.train.driving.inap.unlocktrain4";

//	public static string UnlockTrain5 = "com.monstergamesproductions.modern.train.driving.inap.unlocktrain5";

//	public static string NoAdsid = "com.monstergamesproductions.modern.train.driving.inap.removeads.z";

//	public static string StarterKit = "com.monstergamesproductions.modern.train.driving.inap.starterkit.z";

//	public static string CoinOfferPack = "com.monstergamesproductions.modern.train.driving.inap.coinofferpack.z";

//	public static string CashSet1 = "com.monstergamesproductions.modern.train.driving.inap.cashset.one.z";

//	public static string CashSet2 = "com.monstergamesproductions.modern.train.driving.cashset.two.z";

//	public static string CashSet3 = "com.monstergamesproductions.modern.train.driving.inap.cashset.three.z";

//	public Text CashText;

//	[Header("Prices Text")]
//	public Text UnlockCareerLevelsPriceText;

//	public Text UnlockChallengeLevelsPriceText;

//	public Text UnlockTrain1PriceText;

//	public Text UnlockTrain2PriceText;

//	public Text UnlockTrain3PriceText;

//	public Text UnlockTrain4PriceText;

//	public Text UnlockTrain5PriceText;

//	public Text NoAdsPriceText;

//	public Text StarterKitPriceText;

//	public Text CoinOfferPackPriceText;

//	public Text StarterKitPriceText_Dialogue;

//	public Text CoinOfferPackPriceText_Dialogue;

//	public Text CashSet1PriceText;

//	public Text CashSet2PriceText;

//	public Text CashSet3PriceText;

//	[Header("Garage Buttons Prices Text")]
//	public Text Train2PriceText;

//	public Text Train3PriceText;

//	public Text Train4PriceText;

//	public Text Train5PriceText;

//	public Text Train6PriceText;

//	public Text Train7PriceText;
//}
