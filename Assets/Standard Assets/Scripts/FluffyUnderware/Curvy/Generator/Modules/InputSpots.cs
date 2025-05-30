// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.InputSpots
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Input/Spots", ModuleName = "Input Spots", Description = "Defines an array of placement spots")]
	[HelpURL("https://curvyeditor.com/doclink/cginputspots")]
	public class InputSpots : CGModule
	{
		public List<CGSpot> Spots
		{
			get
			{
				return this.m_Spots;
			}
			set
			{
				if (this.m_Spots != value)
				{
					this.m_Spots = value;
				}
				base.Dirty = true;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.Properties.MinWidth = 250f;
		}

		public override void Reset()
		{
			base.Reset();
			this.Spots.Clear();
			base.Dirty = true;
		}

		public override void OnStateChange()
		{
			base.OnStateChange();
		}

		public override void Refresh()
		{
			if (this.OutSpots.IsLinked)
			{
				this.OutSpots.SetData(new CGData[]
				{
					new CGSpots(this.Spots.ToArray())
				});
			}
		}

		[HideInInspector]
		[OutputSlotInfo(typeof(CGSpots))]
		public CGModuleOutputSlot OutSpots = new CGModuleOutputSlot();

		[ArrayEx]
		[SerializeField]
		private List<CGSpot> m_Spots = new List<CGSpot>();
	}
}
