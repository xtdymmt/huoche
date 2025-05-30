using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MF_PausePanelManager : MonoBehaviour
{

    public static MF_PausePanelManager instance;

    [TextArea] [SerializeField] private string 健康提示 = "健康游戏提醒：请合理安排游戏时间。";
    [ImageEffectOpaque] [SerializeField] private List<Sprite> 宣传图 = null;

    [SerializeField] private bool 定时提示 = false;
    [SerializeField] private int TipDelay = 30;

    [SerializeField] private bool 跨场景展示 = true;

    [SerializeField] private GameObject TipPanel;
    [SerializeField] private Text TipPanelText;
    [SerializeField] private GameObject PausePanel;

    [SerializeField] private static GameObject GameObjectName;
    [SerializeField] private static string preGameObjectName, MethodName, CallBackValue;
    [SerializeField] public const string MF_TYPE = "_XMMF";

    //public delegate void OVEvent(string info);
    //public static OVEvent ShowTipe;
    //public static OVEvent ShowPause;

    private void Reset()
    {
        if (name != "MF_PausePanelManager")
            name = "MF_PausePanelManager";
        GetComponent<Canvas>().sortingOrder = short.MaxValue;
        GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        GetComponent<CanvasScaler>().referenceResolution = new Vector2(1280, 720);
        if (TipPanel == null)
        {
            TipPanel = CreatePanel("TipPanel");
            if (TipPanelText == null)
            {
                TipPanelText = CreateText(TipPanel);
                TipPanelText.verticalOverflow = VerticalWrapMode.Overflow;
                TipPanelText.horizontalOverflow = HorizontalWrapMode.Wrap;
                TipPanelText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);
                TipPanelText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
                TipPanelText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);
                TipPanelText.text = "记得修改一下这个的尺寸";
            }
            CreateButton(TipPanel, "好的");
        }

        if (PausePanel == null)
        {
            PausePanel = CreatePanel("PausePanel");
            CreateButton(PausePanel, "继续游戏");
            PausePanel.AddComponent<MF_PausePanelController>();
            PausePanel.GetComponent<RectTransform>().sizeDelta = this.GetComponent<RectTransform>().sizeDelta;
            PausePanel.GetComponentInChildren<Button>().GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 350);
            PausePanel.SetActive(false);
        }

    }

    private Text CreateText(GameObject Panel)
    {
        //创建文本
        GameObject TextPanel = new GameObject("Text");
        TextPanel.transform.SetParent(Panel.transform);
        TextPanel.AddComponent<Text>();
        TextPanel.AddComponent<RectTransform>();
        TextPanel.transform.localScale = new Vector3(1, 1, 1);
        RectTransform TextPanelRec = TextPanel.GetComponent<RectTransform>();
        Text m_text = TextPanel.GetComponent<Text>();
        m_text.fontSize = 45;
        m_text.color = Color.black;
        m_text.alignment = TextAnchor.MiddleCenter;

        //设置尺寸
        TextPanelRec.anchoredPosition = new Vector2(0, 0);
        TextPanelRec.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Panel.GetComponent<RectTransform>().sizeDelta.y);
        TextPanelRec.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Panel.GetComponent<RectTransform>().sizeDelta.x);

        return m_text;
    }

    private void CreateButton(GameObject Panel,string BtnText)
    {
        //创建按钮
        GameObject PanelBtn = new GameObject("Button");
        PanelBtn.transform.SetParent(Panel.transform);
        PanelBtn.AddComponent<RectTransform>();
        RectTransform PanelBtnRec = PanelBtn.GetComponent<RectTransform>();
        PanelBtn.AddComponent<Image>();
        PanelBtn.AddComponent<Button>();
        PanelBtn.transform.localScale = new Vector3(1, 1, 1);

        //设置尺寸
        PanelBtnRec.anchoredPosition = new Vector2(0, 0);
        PanelBtnRec.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 100 - 75 / 2, 75);
        //PanelBtnRec.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 75);
        PanelBtnRec.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 225);

        //设置按钮名字
        CreateText(PanelBtn).text = BtnText;
    }

    private GameObject CreatePanel(string PanelName)
    {
        //创建提示界面
        GameObject Panel = new GameObject(PanelName);
        Panel.transform.SetParent(transform);
        Panel.AddComponent<RectTransform>();
        RectTransform PanelRec = Panel.GetComponent<RectTransform>();
        Panel.AddComponent<Image>();
        //Panel.GetComponent<Image>().color = Color.gray;
        Panel.transform.localScale = new Vector3(1, 1, 1);

        //设置尺寸
        PanelRec.anchoredPosition = new Vector2(0, 0);
        PanelRec.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 600);
        PanelRec.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 800);

        return Panel;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        if (跨场景展示)
            DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //提示页确认按钮唤起暂停窗口
        TipPanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            TipPanel.SetActive(false);
            PausePanel.GetComponent<Image>().sprite = RandomImage();
            PausePanel.SetActive(true);
            ShowOV();
        });

        if (PausePanel == null)
            PausePanel = transform.Find(MF_PausePanelController.PanelName).gameObject;

        //暂停页窗口回调被暂停的方法
        PausePanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            if (GameObjectName != null)
                Invoke("CallBack", 0.1f);
            PausePanel.SetActive(false);
            Time.timeScale = m_TimeScale;
        });

        TipPanel.SetActive(false);
        PausePanel.SetActive(false);

        if (定时提示)
            Invoke("ShowTip", TipDelay * 1.5f);

    }

    public Sprite RandomImage()
    {
        if (宣传图 == null)
            return null;
        return 宣传图[Random.Range(0, 宣传图.Count)];
    }

    private void ShowOV()
    {
        ShowAD("ShowOPPO");
        ShowAD("ShowVIVO");
    }

    private void ShowAD(string ADName)
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

        }
