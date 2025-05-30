// dnSpy decompiler from Assembly-CSharp.dll class: bl_CameraOrbit
using System;
using System.Collections;
using UnityEngine;

public class bl_CameraOrbit : bl_CameraBase
{
	private void Start()
	{
		this.SetUp();
	}

	private void SetUp()
	{
		if (this.AutoTakeInfo)
		{
			this.distance = Vector3.Distance(base.transform.position, this.Target.position);
			this.Distance = this.distance;
			Vector3 eulerAngles = base.Transform.eulerAngles;
			this.x = eulerAngles.y;
			this.y = eulerAngles.x;
			this.initXRotation = eulerAngles.y;
			UnityEngine.Debug.Log(this.initXRotation);
			this.horizontal = this.x;
			this.vertical = this.y;
		}
		else
		{
			this.distance = this.Distance;
		}
		this.currentFog = base.GetCamera.fieldOfView;
		this.defaultFog = this.currentFog;
		base.GetCamera.fieldOfView = this.FogStart;
		this.defaultAutoSpeed = this.AutoRotSpeed;
		base.StartCoroutine(this.IEDelayFog());
		if (this.RotateInputKey == CameraMouseInputType.MobileTouch && UnityEngine.Object.FindObjectOfType<bl_OrbitTouchPad>() == null)
		{
			UnityEngine.Debug.LogWarning("For use  mobile touched be sure to put the 'OrbitTouchArea in the canvas of scene");
		}
	}

	private void LateUpdate()
	{
		if (this.Target == null)
		{
			UnityEngine.Debug.LogWarning("Target is not assigned to orbit camera!", this);
			return;
		}
		if (this.isSwitchingTarget)
		{
			return;
		}
		if (this.CanRotate)
		{
			this.ZoomControll(false);
			this.OrbitControll();
			if (this.AutoRotate && !this.isInputKeyRotate)
			{
				this.AutoRotation();
			}
		}
		else
		{
			this.ZoomControll(true);
		}
		if (!this.m_Interact)
		{
			return;
		}
		this.FogControl();
		this.InputControl();
	}

	private void InputControl()
	{
		if (this.LockCursorOnRotate && !this.useKeys && !this.isForMobile)
		{
			if (!this.isInputKeyRotate && this.LastHaveInput)
			{
				if (this.LockCursorOnRotate && this.Interact)
				{
					bl_CameraUtils.LockCursor(false);
				}
				this.LastHaveInput = false;
				if (this.lastHorizontal >= 0f)
				{
					this.AutoRotSpeed = this.OutInputSpeed;
				}
				else
				{
					this.AutoRotSpeed = -this.OutInputSpeed;
				}
			}
			if (this.isInputKeyRotate && !this.LastHaveInput)
			{
				if (this.LockCursorOnRotate && this.Interact)
				{
					bl_CameraUtils.LockCursor(true);
				}
				this.LastHaveInput = true;
			}
		}
		if (this.isInputUpKeyRotate)
		{
			this.currentFog -= this.PuwFogAmount;
		}
	}

	private void AutoRotation()
	{
		this.AutoRotSpeed = ((this.lastHorizontal <= 0f) ? Mathf.Lerp(this.AutoRotSpeed, -this.defaultAutoSpeed, Time.deltaTime / 2f) : Mathf.Lerp(this.AutoRotSpeed, this.defaultAutoSpeed, Time.deltaTime / 2f));
		this.horizontal += Time.deltaTime * this.AutoRotSpeed;
	}

	private void FogControl()
	{
		if (!this.canFogControl)
		{
			return;
		}
		this.currentFog = Mathf.SmoothStep(this.currentFog, this.defaultFog, Time.deltaTime * this.FogLerp);
		base.GetCamera.fieldOfView = Mathf.Lerp(base.GetCamera.fieldOfView, this.currentFog, Time.deltaTime * this.FogLerp);
	}

