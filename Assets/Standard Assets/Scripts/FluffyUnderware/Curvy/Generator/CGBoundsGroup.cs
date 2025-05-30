// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGBoundsGroup
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGBoundsGroup
	{
		public CGBoundsGroup(string name)
		{
			this.Name = name;
		}

		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (this.m_Name != value)
				{
					this.m_Name = value;
				}
			}
		}

		public bool KeepTogether
		{
			get
			{
				return this.m_KeepTogether;
			}
			set
			{
				if (this.m_KeepTogether != value)
				{
					this.m_KeepTogether = value;
				}
			}
		}

		public FloatRegion SpaceBefore
		{
			get
			{
				return this.m_SpaceBefore;
			}
			set
			{
				if (this.m_SpaceBefore != value)
				{
					this.m_SpaceBefore = value;
				}
			}
		}

		public FloatRegion SpaceAfter
		{
			get
			{
				return this.m_SpaceAfter;
			}
			set
			{
				if (this.m_SpaceAfter != value)
				{
					this.m_SpaceAfter = value;
				}
			}
		}

		public float Weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				float num = Mathf.Clamp01(value);
				if (this.m_Weight != num)
				{
					this.m_Weight = num;
				}
			}
		}

		public CurvyRepeatingOrderEnum RepeatingOrder
		{
			get
			{
				return this.m_RepeatingOrder;
			}
			set
			{
				if (this.m_RepeatingOrder != value)
				{
					this.m_RepeatingOrder = value;
				}
			}
		}

		public IntRegion RepeatingItems
		{
			get
			{
				return this.m_RepeatingItems;
			}
			set
			{
				if (this.m_RepeatingItems != value)
				{
					this.m_RepeatingItems = value;
				}
			}
		}

		public CGBoundsGroup.DistributionModeEnum DistributionMode
		{
			get
			{
				return this.m_DistributionMode;
			}
			set
			{
				if (this.m_DistributionMode != value)
				{
					this.m_DistributionMode = value;
				}
			}
		}

		public FloatRegion PositionOffset
		{
			get
			{
				return this.m_PositionOffset;
			}
			set
			{
				if (this.m_PositionOffset != value)
				{
					this.m_PositionOffset = value;
				}
			}
		}

		public FloatRegion Height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				if (this.m_Height != value)
				{
					this.m_Height = value;
				}
			}
		}

		public CGBoundsGroup.RotationModeEnum RotationMode
		{
			get
			{
				return this.m_RotationMode;
			}
			set
			{
				if (this.m_RotationMode != value)
				{
					this.m_RotationMode = value;
				}
			}
		}

		public Vector3 RotationOffset
		{
			get
			{
				return this.m_RotationOffset;
			}
			set
			{
				if (this.m_RotationOffset != value)
				{
					this.m_RotationOffset = value;
				}
			}
		}

		public Vector3 RotationScatter
		{
			get
			{
				return this.m_RotationScatter;
			}
			set
			{
				if (this.m_RotationScatter != value)
				{
					this.m_RotationScatter = value;
				}
			}
		}

		public List<CGBoundsGroupItem> Items
		{
			get
			{
				return this.m_Items;
			}
		}

		public int FirstRepeating
		{
			get
			{
				return this.m_RepeatingItems.From;
			}
			set
			{
				int num = Mathf.Clamp(value, 0, Mathf.Max(0, this.ItemCount - 1));
				if (this.m_RepeatingItems.From != num)
				{
					this.m_RepeatingItems.From = num;
				}
			}
		}

		public int LastRepeating
		{
			get
			{
				return this.m_RepeatingItems.To;
			}
			set
			{
				int num = Mathf.Clamp(value, this.FirstRepeating, Mathf.Max(0, this.ItemCount - 1));
				if (this.m_RepeatingItems.To != num)
				{
					this.m_RepeatingItems.To = num;
				}
			}
		}

		public int ItemCount
		{
			get
			{
				return this.Items.Count;
			}
		}

		private RegionOptions<int> RepeatingGroupsOptions
		{
			get
			{
				return RegionOptions<int>.MinMax(0, Mathf.Max(0, this.ItemCount - 1));
			}
		}

		private RegionOptions<float> PositionRangeOptions
		{
			get
			{
				return RegionOptions<float>.MinMax(-1f, 1f);
			}
		}

		private int lastItemIndex
		{
			get
			{
				return Mathf.Max(0, this.ItemCount - 1);
			}
		}

		internal void PrepareINTERNAL()
		{
			this.m_RepeatingItems.MakePositive();
			this.m_RepeatingItems.Clamp(0, this.ItemCount - 1);
			if (this.mItemBag == null)
			{
				this.mItemBag = new WeightedRandom<int>(0);
			}
			else
			{
				this.mItemBag.Clear();
			}
			if (this.Items.Count == 0)
			{
				return;
			}
			if (this.RepeatingOrder == CurvyRepeatingOrderEnum.Random)
			{
				for (int i = this.FirstRepeating; i <= this.LastRepeating; i++)
				{
					this.mItemBag.Add(i, (int)(this.Items[i].Weight * 10f));
				}
			}
		}

		internal int getRandomItemINTERNAL()
		{
			return this.mItemBag.Next();
		}

		[SerializeField]
		private string m_Name;

		[SerializeField]
		private bool m_KeepTogether;

		[SerializeField]
		[FloatRegion(RegionIsOptional = true, Options = AttributeOptionsFlags.Compact)]
		private FloatRegion m_SpaceBefore = new FloatRegion
		{
			SimpleValue = true
		};

		[SerializeField]
		[FloatRegion(RegionIsOptional = true, Options = AttributeOptionsFlags.Compact)]
		private FloatRegion m_SpaceAfter = new FloatRegion
		{
			SimpleValue = true
		};

		[RangeEx(0f, 1f, "", "", Slider = true, Precision = 1)]
		[SerializeField]
		private float m_Weight = 0.5f;

		[SerializeField]
		private CurvyRepeatingOrderEnum m_RepeatingOrder = CurvyRepeatingOrderEnum.Row;

		[IntRegion(UseSlider = false, RegionOptionsPropertyName = "RepeatingGroupsOptions", Options = AttributeOptionsFlags.Compact)]
		[SerializeField]
		private IntRegion m_RepeatingItems;

		[SerializeField]
		[Header("Lateral Placement")]
		private CGBoundsGroup.DistributionModeEnum m_DistributionMode;

		[SerializeField]
		[FloatRegion(RegionIsOptional = true, RegionOptionsPropertyName = "PositionRangeOptions", UseSlider = true, Precision = 3)]
		private FloatRegion m_PositionOffset = new FloatRegion(0f);

		[SerializeField]
		[FloatRegion(RegionIsOptional = true, Options = AttributeOptionsFlags.Compact)]
		private FloatRegion m_Height = new FloatRegion(0f);

		[Header("Rotation")]
		[Label("Mode", "")]
		[SerializeField]
		private CGBoundsGroup.RotationModeEnum m_RotationMode;

		[SerializeField]
		[VectorEx("", "")]
		private Vector3 m_RotationOffset;

		[SerializeField]
		[VectorEx("", "")]
		private Vector3 m_RotationScatter;

		[SerializeField]
		private List<CGBoundsGroupItem> m_Items = new List<CGBoundsGroupItem>();

		private WeightedRandom<int> mItemBag;

		public enum DistributionModeEnum
		{
			Parent,
			Self
		}

		public enum RotationModeEnum
		{
			Full,
			Direction,
			Horizontal,
			Independent
		}
	}
}
