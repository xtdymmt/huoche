using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAdOnClickBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out Button button))
        {
            button.onClick.AddListener(() =>
            {
                LSC_ADManager.Instance.ShowCustom();
            });
        }
    }
}
