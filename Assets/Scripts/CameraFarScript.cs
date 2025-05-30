// dnSpy decompiler from Assembly-CSharp.dll class: CameraFarScript
using System;
using UnityEngine;

public class CameraFarScript : MonoBehaviour
{
	private void Start()
	{
		if (SystemInfo.systemMemorySize > 1024)
		{
			base.gameObject.GetComponent<Camera>().farClipPlane = this.SetMaxFar;
		}
		else
		{
			base.gameObject.GetComponent<Camera>().farClipPlane = this.SetMinFar;
		}
	}

	public float SetMaxFar = 500f;

	public float SetMinFar = 300f;
}
