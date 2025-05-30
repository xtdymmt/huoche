// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.ImportExport.SerializedCurvySplineSegment
using System;
using JetBrains.Annotations;
using UnityEngine;

namespace FluffyUnderware.Curvy.ImportExport
{
	[Serializable]
	public class SerializedCurvySplineSegment
	{
		public SerializedCurvySplineSegment()
		{
			this.Swirl = CurvyOrientationSwirl.None;
			this.AutoHandles = true;
			this.AutoHandleDistance = 0.39f;
			this.HandleOut = CurvySplineSegmentDefaultValues.HandleOut;
			this.HandleIn = CurvySplineSegmentDefaultValues.HandleIn;
		}

		public SerializedCurvySplineSegment([NotNull] CurvySplineSegment segment, CurvySerializationSpace space)
		{
			this.Position = ((space != CurvySerializationSpace.Global) ? segment.transform.localPosition : segment.transform.position);
			this.Rotation = ((space != CurvySerializationSpace.Global) ? segment.transform.localRotation.eulerAngles : segment.transform.rotation.eulerAngles);
			this.AutoBakeOrientation = segment.AutoBakeOrientation;
			this.OrientationAnchor = segment.SerializedOrientationAnchor;
			this.Swirl = segment.Swirl;
			this.SwirlTurns = segment.SwirlTurns;
			this.AutoHandles = segment.AutoHandles;
			this.AutoHandleDistance = segment.AutoHandleDistance;
			this.HandleOut = segment.HandleOut;
			this.HandleIn = segment.HandleIn;
		}

		public void WriteIntoControlPoint([NotNull] CurvySplineSegment controlPoint, CurvySerializationSpace space)
		{
			if (space == CurvySerializationSpace.Global)
			{
				controlPoint.transform.position = this.Position;
				controlPoint.transform.rotation = Quaternion.Euler(this.Rotation);
			}
			else
			{
				controlPoint.transform.localPosition = this.Position;
				controlPoint.transform.localRotation = Quaternion.Euler(this.Rotation);
			}
			controlPoint.AutoBakeOrientation = this.AutoBakeOrientation;
			controlPoint.SerializedOrientationAnchor = this.OrientationAnchor;
			controlPoint.Swirl = this.Swirl;
			controlPoint.SwirlTurns = this.SwirlTurns;
			controlPoint.AutoHandles = this.AutoHandles;
			controlPoint.AutoHandleDistance = this.AutoHandleDistance;
			controlPoint.SetBezierHandleIn(this.HandleIn, Space.Self, CurvyBezierModeEnum.None);
			controlPoint.SetBezierHandleOut(this.HandleOut, Space.Self, CurvyBezierModeEnum.None);
		}

		public Vector3 Position;

		public Vector3 Rotation;

		public bool AutoBakeOrientation;

		public bool OrientationAnchor;

		public CurvyOrientationSwirl Swirl;

		public float SwirlTurns;

		public bool AutoHandles;

		public float AutoHandleDistance;

		public Vector3 HandleOut;

		public Vector3 HandleIn;
	}
}
