using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NativeTestManager : MonoBehaviour
{
    public static NativeTestManager instance;
    public GameObject _testNative;
    public Text _infoText;
    [TextArea]
    public string _info;

    public static void ShowTestNative()
    {
        initNativeTestManager();
        instance._infoText.text = instance._info;
        instance._testNative.SetActive(true);
    }

    public static void ShowTestNative(string info)
    {
        initNativeTestManager();
        instance._infoText.text = info;
        instance._testNative.SetActive(true);
    }

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        _testNative = transform.GetChild(0).gameObject;
        DontDestroyOnLoad(gameObject);
    }

    public static void initNativeTestManager()
    {
        if (instance != null)
            return;
        var NativeTest = Resources.Load("MFTestCanvas");
        GameObject obj = GameObject.Instantiate(NativeTest) as GameObject;
    }
}
