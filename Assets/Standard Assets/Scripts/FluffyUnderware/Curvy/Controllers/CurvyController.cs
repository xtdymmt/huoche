// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Controllers.CurvyController
using System;
using System.Globalization;
using System.Reflection;
using FluffyUnderware.Curvy.Utils;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.Serialization;
using MinAttribute = FluffyUnderware.DevTools.MinAttribute;
namespace FluffyUnderware.Curvy.Controllers
{
	[ExecuteInEditMode]
	public abstract class CurvyController : DTVersionedMonoBehaviour, ISerializationCallbackReceiver
	{
		public ControllerEvent OnInitialized
		{
			get
			{
				return this.onInitialized;
			}
		}

		public CurvyPositionMode PositionMode
		{
			get
			{
				return this.m_PositionMode;
			}
			set
			{
				this.m_PositionMode = value;
			}
		}

		public CurvyController.MoveModeEnum MoveMode
		{
			get
			{
				return this.m_MoveMode;
			}
			set
			{
				if (this.m_MoveMode != value)
				{
					this.m_MoveMode = value;
				}
			}
		}

		public bool PlayAutomatically
		{
			get
			{
				return this.m_PlayAutomatically;
			}
			set
			{
				if (this.m_PlayAutomatically != value)
				{
					this.m_PlayAutomatically = value;
				}
			}
		}

		public CurvyClamping Clamping
		{
			get
			{
				return this.m_Clamping;
			}
			set
			{
				if (this.m_Clamping != value)
				{
					this.m_Clamping = value;
				}
			}
		}

		public OrientationModeEnum OrientationMode
		{
			get
			{
				return this.m_OrientationMode;
			}
			set
			{
				if (this.m_OrientationMode != value)
				{
					this.m_OrientationMode = value;
				}
			}
		}

		public bool LockRotation
		{
			get
			{
				return this.m_LockRotation;
			}
			set
			{
				if (this.m_LockRotation != value)
				{
					this.m_LockRotation = value;
				}
				if (this.m_LockRotation)
				{
					this.lockedRotation = this.Transform.rotation;
				}
			}
		}

		public OrientationAxisEnum OrientationAxis
		{
			get
			{
				return this.m_OrientationAxis;
			}
			set
			{
				if (this.m_OrientationAxis != value)
				{
					this.m_OrientationAxis = value;
				}
			}
		}

		public float DirectionDampingTime
		{
			get
			{
				return this.m_DampingDirection;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_DampingDirection != num)
				{
					this.m_DampingDirection = num;
				}
			}
		}

