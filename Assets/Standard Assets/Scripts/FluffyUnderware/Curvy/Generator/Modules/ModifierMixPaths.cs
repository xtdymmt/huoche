// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.ModifierMixPaths
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using JetBrains.Annotations;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/Mix Paths", ModuleName = "Mix Paths", Description = "Lerps between two paths")]
	[HelpURL("https://curvyeditor.com/doclink/cgmixpaths")]
	public class ModifierMixPaths : CGModule, IOnRequestPath, IOnRequestProcessing
	{
		public float Mix
		{
			get
			{
				return this.m_Mix;
			}
			set
			{
				if (this.m_Mix != value)
				{
					this.m_Mix = value;
				}
				base.Dirty = true;
			}
		}

		public float PathLength
		{
			get
			{
				return (!this.IsConfigured) ? 0f : Mathf.Max(this.InPathA.SourceSlot(0).OnRequestPathModule.PathLength, this.InPathB.SourceSlot(0).OnRequestPathModule.PathLength);
			}
		}

		public bool PathIsClosed
		{
			get
			{
				return this.IsConfigured && this.InPathA.SourceSlot(0).OnRequestPathModule.PathIsClosed && this.InPathB.SourceSlot(0).OnRequestPathModule.PathIsClosed;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.Properties.MinWidth = 200f;
			this.Properties.LabelWidth = 50f;
		}

		public override void Reset()
		{
			base.Reset();
			this.Mix = 0f;
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			CGDataRequestRasterization requestParameter = CGModule.GetRequestParameter<CGDataRequestRasterization>(ref requests);
			if (!requestParameter)
			{
				return null;
			}
			CGPath data = this.InPathA.GetData<CGPath>(requests);
			CGPath data2 = this.InPathB.GetData<CGPath>(requests);
			return new CGData[]
			{
				ModifierMixPaths.MixPath(data, data2, this.Mix, this.UIMessages)
			};
		}

		public static CGPath MixPath(CGPath pathA, CGPath pathB, float mix, [NotNull] List<string> warningsContainer)
		{
			CGPath cgpath = (pathA.Count <= pathB.Count) ? pathB : pathA;
			CGPath cgpath2 = (pathA.Count <= pathB.Count) ? pathA : pathB;
			CGPath cgpath3 = new CGPath();
			cgpath3.Direction = new Vector3[cgpath.Count];
			ModifierMixShapes.InterpolateShape(cgpath3, cgpath, cgpath2, mix, warningsContainer);
			float t = (mix + 1f) * 0.5f;
			Vector3[] array = new Vector3[cgpath.Count];
			for (int i = 0; i < cgpath.Count; i++)
			{
				float t2;
				int findex = cgpath2.GetFIndex(cgpath.F[i], out t2);
				Vector3 b = Vector3.SlerpUnclamped(cgpath2.Direction[findex], cgpath2.Direction[findex + 1], t2);
				array[i] = Vector3.SlerpUnclamped(cgpath.Direction[i], b, t);
			}
			cgpath3.Direction = array;
			return cgpath3;
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGPath)
		}, Name = "Path A")]
		public CGModuleInputSlot InPathA = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGPath)
		}, Name = "Path B")]
		public CGModuleInputSlot InPathB = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath))]
		public CGModuleOutputSlot OutPath = new CGModuleOutputSlot();

		[SerializeField]
		[RangeEx(-1f, 1f, "", "", Tooltip = "Mix between the paths")]
		private float m_Mix;
	}
}
