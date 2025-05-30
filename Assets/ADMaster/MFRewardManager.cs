using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 脚本说明：
///     激励广告的管理器，需要挂载在场景中任意一个空物体上。
/// 使用方法：
///     通过以下任意一种方式调用：
///     MFADManager.ShowVideo = ()=>{   /* 给予奖励的方法或者代码 */   };
///     MFADManager.ShowVideo = delegate{   /* 给予奖励的方法或者代码 */   };
/// </summary>
public class MFRewardManager : MonoBehaviour
{
    public static MFRewardManager instance = null;

    #region ===核心代码===
    //下面这部分代码挪到MFADManager那边去了，现在可以通过MFADManager来调用
    //public static Action ShowVideo
    //{
    //    set
    //    {
    //        MFRewardManager.rewardAction = null;
    //        MFRewardManager.rewardAction = value;
    //        if (MFRewardManager.instance == null)
    //        {
    //            GameObject obj = new GameObject();
    //            obj.AddComponent<MFRewardManager>();
    //            obj.GetComponent<MFRewardManager>().Reset();
    //        }
    //        ShowVideoAD(MFRewardManager.instance.name, "RewardCallBack", "");
    //    }
    //}

    public static Action rewardAction;

    public void RewardCallBack()
    {
        if (rewardAction != null)
            rewardAction();
        rewardAction = null;
    }

    #endregion

    #region ===初始化代码===

    public void Reset()
    {
        if (name != "MF_RewardManager")
            name = "MF_RewardManager";
        transform.Zero();
        print("激励管理器已就绪");
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this);
    }

    #endregion

    #region ===接口代码===

    private static void ShowVideoAD(string GameObjectName, string Method, string Value)//懒到极致的激励接口，挂载体、方法名和参数值全都不用找，直接传就完事了，应该会用的比较少，因为这个用法稍微有些逆天
    {
        ShowAD("ShowVideo", GameObjectName, Method, Value);
#if UNITY_EDITOR_WIN
        print("编辑器回调");
        GameObject.Find(GameObjectName).SendMessage(Method, Value);
#endif
    }

    private static void ShowAD(string ADName, string GameObjectName, string Method, string Value)
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
#else
        print(ADName + ",挂载体为: " + GameObjectName + "，方法名：" + Method + "，值：" + Value);
#endif
    }

    #endregion

}
