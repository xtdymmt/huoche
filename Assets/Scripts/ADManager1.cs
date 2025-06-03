using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 脚本说明：
///     把这个脚本挂载在图片上，点击即可弹广告。
/// </summary>

public class ADManager1 : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("ShowButton1");
        //HuaWeiADManager.ShowButton1();
        LSC_ADManager.Instance.ShowCustom();
    }
}