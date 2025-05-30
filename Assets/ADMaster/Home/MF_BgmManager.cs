using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MF_BgmManager : MonoBehaviour {

	public static MF_BgmManager instance;
	public AudioSource _aud;

	public AudioClip _backGroundMusic;
	public static string BGM_TYPE = "BGM_TYPE";
	public static string BGM_VOLUME_TYPE = "BGM_VOLUME_TYPE";

	public delegate void BGM_Event();

	public static BGM_Event BGM_ON;
	public static BGM_Event BGM_OFF;

	public void Reset()
	{
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;
		transform.localRotation = Quaternion.identity;
		if (name != "MF_BGM Manager")
			name = "MF_BGM Manager";
		if (!GetComponent<AudioSource>())
			this.gameObject.AddComponent<AudioSource>();

		_aud = GetComponent<AudioSource>();
		_aud.loop = true;
		_aud.ignoreListenerPause = true;
	}

	public void BGM_Play()
	{
		print("BGM 开启");
		PlayerPrefs.SetInt(BGM_TYPE, 1);
		_aud.Play();
	}

	public void BGM_Stop()
	{
		print("BGM 关闭");
		PlayerPrefs.SetInt(BGM_TYPE, 0);
		_aud.Pause();
	}

	void OnDestroy()
	{
        BGM_ON -= BGM_Play;
        BGM_OFF -= BGM_Stop;
    }

	void Awake()
	{
		_aud = GetComponent<AudioSource>();

		BGM_ON += BGM_Play;
		BGM_OFF += BGM_Stop;

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {

		if (_backGroundMusic != null)
			_aud.clip = _backGroundMusic;


		if (PlayerPrefs.GetInt(BGM_TYPE, 1) == 1)
		{
			BGM_ON.Invoke();
		}
		else
		{
			BGM_OFF.Invoke();
		}
		//_aud.Play();
		//_aud.Stop();

	}

}
