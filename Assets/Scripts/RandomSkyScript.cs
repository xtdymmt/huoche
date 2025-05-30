// dnSpy decompiler from Assembly-CSharp.dll class: RandomSkyScript
using System;
using UnityEngine;

public class RandomSkyScript : MonoBehaviour
{
	private void Start()
	{
		this.counter = UnityEngine.Random.Range(0, 3);
		if (this.counter == 0)
		{
			RenderSettings.skybox = this.SkyboxObj1;
		}
		else if (this.counter == 1)
		{
			RenderSettings.skybox = this.SkyboxObj1;
		}
		else if (this.counter == 2)
		{
			RenderSettings.skybox = this.SkyboxObj2;
		}
		else if (this.counter == 3)
		{
			RenderSettings.skybox = this.SkyboxObj3;
		}
	}

	private void Update()
	{
		if (this.counter == 3)
		{
			RenderSettings.skybox.SetFloat("_Rotation", Time.time * 2f);
		}
	}

	public Material SkyboxObj1;

	public Material SkyboxObj2;

	public Material SkyboxObj3;

	public int counter;
}
