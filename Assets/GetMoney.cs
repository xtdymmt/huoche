using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMoney : MonoBehaviour
{
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LSC_ADManager.Instance.ShowReward(() =>
            {
                PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 5000);
                ScoreText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
            });
        });
    }
}