#else
        print("调用广告接口：" + ADName);
#endif
    }

    float m_TimeScale = 1;

    /// <summary>
    /// 展示提示页面
    /// </summary>
    public void ShowTip()
    {
        m_TimeScale = Time.timeScale;
        if (preGameObjectName == null)
        {
            TipPanelText.text = 健康提示;
            TipPanel.SetActive(true);
        }
        Invoke("ShowTip", TipDelay);
    }

    /// <summary>
    /// 展示提示页面
    /// </summary>
    /// <param name="info">提示窗显示的内容</param>
    public void ShowTip(string info)
    {
        m_TimeScale = Time.timeScale;
        print(preGameObjectName);
        if (preGameObjectName == null)
        {
            TipPanelText.text = info;
            TipPanel.SetActive(true);
        }
    }
    /// <summary>
    /// 展示提示页面，并且含有倒计时。
    /// </summary>
    /// <param name="info">提示窗显示的内容</param>
    /// <param name="Delay">按钮生效的时间</param>
    public void ShowTip(string info, int Delay)
    {
        m_TimeScale = Time.timeScale;
        //Time.timeScale = 0;
        StartCoroutine(TipBtnDelay(Delay, TipPanel.GetComponentInChildren<Button>()));
        print(preGameObjectName);
        if (preGameObjectName == null)
        {
            TipPanelText.text = info;
            TipPanel.SetActive(true);
        }
    }

    //若干秒后按钮才可以使用的脚本，同时会在按钮上显示倒计时。
    IEnumerator TipBtnDelay(int DelayTime, Button btn)
    {
        Text btnText = btn.GetComponentInChildren<Text>();
        string btnString = btnText.text;
        btn.interactable = false;
        for (int i = DelayTime; i > 0; i--)
        {
            btnText.text = btnString + "(" + i + ")";
            yield return new WaitForSecondsRealtime(1);
        }
        btnText.text = btnString;
        btn.interactable = true;
    }

    /// <summary>
    /// 拉起OV渠道专属的提示页面
    /// </summary>
    /// <param name="_gameobject">调用提示框的挂载体</param>
    /// <param name="_method">断点的方法</param>
    /// <param name="_value">断点的值</param>
    /// <param name="info">提示框的信息</param>
    public void ShowTip(GameObject _gameobject, string _method, string _value, string info)
    {

        print(_gameobject.name + "(" + _gameobject.name + MF_TYPE + ")" + "的 " + _method + " 方法调用了弹窗.\n值为：" + _value + " ,提示信息为： " + info);

        preGameObjectName = _gameobject.name;
        GameObjectName = _gameobject;

        _gameobject.name = _gameobject.name + MF_TYPE;

        MethodName = _method;
        CallBackValue = _value;

        TipPanelText.text = info;
        TipPanel.SetActive(true);
    }

    public void CallBack()
    {
        Time.timeScale = m_TimeScale;
        print("CallBack");
        if (GameObjectName == null)
            return;
        GameObject.Find(preGameObjectName + MF_TYPE).SendMessage(MethodName, CallBackValue);

        GameObjectName.name = preGameObjectName;

        GameObjectName = null;
        MethodName = null;
        CallBackValue = null;
        preGameObjectName = null;
        print("CallBack：" + MethodName + CallBackValue + preGameObjectName);
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

#if UNITY_EDITOR_WIN
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            MF_PausePanelManager.instance.ShowTip("测试弹窗",3);
        }
    }
#endif
}
