// dnSpy decompiler from Assembly-CSharp.dll class: LevelEnvIns
using System;
using System.Collections;
using UnityEngine;

public class LevelEnvIns : MonoBehaviour
{
	private void OnEnable()
	{
		base.StartCoroutine(this.Load());
	}

	private void Start()
	{
		base.Invoke("DelayCall", 2f);
	}

	private void DelayCall()
	{
		this.TerrianObj = UnityEngine.Object.Instantiate<GameObject>(this.TerrianObj, this.TerrianObj.transform.position, this.TerrianObj.transform.rotation);
		this.LoadingPanel.SetActive(false);
		Handheld.StopActivityIndicator();
		if (PlayerPrefs.GetInt("AchivementUnlocked") == 1)
		{
			this.AchievemntPanelMsG.SetActive(true);
			PlayerPrefs.SetInt("AchivementUnlocked", 0);
		}
	}

	private IEnumerator Load()
	{
		Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.InversedLarge);
		Handheld.StartActivityIndicator();
		yield return new WaitForSeconds(0f);
		yield break;
	}

	public GameObject TerrianObj;

	public GameObject LoadingPanel;

	public GameObject AchievemntPanelMsG;
}
