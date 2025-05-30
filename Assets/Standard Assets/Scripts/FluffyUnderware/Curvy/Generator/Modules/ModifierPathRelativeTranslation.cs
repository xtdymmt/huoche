// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.ModifierPathRelativeTranslation
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/Path Relative Translation", ModuleName = "Path Relative Translation", Description = "Translates a path relatively to it's direction, instead of relatively to the world as does the TRS Path module.")]
	[HelpURL("https://curvyeditor.com/doclink/cgpathrelativetranslation")]
	public class ModifierPathRelativeTranslation : CGModule, IOnRequestPath, IOnRequestProcessing
	{
		public float LateralTranslation
		{
			get
			{
				return this.lateralTranslation;
			}
			set
			{
				if (this.lateralTranslation != value)
				{
					this.lateralTranslation = value;
					base.Dirty = true;
				}
			}
		}

		public float PathLength
		{
			get
			{
				return (!this.IsConfigured) ? 0f : this.InPath.SourceSlot(0).OnRequestPathModule.PathLength;
			}
		}

		public bool PathIsClosed
		{
			get
			{
				return this.IsConfigured && this.InPath.SourceSlot(0).OnRequestPathModule.PathIsClosed;
			}
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			if (requestedSlot == this.OutPath)
			{
				CGPath data = this.InPath.GetData<CGPath>(requests);
				for (int i = 0; i < data.Count; i++)
				{
					Vector3 vector = Vector3.Cross(data.Normal[i], data.Direction[i]) * this.lateralTranslation;
					data.Position[i].x = data.Position[i].x + vector.x;
					data.Position[i].y = data.Position[i].y + vector.y;
					data.Position[i].z = data.Position[i].z + vector.z;
				}
				data.Recalculate();
				return new CGData[]
				{
					data
				};
			}
			return null;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.Properties.MinWidth = 230f;
			this.Properties.LabelWidth = 165f;
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGPath)
		}, Name = "Path A", ModifiesData = true)]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath))]
		public CGModuleOutputSlot OutPath = new CGModuleOutputSlot();

		[SerializeField]
		[Tooltip("The translation amount")]
		private float lateralTranslation;
	}
}