	private void OrbitControll()
	{
		if (this.m_Interact && !this.isForMobile)
		{
			if ((this.RequieredInput && !this.useKeys && this.isInputKeyRotate) || !this.RequieredInput)
			{
				this.horizontal += this.SpeedAxis.x * this.InputMultiplier * base.AxisX;
				this.vertical -= this.SpeedAxis.y * this.InputMultiplier * base.AxisY;
				this.lastHorizontal = base.AxisX;
			}
			else if (this.useKeys)
			{
				this.horizontal -= base.KeyAxisX * this.SpeedAxis.x * this.InputMultiplier;
				this.vertical += base.KeyAxisY * this.SpeedAxis.y * this.InputMultiplier;
				this.lastHorizontal = base.KeyAxisX;
			}
		}
		this.vertical = bl_CameraUtils.ClampAngle(this.vertical, this.YLimitClamp.x, this.YLimitClamp.y);
		if (this.XLimitClamp.x < 360f && this.XLimitClamp.y < 360f)
		{
			this.horizontal = bl_CameraUtils.ClampAngle(this.horizontal, this.initXRotation - this.XLimitClamp.y, this.XLimitClamp.x + this.initXRotation);
		}
		this.x = Mathf.Lerp(this.x, this.horizontal, Time.deltaTime * this.InputLerp);
		this.y = Mathf.Lerp(this.y, this.vertical, Time.deltaTime * this.InputLerp);
		this.y = bl_CameraUtils.ClampAngle(this.y, this.YLimitClamp.x, this.YLimitClamp.y);
		this.CurrentRotation = Quaternion.Euler(this.y, this.x, 0f);
		this.CurrentPosition = this.CurrentRotation * this.ZoomVector + this.Target.position;
		CameraMovementType movementType = this.MovementType;
		if (movementType != CameraMovementType.Dynamic)
		{
			if (movementType != CameraMovementType.Normal)
			{
				if (movementType == CameraMovementType.Towars)
				{
					base.Transform.rotation = Quaternion.RotateTowards(base.Transform.rotation, this.CurrentRotation, this.LerpSpeed);
					base.Transform.position = Vector3.MoveTowards(base.Transform.position, this.CurrentPosition, this.LerpSpeed);
				}
			}
			else
			{
				base.Transform.rotation = this.CurrentRotation;
				base.Transform.position = this.CurrentPosition;
			}
		}
		else
		{
			base.Transform.position = Vector3.Lerp(base.Transform.position, this.CurrentPosition, this.LerpSpeed * Time.deltaTime);
			base.Transform.rotation = Quaternion.Lerp(base.Transform.rotation, this.CurrentRotation, this.LerpSpeed * 2f * Time.deltaTime);
		}
	}

	private void ZoomControll(bool autoApply)
	{
		bool flag = false;
		if (CameraControllerScript.CamCounter == 1)
		{
			this.distance = Mathf.Clamp(this.distance - base.MouseScrollWheel * this.ScrollSensitivity, this.DistanceClamp.x, this.DistanceClamp.y);
		}
		if (this.DetectCollision)
		{
			Vector3 vector = base.Transform.position - this.Target.position;
			this.Ray = new Ray(this.Target.position, vector.normalized);
			RaycastHit raycastHit;
			if (Physics.SphereCast(this.Ray.origin, this.CollisionRadius, this.Ray.direction, out raycastHit, this.distance, this.DetectCollisionLayers))
			{
				if (!this.haveHit)
				{
					this.LastDistance = this.distance;
					this.haveHit = true;
				}
				this.distance = Mathf.Clamp(raycastHit.distance, this.DistanceClamp.x, this.DistanceClamp.y);
				if (this.TeleporOnHit)
				{
					this.Distance = this.distance;
				}
				flag = true;
			}
			else if (!this.isDetectingHit)
			{
				base.StartCoroutine(this.DetectHit());
			}
			this.distance = ((this.distance >= 1f) ? this.distance : 1f);
			if (!this.haveHit || !this.TeleporOnHit)
			{
				float num = (!flag) ? 1f : 3.14159274f;
				this.Distance = Mathf.SmoothStep(this.Distance, this.distance, Time.deltaTime * (this.ZoomSpeed * num));
			}
		}
		else
		{
			this.distance = ((this.distance >= 1f) ? this.distance : 1f);
			this.Distance = Mathf.SmoothStep(this.Distance, this.distance, Time.deltaTime * this.ZoomSpeed);
		}
		this.ZoomVector = new Vector3(0f, 0f, -this.Distance);
		if (autoApply)
		{
			this.CurrentPosition = this.CurrentRotation * this.ZoomVector + this.Target.position;
			CameraMovementType movementType = this.MovementType;
			if (movementType != CameraMovementType.Dynamic)
			{
				if (movementType != CameraMovementType.Normal)
				{
					if (movementType == CameraMovementType.Towars)
					{
						base.Transform.rotation = Quaternion.RotateTowards(base.Transform.rotation, this.CurrentRotation, this.LerpSpeed);
						base.Transform.position = Vector3.MoveTowards(base.Transform.position, this.CurrentPosition, this.LerpSpeed);
					}
				}
				else
				{
					base.Transform.rotation = this.CurrentRotation;
					base.Transform.position = this.CurrentPosition;
				}
			}
			else
			{
				base.Transform.position = Vector3.Lerp(base.Transform.position, this.CurrentPosition, this.LerpSpeed * Time.deltaTime);
				base.Transform.rotation = Quaternion.Lerp(base.Transform.rotation, this.CurrentRotation, this.LerpSpeed * 2f * Time.deltaTime);
			}
		}
	}

