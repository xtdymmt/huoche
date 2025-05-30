// dnSpy decompiler from Assembly-CSharp.dll class: TakeScreenshot
using System;
using System.IO;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown("f9"))
		{
			string text;
			do
			{
				this.screenshotCount++;
				text = "screenshot" + this.screenshotCount + ".png";
			}
			while (File.Exists(text));
			ScreenCapture.CaptureScreenshot(text);
		}
	}

	private int screenshotCount;
}
