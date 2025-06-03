// dnSpy decompiler from Assembly-CSharp.dll class: TrainCollisionScript2
using System;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

public class TrainCollisionScript2 : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		UnityEngine.Debug.Log("i Val:" + this.i);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "StopPoint" && this.TrainsplineController.Speed == 0f)
		{
			this.TrainStopBool = true;
			this.MissionCompleteBool = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "MissionFailed")
		{
			this.StationCrossUI.SetActive(true);
			base.Invoke("LevelFailDelay", 6.5f);
		}
		if (other.tag == "SpeedLimit")
		{
			this.SpeedLimitUI.SetActive(true);
		}
		if (other.tag == "AiTrain")
		{
			this.TrainAiControllerzzScript.enabled = true;
			this.TrainAiControllerzzScript.AiTrainStartBool = true;
		}
		if (other.tag == "TrackBusy")
		{
			this.TrackBusyUI.SetActive(true);
		}
		if (other.tag == "TrackChange")
		{
			this.TrackChangeRightButton.SetActive(true);
		}
		if (other.tag == "TrackChnage1")
		{
			UnityEngine.Debug.Log("Yess");
			this.TrackChangeButtons[this.i].SetActive(true);
			this.i++;
		}
		if (other.gameObject.tag == "AiTrainCol")
		{
			if (PlayerPrefs.GetString("Vibration") == "True")
			{
				//Handheld.Vibrate();
			}
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.Metal_effectPrefab, this.MetalEffectPos.transform.position, this.MetalEffectPos.transform.rotation);
			UnityEngine.Object.Destroy(obj, 0.45f);
			base.GetComponent<AudioSource>().PlayOneShot(this.MetalSound, 0.5f);
			base.Invoke("DelayTrainActive", 0.5f);
		}
		if (other.gameObject.tag == "trafficCar")
		{
			this.TrackBusyUI.SetActive(true);
			for (int i = 0; i < this.AICarScriptArray.Length; i++)
			{
				this.AICarScriptArray[i].enabled = true;
			}
		}
		if (other.gameObject.tag == "Deer")
		{
			for (int j = 0; j < this.DeerRunArray.Length; j++)
			{
				this.DeerRunArray[j].enabled = true;
			}
		}
	}

	private void DelayTrainActive()
	{
		this.DupPlayerTrainPos.SetActive(true);
		this.DupAITrainPos.SetActive(true);
		this.DupPlayerTrainPos.transform.parent = null;
		this.DupAITrainPos.transform.parent = null;
		this.AiTrainManager.SetActive(false);
		this.TrainManager.SetActive(false);
		base.Invoke("LevelFailDelay", 6.5f);
	}

	private void DelayTrafficAccident()
	{
		GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.Metal_effectPrefab, this.MetalEffectPos.transform.position, this.MetalEffectPos.transform.rotation);
		UnityEngine.Object.Destroy(obj, 0.45f);
		base.GetComponent<AudioSource>().PlayOneShot(this.MetalSound, 0.5f);
		this.DupPlayerTrainPos.SetActive(true);
		this.DupPlayerTrainPos.transform.parent = null;
		this.TrainManager.SetActive(false);
		if (PlayerPrefs.GetString("Vibration") == "True")
		{
			//Handheld.Vibrate();
		}
		base.Invoke("LevelFailDelay", 6.5f);
	}

	private void LevelFailDelay()
	{
		this.MissionFailedBool = true;
	}

	public SplineController TrainsplineController;

	public bool TrainStopBool;

	public bool MissionFailedBool;

	public bool MissionCompleteBool;

	public GameObject SpeedLimitUI;

	public GameObject SpeedCrossUI;

	public GameObject StationCrossUI;

	public GameObject TrackBusyUI;

	public GameObject TrainManager;

	public GameObject AiTrainManager;

	public GameObject PlayerTrainPos;

	public GameObject DupPlayerTrainPos;

	public GameObject DupAITrainPos;

	public GameObject Metal_effectPrefab;

	public GameObject MetalEffectPos;

	public AudioClip MetalSound;

	public GameObject TrackChangeRightButton;

	public GameObject[] TrackChangeButtons;

	private int i;

	public AICarScript[] AICarScriptArray;

	public DeerRun[] DeerRunArray;

	public TrainAiControllerzz TrainAiControllerzzScript;
}
