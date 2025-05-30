// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.ModifierTRSShape
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/TRS Shape", ModuleName = "TRS Shape", Description = "Transform,Rotate,Scale a Shape")]
	[HelpURL("https://curvyeditor.com/doclink/cgtrsshape")]
	public class ModifierTRSShape : TRSModuleBase, IOnRequestPath, IOnRequestProcessing
	{
		public float PathLength
		{
			get
			{
				return (!this.IsConfigured) ? 0f : this.InShape.SourceSlot(0).OnRequestPathModule.PathLength;
			}
		}

		public bool PathIsClosed
		{
			get
			{
				return this.IsConfigured && this.InShape.SourceSlot(0).OnRequestPathModule.PathIsClosed;
			}
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			if (requestedSlot == this.OutShape)
			{
				CGShape data = this.InShape.GetData<CGShape>(requests);
				Matrix4x4 matrix = base.Matrix;
				Matrix4x4 matrix4x = Matrix4x4.TRS(base.Transpose, Quaternion.Euler(base.Rotation), Vector3.one);
				for (int i = 0; i < data.Count; i++)
				{
					data.Position[i] = matrix.MultiplyPoint3x4(data.Position[i]);
					data.Normal[i] = matrix4x.MultiplyPoint3x4(data.Normal[i]);
				}
				data.Recalculate();
				return new CGData[]
				{
					data
				};
			}
			return null;
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGShape)
		}, Name = "Shape A", ModifiesData = true)]
		public CGModuleInputSlot InShape = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGShape))]
		public CGModuleOutputSlot OutShape = new CGModuleOutputSlot();
	}
}
