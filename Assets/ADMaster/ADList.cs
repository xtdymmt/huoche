using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 脚本说明：
/// 
///     按钮列表，可以在游戏运行时储存数据。
///     实际的使用方法是，在游戏运行的时候，
/// 点击获取按钮的名字，然后将按钮的名字复制
/// 粘贴到列表里对应的区域，然后按键盘上的 B
/// 键刷新按钮列表。
/// 
///     三个布尔值的含义分别为：
///     关闭Log：关闭按钮点击时打印的log；
///     打印按钮名字：显示打印按钮名字的log；
///     接口Log：显示调用广告接口名的log；
///     
///     _ADManager_Log这个开关在接完所有按钮
/// 广告后，务必打开以测试接口的调用情况。
/// </summary>
[CreateAssetMenu(fileName = "btnManager", menuName = "摩丰广告大师/按钮管理器")]

public class ADList : ScriptableObject
{
    public bool 关闭Log = false;
    public bool 打印按钮名字 = true;
    public bool 接口Log = false;
    //public BtnList[] 按钮列表;// = new BtnList[9];
    public BtnList[] 按钮列表 = {
        new BtnList(ADType.OV都弹广告按钮),
        new BtnList(ADType.OPPO广告的按钮),
        new BtnList(ADType.VIVO广告的按钮),
        new BtnList(ADType.测试原生的按钮),
        new BtnList(ADType.测试插屏的按钮),
        new BtnList(ADType.游戏操作的按钮),
        new BtnList(ADType.激励广告的按钮),
        new BtnList(ADType.不接广告的按钮),
        new BtnList(ADType.更新列表的按钮)
    };

}

public enum ADType
{
    OV都弹广告按钮,
    OPPO广告的按钮,
    VIVO广告的按钮,
    测试原生的按钮,
    测试插屏的按钮,
    游戏操作的按钮,
    激励广告的按钮,
    不接广告的按钮,
    更新列表的按钮
}

[Serializable]
public class BtnList
{

    public BtnList(ADType ListName)
    {
        m_adType = ListName;
        name = ListName.ToString();
    }

    public bool Contains(string btn_name)
    {
        return 按钮列表.Contains(btn_name);
    }

    public void Add(string btn_name)
    {
        按钮列表.Add(btn_name);
    }

    public void Remove(string btn_name)
    {
        按钮列表.Remove(btn_name);
    }


    [HideInInspector]
    public string name;
    private ADType m_adType;
    public ADType adType
    {
        get
        {
            return m_adType;
        }
    }

    [SerializeField]
    private List<String> 按钮列表;
}

//[Serializable]
//public class TogList
//{
//    public List<String> 不接广告的触发器 = new List<String> { null, null, null, null, null };
//    public List<String> 游戏操作的触发器 = new List<String> { null, null, null, null, null };
//    public List<String> 旧的广告的触发器 = new List<String> { null, null, null, null, null };
//}