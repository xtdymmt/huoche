// dnSpy decompiler from Assembly-CSharp.dll class: TrainMove
using System;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.Curvy.Examples;
using UnityEngine;
using UnityEngine.UI;

public class TrainMove : MonoBehaviour
{
	private void OnEnable()
	{
		this.PassangerDecideCounter = UnityEngine.Random.Range(0, 3);
		this.BrakeOff = GameObject.Find("Canvas/PanelRaceBtnsPanel/BrakesBtn/BrakeOff");
		this.BrakeON = GameObject.Find("Canvas/PanelRaceBtnsPanel/BrakesBtn/BrakeOn");
		this.ImagFill = GameObject.Find("Canvas/PanelRaceBtnsPanel/RaceButtonPanel/BarFill").GetComponent<Image>();
		this.ScrollbarSize = GameObject.Find("Canvas/PanelRaceBtnsPanel/RaceButtonPanel/Scrollbar").GetComponent<Scrollbar>();
		for (int i = 0; i < this.splineController.Length; i++)
		{
			this.splineController[i].Speed = 0f;
			this.TrainManagerScript.Speed = this.splineController[i].Speed;
		}
		if (this.PassangerDecideCounter == 0)
		{
			this.PassangerEnter = this.PassangerEnter_Group1;
			this.PassangerExit = this.PassangerExit_Group1;
			this.PassangerEnter0 = this.PassangerEnter0_Group1;
			this.StaticPassanger0 = this.StaticPassanger0_Group1;
			this.PassangerEnterAgian0 = this.PassangerEnterAgian0_Group1;
			this.PassangerExit0 = this.PassangerExit0_Group1;
			this.Passanger_Group1.SetActive(true);
			this.Passanger_Group2.SetActive(false);
			this.Passanger_Group3.SetActive(false);
			this.Passanger_Group4.SetActive(false);
		}
		else if (this.PassangerDecideCounter == 1)
		{
			this.PassangerEnter = this.PassangerEnter_Group2;
			this.PassangerExit = this.PassangerExit_Group2;
			this.PassangerEnter0 = this.PassangerEnter0_Group2;
			this.StaticPassanger0 = this.StaticPassanger0_Group2;
			this.PassangerEnterAgian0 = this.PassangerEnterAgian0_Group2;
			this.PassangerExit0 = this.PassangerExit0_Group2;
			this.Passanger_Group1.SetActive(false);
			this.Passanger_Group2.SetActive(true);
			this.Passanger_Group3.SetActive(false);
			this.Passanger_Group4.SetActive(false);
		}
		else if (this.PassangerDecideCounter == 2)
		{
			this.PassangerEnter = this.PassangerEnter_Group3;
			this.PassangerExit = this.PassangerExit_Group3;
			this.PassangerEnter0 = this.PassangerEnter0_Group3;
			this.StaticPassanger0 = this.StaticPassanger0_Group3;
			this.PassangerEnterAgian0 = this.PassangerEnterAgian0_Group3;
			this.PassangerExit0 = this.PassangerExit0_Group3;
			this.Passanger_Group1.SetActive(false);
			this.Passanger_Group2.SetActive(false);
			this.Passanger_Group3.SetActive(true);
			this.Passanger_Group4.SetActive(false);
		}
		else if (this.PassangerDecideCounter == 3)
		{
			this.PassangerEnter = this.PassangerEnter_Group4;
			this.PassangerExit = this.PassangerExit_Group4;
			this.PassangerEnter0 = this.PassangerEnter0_Group4;
			this.StaticPassanger0 = this.StaticPassanger0_Group4;
			this.PassangerEnterAgian0 = this.PassangerEnterAgian0_Group4;
			this.PassangerExit0 = this.PassangerExit0_Group4;
			this.Passanger_Group1.SetActive(false);
			this.Passanger_Group2.SetActive(false);
			this.Passanger_Group3.SetActive(false);
			this.Passanger_Group4.SetActive(true);
		}
	}

