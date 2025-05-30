// dnSpy decompiler from Assembly-CSharp.dll class: BridgeAnimScriptt
using System;
using UnityEngine;

public class BridgeAnimScriptt : MonoBehaviour
{
	private void OnEnable()
	{
		this.counter = 1;
		if (!this._me)
		{
			this._me = base.transform;
		}
	}

	private void FixedUpdate()
	{
		if (this.counter == 1)
		{
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target1.position, maxDistanceDelta);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target1.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this.RotAngle, 0f);
		}
		if (base.transform.position == this.Target1.position)
		{
			this.counter = 2;
			if (this.TrafficLightGreenBool)
			{
				this.BridgeCam.SetActive(false);
				this.ControlsButtons.SetActive(true);
				this.RedLights.SetActive(false);
				this.GreenLights.SetActive(true);
				this.BarrrierAnim.enabled = true;
				this.TrafficLightGreenBool = false;
				this.Trackk.SetActive(true);
			}
		}
	}

	public Transform Target1;

	public float speed;

	private int counter;

	private Transform _me;

	public float RotAngle;

	private bool TrafficLightGreenBool = true;

	public GameObject RedLights;

	public GameObject GreenLights;

	public Animator BarrrierAnim;

	public GameObject BridgeCam;

	public GameObject Trackk;

	public GameObject ControlsButtons;
}
