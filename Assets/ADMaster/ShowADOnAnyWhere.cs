using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowADOnAnyWhere : MonoBehaviour
{
    void Reset()
    {
        transform.Zero();
        name = name == "ShowADOnAnyWhere" ? name : "ShowADOnAnyWhere";
    }

    // Start is called before the first frame update
    void Start()
    {
        var all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        //获取场景里的所有按钮
        foreach (var item in all)
        {
            if (item.scene.isLoaded && item.GetComponent<Button>())
            {
                item.GetComponent<Button>().onClick.AddListener(MFADManager.ShowADOnAnyWhere);
            }
            if (item.scene.isLoaded && item.GetComponent<Toggle>())
            {
                item.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => { MFADManager.ShowADOnAnyWhere(); });
            }
        }
    }
}
