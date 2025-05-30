// dnSpy decompiler from Assembly-CSharp.dll class: ShareAndr
using System;
using UnityEngine;

public class ShareAndr : MonoBehaviour
{
	public void shareText()
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent", new object[0]);
		androidJavaObject.Call<AndroidJavaObject>("setAction", new object[]
		{
			androidJavaClass.GetStatic<string>("ACTION_SEND")
		});
		androidJavaObject.Call<AndroidJavaObject>("setType", new object[]
		{
			"text/plain"
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
		{
			androidJavaClass.GetStatic<string>("EXTRA_SUBJECT"),
			this.subject
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
		{
			androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
			this.body
		});
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
		@static.Call("startActivity", new object[]
		{
			androidJavaObject
		});
	}

	private string subject = "Superhero Mega ramp Moto Rider: 3D GT Auto stunts";

	private string body = "https://play.google.com/store/apps/details?id=com.monstergamesproductions.ramp.moto.rider";
}
