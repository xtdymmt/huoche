// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.InputMesh
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Input/Meshes", ModuleName = "Input Meshes", Description = "Create VMeshes")]
	[HelpURL("https://curvyeditor.com/doclink/cginputmesh")]
	public class InputMesh : CGModule, IExternalInput
	{
		public List<CGMeshProperties> Meshes
		{
			get
			{
				return this.m_Meshes;
			}
		}

		public bool SupportsIPE
		{
			get
			{
				return false;
			}
		}

		public override void Reset()
		{
			base.Reset();
			this.Meshes.Clear();
			base.Dirty = true;
		}

		public override void Refresh()
		{
			base.Refresh();
			if (this.OutVMesh.IsLinked)
			{
				CGVMesh[] array = new CGVMesh[this.Meshes.Count];
				int newSize = 0;
				for (int i = 0; i < this.Meshes.Count; i++)
				{
					if (this.Meshes[i].Mesh)
					{
						array[newSize++] = new CGVMesh(this.Meshes[i]);
					}
				}
				Array.Resize<CGVMesh>(ref array, newSize);
				this.OutVMesh.SetData(array);
			}
		}

		public override void OnTemplateCreated()
		{
			base.OnTemplateCreated();
			this.Meshes.Clear();
		}

		[HideInInspector]
		[OutputSlotInfo(typeof(CGVMesh), Array = true)]
		public CGModuleOutputSlot OutVMesh = new CGModuleOutputSlot();

		[SerializeField]
		[ArrayEx]
		private List<CGMeshProperties> m_Meshes = new List<CGMeshProperties>(new CGMeshProperties[]
		{
			new CGMeshProperties()
		});
	}
}
