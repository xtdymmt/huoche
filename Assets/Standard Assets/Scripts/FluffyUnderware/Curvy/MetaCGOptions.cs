// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.MetaCGOptions
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[HelpURL("https://curvyeditor.com/doclink/metacgoptions")]
	public class MetaCGOptions : CurvyMetadataBase, ICurvyMetadata
	{
		public int MaterialID
		{
			get
			{
				return this.m_MaterialID;
			}
			set
			{
				int num = Mathf.Max(0, value);
				if (this.m_MaterialID != num)
				{
					this.m_MaterialID = num;
					base.NotifyModification();
				}
			}
		}

		public bool HardEdge
		{
			get
			{
				return this.m_HardEdge;
			}
			set
			{
				if (this.m_HardEdge != value)
				{
					this.m_HardEdge = value;
					base.NotifyModification();
				}
			}
		}

		public bool UVEdge
		{
			get
			{
				return this.m_UVEdge;
			}
			set
			{
				if (this.m_UVEdge != value)
				{
					this.m_UVEdge = value;
					base.NotifyModification();
				}
			}
		}

		public bool ExplicitU
		{
			get
			{
				return this.m_ExplicitU;
			}
			set
			{
				if (this.m_ExplicitU != value)
				{
					this.m_ExplicitU = value;
					base.NotifyModification();
				}
			}
		}

		public float FirstU
		{
			get
			{
				return this.m_FirstU;
			}
			set
			{
				if (this.m_FirstU != value)
				{
					this.m_FirstU = value;
					base.NotifyModification();
				}
			}
		}

		public float SecondU
		{
			get
			{
				return this.m_SecondU;
			}
			set
			{
				if (this.m_SecondU != value)
				{
					this.m_SecondU = value;
					base.NotifyModification();
				}
			}
		}

		public float MaxStepDistance
		{
			get
			{
				return this.m_MaxStepDistance;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_MaxStepDistance != num)
				{
					this.m_MaxStepDistance = num;
					base.NotifyModification();
				}
			}
		}

		public bool HasDifferentMaterial
		{
			get
			{
				MetaCGOptions previousData = base.GetPreviousData<MetaCGOptions>(true, true, false);
				return previousData && previousData.MaterialID != this.MaterialID;
			}
		}

		private bool showUVEdge
		{
			get
			{
				return base.ControlPoint && (base.Spline.Closed || (!(base.Spline.FirstVisibleControlPoint == base.ControlPoint) && !(base.Spline.LastVisibleControlPoint == base.ControlPoint))) && !this.HasDifferentMaterial;
			}
		}

		private bool showExplicitU
		{
			get
			{
				return base.ControlPoint && !this.UVEdge && !this.HasDifferentMaterial;
			}
		}

		private bool showFirstU
		{
			get
			{
				bool result = false;
				if (base.ControlPoint)
				{
					result = (this.UVEdge || this.ExplicitU || this.HasDifferentMaterial);
				}
				return result;
			}
		}

		private bool showSecondU
		{
			get
			{
				return this.UVEdge || this.HasDifferentMaterial;
			}
		}

		public void Reset()
		{
			this.MaterialID = 0;
			this.HardEdge = false;
			this.MaxStepDistance = 0f;
			this.UVEdge = false;
			this.ExplicitU = false;
			this.FirstU = 0f;
			this.SecondU = 0f;
		}

		public float GetDefinedFirstU(float defaultValue)
		{
			return (!this.UVEdge && !this.ExplicitU && !this.HasDifferentMaterial) ? defaultValue : this.FirstU;
		}

		public float GetDefinedSecondU(float defaultValue)
		{
			return (!this.UVEdge && !this.HasDifferentMaterial) ? this.GetDefinedFirstU(defaultValue) : this.SecondU;
		}

		[Positive]
		[SerializeField]
		private int m_MaterialID;

		[SerializeField]
		private bool m_HardEdge;

		[Positive(Tooltip = "Max step distance when using optimization")]
		[SerializeField]
		private float m_MaxStepDistance;

		[Section("Extended UV", true, false, 100, HelpURL = "https://curvyeditor.com/doclink/metacgoptions_extendeduv")]
		[FieldCondition("showUVEdge", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private bool m_UVEdge;

		[Positive]
		[FieldCondition("showExplicitU", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private bool m_ExplicitU;

		[FieldCondition("showFirstU", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[FieldAction("CBSetFirstU", ActionAttribute.ActionEnum.Callback)]
		[Positive]
		[SerializeField]
		private float m_FirstU;

		[FieldCondition("showSecondU", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[Positive]
		[SerializeField]
		private float m_SecondU;
	}
}
