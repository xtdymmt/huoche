using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ADSceneManager : MonoBehaviour
{
    public PrivacyPolicy privacyPolicy;
    public Treasure treasure;
    public enum ADSceneType
    {
        None,
        PrivacyPolicy,
        PrivacyPolicyOnly,
        Treasure,
    }
    public static ADSceneType adSceneType = ADSceneType.None;
    public static ADSceneManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        privacyPolicy.gameObject.SetActive(false);
        treasure.gameObject.SetActive(false);
        switch (adSceneType)
        {
            case ADSceneType.None:
                CloseScene();
                break;
            case ADSceneType.PrivacyPolicy:
                privacyPolicy.isOnly = false;
                privacyPolicy.gameObject.SetActive(true);
                break;
            case ADSceneType.PrivacyPolicyOnly:
                privacyPolicy.isOnly = true;
                privacyPolicy.gameObject.SetActive(true);
                break;
            case ADSceneType.Treasure:
                Debug.Log("?");
                treasure.gameObject.SetActive(true);
                break;
        }
    }
    public void CloseScene()
    {
        privacyPolicy.gameObject.SetActive(false);
        treasure.gameObject.SetActive(false);
        SceneManager.UnloadSceneAsync("LSC_ADScene");
    }
}

