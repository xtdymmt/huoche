// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvySplineSegment
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace FluffyUnderware.Curvy
{
	[ExecuteInEditMode]
	[HelpURL("https://curvyeditor.com/doclink/curvysplinesegment")]
	public class CurvySplineSegment : MonoBehaviour, IPoolable
	{
		public bool AutoBakeOrientation
		{
			get
			{
				return this.m_AutoBakeOrientation;
			}
			set
			{
				if (this.m_AutoBakeOrientation != value)
				{
					this.m_AutoBakeOrientation = value;
				}
			}
		}

		public bool SerializedOrientationAnchor
		{
			get
			{
				return this.m_OrientationAnchor;
			}
			set
			{
				if (this.m_OrientationAnchor != value)
				{
					this.m_OrientationAnchor = value;
					this.Spline.SetDirty(this, SplineDirtyingType.OrientationOnly);
					this.Spline.InvalidateControlPointsRelationshipCacheINTERNAL();
				}
			}
		}

		public CurvyOrientationSwirl Swirl
		{
			get
			{
				return this.m_Swirl;
			}
			set
			{
				if (this.m_Swirl != value)
				{
					this.m_Swirl = value;
					this.Spline.SetDirty(this, SplineDirtyingType.OrientationOnly);
				}
			}
		}

		public float SwirlTurns
		{
			get
			{
				return this.m_SwirlTurns;
			}
			set
			{
				if (this.m_SwirlTurns != value)
				{
					this.m_SwirlTurns = value;
					this.Spline.SetDirty(this, SplineDirtyingType.OrientationOnly);
				}
			}
		}

		public Vector3 HandleIn
		{
			get
			{
				return this.m_HandleIn;
			}
			set
			{
				if (this.m_HandleIn != value)
				{
					this.m_HandleIn = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public Vector3 HandleOut
		{
			get
			{
				return this.m_HandleOut;
			}
			set
			{
				if (this.m_HandleOut != value)
				{
					this.m_HandleOut = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public Vector3 HandleInPosition
		{
			get
			{
				return base.transform.position + this.Spline.transform.rotation * this.HandleIn;
			}
			set
			{
				this.HandleIn = this.Spline.transform.InverseTransformDirection(value - base.transform.position);
			}
		}

		public Vector3 HandleOutPosition
		{
			get
			{
				return base.transform.position + this.Spline.transform.rotation * this.HandleOut;
			}
			set
			{
				this.HandleOut = this.Spline.transform.InverseTransformDirection(value - base.transform.position);
			}
		}

		public bool AutoHandles
		{
			get
			{
				return this.m_AutoHandles;
			}
			set
			{
				if (this.SetAutoHandles(value))
				{
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float AutoHandleDistance
		{
			get
			{
				return this.m_AutoHandleDistance;
			}
			set
			{
				if (this.m_AutoHandleDistance != value)
				{
					float num = Mathf.Clamp01(value);
					if (this.m_AutoHandleDistance != num)
					{
						this.m_AutoHandleDistance = num;
						this.Spline.SetDirty(this, SplineDirtyingType.Everything);
					}
				}
			}
		}

		public bool SynchronizeTCB
		{
			get
			{
				return this.m_SynchronizeTCB;
			}
			set
			{
				if (this.m_SynchronizeTCB != value)
				{
					this.m_SynchronizeTCB = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public bool OverrideGlobalTension
		{
			get
			{
				return this.m_OverrideGlobalTension;
			}
			set
			{
				if (this.m_OverrideGlobalTension != value)
				{
					this.m_OverrideGlobalTension = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public bool OverrideGlobalContinuity
		{
			get
			{
				return this.m_OverrideGlobalContinuity;
			}
			set
			{
				if (this.m_OverrideGlobalContinuity != value)
				{
					this.m_OverrideGlobalContinuity = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public bool OverrideGlobalBias
		{
			get
			{
				return this.m_OverrideGlobalBias;
			}
			set
			{
				if (this.m_OverrideGlobalBias != value)
				{
					this.m_OverrideGlobalBias = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float StartTension
		{
			get
			{
				return this.m_StartTension;
			}
			set
			{
				if (this.m_StartTension != value)
				{
					this.m_StartTension = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float StartContinuity
		{
			get
			{
				return this.m_StartContinuity;
			}
			set
			{
				if (this.m_StartContinuity != value)
				{
					this.m_StartContinuity = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float StartBias
		{
			get
			{
				return this.m_StartBias;
			}
			set
			{
				if (this.m_StartBias != value)
				{
					this.m_StartBias = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float EndTension
		{
			get
			{
				return this.m_EndTension;
			}
			set
			{
				if (this.m_EndTension != value)
				{
					this.m_EndTension = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float EndContinuity
		{
			get
			{
				return this.m_EndContinuity;
			}
			set
			{
				if (this.m_EndContinuity != value)
				{
					this.m_EndContinuity = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public float EndBias
		{
			get
			{
				return this.m_EndBias;
			}
			set
			{
				if (this.m_EndBias != value)
				{
					this.m_EndBias = value;
					this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public CurvySplineSegment FollowUp
		{
			get
			{
				return this.m_FollowUp;
			}
			private set
			{
				if (this.m_FollowUp != value)
				{
					this.m_FollowUp = value;
					if (this.mSpline != null)
					{
						this.mSpline.SetDirty(this, SplineDirtyingType.Everything);
					}
				}
			}
		}

		public ConnectionHeadingEnum FollowUpHeading
		{
			get
			{
				return this.m_FollowUpHeading;
			}
			set
			{
				if (this.m_FollowUpHeading != value)
				{
					this.m_FollowUpHeading = value;
					if (this.mSpline != null)
					{
						this.mSpline.SetDirty(this, SplineDirtyingType.Everything);
					}
				}
			}
		}

		public bool ConnectionSyncPosition
		{
			get
			{
				return this.m_ConnectionSyncPosition;
			}
			set
			{
				if (this.m_ConnectionSyncPosition != value)
				{
					this.m_ConnectionSyncPosition = value;
				}
			}
		}

		public bool ConnectionSyncRotation
		{
			get
			{
				return this.m_ConnectionSyncRotation;
			}
			set
			{
				if (this.m_ConnectionSyncRotation != value)
				{
					this.m_ConnectionSyncRotation = value;
				}
			}
		}

		public CurvyConnection Connection
		{
			get
			{
				return this.m_Connection;
			}
			internal set
			{
				if (this.SetConnection(value) && this.mSpline != null)
				{
					this.mSpline.SetDirty(this, SplineDirtyingType.Everything);
				}
			}
		}

		public int CacheSize
		{
			get
			{
				return this.cacheSize;
			}
			private set
			{
				this.cacheSize = value;
			}
		}

		public Bounds Bounds
		{
			get
			{
				if (this.mBounds == null)
				{
					Bounds value;
					if (this.Approximation.Length == 0)
					{
						value = new Bounds(base.transform.position, Vector3.zero);
					}
					else
					{
						Matrix4x4 localToWorldMatrix = this.Spline.transform.localToWorldMatrix;
						value = new Bounds(localToWorldMatrix.MultiplyPoint3x4(this.Approximation[0]), Vector3.zero);
						int num = this.Approximation.Length;
						for (int i = 1; i < num; i++)
						{
							value.Encapsulate(localToWorldMatrix.MultiplyPoint(this.Approximation[i]));
						}
					}
					this.mBounds = new Bounds?(value);
				}
				return this.mBounds.Value;
			}
		}

		public float Length { get; private set; }

		public float Distance { get; internal set; }

		public float TF
		{
			get
			{
				return this.LocalFToTF(0f);
			}
		}

		public bool IsFirstControlPoint
		{
			get
			{
				return this.Spline.GetControlPointIndex(this) == 0;
			}
		}

		public bool IsLastControlPoint
		{
			get
			{
				return (int)this.Spline.GetControlPointIndex(this) == this.Spline.ControlPointCount - 1;
			}
		}

		public List<Component> MetaData
		{
			get
			{
				if (this.mMetaData == null)
				{
					this.ReloadMetaData();
				}
				return this.mMetaData;
			}
		}

		public CurvySpline Spline
		{
			get
			{
				return this.mSpline;
			}
		}

		public bool HasUnprocessedLocalPosition
		{
			get
			{
				return base.transform.localPosition != this.lastProcessedLocalPosition;
			}
		}

		public bool HasUnprocessedLocalOrientation
		{
			get
			{
				return base.transform.localRotation.DifferentOrientation(this.lastProcessedLocalRotation);
			}
		}

		public bool OrientatinInfluencesSpline
		{
			get
			{
				return this.mSpline != null && (this.mSpline.Orientation == CurvyOrientation.Static || this.mSpline.IsControlPointAnOrientationAnchor(this));
			}
		}

		public void SetBezierHandleIn(Vector3 position, Space space = Space.Self, CurvyBezierModeEnum mode = CurvyBezierModeEnum.None)
		{
			if (space == Space.Self)
			{
				this.HandleIn = position;
			}
			else
			{
				this.HandleInPosition = position;
			}
			bool flag = (mode & CurvyBezierModeEnum.Direction) == CurvyBezierModeEnum.Direction;
			bool flag2 = (mode & CurvyBezierModeEnum.Length) == CurvyBezierModeEnum.Length;
			bool flag3 = (mode & CurvyBezierModeEnum.Connections) == CurvyBezierModeEnum.Connections;
			if (flag)
			{
				this.HandleOut = this.HandleOut.magnitude * (this.HandleIn.normalized * -1f);
			}
			if (flag2)
			{
				this.HandleOut = this.HandleIn.magnitude * ((!(this.HandleOut == Vector3.zero)) ? this.HandleOut.normalized : (this.HandleIn.normalized * -1f));
			}
			if (this.Connection && flag3 && (flag || flag2))
			{
				ReadOnlyCollection<CurvySplineSegment> controlPointsList = this.Connection.ControlPointsList;
				for (int i = 0; i < controlPointsList.Count; i++)
				{
					CurvySplineSegment curvySplineSegment = controlPointsList[i];
					if (!(curvySplineSegment == this))
					{
						if (curvySplineSegment.HandleIn.magnitude == 0f)
						{
							curvySplineSegment.HandleIn = this.HandleIn;
						}
						if (flag)
						{
							curvySplineSegment.SetBezierHandleIn(curvySplineSegment.HandleIn.magnitude * this.HandleIn.normalized * Mathf.Sign(Vector3.Dot(this.HandleIn, curvySplineSegment.HandleIn)), Space.Self, CurvyBezierModeEnum.Direction);
						}
						if (flag2)
						{
							curvySplineSegment.SetBezierHandleIn(curvySplineSegment.HandleIn.normalized * this.HandleIn.magnitude, Space.Self, CurvyBezierModeEnum.Length);
						}
					}
				}
			}
		}

		public void SetBezierHandleOut(Vector3 position, Space space = Space.Self, CurvyBezierModeEnum mode = CurvyBezierModeEnum.None)
		{
			if (space == Space.Self)
			{
				this.HandleOut = position;
			}
			else
			{
				this.HandleOutPosition = position;
			}
			bool flag = (mode & CurvyBezierModeEnum.Direction) == CurvyBezierModeEnum.Direction;
			bool flag2 = (mode & CurvyBezierModeEnum.Length) == CurvyBezierModeEnum.Length;
			bool flag3 = (mode & CurvyBezierModeEnum.Connections) == CurvyBezierModeEnum.Connections;
			if (flag)
			{
				this.HandleIn = this.HandleIn.magnitude * (this.HandleOut.normalized * -1f);
			}
			if (flag2)
			{
				this.HandleIn = this.HandleOut.magnitude * ((!(this.HandleIn == Vector3.zero)) ? this.HandleIn.normalized : (this.HandleOut.normalized * -1f));
			}
			if (this.Connection && flag3 && (flag || flag2))
			{
				for (int i = 0; i < this.Connection.ControlPointsList.Count; i++)
				{
					CurvySplineSegment curvySplineSegment = this.Connection.ControlPointsList[i];
					if (!(curvySplineSegment == this))
					{
						if (curvySplineSegment.HandleOut.magnitude == 0f)
						{
							curvySplineSegment.HandleOut = this.HandleOut;
						}
						if (flag)
						{
							curvySplineSegment.SetBezierHandleOut(curvySplineSegment.HandleOut.magnitude * this.HandleOut.normalized * Mathf.Sign(Vector3.Dot(this.HandleOut, curvySplineSegment.HandleOut)), Space.Self, CurvyBezierModeEnum.Direction);
						}
						if (flag2)
						{
							curvySplineSegment.SetBezierHandleOut(curvySplineSegment.HandleOut.normalized * this.HandleOut.magnitude, Space.Self, CurvyBezierModeEnum.Length);
						}
					}
				}
			}
		}

		public void SetBezierHandles(float distanceFrag = -1f, bool setIn = true, bool setOut = true, bool noDirtying = false)
		{
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			if (distanceFrag == -1f)
			{
				distanceFrag = this.AutoHandleDistance;
			}
			if (distanceFrag > 0f)
			{
				CurvySpline spline = this.Spline;
				Transform transform = base.transform;
				CurvySplineSegment nextControlPoint = spline.GetNextControlPoint(this);
				Transform transform2 = (!nextControlPoint) ? transform : nextControlPoint.transform;
				CurvySplineSegment previousControlPoint = spline.GetPreviousControlPoint(this);
				Transform transform3 = (!previousControlPoint) ? transform : previousControlPoint.transform;
				Vector3 localPosition = transform.localPosition;
				Vector3 p = transform3.localPosition - localPosition;
				Vector3 n = transform2.localPosition - localPosition;
				this.SetBezierHandles(distanceFrag, p, n, setIn, setOut, noDirtying);
			}
			else
			{
				if (setIn)
				{
					if (noDirtying)
					{
						this.m_HandleIn = zero;
					}
					else
					{
						this.HandleIn = zero;
					}
				}
				if (setOut)
				{
					if (noDirtying)
					{
						this.m_HandleOut = zero2;
					}
					else
					{
						this.HandleOut = zero2;
					}
				}
			}
		}

		public void SetBezierHandles(float distanceFrag, Vector3 p, Vector3 n, bool setIn = true, bool setOut = true, bool noDirtying = false)
		{
			float magnitude = p.magnitude;
			float magnitude2 = n.magnitude;
			Vector3 handleIn = Vector3.zero;
			Vector3 handleOut = Vector3.zero;
			if (magnitude != 0f || magnitude2 != 0f)
			{
				Vector3 normalized = (magnitude / magnitude2 * n - p).normalized;
				handleIn = -normalized * (magnitude * distanceFrag);
				handleOut = normalized * (magnitude2 * distanceFrag);
			}
			if (setIn)
			{
				if (noDirtying)
				{
					this.m_HandleIn = handleIn;
				}
				else
				{
					this.HandleIn = handleIn;
				}
			}
			if (setOut)
			{
				if (noDirtying)
				{
					this.m_HandleOut = handleOut;
				}
				else
				{
					this.HandleOut = handleOut;
				}
			}
		}

		public void ReloadMetaData()
		{
			this.mMetaData = new List<Component>();
			base.GetComponents(typeof(ICurvyMetadata), this.mMetaData);
		}

		public void SetFollowUp(CurvySplineSegment target, ConnectionHeadingEnum heading = ConnectionHeadingEnum.Auto)
		{
			if (target == null || this.Spline.CanControlPointHaveFollowUp(this))
			{
				this.FollowUp = target;
				this.FollowUpHeading = heading;
			}
			else
			{
				DTLog.LogError("[Curvy] Setting a Follow-Up to a Control Point that can't have one");
			}
		}

		public void Disconnect()
		{
			if (this.Connection)
			{
				this.Connection.RemoveControlPoint(this, true);
			}
			this.ResetConnectionRelatedData();
		}

		public void ResetConnectionRelatedData()
		{
			this.Connection = null;
			this.FollowUp = null;
			this.FollowUpHeading = ConnectionHeadingEnum.Auto;
			this.ConnectionSyncPosition = false;
			this.ConnectionSyncRotation = false;
		}

		public Vector3 Interpolate(float localF)
		{
			return this.Interpolate(localF, this.Spline.Interpolation);
		}

		public Vector3 Interpolate(float localF, CurvyInterpolation interpolation)
		{
			switch (interpolation)
			{
			case CurvyInterpolation.Linear:
				return this.interpolateLinear(localF);
			case CurvyInterpolation.CatmullRom:
				return this.interpolateCatmull(localF);
			case CurvyInterpolation.TCB:
				return this.interpolateTCB(localF);
			case CurvyInterpolation.Bezier:
				return this.interpolateBezier(localF);
			default:
				DTLog.LogError("[Curvy] Invalid interpolation value " + interpolation);
				return Vector3.zero;
			}
		}

		public Vector3 InterpolateFast(float localF)
		{
			float t;
			int approximationIndexINTERNAL = this.getApproximationIndexINTERNAL(localF, out t);
			int num = Mathf.Min(this.Approximation.Length - 1, approximationIndexINTERNAL + 1);
			return Vector3.LerpUnclamped(this.Approximation[approximationIndexINTERNAL], this.Approximation[num], t);
		}

		public Component GetMetaData(Type type, bool autoCreate = false)
		{
			List<Component> metaData = this.MetaData;
			if (metaData != null && type.IsSubclassOf(typeof(Component)) && typeof(ICurvyMetadata).IsAssignableFrom(type))
			{
				for (int i = 0; i < metaData.Count; i++)
				{
					if (metaData[i] != null && metaData[i].GetType() == type)
					{
						return metaData[i];
					}
				}
			}
			Component component = null;
			if (autoCreate)
			{
				component = base.gameObject.AddComponent(type);
				this.MetaData.Add(component);
			}
			return component;
		}

		public T GetMetadata<T>(bool autoCreate = false) where T : Component, ICurvyMetadata
		{
			return (T)((object)this.GetMetaData(typeof(T), autoCreate));
		}

		public U InterpolateMetadata<T, U>(float f) where T : Component, ICurvyInterpolatableMetadata<U>
		{
			T metadata = this.GetMetadata<T>(false);
			if (metadata != null)
			{
				CurvySplineSegment nextControlPointUsingFollowUp = this.Spline.GetNextControlPointUsingFollowUp(this);
				ICurvyInterpolatableMetadata<U> b = null;
				if (nextControlPointUsingFollowUp)
				{
					b = nextControlPointUsingFollowUp.GetMetadata<T>(false);
				}
				return metadata.Interpolate(b, f);
			}
			return default(U);
		}

		public object InterpolateMetadata(Type type, float f)
		{
			ICurvyInterpolatableMetadata curvyInterpolatableMetadata = this.GetMetaData(type, false) as ICurvyInterpolatableMetadata;
			if (curvyInterpolatableMetadata != null)
			{
				CurvySplineSegment nextControlPointUsingFollowUp = this.Spline.GetNextControlPointUsingFollowUp(this);
				if (nextControlPointUsingFollowUp)
				{
					ICurvyInterpolatableMetadata curvyInterpolatableMetadata2 = nextControlPointUsingFollowUp.GetMetaData(type, false) as ICurvyInterpolatableMetadata;
					if (curvyInterpolatableMetadata2 != null)
					{
						return curvyInterpolatableMetadata.InterpolateObject(curvyInterpolatableMetadata2, f);
					}
				}
			}
			return null;
		}

		public void DeleteMetadata()
		{
			List<Component> metaData = this.MetaData;
			for (int i = metaData.Count - 1; i >= 0; i--)
			{
				metaData[i].Destroy();
			}
		}

		public Vector3 InterpolateScale(float localF)
		{
			CurvySplineSegment nextControlPoint = this.Spline.GetNextControlPoint(this);
			return (!nextControlPoint) ? base.transform.lossyScale : Vector3.Lerp(base.transform.lossyScale, nextControlPoint.transform.lossyScale, localF);
		}

		public Vector3 GetTangent(float localF)
		{
			localF = Mathf.Clamp01(localF);
			Vector3 position = this.Interpolate(localF);
			return this.GetTangent(localF, position);
		}

		public Vector3 GetTangent(float localF, Vector3 position)
		{
			CurvySpline spline = this.Spline;
			int num = 2;
			float num2;
			Vector3 vector;
			for (;;)
			{
				num2 = localF + 0.01f;
				if (num2 > 1f)
				{
					CurvySplineSegment nextSegment = spline.GetNextSegment(this);
					if (!nextSegment)
					{
						break;
					}
					vector = nextSegment.Interpolate(num2 - 1f);
				}
				else
				{
					vector = this.Interpolate(num2);
				}
				localF += 0.01f;
				if (!(vector == position) || --num <= 0)
				{
					goto IL_94;
				}
			}
			num2 = localF - 0.01f;
			return (position - this.Interpolate(num2)).normalized;
			IL_94:
			return (vector - position).normalized;
		}

		public Vector3 GetTangentFast(float localF)
		{
			float t;
			int approximationIndexINTERNAL = this.getApproximationIndexINTERNAL(localF, out t);
			int num = Mathf.Min(this.ApproximationT.Length - 1, approximationIndexINTERNAL + 1);
			return Vector3.SlerpUnclamped(this.ApproximationT[approximationIndexINTERNAL], this.ApproximationT[num], t);
		}

		public Quaternion GetOrientationFast(float localF)
		{
			return this.GetOrientationFast(localF, false);
		}

		public Quaternion GetOrientationFast(float localF, bool inverse)
		{
			Vector3 vector = this.GetTangentFast(localF);
			if (vector != Vector3.zero)
			{
				if (inverse)
				{
					vector *= -1f;
				}
				return Quaternion.LookRotation(vector, this.GetOrientationUpFast(localF));
			}
			return Quaternion.identity;
		}

		public Vector3 GetOrientationUpFast(float localF)
		{
			float t;
			int approximationIndexINTERNAL = this.getApproximationIndexINTERNAL(localF, out t);
			int num = Mathf.Min(this.ApproximationUp.Length - 1, approximationIndexINTERNAL + 1);
			return Vector3.SlerpUnclamped(this.ApproximationUp[approximationIndexINTERNAL], this.ApproximationUp[num], t);
		}

		public float GetNearestPointF(Vector3 p)
		{
			int num = this.CacheSize + 1;
			float num2 = float.MaxValue;
			int num3 = 0;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector;
				vector.x = this.Approximation[i].x - p.x;
				vector.y = this.Approximation[i].y - p.y;
				vector.z = this.Approximation[i].z - p.z;
				float num4 = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
				if (num4 <= num2)
				{
					num2 = num4;
					num3 = i;
				}
			}
			int num5 = (num3 <= 0) ? -1 : (num3 - 1);
			int num6 = (num3 >= this.CacheSize) ? -1 : (num3 + 1);
			float num7 = 0f;
			float num8 = 0f;
			float num9 = float.MaxValue;
			float num10 = float.MaxValue;
			if (num5 > -1)
			{
				num9 = DTMath.LinePointDistanceSqr(this.Approximation[num5], this.Approximation[num3], p, out num7);
			}
			if (num6 > -1)
			{
				num10 = DTMath.LinePointDistanceSqr(this.Approximation[num3], this.Approximation[num6], p, out num8);
			}
			if (num9 < num10)
			{
				return this.getApproximationLocalF(num5) + num7 * this.mStepSize;
			}
			return this.getApproximationLocalF(num3) + num8 * this.mStepSize;
		}

		public float DistanceToLocalF(float localDistance)
		{
			localDistance = Mathf.Clamp(localDistance, 0f, this.Length);
			if (this.ApproximationDistances.Length <= 1 || localDistance == 0f)
			{
				return 0f;
			}
			if (Mathf.Approximately(localDistance, this.Length))
			{
				return 1f;
			}
			int num = Mathf.Min(this.ApproximationDistances.Length - 1, this.mCacheLastDistanceToLocalFIndex);
			if (this.ApproximationDistances[num] < localDistance)
			{
				num = this.ApproximationDistances.Length - 1;
			}
			while (this.ApproximationDistances[num] > localDistance)
			{
				num--;
			}
			this.mCacheLastDistanceToLocalFIndex = num + 1;
			float num2 = (localDistance - this.ApproximationDistances[num]) / (this.ApproximationDistances[num + 1] - this.ApproximationDistances[num]);
			float approximationLocalF = this.getApproximationLocalF(num);
			float approximationLocalF2 = this.getApproximationLocalF(num + 1);
			return approximationLocalF + (approximationLocalF2 - approximationLocalF) * num2;
		}

		public float LocalFToDistance(float localF)
		{
			localF = Mathf.Clamp01(localF);
			if (this.ApproximationDistances.Length <= 1 || localF == 0f)
			{
				return 0f;
			}
			if (Mathf.Approximately(localF, 1f))
			{
				return this.Length;
			}
			float num;
			int approximationIndexINTERNAL = this.getApproximationIndexINTERNAL(localF, out num);
			float num2 = this.ApproximationDistances[approximationIndexINTERNAL + 1] - this.ApproximationDistances[approximationIndexINTERNAL];
			return this.ApproximationDistances[approximationIndexINTERNAL] + num2 * num;
		}

		public float LocalFToTF(float localF)
		{
			return this.Spline.SegmentToTF(this, localF);
		}

		public override string ToString()
		{
			if (this.Spline != null)
			{
				return this.Spline.name + "." + base.name;
			}
			return base.ToString();
		}

		public void BakeOrientationToTransform()
		{
			Quaternion orientationFast = this.GetOrientationFast(0f);
			if (base.transform.localRotation.DifferentOrientation(orientationFast))
			{
				this.SetLocalRotation(orientationFast);
			}
		}

		public int getApproximationIndexINTERNAL(float localF, out float frag)
		{
			localF = Mathf.Clamp01(localF);
			if (localF == 1f)
			{
				frag = 1f;
				return Mathf.Max(0, this.Approximation.Length - 2);
			}
			float num = localF / this.mStepSize;
			int num2 = (int)num;
			frag = num - (float)num2;
			return num2;
		}

		public void LinkToSpline(CurvySpline spline)
		{
			this.mSpline = spline;
		}

		public void UnlinkFromSpline()
		{
			this.mSpline = null;
		}

		public void SetLocalPosition(Vector3 newPosition)
		{
			Transform transform = base.transform;
			if (transform.localPosition != newPosition)
			{
				transform.localPosition = newPosition;
				this.Spline.SetDirtyPartial(this, SplineDirtyingType.Everything);
				if ((this.ConnectionSyncPosition || this.ConnectionSyncRotation) && this.Connection != null)
				{
					this.Connection.SetSynchronisationPositionAndRotation((!this.ConnectionSyncPosition) ? this.Connection.transform.position : transform.position, (!this.ConnectionSyncRotation) ? this.Connection.transform.rotation : transform.rotation);
				}
			}
		}

		public void SetPosition(Vector3 value)
		{
			Transform transform = base.transform;
			if (transform.position != value)
			{
				transform.position = value;
				this.Spline.SetDirtyPartial(this, SplineDirtyingType.Everything);
				if ((this.ConnectionSyncPosition || this.ConnectionSyncRotation) && this.Connection != null)
				{
					this.Connection.SetSynchronisationPositionAndRotation((!this.ConnectionSyncPosition) ? this.Connection.transform.position : transform.position, (!this.ConnectionSyncRotation) ? this.Connection.transform.rotation : transform.rotation);
				}
			}
		}

		public void SetLocalRotation(Quaternion value)
		{
			Transform transform = base.transform;
			if (transform.localRotation != value)
			{
				transform.localRotation = value;
				if (this.OrientatinInfluencesSpline)
				{
					this.Spline.SetDirtyPartial(this, SplineDirtyingType.OrientationOnly);
				}
				if ((this.ConnectionSyncPosition || this.ConnectionSyncRotation) && this.Connection != null)
				{
					this.Connection.SetSynchronisationPositionAndRotation((!this.ConnectionSyncPosition) ? this.Connection.transform.position : transform.position, (!this.ConnectionSyncRotation) ? this.Connection.transform.rotation : transform.rotation);
				}
			}
		}

		public void SetRotation(Quaternion value)
		{
			Transform transform = base.transform;
			if (transform.rotation != value)
			{
				transform.rotation = value;
				if (this.OrientatinInfluencesSpline)
				{
					this.Spline.SetDirtyPartial(this, SplineDirtyingType.OrientationOnly);
				}
				if ((this.ConnectionSyncPosition || this.ConnectionSyncRotation) && this.Connection != null)
				{
					this.Connection.SetSynchronisationPositionAndRotation((!this.ConnectionSyncPosition) ? this.Connection.transform.position : transform.position, (!this.ConnectionSyncRotation) ? this.Connection.transform.rotation : transform.rotation);
				}
			}
		}

		public void OnBeforePush()
		{
			this.StripComponents(new Type[0]);
			this.Disconnect();
			this.DeleteMetadata();
		}

		public void OnAfterPop()
		{
			this.Reset();
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void Update()
		{
			if (Application.isPlaying)
			{
				this.DoUpdate();
			}
		}

		private void LateUpdate()
		{
			if (Application.isPlaying)
			{
				this.DoUpdate();
			}
		}

		private void FixedUpdate()
		{
			if (Application.isPlaying)
			{
				this.DoUpdate();
			}
		}

		private void OnDestroy()
		{
			bool flag = true;
			if (flag)
			{
				this.Disconnect();
			}
		}

		public void Reset()
		{
			this.m_OrientationAnchor = false;
			this.m_Swirl = CurvyOrientationSwirl.None;
			this.m_SwirlTurns = 0f;
			this.m_AutoHandles = true;
			this.m_AutoHandleDistance = 0.39f;
			this.m_HandleIn = new Vector3(-1f, 0f, 0f);
			this.m_HandleOut = new Vector3(1f, 0f, 0f);
			this.m_SynchronizeTCB = true;
			this.m_OverrideGlobalTension = false;
			this.m_OverrideGlobalContinuity = false;
			this.m_OverrideGlobalBias = false;
			this.m_StartTension = 0f;
			this.m_EndTension = 0f;
			this.m_StartContinuity = 0f;
			this.m_EndContinuity = 0f;
			this.m_StartBias = 0f;
			this.m_EndBias = 0f;
			if (this.mSpline)
			{
				this.Spline.SetDirty(this, SplineDirtyingType.Everything);
				this.Spline.InvalidateControlPointsRelationshipCacheINTERNAL();
			}
		}

		private CurvyInterpolation interpolation
		{
			get
			{
				return (!this.Spline) ? CurvyInterpolation.Linear : this.Spline.Interpolation;
			}
		}

		private bool isDynamicOrientation
		{
			get
			{
				return this.Spline && this.Spline.Orientation == CurvyOrientation.Dynamic;
			}
		}

		private bool IsOrientationAnchorEditable
		{
			get
			{
				CurvySpline spline = this.Spline;
				return this.isDynamicOrientation && spline.IsControlPointVisible(this) && spline.FirstVisibleControlPoint != this && spline.LastVisibleControlPoint != this;
			}
		}

		private bool canHaveSwirl
		{
			get
			{
				CurvySpline spline = this.Spline;
				return this.isDynamicOrientation && spline && spline.IsControlPointAnOrientationAnchor(this) && (spline.Closed || spline.LastVisibleControlPoint != this);
			}
		}

		internal void SetExtrinsicPropertiesINTERNAL(CurvySplineSegment.ControlPointExtrinsicProperties value)
		{
			this.extrinsicPropertiesINTERNAL = value;
		}

		internal CurvySplineSegment.ControlPointExtrinsicProperties GetExtrinsicPropertiesINTERNAL()
		{
			return this.extrinsicPropertiesINTERNAL;
		}

		private void DoUpdate()
		{
			if (this.AutoBakeOrientation && this.ApproximationUp.Length > 0)
			{
				this.BakeOrientationToTransform();
			}
		}

		private bool SetConnection(CurvyConnection newConnection)
		{
			bool result = false;
			if (this.m_Connection != newConnection)
			{
				result = true;
				this.m_Connection = newConnection;
			}
			if (this.m_Connection == null && this.m_FollowUp != null)
			{
				result = true;
				this.m_FollowUp = null;
			}
			return result;
		}

		private bool SetAutoHandles(bool newValue)
		{
			bool flag = false;
			if (this.Connection)
			{
				ReadOnlyCollection<CurvySplineSegment> controlPointsList = this.Connection.ControlPointsList;
				for (int i = 0; i < controlPointsList.Count; i++)
				{
					CurvySplineSegment curvySplineSegment = controlPointsList[i];
					flag = (flag || curvySplineSegment.m_AutoHandles != newValue);
					curvySplineSegment.m_AutoHandles = newValue;
				}
			}
			else
			{
				flag = (this.m_AutoHandles != newValue);
				this.m_AutoHandles = newValue;
			}
			return flag;
		}

		private float getApproximationLocalF(int idx)
		{
			return (float)idx * this.mStepSize;
		}

		private Vector3 interpolateLinear(float localF)
		{
			Transform transform = base.transform;
			localF = Mathf.Clamp01(localF);
			CurvySplineSegment nextControlPoint = this.Spline.GetNextControlPoint(this);
			return Vector3.LerpUnclamped(transform.localPosition, ((!nextControlPoint) ? transform : nextControlPoint.transform).localPosition, localF);
		}

		private Vector3 interpolateBezier(float localF)
		{
			localF = Mathf.Clamp01(localF);
			CurvySplineSegment nextControlPoint = this.Spline.GetNextControlPoint(this);
			Transform transform = nextControlPoint.transform;
			Vector3 localPosition = base.transform.localPosition;
			return CurvySpline.Bezier(localPosition + this.HandleOut, localPosition, transform.localPosition, transform.localPosition + nextControlPoint.HandleIn, localF);
		}

		private Vector3 interpolateCatmull(float localF)
		{
			localF = Mathf.Clamp01(localF);
			CurvySpline spline = this.Spline;
			CurvySplineSegment previousControlPointUsingFollowUp = spline.GetPreviousControlPointUsingFollowUp(this);
			CurvySplineSegment nextControlPoint = spline.GetNextControlPoint(this);
			CurvySplineSegment nextControlPointUsingFollowUp = nextControlPoint.Spline.GetNextControlPointUsingFollowUp(nextControlPoint);
			Vector3 localPosition = base.transform.localPosition;
			Vector3 t = (!previousControlPointUsingFollowUp) ? localPosition : previousControlPointUsingFollowUp.transform.localPosition;
			Vector3 localPosition2 = nextControlPoint.transform.localPosition;
			Vector3 t2 = (!nextControlPointUsingFollowUp) ? localPosition2 : nextControlPointUsingFollowUp.transform.localPosition;
			return CurvySpline.CatmullRom(t, localPosition, localPosition2, t2, localF);
		}

		private Vector3 interpolateTCB(float localF)
		{
			localF = Mathf.Clamp01(localF);
			float ft = this.StartTension;
			float ft2 = this.EndTension;
			float fc = this.StartContinuity;
			float fc2 = this.EndContinuity;
			float fb = this.StartBias;
			float fb2 = this.EndBias;
			CurvySpline spline = this.Spline;
			if (!this.OverrideGlobalTension)
			{
				ft2 = (ft = spline.Tension);
			}
			if (!this.OverrideGlobalContinuity)
			{
				fc2 = (fc = spline.Continuity);
			}
			if (!this.OverrideGlobalBias)
			{
				fb2 = (fb = spline.Bias);
			}
			CurvySplineSegment previousControlPointUsingFollowUp = spline.GetPreviousControlPointUsingFollowUp(this);
			CurvySplineSegment nextControlPoint = spline.GetNextControlPoint(this);
			CurvySplineSegment nextControlPointUsingFollowUp = nextControlPoint.Spline.GetNextControlPointUsingFollowUp(nextControlPoint);
			Vector3 localPosition = base.transform.localPosition;
			Vector3 t = (!previousControlPointUsingFollowUp) ? localPosition : previousControlPointUsingFollowUp.transform.localPosition;
			Vector3 localPosition2 = nextControlPoint.transform.localPosition;
			Vector3 t2 = (!nextControlPointUsingFollowUp) ? localPosition2 : nextControlPointUsingFollowUp.transform.localPosition;
			return CurvySpline.TCB(t, localPosition, localPosition2, t2, localF, ft, fc, fb, ft2, fc2, fb2);
		}

		internal void refreshCurveINTERNAL(CurvyInterpolation splineInterpolation, bool isControlPointASegment, CurvySpline spline)
		{
			short nextControlPointIndex = spline.GetNextControlPointIndex(this);
			CurvySplineSegment curvySplineSegment = (nextControlPointIndex != -1) ? spline.ControlPointsList[(int)nextControlPointIndex] : null;
			int num;
			if (isControlPointASegment)
			{
				num = CurvySpline.CalculateCacheSize(spline.CacheDensity, (curvySplineSegment.threadSafeLocalPosition - this.threadSafeLocalPosition).magnitude, spline.MaxPointsPerUnit);
			}
			else
			{
				num = 0;
			}
			this.CacheSize = num;
			Array.Resize<Vector3>(ref this.Approximation, num + 1);
			Array.Resize<Vector3>(ref this.ApproximationT, num + 1);
			Array.Resize<float>(ref this.ApproximationDistances, num + 1);
			Array.Resize<Vector3>(ref this.ApproximationUp, num + 1);
			this.Approximation[0] = this.threadSafeLocalPosition;
			this.ApproximationDistances[0] = 0f;
			this.mBounds = null;
			this.Length = 0f;
			this.mStepSize = 1f / (float)num;
			if (num != 0)
			{
				this.Approximation[num] = ((nextControlPointIndex == -1) ? this.threadSafeLocalPosition : curvySplineSegment.threadSafeLocalPosition);
			}
			if (isControlPointASegment)
			{
				float length = 0f;
				switch (splineInterpolation)
				{
				case CurvyInterpolation.Linear:
					length = this.InterpolateLinearSegment(spline, num);
					break;
				case CurvyInterpolation.CatmullRom:
					length = this.InterpolateCatmullSegment(spline, curvySplineSegment, num);
					break;
				case CurvyInterpolation.TCB:
					length = this.InterpolateTCBSegment(spline, curvySplineSegment, num);
					break;
				case CurvyInterpolation.Bezier:
					length = this.InterpolateBezierSegment(spline, num);
					break;
				default:
					DTLog.LogError("[Curvy] Invalid interpolation value " + splineInterpolation);
					break;
				}
				this.Length = length;
				Vector3 vector = this.Approximation[num] - this.Approximation[num - 1];
				this.Length += vector.magnitude;
				this.ApproximationDistances[num] = this.Length;
				this.ApproximationT[num - 1] = vector.normalized;
				this.ApproximationT[num] = this.ApproximationT[num - 1];
			}
			else if (nextControlPointIndex != -1)
			{
				this.ApproximationT[0] = (curvySplineSegment.threadSafeLocalPosition - this.Approximation[0]).normalized;
			}
			else
			{
				short previousControlPointIndex = spline.GetPreviousControlPointIndex(this);
				if (previousControlPointIndex != -1)
				{
					this.ApproximationT[0] = (this.Approximation[0] - spline.ControlPointsList[(int)previousControlPointIndex].threadSafeLocalPosition).normalized;
				}
				else
				{
					this.ApproximationT[0] = this.threadSafeLocalRotation * Vector3.forward;
				}
			}
			this.lastProcessedLocalPosition = this.threadSafeLocalPosition;
		}

		private float InterpolateBezierSegment(CurvySpline spline, int newCacheSize)
		{
			float num = 0f;
			CurvySplineSegment nextControlPoint = spline.GetNextControlPoint(this);
			Vector3 a = this.threadSafeLocalPosition;
			Vector3 vector = a + this.HandleOut;
			Vector3 a2 = nextControlPoint.threadSafeLocalPosition;
			Vector3 vector2 = a2 + nextControlPoint.HandleIn;
			double num2 = (double)(-(double)a.x) + 3.0 * (double)vector.x + -3.0 * (double)vector2.x + (double)a2.x;
			double num3 = 3.0 * (double)a.x + -6.0 * (double)vector.x + 3.0 * (double)vector2.x;
			double num4 = -3.0 * (double)a.x + 3.0 * (double)vector.x;
			double num5 = (double)a.x;
			double num6 = (double)(-(double)a.y) + 3.0 * (double)vector.y + -3.0 * (double)vector2.y + (double)a2.y;
			double num7 = 3.0 * (double)a.y + -6.0 * (double)vector.y + 3.0 * (double)vector2.y;
			double num8 = -3.0 * (double)a.y + 3.0 * (double)vector.y;
			double num9 = (double)a.y;
			double num10 = (double)(-(double)a.z) + 3.0 * (double)vector.z + -3.0 * (double)vector2.z + (double)a2.z;
			double num11 = 3.0 * (double)a.z + -6.0 * (double)vector.z + 3.0 * (double)vector2.z;
			double num12 = -3.0 * (double)a.z + 3.0 * (double)vector.z;
			double num13 = (double)a.z;
			for (int i = 1; i < newCacheSize; i++)
			{
				float num14 = (float)i * this.mStepSize;
				this.Approximation[i].x = (float)(((num2 * (double)num14 + num3) * (double)num14 + num4) * (double)num14 + num5);
				this.Approximation[i].y = (float)(((num6 * (double)num14 + num7) * (double)num14 + num8) * (double)num14 + num9);
				this.Approximation[i].z = (float)(((num10 * (double)num14 + num11) * (double)num14 + num12) * (double)num14 + num13);
				Vector3 vector3;
				vector3.x = this.Approximation[i].x - this.Approximation[i - 1].x;
				vector3.y = this.Approximation[i].y - this.Approximation[i - 1].y;
				vector3.z = this.Approximation[i].z - this.Approximation[i - 1].z;
				float num15 = Mathf.Sqrt(vector3.x * vector3.x + vector3.y * vector3.y + vector3.z * vector3.z);
				num += num15;
				this.ApproximationDistances[i] = num;
				if ((double)num15 > 9.99999974737875E-06)
				{
					float num16 = 1f / num15;
					this.ApproximationT[i - 1].x = vector3.x * num16;
					this.ApproximationT[i - 1].y = vector3.y * num16;
					this.ApproximationT[i - 1].z = vector3.z * num16;
				}
				else
				{
					this.ApproximationT[i - 1].x = 0f;
					this.ApproximationT[i - 1].y = 0f;
					this.ApproximationT[i - 1].z = 0f;
				}
			}
			return num;
		}

		private float InterpolateTCBSegment(CurvySpline spline, CurvySplineSegment nextControlPoint, int newCacheSize)
		{
			float num = 0f;
			float num2 = this.StartTension;
			float num3 = this.EndTension;
			float num4 = this.StartContinuity;
			float num5 = this.EndContinuity;
			float num6 = this.StartBias;
			float num7 = this.EndBias;
			if (!this.OverrideGlobalTension)
			{
				num3 = (num2 = spline.Tension);
			}
			if (!this.OverrideGlobalContinuity)
			{
				num5 = (num4 = spline.Continuity);
			}
			if (!this.OverrideGlobalBias)
			{
				num7 = (num6 = spline.Bias);
			}
			CurvySplineSegment previousControlPointUsingFollowUp = spline.GetPreviousControlPointUsingFollowUp(this);
			CurvySplineSegment nextControlPointUsingFollowUp = nextControlPoint.Spline.GetNextControlPointUsingFollowUp(nextControlPoint);
			Vector3 vector = this.threadSafeLocalPosition;
			Vector3 vector2 = nextControlPoint.threadSafeLocalPosition;
			Vector3 vector3 = (!previousControlPointUsingFollowUp) ? vector : previousControlPointUsingFollowUp.threadSafeLocalPosition;
			Vector3 vector4 = (!nextControlPointUsingFollowUp) ? vector2 : nextControlPointUsingFollowUp.threadSafeLocalPosition;
			double num8 = (double)((1f - num2) * (1f + num4) * (1f + num6));
			double num9 = (double)((1f - num2) * (1f - num4) * (1f - num6));
			double num10 = (double)((1f - num3) * (1f - num5) * (1f + num7));
			double num11 = (double)((1f - num3) * (1f + num5) * (1f - num7));
			double num12 = 2.0;
			double num13 = -num8 / num12;
			double num14 = (4.0 + num8 - num9 - num10) / num12;
			double num15 = (-4.0 + num9 + num10 - num11) / num12;
			double num16 = num11 / num12;
			double num17 = 2.0 * num8 / num12;
			double num18 = (-6.0 - 2.0 * num8 + 2.0 * num9 + num10) / num12;
			double num19 = (6.0 - 2.0 * num9 - num10 + num11) / num12;
			double num20 = -num11 / num12;
			double num21 = -num8 / num12;
			double num22 = (num8 - num9) / num12;
			double num23 = num9 / num12;
			double num24 = 2.0 / num12;
			double num25 = num13 * (double)vector3.x + num14 * (double)vector.x + num15 * (double)vector2.x + num16 * (double)vector4.x;
			double num26 = num17 * (double)vector3.x + num18 * (double)vector.x + num19 * (double)vector2.x + num20 * (double)vector4.x;
			double num27 = num21 * (double)vector3.x + num22 * (double)vector.x + num23 * (double)vector2.x;
			double num28 = num24 * (double)vector.x;
			double num29 = num13 * (double)vector3.y + num14 * (double)vector.y + num15 * (double)vector2.y + num16 * (double)vector4.y;
			double num30 = num17 * (double)vector3.y + num18 * (double)vector.y + num19 * (double)vector2.y + num20 * (double)vector4.y;
			double num31 = num21 * (double)vector3.y + num22 * (double)vector.y + num23 * (double)vector2.y;
			double num32 = num24 * (double)vector.y;
			double num33 = num13 * (double)vector3.z + num14 * (double)vector.z + num15 * (double)vector2.z + num16 * (double)vector4.z;
			double num34 = num17 * (double)vector3.z + num18 * (double)vector.z + num19 * (double)vector2.z + num20 * (double)vector4.z;
			double num35 = num21 * (double)vector3.z + num22 * (double)vector.z + num23 * (double)vector2.z;
			double num36 = num24 * (double)vector.z;
			for (int i = 1; i < newCacheSize; i++)
			{
				float num37 = (float)i * this.mStepSize;
				this.Approximation[i].x = (float)(((num25 * (double)num37 + num26) * (double)num37 + num27) * (double)num37 + num28);
				this.Approximation[i].y = (float)(((num29 * (double)num37 + num30) * (double)num37 + num31) * (double)num37 + num32);
				this.Approximation[i].z = (float)(((num33 * (double)num37 + num34) * (double)num37 + num35) * (double)num37 + num36);
				Vector3 vector5;
				vector5.x = this.Approximation[i].x - this.Approximation[i - 1].x;
				vector5.y = this.Approximation[i].y - this.Approximation[i - 1].y;
				vector5.z = this.Approximation[i].z - this.Approximation[i - 1].z;
				float num38 = Mathf.Sqrt(vector5.x * vector5.x + vector5.y * vector5.y + vector5.z * vector5.z);
				num += num38;
				this.ApproximationDistances[i] = num;
				if ((double)num38 > 9.99999974737875E-06)
				{
					float num39 = 1f / num38;
					this.ApproximationT[i - 1].x = vector5.x * num39;
					this.ApproximationT[i - 1].y = vector5.y * num39;
					this.ApproximationT[i - 1].z = vector5.z * num39;
				}
				else
				{
					this.ApproximationT[i - 1].x = 0f;
					this.ApproximationT[i - 1].y = 0f;
					this.ApproximationT[i - 1].z = 0f;
				}
			}
			return num;
		}

		private float InterpolateCatmullSegment(CurvySpline spline, CurvySplineSegment nextControlPoint, int newCacheSize)
		{
			float num = 0f;
			CurvySplineSegment previousControlPointUsingFollowUp = spline.GetPreviousControlPointUsingFollowUp(this);
			CurvySplineSegment nextControlPointUsingFollowUp = nextControlPoint.Spline.GetNextControlPointUsingFollowUp(nextControlPoint);
			Vector3 vector = this.threadSafeLocalPosition;
			Vector3 vector2 = nextControlPoint.threadSafeLocalPosition;
			Vector3 vector3 = (!previousControlPointUsingFollowUp) ? vector : previousControlPointUsingFollowUp.threadSafeLocalPosition;
			Vector3 vector4 = (!nextControlPointUsingFollowUp) ? vector2 : nextControlPointUsingFollowUp.threadSafeLocalPosition;
			double num2 = -0.5 * (double)vector3.x + 1.5 * (double)vector.x + -1.5 * (double)vector2.x + 0.5 * (double)vector4.x;
			double num3 = (double)vector3.x + -2.5 * (double)vector.x + 2.0 * (double)vector2.x + -0.5 * (double)vector4.x;
			double num4 = -0.5 * (double)vector3.x + 0.5 * (double)vector2.x;
			double num5 = (double)vector.x;
			double num6 = -0.5 * (double)vector3.y + 1.5 * (double)vector.y + -1.5 * (double)vector2.y + 0.5 * (double)vector4.y;
			double num7 = (double)vector3.y + -2.5 * (double)vector.y + 2.0 * (double)vector2.y + -0.5 * (double)vector4.y;
			double num8 = -0.5 * (double)vector3.y + 0.5 * (double)vector2.y;
			double num9 = (double)vector.y;
			double num10 = -0.5 * (double)vector3.z + 1.5 * (double)vector.z + -1.5 * (double)vector2.z + 0.5 * (double)vector4.z;
			double num11 = (double)vector3.z + -2.5 * (double)vector.z + 2.0 * (double)vector2.z + -0.5 * (double)vector4.z;
			double num12 = -0.5 * (double)vector3.z + 0.5 * (double)vector2.z;
			double num13 = (double)vector.z;
			for (int i = 1; i < newCacheSize; i++)
			{
				float num14 = (float)i * this.mStepSize;
				this.Approximation[i].x = (float)(((num2 * (double)num14 + num3) * (double)num14 + num4) * (double)num14 + num5);
				this.Approximation[i].y = (float)(((num6 * (double)num14 + num7) * (double)num14 + num8) * (double)num14 + num9);
				this.Approximation[i].z = (float)(((num10 * (double)num14 + num11) * (double)num14 + num12) * (double)num14 + num13);
				Vector3 vector5;
				vector5.x = this.Approximation[i].x - this.Approximation[i - 1].x;
				vector5.y = this.Approximation[i].y - this.Approximation[i - 1].y;
				vector5.z = this.Approximation[i].z - this.Approximation[i - 1].z;
				float num15 = Mathf.Sqrt(vector5.x * vector5.x + vector5.y * vector5.y + vector5.z * vector5.z);
				num += num15;
				this.ApproximationDistances[i] = num;
				if ((double)num15 > 9.99999974737875E-06)
				{
					float num16 = 1f / num15;
					this.ApproximationT[i - 1].x = vector5.x * num16;
					this.ApproximationT[i - 1].y = vector5.y * num16;
					this.ApproximationT[i - 1].z = vector5.z * num16;
				}
				else
				{
					this.ApproximationT[i - 1].x = 0f;
					this.ApproximationT[i - 1].y = 0f;
					this.ApproximationT[i - 1].z = 0f;
				}
			}
			return num;
		}

		private float InterpolateLinearSegment(CurvySpline spline, int newCacheSize)
		{
			float num = 0f;
			Vector3 a = this.threadSafeLocalPosition;
			Vector3 b = spline.GetNextControlPoint(this).threadSafeLocalPosition;
			for (int i = 1; i < newCacheSize; i++)
			{
				float t = (float)i * this.mStepSize;
				this.Approximation[i] = Vector3.LerpUnclamped(a, b, t);
				Vector3 vector;
				vector.x = this.Approximation[i].x - this.Approximation[i - 1].x;
				vector.y = this.Approximation[i].y - this.Approximation[i - 1].y;
				vector.z = this.Approximation[i].z - this.Approximation[i - 1].z;
				float num2 = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
				num += num2;
				this.ApproximationDistances[i] = num;
				if ((double)num2 > 9.99999974737875E-06)
				{
					float num3 = 1f / num2;
					this.ApproximationT[i - 1].x = vector.x * num3;
					this.ApproximationT[i - 1].y = vector.y * num3;
					this.ApproximationT[i - 1].z = vector.z * num3;
				}
				else
				{
					this.ApproximationT[i - 1].x = 0f;
					this.ApproximationT[i - 1].y = 0f;
					this.ApproximationT[i - 1].z = 0f;
				}
			}
			return num;
		}

		internal void refreshOrientationNoneINTERNAL()
		{
			Array.Clear(this.ApproximationUp, 0, this.ApproximationUp.Length);
			this.lastProcessedLocalRotation = this.threadSafeLocalRotation;
		}

		internal void refreshOrientationStaticINTERNAL()
		{
			Vector3 a = this.ApproximationUp[0] = this.getOrthoUp0INTERNAL();
			if (this.Approximation.Length > 1)
			{
				int num = this.CacheSize;
				Vector3 b = this.ApproximationUp[num] = this.getOrthoUp1INTERNAL();
				float num2 = 1f / (float)num;
				for (int i = 1; i < num; i++)
				{
					this.ApproximationUp[i] = Vector3.SlerpUnclamped(a, b, (float)i * num2);
				}
			}
			this.lastProcessedLocalRotation = this.threadSafeLocalRotation;
		}

		internal void refreshOrientationDynamicINTERNAL(Vector3 initialUp)
		{
			int num = this.ApproximationUp.Length;
			this.ApproximationUp[0] = initialUp;
			for (int i = 1; i < num; i++)
			{
				Vector3 vector = this.ApproximationT[i - 1];
				Vector3 vector2 = this.ApproximationT[i];
				Vector3 axis;
				axis.x = vector.y * vector2.z - vector.z * vector2.y;
				axis.y = vector.z * vector2.x - vector.x * vector2.z;
				axis.z = vector.x * vector2.y - vector.y * vector2.x;
				float num2 = (float)Math.Atan2(Math.Sqrt((double)(axis.x * axis.x + axis.y * axis.y + axis.z * axis.z)), (double)(vector.x * vector2.x + vector.y * vector2.y + vector.z * vector2.z));
				this.ApproximationUp[i] = Quaternion.AngleAxis(57.29578f * num2, axis) * this.ApproximationUp[i - 1];
			}
			this.lastProcessedLocalRotation = this.threadSafeLocalRotation;
		}

		internal void ClearBoundsINTERNAL()
		{
			this.mBounds = null;
		}

		internal Vector3 getOrthoUp0INTERNAL()
		{
			Vector3 result = this.threadSafeLocalRotation * Vector3.up;
			Vector3.OrthoNormalize(ref this.ApproximationT[0], ref result);
			return result;
		}

		private Vector3 getOrthoUp1INTERNAL()
		{
			CurvySplineSegment nextControlPoint = this.Spline.GetNextControlPoint(this);
			Quaternion rotation = (!nextControlPoint) ? this.threadSafeLocalRotation : nextControlPoint.threadSafeLocalRotation;
			Vector3 result = rotation * Vector3.up;
			Vector3.OrthoNormalize(ref this.ApproximationT[this.CacheSize], ref result);
			return result;
		}

		internal void UnsetFollowUpWithoutDirtyingINTERNAL()
		{
			this.m_FollowUp = null;
			this.m_FollowUpHeading = ConnectionHeadingEnum.Auto;
		}

		private bool SnapToFitSplineLength(float newSplineLength, float stepSize)
		{
			CurvySpline spline = this.Spline;
			if (stepSize == 0f || Mathf.Approximately(newSplineLength, spline.Length))
			{
				return true;
			}
			Transform transform = base.transform;
			float length = spline.Length;
			Vector3 position = transform.position;
			Vector3 vector = transform.up * stepSize;
			transform.position += vector;
			spline.SetDirty(this, SplineDirtyingType.Everything);
			spline.Refresh();
			bool flag = spline.Length > length;
			int num = 30000;
			transform.position = position;
			if (newSplineLength > length)
			{
				if (!flag)
				{
					vector *= -1f;
				}
				while (spline.Length < newSplineLength)
				{
					num--;
					length = spline.Length;
					transform.position += vector;
					spline.SetDirty(this, SplineDirtyingType.Everything);
					spline.Refresh();
					if (length > spline.Length)
					{
						return false;
					}
					if (num == 0)
					{
						UnityEngine.Debug.LogError("CurvySplineSegment.SnapToFitSplineLength exceeds 30000 loops, considering this a dead loop! This shouldn't happen, please send a bug report.");
						return false;
					}
				}
			}
			else
			{
				if (flag)
				{
					vector *= -1f;
				}
				while (spline.Length > newSplineLength)
				{
					num--;
					length = spline.Length;
					transform.position += vector;
					spline.SetDirty(this, SplineDirtyingType.Everything);
					spline.Refresh();
					if (length < spline.Length)
					{
						return false;
					}
					if (num == 0)
					{
						UnityEngine.Debug.LogError("CurvySplineSegment.SnapToFitSplineLength exceeds 30000 loops, considering this a dead loop! This shouldn't happen, please send a bug report.");
						return false;
					}
				}
			}
			return true;
		}

		internal void PrepareThreadSafeTransfromINTERNAL()
		{
			Transform transform = base.transform;
			this.threadSafeLocalPosition = transform.localPosition;
			this.threadSafeLocalRotation = transform.localRotation;
		}

		[NonSerialized]
		public Vector3[] Approximation = new Vector3[0];

		[NonSerialized]
		public float[] ApproximationDistances = new float[0];

		[NonSerialized]
		public Vector3[] ApproximationUp = new Vector3[0];

		[NonSerialized]
		public Vector3[] ApproximationT = new Vector3[0];

		[Group("General")]
		[FieldAction("CBBakeOrientation", ActionAttribute.ActionEnum.Callback, Position = ActionAttribute.ActionPositionEnum.Below)]
		[Label("Bake Orientation", "Automatically apply orientation to CP transforms?")]
		[SerializeField]
		private bool m_AutoBakeOrientation;

		[Group("General")]
		[Tooltip("Check to use this transform's rotation")]
		[FieldCondition("IsOrientationAnchorEditable", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private bool m_OrientationAnchor;

		[Label("Swirl", "Add Swirl to orientation?")]
		[Group("General")]
		[FieldCondition("canHaveSwirl", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private CurvyOrientationSwirl m_Swirl;

		[Label("Turns", "Number of swirl turns")]
		[Group("General")]
		[FieldCondition("canHaveSwirl", true, false, ConditionalAttribute.OperatorEnum.AND, "m_Swirl", CurvyOrientationSwirl.None, true)]
		[SerializeField]
		private float m_SwirlTurns;

		[Section("Bezier Options", true, false, 100, Sort = 1, HelpURL = "https://curvyeditor.com/doclink/curvysplinesegment_bezier")]
		[GroupCondition("interpolation", CurvyInterpolation.Bezier, false)]
		[SerializeField]
		private bool m_AutoHandles = true;

		[RangeEx(0f, 1f, "Distance %", "Handle length by distance to neighbours")]
		[FieldCondition("m_AutoHandles", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		[SerializeField]
		private float m_AutoHandleDistance = 0.39f;

		[VectorEx("", "", Precision = 3, Options = (AttributeOptionsFlags)1152, Color = "#FFFF00")]
		[SerializeField]
		[FormerlySerializedAs("HandleIn")]
		private Vector3 m_HandleIn = CurvySplineSegmentDefaultValues.HandleIn;

		[VectorEx("", "", Precision = 3, Options = (AttributeOptionsFlags)1152, Color = "#00FF00")]
		[SerializeField]
		[FormerlySerializedAs("HandleOut")]
		private Vector3 m_HandleOut = CurvySplineSegmentDefaultValues.HandleOut;

		[Section("TCB Options", true, false, 100, Sort = 1, HelpURL = "https://curvyeditor.com/doclink/curvysplinesegment_tcb")]
		[GroupCondition("interpolation", CurvyInterpolation.TCB, false)]
		[GroupAction("TCBOptionsGUI", ActionAttribute.ActionEnum.Callback, Position = ActionAttribute.ActionPositionEnum.Below)]
		[Label("Local Tension", "Override Spline Tension?")]
		[SerializeField]
		[FormerlySerializedAs("OverrideGlobalTension")]
		private bool m_OverrideGlobalTension;

		[Label("Local Continuity", "Override Spline Continuity?")]
		[SerializeField]
		[FormerlySerializedAs("OverrideGlobalContinuity")]
		private bool m_OverrideGlobalContinuity;

		[Label("Local Bias", "Override Spline Bias?")]
		[SerializeField]
		[FormerlySerializedAs("OverrideGlobalBias")]
		private bool m_OverrideGlobalBias;

		[Tooltip("Synchronize Start and End Values")]
		[SerializeField]
		[FormerlySerializedAs("SynchronizeTCB")]
		private bool m_SynchronizeTCB = true;

		[Label("Tension", "")]
		[FieldCondition("m_OverrideGlobalTension", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[FormerlySerializedAs("StartTension")]
		private float m_StartTension;

		[Label("Tension (End)", "")]
		[FieldCondition("m_OverrideGlobalTension", true, false, ConditionalAttribute.OperatorEnum.AND, "m_SynchronizeTCB", false, false)]
		[SerializeField]
		[FormerlySerializedAs("EndTension")]
		private float m_EndTension;

		[Label("Continuity", "")]
		[FieldCondition("m_OverrideGlobalContinuity", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[FormerlySerializedAs("StartContinuity")]
		private float m_StartContinuity;

		[Label("Continuity (End)", "")]
		[FieldCondition("m_OverrideGlobalContinuity", true, false, ConditionalAttribute.OperatorEnum.AND, "m_SynchronizeTCB", false, false)]
		[SerializeField]
		[FormerlySerializedAs("EndContinuity")]
		private float m_EndContinuity;

		[Label("Bias", "")]
		[FieldCondition("m_OverrideGlobalBias", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[FormerlySerializedAs("StartBias")]
		private float m_StartBias;

		[Label("Bias (End)", "")]
		[FieldCondition("m_OverrideGlobalBias", true, false, ConditionalAttribute.OperatorEnum.AND, "m_SynchronizeTCB", false, false)]
		[SerializeField]
		[FormerlySerializedAs("EndBias")]
		private float m_EndBias;

		[SerializeField]
		[HideInInspector]
		private CurvySplineSegment m_FollowUp;

		[SerializeField]
		[HideInInspector]
		private ConnectionHeadingEnum m_FollowUpHeading = ConnectionHeadingEnum.Auto;

		[SerializeField]
		[HideInInspector]
		private bool m_ConnectionSyncPosition;

		[SerializeField]
		[HideInInspector]
		private bool m_ConnectionSyncRotation;

		[SerializeField]
		[HideInInspector]
		private CurvyConnection m_Connection;

		private int cacheSize = -1;

		private Vector3 threadSafeLocalPosition;

		private Quaternion threadSafeLocalRotation;

		private CurvySpline mSpline;

		private float mStepSize;

		private Bounds? mBounds;

		private int mCacheLastDistanceToLocalFIndex;

		private List<Component> mMetaData;

		private Vector3 lastProcessedLocalPosition;

		private Quaternion lastProcessedLocalRotation;

		private CurvySplineSegment.ControlPointExtrinsicProperties extrinsicPropertiesINTERNAL;

		internal struct ControlPointExtrinsicProperties : IEquatable<CurvySplineSegment.ControlPointExtrinsicProperties>
		{
			internal ControlPointExtrinsicProperties(bool isVisible, short segmentIndex, short controlPointIndex, short previousControlPointIndex, short nextControlPointIndex, bool previousControlPointIsSegment, bool nextControlPointIsSegment, bool canHaveFollowUp, short orientationAnchorIndex)
			{
				this.isVisible = isVisible;
				this.segmentIndex = segmentIndex;
				this.controlPointIndex = controlPointIndex;
				this.nextControlPointIndex = nextControlPointIndex;
				this.previousControlPointIndex = previousControlPointIndex;
				this.previousControlPointIsSegment = previousControlPointIsSegment;
				this.nextControlPointIsSegment = nextControlPointIsSegment;
				this.canHaveFollowUp = canHaveFollowUp;
				this.orientationAnchorIndex = orientationAnchorIndex;
			}

			internal bool IsVisible
			{
				get
				{
					return this.isVisible;
				}
			}

			internal short SegmentIndex
			{
				get
				{
					return this.segmentIndex;
				}
			}

			internal short ControlPointIndex
			{
				get
				{
					return this.controlPointIndex;
				}
			}

			internal short NextControlPointIndex
			{
				get
				{
					return this.nextControlPointIndex;
				}
			}

			internal short PreviousControlPointIndex
			{
				get
				{
					return this.previousControlPointIndex;
				}
			}

			internal bool PreviousControlPointIsSegment
			{
				get
				{
					return this.previousControlPointIsSegment;
				}
			}

			internal bool NextControlPointIsSegment
			{
				get
				{
					return this.nextControlPointIsSegment;
				}
			}

			internal bool CanHaveFollowUp
			{
				get
				{
					return this.canHaveFollowUp;
				}
			}

			internal bool IsSegment
			{
				get
				{
					return this.SegmentIndex != -1;
				}
			}

			internal short OrientationAnchorIndex
			{
				get
				{
					return this.orientationAnchorIndex;
				}
			}

			public bool Equals(CurvySplineSegment.ControlPointExtrinsicProperties other)
			{
				return this.IsVisible == other.IsVisible && this.SegmentIndex == other.SegmentIndex && this.ControlPointIndex == other.ControlPointIndex && this.NextControlPointIndex == other.NextControlPointIndex && this.PreviousControlPointIndex == other.PreviousControlPointIndex && this.PreviousControlPointIsSegment == other.PreviousControlPointIsSegment && this.NextControlPointIsSegment == other.NextControlPointIsSegment && this.CanHaveFollowUp == other.CanHaveFollowUp && this.OrientationAnchorIndex == other.OrientationAnchorIndex;
			}

			public override bool Equals(object obj)
			{
				return !object.ReferenceEquals(null, obj) && obj is CurvySplineSegment.ControlPointExtrinsicProperties && this.Equals((CurvySplineSegment.ControlPointExtrinsicProperties)obj);
			}

			public override int GetHashCode()
			{
				int num = this.IsVisible.GetHashCode();
				num = (num * 397 ^ this.SegmentIndex.GetHashCode());
				num = (num * 397 ^ this.ControlPointIndex.GetHashCode());
				num = (num * 397 ^ this.NextControlPointIndex.GetHashCode());
				num = (num * 397 ^ this.PreviousControlPointIndex.GetHashCode());
				num = (num * 397 ^ this.PreviousControlPointIsSegment.GetHashCode());
				num = (num * 397 ^ this.NextControlPointIsSegment.GetHashCode());
				num = (num * 397 ^ this.CanHaveFollowUp.GetHashCode());
				return num * 397 ^ this.OrientationAnchorIndex.GetHashCode();
			}

			public static bool operator ==(CurvySplineSegment.ControlPointExtrinsicProperties left, CurvySplineSegment.ControlPointExtrinsicProperties right)
			{
				return left.Equals(right);
			}

			public static bool operator !=(CurvySplineSegment.ControlPointExtrinsicProperties left, CurvySplineSegment.ControlPointExtrinsicProperties right)
			{
				return !left.Equals(right);
			}

			private readonly bool isVisible;

			private readonly short segmentIndex;

			private readonly short controlPointIndex;

			private readonly short nextControlPointIndex;

			private readonly short previousControlPointIndex;

			private readonly bool previousControlPointIsSegment;

			private readonly bool nextControlPointIsSegment;

			private readonly bool canHaveFollowUp;

			private readonly short orientationAnchorIndex;
		}
	}
}
