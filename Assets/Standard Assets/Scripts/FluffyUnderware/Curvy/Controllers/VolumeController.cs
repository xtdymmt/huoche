// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Controllers.VolumeController
using System;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.Curvy.Utils;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Controllers
{
	[AddComponentMenu("Curvy/Controller/CG Volume Controller", 8)]
	[HelpURL("https://curvyeditor.com/doclink/volumecontroller")]
	public class VolumeController : CurvyController
	{
		public CGDataReference Volume
		{
			get
			{
				return this.m_Volume;
			}
			set
			{
				this.m_Volume = value;
			}
		}

		public CGVolume VolumeData
		{
			get
			{
				return (!this.Volume.HasValue) ? null : this.Volume.GetData<CGVolume>();
			}
		}

		public float CrossFrom
		{
			get
			{
				return this.m_CrossRange.From;
			}
			set
			{
				this.m_CrossRange.From = Mathf.Clamp(value, -0.5f, 0.5f);
			}
		}

		public float CrossTo
		{
			get
			{
				return this.m_CrossRange.To;
			}
			set
			{
				this.m_CrossRange.To = Mathf.Clamp(value, this.CrossFrom, 0.5f);
			}
		}

		public float CrossLength
		{
			get
			{
				return this.m_CrossRange.Length;
			}
		}

		public CurvyClamping CrossClamping
		{
			get
			{
				return this.m_CrossClamping;
			}
			set
			{
				this.m_CrossClamping = value;
			}
		}

		public float CrossRelativePosition
		{
			get
			{
				return this.GetClampedCrossPosition(this.crossRelativePosition);
			}
			set
			{
				this.crossRelativePosition = this.GetClampedCrossPosition(value);
			}
		}

		public override float Length
		{
			get
			{
				return (this.VolumeData == null) ? 0f : this.VolumeData.Length;
			}
		}

		public float CrossRelativeToAbsolute(float relativeDistance)
		{
			return (this.VolumeData == null) ? 0f : this.VolumeData.CrossFToDistance(base.RelativePosition, relativeDistance, this.CrossClamping);
		}

		public float CrossAbsoluteToRelative(float worldUnitDistance)
		{
			return (this.VolumeData == null) ? 0f : this.VolumeData.CrossDistanceToF(base.RelativePosition, worldUnitDistance, this.CrossClamping);
		}

		public override bool IsReady
		{
			get
			{
				return this.Volume != null && !this.Volume.IsEmpty && this.Volume.HasValue;
			}
		}

		protected override float RelativeToAbsolute(float relativeDistance)
		{
			return (this.VolumeData == null) ? 0f : this.VolumeData.FToDistance(relativeDistance);
		}

		protected override float AbsoluteToRelative(float worldUnitDistance)
		{
			return (this.VolumeData == null) ? 0f : this.VolumeData.DistanceToF(worldUnitDistance);
		}

		protected override Vector3 GetInterpolatedSourcePosition(float tf)
		{
			return this.Volume.Module.Generator.transform.TransformPoint(this.VolumeData.InterpolateVolumePosition(tf, this.CrossRelativePosition));
		}

		protected override void GetInterpolatedSourcePosition(float tf, out Vector3 interpolatedPosition, out Vector3 tangent, out Vector3 up)
		{
			this.VolumeData.InterpolateVolume(tf, this.CrossRelativePosition, out interpolatedPosition, out tangent, out up);
			Transform transform = this.Volume.Module.Generator.transform;
			interpolatedPosition = transform.TransformPoint(interpolatedPosition);
			tangent = transform.TransformDirection(tangent);
			up = transform.TransformDirection(up);
		}

		protected override Vector3 GetTangent(float tf)
		{
			return this.Volume.Module.Generator.transform.TransformDirection(this.VolumeData.InterpolateVolumeDirection(tf, this.CrossRelativePosition));
		}

		protected override Vector3 GetOrientation(float tf)
		{
			return this.Volume.Module.Generator.transform.TransformDirection(this.VolumeData.InterpolateVolumeUp(tf, this.CrossRelativePosition));
		}

		protected override void Advance(float speed, float deltaTime)
		{
			float relativePosition = base.RelativePosition;
			MovementDirection movementDirection = base.MovementDirection;
			this.SimulateAdvance(ref relativePosition, ref movementDirection, speed, deltaTime);
			base.MovementDirection = movementDirection;
			base.RelativePosition = relativePosition;
		}

		protected override void SimulateAdvance(ref float tf, ref MovementDirection curyDirection, float speed, float deltaTime)
		{
			int value = curyDirection.ToInt();
			CurvyController.MoveModeEnum moveMode = base.MoveMode;
			if (moveMode != CurvyController.MoveModeEnum.Relative)
			{
				if (moveMode != CurvyController.MoveModeEnum.AbsolutePrecise)
				{
					throw new NotSupportedException();
				}
				this.VolumeData.MoveBy(ref tf, ref value, speed * deltaTime, base.Clamping);
			}
			else
			{
				this.VolumeData.Move(ref tf, ref value, speed * deltaTime, base.Clamping);
			}
			curyDirection = MovementDirectionMethods.FromInt(value);
		}

		private RegionOptions<float> CrossRangeOptions
		{
			get
			{
				return RegionOptions<float>.MinMax(-0.5f, 0.5f);
			}
		}

		private float MinCrossRelativePosition
		{
			get
			{
				return this.m_CrossRange.From;
			}
		}

		private float MaxCrossRelativePosition
		{
			get
			{
				return this.m_CrossRange.To;
			}
		}

		private float GetClampedCrossPosition(float position)
		{
			return CurvyUtility.ClampValue(position, this.CrossClamping, this.CrossFrom, this.CrossTo);
		}

		public override void OnAfterDeserialize()
		{
			base.OnAfterDeserialize();
			if (!float.IsNaN(this.m_CrossInitialPosition))
			{
				this.crossRelativePosition = DTMath.MapValue(this.CrossFrom, this.CrossTo, this.m_CrossInitialPosition, -0.5f, 0.5f);
				this.m_CrossInitialPosition = float.NaN;
			}
		}

		private const float CrossPositionRangeMin = -0.5f;

		private const float CrossPositionRangeMax = 0.5f;

		[Section("General", true, false, 100)]
		[CGDataReferenceSelector(typeof(CGVolume), Label = "Volume/Slot")]
		[SerializeField]
		private CGDataReference m_Volume = new CGDataReference();

		[Section("Cross Position", true, false, 100, Sort = 1, HelpURL = "https://curvyeditor.com/doclink/volumecontroller_crossposition")]
		[SerializeField]
		[FloatRegion(UseSlider = true, Precision = 4, RegionOptionsPropertyName = "CrossRangeOptions", Options = AttributeOptionsFlags.Full)]
		private FloatRegion m_CrossRange = new FloatRegion(-0.5f, 0.5f);

		[RangeEx("MinCrossRelativePosition", "MaxCrossRelativePosition", "", "")]
		[SerializeField]
		private float crossRelativePosition;

		[SerializeField]
		private CurvyClamping m_CrossClamping;

		[SerializeField]
		[HideInInspector]
		[Obsolete("Use crossRelativePosition instead. This field is kept for retro compatibility reasons")]
		private float m_CrossInitialPosition;
	}
}
