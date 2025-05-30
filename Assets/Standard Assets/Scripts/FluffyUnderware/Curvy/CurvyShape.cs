// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvyShape
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[RequireComponent(typeof(CurvySpline))]
	[ExecuteInEditMode]
	[HelpURL("https://curvyeditor.com/doclink/curvyshape")]
	public class CurvyShape : DTVersionedMonoBehaviour
	{
		public CurvyPlane Plane
		{
			get
			{
				return this.m_Plane;
			}
			set
			{
				if (this.m_Plane != value)
				{
					this.m_Plane = value;
					this.Dirty = true;
				}
			}
		}

		public bool Persistent
		{
			get
			{
				return this.m_Persistent;
			}
			set
			{
				if (this.m_Persistent != value)
				{
					this.m_Persistent = value;
					base.hideFlags = ((!value) ? HideFlags.HideInInspector : HideFlags.None);
				}
			}
		}

		public CurvySpline Spline
		{
			get
			{
				if (!this.mSpline)
				{
					this.mSpline = base.GetComponent<CurvySpline>();
				}
				return this.mSpline;
			}
		}

		private void Update()
		{
			base.hideFlags = ((!this.Persistent) ? HideFlags.HideInInspector : HideFlags.None);
			this.Refresh();
		}

		protected virtual void Reset()
		{
			this.Plane = CurvyPlane.XY;
		}

		public void Delete()
		{
			UnityEngine.Object.Destroy(this);
		}

		public void Refresh()
		{
			if (this.Spline && this.Spline.IsInitialized && this.Dirty)
			{
				this.ApplyShape();
				this.applyPlane();
				this.Spline.SetDirtyAll();
				this.Spline.Refresh();
			}
			this.Dirty = false;
		}

		public CurvyShape Replace(string menuName)
		{
			bool persistent = this.Persistent;
			Type shapeType = CurvyShape.GetShapeType(menuName);
			if (shapeType != null)
			{
				GameObject gameObject = base.gameObject;
				this.Delete();
				CurvyShape curvyShape = (CurvyShape)gameObject.AddComponent(shapeType);
				curvyShape.Persistent = persistent;
				curvyShape.Dirty = true;
				return curvyShape;
			}
			return null;
		}

		protected void PrepareSpline(CurvyInterpolation interpolation, CurvyOrientation orientation = CurvyOrientation.Dynamic, int cachedensity = 50, bool closed = true)
		{
			this.Spline.Interpolation = interpolation;
			this.Spline.Orientation = orientation;
			this.Spline.CacheDensity = cachedensity;
			this.Spline.Closed = closed;
			this.Spline.RestrictTo2D = (this is CurvyShape2D);
		}

		protected void SetPosition(int no, Vector3 position)
		{
			this.Spline.ControlPointsList[no].SetLocalPosition(position);
		}

		protected void SetRotation(int no, Quaternion rotation)
		{
			this.Spline.ControlPointsList[no].SetLocalRotation(rotation);
		}

		protected void SetBezierHandles(int no, float distanceFrag)
		{
			this.SetBezierHandles(no, distanceFrag, distanceFrag);
		}

		protected void SetBezierHandles(int no, float inDistanceFrag, float outDistanceFrag)
		{
			CurvySplineSegment curvySplineSegment = this.Spline.ControlPointsList[no];
			if (no >= 0 && no < this.Spline.ControlPointCount)
			{
				if (inDistanceFrag == outDistanceFrag)
				{
					curvySplineSegment.AutoHandles = true;
					curvySplineSegment.AutoHandleDistance = inDistanceFrag;
				}
				else
				{
					curvySplineSegment.AutoHandles = false;
					curvySplineSegment.AutoHandleDistance = (inDistanceFrag + outDistanceFrag) / 2f;
					CurvyShape.SetBezierHandles(inDistanceFrag, true, false, new CurvySplineSegment[]
					{
						curvySplineSegment
					});
					CurvyShape.SetBezierHandles(outDistanceFrag, false, true, new CurvySplineSegment[]
					{
						curvySplineSegment
					});
				}
			}
		}

		protected void SetBezierHandles(int no, Vector3 i, Vector3 o, Space space = Space.World)
		{
			if (no >= 0 && no < this.Spline.ControlPointCount)
			{
				CurvySplineSegment curvySplineSegment = this.Spline.ControlPointsList[no];
				curvySplineSegment.AutoHandles = false;
				if (space == Space.World)
				{
					curvySplineSegment.HandleInPosition = i;
					curvySplineSegment.HandleOutPosition = o;
				}
				else
				{
					curvySplineSegment.HandleIn = i;
					curvySplineSegment.HandleOut = o;
				}
			}
		}

		public static void SetBezierHandles(float distanceFrag, bool setIn, bool setOut, params CurvySplineSegment[] controlPoints)
		{
			if (controlPoints.Length == 0)
			{
				return;
			}
			foreach (CurvySplineSegment curvySplineSegment in controlPoints)
			{
				curvySplineSegment.SetBezierHandles(distanceFrag, setIn, setOut, false);
			}
		}

		protected void SetCGHardEdges(params int[] controlPoints)
		{
			if (controlPoints.Length == 0)
			{
				for (int i = 0; i < this.Spline.ControlPointCount; i++)
				{
					this.Spline.ControlPointsList[i].GetMetadata<MetaCGOptions>(true).HardEdge = true;
				}
			}
			else
			{
				for (int j = 0; j < controlPoints.Length; j++)
				{
					if (j >= 0 && j < this.Spline.ControlPointCount)
					{
						this.Spline.ControlPointsList[j].GetMetadata<MetaCGOptions>(true).HardEdge = true;
					}
				}
			}
		}

		protected virtual void ApplyShape()
		{
		}

		protected void PrepareControlPoints(int count)
		{
			int i = count - this.Spline.ControlPointCount;
			bool flag = i != 0;
			while (i > 0)
			{
				this.Spline.InsertAfter(null, true);
				i--;
			}
			while (i < 0)
			{
				this.Spline.Delete(this.Spline.LastVisibleControlPoint, true);
				i++;
			}
			for (int j = 0; j < this.Spline.ControlPointsList.Count; j++)
			{
				CurvySplineSegment curvySplineSegment = this.Spline.ControlPointsList[j];
				curvySplineSegment.Reset();
				curvySplineSegment.Disconnect();
				MetaCGOptions metadata = curvySplineSegment.GetMetadata<MetaCGOptions>(false);
				if (metadata)
				{
					metadata.Reset();
				}
			}
			if (flag)
			{
				this.Spline.Refresh();
			}
		}

		public static Dictionary<CurvyShapeInfo, Type> ShapeDefinitions
		{
			get
			{
				if (CurvyShape.mShapeDefs.Count == 0)
				{
					CurvyShape.mShapeDefs = typeof(CurvyShape).GetAllTypesWithAttribute<CurvyShapeInfo>();
				}
				return CurvyShape.mShapeDefs;
			}
		}

		public static List<string> GetShapesMenuNames(bool only2D = false)
		{
			List<string> list = new List<string>();
			foreach (CurvyShapeInfo curvyShapeInfo in CurvyShape.ShapeDefinitions.Keys)
			{
				if (!only2D || curvyShapeInfo.Is2D)
				{
					list.Add(curvyShapeInfo.Name);
				}
			}
			return list;
		}

		public static List<string> GetShapesMenuNames(Type currentShapeType, out int currentIndex, bool only2D = false)
		{
			currentIndex = 0;
			if (currentShapeType == null)
			{
				return CurvyShape.GetShapesMenuNames(only2D);
			}
			List<string> list = new List<string>();
			foreach (KeyValuePair<CurvyShapeInfo, Type> keyValuePair in CurvyShape.ShapeDefinitions)
			{
				if (!only2D || keyValuePair.Key.Is2D)
				{
					list.Add(keyValuePair.Key.Name);
				}
				if (keyValuePair.Value == currentShapeType)
				{
					currentIndex = list.Count - 1;
				}
			}
			return list;
		}

		public static string GetShapeName(Type shapeType)
		{
			foreach (KeyValuePair<CurvyShapeInfo, Type> keyValuePair in CurvyShape.ShapeDefinitions)
			{
				if (keyValuePair.Value == shapeType)
				{
					return keyValuePair.Key.Name;
				}
			}
			return null;
		}

		public static Type GetShapeType(string menuName)
		{
			foreach (CurvyShapeInfo curvyShapeInfo in CurvyShape.ShapeDefinitions.Keys)
			{
				if (curvyShapeInfo.Name == menuName)
				{
					return CurvyShape.ShapeDefinitions[curvyShapeInfo];
				}
			}
			return null;
		}

		private void applyPlane()
		{
			CurvyPlane plane = this.Plane;
			if (plane != CurvyPlane.XZ)
			{
				if (plane != CurvyPlane.YZ)
				{
					this.applyRotation(Quaternion.Euler(0f, 0f, 0f));
				}
				else
				{
					this.applyRotation(Quaternion.Euler(0f, 90f, 0f));
				}
			}
			else
			{
				this.applyRotation(Quaternion.Euler(90f, 0f, 0f));
			}
		}

		private void applyRotation(Quaternion q)
		{
			this.Spline.transform.localRotation = Quaternion.identity;
			if (this.Spline.Interpolation == CurvyInterpolation.Bezier)
			{
				for (int i = 0; i < this.Spline.ControlPointCount; i++)
				{
					CurvySplineSegment curvySplineSegment = this.Spline.ControlPointsList[i];
					curvySplineSegment.SetLocalPosition(q * curvySplineSegment.transform.localPosition);
					curvySplineSegment.HandleIn = q * curvySplineSegment.HandleIn;
					curvySplineSegment.HandleOut = q * curvySplineSegment.HandleOut;
				}
			}
			else
			{
				for (int j = 0; j < this.Spline.ControlPointCount; j++)
				{
					CurvySplineSegment curvySplineSegment2 = this.Spline.ControlPointsList[j];
					curvySplineSegment2.SetLocalRotation(Quaternion.identity);
					curvySplineSegment2.SetLocalPosition(q * curvySplineSegment2.transform.localPosition);
				}
			}
			this.Spline.ControlPointsList[0].transform.localRotation = q;
		}

		[SerializeField]
		[Label("Plane", "")]
		private CurvyPlane m_Plane;

		[SerializeField]
		[HideInInspector]
		private bool m_Persistent = true;

		private static Dictionary<CurvyShapeInfo, Type> mShapeDefs = new Dictionary<CurvyShapeInfo, Type>();

		private CurvySpline mSpline;

		[NonSerialized]
		public bool Dirty;
	}
}
