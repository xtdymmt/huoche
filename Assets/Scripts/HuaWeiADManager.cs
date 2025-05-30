using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuaWeiADManager : MonoBehaviour {
    private void OnEnable()
    {
        
    }

    private void ShowAD(string InterFaceName)
    {

        //Debug.Log(InterFaceName);

#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call(InterFaceName);
                }
            }
        }
        catch (System.Exception e)
        {

        }
#endif
    }

    private static void ShowStaticAD(string InterFaceName)
    {

        Debug.Log(InterFaceName);

#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call(InterFaceName);
                }
            }
        }
        catch (System.Exception e)
        {

        }
#endif
    }

    public static void ShowOnPlay()
    {
        ShowStaticAD("ShowOnPlay");

    }
    public static void ShowOnGarage()
    {
        ShowStaticAD("ShowOnGarage");

    }
    public static void ShowChallenge()
    {
        ShowStaticAD("ShowChallenge");

    }
    public static void ShowCareer()
    {
        ShowStaticAD("ShowCareer");

    }
    public static void ShowRight()
    {
        ShowStaticAD("ShowRight");

    }
    public static void ShowLevel()
    {
        ShowStaticAD("ShowLevel");

    }
    public static void ShowSkip()
    {
        ShowStaticAD("ShowSkip");

    }
    public static void ShowClose()
    {
        ShowStaticAD("ShowClose");

    }
    public static void ShowOpen()
    {
        ShowStaticAD("ShowOpen");

    }
    public static void ShowPause()
    {
        ShowStaticAD("ShowPause");

    }
    public static void ShowButton()
    {
        ShowStaticAD("ShowButton");

    }
    public static void ShowButton1()
    {
        ShowStaticAD("ShowButton1");

    }
    public static void ShowMenu()
    {
        ShowStaticAD("ShowMenu");

    }
    public static void ShowWin()
    {
        ShowStaticAD("ShowWin");

    }
    public static void ShowLose()
    {
        ShowStaticAD("ShowLose");

    }
    public static void ShowNextLevel()
    {
        ShowStaticAD("ShowNextLevel");

    }
    public static void ShowRestart()
    {
        ShowStaticAD("ShowRestart");

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
