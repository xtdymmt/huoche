// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Controllers.SplineController
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluffyUnderware.Curvy.Utils;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Controllers
{
	[AddComponentMenu("Curvy/Controller/Spline Controller", 5)]
	[HelpURL("https://curvyeditor.com/doclink/splinecontroller")]
	public class SplineController : CurvyController
	{
		public SplineController()
		{
			this.preAllocatedEventArgs = new CurvySplineMoveEventArgs(this, this.Spline, null, float.NaN, false, float.NaN, MovementDirection.Forward);
		}

		public virtual CurvySpline Spline
		{
			get
			{
				return this.m_Spline;
			}
			set
			{
				this.m_Spline = value;
			}
		}

		public bool UseCache
		{
			get
			{
				return this.m_UseCache;
			}
			set
			{
				if (this.m_UseCache != value)
				{
					this.m_UseCache = value;
				}
			}
		}

		public SplineControllerConnectionBehavior ConnectionBehavior
		{
			get
			{
				return this.connectionBehavior;
			}
			set
			{
				this.connectionBehavior = value;
			}
		}

		public ConnectedControlPointsSelector ConnectionCustomSelector
		{
			get
			{
				return this.connectionCustomSelector;
			}
			set
			{
				this.connectionCustomSelector = value;
			}
		}

		public bool AllowDirectionChange
		{
			get
			{
				return this.allowDirectionChange;
			}
			set
			{
				this.allowDirectionChange = value;
			}
		}

		public bool RejectCurrentSpline
		{
			get
			{
				return this.rejectCurrentSpline;
			}
			set
			{
				this.rejectCurrentSpline = value;
			}
		}

		public bool RejectTooDivergentSplines
		{
			get
			{
				return this.rejectTooDivergentSplines;
			}
			set
			{
				this.rejectTooDivergentSplines = value;
			}
		}

		public float MaxAllowedDivergenceAngle
		{
			get
			{
				return this.maxAllowedDivergenceAngle;
			}
			set
			{
				this.maxAllowedDivergenceAngle = value;
			}
		}

		public CurvySplineMoveEvent OnControlPointReached
		{
			get
			{
				return this.m_OnControlPointReached;
			}
			set
			{
				this.m_OnControlPointReached = value;
			}
		}

		public CurvySplineMoveEvent OnEndReached
		{
			get
			{
				return this.m_OnEndReached;
			}
			set
			{
				this.m_OnEndReached = value;
			}
		}

		public CurvySplineMoveEvent OnSwitch
		{
			get
			{
				return this.m_OnSwitch;
			}
			set
			{
				this.m_OnSwitch = value;
			}
		}

		public bool IsSwitching { get; private set; }

		public override float Length
		{
			get
			{
				return (!this.Spline) ? 0f : this.Spline.Length;
			}
		}

		private float SwitchProgress
		{
			get
			{
				return (!this.IsSwitching) ? 0f : Mathf.Clamp01((Time.time - this.switchStartTime) / this.switchDuration);
			}
		}

		public virtual void SwitchTo(CurvySpline destinationSpline, float destinationTf, float duration)
		{
			if (base.PlayState == CurvyController.CurvyControllerState.Stopped)
			{
				DTLog.LogError("[Curvy] Contoller can not switch when stopped. The switch call will be ignored");
			}
			else
			{
				this.switchStartTime = Time.time;
				this.switchDuration = duration;
				this.switchTarget = destinationSpline;
				this.tfOnSwitchTarget = destinationTf;
				this.directionOnSwitchTarget = base.MovementDirection;
				this.IsSwitching = true;
			}
		}

		public void FinishCurrentSwitch()
		{
			if (this.IsSwitching)
			{
				this.IsSwitching = false;
				this.Spline = this.switchTarget;
				base.RelativePosition = this.tfOnSwitchTarget;
			}
		}

		public void CancelCurrentSwitch()
		{
			if (this.IsSwitching)
			{
				this.IsSwitching = false;
			}
		}

		public static float GetAngleBetweenConnectedSplines(CurvySplineSegment before, MovementDirection movementMode, CurvySplineSegment after, bool allowMovementModeChange)
		{
			Vector3 from = before.GetTangentFast(0f) * (float)movementMode.ToInt();
			Vector3 to = after.GetTangentFast(0f) * (float)SplineController.GetPostConnectionDirection(after, movementMode, allowMovementModeChange).ToInt();
			return Vector3.Angle(from, to);
		}

		public override bool IsReady
		{
			get
			{
				return this.Spline != null && this.Spline.IsInitialized;
			}
		}

		protected override void SavePrePlayState()
		{
			this.prePlaySpline = this.Spline;
			base.SavePrePlayState();
		}

		protected override void RestorePrePlayState()
		{
			this.Spline = this.prePlaySpline;
			base.RestorePrePlayState();
		}

		protected override float RelativeToAbsolute(float relativeDistance)
		{
			return this.Spline.TFToDistance(relativeDistance, base.Clamping);
		}

		protected override float AbsoluteToRelative(float worldUnitDistance)
		{
			return this.Spline.DistanceToTF(worldUnitDistance, base.Clamping);
		}

		protected override Vector3 GetInterpolatedSourcePosition(float tf)
		{
			Vector3 position = (!this.UseCache) ? this.Spline.Interpolate(tf) : this.Spline.InterpolateFast(tf);
			return this.Spline.transform.TransformPoint(position);
		}

		protected override void GetInterpolatedSourcePosition(float tf, out Vector3 interpolatedPosition, out Vector3 tangent, out Vector3 up)
		{
			CurvySpline spline = this.Spline;
			Transform transform = spline.transform;
			if (this.UseCache)
			{
				interpolatedPosition = spline.InterpolateFast(tf);
				tangent = spline.GetTangentFast(tf);
			}
			else
			{
				interpolatedPosition = spline.Interpolate(tf);
				tangent = spline.GetTangent(tf, interpolatedPosition);
			}
			up = spline.GetOrientationUpFast(tf);
			interpolatedPosition = transform.TransformPoint(interpolatedPosition);
			tangent = transform.TransformDirection(tangent);
			up = transform.TransformDirection(up);
		}

		protected override Vector3 GetTangent(float tf)
		{
			Vector3 direction = (!this.UseCache) ? this.Spline.GetTangent(tf) : this.Spline.GetTangentFast(tf);
			return this.Spline.transform.TransformDirection(direction);
		}

		protected override Vector3 GetOrientation(float tf)
		{
			return this.Spline.transform.TransformDirection(this.Spline.GetOrientationUpFast(tf));
		}

		protected override void Advance(float speed, float deltaTime)
		{
			float distance = speed * deltaTime;
			if (this.Spline.Count != 0)
			{
				this.EventAwareMove(distance);
			}
			if (this.IsSwitching && this.switchTarget.Count > 0)
			{
				this.SimulateAdvanceOnSpline(ref this.tfOnSwitchTarget, ref this.directionOnSwitchTarget, this.switchTarget, speed * deltaTime);
				this.preAllocatedEventArgs.Set_INTERNAL(this, this.switchTarget, null, this.tfOnSwitchTarget, this.SwitchProgress, this.directionOnSwitchTarget, false);
				this.OnSwitch.Invoke(this.preAllocatedEventArgs);
				if (this.preAllocatedEventArgs.Cancel)
				{
					this.CancelCurrentSwitch();
				}
			}
		}

		protected override void SimulateAdvance(ref float tf, ref MovementDirection curyDirection, float speed, float deltaTime)
		{
			this.SimulateAdvanceOnSpline(ref tf, ref curyDirection, this.Spline, speed * deltaTime);
		}

		private void SimulateAdvanceOnSpline(ref float tf, ref MovementDirection curyDirection, CurvySpline spline, float distance)
		{
			if (spline.Count > 0)
			{
				int num = curyDirection.ToInt();
				CurvyController.MoveModeEnum moveMode = base.MoveMode;
				if (moveMode != CurvyController.MoveModeEnum.AbsolutePrecise)
				{
					if (moveMode != CurvyController.MoveModeEnum.Relative)
					{
						throw new NotSupportedException();
					}
					tf = CurvyUtility.ClampTF(tf + distance * (float)num, ref num, base.Clamping);
				}
				else
				{
					tf = spline.DistanceToTF(spline.ClampDistance(spline.TFToDistance(tf, CurvyClamping.Clamp) + distance * (float)num, ref num, base.Clamping), CurvyClamping.Clamp);
				}
				curyDirection = MovementDirectionMethods.FromInt(num);
			}
		}

		protected override void InitializedApplyDeltaTime(float deltaTime)
		{
			if (this.Spline.Dirty)
			{
				this.Spline.Refresh();
			}
			base.InitializedApplyDeltaTime(deltaTime);
			if (this.IsSwitching && this.SwitchProgress >= 1f)
			{
				this.FinishCurrentSwitch();
			}
		}

		protected override void ComputeTargetPositionAndRotation(out Vector3 targetPosition, out Vector3 targetUp, out Vector3 targetForward)
		{
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			base.ComputeTargetPositionAndRotation(out vector, out vector2, out vector3);
			Quaternion a = Quaternion.LookRotation(vector3, vector2);
			if (this.IsSwitching)
			{
				CurvySpline spline = this.Spline;
				float relativePosition = base.RelativePosition;
				this.m_Spline = this.switchTarget;
				base.RelativePosition = this.tfOnSwitchTarget;
				Vector3 b;
				Vector3 upwards;
				Vector3 forward;
				base.ComputeTargetPositionAndRotation(out b, out upwards, out forward);
				Quaternion b2 = Quaternion.LookRotation(forward, upwards);
				this.m_Spline = spline;
				base.RelativePosition = relativePosition;
				targetPosition = Vector3.LerpUnclamped(vector, b, this.SwitchProgress);
				Quaternion rotation = Quaternion.LerpUnclamped(a, b2, this.SwitchProgress);
				targetUp = rotation * Vector3.up;
				targetForward = rotation * Vector3.forward;
			}
			else
			{
				targetPosition = vector;
				targetUp = vector2;
				targetForward = vector3;
			}
		}

		private static float MovementCompatibleGetPosition(SplineController controller, CurvyPositionMode positionMode, out CurvySplineSegment controlPoint, out bool isOnControlPoint)
		{
			CurvySpline spline = controller.Spline;
			float position = controller.m_Position;
			CurvyPositionMode positionMode2 = controller.PositionMode;
			float num;
			if (positionMode2 != CurvyPositionMode.Relative)
			{
				if (positionMode2 != CurvyPositionMode.WorldUnits)
				{
					throw new NotSupportedException();
				}
				float length = controller.Length;
				num = ((controller.Clamping != CurvyClamping.Loop || !position.Approximately(length)) ? CurvyUtility.ClampDistance(position, controller.Clamping, length) : length);
			}
			else
			{
				num = ((controller.Clamping != CurvyClamping.Loop || !position.Approximately(1f)) ? CurvyUtility.ClampTF(position, controller.Clamping) : 1f);
			}
			CurvyPositionMode positionMode3 = controller.PositionMode;
			float num2;
			bool flag;
			bool flag2;
			if (positionMode3 != CurvyPositionMode.Relative)
			{
				if (positionMode3 != CurvyPositionMode.WorldUnits)
				{
					throw new NotSupportedException();
				}
				controlPoint = spline.DistanceToSegment(num, out num2, CurvyClamping.Clamp);
				flag = num2.Approximately(0f);
				flag2 = num2.Approximately(controlPoint.Length);
			}
			else
			{
				controlPoint = spline.TFToSegment(num, out num2, CurvyClamping.Clamp);
				flag = num2.Approximately(0f);
				flag2 = num2.Approximately(1f);
			}
			float result;
			if (positionMode == controller.PositionMode)
			{
				result = num;
			}
			else if (positionMode != CurvyPositionMode.Relative)
			{
				if (positionMode != CurvyPositionMode.WorldUnits)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = controlPoint.Distance + controlPoint.LocalFToDistance(num2);
			}
			else
			{
				result = spline.SegmentToTF(controlPoint, controlPoint.DistanceToLocalF(num2));
			}
			if (flag2)
			{
				controlPoint = spline.GetNextControlPoint(controlPoint);
			}
			isOnControlPoint = (flag || flag2);
			return result;
		}

		private static void MovementCompatibleSetPosition(SplineController controller, CurvyPositionMode positionMode, float position)
		{
			CurvyPositionMode positionMode2 = controller.PositionMode;
			CurvyClamping clamping = controller.Clamping;
			float num;
			if (positionMode != CurvyPositionMode.Relative)
			{
				if (positionMode != CurvyPositionMode.WorldUnits)
				{
					throw new NotSupportedException();
				}
				float length = controller.Length;
				num = ((clamping != CurvyClamping.Loop || !position.Approximately(length)) ? CurvyUtility.ClampDistance(position, clamping, length) : length);
			}
			else
			{
				num = ((clamping != CurvyClamping.Loop || !position.Approximately(1f)) ? CurvyUtility.ClampTF(position, clamping) : 1f);
			}
			if (positionMode == positionMode2)
			{
				controller.m_Position = num;
			}
			else if (positionMode != CurvyPositionMode.Relative)
			{
				if (positionMode != CurvyPositionMode.WorldUnits)
				{
					throw new ArgumentOutOfRangeException();
				}
				controller.m_Position = controller.Spline.DistanceToTF(num, controller.Clamping);
			}
			else
			{
				controller.m_Position = controller.Spline.TFToDistance(num, controller.Clamping);
			}
		}

		private void EventAwareMove(float distance)
		{
			CurvyController.MoveModeEnum moveMode = base.MoveMode;
			CurvyPositionMode positionMode;
			if (moveMode != CurvyController.MoveModeEnum.AbsolutePrecise)
			{
				if (moveMode != CurvyController.MoveModeEnum.Relative)
				{
					throw new NotSupportedException();
				}
				positionMode = CurvyPositionMode.Relative;
			}
			else
			{
				positionMode = CurvyPositionMode.WorldUnits;
			}
			float num = distance;
			bool flag = false;
			if ((base.MovementDirection == MovementDirection.Backward && base.RelativePosition.Approximately(0f)) || (base.MovementDirection == MovementDirection.Forward && base.RelativePosition.Approximately(1f)))
			{
				CurvyClamping clamping = base.Clamping;
				if (clamping != CurvyClamping.Clamp)
				{
					if (clamping == CurvyClamping.PingPong)
					{
						base.MovementDirection = base.MovementDirection.GetOpposite();
					}
				}
				else
				{
					num = 0f;
				}
			}
			int num2 = 50;
			while (!flag && num > 0f && num2-- > 0)
			{
				bool flag2;
				float num3;
				CurvySplineSegment currentControlPoint = this.GetCurrentControlPoint(out flag2, out num3, positionMode);
				CurvySplineSegment curvySplineSegment;
				if (base.MovementDirection == MovementDirection.Forward)
				{
					curvySplineSegment = this.Spline.GetNextControlPoint(currentControlPoint);
				}
				else
				{
					curvySplineSegment = ((!flag2) ? currentControlPoint : this.Spline.GetPreviousControlPoint(currentControlPoint));
				}
				if (curvySplineSegment != null && this.Spline.IsControlPointVisible(curvySplineSegment))
				{
					float num4 = Mathf.Abs(SplineController.GetControlPointPosition(curvySplineSegment, positionMode, base.MovementDirection) - num3);
					if (num4 > num)
					{
						SplineController.MovementCompatibleSetPosition(this, positionMode, num3 + num * (float)base.MovementDirection.ToInt());
						break;
					}
					num -= num4;
					this.HandleReachingNewControlPoint(curvySplineSegment, positionMode, num, ref flag);
				}
				bool flag3;
				float num5;
				CurvySplineSegment currentControlPoint2 = this.GetCurrentControlPoint(out flag3, out num5, positionMode);
				if (flag3 && currentControlPoint2.Connection && currentControlPoint2.Connection.ControlPointsList.Count > 1)
				{
					CurvySplineSegment curvySplineSegment2;
					MovementDirection movementDirection;
					switch (this.ConnectionBehavior)
					{
					case SplineControllerConnectionBehavior.CurrentSpline:
						curvySplineSegment2 = currentControlPoint2;
						movementDirection = base.MovementDirection;
						break;
					case SplineControllerConnectionBehavior.FollowUpSpline:
						curvySplineSegment2 = this.HandleFolloUpConnectionBahavior(currentControlPoint2, base.MovementDirection, out movementDirection);
						break;
					case SplineControllerConnectionBehavior.RandomSpline:
						curvySplineSegment2 = this.HandleRandomConnectionBehavior(currentControlPoint2, base.MovementDirection, out movementDirection, currentControlPoint2.Connection.ControlPointsList);
						break;
					case SplineControllerConnectionBehavior.FollowUpOtherwiseRandom:
						curvySplineSegment2 = ((!currentControlPoint2.FollowUp) ? this.HandleRandomConnectionBehavior(currentControlPoint2, base.MovementDirection, out movementDirection, currentControlPoint2.Connection.ControlPointsList) : this.HandleFolloUpConnectionBahavior(currentControlPoint2, base.MovementDirection, out movementDirection));
						break;
					case SplineControllerConnectionBehavior.Custom:
						if (this.ConnectionCustomSelector == null)
						{
							DTLog.LogError("[Curvy] You need to set a non null ConnectionCustomSelector when using SplineControllerConnectionBehavior.Custom");
							curvySplineSegment2 = currentControlPoint2;
						}
						else
						{
							curvySplineSegment2 = this.ConnectionCustomSelector.SelectConnectedControlPoint(this, currentControlPoint2.Connection, currentControlPoint2);
						}
						movementDirection = base.MovementDirection;
						break;
					default:
						throw new ArgumentOutOfRangeException();
					}
					if (curvySplineSegment2 != currentControlPoint2)
					{
						base.MovementDirection = movementDirection;
						this.HandleReachingNewControlPoint(curvySplineSegment2, positionMode, num, ref flag);
					}
				}
				bool flag4;
				float num6;
				CurvySplineSegment currentControlPoint3 = this.GetCurrentControlPoint(out flag4, out num6, positionMode);
				if (flag4)
				{
					switch (base.Clamping)
					{
					case CurvyClamping.Clamp:
						if ((base.MovementDirection == MovementDirection.Backward && currentControlPoint3 == this.Spline.FirstVisibleControlPoint) || (base.MovementDirection == MovementDirection.Forward && currentControlPoint3 == this.Spline.LastVisibleControlPoint))
						{
							num = 0f;
						}
						break;
					case CurvyClamping.Loop:
						if (!this.Spline.Closed)
						{
							if (base.MovementDirection == MovementDirection.Backward && currentControlPoint3 == this.Spline.FirstVisibleControlPoint)
							{
								this.HandleReachingNewControlPoint(this.Spline.LastVisibleControlPoint, positionMode, num, ref flag);
							}
							else if (base.MovementDirection == MovementDirection.Forward && currentControlPoint3 == this.Spline.LastVisibleControlPoint)
							{
								this.HandleReachingNewControlPoint(this.Spline.FirstVisibleControlPoint, positionMode, num, ref flag);
							}
						}
						break;
					case CurvyClamping.PingPong:
						if ((base.MovementDirection == MovementDirection.Backward && currentControlPoint3 == this.Spline.FirstVisibleControlPoint) || (base.MovementDirection == MovementDirection.Forward && currentControlPoint3 == this.Spline.LastVisibleControlPoint))
						{
							base.MovementDirection = base.MovementDirection.GetOpposite();
						}
						break;
					default:
						throw new ArgumentOutOfRangeException();
					}
				}
				bool flag5;
				CurvySplineSegment currentControlPoint4 = this.GetCurrentControlPoint(out flag5, out num3, positionMode);
				if (currentControlPoint == currentControlPoint4 && flag2 == flag5)
				{
					CurvySplineSegment curvySplineSegment3 = null;
					for (int i = 0; i < this.Spline.Count; i++)
					{
						if (this.Spline[i].Length == 0f)
						{
							curvySplineSegment3 = this.Spline[i];
							break;
						}
					}
					if (curvySplineSegment3 != null)
					{
						DTLog.LogError(string.Format("[Curvy] Spline Controller '{0}' is stuck at control point '{1}'. This is probably caused by the presence of a segment with a length of 0. Please remove control point '{2}' to proceed", base.name, currentControlPoint4, curvySplineSegment3));
					}
					else
					{
						DTLog.LogError(string.Format("[Curvy] Spline Controller '{0}' is stuck at control point '{1}'. Please raise a bug report", base.name, currentControlPoint4));
					}
					break;
				}
			}
			if (num2 <= 0)
			{
				DTLog.LogError(string.Format("[Curvy] Unexpected behavior in Spline Controller '{0}'. Please raise a Bug Report.", base.name));
			}
		}

		private CurvySplineSegment GetCurrentControlPoint(out bool isOnControlPoint, out float position, CurvyPositionMode positionMode)
		{
			CurvySplineSegment result;
			position = SplineController.MovementCompatibleGetPosition(this, positionMode, out result, out isOnControlPoint);
			return result;
		}

		private void HandleReachingNewControlPoint(CurvySplineSegment newControlPoint, CurvyPositionMode positionMode, float currentDelta, ref bool cancelMovement)
		{
			this.Spline = newControlPoint.Spline;
			float controlPointPosition = SplineController.GetControlPointPosition(newControlPoint, positionMode, base.MovementDirection);
			SplineController.MovementCompatibleSetPosition(this, positionMode, controlPointPosition);
			bool usingWorldUnits;
			if (positionMode != CurvyPositionMode.Relative)
			{
				if (positionMode != CurvyPositionMode.WorldUnits)
				{
					throw new ArgumentOutOfRangeException();
				}
				usingWorldUnits = true;
			}
			else
			{
				usingWorldUnits = false;
			}
			this.preAllocatedEventArgs.Set_INTERNAL(this, this.Spline, newControlPoint, controlPointPosition, currentDelta, base.MovementDirection, usingWorldUnits);
			this.OnControlPointReached.Invoke(this.preAllocatedEventArgs);
			if (this.preAllocatedEventArgs.Spline.FirstVisibleControlPoint == this.preAllocatedEventArgs.ControlPoint || this.preAllocatedEventArgs.Spline.LastVisibleControlPoint == this.preAllocatedEventArgs.ControlPoint)
			{
				this.OnEndReached.Invoke(this.preAllocatedEventArgs);
			}
			cancelMovement |= this.preAllocatedEventArgs.Cancel;
		}

		private CurvySplineSegment HandleRandomConnectionBehavior(CurvySplineSegment currentControlPoint, MovementDirection currentDirection, out MovementDirection newDirection, ReadOnlyCollection<CurvySplineSegment> connectedControlPoints)
		{
			List<CurvySplineSegment> list = new List<CurvySplineSegment>(connectedControlPoints.Count);
			for (int i = 0; i < connectedControlPoints.Count; i++)
			{
				CurvySplineSegment curvySplineSegment = connectedControlPoints[i];
				if (!this.RejectCurrentSpline || !(curvySplineSegment == currentControlPoint))
				{
					if (!this.RejectTooDivergentSplines || SplineController.GetAngleBetweenConnectedSplines(currentControlPoint, currentDirection, curvySplineSegment, this.AllowDirectionChange) <= this.MaxAllowedDivergenceAngle)
					{
						list.Add(curvySplineSegment);
					}
				}
			}
			CurvySplineSegment curvySplineSegment2 = (list.Count != 0) ? list[UnityEngine.Random.Range(0, list.Count)] : currentControlPoint;
			newDirection = SplineController.GetPostConnectionDirection(curvySplineSegment2, currentDirection, this.AllowDirectionChange);
			return curvySplineSegment2;
		}

		private static MovementDirection GetPostConnectionDirection(CurvySplineSegment connectedControlPoint, MovementDirection currentDirection, bool directionChangeAllowed)
		{
			return (!directionChangeAllowed || connectedControlPoint.Spline.Closed) ? currentDirection : SplineController.HeadingToDirection(ConnectionHeadingEnum.Auto, connectedControlPoint, currentDirection);
		}

		private CurvySplineSegment HandleFolloUpConnectionBahavior(CurvySplineSegment currentControlPoint, MovementDirection currentDirection, out MovementDirection newDirection)
		{
			CurvySplineSegment result = (!currentControlPoint.FollowUp) ? currentControlPoint : currentControlPoint.FollowUp;
			newDirection = ((!this.AllowDirectionChange || !currentControlPoint.FollowUp) ? currentDirection : SplineController.HeadingToDirection(currentControlPoint.FollowUpHeading, currentControlPoint.FollowUp, currentDirection));
			return result;
		}

		private static MovementDirection HeadingToDirection(ConnectionHeadingEnum heading, CurvySplineSegment controlPoint, MovementDirection currentDirection)
		{
			ConnectionHeadingEnum connectionHeadingEnum = heading.ResolveAuto(controlPoint);
			MovementDirection result;
			switch (connectionHeadingEnum + 1)
			{
			case ConnectionHeadingEnum.Sharp:
				result = MovementDirection.Backward;
				break;
			case ConnectionHeadingEnum.Plus:
				result = currentDirection;
				break;
			case ConnectionHeadingEnum.Auto:
				result = MovementDirection.Forward;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		private static float GetControlPointPosition(CurvySplineSegment controlPoint, CurvyPositionMode positionMode, MovementDirection movementDirection)
		{
			CurvySpline spline = controlPoint.Spline;
			float num;
			if (positionMode != CurvyPositionMode.Relative)
			{
				if (positionMode != CurvyPositionMode.WorldUnits)
				{
					throw new ArgumentOutOfRangeException();
				}
				num = controlPoint.Distance;
			}
			else
			{
				num = spline.SegmentToTF(controlPoint);
			}
			float num2;
			if (positionMode != CurvyPositionMode.Relative)
			{
				if (positionMode != CurvyPositionMode.WorldUnits)
				{
					throw new ArgumentOutOfRangeException();
				}
				num2 = spline.Length;
			}
			else
			{
				num2 = 1f;
			}
			return (movementDirection != MovementDirection.Forward || !num.Approximately(0f) || !spline.Closed) ? num : num2;
		}

		private bool ShowRandomConnectionOptions
		{
			get
			{
				return this.ConnectionBehavior == SplineControllerConnectionBehavior.FollowUpOtherwiseRandom || this.ConnectionBehavior == SplineControllerConnectionBehavior.RandomSpline;
			}
		}

		[Section("General", true, false, 100, Sort = 0)]
		[FieldCondition("m_Spline", null, false, ActionAttribute.ActionEnum.ShowError, "Missing source Spline", ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		protected CurvySpline m_Spline;

		[SerializeField]
		[Tooltip("Whether spline's cache data should be used. Set this to true to gain performance if precision is not required.")]
		private bool m_UseCache;

		[Section("Connections handling", true, false, 100, Sort = 250, HelpURL = "https://curvyeditor.com/doclink/curvycontroller_move")]
		[SerializeField]
		[Label("At connection, use", "What spline should the controller use when reaching a Connection")]
		private SplineControllerConnectionBehavior connectionBehavior;

		[SerializeField]
		[Label("Allow direction change", "When true, the controller will modify its direction to best fit the connected spline")]
		private bool allowDirectionChange = true;

		[SerializeField]
		[Label("Reject current spline", "Whether the current spline should be excluded from the randomly selected splines")]
		[FieldCondition("ShowRandomConnectionOptions", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		private bool rejectCurrentSpline = true;

		[SerializeField]
		[Label("Reject divergent splines", "Whether splines that diverge from the current spline with more than a specific angle should be excluded from the randomly selected splines")]
		[FieldCondition("ShowRandomConnectionOptions", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		private bool rejectTooDivergentSplines;

		[SerializeField]
		[Label("Max allowed angle", "Maximum allowed divergence angle in degrees")]
		[Range(0f, 180f)]
		private float maxAllowedDivergenceAngle = 90f;

		[SerializeField]
		[Label("Custom Selector", "A custom logic to select which connected spline to follow. Select a Script inheriting from SplineControllerConnectionBehavior")]
		[FieldCondition("connectionBehavior", SplineControllerConnectionBehavior.Custom, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[FieldCondition("connectionCustomSelector", null, false, ActionAttribute.ActionEnum.ShowWarning, "Missing custom selector", ActionAttribute.ActionPositionEnum.Below)]
		private ConnectedControlPointsSelector connectionCustomSelector;

		[Section("Events", false, false, 1000, HelpURL = "https://curvyeditor.com/doclink/splinecontroller_events")]
		[SerializeField]
		private CurvySplineMoveEvent m_OnControlPointReached;

		[SerializeField]
		private CurvySplineMoveEvent m_OnEndReached;

		[SerializeField]
		private CurvySplineMoveEvent m_OnSwitch;

		private const float StepSize = 0.002f;

		private CurvySpline prePlaySpline;

		private float switchStartTime;

		private float switchDuration;

		private CurvySpline switchTarget;

		private float tfOnSwitchTarget;

		private MovementDirection directionOnSwitchTarget;

		private readonly CurvySplineMoveEventArgs preAllocatedEventArgs;
	}
}
