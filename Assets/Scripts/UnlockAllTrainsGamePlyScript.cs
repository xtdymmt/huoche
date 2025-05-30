// dnSpy decompiler from Assembly-CSharp.dll class: UnlockAllTrainsGamePlyScript
using System;
using UnityEngine;
using UnityEngine.UI;

public class UnlockAllTrainsGamePlyScript : MonoBehaviour
{
	private void OnEnable()
	{
		if (PlayerPrefs.GetInt("CarTwoPurcahsed") == 0 && PlayerPrefs.GetInt("CarThreePurcahsed") == 0 && PlayerPrefs.GetInt("CarFourPurcahsed") == 0 && PlayerPrefs.GetInt("CarFivePurcahsed") == 0)
		{
			//this.INAPPScript = (INAPP)UnityEngine.Object.FindObjectOfType(typeof(INAPP));
			this.UnlockAllTrainBtn = base.gameObject.GetComponent<Button>();
			this.UnlockAllTrainBtn.onClick.AddListener(delegate()
			{
			});
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	private void FixedUpdate()
	{
		if (PlayerPrefs.GetInt("CarTwoPurcahsed") == 0 && PlayerPrefs.GetInt("CarThreePurcahsed") == 0 && PlayerPrefs.GetInt("CarFourPurcahsed") == 0 && PlayerPrefs.GetInt("CarFivePurcahsed") == 0)
		{
			base.gameObject.SetActive(true);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}



	private Button UnlockAllTrainBtn;
}
