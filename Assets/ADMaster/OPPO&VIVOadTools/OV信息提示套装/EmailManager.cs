using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 脚本说明：
///     客服邮箱脚本，挂载在客服邮箱的按钮上即可，可以根据回调控制是否显示客服邮箱按钮。
///     客服邮箱用按钮对象做比较方便一些。
/// </summary>

public class EmailManager : MonoBehaviour
{

    [SerializeField] private Text EmailNum;

    public string TestValue = "1234567890";

    private void Reset()
    {
        print("邮箱检测器已就绪");
        if (name != "MF_EmailManager")
        {
            print("邮箱管理器名字已重置");
            name = "MF_EmailManager";
        }

        RectTransform _rec = GetComponent<RectTransform>();

        //将邮箱固定在屏幕某个位置，第二个参数是距离，第三个参数是尺寸
        _rec.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 5, 0);
        _rec.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 5, 0);

        if (EmailNum == null)
        {
            EmailNum = GetComponentInChildren<Text>();
            EmailNum.color = Color.gray;
            EmailNum.text = "客服邮箱：" + TestValue + "@qq.com";
            EmailNum.fontStyle = FontStyle.Bold;
            EmailNum.alignment = TextAnchor.UpperLeft;
            EmailNum.horizontalOverflow = HorizontalWrapMode.Overflow;
            EmailNum.verticalOverflow = VerticalWrapMode.Overflow;
            EmailNum.fontSize = 20;

            //给文字增加边缘线
            if (!EmailNum.gameObject.GetComponent<Outline>())
            {
                Outline _out = EmailNum.gameObject.AddComponent<Outline>();
                _out.effectColor = Color.white;
                _out.effectDistance = new Vector2(2, 2);
            }
        }
    }

    private void OnEnable()
    {
        //MFADManager.SetEmail(name, "setEmail", TestValue);
        SetEmail(name, "setEmail", TestValue);

        //因为仅仅是用来展示客服邮箱的，所以按钮的组件需要关闭一下。
        if (GetComponent<Button>())
        {
            GetComponent<Button>().enabled = false;
        }
    }

    public void setEmail(string value)
    {
        if (value == "-1")
        {
            print("隐藏" + name);
            gameObject.SetActive(false);
            return;
        }
        EmailNum.text = "客服邮箱：" + value + "@qq.com";
    }

    public void SetEmail(string EmailManagerName, string SetMethod, string testValue)//设置邮箱的接口，回调为-1时将隐藏邮箱
    {
        ShowAD("SetEmail", EmailManagerName, SetMethod, testValue);
#if UNITY_EDITOR_WIN
        print("编辑器回调");
        GameObject.Find(EmailManagerName).SendMessage(SetMethod, testValue);
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
            print("接口：" + ADName + "()没有实现");
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
        
    }
}
