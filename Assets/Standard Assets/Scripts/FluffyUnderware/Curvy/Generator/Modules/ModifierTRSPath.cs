// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.ModifierTRSPath
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/TRS Path", ModuleName = "TRS Path", Description = "Transform,Rotate,Scale a Path")]
	[HelpURL("https://curvyeditor.com/doclink/cgtrspath")]
	public class ModifierTRSPath : TRSModuleBase, IOnRequestPath, IOnRequestProcessing
	{
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
				Matrix4x4 matrix = base.Matrix;
				Matrix4x4 matrix4x = Matrix4x4.TRS(base.Transpose, Quaternion.Euler(base.Rotation), Vector3.one);
				for (int i = 0; i < data.Count; i++)
				{
					data.Position[i] = matrix.MultiplyPoint3x4(data.Position[i]);
					data.Normal[i] = matrix4x.MultiplyVector(data.Normal[i]);
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
			typeof(CGPath)
		}, Name = "Path A", ModifiesData = true)]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath))]
		public CGModuleOutputSlot OutPath = new CGModuleOutputSlot();
	}
}
