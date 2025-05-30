using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseLoad : MonoBehaviour
{
    void Start()
    {
        
        //30s
        Invoke("loadTimeFinish", 30);
    }
    void loadTimeFinish()
    {
        //关闭假的加载页
        gameObject.SetActive(false);
    }
}
