// dnSpy decompiler from Assembly-CSharp.dll class: FullScreenMapShow
using System;
using UnityEngine;

public class FullScreenMapShow : MonoBehaviour
{
	public void MapShowBtn()
	{
		this.MapCamera.SetActive(true);
		this.UIMapCanvas.SetActive(true);
		this.DestinationPoint.SetActive(true);
		if (PlayerPrefs.GetInt("TrainDB") == 0)
		{
			this.PlayerArrow.SetActive(true);
		}
		else
		{
			this.Cargo_PlayerArrow.SetActive(true);
		}
		this.Path.SetActive(true);
		this.UIPanelsCanvas.SetActive(false);
		RenderSettings.fog = false;
		Time.timeScale = 0f;
	}

	public void MapHideBtn()
	{
		this.MapCamera.SetActive(false);
		this.UIMapCanvas.SetActive(false);
		this.DestinationPoint.SetActive(false);
		if (PlayerPrefs.GetInt("TrainDB") == 0)
		{
			this.PlayerArrow.SetActive(false);
		}
		else
		{
			this.Cargo_PlayerArrow.SetActive(false);
		}
		this.Path.SetActive(false);
		this.UIPanelsCanvas.SetActive(true);
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 20 || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 24 || UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 26)
		{
			RenderSettings.fog = true;
		}
		Time.timeScale = 1f;
	}

	public void OrignalMapBtn_Click()
	{
		this.OrignalMap.SetActive(true);
		this.RouteMap.SetActive(false);
		this.RouteMapButton.SetActive(true);
		this.OrignalMapButton.SetActive(false);
	}

	public void RouteMapBtn_Click()
	{
		this.OrignalMap.SetActive(false);
		this.RouteMap.SetActive(true);
		this.OrignalMapButton.SetActive(true);
		this.RouteMapButton.SetActive(false);
	}

	[Header("Passanger Train")]
	public GameObject PlayerArrow;

	[Header("Cargo Train")]
	public GameObject Cargo_PlayerArrow;

	[Header("OverAll Settings")]
	public GameObject UIPanelsCanvas;

	public GameObject UIMapCanvas;

	public GameObject MapCamera;

	public GameObject DestinationPoint;

	public GameObject Path;

	public GameObject OrignalMap;

	public GameObject RouteMap;

	public GameObject OrignalMapButton;

	public GameObject RouteMapButton;
}
