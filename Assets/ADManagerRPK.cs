using QGMiniGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//创建一个物体挂载这个脚本


public class ADManagerRPK : MonoBehaviour
{

    [Header("原生ID")]
    public string ShowYSID = string.Empty;
    [Header("横幅ID")]
    public string ShowBannerID = string.Empty;
    [Header("激励ID")]
    public string ShowVidoID = string.Empty;





    //写死的时间戳 在网站上直接转换

    [Header("广告显示的时间戳")]
    public int ShowTime = 1686830400;
    int m_timestamp;
    public bool TimeShowAD;
    //1676336400


    private int GetTimeStamp(DateTime dt)// 获取时间戳Timestamp  
    {
        DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
        int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);
        return timeStamp;
    }
    private DateTime GetDateTime(int timeStamp)//时间戳Timestamp转换成日期
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = ((long)timeStamp * 10000000);
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime targetDt = dtStart.Add(toNow);
        return targetDt;
    }


    private DateTime GetDateTime(string timeStamp)// 时间戳Timestamp转换成日期
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime targetDt = dtStart.Add(toNow);
        return dtStart.Add(toNow);

    }



    private static ADManagerRPK instance;
    public static ADManagerRPK Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("ShowADManager");
                obj.AddComponent<ADManagerRPK>();
                instance = obj.GetComponent<ADManagerRPK>();
            }
            return instance;
        }
    }

    void Start()
    {
        time = Time.time;

        DontDestroyOnLoad(this.gameObject);
        Debug.Log("不可删除单例广告类生成完毕");
        if (ShowYSID == string.Empty || ShowBannerID == string.Empty || ShowVidoID == string.Empty)
        {
            Debug.LogError("未设置ID");
        }
        DateTime dtNow = DateTime.Now;
        m_timestamp = GetTimeStamp(dtNow);
        Debug.Log(string.Format("获取当前时间的时间戳 = {0} -> {1}", dtNow.ToString("yyyy-MM-dd hh:mm:ss"), m_timestamp));
        if (m_timestamp < ShowTime)
        {
            TimeShowAD = false;
            //Debug.Log("广告不可实现");
            return;
        }
        else
        {
            TimeShowAD = true;
            //Debug.Log("广告可实现");

        }

    }


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //Debug.Log("赋值单例");
        }
        else
        {
            //Debug.Log("删除单例");
            Destroy(this.gameObject);
        }


    }

    [Header("定时")]
    public float TargetTime = 30;


    float time;
    [Header("测试观察游戏时间")]
    public float timee;
    bool isNativeTime = true;

    //[Header("多少秒后正常弹")]
    //public float overtime=60;

    void Update()
    {
        timee = Time.time;

        //	if (Time.time - time >= TargetTime && isNativeTime == false|| timee>overtime)
        //	{
        //		Debug.Log("倒计时结束,播放广告");
        //		time = Time.time;
        //		isNativeTime = true;
        //	}
        if (timingPopUp > 0f)
        {
            timingPopUp -= Time.deltaTime;
            //Debug.Log(timingPopUp);
            //isTimingPopUp = false;

        }
        else
        {
            timingPopUp = 30f;
            Debug.Log("30秒计时结束，播放广告");
            //isTimingPopUp = true;
            ShowYS();
        }
    }
    private int TapCount = 15;
    private float timingPopUp = 30f;
    private bool isTimingPopUp = false;

    public void ShowYS()
    {
        //if (isTimingPopUp)
        //{
        //    Debug.Log("30秒计时结束，播放广告");
        //}
        //else if (TapCount < 1)
        //{
        //    TapCount = 15;
        //    Debug.Log("点击15次后弹广告");
        //}
        //else
        //{
        //    TapCount--;
        //    Debug.Log("剩余点击次数:" + TapCount);
        //    return;
        //}

        //if (isNativeTime == false) return;

        //isNativeTime = false;

        Debug.Log("ShowYS");



#if UNITY_EDITOR
		return;
#endif
        if (TimeShowAD == false) return;








        var customAd = QG.CreateCustomAd(new QGCreateCustomAdParam()
        {
            posId = ShowYSID
        });
        customAd.OnLoad(() =>
        {
            Debug.Log("QG.customAd.OnLoad success = ");
            customAd.Show(
            (msg) => { Debug.Log("QG.customAd.Show success = " + JsonUtility.ToJson(msg)); },
            (msg) => { Debug.Log("QG.customAd.Show fail = " + msg.errMsg); }
            );
        });
        customAd.OnError((QGBaseResponse msg) =>
        {
            Debug.Log("QG.customAd.OnError success = " + JsonUtility.ToJson(msg));
        });
        customAd.OnHide(() =>
        {
            Debug.Log("QG.customAd.OnHide success ");
        });

    }

    private bool isFirstBanner = true;

    public void ShowBanner()
    {
        if (!isFirstBanner)
            return;
        isFirstBanner = false;
        StartCoroutine(LunBo(60));
    }

    private IEnumerator LunBo(int time)
    {
        WaitForSecondsRealtime WaitTime = new WaitForSecondsRealtime(time);

        while (true)
        {
            ShowBannerWork();
            yield return WaitTime;
        }
    }

    private void ShowBannerWork()
    {
        Debug.Log("ShowBanner");

        if (TimeShowAD == false) return;

        var bannerAd = QG.CreateBannerAd(new QGCreateBannerAdParam()
        {
            posId = ShowBannerID,
            //style = new Style() { top = 0 }//显示在顶部(默认是底部)
        });
        bannerAd.OnLoad(() =>
        {
            bannerAd.Show(
            (msg) => { Debug.Log("QG.bannerAd.Show success = " + JsonUtility.ToJson(msg)); },
            (msg) => { Debug.Log("QG.bannerAd.Show fail = " + msg.errMsg); }
            );
        });
        bannerAd.OnError((QGBaseResponse msg) =>
        {
            Debug.Log("QG.bannerAd.OnError success = " + JsonUtility.ToJson(msg));
        });
    }


    public void ShowVido(Action action)
    {
        Debug.Log("ShowVido");


#if !UNITY_EDITOR









        var rewardedVideoAd = QG.CreateRewardedVideoAd(new QGCommonAdParam()
        {
            posId = ShowVidoID
        });
        rewardedVideoAd.OnLoad(() =>
        {
            rewardedVideoAd.Show(
            (msg) => { Debug.Log("QG.rewardedVideoAd.Show success = " + JsonUtility.ToJson(msg)); },
            (msg) => { Debug.Log("QG.rewardedVideoAd.Show fail = " + msg.errMsg); }
            );
        });
        rewardedVideoAd.OnError((QGBaseResponse msg) =>
        {
            QG.ShowToast("视频播放失败，请稍后再试");
            Debug.Log("QG.rewardedVideoAd.OnError success = " + JsonUtility.ToJson(msg));
        });
        rewardedVideoAd.OnClose((QGRewardedVideoResponse msg) =>
        {
            if (msg.isEnded)
            {
                Debug.Log("QG.rewardedVideoAd.OnClose success = " + " 播放成功");
                action();
            }
        });
#else
		action();
#endif
    }















}




