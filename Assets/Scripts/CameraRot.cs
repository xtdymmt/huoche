// dnSpy decompiler from Assembly-CSharp.dll class: CameraRot
using System;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
	private void Start()
	{
		this.xSpeed = 65f;
		this.yspeed = 10f;
		this.yAxis = 1f;
	}

	private void Update()
	{
		if (this.AutoRotate)
		{
			this.xAxis += 7.5f * Time.deltaTime;
			this.CarCamObj.rotation = Quaternion.Euler(0f, this.xAxis, 0f);
		}
		if (this.clicked)
		{
			this.xAxis += UnityEngine.Input.GetAxis("Mouse X") * this.xSpeed * Time.deltaTime;
			this.yAxis -= UnityEngine.Input.GetAxis("Mouse Y") * this.yspeed * Time.deltaTime;
			this.CarCamObj.rotation = Quaternion.Euler(0f, this.xAxis, 0f);
			this.yAxis = Mathf.Clamp(this.yAxis, 0f, 2f);
			this.SmoothFollowCamera.GetComponent<SmoothFollowMenu>().height = this.yAxis;
		}
		else
		{
			this.xAxis = this.CarCamObj.eulerAngles.y;
		}
	}

	public void Click1()
	{
		this.clicked = true;
		this.AutoRotate = false;
	}

	public void click2()
	{
		this.clicked = false;
		this.AutoRotate = true;
	}

	private bool clicked;

	private float xAxis;

	private float yAxis;

	public SmoothFollowMenu SmoothFollowCamera;

	public Transform CarCamObj;

	private float xSpeed;

	private float yspeed;

	private bool AutoRotate = true;
}
