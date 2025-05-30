using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MF_MusicSetting : MonoBehaviour {

	[SerializeField] private Toggle _BGMController;
	[SerializeField] private Text _volumeValue;
	[SerializeField] private Slider _volumeSlider;

	// Use this for initialization    [SerializeField]
	private void Start()
	{
		if (MF_BgmManager.instance == null)
			return;

		_BGMController.isOn = (PlayerPrefs.GetInt(MF_BgmManager.BGM_TYPE, 1) == 1);
		_volumeSlider.maxValue = 10;
		_volumeSlider.value = PlayerPrefs.GetInt(MF_BgmManager.BGM_VOLUME_TYPE, 6);

		_volumeSlider.onValueChanged.AddListener((float a)=>
		{
			BGM_Volume_Setting();
		});


        _BGMController.onValueChanged.AddListener((bool isOn) =>
        {
			if (MF_BgmManager.instance == null)
				return;
			if (_BGMController.isOn)
				MF_BgmManager.BGM_ON.Invoke();
            else
				MF_BgmManager.BGM_OFF.Invoke();
        });
        BGM_Volume_Setting();
	}

	private void BGM_Volume_Setting()
	{
		if (MF_BgmManager.instance == null)
			return;

		PlayerPrefs.SetInt(MF_BgmManager.BGM_VOLUME_TYPE, (int)_volumeSlider.value);
		if(_volumeValue!=null)
			_volumeValue.text = "音量：" + (int)_volumeSlider.value;
		MF_BgmManager.instance._aud.volume = _volumeSlider.value / 10;
	}

}
