// dnSpy decompiler from Assembly-CSharp.dll class: DailyRewardCheck
using System;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardCheck : MonoBehaviour
{
	private void Update()
	{

		if (Input.GetKeyDown("r"))
		{
			Debug.Log("已清空全部数据");
			PlayerPrefs.DeleteAll();
		}
	}
	private void Start()
	{
		if (PlayerPrefs.GetInt("FTime") == 0)
		{
			PlayerPrefs.SetString("PlayDate", DateTime.Now.ToString("MM/dd/yyy hh:mm:ss"));
			this.BtnCheckkCall();
			PlayerPrefs.SetInt("FTime", 1);
		}
		this.dayCheck();
	}

	public void DailyReward_Claim_Btn_Click()
	{
		if (PlayerPrefs.GetInt("ClaimRewardDB") == 0)
		{
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 100);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 1)
		{
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 200);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 2)
		{
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 300);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 3)
		{
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 400);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 4)
		{
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 500);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 5)
		{
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 600);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 6)
		{
			PlayerPrefs.SetInt("HighScoreDB", PlayerPrefs.GetInt("HighScoreDB") + 700);
		}
		this.ScoreText.text = PlayerPrefs.GetInt("HighScoreDB").ToString();
		if (PlayerPrefs.GetInt("ClaimRewardDB") >= 6)
		{
			PlayerPrefs.SetInt("ClaimRewardDB", 0);
		}
		else
		{
			PlayerPrefs.SetInt("ClaimRewardDB", PlayerPrefs.GetInt("ClaimRewardDB") + 1);
		}
		this.DailyRewradPanel.SetActive(false);
	}

	public void DailyReward_Cancel_Btn_Click()
	{
		this.DailyRewradPanel.SetActive(false);
	}

	public void dayCheck()
	{
		string @string = PlayerPrefs.GetString("PlayDate");
		DateTime value = Convert.ToDateTime(@string);
		DateTime now = DateTime.Now;
		if (now.Subtract(value).Days >= 1)
		{
			string value2 = Convert.ToString(now);
			PlayerPrefs.SetString("PlayDate", value2);
			this.BtnCheckkCall();
		}
	}

	private void BtnCheckkCall()
	{
		this.DailyRewradPanel.SetActive(true);
		if (PlayerPrefs.GetInt("ClaimRewardDB") == 0)
		{
			this.Blink_img1.SetActive(true);
			this.Blink_img2.SetActive(false);
			this.Blink_img3.SetActive(false);
			this.Blink_img4.SetActive(false);
			this.Blink_img5.SetActive(false);
			this.Blink_img6.SetActive(false);
			this.Blink_img7.SetActive(false);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 1)
		{
			this.Blink_img1.SetActive(false);
			this.Blink_img2.SetActive(true);
			this.Blink_img3.SetActive(false);
			this.Blink_img4.SetActive(false);
			this.Blink_img5.SetActive(false);
			this.Blink_img6.SetActive(false);
			this.Blink_img7.SetActive(false);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 2)
		{
			this.Blink_img1.SetActive(false);
			this.Blink_img2.SetActive(false);
			this.Blink_img3.SetActive(true);
			this.Blink_img4.SetActive(false);
			this.Blink_img5.SetActive(false);
			this.Blink_img6.SetActive(false);
			this.Blink_img7.SetActive(false);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 3)
		{
			this.Blink_img1.SetActive(false);
			this.Blink_img2.SetActive(false);
			this.Blink_img3.SetActive(false);
			this.Blink_img4.SetActive(true);
			this.Blink_img5.SetActive(false);
			this.Blink_img6.SetActive(false);
			this.Blink_img7.SetActive(false);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 4)
		{
			this.Blink_img1.SetActive(false);
			this.Blink_img2.SetActive(false);
			this.Blink_img3.SetActive(false);
			this.Blink_img4.SetActive(false);
			this.Blink_img5.SetActive(true);
			this.Blink_img6.SetActive(false);
			this.Blink_img7.SetActive(false);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 5)
		{
			this.Blink_img1.SetActive(false);
			this.Blink_img2.SetActive(false);
			this.Blink_img3.SetActive(false);
			this.Blink_img4.SetActive(false);
			this.Blink_img5.SetActive(false);
			this.Blink_img6.SetActive(true);
			this.Blink_img7.SetActive(false);
		}
		else if (PlayerPrefs.GetInt("ClaimRewardDB") == 6)
		{
			this.Blink_img1.SetActive(false);
			this.Blink_img2.SetActive(false);
			this.Blink_img3.SetActive(false);
			this.Blink_img4.SetActive(false);
			this.Blink_img5.SetActive(false);
			this.Blink_img6.SetActive(false);
			this.Blink_img7.SetActive(true);
		}
	}

	public GameObject DailyRewradPanel;

	public Text ScoreText;

	public GameObject Blink_img1;

	public GameObject Blink_img2;

	public GameObject Blink_img3;

	public GameObject Blink_img4;

	public GameObject Blink_img5;

	public GameObject Blink_img6;

	public GameObject Blink_img7;
}