	private void FixedUpdate()
	{
		if (this.TrainCollisionScriptScript.TrainStopBoolMulti)
		{
			for (int i = 0; i < this.splineController.Length; i++)
			{
				this.splineController[i].Speed = 0f;
			}
			this.TrainIdleSoundBool = true;
			this.TrainSound.enabled = false;
			if (this.TrainIdleSoundBool)
			{
				this.TrainIdleSound.SetActive(true);
			}
			this.ApplyBrakesBool = true;
			this.TrainBrakeSound.SetActive(false);
		}
		else if (this.TrainCollisionScriptScript.TrainStopBool)
		{
			for (int j = 0; j < this.splineController.Length; j++)
			{
				this.splineController[j].Speed = 0f;
			}
			this.TrainIdleSoundBool = true;
			this.TrainSound.enabled = false;
			if (this.TrainIdleSoundBool)
			{
				this.TrainIdleSound.SetActive(true);
			}
			this.ApplyBrakesBool = true;
			this.TrainBrakeSound.SetActive(false);
		}
		else
		{
			for (int k = 0; k < this.splineController.Length; k++)
			{
				this.TrainManagerScript.Speed = this.splineController[k].Speed;
				if (this.AiRaceTrainScript != null)
				{
					this.AiRaceTrainScript.Speed = this.splineController[k].Speed;
				}
				if (this.splineController[k].Speed > 0.2f)
				{
					if (this.RaceTrainGo)
					{
						AiRaceTrain.counter = 1;
						this.RaceTrainGo = false;
					}
					this.TrainSound.enabled = true;
					this.TrainIdleSound.SetActive(false);
					this.pitch = this.splineController[k].Speed / 20f;
					base.GetComponent<AudioSource>().pitch = this.pitch;
				}
				else if (this.splineController[k].Speed <= 0.2f)
				{
					this.TrainSound.enabled = false;
					this.TrainIdleSoundBool = true;
					if (this.TrainIdleSoundBool)
					{
						this.TrainIdleSound.SetActive(true);
					}
				}
				if (!this.LimitCrossBool && !this.TrainCollisionScriptScript.TrainStopBool && !this.TrainCollisionScriptScript.TrainStopBoolMulti)
				{
					if (this.ApplyBrakesBool)
					{
						if (this.splineController[k].Speed > 0f)
						{
							this.splineController[k].Speed -= Time.deltaTime * this.BrakeSpeedAmount;
							this.TrainBrakeSound.SetActive(true);
						}
						else
						{
							this.splineController[k].Speed = 0f;
							this.TrainBrakeSound.SetActive(false);
						}
					}
					else
					{
						this.TrainBrakeSound.SetActive(false);
						if (this.ScrollbarSize.value > 0f)
						{
							this.TrainIdleSoundBool = true;
						}
						if (this.ScrollbarSize.value <= 0f)
						{
							this.ImagFill.fillAmount = 0f;
							if (this.splineController[k].Speed > 0f)
							{
								this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
							}
							else
							{
								this.splineController[k].Speed = 0f;
							}
						}
						else if (this.ScrollbarSize.value >= 0f && (double)this.ScrollbarSize.value <= 0.2)
						{
							this.ImagFill.fillAmount = 0.2f;
							if (this.splineController[k].Speed >= 0f && this.splineController[k].Speed <= 4f)
							{
								this.splineController[k].Speed += Time.deltaTime / this.speedDelay;
							}
							else if (this.splineController[k].Speed >= 4f && this.splineController[k].Speed <= 8f)
							{
								this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
							}
							else if (this.splineController[k].Speed >= 4f)
							{
								this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
							}
							else
							{
								this.splineController[k].Speed += Time.deltaTime / this.speedDelay;
							}
						}
						else if ((double)this.ScrollbarSize.value >= 0.2 && (double)this.ScrollbarSize.value <= 0.4)
						{
							this.ImagFill.fillAmount = 0.4f;
							if (this.splineController[k].Speed >= 4f && this.splineController[k].Speed <= 8f)
							{
								this.splineController[k].Speed += Time.deltaTime / this.speedDelay;
							}
							else if (this.splineController[k].Speed >= 8f && this.splineController[k].Speed <= 12f)
							{
								this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
							}
							else if (this.splineController[k].Speed >= 8f)
							{
								this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
							}
							else
							{
								this.splineController[k].Speed += Time.deltaTime / this.speedDelay;
							}
						}
						else if ((double)this.ScrollbarSize.value >= 0.4 && (double)this.ScrollbarSize.value <= 0.6)
						{
							this.ImagFill.fillAmount = 0.6f;
							if (this.splineController[k].Speed >= 8f && this.splineController[k].Speed <= 12f)
							{
								this.splineController[k].Speed += Time.deltaTime / this.speedDelay;
							}
							else if (this.splineController[k].Speed >= 12f && this.splineController[k].Speed <= 16f)
							{
								this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
							}
							else if (this.splineController[k].Speed >= 12f)
							{
								this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
							}
							else
							{
								this.splineController[k].Speed += Time.deltaTime / this.speedDelay;
							}
						}
						else if ((double)this.ScrollbarSize.value >= 0.6 && (double)this.ScrollbarSize.value <= 0.8)
						{
							this.ImagFill.fillAmount = 0.8f;
							if (this.splineController[k].Speed >= 12f && this.splineController[k].Speed <= 16f)
							{
								this.splineController[k].Speed += Time.deltaTime / this.speedDelay;
							}
							else if (this.splineController[k].Speed >= 16f && this.splineController[k].Speed <= 20f)
							{
								this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
							}
							else if (this.splineController[k].Speed >= 16f)
							{
								this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
							}
							else
							{
								this.splineController[k].Speed += Time.deltaTime / this.speedDelay;
							}
						}
						else if ((double)this.ScrollbarSize.value >= 0.8 && (double)this.ScrollbarSize.value <= 1.0)
						{
							this.ImagFill.fillAmount = 1f;
							if (this.splineController[k].Speed != 20f)
							{
								if (this.splineController[k].Speed >= 16f && this.splineController[k].Speed <= 20f)
								{
									this.splineController[k].Speed += Time.deltaTime / this.speedDelay;
								}
								else if (this.splineController[k].Speed >= 20f && this.splineController[k].Speed <= 22f)
								{
									this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
								}
								else if (this.splineController[k].Speed >= 20f)
								{
									this.splineController[k].Speed -= Time.deltaTime / this.speedDelay;
								}
								else
								{
									this.splineController[k].Speed += Time.deltaTime / this.speedDelay;
								}
							}
						}
					}
				}
				if (this.splineController[k].Speed > 0.5f && !this.ApplyBrakesBool)
				{
					for (int l = 0; l < this.Wheels.Length; l++)
					{
						this.Wheels[l].gameObject.transform.Rotate(Time.deltaTime * 500f, 0f, 0f);
					}
				}
				else
				{
					for (int m = 0; m < this.Wheels.Length; m++)
					{
						this.Wheels[m].gameObject.transform.Rotate(0f, 0f, 0f);
					}
				}
			}
		}
	}

