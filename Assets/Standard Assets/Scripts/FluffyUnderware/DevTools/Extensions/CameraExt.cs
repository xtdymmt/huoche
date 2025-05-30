// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.Extensions.CameraExt
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools.Extensions
{
	public static class CameraExt
	{
		public static bool BoundsInView(this Camera c, Bounds bounds)
		{
			if (CameraExt.camPos != c.transform.position || CameraExt.camForward != c.transform.forward || CameraExt.screenW != (float)Screen.width || CameraExt.screenH != (float)Screen.height || CameraExt.fov != c.fieldOfView)
			{
				CameraExt.camPos = c.transform.position;
				CameraExt.camForward = c.transform.forward;
				CameraExt.screenW = (float)Screen.width;
				CameraExt.screenH = (float)Screen.height;
				CameraExt.fov = c.fieldOfView;
				GeometryUtility.CalculateFrustumPlanes(c, CameraExt.camPlanes);
			}
			return GeometryUtility.TestPlanesAABB(CameraExt.camPlanes, bounds);
		}

		public static bool BoundsPartiallyInView(this Camera c, Bounds bounds)
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(c);
			Vector3 zero = Vector3.zero;
			Vector3 center = bounds.center;
			Vector3 extents = bounds.extents;
			zero.Set(center.x - extents.x, center.y + extents.y, center.z - extents.z);
			if (GeometryUtility.TestPlanesAABB(planes, new Bounds(zero, new Vector3(0.1f, 0.1f, 0.1f))))
			{
				return true;
			}
			zero.Set(center.x + extents.x, center.y + extents.y, center.z - extents.z);
			if (GeometryUtility.TestPlanesAABB(planes, new Bounds(zero, new Vector3(0.1f, 0.1f, 0.1f))))
			{
				return true;
			}
			zero.Set(center.x - extents.x, center.y - extents.y, center.z - extents.z);
			if (GeometryUtility.TestPlanesAABB(planes, new Bounds(zero, new Vector3(0.1f, 0.1f, 0.1f))))
			{
				return true;
			}
			zero.Set(center.x + extents.x, center.y - extents.y, center.z - extents.z);
			if (GeometryUtility.TestPlanesAABB(planes, new Bounds(zero, new Vector3(0.1f, 0.1f, 0.1f))))
			{
				return true;
			}
			zero.Set(center.x - extents.x, center.y + extents.y, center.z + extents.z);
			if (GeometryUtility.TestPlanesAABB(planes, new Bounds(zero, new Vector3(0.1f, 0.1f, 0.1f))))
			{
				return true;
			}
			zero.Set(center.x + extents.x, center.y + extents.y, center.z + extents.z);
			if (GeometryUtility.TestPlanesAABB(planes, new Bounds(zero, new Vector3(0.1f, 0.1f, 0.1f))))
			{
				return true;
			}
			zero.Set(center.x - extents.x, center.y - extents.y, center.z + extents.z);
			if (GeometryUtility.TestPlanesAABB(planes, new Bounds(zero, new Vector3(0.1f, 0.1f, 0.1f))))
			{
				return true;
			}
			zero.Set(center.x + extents.x, center.y - extents.y, center.z + extents.z);
			return GeometryUtility.TestPlanesAABB(planes, new Bounds(zero, new Vector3(0.1f, 0.1f, 0.1f)));
		}

		private static Plane[] camPlanes = new Plane[6];

		private static Vector3 camPos;

		private static Vector3 camForward;

		private static float fov;

		private static float screenW;

		private static float screenH;
	}
}
