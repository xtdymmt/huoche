// dnSpy decompiler from Assembly-CSharp.dll class: RemoveAdsGamePlyScript
using System;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAdsGamePlyScript : MonoBehaviour
{
	private void OnEnable()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			if (PlayerPrefs.GetInt("NoAdsPurchase") == 0)
			{
				//this.INAPPScript = (INAPP)UnityEngine.Object.FindObjectOfType(typeof(INAPP));
				this.RemoveAdsBtn = base.gameObject.GetComponent<Button>();
				this.RemoveAdsBtn.onClick.AddListener(delegate()
				{
					//this.INAPPScript.BuyNoAdsProduct_Click();
				});
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	private void FixedUpdate()
	{
		if (PlayerPrefs.GetInt("NoAdsPurchase") == 0)
		{
			base.gameObject.SetActive(true);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}



	private Button RemoveAdsBtn;
}
