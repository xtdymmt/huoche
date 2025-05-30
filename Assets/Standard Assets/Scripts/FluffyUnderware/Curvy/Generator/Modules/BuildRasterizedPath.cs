// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.BuildRasterizedPath
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Build/Rasterize Path", ModuleName = "Rasterize Path", Description = "Rasterizes a virtual path")]
	[HelpURL("https://curvyeditor.com/doclink/cgbuildrasterizedpath")]
	public class BuildRasterizedPath : CGModule
	{
		public float From
		{
			get
			{
				return this.m_Range.From;
			}
			set
			{
				float num = Mathf.Repeat(value, 1f);
				if (this.m_Range.From != num)
				{
					this.m_Range.From = num;
				}
				base.Dirty = true;
			}
		}

		public float To
		{
			get
			{
				return this.m_Range.To;
			}
			set
			{
				float num = Mathf.Max(this.From, value);
				if (this.PathIsClosed)
				{
					num = Mathf.Repeat(value, 1f);
				}
				if (this.m_Range.To != num)
				{
					this.m_Range.To = num;
				}
				base.Dirty = true;
			}
		}

		public float Length
		{
			get
			{
				return (!this.PathIsClosed) ? this.m_Range.To : (this.m_Range.To - this.m_Range.From);
			}
			set
			{
				float num = (!this.PathIsClosed) ? value : (value - this.m_Range.To);
				if (this.m_Range.To != num)
				{
					this.m_Range.To = num;
				}
				base.Dirty = true;
			}
		}

		public int Resolution
		{
			get
			{
				return this.m_Resolution;
			}
			set
			{
				int num = Mathf.Clamp(value, 1, 100);
				if (this.m_Resolution != num)
				{
					this.m_Resolution = num;
				}
				base.Dirty = true;
			}
		}

		public bool Optimize
		{
			get
			{
				return this.m_Optimize;
			}
			set
			{
				if (this.m_Optimize != value)
				{
					this.m_Optimize = value;
				}
				base.Dirty = true;
			}
		}

		public float AngleThreshold
		{
			get
			{
				return this.m_AngleTreshold;
			}
			set
			{
				float num = Mathf.Clamp(value, 0.1f, 120f);
				if (this.m_AngleTreshold != num)
				{
					this.m_AngleTreshold = num;
				}
				base.Dirty = true;
			}
		}

		public CGPath Path
		{
			get
			{
				return this.OutPath.GetData<CGPath>();
			}
		}

		public bool PathIsClosed
		{
			get
			{
				return !this.IsConfigured || this.InPath.SourceSlot(0).OnRequestPathModule.PathIsClosed;
			}
		}

		private RegionOptions<float> RangeOptions
		{
			get
			{
				if (!this.PathIsClosed)
				{
					return RegionOptions<float>.MinMax(0f, 1f);
				}
				return new RegionOptions<float>
				{
					LabelFrom = "Start",
					ClampFrom = DTValueClamping.Min,
					FromMin = 0f,
					LabelTo = "Length",
					ClampTo = DTValueClamping.Range,
					ToMin = 0f,
					ToMax = 1f
				};
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.Properties.MinWidth = 250f;
			this.Properties.LabelWidth = 100f;
		}

		public override void Reset()
		{
			base.Reset();
			this.m_Range = FloatRegion.ZeroOne;
			this.Resolution = 50;
			this.AngleThreshold = 10f;
			this.OutPath.ClearData();
		}

		public override void Refresh()
		{
			base.Refresh();
			if (this.Length == 0f)
			{
				this.Reset();
			}
			else
			{
				List<CGDataRequestParameter> list = new List<CGDataRequestParameter>();
				list.Add(new CGDataRequestRasterization(this.From, this.Length, this.Resolution, this.InPath.SourceSlot(0).OnRequestPathModule.PathLength, this.AngleThreshold, (!this.Optimize) ? CGDataRequestRasterization.ModeEnum.Even : CGDataRequestRasterization.ModeEnum.Optimized));
				CGPath data = this.InPath.GetData<CGPath>(list.ToArray());
				this.OutPath.SetData(new CGData[]
				{
					data
				});
			}
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGPath)
		}, Name = "Path", RequestDataOnly = true)]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath), DisplayName = "Rasterized Path", Name = "Path")]
		public CGModuleOutputSlot OutPath = new CGModuleOutputSlot();

		[FloatRegion(UseSlider = true, RegionOptionsPropertyName = "RangeOptions", Precision = 4)]
		[SerializeField]
		private FloatRegion m_Range = FloatRegion.ZeroOne;

		[SerializeField]
		[RangeEx(1f, 100f, "Resolution", "Defines how densely the path spline's sampling points are. When the value is 100, the number of sampling points per world distance unit is equal to the spline's Max Points Per Unit")]
		private int m_Resolution = 50;

		[SerializeField]
		private bool m_Optimize;

		[FieldCondition("m_Optimize", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[RangeEx(0.1f, 120f, "", "")]
		private float m_AngleTreshold = 10f;
	}
}
