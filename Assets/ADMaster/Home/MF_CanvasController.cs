using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 脚本说明：
///		此脚本挂载在首页的Canvas上，用来管理游戏中所有摩丰相关的弹窗和页面。
/// </summary>

public class MF_CanvasController : MonoBehaviour
{

    public static MF_CanvasController instance;

    public delegate void MF_Event();
    /// <summary>
    /// 关闭厦门摩丰相关的所有窗口
    /// </summary>
    public static MF_Event ALL_MF_Panel_Close;

    public delegate void PauseEvent(string info);
    /// <summary>
    /// 打开用来弹出广告的暂停页面
    /// </summary>
    public static PauseEvent ShowPausePanel;

    [Header("==========配置列表==========")]
    public float MINI_LOADING_TIME = 2.0f;//最短加载时间
    [SerializeField] private AsyncOperation async = null;

    [Header("==========界面列表==========")]
    [SerializeField] private GameObject HomePanel;
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] public GameObject PausePanel;

    [Header("==========按钮列表==========")]
    [SerializeField] private Button StartButton;

    [Header("==========控件列表==========")]
    [SerializeField] private Slider LoadingSlider;//进度条加载框
    [SerializeField] private Text _infoText;//暂停页信息框


    public void ShowPauseInfo(string str)
    {
        MFADManager.ShowOV();
        PausePanel.SetActive(true);
        _infoText.text = str;
    }

    void Reset()
    {
        if (name != "MF_Canvas")
            name = "MF_Canvas";
        GetComponent<Canvas>().sortingOrder = (short.MaxValue);
    }

    // Use this for initialization
    void Start()
    {

        ALL_MF_Panel_Close += closeallPanel;
        ShowPausePanel += ShowPauseInfo;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        LoadingSlider.interactable = false;

        ALL_MF_Panel_Close.Invoke();
        StartButton.onClick.AddListener(StartGame);
        HomePanel.SetActive(true);
    }

    public void StartGame()
    {
        ALL_MF_Panel_Close.Invoke();
        LoadingPanel.SetActive(true);
        StartCoroutine(LoadFirstScene());
    }

    //进度条的数值
    private float progressValue;

    IEnumerator LoadFirstScene()
    {
        float loadTime = 0f;
        async = SceneManager.LoadSceneAsync(1);
        //async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                progressValue = async.progress;
            else
                progressValue = 1.0f;

            LoadingSlider.value = progressValue;

            //用文本显示加载进度的百分比
            //progress.text = (int)(slider.value * 100) + " %";

            yield return null;
            loadTime += Time.deltaTime;
        }

        if (loadTime < MINI_LOADING_TIME)
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(MINI_LOADING_TIME - loadTime); 
        }

        //TODO 加载场景
        Time.timeScale = 1;
        ALL_MF_Panel_Close.Invoke();
        ShowPausePanel.Invoke("进入游戏");
    }

    void OnDestroy()
    {
        ALL_MF_Panel_Close -= closeallPanel;
        ShowPausePanel -= ShowPauseInfo;
    }

    public void closeallPanel()
    {
        HomePanel.SetActive(false);
        LoadingPanel.SetActive(false);
        PausePanel.SetActive(false);
    }
}
