// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGModuleLink
using System;
using System.Globalization;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGModuleLink
	{
		public CGModuleLink(int sourceID, string sourceSlotName, int targetID, string targetSlotName)
		{
			this.m_ModuleID = sourceID;
			this.m_SlotName = sourceSlotName;
			this.m_TargetModuleID = targetID;
			this.m_TargetSlotName = targetSlotName;
		}

		public CGModuleLink(CGModuleSlot source, CGModuleSlot target) : this(source.Module.UniqueID, source.Name, target.Module.UniqueID, target.Name)
		{
		}

		public int ModuleID
		{
			get
			{
				return this.m_ModuleID;
			}
		}

		public string SlotName
		{
			get
			{
				return this.m_SlotName;
			}
		}

		public int TargetModuleID
		{
			get
			{
				return this.m_TargetModuleID;
			}
		}

		public string TargetSlotName
		{
			get
			{
				return this.m_TargetSlotName;
			}
		}

		public bool IsSame(CGModuleLink o)
		{
			return this.ModuleID == o.ModuleID && this.SlotName == o.SlotName && this.TargetModuleID == o.TargetModuleID && this.TargetSlotName == o.m_TargetSlotName;
		}

		public bool IsSame(CGModuleSlot source, CGModuleSlot target)
		{
			return this.ModuleID == source.Module.UniqueID && this.SlotName == source.Name && this.TargetModuleID == target.Module.UniqueID && this.TargetSlotName == target.Name;
		}

		public bool IsTo(CGModuleSlot s)
		{
			return s.Module.UniqueID == this.TargetModuleID && s.Name == this.TargetSlotName;
		}

		public bool IsFrom(CGModuleSlot s)
		{
			return s.Module.UniqueID == this.ModuleID && s.Name == this.SlotName;
		}

		public bool IsUsing(CGModule module)
		{
			return this.ModuleID == module.UniqueID || this.TargetModuleID == module.UniqueID;
		}

		public bool IsBetween(CGModuleSlot one, CGModuleSlot another)
		{
			return (this.IsTo(one) && this.IsFrom(another)) || (this.IsTo(another) && this.IsFrom(one));
		}

		public void SetModuleIDIINTERNAL(int moduleID, int targetModuleID)
		{
			this.m_ModuleID = moduleID;
			this.m_TargetModuleID = targetModuleID;
		}

		public static implicit operator bool(CGModuleLink a)
		{
			return !object.ReferenceEquals(a, null);
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}({1})->{2}({3})", new object[]
			{
				this.SlotName,
				this.ModuleID,
				this.TargetSlotName,
				this.TargetModuleID
			});
		}

		[SerializeField]
		private int m_ModuleID;

		[SerializeField]
		private string m_SlotName;

		[SerializeField]
		private int m_TargetModuleID;

		[SerializeField]
		private string m_TargetSlotName;
	}
}
