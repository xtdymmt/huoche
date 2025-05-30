// dnSpy decompiler from Assembly-CSharp.dll class: SocialScripttt
using System;
using UnityEngine;

public class SocialScripttt : MonoBehaviour
{
	public void FBLikeBtn_Click()
	{
		Application.OpenURL("https://www.facebook.com/monstergamesproductions");
	}

	public void WebsiteBtn_Click()
	{
		Application.OpenURL("http://monstergamesproductions.com.au");
	}

	public void InstagramBtn_Click()
	{
		Application.OpenURL("https://instagram.com/monstergamesproductions?igshid=4zrnjx5x898i");
	}

	public void TwitterBtn_Click()
	{
		Application.OpenURL("https://twitter.com/MonsterGamesPr2");
	}

	public void LinkedInBtn_Click()
	{
		Application.OpenURL("https://www.linkedin.com/in/monster-games-productions-7a2890186");
	}

	public void MoreGamesBtn_Click()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			Application.OpenURL("https://play.google.com/store/apps/developer?id=Monster+Games+Productions+PTY+LTD");
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Application.OpenURL("https://itunes.apple.com/us/developer/monster-games-productions/id1268577251?mt=8");
		}
	}

	public void RateUsBtn_Click()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.monstergamesproductions.train.driving.simulator");
	}

	public void PrivacyPolicyBtn_Click()
	{
		Application.OpenURL("http://monstergamesproductions.com.au/PrivacyPolicy.html");
	}
}
