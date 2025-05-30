// dnSpy decompiler from Assembly-CSharp.dll class: CameraPinchZoom
using System;
using UnityEngine;

public class CameraPinchZoom : MonoBehaviour
{
	private void Start()
	{
		this.MapCamera = base.GetComponent<Camera>();
	}

	private void Update()
	{
		if (UnityEngine.Input.touchCount == 2)
		{
			Touch touch = UnityEngine.Input.GetTouch(0);
			Touch touch2 = UnityEngine.Input.GetTouch(1);
			Vector2 a = touch.position - touch.deltaPosition;
			Vector2 b = touch2.position - touch2.deltaPosition;
			float magnitude = (a - b).magnitude;
			float magnitude2 = (touch.position - touch2.position).magnitude;
			float num = magnitude - magnitude2;
			if (this.MapCamera.orthographic)
			{
				this.MapCamera.orthographicSize += num * this.orthoZoomSpeed;
				this.MapCamera.orthographicSize = Mathf.Max(this.MapCamera.orthographicSize, 0.1f);
			}
			else
			{
				this.MapCamera.fieldOfView += num * this.perspectiveZoomSpeed;
				this.MapCamera.fieldOfView = Mathf.Clamp(this.MapCamera.fieldOfView, 0.1f, 179.9f);
			}
		}
		else if (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Vector2 deltaPosition = UnityEngine.Input.GetTouch(0).deltaPosition;
			base.transform.Translate(-deltaPosition.x * this.Movingspeed, -deltaPosition.y * this.Movingspeed, 0f);
		}
	}

	public float perspectiveZoomSpeed = 0.5f;

	public float orthoZoomSpeed = 0.5f;

	private Camera MapCamera;

	public float Movingspeed = 0.5f;
}
