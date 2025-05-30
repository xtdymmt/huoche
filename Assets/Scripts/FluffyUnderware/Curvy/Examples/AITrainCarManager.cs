// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.AITrainCarManager
using System;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace FluffyUnderware.Curvy.Examples
{
	[ExecuteInEditMode]
	public class AITrainCarManager : MonoBehaviour
	{
		public float Position
		{
			get
			{
				return this.Waggon.AbsolutePosition;
			}
			set
			{
				if (this.Waggon.AbsolutePosition != value)
				{
					this.Waggon.AbsolutePosition = value;
					this.FrontAxis.AbsolutePosition = value + this.mTrain.AxisDistance / 2f;
					this.BackAxis.AbsolutePosition = value - this.mTrain.AxisDistance / 2f;
				}
			}
		}

		private void LateUpdate()
		{
			if (!this.mTrain)
			{
				return;
			}
			if (this.BackAxis.Spline == this.FrontAxis.Spline && this.FrontAxis.RelativePosition > this.BackAxis.RelativePosition)
			{
				float absolutePosition = this.Waggon.AbsolutePosition;
				float absolutePosition2 = this.FrontAxis.AbsolutePosition;
				float absolutePosition3 = this.BackAxis.AbsolutePosition;
				if (Mathf.Abs(Mathf.Abs(absolutePosition2 - absolutePosition3) - this.mTrain.AxisDistance) >= this.mTrain.Limit)
				{
					float num = absolutePosition2 - absolutePosition - this.mTrain.AxisDistance / 2f;
					this.FrontAxis.TeleportBy(Mathf.Abs(-num), MovementDirectionMethods.FromInt((int)Mathf.Sign(-num)));
					float f = absolutePosition - absolutePosition3 - this.mTrain.AxisDistance / 2f;
					this.BackAxis.TeleportBy(Mathf.Abs(f), MovementDirectionMethods.FromInt((int)Mathf.Sign(f)));
				}
			}
		}

		public void setup()
		{
			this.mTrain = base.GetComponentInParent<AITrainManagerCon>();
			if (this.mTrain.Spline)
			{
				this.setController(this.Waggon, this.mTrain.Spline, this.mTrain.Speed);
				this.setController(this.FrontAxis, this.mTrain.Spline, this.mTrain.Speed);
				this.setController(this.BackAxis, this.mTrain.Spline, this.mTrain.Speed);
			}
		}

		private void setController(SplineController c, CurvySpline spline, float speed)
		{
			c.Spline = spline;
			c.Speed = speed;
			c.OnControlPointReached.AddListenerOnce(new UnityAction<CurvySplineMoveEventArgs>(this.OnCPReached));
		}

		public void OnCPReached(CurvySplineMoveEventArgs e)
		{
			MDJunctionControl metadata = e.ControlPoint.GetMetadata<MDJunctionControl>(false);
			SplineController sender = e.Sender;
			sender.ConnectionBehavior = ((!metadata || metadata.UseJunction) ? SplineControllerConnectionBehavior.RandomSpline : SplineControllerConnectionBehavior.CurrentSpline);
		}

		public SplineController Waggon;

		public SplineController FrontAxis;

		public SplineController BackAxis;

		private AITrainManagerCon mTrain;
	}
}
