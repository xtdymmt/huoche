using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetCanvasScaleSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enumerator());
    }
    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.2f);
        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
        if ((float)canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y < (float)Screen.width / Screen.height)
        {
            canvasScaler.matchWidthOrHeight = 1;
        }
    }
}
