// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvyGlobalManager
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(PoolManager))]
	public class CurvyGlobalManager : DTSingleton<CurvyGlobalManager>
	{
		public static bool ShowCurveGizmo
		{
			get
			{
				return (CurvyGlobalManager.Gizmos & CurvySplineGizmos.Curve) == CurvySplineGizmos.Curve;
			}
			set
			{
				if (value)
				{
					CurvyGlobalManager.Gizmos |= CurvySplineGizmos.Curve;
				}
				else
				{
					CurvyGlobalManager.Gizmos &= ~CurvySplineGizmos.Curve;
				}
			}
		}

		public static bool ShowApproximationGizmo
		{
			get
			{
				return (CurvyGlobalManager.Gizmos & CurvySplineGizmos.Approximation) == CurvySplineGizmos.Approximation;
			}
			set
			{
				if (value)
				{
					CurvyGlobalManager.Gizmos |= CurvySplineGizmos.Approximation;
				}
				else
				{
					CurvyGlobalManager.Gizmos &= ~CurvySplineGizmos.Approximation;
				}
			}
		}

		public static bool ShowTangentsGizmo
		{
			get
			{
				return (CurvyGlobalManager.Gizmos & CurvySplineGizmos.Tangents) == CurvySplineGizmos.Tangents;
			}
			set
			{
				if (value)
				{
					CurvyGlobalManager.Gizmos |= CurvySplineGizmos.Tangents;
				}
				else
				{
					CurvyGlobalManager.Gizmos &= ~CurvySplineGizmos.Tangents;
				}
			}
		}

		public static bool ShowOrientationGizmo
		{
			get
			{
				return (CurvyGlobalManager.Gizmos & CurvySplineGizmos.Orientation) == CurvySplineGizmos.Orientation;
			}
			set
			{
				if (value)
				{
					CurvyGlobalManager.Gizmos |= CurvySplineGizmos.Orientation;
				}
				else
				{
					CurvyGlobalManager.Gizmos &= ~CurvySplineGizmos.Orientation;
				}
			}
		}

		public static bool ShowLabelsGizmo
		{
			get
			{
				return (CurvyGlobalManager.Gizmos & CurvySplineGizmos.Labels) == CurvySplineGizmos.Labels;
			}
			set
			{
				if (value)
				{
					CurvyGlobalManager.Gizmos |= CurvySplineGizmos.Labels;
				}
				else
				{
					CurvyGlobalManager.Gizmos &= ~CurvySplineGizmos.Labels;
				}
			}
		}

		public static bool ShowMetadataGizmo
		{
			get
			{
				return (CurvyGlobalManager.Gizmos & CurvySplineGizmos.Metadata) == CurvySplineGizmos.Metadata;
			}
			set
			{
				if (value)
				{
					CurvyGlobalManager.Gizmos |= CurvySplineGizmos.Metadata;
				}
				else
				{
					CurvyGlobalManager.Gizmos &= ~CurvySplineGizmos.Metadata;
				}
			}
		}

		public static bool ShowBoundsGizmo
		{
			get
			{
				return (CurvyGlobalManager.Gizmos & CurvySplineGizmos.Bounds) == CurvySplineGizmos.Bounds;
			}
			set
			{
				if (value)
				{
					CurvyGlobalManager.Gizmos |= CurvySplineGizmos.Bounds;
				}
				else
				{
					CurvyGlobalManager.Gizmos &= ~CurvySplineGizmos.Bounds;
				}
			}
		}

		public PoolManager PoolManager
		{
			get
			{
				if (this.mPoolManager == null)
				{
					this.mPoolManager = base.GetComponent<PoolManager>();
				}
				return this.mPoolManager;
			}
		}

		public ComponentPool ControlPointPool
		{
			get
			{
				return this.mControlPointPool;
			}
		}

		public CurvyConnection[] Connections
		{
			get
			{
				return base.GetComponentsInChildren<CurvyConnection>();
			}
		}

		public CurvyConnection[] GetContainingConnections(params CurvySpline[] splines)
		{
			List<CurvyConnection> list = new List<CurvyConnection>();
			List<CurvySpline> list2 = new List<CurvySpline>(splines);
			foreach (CurvySpline curvySpline in list2)
			{
				foreach (CurvySplineSegment curvySplineSegment in curvySpline.ControlPointsList)
				{
					if (curvySplineSegment.Connection != null && !list.Contains(curvySplineSegment.Connection))
					{
						bool flag = true;
						foreach (CurvySplineSegment curvySplineSegment2 in curvySplineSegment.Connection.ControlPointsList)
						{
							if (curvySplineSegment2.Spline != null && !list2.Contains(curvySplineSegment2.Spline))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							list.Add(curvySplineSegment.Connection);
						}
					}
				}
			}
			return list.ToArray();
		}

		public override void Awake()
		{
			base.Awake();
			base.name = "_CurvyGlobal_";
			base.transform.SetAsLastSibling();
			if (Application.isPlaying)
			{
				UnityEngine.Object.DontDestroyOnLoad(this);
			}
			this.mPoolManager = base.GetComponent<PoolManager>();
			PoolSettings settings = new PoolSettings
			{
				MinItems = 0,
				Threshold = 50,
				Prewarm = true,
				AutoCreate = true,
				AutoEnableDisable = true
			};
			this.mControlPointPool = this.mPoolManager.CreateComponentPool<CurvySplineSegment>(settings);
		}

		private void Start()
		{
			if (CurvyGlobalManager.HideManager)
			{
				base.gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
			else
			{
				base.gameObject.hideFlags = HideFlags.None;
			}
		}

		[RuntimeInitializeOnLoadMethod]
		private static void LoadRuntimeSettings()
		{
			if (!PlayerPrefs.HasKey("Curvy_MaxCachePPU"))
			{
				CurvyGlobalManager.SaveRuntimeSettings();
			}
			CurvyGlobalManager.SceneViewResolution = DTUtility.GetPlayerPrefs<float>("Curvy_SceneViewResolution", CurvyGlobalManager.SceneViewResolution);
			CurvyGlobalManager.HideManager = DTUtility.GetPlayerPrefs<bool>("Curvy_HideManager", CurvyGlobalManager.HideManager);
			CurvyGlobalManager.DefaultGizmoColor = DTUtility.GetPlayerPrefs<Color>("Curvy_DefaultGizmoColor", CurvyGlobalManager.DefaultGizmoColor);
			CurvyGlobalManager.DefaultGizmoSelectionColor = DTUtility.GetPlayerPrefs<Color>("Curvy_DefaultGizmoSelectionColor", CurvyGlobalManager.DefaultGizmoColor);
			CurvyGlobalManager.DefaultInterpolation = DTUtility.GetPlayerPrefs<CurvyInterpolation>("Curvy_DefaultInterpolation", CurvyGlobalManager.DefaultInterpolation);
			CurvyGlobalManager.GizmoControlPointSize = DTUtility.GetPlayerPrefs<float>("Curvy_ControlPointSize", CurvyGlobalManager.GizmoControlPointSize);
			CurvyGlobalManager.GizmoOrientationLength = DTUtility.GetPlayerPrefs<float>("Curvy_OrientationLength", CurvyGlobalManager.GizmoOrientationLength);
			CurvyGlobalManager.GizmoOrientationColor = DTUtility.GetPlayerPrefs<Color>("Curvy_OrientationColor", CurvyGlobalManager.GizmoOrientationColor);
			CurvyGlobalManager.Gizmos = DTUtility.GetPlayerPrefs<CurvySplineGizmos>("Curvy_Gizmos", CurvyGlobalManager.Gizmos);
			CurvyGlobalManager.SplineLayer = DTUtility.GetPlayerPrefs<int>("Curvy_SplineLayer", CurvyGlobalManager.SplineLayer);
		}

		public static void SaveRuntimeSettings()
		{
			DTUtility.SetPlayerPrefs<float>("Curvy_SceneViewResolution", CurvyGlobalManager.SceneViewResolution);
			DTUtility.SetPlayerPrefs<bool>("Curvy_HideManager", CurvyGlobalManager.HideManager);
			DTUtility.SetPlayerPrefs<Color>("Curvy_DefaultGizmoColor", CurvyGlobalManager.DefaultGizmoColor);
			DTUtility.SetPlayerPrefs<Color>("Curvy_DefaultGizmoSelectionColor", CurvyGlobalManager.DefaultGizmoSelectionColor);
			DTUtility.SetPlayerPrefs<CurvyInterpolation>("Curvy_DefaultInterpolation", CurvyGlobalManager.DefaultInterpolation);
			DTUtility.SetPlayerPrefs<float>("Curvy_ControlPointSize", CurvyGlobalManager.GizmoControlPointSize);
			DTUtility.SetPlayerPrefs<float>("Curvy_OrientationLength", CurvyGlobalManager.GizmoOrientationLength);
			DTUtility.SetPlayerPrefs<Color>("Curvy_OrientationColor", CurvyGlobalManager.GizmoOrientationColor);
			DTUtility.SetPlayerPrefs<CurvySplineGizmos>("Curvy_Gizmos", CurvyGlobalManager.Gizmos);
			DTUtility.SetPlayerPrefs<int>("Curvy_SplineLayer", CurvyGlobalManager.SplineLayer);
			PlayerPrefs.Save();
		}

		public override void MergeDoubleLoaded(IDTSingleton newInstance)
		{
			base.MergeDoubleLoaded(newInstance);
			CurvyGlobalManager curvyGlobalManager = newInstance as CurvyGlobalManager;
			CurvyConnection[] connections = curvyGlobalManager.Connections;
			for (int i = 0; i < connections.Length; i++)
			{
				connections[i].transform.SetParent(base.transform);
			}
		}

		public static bool HideManager = false;

		public static float SceneViewResolution = 0.5f;

		public static Color DefaultGizmoColor = new Color(0.71f, 0.71f, 0.71f);

		public static Color DefaultGizmoSelectionColor = new Color(0.15f, 0.35f, 0.68f);

		public static CurvyInterpolation DefaultInterpolation = CurvyInterpolation.CatmullRom;

		public static float GizmoControlPointSize = 0.15f;

		public static float GizmoOrientationLength = 1f;

		public static Color GizmoOrientationColor = new Color(0.75f, 0.75f, 0.4f);

		public static int SplineLayer = 0;

		public static CurvySplineGizmos Gizmos = CurvySplineGizmos.Curve | CurvySplineGizmos.Orientation;

		private PoolManager mPoolManager;

		private ComponentPool mControlPointPool;
	}
}
