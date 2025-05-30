using UnityEngine;
using System;
/// <summary>
/// 脚本说明：
///     管理广告接口的脚本，整合了一些常用的广告接口，
/// 因为是用Try-Catch调用的接口，所以打包APK的时候即便
/// 没有实现对应的接口，也不会影响游戏的运行，可以通过
/// 按照需实现对应接口的方式实现指定的策略。
/// </summary>
public class MFADManager : MonoBehaviour
{

    private static MFADManager _instance;
    public static ADList _ADList;

    void Awake()
    {
        if (_instance != null)
        {
            //这里一定要是销毁this.gameObject
            Destroy(this.gameObject);
            //print("销毁了一个多余的监听器");
            return;
        }
        //这句话只执行一次，第二次上面return了
        _instance = this;
        _ADList = Resources.Load<ADList>("btnManager");
    }

    private void Reset()
    {
        print("广告管理器已就绪");
        if (GetComponent<ShowADManager>() == null)
            this.gameObject.AddComponent<ShowADManager>();
    }

    #region ===华为常用接口===

    /// <summary>
    /// 评测包用原生接口
    /// </summary>
    public static void ShowTestNative()
    {
        ShowAD("ShowTestNative");
    }

    /// <summary>
    /// 评测包用插屏接口，注意方法名和调用的接口名是不一样的如果Android Studio那边有用我的打包模板来打包，那么在Android Studio里面会有我预制好的接口，无需特别关注。
    /// </summary>
    public static void ShowTestTable()
    {
        ShowAD("ShowTestChaPing");
    }

    /// <summary>
    /// 操作键弹广告，行走/跳跃/攻击等对游戏进行操作的按钮调用的接口
    /// </summary>
    public static void ShowADOnPlay()
    {
        ShowAD("ShowADOnPlay");
    }

    /// <summary>
    /// 如字面意思一样，在任意按钮点击时都会弹出广告的接口
    /// </summary>
    public static void ShowADOnAnyWhere()
    {
        ShowAD("ShowADOnAnyWhere");
    }

    #endregion

    #region ===通用广告接口===

    /// <summary>
    /// 游戏启动弹广告，首页处点击开始游戏时调用的接口，一般不大用得到
    /// </summary>
    public static void ShowADOnStart()
    {
        ShowAD("ShowADOnStart");
    }

    /// <summary>
    /// 设置按钮弹广告，一般不大用得到
    /// </summary>
    public static void ShowADOnSetting()
    {
        ShowAD("ShowADOnSetting");
    }

    /// <summary>
    /// 胜利/失败处，或者结算的地方弹广告
    /// </summary>
    public static void ShowADOnFinish()
    {
        ShowAD("ShowADOnFinish");
    }

    /// <summary>
    /// 暂停弹广告，一般不大用得到
    /// </summary>
    public static void ShowADOnPause()
    {
        ShowAD("ShowADOnPause");
    }

    /// <summary>
    /// 返回键弹广告，这功能和ShowADOnAnyWhere稍微有些重叠了，一般不大用得到
    /// </summary>
    public static void ShowADOnBack()
    {
        ShowAD("ShowADOnBack");
    }

    /// <summary>
    /// 在退出时调用的方法，所有调用了Application.Quit()方法的地方都要替换成这个方法，然后在Android Studio上接入SDK的退出API。
    /// </summary>
    public static void UnityExit()//退出键调用的接口，用来在安卓那边实现退出功能
    {
        ShowAD("UnityExit");
    }

    /// <summary>
    /// 激励广告，通过 
    /// MFADManager.ShowVideo = delegate
    /// {
    ///     /*给予奖励的代码*/ 
    /// };
    /// 的方式来调用
    /// </summary>
    public static Action ShowVideo
    {
        set
        {
            MFRewardManager.rewardAction = null;
            MFRewardManager.rewardAction = value;
            if (MFRewardManager.instance == null)
            {
                GameObject obj = new GameObject();
                obj.AddComponent<MFRewardManager>();
                obj.GetComponent<MFRewardManager>().Reset();
                //MFRewardManager.instance = obj.GetComponent<MFRewardManager>();
            }
            ShowVideoAD(MFRewardManager.instance.name, "RewardCallBack", "");
        }
    }

    private static void ShowVideoAD(string GameObjectName, string Method, string Value)
    {
        ShowAD("ShowVideo", GameObjectName, Method, Value);
#if UNITY_EDITOR_WIN
        print("激励视频_编辑器回调");
        GameObject.Find(GameObjectName).SendMessage(Method, Value);
#endif
    }

    #endregion

    #region ===其它渠道接口===
    //备注：如果和华为的一些功能重叠，则在华为调用接口的方法中调用以下调用接口的方法即可

    /// <summary>
    /// OPPO渠道和VIVO渠道都弹广告的接口
    /// </summary>
    public static void ShowOV()
    {
        ShowOPPO();
        ShowVIVO();
    }

