using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 脚本说明：
///     把这个脚本挂载在图片上，点击即可弹广告。
/// </summary>

public class ADManager : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("ShowButton");
        HuaWeiADManager.ShowButton();
    }
}