// dnSpy decompiler from Assembly-CSharp.dll class: TrainCollisionScript
using System;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class TrainCollisionScript : MonoBehaviour
{
	private void OnEnable()
	{
		this.SpeedLimitUI = GameObject.Find("Canvas/PanelSpeedLimit");
		this.StationCrossUI = GameObject.Find("Canvas/PanelStationCross");
		this.TrackBusyUI = GameObject.Find("Canvas/PanelTrackBusy");
		this.TrafficControllerSCRIPT = (TrafficController)UnityEngine.Object.FindObjectOfType(typeof(TrafficController));
		this.AITrainManagerSCRIPT = (AITrainManager)UnityEngine.Object.FindObjectOfType(typeof(AITrainManager));
		base.Invoke("DelayCall", 0.5f);
	}

	private void DelayCall()
	{
		this.SpeedLimitUI.SetActive(false);
		this.StationCrossUI.SetActive(false);
		this.TrackBusyUI.SetActive(false);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "StopPoint" && this.TrainsplineController.Speed >= 0f && this.TrainsplineController.Speed <= 1f)
		{
			this.TrainStopBool = true;
			this.MissionCompleteBool = true;
		}
		if (other.tag == "StopPointMulti" && this.TrainsplineController.Speed >= 0f && this.TrainsplineController.Speed <= 1f)
		{
			this.TrainStopBoolMulti = true;
			this.TrainStopCounter = 1;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "points")
		{
			UnityEngine.Debug.Log("player col");
			this.PlayerTrainPoints++;
		}
		if (other.tag == "ShipStart")
		{
			UnityEngine.Object.Destroy(other.gameObject);
		}
		if (other.tag == "Racewin")
		{
			this.PlayerTraincounter = 1;
			UnityEngine.Object.Destroy(other.gameObject);
		}
		if (other.tag == "MissionFailed")
		{
			this.StationCrossUI.SetActive(true);
			base.Invoke("LevelFailDelay", 6.5f);
		}
		if (other.tag == "CollScript2")
		{
			this.CollisionScript2.enabled = true;
		}
		if (other.tag == "SpeedLimit")
		{
			this.SpeedLimitUI.SetActive(true);
		}
		if (other.tag == "AiTrain")
		{
		}
		if (other.tag == "AiTrainMulti")
		{
			this.AITrainManagerSCRIPT.AITrainStartCounter++;
		}
		if (other.tag == "TrackBusy")
		{
			this.TrackBusyUI.SetActive(true);
		}
		if (other.tag == "TrackChangeRight")
		{
			this.TrackChangeRightBool = true;
			this.i++;
		}
		if (other.tag == "TrackChangeLeft")
		{
			this.TrackChangeLefttBool = true;
			this.i++;
		}
		if (other.gameObject.tag == "AiTrainCol")
		{
			if (PlayerPrefs.GetString("Vibration") == "True")
			{
				Handheld.Vibrate();
			}
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.Metal_effectPrefab, this.MetalEffectPos.transform.position, this.MetalEffectPos.transform.rotation);
			UnityEngine.Object.Destroy(obj, 0.45f);
			base.GetComponent<AudioSource>().PlayOneShot(this.MetalSound, 0.5f);
			base.Invoke("DelayTrainActive", 0.5f);
		}
		if (other.gameObject.tag == "AiTrainColMulti")
		{
			if (PlayerPrefs.GetString("Vibration") == "True")
			{
				Handheld.Vibrate();
			}
			GameObject obj2 = UnityEngine.Object.Instantiate<GameObject>(this.Metal_effectPrefab, this.MetalEffectPos.transform.position, this.MetalEffectPos.transform.rotation);
			UnityEngine.Object.Destroy(obj2, 0.45f);
			base.GetComponent<AudioSource>().PlayOneShot(this.MetalSound, 0.5f);
			base.Invoke("DelayTrainActiveMulti", 0.5f);
		}
		if (other.gameObject.tag == "trafficCar")
		{
			this.TrackBusyUI.SetActive(true);
			this.TrafficControllerSCRIPT.AICarGo = true;
		}
		if (other.gameObject.tag == "trainCol")
		{
			if (PlayerPrefs.GetString("Vibration") == "True")
			{
				Handheld.Vibrate();
			}
			base.Invoke("DelayTrafficAccident", 0.2f);
		}
		if (other.tag == "PBuggy2")
		{
			this.JeepPushScript.enabled = true;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "trafficCar")
		{
			this.JeepPushScript.enabled = false;
			other.gameObject.GetComponent<Rigidbody>().AddForce(0f, 50f, 0f);
			ContactPoint contactPoint = other.contacts[0];
			Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
			Vector3 point = contactPoint.point;
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.Metal_effectPrefab, point, rotation);
			UnityEngine.Object.Destroy(obj, 0.45f);
			base.Invoke("LevelFailDelay", 3.5f);
			if (PlayerPrefs.GetString("Vibration") == "True")
			{
				Handheld.Vibrate();
			}
		}
		if (other.gameObject.tag == "Traffic")
		{
			other.gameObject.GetComponent<Rigidbody>().AddForce(0f, 50f, 0f);
			ContactPoint contactPoint2 = other.contacts[0];
			Quaternion rotation2 = Quaternion.FromToRotation(Vector3.up, contactPoint2.normal);
			Vector3 point2 = contactPoint2.point;
			GameObject obj2 = UnityEngine.Object.Instantiate<GameObject>(this.Metal_effectPrefab, point2, rotation2);
			UnityEngine.Object.Destroy(obj2, 0.45f);
			this.TrafficControllerSCRIPT.AICarStop = true;
			base.Invoke("DelayTrafficAccident", 0.2f);
			if (PlayerPrefs.GetString("Vibration") == "True")
			{
				Handheld.Vibrate();
			}
		}
		if (other.gameObject.tag == "trainCol")
		{
			UnityEngine.Debug.Log("Train coll");
			ContactPoint contactPoint3 = other.contacts[0];
			Quaternion rotation3 = Quaternion.FromToRotation(Vector3.up, contactPoint3.normal);
			Vector3 point3 = contactPoint3.point;
			GameObject obj3 = UnityEngine.Object.Instantiate<GameObject>(this.Metal_effectPrefab, point3, rotation3);
			UnityEngine.Object.Destroy(obj3, 0.45f);
			if (PlayerPrefs.GetString("Vibration") == "True")
			{
				Handheld.Vibrate();
			}
			base.Invoke("DelayTrafficAccident", 0.2f);
		}
	}

	private void DelayTrainActive()
	{
		this.DupPlayerTrainPos.SetActive(true);
		this.DupPlayerTrainPos.transform.parent = null;
		this.AITrainManagerSCRIPT.AITrainCollisonCounter++;
		this.TrainManager.SetActive(false);
		base.Invoke("LevelFailDelay", 6.5f);
	}

	private void DelayTrainActiveMulti()
	{
		this.DupPlayerTrainPos.SetActive(true);
		this.DupPlayerTrainPos.transform.parent = null;
		this.AITrainManagerSCRIPT.AITrainCollisonCounter++;
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
		base.Invoke("LevelFailDelay", 6.5f);
	}

	private void LevelFailDelay()
	{
		this.MissionFailedBool = true;
	}

	private void RightBoolFalse_Delay()
	{
		this.TrackChangeRightBool = false;
	}

	private void FixedUpdate()
	{
		if (!this.TrackChangeRightBool && base.IsInvoking("RightBoolFalse_Delay"))
		{
			base.CancelInvoke("RightBoolFalse_Delay");
		}
		if (!this.TrackChangeLefttBool && base.IsInvoking("LeftBoolFalse_Delay"))
		{
			base.CancelInvoke("LeftBoolFalse_Delay");
		}
	}

	private void LeftBoolFalse_Delay()
	{
		this.TrackChangeLefttBool = false;
	}

	private void OnDisable()
	{
		if (base.IsInvoking("LevelFailDelay"))
		{
			base.CancelInvoke("LevelFailDelay");
		}
		if (base.IsInvoking("DelayTrafficAccident"))
		{
			base.CancelInvoke("DelayTrafficAccident");
		}
		if (base.IsInvoking("DelayTrainActiveMulti"))
		{
			base.CancelInvoke("DelayTrainActiveMulti");
		}
		if (base.IsInvoking("RightBoolFalse_Delay"))
		{
			base.CancelInvoke("RightBoolFalse_Delay");
		}
		if (base.IsInvoking("LeftBoolFalse_Delay"))
		{
			base.CancelInvoke("LeftBoolFalse_Delay");
		}
	}

	public SplineController TrainsplineController;

	public bool TrainStopBool;

	public bool TrainStopBoolMulti;

	public int TrainStopCounter;

	public bool MissionFailedBool;

	public bool MissionCompleteBool;

	public GameObject SpeedLimitUI;

	public GameObject StationCrossUI;

	public GameObject TrackBusyUI;

	public GameObject TrainManager;

	public GameObject AiTrainManager;

	public GameObject PlayerTrainPos;

	public GameObject DupPlayerTrainPos;

	public GameObject Metal_effectPrefab;

	public GameObject MetalEffectPos;

	public AudioClip MetalSound;

	public bool TrackChangeRightBool;

	public bool TrackChangeLefttBool;

	public GameObject[] TrackChangeButtons;

	public int i;

	public TrafficController TrafficControllerSCRIPT;

	public AITrainManager AITrainManagerSCRIPT;

	public TrainCollisionScript2 CollisionScript2;

	public JeepPush JeepPushScript;

	public int PlayerTraincounter;

	public int PlayerTrainPoints;

	public Image RightIndicator;

	public Image LeftIndicator;
}
