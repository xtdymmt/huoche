using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// 脚本说明：
///     当游戏比较奇葩，按钮功能不是使用Unity自带的UI组件实现、但是是图片的话，
/// 可以将这个脚本挂载到那张图片上，一般能行。
///     我在这边预设了四个我经常会用到的接口名字，挂载到图片上以后可以通过下拉
/// 菜单选择，默认是ShowADOnAnyWhere，想要扩充的话在下面的ADClass中添加即可。
/// </summary>
public class ClickAD : MonoBehaviour,IPointerDownHandler
{
    public ADClass _adtype = ADClass.ShowADOnAnyWhere;

    public int 弹出广告的点击次数 = 0;
    private int ClickTime;
    public void OnPointerDown(PointerEventData eventData)
    {
        ClickTime++;
        if (ClickTime > 弹出广告的点击次数)
        {
            ClickTime = 0;
            ShowAD();
        }
    }

    public void ShowAD()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call(_adtype.ToString());
                }
            }
        }
        catch
        {

        }
#else
        print(_adtype.ToString());
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

public enum ADClass
{
    ShowTestNative,
    ShowTestTable,
    ShowADOnPlay,
    ShowADOnAnyWhere,
    ShowOPPO,
    ShowVIVO,
    ShowOV
}
