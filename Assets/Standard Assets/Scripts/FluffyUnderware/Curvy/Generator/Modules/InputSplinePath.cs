// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.InputSplinePath
using System;
using FluffyUnderware.DevTools;
using UnityEngine;
using UnityEngine.Events;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Input/Spline Path", ModuleName = "Input Spline Path", Description = "Spline Path")]
	[HelpURL("https://curvyeditor.com/doclink/cginputsplinepath")]
	public class InputSplinePath : SplineInputModuleBase, IExternalInput, IOnRequestPath, IOnRequestProcessing
	{
		public CurvySpline Spline
		{
			get
			{
				return this.m_Spline;
			}
			set
			{
				if (this.m_Spline != value)
				{
					this.m_Spline = value;
					this.OnSplineAssigned();
					this.ValidateStartAndEndCps();
				}
				base.Dirty = true;
			}
		}

		public override bool IsInitialized
		{
			get
			{
				return base.IsInitialized && (this.Spline == null || this.Spline.IsInitialized);
			}
		}

		public override bool IsConfigured
		{
			get
			{
				return base.IsConfigured && this.Spline != null;
			}
		}

		public bool SupportsIPE
		{
			get
			{
				return false;
			}
		}

		public float PathLength
		{
			get
			{
				return (!this.IsConfigured) ? 0f : base.getPathLength(this.m_Spline);
			}
		}

		public bool PathIsClosed
		{
			get
			{
				return this.IsConfigured && base.getPathClosed(this.m_Spline);
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.OnSplineAssigned();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.Spline)
			{
				this.Spline.OnRefresh.RemoveListener(new UnityAction<CurvySplineEventArgs>(this.m_Spline_OnRefresh));
			}
		}

		public override void Reset()
		{
			base.Reset();
			this.Spline = null;
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			CGDataRequestRasterization requestParameter = CGModule.GetRequestParameter<CGDataRequestRasterization>(ref requests);
			CGDataRequestMetaCGOptions requestParameter2 = CGModule.GetRequestParameter<CGDataRequestMetaCGOptions>(ref requests);
			if (requestParameter2)
			{
				if (requestParameter2.CheckMaterialID)
				{
					requestParameter2.CheckMaterialID = false;
					this.UIMessages.Add("MaterialID option not supported!");
				}
				if (requestParameter2.IncludeControlPoints)
				{
					requestParameter2.IncludeControlPoints = false;
					this.UIMessages.Add("IncludeCP option not supported!");
				}
			}
			if (!requestParameter || requestParameter.RasterizedRelativeLength == 0f)
			{
				return null;
			}
			CGData splineData = base.GetSplineData(this.Spline, true, requestParameter, requestParameter2);
			return new CGData[]
			{
				splineData
			};
		}

		public override void Refresh()
		{
			base.Refresh();
		}

		public override void OnTemplateCreated()
		{
			base.OnTemplateCreated();
			if (this.Spline && !base.IsManagedResource(this.Spline))
			{
				this.Spline = null;
			}
		}

		private void m_Spline_OnRefresh(CurvySplineEventArgs e)
		{
			if (!base.enabled || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (this.m_Spline == e.Spline)
			{
				base.Dirty = true;
			}
			else
			{
				e.Spline.OnRefresh.RemoveListener(new UnityAction<CurvySplineEventArgs>(this.m_Spline_OnRefresh));
			}
		}

		private void OnSplineAssigned()
		{
			if (this.m_Spline)
			{
				this.m_Spline.OnRefresh.AddListenerOnce(new UnityAction<CurvySplineEventArgs>(this.m_Spline_OnRefresh));
			}
		}

		protected override void ValidateStartAndEndCps()
		{
			if (this.Spline == null)
			{
				return;
			}
			if (this.m_StartCP && this.m_StartCP.Spline != this.Spline)
			{
				this.m_StartCP = null;
			}
			if (this.m_EndCP && this.m_EndCP.Spline != this.Spline)
			{
				this.m_EndCP = null;
			}
			if (this.Spline.IsInitialized && this.m_EndCP != null && this.m_StartCP != null && this.Spline.GetControlPointIndex(this.m_EndCP) <= this.Spline.GetControlPointIndex(this.m_StartCP))
			{
				this.m_EndCP = null;
			}
		}

		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath))]
		public CGModuleOutputSlot Path = new CGModuleOutputSlot();

		[Tab("General", Sort = 0)]
		[SerializeField]
		[CGResourceManager("Spline")]
		[FieldCondition("m_Spline", null, false, ActionAttribute.ActionEnum.ShowWarning, "Create or assign spline", ActionAttribute.ActionPositionEnum.Below)]
		private CurvySpline m_Spline;
	}
}
