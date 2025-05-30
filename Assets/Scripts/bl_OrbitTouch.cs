// dnSpy decompiler from Assembly-CSharp.dll class: bl_OrbitTouch
using System;
using UnityEngine;

public class bl_OrbitTouch : MonoBehaviour
{
	private void Awake()
	{
		this.direction = Vector2.zero;
		this.touched = false;
		if (SystemInfo.systemMemorySize < 2000)
		{
			this.MovementMultiplier = new Vector2(4f, 4f);
		}
		else
		{
			this.MovementMultiplier = new Vector2(2f, 2f);
		}
	}

	private void Update()
	{
		this.ControlInput();
	}

	private void ControlInput()
	{
		for (int i = 0; i < UnityEngine.Input.touchCount; i++)
		{
			Touch data = Input.touches[i];
			if (data.phase == TouchPhase.Began)
			{
				if (UnityEngine.Input.GetTouch(i).position.x < (float)Screen.width * 0.6f && UnityEngine.Input.GetTouch(i).position.x > (float)Screen.width * 0.2f && UnityEngine.Input.GetTouch(i).position.y < (float)Screen.height * 0.5f && UnityEngine.Input.GetTouch(i).position.y > (float)Screen.height * 0.2f)
				{
					this.OnPointerDown(data);
				}
			}
			else if (data.phase == TouchPhase.Moved)
			{
				if (UnityEngine.Input.GetTouch(i).position.x < (float)Screen.width * 0.6f && UnityEngine.Input.GetTouch(i).position.x > (float)Screen.width * 0.2f && UnityEngine.Input.GetTouch(i).position.y < (float)Screen.height * 0.5f && UnityEngine.Input.GetTouch(i).position.y > (float)Screen.height * 0.2f)
				{
					this.OnDrag(data);
				}
			}
			else if (data.phase == TouchPhase.Ended && UnityEngine.Input.GetTouch(i).position.x < (float)Screen.width * 0.6f && UnityEngine.Input.GetTouch(i).position.x > (float)Screen.width * 0.2f && UnityEngine.Input.GetTouch(i).position.y < (float)Screen.height * 0.5f && UnityEngine.Input.GetTouch(i).position.y > (float)Screen.height * 0.2f)
			{
				this.OnPointerUp(data);
			}
		}
	}

	public void OnPointerDown(Touch data)
	{
		if (this.m_CameraOrbit == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a camera orbit target");
			return;
		}
		if (!this.touched)
		{
			this.touched = true;
			this.pointerID = data.fingerId;
		}
	}

	public void OnDrag(Touch data)
	{
		if (this.m_CameraOrbit == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a camera orbit target");
			return;
		}
		this.PinchZoom();
		if (this.Pinched)
		{
			return;
		}
		if (data.fingerId == this.pointerID)
		{
			this.direction = data.deltaPosition.normalized;
			this.m_CameraOrbit.Horizontal = this.direction.x * this.MovementMultiplier.x;
			this.m_CameraOrbit.Vertical = -this.direction.y * this.MovementMultiplier.y;
		}
	}

	private void ReanudeControl()
	{
		this.m_CameraOrbit.Interact = true;
		this.m_CameraOrbit.CanRotate = true;
		this.Pinched = false;
	}

	private void PinchZoom()
	{
		if (CameraControllerScript.CamCounter == 1 && UnityEngine.Input.touchCount == 2 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved && UnityEngine.Input.GetTouch(1).phase == TouchPhase.Moved)
		{
			Touch touch = UnityEngine.Input.GetTouch(0);
			Touch touch2 = UnityEngine.Input.GetTouch(1);
			Vector2 a = touch.position - touch.deltaPosition;
			Vector2 b = touch2.position - touch2.deltaPosition;
			float magnitude = (a - b).magnitude;
			float magnitude2 = (touch.position - touch2.position).magnitude;
			float num = magnitude - magnitude2;
			this.m_CameraOrbit.SetStaticZoom(num * (this.m_PinchZoomSpeed / 3.14159274f));
			if (this.CancelRotateOnPinch)
			{
				base.CancelInvoke("ReanudeControl");
				this.m_CameraOrbit.Interact = false;
				this.m_CameraOrbit.CanRotate = false;
				base.Invoke("ReanudeControl", 0.2f);
				this.Pinched = true;
			}
		}
	}

	public void OnPointerUp(Touch data)
	{
		if (this.m_CameraOrbit == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a camera orbit target");
			return;
		}
		if (data.fingerId == this.pointerID)
		{
			this.direction = Vector2.zero;
			this.touched = false;
		}
	}

	[Header("Movement")]
	[SerializeField]
	private bl_CameraOrbit m_CameraOrbit;

	[SerializeField]
	private Vector2 MovementMultiplier = new Vector2(1f, 1f);

	[Header("Pinch Zoom")]
	public bool CancelRotateOnPinch = true;

	[SerializeField]
	[Range(0.01f, 2f)]
	private float m_PinchZoomSpeed = 0.5f;

	private Vector2 direction;

	private Vector2 smoothDirection;

	private bool touched;

	private int pointerID;

	private bool Pinched;

	public Texture fish;
}
