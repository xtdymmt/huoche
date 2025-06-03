using System.Collections;
using System.Collections.Generic;
//using QGMiniGame;
using UnityEngine;

public class PrivacyPolicy : MonoBehaviour
{
    public GameObject healthAdvice;
    public GameObject privacyPolicyNode;
    public bool isOnly = false;
    // Start is called before the first frame update
    void Start()
    {
        if (LSC_ADManager.Instance.PlayerPrefsGetInt("PrivacyPolicy", 0) == 1)
        {
            if (!isOnly)
            {
                ShowHealthAdvice();
            }
        }
    }
    public void UserAgree()
    {
        LSC_ADManager.Instance.PlayerPrefsSetInt("PrivacyPolicy", 1);
        if (isOnly)
        {
            ADSceneManager.Instance.CloseScene();
        }
        else
        {
            ShowHealthAdvice();
        }
    }
    private void ShowHealthAdvice()
    {
        privacyPolicyNode.SetActive(false);
        healthAdvice.SetActive(true);
        StartCoroutine(StartGame());
    }
    public void UserDisagree()
    {
        LSC_ADManager.Instance.ExitApplication();
    }
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        privacyPolicyNode.SetActive(true);
        healthAdvice.SetActive(false);
        ADSceneManager.Instance.CloseScene();
        LSC_ADManager.Instance.StartGame.Invoke();
    }
}