    /// <summary>
    /// 比较特殊的OV都弹广告的接口，在调用的时候，会记录不同关键字(Key)被调用的次数，并且在调用次数超过clickNum时调用广告接口，以此实现按钮间彼此独立的点击计数。
    /// </summary>
    /// <param name="Key">关键字</param>
    /// <param name="clickNum">某一关键字弹出广告所需要的点击次数</param>
    public static void ShowOV(string Key,int clickNum)
    {
        PlayerPrefs.SetInt(Key, (PlayerPrefs.GetInt(Key, 0) + 1));
#if UNITY_EDITOR_WIN
        print("字符" + Key + "点击了 " + PlayerPrefs.GetInt(Key, 0) + " 次。");
#endif
        if (PlayerPrefs.GetInt(Key, 0) > clickNum)
        {
            PlayerPrefs.SetInt(Key, 0);
            ShowOV();
        }
    }

    /// <summary>
    /// OPPO渠道调用的广告接口
    /// </summary>
    public static void ShowOPPO()
    {
        ShowAD("ShowOPPO");
    }

    /// <summary>
    /// VIVO渠道调用的广告接口
    /// </summary>
    public static void ShowVIVO()
    {
        ShowAD("ShowVIVO");
    }

    /// <summary>
    /// 233渠道调用的广告接口
    /// </summary>
    public static void Show233()
    {
        ShowAD("Show233");
    }

    /// <summary>
    /// 三星渠道调用的广告接口
    /// </summary>
    public static void ShowSanXing()
    {
        ShowAD("ShowSanXing");
    }

    #endregion

    #region ===安卓通讯接口===

    /// <summary>
    /// 根据传入的名字调用对应接口的方法。当对应的接口在Android Studio上没有被实现的时候，将会打印如catch中的Log
    /// </summary>
    /// <param name="ADName">调用接口的名字</param>
    public static void ShowAD(string ADName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call(ADName);
                }
            }
        }
        catch
        {
            print("接口：" + ADName + "()没有实现");
        }
#else
        try
        {
            if (!MFADManager._ADList.接口Log && ADName == "ShowADOnAnyWhere")
                return;
            print(ADName);
        }
        catch
        {
            print(ADName);
        }
#endif
    }

    /// <summary>
    /// 根据传入的名字调用对应接口的方法。当对应的接口在Android Studio上没有被实现的时候，将会打印如catch中的Log。
    /// 
    /// 原本是为了方便调用激励广告所设置的方法，但因为激励广告有了更便捷的接入方式，因此该方法基本上等于废弃了。
    /// </summary>
    /// <param name="ADName">接口的名字</param>
    /// <param name="GameObjectName">挂载体的名字</param>
    public static void ShowAD(string ADName, string GameObjectName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call(ADName,GameObjectName);
                }
            }
        }
        catch
        {
            print("接口：" + ADName + "(String GameObjectName)没有实现");
        }
#else
        print(ADName + ",挂载体为: " + GameObjectName);
#endif
    }

    /// <summary>
    /// 根据传入的名字调用对应接口的方法。当对应的接口在Android Studio上没有被实现的时候，将会打印如catch中的Log。
    /// 
    /// 原本是为了方便调用激励广告所设置的方法，但因为激励广告有了更便捷的接入方式，因此该方法基本上等于废弃了。
    /// </summary>
    /// <param name="ADName">接口的名字</param>
    /// <param name="GameObjectName">挂载体的名字</param>
    /// <param name="Method">给予奖励的方法名</param>
    public static void ShowAD(string ADName, string GameObjectName, string Method)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call(ADName,GameObjectName,Method);
                }
            }
        }
        catch
        {
            print("接口：" + ADName + "(String GameObjectName,String Method)没有实现");
        }
#else
        print(ADName + ",挂载体为: " + GameObjectName + "，方法名：" + Method);
#endif
    }

    /// <summary>
    /// 根据传入的名字调用对应接口的方法。当对应的接口在Android Studio上没有被实现的时候，将会打印如catch中的Log。
    /// 
    /// 原本是为了方便调用激励广告所设置的方法，但激励广告有了更便捷的接入方式。
    /// 
    /// 本方法便是新版接入激励广告的核心方法。
    /// </summary>
    /// <param name="ADName">接口的名字</param>
    /// <param name="GameObjectName">挂载体的名字</param>
    /// <param name="Method">给予奖励的方法名</param>
    /// <param name="Value">给予奖励的参数</param>
    public static void ShowAD(string ADName, string GameObjectName, string Method, string Value)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call(ADName,GameObjectName,Method,Value);
                }
            }
        }
        catch
        {
            print("接口：" + ADName + "(String GameObjectName,String Method,String CallbackValue)没有实现");
        }
#endif
    }

    #endregion
}
