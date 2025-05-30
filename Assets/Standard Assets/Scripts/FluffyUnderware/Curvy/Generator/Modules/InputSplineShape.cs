// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.InputSplineShape
using System;
using FluffyUnderware.Curvy.Shapes;
using FluffyUnderware.DevTools;
using UnityEngine;
using UnityEngine.Events;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Input/Spline Shape", ModuleName = "Input Spline Shape", Description = "Spline Shape")]
	[HelpURL("https://curvyeditor.com/doclink/cginputsplineshape")]
	public class InputSplineShape : SplineInputModuleBase, IExternalInput, IOnRequestPath, IOnRequestProcessing
	{
		public CurvySpline Shape
		{
			get
			{
				return this.m_Shape;
			}
			set
			{
				if (this.m_Shape != value)
				{
					this.m_Shape = value;
					this.OnShapeAssigned();
					this.ValidateStartAndEndCps();
				}
				base.Dirty = true;
			}
		}

		public bool FreeForm
		{
			get
			{
				return this.Shape != null && this.Shape.GetComponent<CurvyShape>() == null;
			}
			set
			{
				if (this.Shape != null)
				{
					CurvyShape component = this.Shape.GetComponent<CurvyShape>();
					if (value && component != null)
					{
						component.Delete();
					}
					else if (!value && component == null)
					{
						this.Shape.gameObject.AddComponent<CSCircle>();
					}
				}
			}
		}

		public override bool IsInitialized
		{
			get
			{
				return base.IsInitialized && (this.Shape == null || this.Shape.IsInitialized);
			}
		}

		public override bool IsConfigured
		{
			get
			{
				return base.IsConfigured && this.Shape != null;
			}
		}

		public float PathLength
		{
			get
			{
				return (!this.IsConfigured) ? 0f : base.getPathLength(this.m_Shape);
			}
		}

		public bool PathIsClosed
		{
			get
			{
				return this.IsConfigured && base.getPathClosed(this.m_Shape);
			}
		}

		public bool SupportsIPE
		{
			get
			{
				return this.FreeForm;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.OnShapeAssigned();
			base.Dirty = true;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
		}

		public override void Reset()
		{
			base.Reset();
			this.Shape = null;
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			CGDataRequestRasterization requestParameter = CGModule.GetRequestParameter<CGDataRequestRasterization>(ref requests);
			CGDataRequestMetaCGOptions requestParameter2 = CGModule.GetRequestParameter<CGDataRequestMetaCGOptions>(ref requests);
			if (!requestParameter || requestParameter.RasterizedRelativeLength == 0f)
			{
				return null;
			}
			CGData splineData = base.GetSplineData(this.Shape, false, requestParameter, requestParameter2);
			return new CGData[]
			{
				splineData
			};
		}

		public T SetManagedShape<T>() where T : CurvyShape2D
		{
			if (!this.Shape)
			{
				this.Shape = (CurvySpline)base.AddManagedResource("Shape", string.Empty, -1);
			}
			CurvyShape component = this.Shape.GetComponent<CurvyShape>();
			if (component != null)
			{
				component.Delete();
			}
			return this.Shape.gameObject.AddComponent<T>();
		}

		public void RemoveManagedShape()
		{
			if (this.Shape)
			{
				base.DeleteManagedResource("Shape", this.Shape, string.Empty, false);
			}
		}

		private void m_Shape_OnRefresh(CurvySplineEventArgs e)
		{
			if (!base.enabled || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (this.m_Shape == e.Spline)
			{
				base.Dirty = true;
			}
			else
			{
				e.Spline.OnRefresh.RemoveListener(new UnityAction<CurvySplineEventArgs>(this.m_Shape_OnRefresh));
			}
		}

		private void OnShapeAssigned()
		{
			if (this.m_Shape)
			{
				this.m_Shape.OnRefresh.AddListenerOnce(new UnityAction<CurvySplineEventArgs>(this.m_Shape_OnRefresh));
				this.m_Shape.RestrictTo2D = true;
			}
		}

		protected override void ValidateStartAndEndCps()
		{
			if (this.Shape == null)
			{
				return;
			}
			if (this.m_StartCP && this.m_StartCP.Spline != this.Shape)
			{
				this.m_StartCP = null;
			}
			if (this.m_EndCP && this.m_EndCP.Spline != this.Shape)
			{
				this.m_EndCP = null;
			}
			if (this.Shape.IsInitialized && this.m_EndCP != null && this.m_StartCP != null && this.Shape.GetControlPointIndex(this.m_EndCP) <= this.Shape.GetControlPointIndex(this.m_StartCP))
			{
				this.m_EndCP = null;
			}
		}

		[HideInInspector]
		[OutputSlotInfo(typeof(CGShape))]
		public CGModuleOutputSlot OutShape = new CGModuleOutputSlot();

		[Tab("General", Sort = 0)]
		[SerializeField]
		[CGResourceManager("Shape")]
		private CurvySpline m_Shape;
	}
}
