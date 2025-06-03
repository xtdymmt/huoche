// // dnSpy decompiler from Assembly-CSharp.dll class: PrivacyPolicy
// using System;
// using UnityEngine;

// public class PrivacyPolicy : MonoBehaviour
// {
// 	private void Start()
// 	{
// 		if (PlayerPrefs.GetInt("PolicyCheckDB") == 1)
// 		{
// 			this.privacyPolicyDialogue.SetActive(false);
// 		}
// 		else
// 		{
// 			this.privacyPolicyDialogue.SetActive(true);
// 		}
// 	}

// 	public void AgreeBtnClicked()
// 	{
// 		this.privacyPolicyDialogue.SetActive(false);
// 		PlayerPrefs.SetInt("PolicyCheckDB", 1);
// 	}

// 	public void ShowPrivacyClicked()
// 	{
// 		Application.OpenURL("http://monstergamesproductions.com.au/PrivacyPolicy.html");
// 	}

// 	public GameObject privacyPolicyDialogue;
// }
