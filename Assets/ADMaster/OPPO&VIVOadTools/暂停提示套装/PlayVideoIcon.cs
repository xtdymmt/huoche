using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 脚本说明：
///		此为播放图标管理器，而非播放图标本身。
///		播放图标管理器的职能是通过委托来控制其子对象播放图标的显示和隐藏，而非控制自身，
///	所以播放图标需要挂在在其子对象上，而非这个脚本所挂载的物体。
///	
///		想要显示控制Icon的时候，执行PlayVideoIcon.ShowVideoIcon(true/false);方法
/// </summary>

public class PlayVideoIcon : MonoBehaviour {

	public delegate void PlayerVideoIconEvent(bool type);

	public static PlayerVideoIconEvent ShowPlayerVideoIcon;
	//public static PlayerVideoIconEvent HidePlayerVideoIcon;

	public GameObject _chi;

	public static void ShowVideoIcon(bool type)
	{
		if (ShowPlayerVideoIcon != null)
			ShowPlayerVideoIcon(type);
	}

	private void Reset()
    {
		if (name != "MF_PlayVideoIconManager")
			name = "MF_PlayVideoIconManager";
		_chi = transform.GetChild(0).gameObject;
	}

	private void OnEnable()
	{
		ShowPlayerVideoIcon += _chi.SetActive;
	}

    private void OnDisable()
    {
		ShowPlayerVideoIcon -= _chi.SetActive;
	}

    private void OnLevelWasLoaded(int level)
    {

	}

    private void Awake()
	{

	}

	// Use this for initialization
	void Start () {
		//print("加载VideoIcon控制");
		_chi.SetActive(false);
	}

    private void OnDestroy()
    {

	}

	// Update is called once per frame
	void Update () {
		
	}
}
