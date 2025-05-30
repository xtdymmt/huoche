// dnSpy decompiler from Assembly-CSharp.dll class: bl_CameraBase
using System;
using UnityEngine;

public class bl_CameraBase : MonoBehaviour
{
	public Transform Transform
	{
		get
		{
			if (this.m_Transform == null)
			{
				this.m_Transform = base.GetComponent<Transform>();
			}
			return this.m_Transform;
		}
	}

	public float MouseScrollWheel
	{
		get
		{
			return UnityEngine.Input.GetAxis("Mouse ScrollWheel");
		}
	}

	public float AxisY
	{
		get
		{
			return UnityEngine.Input.GetAxis("Mouse Y");
		}
	}

	public float AxisX
	{
		get
		{
			return UnityEngine.Input.GetAxis("Mouse X");
		}
	}

	public float KeyAxisY
	{
		get
		{
			return UnityEngine.Input.GetAxis("Vertical");
		}
	}

	public float KeyAxisX
	{
		get
		{
			return UnityEngine.Input.GetAxis("Horizontal");
		}
	}

	public Camera GetCamera
	{
		get
		{
			if (this.m_Camera == null)
			{
				this.m_Camera = base.GetComponent<Camera>();
			}
			if (this.m_Camera == null)
			{
				this.m_Camera = base.GetComponentInChildren<Camera>();
			}
			return this.m_Camera;
		}
	}

	private Transform m_Transform;

	private Camera m_Camera;
}