	private bool isInputKeyRotate
	{
		get
		{
			switch (this.RotateInputKey)
			{
			case CameraMouseInputType.LeftMouse:
				return UnityEngine.Input.GetKey(KeyCode.Mouse0);
			case CameraMouseInputType.RightMouse:
				return UnityEngine.Input.GetKey(KeyCode.Mouse1);
			case CameraMouseInputType.LeftAndRight:
				return UnityEngine.Input.GetKey(KeyCode.Mouse0) || UnityEngine.Input.GetKey(KeyCode.Mouse1);
			case CameraMouseInputType.MouseScroll:
				return UnityEngine.Input.GetKey(KeyCode.Mouse2);
			case CameraMouseInputType.MobileTouch:
				return Input.GetMouseButton(0) || Input.GetMouseButton(1);
			case CameraMouseInputType.All:
				return UnityEngine.Input.GetKey(KeyCode.Mouse0) || UnityEngine.Input.GetKey(KeyCode.Mouse1) || UnityEngine.Input.GetKey(KeyCode.Mouse2) || Input.GetMouseButton(0);
			default:
				return UnityEngine.Input.GetKey(KeyCode.Mouse0);
			}
		}
	}

	private void OnGUI()
	{
		if (this.isSwitchingTarget)
		{
			GUI.color = new Color(1f, 1f, 1f, this.FadeAlpha);
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.FadeTexture, ScaleMode.StretchToFill);
			return;
		}
		if (this.FadeOnStart && this.FadeAlpha > 0f)
		{
			this.FadeAlpha -= Time.deltaTime * this.FadeSpeed;
			GUI.color = new Color(1f, 1f, 1f, this.FadeAlpha);
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.FadeTexture, ScaleMode.StretchToFill);
		}
	}

	private bool isInputUpKeyRotate
	{
		get
		{
			switch (this.RotateInputKey)
			{
			case CameraMouseInputType.LeftMouse:
				return UnityEngine.Input.GetKeyUp(KeyCode.Mouse0) || Input.GetMouseButtonUp(0);
			case CameraMouseInputType.RightMouse:
				return UnityEngine.Input.GetKeyUp(KeyCode.Mouse1);
			case CameraMouseInputType.LeftAndRight:
				return UnityEngine.Input.GetKeyUp(KeyCode.Mouse0) || UnityEngine.Input.GetKeyUp(KeyCode.Mouse1);
			case CameraMouseInputType.MouseScroll:
				return UnityEngine.Input.GetKeyUp(KeyCode.Mouse2);
			case CameraMouseInputType.MobileTouch:
				return Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1);
			case CameraMouseInputType.All:
				return UnityEngine.Input.GetKeyUp(KeyCode.Mouse0) || UnityEngine.Input.GetKeyUp(KeyCode.Mouse1) || UnityEngine.Input.GetKeyUp(KeyCode.Mouse2) || Input.GetMouseButtonUp(0);
			default:
				return UnityEngine.Input.GetKey(KeyCode.Mouse0) || Input.GetMouseButton(0);
			}
		}
	}

	public void SetTarget(Transform newTarget)
	{
		base.StopCoroutine("TranslateTarget");
		base.StartCoroutine("TranslateTarget", newTarget);
	}

	private IEnumerator TranslateTarget(Transform newTarget)
	{
		this.isSwitchingTarget = true;
		while (this.FadeAlpha < 1f)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, base.transform.position + new Vector3(0f, 2f, -2f), Time.deltaTime);
			this.FadeAlpha += Time.smoothDeltaTime * this.SwichtSpeed;
			yield return null;
		}
		this.Target = newTarget;
		this.isSwitchingTarget = false;
		while (this.FadeAlpha > 0f)
		{
			this.FadeAlpha -= Time.smoothDeltaTime * this.SwichtSpeed;
			yield return null;
		}
		yield break;
	}

	private IEnumerator DetectHit()
	{
		this.isDetectingHit = true;
		yield return new WaitForSeconds(0.4f);
		if (this.haveHit)
		{
			this.distance = this.LastDistance;
			this.haveHit = false;
		}
		this.isDetectingHit = false;
		yield break;
	}

	private IEnumerator IEDelayFog()
	{
		yield return new WaitForSeconds(this.DelayStartFog);
		this.canFogControl = true;
		yield break;
	}

	public float Horizontal
	{
		get
		{
			return this.horizontal;
		}
		set
		{
			this.horizontal += value;
			this.lastHorizontal = this.horizontal;
		}
	}

	public float Vertical
	{
		get
		{
			return this.vertical;
		}
		set
		{
			this.vertical += value;
		}
	}

	public bool Interact
	{
		get
		{
			return this.m_Interact;
		}
		set
		{
			this.m_Interact = value;
		}
	}

	public bool CanRotate
	{
		get
		{
			return this.m_CanRotate;
		}
		set
		{
			this.m_CanRotate = value;
		}
	}

	public float AutoRotationSpeed
	{
		get
		{
			return this.defaultAutoSpeed;
		}
		set
		{
			this.defaultAutoSpeed = value;
		}
	}

	public void SetZoom(float value)
	{
		if (CameraControllerScript.CamCounter == 1)
		{
			this.distance += -(value * 0.5f) * this.ScrollSensitivity;
		}
	}

	public void SetStaticZoom(float value)
	{
		if (CameraControllerScript.CamCounter == 1)
		{
			this.distance += value;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color32(0, 221, 221, byte.MaxValue);
		if (this.Target != null)
		{
			Gizmos.DrawLine(base.transform.position, this.Target.position);
			Gizmos.matrix = Matrix4x4.TRS(this.Target.position, base.transform.rotation, new Vector3(1f, 0f, 1f));
			Gizmos.DrawWireSphere(this.Target.position, this.Distance);
			Gizmos.matrix = Matrix4x4.identity;
		}
		Gizmos.DrawCube(base.transform.position, new Vector3(1f, 0.2f, 0.2f));
		Gizmos.DrawCube(base.transform.position, Vector3.one / 2f);
	}

	[HideInInspector]
	public bool m_Interact = true;

	[Header("Target")]
	public Transform Target;

	[Header("Settings")]
	public bool isForMobile;

	public bool AutoTakeInfo = true;

	public float Distance = 5f;

	[Range(0.01f, 5f)]
	public float SwichtSpeed = 2f;

	public Vector2 DistanceClamp = new Vector2(1.5f, 5f);

	public Vector2 YLimitClamp = new Vector2(-20f, 80f);

	public Vector2 XLimitClamp = new Vector2(360f, 360f);

	public Vector2 SpeedAxis = new Vector2(100f, 100f);

	public bool LockCursorOnRotate = true;

	[Header("Input")]
	public bool RequieredInput = true;

	public CameraMouseInputType RotateInputKey = CameraMouseInputType.LeftAndRight;

	[Range(0.001f, 0.07f)]
	public float InputMultiplier = 0.02f;

	[Range(0.1f, 15f)]
	public float InputLerp = 7f;

	public bool useKeys;

	[Header("Movement")]
	public CameraMovementType MovementType;

	[Range(-90f, 90f)]
	public float PuwFogAmount = -5f;

	[Range(0.1f, 20f)]
	public float LerpSpeed = 7f;

	[Range(1f, 100f)]
	public float OutInputSpeed = 20f;

	[Header("Fog")]
	[Range(5f, 179f)]
	public float FogStart = 100f;

	[Range(0.1f, 15f)]
	public float FogLerp = 7f;

	[Range(0f, 7f)]
	public float DelayStartFog = 2f;

	[Range(1f, 10f)]
	public float ScrollSensitivity = 5f;

	[Range(1f, 25f)]
	public float ZoomSpeed = 7f;

	[Header("Auto Rotation")]
	public bool AutoRotate = true;

	[Range(0f, 20f)]
	public float AutoRotSpeed = 5f;

	[Header("Collision")]
	public bool DetectCollision = true;

	public bool TeleporOnHit = true;

	[Range(0.01f, 4f)]
	public float CollisionRadius = 2f;

	public LayerMask DetectCollisionLayers;

	[Header("Fade")]
	public bool FadeOnStart = true;

	[Range(0.01f, 5f)]
	public float FadeSpeed = 2f;

	[SerializeField]
	private Texture2D FadeTexture;

	private float y;

	private float x;

	private Ray Ray;

	private bool LastHaveInput;

	private float distance;

	private float currentFog = 60f;

	private float defaultFog;

	private float horizontal;

	private float vertical;

	private float defaultAutoSpeed;

	private float lastHorizontal;

	private bool canFogControl;

	private bool haveHit;

	private float LastDistance;

	private bool m_CanRotate = true;

	private Vector3 ZoomVector;

	private Quaternion CurrentRotation;

	private Vector3 CurrentPosition;

	private float FadeAlpha = 1f;

	private bool isSwitchingTarget;

	private bool isDetectingHit;

	private float initXRotation;
}
