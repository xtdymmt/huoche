// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvyMetadataBase
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[ExecuteInEditMode]
	public class CurvyMetadataBase : MonoBehaviour
	{
		public CurvySplineSegment ControlPoint
		{
			get
			{
				return this.mCP;
			}
		}

		public CurvySpline Spline
		{
			get
			{
				return (!this.mCP) ? null : this.mCP.Spline;
			}
		}

		protected virtual void Awake()
		{
			this.mCP = base.GetComponent<CurvySplineSegment>();
		}

		public T GetPreviousData<T>(bool autoCreate = true, bool segmentsOnly = true, bool useFollowUp = false) where T : MonoBehaviour, ICurvyMetadata
		{
			if (this.ControlPoint)
			{
				CurvySplineSegment controlPoint = this.ControlPoint;
				CurvySpline spline = this.Spline;
				CurvySplineSegment curvySplineSegment;
				if (!spline || spline.ControlPointsList.Count == 0)
				{
					curvySplineSegment = null;
				}
				else
				{
					curvySplineSegment = ((!useFollowUp) ? spline.GetPreviousControlPoint(controlPoint) : spline.GetPreviousControlPointUsingFollowUp(controlPoint));
					if (segmentsOnly && curvySplineSegment && !spline.IsControlPointASegment(curvySplineSegment))
					{
						curvySplineSegment = null;
					}
				}
				if (curvySplineSegment)
				{
					return curvySplineSegment.GetMetadata<T>(autoCreate);
				}
			}
			return (T)((object)null);
		}

		public T GetNextData<T>(bool autoCreate = true, bool segmentsOnly = true, bool useFollowUp = false) where T : MonoBehaviour, ICurvyMetadata
		{
			if (this.ControlPoint)
			{
				CurvySplineSegment controlPoint = this.ControlPoint;
				CurvySpline spline = this.Spline;
				CurvySplineSegment curvySplineSegment;
				if (!spline || spline.ControlPointsList.Count == 0)
				{
					curvySplineSegment = null;
				}
				else
				{
					curvySplineSegment = ((!useFollowUp) ? spline.GetNextControlPoint(controlPoint) : spline.GetNextControlPointUsingFollowUp(controlPoint));
					if (segmentsOnly && curvySplineSegment && !spline.IsControlPointASegment(curvySplineSegment))
					{
						curvySplineSegment = null;
					}
				}
				if (curvySplineSegment)
				{
					return curvySplineSegment.GetMetadata<T>(autoCreate);
				}
			}
			return (T)((object)null);
		}

		protected void NotifyModification()
		{
			CurvySpline spline = this.Spline;
			if (spline && spline.IsInitialized)
			{
				spline.NotifyMetaDataModification();
			}
		}

		private CurvySplineSegment mCP;
	}
}
