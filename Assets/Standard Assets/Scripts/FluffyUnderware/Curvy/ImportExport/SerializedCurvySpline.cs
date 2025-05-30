// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.ImportExport.SerializedCurvySpline
using System;
using JetBrains.Annotations;
using UnityEngine;

namespace FluffyUnderware.Curvy.ImportExport
{
	[Serializable]
	public class SerializedCurvySpline
	{
		public SerializedCurvySpline()
		{
			this.Interpolation = CurvyGlobalManager.DefaultInterpolation;
			this.AutoEndTangents = true;
			this.Orientation = CurvyOrientation.Dynamic;
			this.AutoHandleDistance = 0.39f;
			this.CacheDensity = 50;
			this.MaxPointsPerUnit = 8f;
			this.UsePooling = true;
			this.CheckTransform = true;
			this.UpdateIn = CurvyUpdateMethod.Update;
			this.ControlPoints = new SerializedCurvySplineSegment[0];
		}

		public SerializedCurvySpline([NotNull] CurvySpline spline, CurvySerializationSpace space)
		{
			this.Name = spline.name;
			this.Position = ((space != CurvySerializationSpace.Local) ? spline.transform.position : spline.transform.localPosition);
			this.Rotation = ((space != CurvySerializationSpace.Local) ? spline.transform.rotation.eulerAngles : spline.transform.localRotation.eulerAngles);
			this.Interpolation = spline.Interpolation;
			this.RestrictTo2D = spline.RestrictTo2D;
			this.Closed = spline.Closed;
			this.AutoEndTangents = spline.AutoEndTangents;
			this.Orientation = spline.Orientation;
			this.AutoHandleDistance = spline.AutoHandleDistance;
			this.CacheDensity = spline.CacheDensity;
			this.MaxPointsPerUnit = spline.MaxPointsPerUnit;
			this.UsePooling = spline.UsePooling;
			this.UseThreading = spline.UseThreading;
			this.CheckTransform = spline.CheckTransform;
			this.UpdateIn = spline.UpdateIn;
			this.ControlPoints = new SerializedCurvySplineSegment[spline.ControlPointCount];
			for (int i = 0; i < spline.ControlPointCount; i++)
			{
				this.ControlPoints[i] = new SerializedCurvySplineSegment(spline.ControlPointsList[i], space);
			}
		}

		public void WriteIntoSpline([NotNull] CurvySpline deserializedSpline, CurvySerializationSpace space)
		{
			deserializedSpline.name = this.Name;
			if (space == CurvySerializationSpace.Local)
			{
				deserializedSpline.transform.localPosition = this.Position;
				deserializedSpline.transform.localRotation = Quaternion.Euler(this.Rotation);
			}
			else
			{
				deserializedSpline.transform.position = this.Position;
				deserializedSpline.transform.rotation = Quaternion.Euler(this.Rotation);
			}
			deserializedSpline.Interpolation = this.Interpolation;
			deserializedSpline.RestrictTo2D = this.RestrictTo2D;
			deserializedSpline.Closed = this.Closed;
			deserializedSpline.AutoEndTangents = this.AutoEndTangents;
			deserializedSpline.Orientation = this.Orientation;
			deserializedSpline.AutoHandleDistance = this.AutoHandleDistance;
			deserializedSpline.CacheDensity = this.CacheDensity;
			deserializedSpline.MaxPointsPerUnit = this.MaxPointsPerUnit;
			deserializedSpline.UsePooling = this.UsePooling;
			deserializedSpline.UseThreading = this.UseThreading;
			deserializedSpline.CheckTransform = this.CheckTransform;
			deserializedSpline.UpdateIn = this.UpdateIn;
			foreach (SerializedCurvySplineSegment serializedCurvySplineSegment in this.ControlPoints)
			{
				serializedCurvySplineSegment.WriteIntoControlPoint(deserializedSpline.InsertAfter(null, true), space);
			}
			deserializedSpline.SetDirtyAll();
		}

		public string Name;

		public Vector3 Position;

		public Vector3 Rotation;

		public CurvyInterpolation Interpolation;

		public bool RestrictTo2D;

		public bool Closed;

		public bool AutoEndTangents;

		public CurvyOrientation Orientation;

		public float AutoHandleDistance;

		public int CacheDensity;

		public float MaxPointsPerUnit;

		public bool UsePooling;

		public bool UseThreading;

		public bool CheckTransform;

		public CurvyUpdateMethod UpdateIn;

		public SerializedCurvySplineSegment[] ControlPoints;
	}
}
