// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Controllers.PathController
using System;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Controllers
{
	[AddComponentMenu("Curvy/Controller/CG Path Controller", 7)]
	[HelpURL("https://curvyeditor.com/doclink/pathcontroller")]
	public class PathController : CurvyController
	{
		public CGDataReference Path
		{
			get
			{
				return this.m_Path;
			}
			set
			{
				this.m_Path = value;
			}
		}

		public CGPath PathData
		{
			get
			{
				return (!this.Path.HasValue) ? null : this.Path.GetData<CGPath>();
			}
		}

		public override float Length
		{
			get
			{
				return (this.PathData == null) ? 0f : this.PathData.Length;
			}
		}

		public override bool IsReady
		{
			get
			{
				return this.Path != null && !this.Path.IsEmpty && this.Path.HasValue;
			}
		}

		protected override float RelativeToAbsolute(float relativeDistance)
		{
			return (this.PathData == null) ? 0f : this.PathData.FToDistance(relativeDistance);
		}

		protected override float AbsoluteToRelative(float worldUnitDistance)
		{
			return (this.PathData == null) ? 0f : this.PathData.DistanceToF(worldUnitDistance);
		}

		protected override Vector3 GetInterpolatedSourcePosition(float tf)
		{
			return this.Path.Module.Generator.transform.TransformPoint(this.PathData.InterpolatePosition(tf));
		}

		protected override void GetInterpolatedSourcePosition(float tf, out Vector3 interpolatedPosition, out Vector3 tangent, out Vector3 up)
		{
			this.PathData.Interpolate(tf, out interpolatedPosition, out tangent, out up);
			Transform transform = this.Path.Module.Generator.transform;
			interpolatedPosition = transform.TransformPoint(interpolatedPosition);
			tangent = transform.TransformDirection(tangent);
			up = transform.TransformDirection(up);
		}

		protected override Vector3 GetTangent(float tf)
		{
			return this.Path.Module.Generator.transform.TransformDirection(this.PathData.InterpolateDirection(tf));
		}

		protected override Vector3 GetOrientation(float tf)
		{
			return this.Path.Module.Generator.transform.TransformDirection(this.PathData.InterpolateUp(tf));
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
				this.PathData.MoveBy(ref tf, ref value, speed * deltaTime, base.Clamping);
			}
			else
			{
				this.PathData.Move(ref tf, ref value, speed * deltaTime, base.Clamping);
			}
			curyDirection = MovementDirectionMethods.FromInt(value);
		}

		[Section("General", true, false, 100, Sort = 0)]
		[SerializeField]
		[CGDataReferenceSelector(typeof(CGPath), Label = "Path/Slot")]
		private CGDataReference m_Path = new CGDataReference();
	}
}
