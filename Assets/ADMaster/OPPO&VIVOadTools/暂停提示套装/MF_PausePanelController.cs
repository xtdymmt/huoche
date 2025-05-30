using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MF_PausePanelController : MonoBehaviour {

	private float PauseTimeScale = 1;

	public static string PanelName = "MF_PausePanel";

    private void Reset()
    {
		if (name != PanelName)
			name = PanelName;
    }

    void OnEnable()
	{
		PauseTimeScale = Time.timeScale;
		Time.timeScale = 0;
		GetComponentInChildren<Button>().enabled = false;
		StartCoroutine(ButtonCanClick(2.0f));
	}

	private IEnumerator ButtonCanClick(float time)
	{
		yield return new WaitForSecondsRealtime(time);
		GetComponentInChildren<Button>().enabled = true;
	}

	void OnDisable()
	{
		Time.timeScale = PauseTimeScale;
	}

	// Use this for initialization
	void Start () {

	}

	void OnDestroy()
	{
		
	}
	
}
