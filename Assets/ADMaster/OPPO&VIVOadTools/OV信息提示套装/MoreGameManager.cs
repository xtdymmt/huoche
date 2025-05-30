using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 脚本说明：
///     更多精彩脚本，挂在更多精彩按钮上即可，可以根据回调控制是否显示更多精彩按钮。
/// </summary>

public class MoreGameManager : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        MoreGame();
        //throw new System.NotImplementedException();
    }

    public string TestValue = "1";

    private void Reset()
    {
        print("更多精彩检测器已就绪");
        if (name != "MF_MoreGameManager")
        {
            print("更多精彩管理器名字已重置");
            name = "MF_MoreGameManager";
        }

        //RectTransform _rec = GetComponent<RectTransform>();

        ////将邮箱固定在屏幕某个位置，第二个参数是距离，第三个参数是尺寸
        //_rec.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 5, 120);
        //_rec.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 5 + 30 + 5, 30);

        if (GetComponentInChildren<Text>())
        {
            print("更多精彩按钮文案已重置为“更多精彩”");
            Text _text = GetComponentInChildren<Text>();
            _text.text = "更多精彩";
            //_text.fontStyle = FontStyle.Normal;
            ////_text.horizontalOverflow = HorizontalWrapMode.Overflow;
            ////_text.verticalOverflow = VerticalWrapMode.Overflow;
            //_text.fontSize = 18;
        }

    }

    private bool FirstEnable = false;

    private void OnEnable()
    {
        //MFADManager.SetMoreGame(name, "setMoreGame", TestValue);
        SetMoreGame(name, "setMoreGame", TestValue);

        //if (FirstEnable)
        //    return;
        //FirstEnable = true;

        //GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    //MFADManager.MoreGame();
        //    MoreGame();
        //});
    }

    public void setMoreGame(string value)
    {
        if (value == "-1")
        {
            print("隐藏" + name);
            gameObject.SetActive(false);
            return;
        }
    }

    public void SetMoreGame(string MoreGameManagerName, string SetMethod, string testValue)//设置是否显示更多精彩的接口，回调为-1时将隐藏更多精彩按钮
    {
        ShowAD("SetMoreGame", MoreGameManagerName, SetMethod, testValue);
#if UNITY_EDITOR_WIN
        print("编辑器回调");
        GameObject.Find(MoreGameManagerName).SendMessage(SetMethod, testValue);
#endif
    }

    public void MoreGame()//点击更多精彩时调用的接口
    {
        ShowAD("MoreGame");
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
            print("接口：" + ADName + "()没有实现");
        }
#else
        print(ADName);
#endif
    }

    private void ShowAD(string ADName, string GameObjectName, string Method, string Value)
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR_WIN
        if (Input.GetKey(KeyCode.R))
        {
            print("移除 " + name + " 所有监听器");
            GetComponent<Button>().onClick.RemoveAllListeners();
        }
#endif
    }
}
