// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.ModifierMixShapes
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using JetBrains.Annotations;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/Mix Shapes", ModuleName = "Mix Shapes", Description = "Lerps between two shapes")]
	[HelpURL("https://curvyeditor.com/doclink/cgmixshapes")]
	public class ModifierMixShapes : CGModule, IOnRequestPath, IOnRequestProcessing
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
				return (!this.IsConfigured) ? 0f : Mathf.Max(this.InShapeA.SourceSlot(0).OnRequestPathModule.PathLength, this.InShapeB.SourceSlot(0).OnRequestPathModule.PathLength);
			}
		}

		public bool PathIsClosed
		{
			get
			{
				return this.IsConfigured && this.InShapeA.SourceSlot(0).OnRequestPathModule.PathIsClosed && this.InShapeB.SourceSlot(0).OnRequestPathModule.PathIsClosed;
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
			CGShape data = this.InShapeA.GetData<CGShape>(requests);
			CGShape data2 = this.InShapeB.GetData<CGShape>(requests);
			CGShape cgshape = ModifierMixShapes.MixShapes(data, data2, this.Mix, this.UIMessages);
			return new CGData[]
			{
				cgshape
			};
		}

		public static CGShape MixShapes(CGShape shapeA, CGShape shapeB, float mix, [NotNull] List<string> warningsContainer)
		{
			CGShape mainShape = (shapeA.Count <= shapeB.Count) ? shapeB : shapeA;
			CGShape secondaryShape = (shapeA.Count <= shapeB.Count) ? shapeA : shapeB;
			CGShape cgshape = new CGShape();
			ModifierMixShapes.InterpolateShape(cgshape, mainShape, secondaryShape, mix, warningsContainer);
			return cgshape;
		}

		public static void InterpolateShape([NotNull] CGShape resultShape, CGShape mainShape, CGShape secondaryShape, float mix, [NotNull] List<string> warningsContainer)
		{
			float t = (mix + 1f) * 0.5f;
			Vector3[] array = new Vector3[mainShape.Count];
			Vector3[] array2 = new Vector3[mainShape.Count];
			Bounds bounds = default(Bounds);
			for (int i = 0; i < mainShape.Count; i++)
			{
				float t2;
				int findex = secondaryShape.GetFIndex(mainShape.F[i], out t2);
				Vector3 b = Vector3.LerpUnclamped(secondaryShape.Position[findex], secondaryShape.Position[findex + 1], t2);
				array[i] = Vector3.LerpUnclamped(mainShape.Position[i], b, t);
				Vector3 b2 = Vector3.LerpUnclamped(secondaryShape.Normal[findex], secondaryShape.Normal[findex + 1], t2);
				array2[i] = Vector3.LerpUnclamped(mainShape.Normal[i], b2, t);
				bounds.Encapsulate(array[i]);
			}
			resultShape.Position = array;
			resultShape.Normal = array2;
			resultShape.Map = (float[])mainShape.Map.Clone();
			resultShape.F = new float[mainShape.Count];
			resultShape.Recalculate();
			resultShape.SourceF = (float[])mainShape.SourceF.Clone();
			resultShape.Bounds = bounds;
			resultShape.MaterialGroups = new List<SamplePointsMaterialGroup>(mainShape.MaterialGroups);
			if (mainShape.Closed != secondaryShape.Closed)
			{
				warningsContainer.Add("Mixing inputs with different Closed values is not supported");
			}
			if (mainShape.Seamless != secondaryShape.Seamless)
			{
				warningsContainer.Add("Mixing inputs with different Seamless values is not supported");
			}
			if (mainShape.SourceIsManaged != secondaryShape.SourceIsManaged)
			{
				warningsContainer.Add("Mixing inputs with different SourceIsManaged values is not supported");
			}
			resultShape.Closed = mainShape.Closed;
			resultShape.Seamless = mainShape.Seamless;
			resultShape.SourceIsManaged = mainShape.SourceIsManaged;
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGShape)
		}, Name = "Shape A")]
		public CGModuleInputSlot InShapeA = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGShape)
		}, Name = "Shape B")]
		public CGModuleInputSlot InShapeB = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGShape))]
		public CGModuleOutputSlot OutShape = new CGModuleOutputSlot();

		[SerializeField]
		[RangeEx(-1f, 1f, "", "", Tooltip = "Mix between the paths")]
		private float m_Mix;
	}
}