	public void BrakesApply_Click()
	{
		this.ApplyBrakesBool = true;
		this.BrakeOff.SetActive(false);
		this.BrakeON.SetActive(true);
	}

	public void BrakesApplyOFF_Click()
	{
		this.ApplyBrakesBool = false;
		this.BrakeOff.SetActive(true);
		this.BrakeON.SetActive(false);
	}

	public void Horn_Click()
	{
		this.TrainHornSound.SetActive(true);
	}

	public void Horn_UnClick()
	{
		this.TrainHornSound.SetActive(false);
	}

	public void DoorOpenCall()
	{
		this.TrainEngineAnim.SetBool("DoorOpen", true);
		this.TrainEngineAnim.SetBool("DoorClosed", false);
		this.TrainBuggy1Anim.SetBool("DoorOpen", true);
		this.TrainBuggy1Anim.SetBool("DoorClosed", false);
		this.TrainBuggy2Anim.SetBool("DoorOpen", true);
		this.TrainBuggy2Anim.SetBool("DoorClosed", false);
		this.TrainBuggy3Anim.SetBool("DoorOpen", true);
		this.TrainBuggy3Anim.SetBool("DoorClosed", false);
		this.TrainBuggy4Anim.SetBool("DoorOpen", true);
		this.TrainBuggy4Anim.SetBool("DoorClosed", false);
	}

