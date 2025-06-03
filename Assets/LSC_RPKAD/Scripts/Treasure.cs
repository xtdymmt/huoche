using System.Collections;
using System.Collections.Generic;
using QGMiniGame;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : MonoBehaviour
{
    public Image bar;
    public Button button;
    private float passTime = 0;
    public static Treasure Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        bar.fillAmount = 0;
        button.onClick.AddListener(OnBtnCliack);
    }
    private void OnBtnCliack()
    {
        bar.fillAmount += 0.1f;

        if (bar.fillAmount >= 0.5f && LSC_ADManager.Instance.IsLoadRewardedVideo)
        {
            LSC_ADManager.Instance.ShowBanner();
            bar.fillAmount = 0;
            gameObject.SetActive(false);
            LSC_ADManager.Instance.ShowReward(() =>
            {
                // Config.COINS += 500;
                // PlayerPrefs.SetInt("coins", Config.COINS);
                // PlayerPrefs.Save();
                // Config.myToast.showToast("你获得了500金币");
            });
            ADSceneManager.Instance.CloseScene();
            // ClientIdeSystem.Control(ClientIde.Video, "buy", true, (bool _s) =>
            // {
            //     //_s:true 观看视频成功 _s:false 观看视频失败
            //     if (_s)
            //     {
            //         //成功逻辑
            //     }
            //     else
            //     {
            //         //失败逻辑
            //     }
            // });
        }
        if (bar.fillAmount >= 1)
        {
            LSC_ADManager.Instance.ShowBanner();
            bar.fillAmount = 0;
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        passTime += Time.deltaTime;
        if (passTime > 0.025f)
        {
            if (bar.fillAmount >= 0.01f)
            {
                bar.fillAmount -= 0.01f;
            }
            passTime = 0;
        }
    }
    public void Show()
    {
        if (LSC_ADManager.Instance.TimeShowAD)
        {
            gameObject.SetActive(true);
            LSC_ADManager.Instance.HideBanner();
        }
    }
}
