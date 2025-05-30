//// dnSpy decompiler from Assembly-CSharp.dll class: PlayservicesChecker
//using System;
//using UnityEngine;

//public class PlayservicesChecker : MonoBehaviour
//{
//	public bool IsAppInstalled(string bundleID)
//	{
//		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
//		AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getPackageManager", new object[0]);
//		UnityEngine.Debug.Log(" ********LaunchOtherApp ");
//		AndroidJavaObject result = null;
//		try
//		{
//			result = androidJavaObject.Call<AndroidJavaObject>("getLaunchIntentForPackage", new object[]
//			{
//				bundleID
//			});
//		}
//		catch (Exception ex)
//		{
//			UnityEngine.Debug.Log("exception" + ex.Message);
//		}
//		return result != null;
//	}

//	private void Awake()
//	{
//		this.serviceavlbl = this.IsAppInstalled("com.android.vending");
//	}

//	private void Start()
//	{
//		if (this.serviceavlbl)
//		{
//			PlayerPrefs.SetInt("PServicesDB", 0);
//		}
//		else
//		{
//			PlayerPrefs.SetInt("PServicesDB", 1);
//		}
//	}

//	private bool serviceavlbl;
//}