	public void DoorCloseCall()
	{
		this.TrainEngineAnim.SetBool("DoorOpen", false);
		this.TrainEngineAnim.SetBool("DoorClosed", true);
		this.TrainBuggy1Anim.SetBool("DoorOpen", false);
		this.TrainBuggy1Anim.SetBool("DoorClosed", true);
		this.TrainBuggy2Anim.SetBool("DoorOpen", false);
		this.TrainBuggy2Anim.SetBool("DoorClosed", true);
		this.TrainBuggy3Anim.SetBool("DoorOpen", false);
		this.TrainBuggy3Anim.SetBool("DoorClosed", true);
		this.TrainBuggy4Anim.SetBool("DoorOpen", false);
		this.TrainBuggy4Anim.SetBool("DoorClosed", true);
	}

	public SplineController[] splineController;

	public TrainManager TrainManagerScript;

	public GameObject[] Wheels;

	public Scrollbar ScrollbarSize;

	public Image ImagFill;

	private float speedDelay = 2f;

	private float BrakeSpeedAmount = 3f;

	public bool ApplyBrakesBool;

	private float pitch;

	public AudioSource TrainSound;

	public GameObject BrakeOff;

	public GameObject BrakeON;

	public GameObject TrainIdleSound;

	public bool TrainIdleSoundBool;

	public GameObject TrainBrakeSound;

	public GameObject TrainHornSound;

	private bool RaceTrainGo = true;

	public AiRaceTrain AiRaceTrainScript;

	private bool LimitCrossBool;

	public TrainCollisionScript TrainCollisionScriptScript;

	public Transform TargetObj;

	[Header("Animations Animators")]
	public Animator TrainEngineAnim;

	public Animator TrainBuggy1Anim;

	public Animator TrainBuggy2Anim;

	public Animator TrainBuggy3Anim;

	public Animator TrainBuggy4Anim;

	[Header("Train Camera")]
	public GameObject MainCameraObj;

	public GameObject CabinCameraObj;

	public GameObject AnimationCameraObj;

	public GameObject ExitCamera0;

	public GameObject CarCameraObj;

	public GameObject CameraExit;

	public GameObject Driver;

	[Header("Camera Touch Target")]
	public bl_CameraOrbit Main_CameraOrbit;

	public bl_CameraOrbit CAR_CameraOrbit;

	public bl_CameraOrbit Cabin_CameraOrbit;

	[Header("Passangers autamatic Assign")]
	public GameObject PassangerEnter;

	public GameObject PassangerExit;

	public GameObject PassangerEnter0;

	public GameObject StaticPassanger0;

	public GameObject PassangerEnterAgian0;

	public GameObject PassangerExit0;

	private int PassangerDecideCounter;

	[Header("Passanger Groups 1")]
	public GameObject PassangerEnter_Group1;

	public GameObject PassangerExit_Group1;

	public GameObject PassangerEnter0_Group1;

	public GameObject StaticPassanger0_Group1;

	public GameObject PassangerEnterAgian0_Group1;

	public GameObject PassangerExit0_Group1;

	[Header("Passanger Groups 2")]
	public GameObject PassangerEnter_Group2;

	public GameObject PassangerExit_Group2;

	public GameObject PassangerEnter0_Group2;

	public GameObject StaticPassanger0_Group2;

	public GameObject PassangerEnterAgian0_Group2;

	public GameObject PassangerExit0_Group2;

	[Header("Passanger Groups 3")]
	public GameObject PassangerEnter_Group3;

	public GameObject PassangerExit_Group3;

	public GameObject PassangerEnter0_Group3;

	public GameObject StaticPassanger0_Group3;

	public GameObject PassangerEnterAgian0_Group3;

	public GameObject PassangerExit0_Group3;

	[Header("Passanger Groups 4")]
	public GameObject PassangerEnter_Group4;

	public GameObject PassangerExit_Group4;

	public GameObject PassangerEnter0_Group4;

	public GameObject StaticPassanger0_Group4;

	public GameObject PassangerEnterAgian0_Group4;

	public GameObject PassangerExit0_Group4;

	[Header("Passanger Group Assign")]
	public GameObject Passanger_Group1;

	public GameObject Passanger_Group2;

	public GameObject Passanger_Group3;

	public GameObject Passanger_Group4;
}
