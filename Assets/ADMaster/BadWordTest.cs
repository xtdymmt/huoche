using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadWordTest : MonoBehaviour
{

    public TextAsset badWordText;

    [HideInInspector]
    public string[] BadWordList;

    public static BadWordTest instance;

    #region ===初始化代码===

    public void Reset()
    {
        if (name != "BadWordTestManager")
            name = "BadWordTestManager";
        transform.Zero();
        print("敏感词检测器已就绪");
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

    #region ===核心代码===

    /// <summary>
    /// 检测传入字符是否包含敏感词的方法，因为是遍历检测六千多个关键词，所以运行效率可能会比较慢一些。
    /// 当传入字符串包含敏感词时，将会返回true，否则返回false。
    /// </summary>
    /// <param name="str">需要被检测的字符串</param>
    /// <returns></returns>
    public static bool HaveBadWord(string str)
    {
        //构建敏感词管理器
        CreateBadWordTestManager();

        return new_BadWordTest(str);
    }

    /// <summary>
    /// 检测传入字符是否为敏感词的方法，当传入字符为敏感词时返回true
    /// </summary>
    /// <param name="str">需要被检测的字符串</param>
    /// <returns></returns>
    public static bool IsBadWord(string str)
    {
        //构建敏感词管理器
        CreateBadWordTestManager();

        return f_BadWordTest(str);
    }

    /// <summary>
    /// 检测传入字符是否为非敏感词的方法，当传入字符为非敏感词时返回true
    /// </summary>
    /// <param name="str">需要被检测的字符串</param>
    /// <returns></returns>
    public static bool IsNotBadWord(string str)
    {
        //构建敏感词管理器
        CreateBadWordTestManager();

        return !f_BadWordTest(str);
    }

    private static void CreateBadWordTestManager()
    {
        //创建敏感词检测器
        CreateBadWordTestInstance();

        //加载敏感词文件
        LoadBadWordText();

        //构造敏感词列表
        BuildBadWordList();
    }

    /// <summary>
    /// 检测传入字符串是否为敏感词的方法
    /// </summary>
    /// <param name="str">待检测字符串</param>
    /// <returns></returns>
    private static bool f_BadWordTest(string str)
    {
        if (((IList)instance.BadWordList).Contains(str))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 检测传入字符串是否包含敏感词的方法
    /// </summary>
    /// <param name="str">待检测字符串</param>
    /// <returns></returns>
    private static bool new_BadWordTest(string str)
    {
        foreach (string badWord in instance.BadWordList)
        {
            if (str.Contains(badWord))
                return true;
        }
        return false;
    }

    private static void CreateBadWordTestInstance()
    {
        if (instance == null)
        {
            print("敏感词检测器不存在，创建敏感词检测器");
            GameObject obj = new GameObject();
            obj.AddComponent<BadWordTest>();
            obj.GetComponent<BadWordTest>().Reset();
        }
        else
        {
            print("敏感词检测器已创建");
        }
    }

    private static void LoadBadWordText()
    {
        if (instance.badWordText == null)
        {
            print("没有加载屏蔽词文件");
            instance.badWordText = Resources.Load<TextAsset>("BadWord");
            print("屏蔽词文件加载成功");
        }
        else
        {
            print("已加载屏蔽词文件");
        }
    }

    private static void BuildBadWordList()
    {
        if (instance.BadWordList.Length == 0)
        {
            print("没有加载屏蔽词列表");
            instance.BadWordList = instance.badWordText.text.Split('、', ',', '\n');
            print("屏蔽词列表加载成功");
        }
        else
        {
            print("已加载屏蔽词列表");
        }
    }

    #endregion

}
