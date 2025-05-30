// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.BuildVolumeSpots
using System;
using System.Collections.Generic;
using System.Globalization;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Build/Volume Spots", ModuleName = "Volume Spots", Description = "Generate spots along a path/volume", UsesRandom = true)]
	[HelpURL("https://curvyeditor.com/doclink/cgvolumespots")]
	public class BuildVolumeSpots : CGModule
	{
		public FloatRegion Range
		{
			get
			{
				return this.m_Range;
			}
			set
			{
				if (this.m_Range != value)
				{
					this.m_Range = value;
				}
				base.Dirty = true;
			}
		}

		public bool UseVolume
		{
			get
			{
				return this.m_UseVolume;
			}
			set
			{
				if (this.m_UseVolume != value)
				{
					this.m_UseVolume = value;
				}
				base.Dirty = true;
			}
		}

		public bool Simulate
		{
			get
			{
				return this.m_Simulate;
			}
			set
			{
				if (this.m_Simulate != value)
				{
					this.m_Simulate = value;
				}
				base.Dirty = true;
			}
		}

		public float CrossBase
		{
			get
			{
				return this.m_CrossBase;
			}
			set
			{
				float num = Mathf.Repeat(value, 1f);
				if (this.m_CrossBase != num)
				{
					this.m_CrossBase = num;
				}
				base.Dirty = true;
			}
		}

		public AnimationCurve CrossCurve
		{
			get
			{
				return this.m_CrossCurve;
			}
			set
			{
				if (this.m_CrossCurve != value)
				{
					this.m_CrossCurve = value;
				}
				base.Dirty = true;
			}
		}

		public List<CGBoundsGroup> Groups
		{
			get
			{
				return this.m_Groups;
			}
			set
			{
				if (this.m_Groups != value)
				{
					this.m_Groups = value;
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
				base.Dirty = true;
			}
		}

		public int FirstRepeating
		{
			get
			{
				return this.m_RepeatingGroups.From;
			}
			set
			{
				int num = Mathf.Clamp(value, 0, Mathf.Max(0, this.GroupCount - 1));
				if (this.m_RepeatingGroups.From != num)
				{
					this.m_RepeatingGroups.From = num;
				}
				base.Dirty = true;
			}
		}

		public int LastRepeating
		{
			get
			{
				return this.m_RepeatingGroups.To;
			}
			set
			{
				int num = Mathf.Clamp(value, this.FirstRepeating, Mathf.Max(0, this.GroupCount - 1));
				if (this.m_RepeatingGroups.To != num)
				{
					this.m_RepeatingGroups.To = num;
				}
				base.Dirty = true;
			}
		}

		public bool FitEnd
		{
			get
			{
				return this.m_FitEnd;
			}
			set
			{
				if (this.m_FitEnd != value)
				{
					this.m_FitEnd = value;
				}
				base.Dirty = true;
			}
		}

		public int GroupCount
		{
			get
			{
				return this.Groups.Count;
			}
		}

		public GUIContent[] BoundsNames
		{
			get
			{
				if (this.mBounds == null)
				{
					return new GUIContent[0];
				}
				GUIContent[] array = new GUIContent[this.mBounds.Count];
				for (int i = 0; i < this.mBounds.Count; i++)
				{
					array[i] = new GUIContent(string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
					{
						i.ToString(CultureInfo.InvariantCulture),
						this.mBounds[i].Name
					}));
				}
				return array;
			}
		}

		public int[] BoundsIndices
		{
			get
			{
				if (this.mBounds == null)
				{
					return new int[0];
				}
				int[] array = new int[this.mBounds.Count];
				for (int i = 0; i < this.mBounds.Count; i++)
				{
					array[i] = i;
				}
				return array;
			}
		}

		public int Count { get; private set; }

		private int lastGroupIndex
		{
			get
			{
				return Mathf.Max(0, this.GroupCount - 1);
			}
		}

		private RegionOptions<float> RangeOptions
		{
			get
			{
				return RegionOptions<float>.MinMax(0f, 1f);
			}
		}

		private RegionOptions<int> RepeatingGroupsOptions
		{
			get
			{
				return RegionOptions<int>.MinMax(0, Mathf.Max(0, this.GroupCount - 1));
			}
		}

		private CGPath Path { get; set; }

		private CGVolume Volume
		{
			get
			{
				return this.Path as CGVolume;
			}
		}

		private float Length
		{
			get
			{
				return (this.Path == null) ? 0f : (this.Path.Length * this.m_Range.Length);
			}
		}

		private float StartDistance { get; set; }

		protected override void OnEnable()
		{
			base.OnEnable();
			this.Properties.MinWidth = 350f;
		}

		public override void Reset()
		{
			base.Reset();
			this.m_Range = FloatRegion.ZeroOne;
			this.UseVolume = false;
			this.Simulate = false;
			this.CrossBase = 0f;
			this.CrossCurve = AnimationCurve.Linear(0f, 0f, 1f, 0f);
			this.RepeatingOrder = CurvyRepeatingOrderEnum.Row;
			this.FirstRepeating = 0;
			this.LastRepeating = 0;
			this.FitEnd = false;
			this.Groups.Clear();
			this.AddGroup("Group");
		}

		public override void OnStateChange()
		{
			base.OnStateChange();
			if (!this.IsConfigured)
			{
				this.Clear();
			}
		}

		public void Clear()
		{
			this.Count = 0;
			this.SimulatedSpots = new CGSpots();
			this.OutSpots.SetData(new CGData[]
			{
				this.SimulatedSpots
			});
		}

		public override void Refresh()
		{
			base.Refresh();
			this.mBounds = this.InBounds.GetAllData<CGBounds>(new CGDataRequestParameter[0]);
			bool flag = false;
			for (int i = 0; i < this.mBounds.Count; i++)
			{
				CGBounds cgbounds = this.mBounds[i];
				if (cgbounds is CGGameObject && ((CGGameObject)cgbounds).Object == null)
				{
					flag = true;
					this.UIMessages.Add(string.Format("Input object of index {0} has no Game Object attached to it. Correct this to enable spots generation.", i));
				}
				else if (cgbounds.Depth <= 0.01f)
				{
					CGBounds cgbounds2 = new CGBounds(cgbounds);
					this.UIMessages.Add(string.Format("Input object \"{0}\" has bounds with a depth of {1}. The minimal accepted depth is {2}. The depth value was overriden.", cgbounds2.Name, cgbounds.Depth, 0.01f));
					cgbounds2.Bounds = new Bounds(cgbounds.Bounds.center, new Vector3(cgbounds.Bounds.size.x, cgbounds.Bounds.size.y, 0.01f));
					this.mBounds[i] = cgbounds2;
				}
			}
			if (this.mBounds.Count == 0)
			{
				flag = true;
				this.UIMessages.Add("The input bounds list is empty. Add some to enable spots generation.");
			}
			foreach (CGBoundsGroup cgboundsGroup in this.Groups)
			{
				if (cgboundsGroup.ItemCount == 0)
				{
					flag = true;
					this.UIMessages.Add(string.Format("Group \"{0}\" has 0 item in it. Add some to enable spots generation.", cgboundsGroup.Name));
				}
				else
				{
					foreach (CGBoundsGroupItem cgboundsGroupItem in cgboundsGroup.Items)
					{
						int index = cgboundsGroupItem.Index;
						if (index < 0 || index >= this.mBounds.Count)
						{
							flag = true;
							this.UIMessages.Add(string.Format("Group \"{0}\" has a reference to an inexistent item of index {1}. Correct the reference to enable spots generation.", cgboundsGroup.Name, index));
							break;
						}
					}
				}
			}
			this.Path = this.InPath.GetData<CGPath>(new CGDataRequestParameter[0]);
			if (this.Path != null && this.Volume == null && this.UseVolume)
			{
				this.m_UseVolume = false;
			}
			List<CGSpot> list = new List<CGSpot>();
			List<BuildVolumeSpots.GroupSet> list2 = null;
			this.prepare();
			if (this.Path && !flag)
			{
				float length = this.Length;
				this.StartDistance = this.Path.FToDistance(this.m_Range.Low);
				float startDistance = this.StartDistance;
				for (int j = 0; j < this.FirstRepeating; j++)
				{
					this.addGroupItems(this.Groups[j], ref list, ref length, ref startDistance, false);
					if (length <= 0f)
					{
						break;
					}
				}
				if (this.GroupCount - this.LastRepeating - 1 > 0)
				{
					list2 = new List<BuildVolumeSpots.GroupSet>();
					float num = 0f;
					for (int k = this.LastRepeating + 1; k < this.GroupCount; k++)
					{
						list2.Add(this.addGroupItems(this.Groups[k], ref list, ref length, ref num, true));
					}
				}
				bool flag2 = false;
				if (this.RepeatingOrder == CurvyRepeatingOrderEnum.Row)
				{
					int firstRepeating = this.FirstRepeating;
					while (length > 0f)
					{
						this.addGroupItems(this.Groups[firstRepeating++], ref list, ref length, ref startDistance, false);
						if (firstRepeating > this.LastRepeating)
						{
							firstRepeating = this.FirstRepeating;
						}
						if (list.Count >= 10000)
						{
							flag2 = true;
							break;
						}
					}
				}
				else
				{
					while (length > 0f)
					{
						this.addGroupItems(this.Groups[this.mGroupBag.Next()], ref list, ref length, ref startDistance, false);
						if (list.Count >= 10000)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (flag2)
				{
					string text = string.Format("Number of generated spots reached the maximal allowed number, which is {0}. Spots generation was stopped. Try to reduce the number of spots needed by using bigger Bounds as inputs and/or setting bigger space between two spots.", 10000);
					this.UIMessages.Add(text);
					DTLog.LogError("[Curvy] Volume spots: " + text);
				}
				if (list2 != null)
				{
					this.rebase(ref list, ref list2, startDistance);
				}
			}
			this.Count = list.Count;
			this.SimulatedSpots = new CGSpots(new List<CGSpot>[]
			{
				list
			});
			if (this.Simulate)
			{
				this.OutSpots.SetData(new CGData[]
				{
					new CGSpots()
				});
			}
			else
			{
				this.OutSpots.SetData(new CGData[]
				{
					this.SimulatedSpots
				});
			}
		}

		public CGBoundsGroup AddGroup(string name)
		{
			CGBoundsGroup cgboundsGroup = new CGBoundsGroup(name);
			cgboundsGroup.Items.Add(new CGBoundsGroupItem());
			this.Groups.Add(cgboundsGroup);
			base.Dirty = true;
			return cgboundsGroup;
		}

		public void RemoveGroup(CGBoundsGroup group)
		{
			this.Groups.Remove(group);
			base.Dirty = true;
		}

		private BuildVolumeSpots.GroupSet addGroupItems(CGBoundsGroup group, ref List<CGSpot> spots, ref float remainingLength, ref float currentDistance, bool calcLengthOnly = false)
		{
			for (int i = 0; i < group.ItemCount; i++)
			{
				CGBounds itemBounds = this.getItemBounds(group.Items[i].Index);
			}
			int num = 0;
			float next = group.SpaceBefore.Next;
			float next2 = group.SpaceAfter.Next;
			float num2 = remainingLength - next;
			BuildVolumeSpots.GroupSet groupSet = null;
			BuildVolumeSpots.GroupSet groupSet2 = new BuildVolumeSpots.GroupSet();
			float num3 = currentDistance + next;
			if (calcLengthOnly)
			{
				groupSet = new BuildVolumeSpots.GroupSet();
				groupSet.Group = group;
				groupSet.Length = next + next2;
			}
			for (int j = 0; j < group.FirstRepeating; j++)
			{
				int index = group.Items[j].Index;
				CGBounds itemBounds2 = this.getItemBounds(index);
				num2 -= itemBounds2.Depth;
				if (num2 <= 0f)
				{
					if (group.KeepTogether && num > 0)
					{
						spots.RemoveRange(spots.Count - num, num);
					}
					break;
				}
				if (calcLengthOnly)
				{
					groupSet.Length += itemBounds2.Depth;
					groupSet.Items.Add(index);
					groupSet.Distances.Add(num3);
				}
				else
				{
					spots.Add(this.getSpot(index, ref group, ref itemBounds2, num3));
				}
				num3 += itemBounds2.Depth;
				num++;
			}
			if (num2 > 0f)
			{
				float num4 = 0f;
				for (int k = group.LastRepeating + 1; k < group.ItemCount; k++)
				{
					int index = group.Items[k].Index;
					CGBounds itemBounds2 = this.getItemBounds(index);
					num2 -= itemBounds2.Depth;
					if (num2 <= 0f)
					{
						break;
					}
					groupSet2.Length += itemBounds2.Depth;
					groupSet2.Items.Add(index);
					groupSet2.Distances.Add(num4);
					num4 += itemBounds2.Depth;
				}
				if (num2 > 0f)
				{
					for (int l = group.FirstRepeating; l <= group.LastRepeating; l++)
					{
						int index2 = (group.RepeatingOrder != CurvyRepeatingOrderEnum.Row) ? group.getRandomItemINTERNAL() : l;
						int index = group.Items[index2].Index;
						CGBounds itemBounds2 = this.getItemBounds(index);
						num2 -= itemBounds2.Depth;
						if (num2 <= 0f)
						{
							if (group.KeepTogether && num > 0)
							{
								spots.RemoveRange(spots.Count - num, num);
							}
							break;
						}
						if (calcLengthOnly)
						{
							groupSet.Length += itemBounds2.Depth;
							groupSet.Items.Add(index);
							groupSet.Distances.Add(num3);
						}
						else
						{
							spots.Add(this.getSpot(index, ref group, ref itemBounds2, num3));
						}
						num3 += itemBounds2.Depth;
						num++;
					}
				}
				if (num2 > 0f || !group.KeepTogether)
				{
					for (int m = 0; m < groupSet2.Items.Count; m++)
					{
						CGBounds itemBounds3 = this.getItemBounds(groupSet2.Items[m]);
						spots.Add(this.getSpot(groupSet2.Items[m], ref group, ref itemBounds3, num3 + groupSet2.Distances[m]));
						num3 += itemBounds3.Depth;
					}
				}
			}
			remainingLength = num2 - next2;
			currentDistance = num3 + next2;
			return groupSet;
		}

		private void rebase(ref List<CGSpot> spots, ref List<BuildVolumeSpots.GroupSet> sets, float currentDistance)
		{
			if (this.FitEnd)
			{
				currentDistance = this.Path.FToDistance(this.m_Range.To);
				for (int i = 0; i < sets.Count; i++)
				{
					currentDistance -= sets[i].Length;
				}
			}
			for (int j = 0; j < sets.Count; j++)
			{
				BuildVolumeSpots.GroupSet groupSet = sets[j];
				for (int k = 0; k < groupSet.Items.Count; k++)
				{
					CGBounds itemBounds = this.getItemBounds(groupSet.Items[k]);
					spots.Add(this.getSpot(groupSet.Items[k], ref groupSet.Group, ref itemBounds, currentDistance + groupSet.Distances[k]));
				}
			}
		}

		private CGSpot getSpot(int itemID, ref CGBoundsGroup group, ref CGBounds bounds, float startDist)
		{
			CGSpot result = new CGSpot(itemID);
			float f = this.Path.DistanceToF(startDist + bounds.Depth / 2f);
			Vector3 a = Vector3.zero;
			Vector3 forward = Vector3.forward;
			Vector3 up = Vector3.up;
			float crossValue = this.getCrossValue((startDist - this.StartDistance) / this.Length, group);
			if (group.RotationMode != CGBoundsGroup.RotationModeEnum.Independent)
			{
				if (this.UseVolume)
				{
					this.Volume.InterpolateVolume(f, crossValue, out a, out forward, out up);
				}
				else
				{
					this.Path.Interpolate(f, crossValue, out a, out forward, out up);
				}
				CGBoundsGroup.RotationModeEnum rotationMode = group.RotationMode;
				if (rotationMode != CGBoundsGroup.RotationModeEnum.Direction)
				{
					if (rotationMode == CGBoundsGroup.RotationModeEnum.Horizontal)
					{
						up = Vector3.up;
						forward.y = 0f;
					}
				}
				else
				{
					up = Vector3.up;
				}
			}
			else
			{
				a = ((!this.UseVolume) ? this.Path.InterpolatePosition(f) : this.Volume.InterpolateVolumePosition(f, crossValue));
			}
			if (this.Path.SourceIsManaged)
			{
				result.Rotation = Quaternion.LookRotation(forward, up) * Quaternion.Euler(group.RotationOffset.x + group.RotationScatter.x * (float)UnityEngine.Random.Range(-1, 1), group.RotationOffset.y + group.RotationScatter.y * (float)UnityEngine.Random.Range(-1, 1), group.RotationOffset.z + group.RotationScatter.z * (float)UnityEngine.Random.Range(-1, 1));
				result.Position = a + result.Rotation * new Vector3(0f, group.Height.Next, 0f);
			}
			else
			{
				result.Rotation = Quaternion.LookRotation(forward, up) * Quaternion.Euler(group.RotationOffset.x + group.RotationScatter.x * (float)UnityEngine.Random.Range(-1, 1), group.RotationOffset.y + group.RotationScatter.y * (float)UnityEngine.Random.Range(-1, 1), group.RotationOffset.z + group.RotationScatter.z * (float)UnityEngine.Random.Range(-1, 1));
				result.Position = a + result.Rotation * new Vector3(0f, group.Height.Next, 0f);
			}
			return result;
		}

		private void prepare()
		{
			this.m_RepeatingGroups.MakePositive();
			this.m_RepeatingGroups.Clamp(0, this.GroupCount - 1);
			if (this.mGroupBag == null)
			{
				this.mGroupBag = new WeightedRandom<int>(0);
			}
			else
			{
				this.mGroupBag.Clear();
			}
			if (this.RepeatingOrder == CurvyRepeatingOrderEnum.Random)
			{
				for (int i = this.FirstRepeating; i <= this.LastRepeating; i++)
				{
					this.mGroupBag.Add(i, (int)(this.Groups[i].Weight * 10f));
				}
			}
			for (int j = 0; j < this.Groups.Count; j++)
			{
				this.Groups[j].PrepareINTERNAL();
			}
		}

		private CGBounds getItemBounds(int itemIndex)
		{
			return (itemIndex < 0 || itemIndex >= this.mBounds.Count) ? null : this.mBounds[itemIndex];
		}

		private float getCrossValue(float globalF, CGBoundsGroup group)
		{
			CGBoundsGroup.DistributionModeEnum distributionMode = group.DistributionMode;
			if (distributionMode == CGBoundsGroup.DistributionModeEnum.Parent)
			{
				return DTMath.MapValue(-0.5f, 0.5f, this.CrossBase + this.m_CrossCurve.Evaluate(globalF) + group.PositionOffset.Next, -1f, 1f);
			}
			if (distributionMode != CGBoundsGroup.DistributionModeEnum.Self)
			{
				return 0f;
			}
			return DTMath.MapValue(-0.5f, 0.5f, group.PositionOffset.Next, -1f, 1f);
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGPath)
		}, DisplayName = "Volume/Rasterized Path", Name = "Path/Volume")]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGBounds)
		}, Array = true)]
		public CGModuleInputSlot InBounds = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGSpots))]
		public CGModuleOutputSlot OutSpots = new CGModuleOutputSlot();

		[Tab("General")]
		[FloatRegion(RegionOptionsPropertyName = "RangeOptions", Precision = 4)]
		[SerializeField]
		private FloatRegion m_Range = FloatRegion.ZeroOne;

		[Tooltip("When the source is a Volume, you can choose if you want to use it's path or the volume")]
		[FieldCondition("Volume", null, true, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private bool m_UseVolume;

		[Tooltip("Dry run without actually creating spots?")]
		[SerializeField]
		private bool m_Simulate;

		[Section("Default/General/Cross", true, false, 100)]
		[SerializeField]
		[RangeEx(-1f, 1f, "", "")]
		private float m_CrossBase;

		[SerializeField]
		private AnimationCurve m_CrossCurve = AnimationCurve.Linear(0f, 0f, 1f, 0f);

		[Tab("Groups")]
		[ArrayEx(Space = 10)]
		[SerializeField]
		private List<CGBoundsGroup> m_Groups = new List<CGBoundsGroup>();

		[IntRegion(UseSlider = false, RegionOptionsPropertyName = "RepeatingGroupsOptions", Options = AttributeOptionsFlags.Compact)]
		[SerializeField]
		private IntRegion m_RepeatingGroups;

		[SerializeField]
		private CurvyRepeatingOrderEnum m_RepeatingOrder = CurvyRepeatingOrderEnum.Row;

		[SerializeField]
		private bool m_FitEnd;

		public CGSpots SimulatedSpots;

		private WeightedRandom<int> mGroupBag;

		private List<CGBounds> mBounds;

		private class GroupSet
		{
			public CGBoundsGroup Group;

			public float Length;

			public List<int> Items = new List<int>();

			public List<float> Distances = new List<float>();
		}
	}
}
