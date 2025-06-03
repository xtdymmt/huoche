// dnSpy decompiler from Assembly-CSharp.dll class: CameraControllerScript
using System;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
	private void Start()
	{
		CameraControllerScript.CamCounter = 0;
		base.Invoke("DelayObjCall", 1f);
		base.Invoke("SkiptBtanActive", 7f);
		base.Invoke("AnimationCam_False", 27f);
	}

	private void DelayObjCall()
	{
		this.TrainMoveScript = (TrainMove)UnityEngine.Object.FindObjectOfType(typeof(TrainMove));
		this.TrainCollisionScriptScript = (TrainCollisionScript)UnityEngine.Object.FindObjectOfType(typeof(TrainCollisionScript));
		this.MainCameraObj = this.TrainMoveScript.MainCameraObj;
		this.CabinCameraObj = this.TrainMoveScript.CabinCameraObj;
		this.CarCameraObj = this.TrainMoveScript.CarCameraObj;
		this.AnimationCameraObj = this.TrainMoveScript.AnimationCameraObj;
		this.Driver = this.TrainMoveScript.Driver;
		this.CameraExit = this.TrainMoveScript.CameraExit;
		this.MainCamera_bl_OrbitTouchPadScript.m_CameraOrbit = this.TrainMoveScript.Main_CameraOrbit;
		this.CarCamera_bl_OrbitTouchPadScript.m_CameraOrbit = this.TrainMoveScript.CAR_CameraOrbit;
		this.CabinCamera_bl_OrbitTouchPadScript.m_CameraOrbit = this.TrainMoveScript.Cabin_CameraOrbit;
		this.MainCameraObj.SetActive(false);
		this.MainCameraTouchArea.SetActive(false);
		this.StaticCameraObj.enabled = false;
		this.CabinCameraObj.SetActive(false);
		this.CabinCameraTouchArea.SetActive(false);
		this.Driver.SetActive(false);
		this.CarCameraObj.SetActive(false);
		this.CarCameraTouchArea.SetActive(false);
		this.CameraExit.SetActive(false);
		this.AnimationCameraObj.SetActive(true);
		this.InitBool = true;
	}

	private void SkiptBtanActive()
	{
		this.SkipBtn.SetActive(true);
	}

	public void AnimationCam_False()
	{
		Debug.Log("ShowSkip");
		//HuaWeiADManager.ShowSkip();
		LSC_ADManager.Instance.ShowCustom();
        base.CancelInvoke("AnimationCam_False");
		this.DoorBtnsPanel.SetActive(true);
		this.StaticCameraObj.enabled = true;
		if (PlayerPrefs.GetInt("TrainDB") == 0)
		{
			this.AnimationCameraObj.SetActive(false);
		}
		else
		{
			this.AnimationCameraObj_CargoTrain.SetActive(false);
		}
		this.SkipBtn.SetActive(false);
	}

	private void LateUpdate()
	{
		if (this.InitBool)
		{
			if (this.TrainCollisionScriptScript.MissionFailedBool)
			{
				CameraControllerScript.CamCounter = 4;
			}
			if (UnityEngine.Input.GetKey(KeyCode.J))
			{
				if (PlayerPrefs.GetInt("TrainDB") == 0)
				{
					this.MainCameraObj.SetActive(true);
					this.MainCameraTouchArea.SetActive(true);
				}
				else
				{
					this.MainCameraObjCargoTrain.SetActive(true);
					this.MainCameraTouchArea_Cargo.SetActive(true);
				}
				this.CabinCameraObj.SetActive(false);
				this.CabinCameraTouchArea.SetActive(false);
				this.CarCameraObj.SetActive(false);
				this.CarCameraTouchArea.SetActive(false);
				this.Driver.SetActive(false);
			}
			if (UnityEngine.Input.GetKey(KeyCode.K))
			{
				if (PlayerPrefs.GetInt("TrainDB") == 0)
				{
					this.MainCameraTouchArea.SetActive(false);
					this.MainCameraObj.SetActive(false);
				}
				else
				{
					this.MainCameraObjCargoTrain.SetActive(false);
					this.MainCameraTouchArea_Cargo.SetActive(false);
				}
				this.CabinCameraObj.SetActive(true);
				this.CabinCameraTouchArea.SetActive(true);
				this.Driver.SetActive(true);
				this.CarCameraObj.SetActive(false);
				this.CarCameraTouchArea.SetActive(false);
			}
			if (UnityEngine.Input.GetKey(KeyCode.L))
			{
				if (PlayerPrefs.GetInt("TrainDB") == 0)
				{
					this.MainCameraTouchArea.SetActive(false);
					this.MainCameraObj.SetActive(false);
				}
				else
				{
					this.MainCameraObjCargoTrain.SetActive(false);
					this.MainCameraTouchArea_Cargo.SetActive(false);
				}
				this.CabinCameraObj.SetActive(false);
				this.CabinCameraTouchArea.SetActive(false);
				this.Driver.SetActive(false);
				this.CarCameraObj.SetActive(true);
				this.CarCameraTouchArea.SetActive(true);
			}
			if (this.GameMasterScript.ExitCamBool)
			{
				this.CameraExit.SetActive(true);
				if (PlayerPrefs.GetInt("TrainDB") == 0)
				{
					this.MainCameraTouchArea.SetActive(false);
					this.MainCameraObj.SetActive(false);
				}
				else
				{
					this.MainCameraObjCargoTrain.SetActive(false);
					this.MainCameraTouchArea_Cargo.SetActive(false);
				}
				this.CabinCameraObj.SetActive(false);
				this.Driver.SetActive(false);
				this.CabinCameraTouchArea.SetActive(false);
				this.CarCameraObj.SetActive(false);
				this.CarCameraTouchArea.SetActive(false);
			}
			else if (PlayerPrefs.GetInt("TrainDB") == 0)
			{
				if (!this.AnimationCameraObj.activeInHierarchy)
				{
					if (CameraControllerScript.CamCounter == 0)
					{
						this.StaticCameraObj.enabled = true;
						if (PlayerPrefs.GetInt("TrainDB") == 0)
						{
							this.MainCameraTouchArea.SetActive(false);
							this.MainCameraObj.SetActive(false);
						}
						else
						{
							this.MainCameraObjCargoTrain.SetActive(false);
							this.MainCameraTouchArea_Cargo.SetActive(false);
						}
						this.CabinCameraObj.SetActive(false);
						this.CabinCameraTouchArea.SetActive(false);
						this.Driver.SetActive(false);
						this.CarCameraObj.SetActive(false);
						this.CarCameraTouchArea.SetActive(false);
					}
					else if (CameraControllerScript.CamCounter == 1)
					{
						this.StaticCameraObj.enabled = false;
						if (PlayerPrefs.GetInt("TrainDB") == 0)
						{
							this.MainCameraObj.SetActive(true);
							this.MainCameraTouchArea.SetActive(true);
						}
						else
						{
							this.MainCameraObjCargoTrain.SetActive(true);
							this.MainCameraTouchArea_Cargo.SetActive(true);
						}
						this.CabinCameraObj.SetActive(false);
						this.CabinCameraTouchArea.SetActive(false);
						this.Driver.SetActive(false);
						this.CarCameraObj.SetActive(false);
						this.CarCameraTouchArea.SetActive(false);
					}
					else if (CameraControllerScript.CamCounter == 2)
					{
						this.StaticCameraObj.enabled = false;
						if (PlayerPrefs.GetInt("TrainDB") == 0)
						{
							this.MainCameraTouchArea.SetActive(false);
							this.MainCameraObj.SetActive(false);
						}
						else
						{
							this.MainCameraObjCargoTrain.SetActive(false);
							this.MainCameraTouchArea_Cargo.SetActive(false);
						}
						this.CabinCameraObj.SetActive(true);
						this.CabinCameraTouchArea.SetActive(true);
						this.Driver.SetActive(true);
						this.CarCameraObj.SetActive(false);
						this.CarCameraTouchArea.SetActive(false);
					}
					else if (CameraControllerScript.CamCounter == 3)
					{
						this.StaticCameraObj.enabled = false;
						if (PlayerPrefs.GetInt("TrainDB") == 0)
						{
							this.MainCameraTouchArea.SetActive(false);
							this.MainCameraObj.SetActive(false);
						}
						else
						{
							this.MainCameraObjCargoTrain.SetActive(false);
							this.MainCameraTouchArea_Cargo.SetActive(false);
						}
						this.CabinCameraObj.SetActive(false);
						this.CabinCameraTouchArea.SetActive(false);
						this.Driver.SetActive(false);
						this.CarCameraObj.SetActive(true);
						this.CarCameraTouchArea.SetActive(true);
					}
					else if (CameraControllerScript.CamCounter == 4)
					{
						this.StaticCameraObj.enabled = true;
						if (PlayerPrefs.GetInt("TrainDB") == 0)
						{
							this.MainCameraTouchArea.SetActive(false);
							this.MainCameraObj.SetActive(false);
						}
						else
						{
							this.MainCameraObjCargoTrain.SetActive(false);
							this.MainCameraTouchArea_Cargo.SetActive(false);
						}
						this.CabinCameraObj.SetActive(false);
						this.CabinCameraTouchArea.SetActive(false);
						this.Driver.SetActive(false);
						this.CarCameraObj.SetActive(false);
						this.CarCameraTouchArea.SetActive(false);
					}
				}
			}
			else if (!this.AnimationCameraObj_CargoTrain.activeInHierarchy)
			{
				if (CameraControllerScript.CamCounter == 0)
				{
					this.StaticCameraObj.enabled = true;
					if (PlayerPrefs.GetInt("TrainDB") == 0)
					{
						this.MainCameraTouchArea.SetActive(false);
						this.MainCameraObj.SetActive(false);
					}
					else
					{
						this.MainCameraObjCargoTrain.SetActive(false);
						this.MainCameraTouchArea_Cargo.SetActive(false);
					}
					this.CabinCameraObj.SetActive(false);
					this.CabinCameraTouchArea.SetActive(false);
					this.Driver.SetActive(false);
					this.CarCameraObj.SetActive(false);
					this.CarCameraTouchArea.SetActive(false);
				}
				else if (CameraControllerScript.CamCounter == 1)
				{
					this.StaticCameraObj.enabled = false;
					if (PlayerPrefs.GetInt("TrainDB") == 0)
					{
						this.MainCameraObj.SetActive(true);
						this.MainCameraTouchArea.SetActive(true);
					}
					else
					{
						this.MainCameraObjCargoTrain.SetActive(true);
						this.MainCameraTouchArea_Cargo.SetActive(true);
					}
					this.CabinCameraObj.SetActive(false);
					this.CabinCameraTouchArea.SetActive(false);
					this.Driver.SetActive(false);
					this.CarCameraObj.SetActive(false);
					this.CarCameraTouchArea.SetActive(false);
				}
			}
			//UnityEngine.Debug.Log("Counter Val:" + CameraControllerScript.CamCounter);
		}
	}

	public void MainCameraBtn_Click()
	{
		if (PlayerPrefs.GetInt("TrainDB") == 0)
		{
			this.MainCameraObj.SetActive(true);
			this.MainCameraTouchArea.SetActive(true);
		}
		else
		{
			this.MainCameraObjCargoTrain.SetActive(true);
			this.MainCameraTouchArea_Cargo.SetActive(true);
		}
		this.CabinCameraObj.SetActive(false);
		this.CabinCameraTouchArea.SetActive(false);
		this.Driver.SetActive(false);
		this.CarCameraObj.SetActive(false);
		this.CarCameraTouchArea.SetActive(false);
	}

	public void CabinCameraBtn_Click()
	{
		if (PlayerPrefs.GetInt("TrainDB") == 0)
		{
			this.MainCameraTouchArea.SetActive(false);
			this.MainCameraObj.SetActive(false);
		}
		else
		{
			this.MainCameraObjCargoTrain.SetActive(false);
			this.MainCameraTouchArea_Cargo.SetActive(false);
		}
		this.CabinCameraObj.SetActive(true);
		this.CabinCameraTouchArea.SetActive(true);
		this.Driver.SetActive(true);
		this.CarCameraObj.SetActive(false);
		this.CarCameraTouchArea.SetActive(false);
	}

	public void CarCameraBtn_Click()
	{
		if (PlayerPrefs.GetInt("TrainDB") == 0)
		{
			this.MainCameraTouchArea.SetActive(false);
			this.MainCameraObj.SetActive(false);
		}
		else
		{
			this.MainCameraObjCargoTrain.SetActive(false);
			this.MainCameraTouchArea_Cargo.SetActive(false);
		}
		this.CabinCameraObj.SetActive(false);
		this.CabinCameraTouchArea.SetActive(false);
		this.Driver.SetActive(false);
		this.CarCameraObj.SetActive(true);
		this.CarCameraTouchArea.SetActive(true);
	}

	public void CameraChangeBtn_Click()
	{
		CameraControllerScript.CamCounter++;
		if (PlayerPrefs.GetInt("TrainDB") == 0)
		{
			if (CameraControllerScript.CamCounter >= 4)
			{
				CameraControllerScript.CamCounter = 0;
			}
		}
		else if (CameraControllerScript.CamCounter >= 2)
		{
			CameraControllerScript.CamCounter = 0;
		}
	}

	[Header("Orbit Touch Area Target")]
	public bl_OrbitTouchPad MainCamera_bl_OrbitTouchPadScript;

	public bl_OrbitTouchPad CarCamera_bl_OrbitTouchPadScript;

	public bl_OrbitTouchPad CabinCamera_bl_OrbitTouchPadScript;

	[Header("Passanger Train")]
	public GameObject MainCameraObj;

	public GameObject MainCameraTouchArea;

	public GameObject AnimationCameraObj;

	[Header("Cargo Train")]
	public GameObject MainCameraObjCargoTrain;

	public GameObject MainCameraTouchArea_Cargo;

	public GameObject AnimationCameraObj_CargoTrain;

	[Header("OverAll Settings")]
	public Camera StaticCameraObj;

	public GameObject CabinCameraObj;

	public GameObject CarCameraObj;

	public GameObject Driver;

	public GameObject CabinCameraTouchArea;

	public GameObject CarCameraTouchArea;

	public GameObject TutorialPanel;

	public GameObject DoorBtnsPanel;

	public GameObject RaceBtnPanel;

	public GameObject CameraBtnPanel;

	public GameMaster GameMasterScript;

	public GameObject CameraExit;

	public static int CamCounter;

	public GameObject SkipBtn;

	public TrainMove TrainMoveScript;

	private bool InitBool;

	public TrainCollisionScript TrainCollisionScriptScript;
}
