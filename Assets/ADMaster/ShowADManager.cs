using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
/// <summary>
/// 脚本说明：
///     该脚本可以检测场景变化，在更换场景时更新按钮列表并重新为之添加监听器。
///     在编辑器里按 R 可以重置由PlayerPrefs记录的用户数据，在大部分的游戏中可以清空游戏存档，
/// 或者重置金钱数量等。
///     在编辑器里按 B 可以在当前场景重置按钮列表，移除所有按钮的监听器，然后重新添加。值得注
/// 意的是，在小部分很奇葩的游戏里实现按钮功能的方式就是在脚本里检测按钮然后添加监听器，在这里
/// 直接移除监听器就会引起按钮失效的问题，这种情况下就只能通过重启游戏或者切换场景的方式来更新
/// 按钮列表了。
///     当这些功能按键和游戏的操作键冲突时，可以在这个脚本的底部那里进行修改。
/// </summary>
public class ShowADManager : MonoBehaviour
{

    [SerializeField] private Scene _thisScene;

    [SerializeField] private List<Button> _allButton;
    [SerializeField] private List<Toggle> _allToggle;

    [SerializeField] private static ADList _ADList;

    public static ShowADManager _instance;   // 单例
    public ShowADManager ShowADOnAnyWhereInstance
    {
        get { return _instance; }
    }

    public static void doNothing()
    {

    }

    private void Reset()
    {
        transform.Zero();
        print("按钮检测器已就绪");
        if (GetComponent<MFADManager>() == null)
            this.gameObject.AddComponent<MFADManager>();
        if (name != "MF_ADManager")
            name = "MF_ADManager";
    }

    private void OnLevelWasLoaded(int level)//加载场景时调用
    {
        //CreateListener();//如果场景已经变化，则重新创建监听器
        isNewScene();
    }

    void Awake()
    {
        if (_instance != null)
        {
            //这里一定要是销毁this.gameObject
            Destroy(this.gameObject);
            //print("销毁了一个多余的监听器");
            return;
        }
        //这句话只执行一次，第二次上面return了
        _instance = this;

        _ADList = Resources.Load<ADList>("btnManager");
        DontDestroyOnLoad(this);//加载场景时不要自动销毁该脚本挂载的物体
        isNewScene();
    }


    // Use this for initialization
    //void Start()
    //{
    //    _ADList = Resources.Load<ADList>("btnManager");
    //    DontDestroyOnLoad(this);//加载场景时不要自动销毁该脚本挂载的物体
    //    isNewScene();
    //    //CreateListener();//创建场景按钮监听器
    //    //InvokeRepeating("isNewScene", 0.5f, 1);//游戏启动的0.5秒以后，每秒钟检查一下场景是否改变
    //}

    private void isNewScene()
    {
        print("检测场景加载是否完成");
        if (_thisScene == SceneManager.GetActiveScene())//如果当前场景与记录中的场景一致，则直接返回
            return;
        if (SceneManager.GetActiveScene().isLoaded)
            CreateListener();//如果场景已经变化，则重新创建监听器
        //Invoke("CreateListener", 5);
        else
            Invoke("isNewScene", 0.5f);
    }

    public static void ShowClickTipe(string info)
    {
#if UNITY_EDITOR_WIN
        try
        {
            if (!_ADList.关闭Log)
                print(info);
        }
        catch
        {
            print(info);
        }
#endif
    }

    public static bool checkButton(ADType ad_type, string btn_name)
    {
        BtnList btn_list = _ADList.按钮列表[(int)ad_type];
        if (btn_list.Contains(btn_name))
        {
            ShowClickTipe("按钮 \"" + btn_name + "\" 是 " + ad_type.ToString() + " 。");
        }
        else
        {
            return false;
        }
        return true;
    }

    public static bool checkButton(ADType ad_type, string btn_name, Action ADMethpd)
    {
        BtnList btn_list = _ADList.按钮列表[(int)ad_type];
        if (btn_list.Contains(btn_name))
        {
            ShowClickTipe("按钮 \"" + btn_name + "\" 是 " + ad_type.ToString() + " 。");
        }
        else
        {
            return false;
        }
        if (ADMethpd != null)
        {
            ADMethpd();
        }
        return true;
    }

