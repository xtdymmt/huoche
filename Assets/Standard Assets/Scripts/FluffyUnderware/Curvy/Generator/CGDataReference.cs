// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGDataReference
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGDataReference
	{
		public CGDataReference()
		{
		}

		public CGDataReference(CGModule module, string slotName)
		{
			this.setINTERNAL(module, slotName);
		}

		public CGDataReference(CurvyGenerator generator, string moduleName, string slotName)
		{
			this.setINTERNAL(generator, moduleName, slotName);
		}

		public CGData[] Data
		{
			get
			{
				return (this.Slot == null) ? new CGData[0] : this.Slot.Data;
			}
		}

		public CGModuleOutputSlot Slot
		{
			get
			{
				if ((this.mSlot == null || this.mSlot.Module != this.m_Module || this.mSlot.Info == null || this.mSlot.Info.Name != this.m_SlotName) && this.m_Module != null && this.m_Module.Generator != null && this.m_Module.Generator.IsInitialized && !string.IsNullOrEmpty(this.m_SlotName))
				{
					this.mSlot = this.m_Module.GetOutputSlot(this.m_SlotName);
				}
				return this.mSlot;
			}
		}

		public bool HasValue
		{
			get
			{
				CGModuleOutputSlot slot = this.Slot;
				return slot != null && slot.Data.Length > 0;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.SlotName);
			}
		}

		public CGModule Module
		{
			get
			{
				return this.m_Module;
			}
		}

		public string SlotName
		{
			get
			{
				return this.m_SlotName;
			}
		}

		public void Clear()
		{
			this.setINTERNAL(null, string.Empty);
		}

		public T GetData<T>() where T : CGData
		{
			return (this.Data.Length != 0) ? (this.Data[0] as T) : ((T)((object)null));
		}

		public T[] GetAllData<T>() where T : CGData
		{
			return this.Data as T[];
		}

		public void setINTERNAL(CGModule module, string slotName)
		{
			this.m_Module = module;
			this.m_SlotName = slotName;
			this.mSlot = null;
		}

		public void setINTERNAL(CurvyGenerator generator, string moduleName, string slotName)
		{
			this.m_Module = generator.GetModule(moduleName, false);
			this.m_SlotName = slotName;
			this.mSlot = null;
		}

		[SerializeField]
		private CGModule m_Module;

		[SerializeField]
		private string m_SlotName;

		private CGModuleOutputSlot mSlot;
	}
}
