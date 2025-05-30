// dnSpy decompiler from Assembly-CSharp.dll class: GmeObjNotDesScriptt
using System;
using UnityEngine;

public class GmeObjNotDesScriptt : MonoBehaviour
{
	public static GmeObjNotDesScriptt Instance
	{
		get
		{
			if (GmeObjNotDesScriptt._instance == null)
			{
				GmeObjNotDesScriptt._instance = UnityEngine.Object.FindObjectOfType<GmeObjNotDesScriptt>();
				UnityEngine.Object.DontDestroyOnLoad(GmeObjNotDesScriptt._instance.gameObject);
			}
			return GmeObjNotDesScriptt._instance;
		}
	}

	private void Awake()
	{
		if (GmeObjNotDesScriptt._instance == null)
		{
			GmeObjNotDesScriptt._instance = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else if (this != GmeObjNotDesScriptt._instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private static GmeObjNotDesScriptt _instance;
}
