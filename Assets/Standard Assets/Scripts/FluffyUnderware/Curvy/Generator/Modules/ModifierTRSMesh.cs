// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.ModifierTRSMesh
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/TRS Mesh", ModuleName = "TRS Mesh", Description = "Transform,Rotate,Scale a VMesh")]
	[HelpURL("https://curvyeditor.com/doclink/cgtrsmesh")]
	public class ModifierTRSMesh : TRSModuleBase
	{
		public override void Refresh()
		{
			base.Refresh();
			if (this.OutVMesh.IsLinked)
			{
				List<CGVMesh> allData = this.InVMesh.GetAllData<CGVMesh>(new CGDataRequestParameter[0]);
				Matrix4x4 matrix = base.Matrix;
				for (int i = 0; i < allData.Count; i++)
				{
					allData[i].TRS(matrix);
				}
				this.OutVMesh.SetData<CGVMesh>(allData);
			}
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGVMesh)
		}, Array = true, ModifiesData = true)]
		public CGModuleInputSlot InVMesh = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGVMesh), Array = true)]
		public CGModuleOutputSlot OutVMesh = new CGModuleOutputSlot();
	}
}