    public static void ClickEvent(string btn_name)
    {
        //添加按钮标记检测
        AdAddBtn(KeyCode.A, KeyCode.N, ADType.OV都弹广告按钮, btn_name);
        AdAddBtn(KeyCode.A, KeyCode.O, ADType.OPPO广告的按钮, btn_name);
        AdAddBtn(KeyCode.A, KeyCode.V, ADType.VIVO广告的按钮, btn_name);
        AdAddBtn(KeyCode.T, KeyCode.N, ADType.测试原生的按钮, btn_name);
        AdAddBtn(KeyCode.T, KeyCode.I, ADType.测试插屏的按钮, btn_name);
        AdAddBtn(KeyCode.T, KeyCode.P, ADType.游戏操作的按钮, btn_name);
        AdAddBtn(KeyCode.T, KeyCode.V, ADType.激励广告的按钮, btn_name);
        AdAddBtn(KeyCode.H, KeyCode.D, ADType.不接广告的按钮, btn_name);
        AdAddBtn(KeyCode.T, KeyCode.C, ADType.更新列表的按钮, btn_name);

        //移除按钮标记检测
        AdRemoveBtn(KeyCode.T, KeyCode.D, btn_name);

        //按钮标记检测
        checkButton(ADType.OV都弹广告按钮, btn_name, MFADManager.ShowOV);
        checkButton(ADType.OPPO广告的按钮, btn_name, MFADManager.ShowOPPO);
        checkButton(ADType.VIVO广告的按钮, btn_name, MFADManager.ShowVIVO);
        checkButton(ADType.测试原生的按钮, btn_name, MFADManager.ShowTestNative);
        checkButton(ADType.测试插屏的按钮, btn_name, MFADManager.ShowTestTable);
        checkButton(ADType.更新列表的按钮, btn_name, RefreshBtnList);

        if (!checkButton(ADType.游戏操作的按钮, btn_name, MFADManager.ShowADOnPlay) &&
            !checkButton(ADType.激励广告的按钮, btn_name) &&
            !checkButton(ADType.不接广告的按钮, btn_name))
        {
            ShowClickTipe("按钮 \"" + btn_name + "\" 是 点哪都弹的按钮 。");
            MFADManager.ShowADOnAnyWhere();
        }

        #region ===旧代码===
        //if (_ADList.广告类型[((int)ADType.OV都弹广告按钮)].Contains("")) { }

        //        if (_ADList.按钮列表.OV都弹广告按钮.Contains(btn_name))
        //        {
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"是OPPO和VIVO都弹广告的按钮。");
        //#endif
        //            MFADManager.ShowOV();
        //        }
        //        else if (_ADList.按钮列表.OPPO广告的按钮.Contains(btn_name))
        //        {
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"是OPPO渠道的广告按钮。");
        //#endif
        //            MFADManager.ShowOPPO();
        //        }
        //        else if (_ADList.按钮列表.VIVO广告的按钮.Contains(btn_name))
        //        {
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"是VIVO渠道的广告按钮。");
        //#endif
        //            MFADManager.ShowVIVO();
        //        }
        //        else if (_ADList.按钮列表.不接广告的按钮.Contains(btn_name))
        //        {
        //            //虽然都已经在不弹广告的列表里的了，但来都来了，打个log再走呗
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"没有添加广告。");
        //#endif
        //        }
        //        else if (_ADList.按钮列表.激励广告的按钮.Contains(btn_name))
        //        {
        //            //为了避免激励广告和其他广告一起弹出来，所以这边要特别记录一下，不做任何处理，同样来都来了，打个log再走
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"添加了激励广告。");
        //            if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.D) && _ADList.按钮列表.激励广告的按钮.Contains(btn_name))
        //            {
        //                ShowClickTipe("按钮\"" + btn_name + "\"修改为点哪都弹广告的按钮。");
        //                _ADList.按钮列表.激励广告的按钮.Remove(btn_name);
        //            }
        //#endif
        //        }
        //        else if (_ADList.按钮列表.游戏操作的按钮.Contains(btn_name))
        //        {
        //            //游戏中的操作键弹出广告
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"是游戏的操作键按钮，添加了操作键广告。");
        //            if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.D) && _ADList.按钮列表.游戏操作的按钮.Contains(btn_name))
        //            {
        //                ShowClickTipe("按钮\"" + btn_name + "\"修改为点哪都弹广告的按钮。");
        //                _ADList.按钮列表.游戏操作的按钮.Remove(btn_name);
        //            }
        //#endif
        //            MFADManager.ShowADOnPlay();
        //        }
        //        else if (_ADList.按钮列表.游戏启动的按钮.Contains(btn_name))
        //        {
        //            //用来和其他按钮做区分的接口，用来实现一些策略，但现在基本上都是点哪都弹了，所以分出来的意义不大
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"是游戏的启动按钮，添加了启动广告。");
        //#endif
        //            MFADManager.ShowADOnStart();
        //        }
        //        else if (_ADList.按钮列表.游戏暂停的按钮.Contains(btn_name))
        //        {
        //            //用来和其他按钮做区分的接口，用来实现一些策略，但现在基本上都是点哪都弹了，所以分出来的意义不大
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"是游戏的暂停按钮，调用了暂停的广告");
        //#endif
        //            MFADManager.ShowADOnPause();
        //        }
        //        else if (_ADList.按钮列表.游戏设置的按钮.Contains(btn_name))
        //        {
        //            //用来和其他按钮做区分的接口，用来实现一些策略，但现在基本上都是点哪都弹了，所以分出来的意义不大
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"是游戏的设置按钮，调用了设置的广告");
        //#endif
        //            MFADManager.ShowADOnSetting();
        //        }
        //        else if (_ADList.按钮列表.返回功能的按钮.Contains(btn_name))
        //        {
        //            //为了避免激励广告和其他广告一起弹出来，所以这边要特别记录一下，不做任何处理，同样来都来了，打个log再走
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"是返回键按钮，添加了广告。");
        //#endif
        //            MFADManager.ShowADOnBack();
        //        }
        //        else if (_ADList.按钮列表.旧的广告的按钮.Contains(btn_name))
        //        {
        //            //旧的工程里有旧的接口，当你不想和他们重叠在一起的时候就记在这里。
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"已经添加了广告，不再额外增加广告接口。");
        //#endif
        //        }
        //        else if (_ADList.按钮列表.测试原生的按钮.Contains(btn_name))
        //        {
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"是测试原生的按钮，会调用测试原生接口。");
        //            if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.D) && _ADList.按钮列表.测试原生的按钮.Contains(btn_name))
        //            {
        //                ShowClickTipe("按钮\"" + btn_name + "\"修改为点哪都弹广告的按钮。");
        //                _ADList.按钮列表.测试原生的按钮.Remove(btn_name);
        //            }
        //#endif
        //            MFADManager.ShowTestNative();
        //        }
        //        else if (_ADList.按钮列表.测试插屏的按钮.Contains(btn_name))
        //        {
        //#if UNITY_EDITOR_WIN
        //            ShowClickTipe("按钮\"" + btn_name + "\"是测试插屏的按钮，会调用测试插屏接口。");
        //            if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.D) && _ADList.按钮列表.测试插屏的按钮.Contains(btn_name))
        //            {
        //                ShowClickTipe("按钮\"" + btn_name + "\"修改为点哪都弹广告的按钮。");
        //                _ADList.按钮列表.测试插屏的按钮.Remove(btn_name);
        //            }
        //#endif
        //            MFADManager.ShowTestTable();
        //        }
        //        else
        //        {
        //            //所有没有特别记录的按钮全部通过ShowADOnAnyWhere这个接口弹广告
        //#if UNITY_EDITOR_WIN

        //            ShowClickTipe("按钮\"" + btn_name + "\"是点哪都弹广告的按钮。");
        //            #region 华为按钮
        //            if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.V) && !_ADList.按钮列表.激励广告的按钮.Contains(btn_name))
        //            {
        //                _ADList.按钮列表.激励广告的按钮.Add(btn_name);
        //                ShowClickTipe("按钮\"" + btn_name + "\"更换为激励按钮。");
        //            }
        //            if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.P) && !_ADList.按钮列表.游戏操作的按钮.Contains(btn_name))
        //            {
        //                _ADList.按钮列表.游戏操作的按钮.Add(btn_name);
        //                ShowClickTipe("按钮\"" + btn_name + "\"更换为操作按钮。");
        //            }
        //            if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.N) && !_ADList.按钮列表.测试原生的按钮.Contains(btn_name))
        //            {
        //                _ADList.按钮列表.测试原生的按钮.Add(btn_name);
        //                ShowClickTipe("按钮\"" + btn_name + "\"更换为测试原生的按钮。");
        //            }
        //            if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.I) && !_ADList.按钮列表.测试插屏的按钮.Contains(btn_name))
        //            {
        //                _ADList.按钮列表.测试插屏的按钮.Add(btn_name);
        //                ShowClickTipe("按钮\"" + btn_name + "\"更换为测试插屏的按钮。");
        //            }
        //            #endregion

        //            #region OV按钮
        //            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.O) && !_ADList.按钮列表.OPPO广告的按钮.Contains(btn_name))
        //            {
        //                _ADList.按钮列表.OPPO广告的按钮.Add(btn_name);
        //                ShowClickTipe("按钮\"" + btn_name + "\"更换为OPPO广告的按钮。");
        //            }
        //            #endregion
        //#endif
        //            MFADManager.ShowADOnAnyWhere();
        //        }
        #endregion

    }

    private static void AdAddBtn(KeyCode key_1, KeyCode key_2, ADType adType, string btn_name)
    {
        if (Input.GetKey(key_1) && Input.GetKey(key_2) && !_ADList.按钮列表[(int)adType].Contains(btn_name))
        {
            ShowClickTipe("按钮\"" + btn_name + "\"更换为 " + adType.ToString() + " 。");
            _ADList.按钮列表[(int)adType].Add(btn_name);
        }
    }

    private static void AdRemoveBtn(KeyCode key_1, KeyCode key_2, string btn_name)
    {
        if (Input.GetKey(key_1) && Input.GetKey(key_2))
        {
            foreach (BtnList btn_list in _ADList.按钮列表)
            {
                if (btn_list.Contains(btn_name))
                {
                    ShowClickTipe("按钮\"" + btn_name + "\"从列表 " + btn_list.adType.ToString() + " 移除。");
                    btn_list.Remove(btn_name);
                }
            }
        }
    }

    public static void RefreshBtnList()
    {
        print("更新列表");
        _instance.Invoke("RefreshBtnList_Con", 1);
    }

    public void RefreshBtnList_Con()
    {
        var all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        foreach (var item in all)
        {
            if (item.scene.isLoaded && item.GetComponent<Button>())
            {
                if (!_allButton.Contains(item.GetComponent<Button>()))
                {
                    item.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        ClickEvent(item.name);
                    });
                    _allButton.Add(item.GetComponent<Button>());
                }
            }
            if (item.scene.isLoaded && item.GetComponent<Toggle>())
            {
                if (!_allToggle.Contains(item.GetComponent<Toggle>()))
                {
                    item.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) =>
                    {
                        ClickEvent(item.name);
                    });
                    _allToggle.Add(item.GetComponent<Toggle>());
                }
            }
        }
    }

    private void CreateListener()
    {
        _allButton.Clear();
        _allToggle.Clear();
        _thisScene = SceneManager.GetActiveScene();//记录当前场景
        print("新场景已加载，场景名字" + _thisScene.name);
        var all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        //获取场景里的所有按钮
        foreach (var item in all)
        {
            if (item.scene.isLoaded && item.GetComponent<Button>())
            {
                _allButton.Add(item.GetComponent<Button>());
            }
            if (item.scene.isLoaded && item.GetComponent<Toggle>())
            {
                _allToggle.Add(item.GetComponent<Toggle>());
            }
        }

        //为场景里的所有按钮增加点击的监听事件
        for (int i = 0; i < _allButton.Count; i++)
        {
            int index = i;
            _allButton[index].onClick.AddListener(() =>
            {
                ClickEvent(_allButton[index].name);
            });
        }

        for (int i = 0; i < _allToggle.Count; i++)
        {
            int index = i;
            _allToggle[index].onValueChanged.AddListener((bool isOn) =>
            {
                ClickEvent(_allToggle[index].name);
            });
        }
    }

    //public void CleanAllBtnListener()
    //{
    //    for (int i = 0; i < _allButton.Count; i++)
    //    {
    //        int index = i;
    //        _allButton[index].onClick.RemoveAllListeners();
    //    }
    //    for (int i = 0; i < _allToggle.Count; i++)
    //    {
    //        int index = i;
    //        _allToggle[index].onValueChanged.RemoveAllListeners();
    //    }
    //}

    //    // Update is called once per frame
#if UNITY_EDITOR_WIN
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.D))
        {
            print("重置游戏数据");
            PlayerPrefs.DeleteAll();
        }
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    Debug.LogError("Current Key is : " + keyCode.ToString());
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    print("重置按钮列表");
        //    CleanAllBtnListener();
        //    CreateListener();
        //}
    }
#endif
}

public static class MF_KuoZhan
{
    public static Transform Zero(this Transform t_transform)
    {
        t_transform.position = Vector3.zero;
        t_transform.eulerAngles = Vector3.zero;
        t_transform.localScale = Vector3.one;
        return t_transform;
    }
}
