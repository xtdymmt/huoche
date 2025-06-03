// dnSpy decompiler from Assembly-CSharp.dll class: LevelDeciderScript
using System;
using System.Collections;
using UnityEngine;

public class LevelDeciderScript : MonoBehaviour
{
	private void Awake()
	{
		base.StartCoroutine(this.Load());
		this.LevelCounter = PlayerPrefs.GetInt("LevelDB");
		this.LevelStartBool = true;
	}

	private void Start()
	{
		////Handheld.StartActivityIndicator();
	}

	private void Update()
	{
		if (this.LevelStartBool)
		{
			this.EnvironmentIns();
			this.LevelStartBool = false;
		}
	}

	private void EnvironmentIns()
	{
		UnityEngine.Debug.Log("Environemmmmmmmmmmmmmmmmmmmmt");
		this.TempLevelIns = UnityEngine.Object.Instantiate<GameObject>(this.LevelPrfab, this.LevelPrfab.transform.position, this.LevelPrfab.transform.rotation);
		base.Invoke("DelayLodingFalse", 2f);
	}

	private void DelayLodingFalse()
	{
		////Handheld.StopActivityIndicator();
		this.LoadingStuffs.SetActive(false);
	}

	private IEnumerator Load()
	{
		////Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
		////Handheld.StartActivityIndicator();
		yield return new WaitForSeconds(0f);
		yield break;
	}

	private GameObject TempLevelIns;

	private int LevelCounter;

	public GameObject LoadingStuffs;

	public GameObject LevelPrfab;

	private bool LevelStartBool = true;
}
