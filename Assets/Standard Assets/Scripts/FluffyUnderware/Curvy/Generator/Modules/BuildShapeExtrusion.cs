// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.BuildShapeExtrusion
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Build/Shape Extrusion", ModuleName = "Shape Extrusion", Description = "Simple Shape Extrusion")]
	[HelpURL("https://curvyeditor.com/doclink/cgbuildshapeextrusion")]
	public class BuildShapeExtrusion : CGModule
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
				if (this.ClampPath)
				{
					num = DTMath.Repeat(value, 1f);
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
				return (!this.ClampPath) ? this.m_Range.To : (this.m_Range.To - this.m_Range.From);
			}
			set
			{
				float num = (!this.ClampPath) ? value : (value - this.m_Range.To);
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
				return this.m_AngleThreshold;
			}
			set
			{
				float num = Mathf.Clamp(value, 0.1f, 120f);
				if (this.m_AngleThreshold != num)
				{
					this.m_AngleThreshold = num;
				}
				base.Dirty = true;
			}
		}

		public float CrossFrom
		{
			get
			{
				return this.m_CrossRange.From;
			}
			set
			{
				float num = Mathf.Repeat(value, 1f);
				if (this.m_CrossRange.From != num)
				{
					this.m_CrossRange.From = num;
				}
				base.Dirty = true;
			}
		}

		public float CrossTo
		{
			get
			{
				return this.m_CrossRange.To;
			}
			set
			{
				float num = Mathf.Max(this.CrossFrom, value);
				if (this.ClampCross)
				{
					num = DTMath.Repeat(value, 1f);
				}
				if (this.m_CrossRange.To != num)
				{
					this.m_CrossRange.To = num;
				}
				base.Dirty = true;
			}
		}

		public float CrossLength
		{
			get
			{
				return (!this.ClampCross) ? this.m_CrossRange.To : (this.m_CrossRange.To - this.m_CrossRange.From);
			}
			set
			{
				float num = (!this.ClampCross) ? value : (value - this.m_CrossRange.To);
				if (this.m_CrossRange.To != num)
				{
					this.m_CrossRange.To = num;
				}
				base.Dirty = true;
			}
		}

		public int CrossResolution
		{
			get
			{
				return this.m_CrossResolution;
			}
			set
			{
				int num = Mathf.Clamp(value, 1, 100);
				if (this.m_CrossResolution != num)
				{
					this.m_CrossResolution = num;
				}
				base.Dirty = true;
			}
		}

		public bool CrossOptimize
		{
			get
			{
				return this.m_CrossOptimize;
			}
			set
			{
				if (this.m_CrossOptimize != value)
				{
					this.m_CrossOptimize = value;
				}
				base.Dirty = true;
			}
		}

		public float CrossAngleThreshold
		{
			get
			{
				return this.m_CrossAngleThreshold;
			}
			set
			{
				float num = Mathf.Clamp(value, 0.1f, 120f);
				if (this.m_CrossAngleThreshold != num)
				{
					this.m_CrossAngleThreshold = num;
				}
				base.Dirty = true;
			}
		}

		public bool CrossIncludeControlPoints
		{
			get
			{
				return this.m_CrossIncludeControlpoints;
			}
			set
			{
				if (this.m_CrossIncludeControlpoints != value)
				{
					this.m_CrossIncludeControlpoints = value;
				}
				base.Dirty = true;
			}
		}

		public bool CrossHardEdges
		{
			get
			{
				return this.m_CrossHardEdges;
			}
			set
			{
				if (this.m_CrossHardEdges != value)
				{
					this.m_CrossHardEdges = value;
				}
				base.Dirty = true;
			}
		}

		public bool CrossMaterials
		{
			get
			{
				return this.m_CrossMaterials;
			}
			set
			{
				if (this.m_CrossMaterials != value)
				{
					this.m_CrossMaterials = value;
				}
				base.Dirty = true;
			}
		}

		public bool CrossExtendedUV
		{
			get
			{
				return this.m_CrossExtendedUV;
			}
			set
			{
				if (this.m_CrossExtendedUV != value)
				{
					this.m_CrossExtendedUV = value;
				}
				base.Dirty = true;
			}
		}

		public BuildShapeExtrusion.CrossShiftModeEnum CrossShiftMode
		{
			get
			{
				return this.m_CrossShiftMode;
			}
			set
			{
				if (this.m_CrossShiftMode != value)
				{
					this.m_CrossShiftMode = value;
				}
				base.Dirty = true;
			}
		}

		public float CrossShiftValue
		{
			get
			{
				return this.m_CrossShiftValue;
			}
			set
			{
				float num = Mathf.Repeat(value, 1f);
				if (this.m_CrossShiftValue != num)
				{
					this.m_CrossShiftValue = num;
				}
				base.Dirty = true;
			}
		}

		public bool CrossReverseNormals
		{
			get
			{
				return this.m_CrossReverseNormals;
			}
			set
			{
				if (this.m_CrossReverseNormals != value)
				{
					this.m_CrossReverseNormals = value;
				}
				base.Dirty = true;
			}
		}

		public BuildShapeExtrusion.ScaleModeEnum ScaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				if (this.m_ScaleMode != value)
				{
					this.m_ScaleMode = value;
				}
				base.Dirty = true;
			}
		}

		public CGReferenceMode ScaleReference
		{
			get
			{
				return this.m_ScaleReference;
			}
			set
			{
				if (this.m_ScaleReference != value)
				{
					this.m_ScaleReference = value;
				}
				base.Dirty = true;
			}
		}

		public bool ScaleUniform
		{
			get
			{
				return this.m_ScaleUniform;
			}
			set
			{
				if (this.m_ScaleUniform != value)
				{
					this.m_ScaleUniform = value;
				}
				base.Dirty = true;
			}
		}

		public float ScaleOffset
		{
			get
			{
				return this.m_ScaleOffset;
			}
			set
			{
				if (this.m_ScaleOffset != value)
				{
					this.m_ScaleOffset = value;
				}
				base.Dirty = true;
			}
		}

		public float ScaleX
		{
			get
			{
				return this.m_ScaleX;
			}
			set
			{
				if (this.m_ScaleX != value)
				{
					this.m_ScaleX = value;
				}
				base.Dirty = true;
			}
		}

		public float ScaleY
		{
			get
			{
				return this.m_ScaleY;
			}
			set
			{
				if (this.m_ScaleY != value)
				{
					this.m_ScaleY = value;
				}
				base.Dirty = true;
			}
		}

		public AnimationCurve ScaleMultiplierX
		{
			get
			{
				return this.m_ScaleCurveX;
			}
			set
			{
				if (this.m_ScaleCurveX != value)
				{
					this.m_ScaleCurveX = value;
				}
				base.Dirty = true;
			}
		}

		public AnimationCurve ScaleMultiplierY
		{
			get
			{
				return this.m_ScaleCurveY;
			}
			set
			{
				if (this.m_ScaleCurveY != value)
				{
					this.m_ScaleCurveY = value;
				}
				base.Dirty = true;
			}
		}

		public float HollowInset
		{
			get
			{
				return this.m_HollowInset;
			}
			set
			{
				float num = Mathf.Clamp01(value);
				if (this.m_HollowInset != num)
				{
					this.m_HollowInset = num;
				}
				base.Dirty = true;
			}
		}

		public bool HollowReverseNormals
		{
			get
			{
				return this.m_HollowReverseNormals;
			}
			set
			{
				if (this.m_HollowReverseNormals != value)
				{
					this.m_HollowReverseNormals = value;
				}
				base.Dirty = true;
			}
		}

		public int PathSamples { get; private set; }

		public int CrossSamples { get; private set; }

		public int CrossGroups { get; private set; }

		public IExternalInput Cross
		{
			get
			{
				return (!this.IsConfigured) ? null : this.InCross.SourceSlot(0).ExternalInput;
			}
		}

		public Vector3 CrossPosition { get; protected set; }

		public Quaternion CrossRotation { get; protected set; }

		private bool ClampPath
		{
			get
			{
				return !this.InPath.IsLinked || !this.InPath.SourceSlot(0).OnRequestPathModule.PathIsClosed;
			}
		}

		private bool ClampCross
		{
			get
			{
				return !this.InCross.IsLinked || !this.InCross.SourceSlot(0).OnRequestPathModule.PathIsClosed;
			}
		}

		private RegionOptions<float> RangeOptions
		{
			get
			{
				if (this.ClampPath)
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

		private RegionOptions<float> CrossRangeOptions
		{
			get
			{
				if (this.ClampCross)
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
			this.Properties.MinWidth = 270f;
			this.Properties.LabelWidth = 100f;
		}

		public override void Reset()
		{
			base.Reset();
			this.From = 0f;
			this.To = 1f;
			this.Resolution = 50;
			this.AngleThreshold = 10f;
			this.Optimize = true;
			this.CrossFrom = 0f;
			this.CrossTo = 1f;
			this.CrossResolution = 50;
			this.CrossAngleThreshold = 10f;
			this.CrossOptimize = true;
			this.CrossIncludeControlPoints = false;
			this.CrossHardEdges = false;
			this.CrossMaterials = false;
			this.CrossShiftMode = BuildShapeExtrusion.CrossShiftModeEnum.ByOrientation;
			this.ScaleMode = BuildShapeExtrusion.ScaleModeEnum.Simple;
			this.ScaleUniform = true;
			this.ScaleX = 1f;
			this.ScaleY = 1f;
			this.ScaleMultiplierX = AnimationCurve.Linear(0f, 1f, 1f, 1f);
			this.ScaleMultiplierY = AnimationCurve.Linear(0f, 1f, 1f, 1f);
			this.HollowInset = 0f;
		}

		public override void Refresh()
		{
			base.Refresh();
			if (this.Length == 0f)
			{
				this.OutVolume.SetData(null);
				this.OutVolumeHollow.SetData(null);
			}
			else
			{
				List<CGDataRequestParameter> list = new List<CGDataRequestParameter>();
				list.Add(new CGDataRequestRasterization(this.From, this.Length, this.Resolution, this.InPath.SourceSlot(0).OnRequestPathModule.PathLength, this.AngleThreshold, (!this.Optimize) ? CGDataRequestRasterization.ModeEnum.Even : CGDataRequestRasterization.ModeEnum.Optimized));
				CGPath data = this.InPath.GetData<CGPath>(list.ToArray());
				list.Clear();
				list.Add(new CGDataRequestRasterization(this.CrossFrom, this.CrossLength, this.CrossResolution, this.InCross.SourceSlot(0).OnRequestPathModule.PathLength, this.CrossAngleThreshold, (!this.CrossOptimize) ? CGDataRequestRasterization.ModeEnum.Even : CGDataRequestRasterization.ModeEnum.Optimized));
				if (this.CrossIncludeControlPoints || this.CrossHardEdges || this.CrossMaterials)
				{
					list.Add(new CGDataRequestMetaCGOptions(this.CrossHardEdges, this.CrossMaterials, this.CrossIncludeControlPoints, this.CrossExtendedUV));
				}
				CGShape data2 = this.InCross.GetData<CGShape>(list.ToArray());
				if (!data || !data2 || data.Count == 0 || data2.Count == 0)
				{
					this.OutVolume.ClearData();
					this.OutVolumeHollow.ClearData();
					return;
				}
				CGVolume cgvolume = CGVolume.Get(this.OutVolume.GetData<CGVolume>(), data, data2);
				CGVolume cgvolume2 = (!this.OutVolumeHollow.IsLinked) ? null : CGVolume.Get(this.OutVolumeHollow.GetData<CGVolume>(), data, data2);
				this.PathSamples = data.Count;
				this.CrossSamples = data2.Count;
				this.CrossGroups = data2.MaterialGroups.Count;
				this.CrossPosition = cgvolume.Position[0];
				this.CrossRotation = Quaternion.LookRotation(cgvolume.Direction[0], cgvolume.Normal[0]);
				Vector3 vector = (!this.ScaleUniform) ? new Vector3(this.ScaleX, this.ScaleY, 1f) : new Vector3(this.ScaleX, this.ScaleX, 1f);
				Vector3 vector2 = vector;
				int num = 0;
				float[] array = (this.ScaleReference != CGReferenceMode.Source) ? data.F : data.SourceF;
				float num2 = (float)((!this.CrossReverseNormals) ? 1 : -1);
				float num3 = (float)((!this.HollowReverseNormals) ? 1 : -1);
				for (int i = 0; i < data.Count; i++)
				{
					Quaternion quaternion = Quaternion.LookRotation(data.Direction[i], data.Normal[i]);
					this.getScaleInternal(array[i], vector, ref vector2);
					Matrix4x4 matrix4x = Matrix4x4.TRS(data.Position[i], quaternion, vector2);
					Matrix4x4 matrix4x2 = (!cgvolume2) ? default(Matrix4x4) : Matrix4x4.TRS(data.Position[i], quaternion, vector2 * (1f - this.HollowInset));
					for (int j = 0; j < data2.Count; j++)
					{
						cgvolume.Vertex[num] = matrix4x.MultiplyPoint(data2.Position[j]);
						Vector3 vector3 = quaternion * data2.Normal[j];
						cgvolume.VertexNormal[num].x = vector3.x * num2;
						cgvolume.VertexNormal[num].y = vector3.y * num2;
						cgvolume.VertexNormal[num].z = vector3.z * num2;
						if (cgvolume2)
						{
							cgvolume2.Vertex[num] = matrix4x2.MultiplyPoint(data2.Position[j]);
							cgvolume2.VertexNormal[num].x = vector3.x * num3;
							cgvolume2.VertexNormal[num].y = vector3.y * num3;
							cgvolume2.VertexNormal[num].z = vector3.z * num3;
						}
						num++;
					}
				}
				BuildShapeExtrusion.CrossShiftModeEnum crossShiftMode = this.CrossShiftMode;
				if (crossShiftMode != BuildShapeExtrusion.CrossShiftModeEnum.ByOrientation)
				{
					if (crossShiftMode != BuildShapeExtrusion.CrossShiftModeEnum.Custom)
					{
						cgvolume.CrossFShift = 0f;
					}
					else
					{
						cgvolume.CrossFShift = this.CrossShiftValue;
					}
				}
				else
				{
					Vector2 vector4;
					float num4;
					for (int k = 0; k < data2.Count - 1; k++)
					{
						if (DTMath.RayLineSegmentIntersection(cgvolume.Position[0], cgvolume.VertexNormal[0], cgvolume.Vertex[k], cgvolume.Vertex[k + 1], out vector4, out num4))
						{
							cgvolume.CrossFShift = DTMath.SnapPrecision(cgvolume.CrossF[k] + (cgvolume.CrossF[k + 1] - cgvolume.CrossF[k]) * num4, 2);
							break;
						}
					}
					if (cgvolume.CrossClosed && DTMath.RayLineSegmentIntersection(cgvolume.Position[0], cgvolume.VertexNormal[0], cgvolume.Vertex[data2.Count - 1], cgvolume.Vertex[0], out vector4, out num4))
					{
						cgvolume.CrossFShift = DTMath.SnapPrecision(cgvolume.CrossF[data2.Count - 1] + (cgvolume.CrossF[0] - cgvolume.CrossF[data2.Count - 1]) * num4, 2);
					}
				}
				if (cgvolume2 != null)
				{
					cgvolume2.CrossFShift = cgvolume.CrossFShift;
				}
				this.OutVolume.SetData(new CGData[]
				{
					cgvolume
				});
				this.OutVolumeHollow.SetData(new CGData[]
				{
					cgvolume2
				});
			}
		}

		public Vector3 GetScale(float f)
		{
			Vector3 baseScale = (!this.ScaleUniform) ? new Vector3(this.ScaleX, this.ScaleY, 1f) : new Vector3(this.ScaleX, this.ScaleX, 1f);
			Vector3 zero = Vector3.zero;
			this.getScaleInternal(f, baseScale, ref zero);
			return zero;
		}

		private void getScaleInternal(float f, Vector3 baseScale, ref Vector3 scale)
		{
			if (this.ScaleMode == BuildShapeExtrusion.ScaleModeEnum.Advanced)
			{
				float time = DTMath.Repeat(f - this.ScaleOffset, 1f);
				float num = baseScale.x * this.ScaleMultiplierX.Evaluate(time);
				scale.Set(num, (!this.m_ScaleUniform) ? (baseScale.y * this.ScaleMultiplierY.Evaluate(time)) : num, 1f);
			}
			else
			{
				scale = baseScale;
			}
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGPath)
		}, RequestDataOnly = true)]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGShape)
		}, RequestDataOnly = true)]
		public CGModuleInputSlot InCross = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGVolume))]
		public CGModuleOutputSlot OutVolume = new CGModuleOutputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGVolume))]
		public CGModuleOutputSlot OutVolumeHollow = new CGModuleOutputSlot();

		[Tab("Path")]
		[FloatRegion(UseSlider = true, RegionOptionsPropertyName = "RangeOptions", Precision = 4)]
		[SerializeField]
		private FloatRegion m_Range = FloatRegion.ZeroOne;

		[SerializeField]
		[RangeEx(1f, 100f, "Resolution", "Defines how densely the path spline's sampling points are. When the value is 100, the number of sampling points per world distance unit is equal to the spline's Max Points Per Unit")]
		private int m_Resolution = 50;

		[SerializeField]
		private bool m_Optimize = true;

		[FieldCondition("m_Optimize", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[RangeEx(0.1f, 120f, "", "", Tooltip = "Max angle")]
		private float m_AngleThreshold = 10f;

		[Tab("Cross")]
		[FieldAction("CBEditCrossButton", ActionAttribute.ActionEnum.Callback, Position = ActionAttribute.ActionPositionEnum.Above)]
		[FloatRegion(UseSlider = true, RegionOptionsPropertyName = "CrossRangeOptions", Precision = 4)]
		[SerializeField]
		private FloatRegion m_CrossRange = FloatRegion.ZeroOne;

		[SerializeField]
		[RangeEx(1f, 100f, "Resolution", "", Tooltip = "Defines how densely the cross spline's sampling points are. When the value is 100, the number of sampling points per world distance unit is equal to the spline's Max Points Per Unit")]
		private int m_CrossResolution = 50;

		[SerializeField]
		[Label("Optimize", "")]
		private bool m_CrossOptimize = true;

		[FieldCondition("m_CrossOptimize", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[RangeEx(0.1f, 120f, "Angle Threshold", "", Tooltip = "Max angle")]
		private float m_CrossAngleThreshold = 10f;

		[SerializeField]
		[Label("Include CP", "")]
		private bool m_CrossIncludeControlpoints;

		[SerializeField]
		[Label("Hard Edges", "")]
		private bool m_CrossHardEdges;

		[SerializeField]
		[Label("Materials", "")]
		private bool m_CrossMaterials;

		[SerializeField]
		[Label("Extended UV", "")]
		private bool m_CrossExtendedUV;

		[SerializeField]
		[Label("Shift", "", Tooltip = "Shift Cross Start")]
		private BuildShapeExtrusion.CrossShiftModeEnum m_CrossShiftMode = BuildShapeExtrusion.CrossShiftModeEnum.ByOrientation;

		[SerializeField]
		[RangeEx(0f, 1f, "Value", "Shift By", Slider = true)]
		[FieldCondition("m_CrossShiftMode", BuildShapeExtrusion.CrossShiftModeEnum.Custom, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		private float m_CrossShiftValue;

		[Label("Reverse Normal", "Reverse Vertex Normals?")]
		[SerializeField]
		private bool m_CrossReverseNormals;

		[Tab("Scale")]
		[Label("Mode", "")]
		[SerializeField]
		private BuildShapeExtrusion.ScaleModeEnum m_ScaleMode;

		[FieldCondition("m_ScaleMode", BuildShapeExtrusion.ScaleModeEnum.Advanced, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[Label("Reference", "")]
		[SerializeField]
		private CGReferenceMode m_ScaleReference = CGReferenceMode.Self;

		[FieldCondition("m_ScaleMode", BuildShapeExtrusion.ScaleModeEnum.Advanced, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[Label("Offset", "")]
		[SerializeField]
		private float m_ScaleOffset;

		[SerializeField]
		[Label("Uniform", "", Tooltip = "Use a single curve")]
		private bool m_ScaleUniform = true;

		[SerializeField]
		private float m_ScaleX = 1f;

		[SerializeField]
		[FieldCondition("m_ScaleUniform", false, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		private float m_ScaleY = 1f;

		[SerializeField]
		[FieldCondition("m_ScaleMode", BuildShapeExtrusion.ScaleModeEnum.Advanced, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[AnimationCurveEx("Multiplier X", "")]
		[Tooltip("Defines scale multiplier depending on the TF, the relative position of a point on the path")]
		private AnimationCurve m_ScaleCurveX = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		[SerializeField]
		[FieldCondition("m_ScaleUniform", false, false, ConditionalAttribute.OperatorEnum.AND, "m_ScaleMode", BuildShapeExtrusion.ScaleModeEnum.Advanced, false)]
		[AnimationCurveEx("Multiplier Y", "")]
		[Tooltip("Defines scale multiplier depending on the TF, the relative position of a point on the path")]
		private AnimationCurve m_ScaleCurveY = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		[Tab("Hollow")]
		[RangeEx(0f, 1f, "", "", Slider = true, Label = "Inset")]
		[SerializeField]
		private float m_HollowInset;

		[Label("Reverse Normal", "Reverse Vertex Normals?")]
		[SerializeField]
		private bool m_HollowReverseNormals;

		public enum ScaleModeEnum
		{
			Simple,
			Advanced
		}

		public enum CrossShiftModeEnum
		{
			None,
			ByOrientation,
			Custom
		}
	}
}
