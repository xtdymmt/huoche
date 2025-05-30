// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGModuleSlot
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public class CGModuleSlot
	{
		public CGModule Module { get; internal set; }

		public SlotInfo Info { get; internal set; }

		public Vector2 Origin { get; set; }

		public Rect DropZone { get; set; }

		public bool IsLinked
		{
			get
			{
				return this.LinkedSlots != null && this.LinkedSlots.Count > 0;
			}
		}

		public bool IsLinkedAndConfigured
		{
			get
			{
				if (!this.IsLinked)
				{
					return false;
				}
				for (int i = 0; i < this.LinkedSlots.Count; i++)
				{
					if (!this.LinkedSlots[i].Module.IsConfigured)
					{
						return false;
					}
				}
				return true;
			}
		}

		public IOnRequestProcessing OnRequestModule
		{
			get
			{
				return this.Module as IOnRequestProcessing;
			}
		}

		public IOnRequestPath OnRequestPathModule
		{
			get
			{
				return this.Module as IOnRequestPath;
			}
		}

		public IExternalInput ExternalInput
		{
			get
			{
				return this.Module as IExternalInput;
			}
		}

		public List<CGModuleSlot> LinkedSlots
		{
			get
			{
				if (this.mLinkedSlots == null)
				{
					this.LoadLinkedSlots();
				}
				return this.mLinkedSlots ?? new List<CGModuleSlot>();
			}
		}

		public int Count
		{
			get
			{
				return this.LinkedSlots.Count;
			}
		}

		public string Name
		{
			get
			{
				return (this.Info == null) ? string.Empty : this.Info.Name;
			}
		}

		public bool HasLinkTo(CGModuleSlot other)
		{
			for (int i = 0; i < this.LinkedSlots.Count; i++)
			{
				if (this.LinkedSlots[i] == other)
				{
					return true;
				}
			}
			return false;
		}

		public List<CGModule> GetLinkedModules()
		{
			List<CGModule> list = new List<CGModule>();
			for (int i = 0; i < this.LinkedSlots.Count; i++)
			{
				list.Add(this.LinkedSlots[i].Module);
			}
			return list;
		}

		public virtual void LinkTo(CGModuleSlot other)
		{
			if (this.Module)
			{
				this.Module.Generator.sortModulesINTERNAL();
				this.Module.Dirty = true;
			}
			if (other.Module)
			{
				other.Module.Dirty = true;
			}
		}

		public virtual void UnlinkFrom(CGModuleSlot other)
		{
			if (this.Module)
			{
				this.Module.Generator.sortModulesINTERNAL();
				this.Module.Dirty = true;
			}
			if (other.Module)
			{
				other.Module.Dirty = true;
			}
		}

		public virtual void UnlinkAll()
		{
		}

		public void ReInitializeLinkedSlots()
		{
			this.mLinkedSlots = null;
		}

		public void ReInitializeLinkedTargetModules()
		{
			List<CGModule> linkedModules = this.GetLinkedModules();
			foreach (CGModule cgmodule in linkedModules)
			{
				if (cgmodule != null)
				{
					cgmodule.ReInitializeLinkedSlots();
				}
			}
		}

		protected virtual void LoadLinkedSlots()
		{
		}

		public static implicit operator bool(CGModuleSlot a)
		{
			return !object.ReferenceEquals(a, null);
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1}.{2}", new object[]
			{
				base.GetType().Name,
				this.Module.name,
				this.Name
			});
		}

		protected List<CGModuleSlot> mLinkedSlots;
	}
}
