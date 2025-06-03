// dnSpy decompiler from Assembly-CSharp.dll class: SplashScript
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
	private void Awake()
	{
		
	}

	private void Start()
	{
		base.Invoke("DelaLoadLevelCall", 1f);
	}

	private void DelaLoadLevelCall()
	{
	
		base.StartCoroutine(this.LoadNewScene());
	}

	private IEnumerator LoadNewScene()
	{
		yield return new WaitForSeconds(0f);
		AsyncOperation async = SceneManager.LoadSceneAsync(1);
		while (!async.isDone)
		{
			yield return null;
		}
		yield break;
	}

	private IEnumerator Load()
	{
		//Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
		//Handheld.StartActivityIndicator();
		yield return new WaitForSeconds(0f);
		yield break;
	}

	
}
