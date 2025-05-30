using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 脚本说明：
///     挂在在胜利失败的界面上，这是在胜利失败处加广告最方便的方法
/// </summary>
public class ShowADOnFinish : MonoBehaviour
{
    private void OnEnable()
    {
        MFADManager.ShowADOnFinish();
    }
}
