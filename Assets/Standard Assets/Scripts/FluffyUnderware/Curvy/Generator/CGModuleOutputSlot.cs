// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGModuleOutputSlot
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGModuleOutputSlot : CGModuleSlot
	{
		public OutputSlotInfo OutputInfo
		{
			get
			{
				return base.Info as OutputSlotInfo;
			}
		}

		protected override void LoadLinkedSlots()
		{
			if (!base.Module.Generator.IsInitialized)
			{
				return;
			}
			base.LoadLinkedSlots();
			this.mLinkedSlots = new List<CGModuleSlot>();
			List<CGModuleLink> outputLinks = base.Module.GetOutputLinks(this);
			foreach (CGModuleLink cgmoduleLink in outputLinks)
			{
				CGModule module = base.Module.Generator.GetModule(cgmoduleLink.TargetModuleID, true);
				if (module)
				{
					CGModuleInputSlot cgmoduleInputSlot = module.InputByName[cgmoduleLink.TargetSlotName];
					if (!cgmoduleInputSlot.Module.GetInputLink(cgmoduleInputSlot, this))
					{
						cgmoduleInputSlot.Module.InputLinks.Add(new CGModuleLink(cgmoduleInputSlot, this));
						cgmoduleInputSlot.ReInitializeLinkedSlots();
					}
					if (!this.mLinkedSlots.Contains(cgmoduleInputSlot))
					{
						this.mLinkedSlots.Add(cgmoduleInputSlot);
					}
				}
				else
				{
					base.Module.OutputLinks.Remove(cgmoduleLink);
				}
			}
		}

		public override void LinkTo(CGModuleSlot inputSlot)
		{
			if (!base.HasLinkTo(inputSlot))
			{
				if (!inputSlot.Info.Array && inputSlot.IsLinked)
				{
					inputSlot.UnlinkAll();
				}
				base.Module.OutputLinks.Add(new CGModuleLink(this, inputSlot));
				inputSlot.Module.InputLinks.Add(new CGModuleLink(inputSlot, this));
				if (!base.LinkedSlots.Contains(inputSlot))
				{
					base.LinkedSlots.Add(inputSlot);
				}
				if (!inputSlot.LinkedSlots.Contains(this))
				{
					inputSlot.LinkedSlots.Add(this);
				}
				base.LinkTo(inputSlot);
			}
		}

		public override void UnlinkFrom(CGModuleSlot inputSlot)
		{
			if (base.HasLinkTo(inputSlot))
			{
				CGModuleInputSlot inSlot = (CGModuleInputSlot)inputSlot;
				CGModuleLink outputLink = base.Module.GetOutputLink(this, inSlot);
				base.Module.OutputLinks.Remove(outputLink);
				CGModuleLink inputLink = inputSlot.Module.GetInputLink(inSlot, this);
				inputSlot.Module.InputLinks.Remove(inputLink);
				base.LinkedSlots.Remove(inputSlot);
				inputSlot.LinkedSlots.Remove(this);
				base.UnlinkFrom(inputSlot);
			}
		}

		public bool HasData
		{
			get
			{
				return this.Data != null && this.Data.Length > 0 && this.Data[0] != null;
			}
		}

		public void ClearData()
		{
			this.Data = new CGData[0];
		}

		public void SetData<T>(List<T> data) where T : CGData
		{
			if (data == null)
			{
				this.Data = new CGData[0];
			}
			else
			{
				if (!base.Info.Array && data.Count > 1)
				{
					UnityEngine.Debug.LogWarning(string.Concat(new string[]
					{
						"[Curvy] ",
						base.Module.GetType().Name,
						" (",
						base.Info.Name,
						") only supports a single data item! Either avoid calculating unneccessary data or define the slot as an array!"
					}));
				}
				this.Data = data.ToArray();
			}
		}

		public void SetData(params CGData[] data)
		{
			this.Data = ((data != null) ? data : new CGData[0]);
		}

		public T GetData<T>() where T : CGData
		{
			return (this.Data.Length != 0) ? (this.Data[0] as T) : ((T)((object)null));
		}

		public T[] GetAllData<T>() where T : CGData
		{
			return this.Data as T[];
		}

		public CGData[] Data = new CGData[0];

		public CGDataRequestParameter[] LastRequestParameters;
	}
}
