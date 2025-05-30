// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGModuleInputSlot
using System;
using System.Collections.Generic;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGModuleInputSlot : CGModuleSlot
	{
		public InputSlotInfo InputInfo
		{
			get
			{
				return base.Info as InputSlotInfo;
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
			List<CGModuleLink> inputLinks = base.Module.GetInputLinks(this);
			foreach (CGModuleLink cgmoduleLink in inputLinks)
			{
				CGModule module = base.Module.Generator.GetModule(cgmoduleLink.TargetModuleID, true);
				if (module)
				{
					CGModuleOutputSlot cgmoduleOutputSlot = module.OutputByName[cgmoduleLink.TargetSlotName];
					if (!cgmoduleOutputSlot.Module.GetOutputLink(cgmoduleOutputSlot, this))
					{
						cgmoduleOutputSlot.Module.OutputLinks.Add(new CGModuleLink(cgmoduleOutputSlot, this));
						cgmoduleOutputSlot.ReInitializeLinkedSlots();
					}
					if (!this.mLinkedSlots.Contains(cgmoduleOutputSlot))
					{
						this.mLinkedSlots.Add(cgmoduleOutputSlot);
					}
				}
				else
				{
					base.Module.InputLinks.Remove(cgmoduleLink);
				}
			}
		}

		public override void UnlinkAll()
		{
			List<CGModuleSlot> list = new List<CGModuleSlot>(base.LinkedSlots);
			foreach (CGModuleSlot other in list)
			{
				this.UnlinkFrom(other);
			}
		}

		public override void LinkTo(CGModuleSlot outputSlot)
		{
			if (!base.HasLinkTo(outputSlot))
			{
				base.Module.InputLinks.Add(new CGModuleLink(this, outputSlot));
				outputSlot.Module.OutputLinks.Add(new CGModuleLink(outputSlot, this));
				if (!base.LinkedSlots.Contains(outputSlot))
				{
					base.LinkedSlots.Add(outputSlot);
				}
				if (!outputSlot.LinkedSlots.Contains(this))
				{
					outputSlot.LinkedSlots.Add(this);
				}
				base.LinkTo(outputSlot);
			}
		}

		public override void UnlinkFrom(CGModuleSlot outputSlot)
		{
			if (base.HasLinkTo(outputSlot))
			{
				CGModuleOutputSlot outSlot = (CGModuleOutputSlot)outputSlot;
				CGModuleLink inputLink = base.Module.GetInputLink(this, outSlot);
				base.Module.InputLinks.Remove(inputLink);
				CGModuleLink outputLink = outputSlot.Module.GetOutputLink(outSlot, this);
				outputSlot.Module.OutputLinks.Remove(outputLink);
				base.LinkedSlots.Remove(outputSlot);
				outputSlot.LinkedSlots.Remove(this);
				base.UnlinkFrom(outputSlot);
			}
		}

		public CGModuleOutputSlot SourceSlot(int index = 0)
		{
			return (index >= base.Count || index < 0) ? null : ((CGModuleOutputSlot)base.LinkedSlots[index]);
		}

		public bool CanLinkTo(CGModuleOutputSlot source)
		{
			return source.Module != base.Module && CGModuleInputSlot.AreInputAndOutputSlotsCompatible(this.InputInfo, base.OnRequestModule != null, source.OutputInfo, source.OnRequestModule != null);
		}

		public static bool AreInputAndOutputSlotsCompatible(InputSlotInfo inputSlotInfo, bool inputSlotModuleIsOnRequest, OutputSlotInfo outputSlotInfo, bool outputSlotModuleIsOnRequest)
		{
			return inputSlotInfo.IsValidFrom(outputSlotInfo.DataType) && ((outputSlotModuleIsOnRequest && (inputSlotInfo.RequestDataOnly || inputSlotModuleIsOnRequest)) || (!outputSlotModuleIsOnRequest && !inputSlotInfo.RequestDataOnly));
		}

		private CGModule SourceModule(int index)
		{
			return (index >= base.Count || index < 0) ? null : base.LinkedSlots[index].Module;
		}

		public T GetData<T>(params CGDataRequestParameter[] requests) where T : CGData
		{
			CGData[] data = this.GetData<T>(0, requests);
			return (data != null && data.Length != 0) ? (data[0] as T) : ((T)((object)null));
		}

		public List<T> GetAllData<T>(params CGDataRequestParameter[] requests) where T : CGData
		{
			List<T> list = new List<T>();
			for (int i = 0; i < base.Count; i++)
			{
				CGData[] data = this.GetData<T>(i, requests);
				if (data != null)
				{
					if (!base.Info.Array)
					{
						list.Add(data[0] as T);
						break;
					}
					list.Capacity += data.Length;
					for (int j = 0; j < data.Length; j++)
					{
						list.Add(data[j] as T);
					}
				}
			}
			return list;
		}

		private CGData[] GetData<T>(int slotIndex, params CGDataRequestParameter[] requests) where T : CGData
		{
			CGModuleOutputSlot cgmoduleOutputSlot = this.SourceSlot(slotIndex);
			if (!cgmoduleOutputSlot)
			{
				return new CGData[0];
			}
			if (!cgmoduleOutputSlot.Module.Active)
			{
				return new T[0];
			}
			bool flag = this.InputInfo.ModifiesData && (cgmoduleOutputSlot.Module is IOnRequestProcessing || cgmoduleOutputSlot.Count > 1);
			if (cgmoduleOutputSlot.Module is IOnRequestProcessing)
			{
				bool flag2 = cgmoduleOutputSlot.Data == null || cgmoduleOutputSlot.Data.Length == 0;
				if (!flag2 && cgmoduleOutputSlot.LastRequestParameters != null && cgmoduleOutputSlot.LastRequestParameters.Length == requests.Length)
				{
					for (int i = 0; i < requests.Length; i++)
					{
						if (!requests[i].Equals(cgmoduleOutputSlot.LastRequestParameters[i]))
						{
							flag2 = true;
							break;
						}
					}
				}
				else
				{
					flag2 = true;
				}
				if (flag2)
				{
					cgmoduleOutputSlot.LastRequestParameters = requests;
					cgmoduleOutputSlot.Module.UIMessages.Clear();
					cgmoduleOutputSlot.SetData(((IOnRequestProcessing)cgmoduleOutputSlot.Module).OnSlotDataRequest(this, cgmoduleOutputSlot, requests));
				}
				if (flag)
				{
					return CGModuleInputSlot.cloneData<T>(ref cgmoduleOutputSlot.Data);
				}
				return cgmoduleOutputSlot.Data;
			}
			else
			{
				if (flag)
				{
					return CGModuleInputSlot.cloneData<T>(ref cgmoduleOutputSlot.Data);
				}
				return cgmoduleOutputSlot.Data;
			}
		}

		private static CGData[] cloneData<T>(ref CGData[] source) where T : CGData
		{
			T[] array = new T[source.Length];
			for (int i = 0; i < source.Length; i++)
			{
				array[i] = source[i].Clone<T>();
			}
			return array;
		}
	}
}