		public float UpDampingTime
		{
			get
			{
				return this.m_DampingUp;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_DampingUp != num)
				{
					this.m_DampingUp = num;
				}
			}
		}

		public bool IgnoreDirection
		{
			get
			{
				return this.m_IgnoreDirection;
			}
			set
			{
				if (this.m_IgnoreDirection != value)
				{
					this.m_IgnoreDirection = value;
				}
			}
		}

		public float OffsetAngle
		{
			get
			{
				return this.m_OffsetAngle;
			}
			set
			{
				if (this.m_OffsetAngle != value)
				{
					this.m_OffsetAngle = value;
				}
			}
		}

		public float OffsetRadius
		{
			get
			{
				return this.m_OffsetRadius;
			}
			set
			{
				if (this.m_OffsetRadius != value)
				{
					this.m_OffsetRadius = value;
				}
			}
		}

		public bool OffsetCompensation
		{
			get
			{
				return this.m_OffsetCompensation;
			}
			set
			{
				this.m_OffsetCompensation = value;
			}
		}

		public float Speed
		{
			get
			{
				return this.m_Speed;
			}
			set
			{
				if (value < 0f)
				{
					value = -value;
				}
				this.m_Speed = value;
			}
		}

		public float RelativePosition
		{
			get
			{
				CurvyPositionMode positionMode = this.PositionMode;
				float result;
				if (positionMode != CurvyPositionMode.Relative)
				{
					if (positionMode != CurvyPositionMode.WorldUnits)
					{
						throw new NotSupportedException();
					}
					result = this.AbsoluteToRelative(CurvyController.GetClampedPosition(this.m_Position, CurvyPositionMode.WorldUnits, this.Clamping, this.Length));
				}
				else
				{
					result = CurvyController.GetClampedPosition(this.m_Position, CurvyPositionMode.Relative, this.Clamping, this.Length);
				}
				return result;
			}
			set
			{
				float clampedPosition = CurvyController.GetClampedPosition(value, CurvyPositionMode.Relative, this.Clamping, this.Length);
				CurvyPositionMode positionMode = this.PositionMode;
				if (positionMode != CurvyPositionMode.Relative)
				{
					if (positionMode != CurvyPositionMode.WorldUnits)
					{
						throw new ArgumentOutOfRangeException();
					}
					this.m_Position = this.RelativeToAbsolute(clampedPosition);
				}
				else
				{
					this.m_Position = clampedPosition;
				}
			}
		}

		public float AbsolutePosition
		{
			get
			{
				CurvyPositionMode positionMode = this.PositionMode;
				float result;
				if (positionMode != CurvyPositionMode.Relative)
				{
					if (positionMode != CurvyPositionMode.WorldUnits)
					{
						throw new NotSupportedException();
					}
					result = CurvyController.GetClampedPosition(this.m_Position, CurvyPositionMode.WorldUnits, this.Clamping, this.Length);
				}
				else
				{
					result = this.RelativeToAbsolute(CurvyController.GetClampedPosition(this.m_Position, CurvyPositionMode.Relative, this.Clamping, this.Length));
				}
				return result;
			}
			set
			{
				float clampedPosition = CurvyController.GetClampedPosition(value, CurvyPositionMode.WorldUnits, this.Clamping, this.Length);
				CurvyPositionMode positionMode = this.PositionMode;
				if (positionMode != CurvyPositionMode.Relative)
				{
					if (positionMode != CurvyPositionMode.WorldUnits)
					{
						throw new ArgumentOutOfRangeException();
					}
					this.m_Position = clampedPosition;
				}
				else
				{
					this.m_Position = this.AbsoluteToRelative(clampedPosition);
				}
			}
		}

		public float Position
		{
			get
			{
				CurvyPositionMode positionMode = this.PositionMode;
				float result;
				if (positionMode != CurvyPositionMode.Relative)
				{
					if (positionMode != CurvyPositionMode.WorldUnits)
					{
						throw new NotSupportedException();
					}
					result = this.AbsolutePosition;
				}
				else
				{
					result = this.RelativePosition;
				}
				return result;
			}
			set
			{
				CurvyPositionMode positionMode = this.PositionMode;
				if (positionMode != CurvyPositionMode.Relative)
				{
					if (positionMode != CurvyPositionMode.WorldUnits)
					{
						throw new NotSupportedException();
					}
					this.AbsolutePosition = value;
				}
				else
				{
					this.RelativePosition = value;
				}
			}
		}

		public MovementDirection MovementDirection
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value;
			}
		}

		public CurvyController.CurvyControllerState PlayState
		{
			get
			{
				return this.state;
			}
		}

		public abstract bool IsReady { get; }

		 protected bool isInitialized {  get; private set; }

		protected virtual void OnEnable()
		{
			if (!this.isInitialized && this.IsReady)
			{
				this.Initialize();
				this.InitializedApplyDeltaTime(0f);
			}
		}

		protected virtual void Start()
		{
			if (!this.isInitialized && this.IsReady)
			{
				this.Initialize();
				this.InitializedApplyDeltaTime(0f);
			}
			if (this.PlayAutomatically && Application.isPlaying)
			{
				this.Play();
			}
		}

		protected virtual void OnDisable()
		{
			if (this.isInitialized)
			{
				this.Deinitialize();
			}
		}

		protected virtual void Update()
		{
			if (this.UpdateIn == CurvyUpdateMethod.Update)
			{
				this.ApplyDeltaTime(this.TimeSinceLastUpdate);
			}
		}

		protected virtual void LateUpdate()
		{
			if (this.UpdateIn == CurvyUpdateMethod.LateUpdate || (!Application.isPlaying && this.UpdateIn == CurvyUpdateMethod.FixedUpdate))
			{
				this.ApplyDeltaTime(this.TimeSinceLastUpdate);
			}
		}

		protected virtual void FixedUpdate()
		{
			if (this.UpdateIn == CurvyUpdateMethod.FixedUpdate)
			{
				this.ApplyDeltaTime(this.TimeSinceLastUpdate);
			}
		}

		protected virtual void Reset()
		{
			this.UpdateIn = CurvyUpdateMethod.Update;
			this.PositionMode = CurvyPositionMode.Relative;
			this.m_Position = 0f;
			this.PlayAutomatically = true;
			this.MoveMode = CurvyController.MoveModeEnum.AbsolutePrecise;
			this.Speed = 0f;
			this.LockRotation = true;
			this.Clamping = CurvyClamping.Loop;
			this.OrientationMode = OrientationModeEnum.Orientation;
			this.OrientationAxis = OrientationAxisEnum.Up;
			this.IgnoreDirection = false;
		}

		public virtual Transform Transform
		{
			get
			{
				return base.transform;
			}
		}

		protected virtual void InitializedApplyDeltaTime(float deltaTime)
		{
			if (this.state == CurvyController.CurvyControllerState.Playing && this.Speed * deltaTime != 0f)
			{
				float num = (!this.OffsetCompensation) ? this.Speed : this.ComputeOffsetCompensatedSpeed(deltaTime);
				if (num * deltaTime != 0f)
				{
					this.Advance(num, deltaTime);
				}
			}
			Vector3 position = this.Transform.position;
			Quaternion rotation = this.Transform.rotation;
			Vector3 position2;
			Vector3 vector;
			Vector3 vector2;
			this.ComputeTargetPositionAndRotation(out position2, out vector, out vector2);
			Vector3 forward;
			if (this.DirectionDampingTime > 0f && this.state == CurvyController.CurvyControllerState.Playing)
			{
				forward = ((deltaTime <= 0f) ? this.Transform.forward : Vector3.SmoothDamp(this.Transform.forward, vector2, ref this.directionDampingVelocity, this.DirectionDampingTime, float.PositiveInfinity, deltaTime));
			}
			else
			{
				forward = vector2;
			}
			Vector3 upwards;
			if (this.UpDampingTime > 0f && this.state == CurvyController.CurvyControllerState.Playing)
			{
				upwards = ((deltaTime <= 0f) ? this.Transform.up : Vector3.SmoothDamp(this.Transform.up, vector, ref this.upDampingVelocity, this.UpDampingTime, float.PositiveInfinity, deltaTime));
			}
			else
			{
				upwards = vector;
			}
			this.Transform.rotation = Quaternion.LookRotation(forward, upwards);
			this.Transform.position = position2;
			if (position.NotApproximately(this.Transform.position) || rotation.DifferentOrientation(this.Transform.rotation))
			{
				this.UserAfterUpdate();
			}
		}

		protected virtual void ComputeTargetPositionAndRotation(out Vector3 targetPosition, out Vector3 targetUp, out Vector3 targetForward)
		{
			Vector3 pos;
			Vector3 vector;
			Vector3 vector2;
			this.GetInterpolatedSourcePosition(this.RelativePosition, out pos, out vector, out vector2);
			if (vector == Vector3.zero || vector2 == Vector3.zero)
			{
				this.GetOrientationNoneUpAndForward(out targetUp, out targetForward);
			}
			else
			{
				switch (this.OrientationMode)
				{
				case OrientationModeEnum.None:
					this.GetOrientationNoneUpAndForward(out targetUp, out targetForward);
					break;
				case OrientationModeEnum.Orientation:
				{
					Vector3 vector3 = (this.m_Direction != MovementDirection.Backward || this.IgnoreDirection) ? vector : (-vector);
					switch (this.OrientationAxis)
					{
					case OrientationAxisEnum.Up:
						targetUp = vector2;
						targetForward = vector3;
						break;
					case OrientationAxisEnum.Down:
						targetUp = -vector2;
						targetForward = vector3;
						break;
					case OrientationAxisEnum.Forward:
						targetUp = -vector3;
						targetForward = vector2;
						break;
					case OrientationAxisEnum.Backward:
						targetUp = vector3;
						targetForward = -vector2;
						break;
					case OrientationAxisEnum.Left:
						targetUp = Vector3.Cross(vector2, vector3);
						targetForward = vector3;
						break;
					case OrientationAxisEnum.Right:
						targetUp = Vector3.Cross(vector3, vector2);
						targetForward = vector3;
						break;
					default:
						throw new NotSupportedException();
					}
					break;
				}
				case OrientationModeEnum.Tangent:
				{
					Vector3 vector4 = (this.m_Direction != MovementDirection.Backward || this.IgnoreDirection) ? vector : (-vector);
					switch (this.OrientationAxis)
					{
					case OrientationAxisEnum.Up:
						targetUp = vector4;
						targetForward = -vector2;
						break;
					case OrientationAxisEnum.Down:
						targetUp = -vector4;
						targetForward = vector2;
						break;
					case OrientationAxisEnum.Forward:
						targetUp = vector2;
						targetForward = vector4;
						break;
					case OrientationAxisEnum.Backward:
						targetUp = vector2;
						targetForward = -vector4;
						break;
					case OrientationAxisEnum.Left:
						targetUp = vector2;
						targetForward = Vector3.Cross(vector2, vector4);
						break;
					case OrientationAxisEnum.Right:
						targetUp = vector2;
						targetForward = Vector3.Cross(vector4, vector2);
						break;
					default:
						throw new NotSupportedException();
					}
					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
			targetPosition = CurvyController.ApplyOffset(pos, vector, vector2, this.OffsetAngle, this.OffsetRadius);
		}

		private void GetOrientationNoneUpAndForward(out Vector3 targetUp, out Vector3 targetForward)
		{
			if (this.LockRotation)
			{
				targetUp = this.lockedRotation * Vector3.up;
				targetForward = this.lockedRotation * Vector3.forward;
			}
			else
			{
				targetUp = this.Transform.up;
				targetForward = this.Transform.forward;
			}
		}

		protected virtual void Initialize()
		{
			this.isInitialized = true;
			this.lockedRotation = this.Transform.rotation;
			this.directionDampingVelocity = (this.upDampingVelocity = Vector3.zero);
			this.BindEvents();
			this.UserAfterInit();
			this.onInitialized.Invoke(this);
		}

		protected virtual void Deinitialize()
		{
			this.UnbindEvents();
			this.isInitialized = false;
		}

		protected virtual void BindEvents()
		{
		}

		protected virtual void UnbindEvents()
		{
		}

		protected virtual void SavePrePlayState()
		{
			this.prePlayPosition = this.m_Position;
			this.prePlayDirection = this.m_Direction;
		}

		protected virtual void RestorePrePlayState()
		{
			this.m_Position = this.prePlayPosition;
			this.m_Direction = this.prePlayDirection;
		}

		protected virtual void UserAfterInit()
		{
		}

		protected virtual void UserAfterUpdate()
		{
		}

		protected virtual bool ShowOrientationSection
		{
			get
			{
				return true;
			}
		}

		protected virtual bool ShowOffsetSection
		{
			get
			{
				return true;
			}
		}

		public abstract float Length { get; }

		protected abstract void Advance(float speed, float deltaTime);

		protected abstract void SimulateAdvance(ref float tf, ref MovementDirection curyDirection, float speed, float deltaTime);

		protected abstract float AbsoluteToRelative(float worldUnitDistance);

		protected abstract float RelativeToAbsolute(float relativeDistance);

		protected abstract Vector3 GetInterpolatedSourcePosition(float tf);

		protected abstract void GetInterpolatedSourcePosition(float tf, out Vector3 interpolatedPosition, out Vector3 tangent, out Vector3 up);

		protected abstract Vector3 GetOrientation(float tf);

		protected abstract Vector3 GetTangent(float tf);

		public void Play()
		{
			if (this.PlayState == CurvyController.CurvyControllerState.Stopped)
			{
				this.SavePrePlayState();
			}
			this.state = CurvyController.CurvyControllerState.Playing;
		}

		public void Stop()
		{
			if (this.PlayState != CurvyController.CurvyControllerState.Stopped)
			{
				this.RestorePrePlayState();
			}
			this.state = CurvyController.CurvyControllerState.Stopped;
		}

		public void Pause()
		{
			if (this.PlayState == CurvyController.CurvyControllerState.Playing)
			{
				this.state = CurvyController.CurvyControllerState.Paused;
			}
		}

		public void Refresh()
		{
			this.ApplyDeltaTime(0f);
		}

		public void ApplyDeltaTime(float deltaTime)
		{
			if (!this.isInitialized && this.IsReady)
			{
				this.Initialize();
			}
			else if (this.isInitialized && !this.IsReady)
			{
				this.Deinitialize();
			}
			if (this.isInitialized)
			{
				this.InitializedApplyDeltaTime(deltaTime);
			}
		}

		public void TeleportTo(float newPosition)
		{
			float distance = Mathf.Abs(this.Position - newPosition);
			MovementDirection direction = (this.Position >= newPosition) ? MovementDirection.Backward : MovementDirection.Forward;
			this.TeleportBy(distance, direction);
		}

		public void TeleportBy(float distance, MovementDirection direction)
		{
			float speed = this.Speed;
			MovementDirection movementDirection = this.MovementDirection;
			this.Speed = Mathf.Abs(distance) * 1000f;
			this.MovementDirection = direction;
			this.ApplyDeltaTime(0.001f);
			this.Speed = speed;
			this.MovementDirection = movementDirection;
		}

		public void SetFromString(string fieldAndValue)
		{
			string[] array = fieldAndValue.Split(new char[]
			{
				'='
			});
			if (array.Length != 2)
			{
				return;
			}
			FieldInfo fieldInfo = base.GetType().FieldByName(array[0], true, false);
			if (fieldInfo != null)
			{
				try
				{
					if (fieldInfo.FieldType.IsEnum)
					{
						fieldInfo.SetValue(this, Enum.Parse(fieldInfo.FieldType, array[1]));
					}
					else
					{
						fieldInfo.SetValue(this, Convert.ChangeType(array[1], fieldInfo.FieldType, CultureInfo.InvariantCulture));
					}
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogWarning(base.name + ".SetFromString(): " + ex.ToString());
				}
			}
			else
			{
				PropertyInfo propertyInfo = base.GetType().PropertyByName(array[0], true, false);
				if (propertyInfo != null)
				{
					try
					{
						if (propertyInfo.PropertyType.IsEnum)
						{
							propertyInfo.SetValue(this, Enum.Parse(propertyInfo.PropertyType, array[1]), null);
						}
						else
						{
							propertyInfo.SetValue(this, Convert.ChangeType(array[1], propertyInfo.PropertyType, CultureInfo.InvariantCulture), null);
						}
					}
					catch (Exception ex2)
					{
						UnityEngine.Debug.LogWarning(base.name + ".SetFromString(): " + ex2.ToString());
					}
				}
			}
		}

		private float TimeSinceLastUpdate
		{
			get
			{
				return Time.deltaTime;
			}
		}

		private void editorUpdate()
		{
			if (!Application.isPlaying)
			{
				this.ApplyDeltaTime(this.TimeSinceLastUpdate);
			}
		}

		private static Vector3 ApplyOffset(Vector3 pos, Vector3 tan, Vector3 up, float angle, float radius)
		{
			Quaternion rotation = Quaternion.AngleAxis(angle, tan);
			return pos + rotation * up * radius;
		}

		protected static float GetClampedPosition(float position, CurvyPositionMode positionMode, CurvyClamping clampingMode, float length)
		{
			float result;
			if (positionMode != CurvyPositionMode.Relative)
			{
				if (positionMode != CurvyPositionMode.WorldUnits)
				{
					throw new NotSupportedException();
				}
				result = CurvyUtility.ClampDistance(position, clampingMode, length);
			}
			else
			{
				result = CurvyUtility.ClampTF(position, clampingMode);
			}
			return result;
		}

		private float maxPosition
		{
			get
			{
				CurvyPositionMode positionMode = this.PositionMode;
				float result;
				if (positionMode != CurvyPositionMode.Relative)
				{
					if (positionMode != CurvyPositionMode.WorldUnits)
					{
						throw new NotSupportedException();
					}
					result = ((!this.IsReady) ? 0f : this.Length);
				}
				else
				{
					result = 1f;
				}
				return result;
			}
		}

		private float ComputeOffsetCompensatedSpeed(float deltaTime)
		{
			if (this.OrientationMode == OrientationModeEnum.None || this.OffsetRadius.Approximately(0f))
			{
				return this.Speed;
			}
			Vector3 vector;
			Vector3 tan;
			Vector3 up;
			this.GetInterpolatedSourcePosition(this.RelativePosition, out vector, out tan, out up);
			Vector3 a = CurvyController.ApplyOffset(vector, tan, up, this.OffsetAngle, this.OffsetRadius);
			float relativePosition = this.RelativePosition;
			MovementDirection direction = this.m_Direction;
			this.SimulateAdvance(ref relativePosition, ref direction, this.Speed, deltaTime);
			Vector3 vector2;
			Vector3 tan2;
			Vector3 up2;
			this.GetInterpolatedSourcePosition(relativePosition, out vector2, out tan2, out up2);
			Vector3 b = CurvyController.ApplyOffset(vector2, tan2, up2, this.OffsetAngle, this.OffsetRadius);
			float magnitude = (vector2 - vector).magnitude;
			float magnitude2 = (a - b).magnitude;
			float num = magnitude / magnitude2;
			return this.Speed * ((!float.IsNaN(num)) ? num : 1f);
		}

		public virtual void OnAfterDeserialize()
		{
			if (this.m_Speed < 0f)
			{
				this.m_Speed = Mathf.Abs(this.m_Speed);
				this.m_Direction = MovementDirection.Backward;
			}
			if ((short)this.MoveMode == 2)
			{
				this.MoveMode = CurvyController.MoveModeEnum.AbsolutePrecise;
			}
		}

		public void OnBeforeSerialize()
		{
		}

		[Section("General", true, false, 100, Sort = 0, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_general")]
		[Label(Tooltip = "Determines when to update")]
		public CurvyUpdateMethod UpdateIn;

		[Section("Position", true, false, 100, Sort = 100, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_position")]
		[SerializeField]
		private CurvyPositionMode m_PositionMode;

		[RangeEx(0f, "maxPosition", "", "")]
		[SerializeField]
		[FormerlySerializedAs("m_InitialPosition")]
		protected float m_Position;

		[Section("Move", true, false, 100, Sort = 200, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_move")]
		[SerializeField]
		private CurvyController.MoveModeEnum m_MoveMode = CurvyController.MoveModeEnum.AbsolutePrecise;

		[Positive]
		[SerializeField]
		private float m_Speed;

		[SerializeField]
		private MovementDirection m_Direction;

		[SerializeField]
		private CurvyClamping m_Clamping = CurvyClamping.Loop;

		[SerializeField]
		[Tooltip("Start playing automatically when entering play mode")]
		private bool m_PlayAutomatically = true;

		[Section("Orientation", true, false, 100, Sort = 300, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_orientation")]
		[Label("Source", "Source Vector")]
		[FieldCondition("ShowOrientationSection", false, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private OrientationModeEnum m_OrientationMode = OrientationModeEnum.Orientation;

		[Label("Lock Rotation", "When set, the controller will enforce the rotation to not change")]
		[SerializeField]
		private bool m_LockRotation = true;

		[Label("Target", "Target Vector3")]
		[FieldCondition("m_OrientationMode", OrientationModeEnum.None, false, ConditionalAttribute.OperatorEnum.OR, "ShowOrientationSection", false, false, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private OrientationAxisEnum m_OrientationAxis;

		[Tooltip("Should the orientation ignore the movement direction?")]
		[FieldCondition("m_OrientationMode", OrientationModeEnum.None, false, ConditionalAttribute.OperatorEnum.OR, "ShowOrientationSection", false, false, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private bool m_IgnoreDirection;

		[Min(0f, "Direction Damping Time", "If non zero, the direction vector will not be updated instantly, but using a damping effect that will last the specified amount of time.")]
		[FieldCondition("ShowOrientationSection", false, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private float m_DampingDirection;

		[Min(0f, "Up Damping Time", "If non zero, the up vector will not be updated instantly, but using a damping effect that will last the specified amount of time.")]
		[FieldCondition("ShowOrientationSection", false, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private float m_DampingUp;

		[Section("Offset", true, false, 100, Sort = 400, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_orientation")]
		[FieldCondition("m_OrientationMode", OrientationModeEnum.None, false, ConditionalAttribute.OperatorEnum.OR, "ShowOffsetSection", false, false, Action = ActionAttribute.ActionEnum.Hide)]
		[RangeEx(-180f, 180f, "", "")]
		[SerializeField]
		private float m_OffsetAngle;

		[FieldCondition("m_OrientationMode", OrientationModeEnum.None, false, ConditionalAttribute.OperatorEnum.OR, "ShowOffsetSection", false, false, Action = ActionAttribute.ActionEnum.Hide)]
		[SerializeField]
		private float m_OffsetRadius;

		[FieldCondition("m_OrientationMode", OrientationModeEnum.None, false, ConditionalAttribute.OperatorEnum.OR, "ShowOffsetSection", false, false, Action = ActionAttribute.ActionEnum.Hide)]
		[Label("Compensate Offset", "")]
		[SerializeField]
		private bool m_OffsetCompensation = true;

		[Section("Events", true, false, 100, Sort = 500)]
		[SerializeField]
		private ControllerEvent onInitialized;

		protected const string ControllerNotReadyMessage = "The controller is not yet ready";

		private CurvyController.CurvyControllerState state;

		private Vector3 directionDampingVelocity;

		private Vector3 upDampingVelocity;

		private float prePlayPosition;

		private Quaternion lockedRotation;

		private MovementDirection prePlayDirection;

		public enum MoveModeEnum
		{
			Relative,
			AbsolutePrecise
		}

		public enum CurvyControllerState
		{
			Stopped,
			Playing,
			Paused
		}
	}
}
