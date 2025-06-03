using System;
using System.Collections;
using System.Collections.Generic;
using QGMiniGame;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LSC_ADManager : MonoBehaviour
{
    public const string AD_ID_BANNER = "378ebaeefae54ae18cb5fa916bb13a12";
    public const string AD_ID_CUSTOM = "177459593bec4076a990393269df665d";
    public const string AD_ID_REWARD = "268a486410c2405b9f53b053cabbd35c";
    private int ShowTime = 0;
    int m_timestamp;
    public bool TimeShowAD;
    public static LSC_ADManager Instance;
    QGRewardedVideoAd rewardedVideoAd = null;
    QGBannerAd bannerAd = null;
    QGCustomAd customAd = null;
    private bool isLoadRewardedVideo = false;
    public bool IsLoadRewardedVideo => isLoadRewardedVideo;
    public UnityEvent StartGame;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        ShowTime = GetTimeStamp(new DateTime(2025, 06, 04, 19, 00, 00));
    }
    private void Start()
    {
        ShowPrivacyPolicy();
        StartGame.AddListener(() =>
        {
            StartCoroutine(ShowBannerLoop());
            StartCoroutine(ShowCustomLoop());
        });
        DontDestroyOnLoad(this.gameObject);
        DateTime dtNow = DateTime.Now;
        m_timestamp = GetTimeStamp(dtNow);
        Debug.Log(string.Format("获取当前时间的时间戳 = {0} -> {1}", dtNow.ToString("yyyy-MM-dd hh:mm:ss"), m_timestamp));
        if (m_timestamp < ShowTime)
        {
            TimeShowAD = false;
            Debug.Log("广告不可实现");
            return;
        }
        else
        {
            TimeShowAD = true;
            Debug.Log("广告可实现");
        }

#if !UNITY_EDITOR
        if (rewardedVideoAd == null)
        {
            rewardedVideoAd = QG.CreateRewardedVideoAd(new QGCommonAdParam()
            {
                posId = AD_ID_REWARD
            });
        }
        else
        {
            rewardedVideoAd.Load(
               (msg) => { Debug.Log("QG.rewardedVideoAd.Load success = " + JsonUtility.ToJson(msg)); },
               (msg) => { Debug.Log("QG.rewardedVideoAd.Load fail = " + msg.errMsg); }
            );
        }

        rewardedVideoAd.OnLoad(() =>
        {
            isLoadRewardedVideo = true;
        });
#endif
    }
    private IEnumerator ShowBannerLoop()
    {
        yield return new WaitForSeconds(60);
        ShowBanner();
        StartCoroutine(ShowBannerLoop());
    }
    private IEnumerator ShowCustomLoop()
    {
        yield return new WaitForSeconds(35);
        ShowCustom();
        StartCoroutine(ShowCustomLoop());
    }
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
    public void ShowBanner()
    {
#if UNITY_EDITOR
        return;
#endif
        if (TimeShowAD)
        {
            if (bannerAd != null)
            {
                bannerAd.Destroy();
            }
            bannerAd = QG.CreateBannerAd(new QGCreateBannerAdParam()
            {
                posId = AD_ID_BANNER,
                adIntervals = 30,
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
            StartCoroutine(BannerIE());
        }
    }
    private IEnumerator BannerIE()
    {
        yield return new WaitForSeconds(30);
        ShowBanner();
    }
    public void HideBanner()
    {
#if UNITY_EDITOR
        return;
#endif
        if (TimeShowAD)
        {
            if (bannerAd != null)
            {
                bannerAd.Hide();
                StopCoroutine(BannerIE());
            }
        }
    }
    public void ShowCustom()
    {
#if UNITY_EDITOR
        return;
#endif
        if (TimeShowAD)
        {
            customAd = QG.CreateCustomAd(new QGCreateCustomAdParam()
            {
                posId = AD_ID_CUSTOM
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
    }
    public void ShowReward(Action action, Action action1 = null)
    {
        if (action1 == null)
        {
            action1 = () => { };
        }
#if UNITY_EDITOR
        action();
        return;
#endif
        if (TimeShowAD)
        {
            if (!isLoadRewardedVideo)
            {
                return;
            }
            rewardedVideoAd.Show(
    (msg) => { Debug.Log("QG.rewardedVideoAd.Show success = " + JsonUtility.ToJson(msg)); },
    (msg) => { Debug.Log("QG.rewardedVideoAd.Show fail = " + msg.errMsg); }
    );
            rewardedVideoAd.OnError((QGBaseResponse msg) =>
            {
                action1();
                Debug.Log("QG.rewardedVideoAd.OnError success = " + JsonUtility.ToJson(msg));
            });

            rewardedVideoAd.OnClose((QGRewardedVideoResponse msg) =>
            {
                if (msg.isEnded)
                {
                    Debug.Log("QG.rewardedVideoAd.OnClose success = " + " 播放成功");
                    action();
                }
                else
                {
                    action1();
                }
            });

            rewardedVideoAd.Hide(
              (msg) => { Debug.Log("QG.rewardedVideoAd.Hide success = " + JsonUtility.ToJson(msg)); },
              (msg) => { Debug.Log("QG.rewardedVideoAd.Hide fail = " + msg.errMsg); }
           );
        }
        else
        {
            action();
        }
    }
    public int PlayerPrefsGetInt(string key, int value)
    {
#if UNITY_EDITOR
        return PlayerPrefs.GetInt(key, value);
#else
        return QGMiniGameManager.Instance.StorageGetIntSync(key, value);
#endif
    }
    public void PlayerPrefsSetInt(string key, int value)
    {
#if UNITY_EDITOR
        PlayerPrefs.SetInt(key, value);
#else
        QGMiniGameManager.Instance.StorageSetIntSync(key, value);
#endif
    }
    public void ExitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
         QG.ExitApplication();
#endif
        //ClientIdeSystem.Control(ClientIde.Quit);
    }
    public void ShowPrivacyPolicy(bool isOnly = false)
    {
        if (isOnly)
        {
            LoadADScene(ADSceneManager.ADSceneType.PrivacyPolicyOnly);
        }
        else
        {
            LoadADScene(ADSceneManager.ADSceneType.PrivacyPolicy);
        }
    }
    private void LoadADScene(ADSceneManager.ADSceneType sceneType)
    {
        ADSceneManager.adSceneType = sceneType;
        SceneManager.LoadScene("LSC_ADScene", LoadSceneMode.Additive);
    }
    public void ShowTreasure()
    {
        if (TimeShowAD)
        {
            LoadADScene(ADSceneManager.ADSceneType.Treasure);
        }
    }
}
