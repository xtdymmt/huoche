// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.InputGameObject
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Input/Game Objects", ModuleName = "Input GameObjects", Description = "")]
	[HelpURL("https://curvyeditor.com/doclink/cginputgameobject")]
	public class InputGameObject : CGModule
	{
		public List<CGGameObjectProperties> GameObjects
		{
			get
			{
				return this.m_GameObjects;
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
			this.GameObjects.Clear();
			base.Dirty = true;
		}

		public override void Refresh()
		{
			base.Refresh();
			if (this.OutGameObject.IsLinked)
			{
				CGGameObject[] array = new CGGameObject[this.GameObjects.Count];
				int newSize = 0;
				for (int i = 0; i < this.GameObjects.Count; i++)
				{
					if (this.GameObjects[i] != null)
					{
						array[newSize++] = new CGGameObject(this.GameObjects[i]);
					}
				}
				Array.Resize<CGGameObject>(ref array, newSize);
				this.OutGameObject.SetData(array);
			}
		}

		public override void OnTemplateCreated()
		{
			base.OnTemplateCreated();
			this.GameObjects.Clear();
		}

		[HideInInspector]
		[OutputSlotInfo(typeof(CGGameObject), Array = true)]
		public CGModuleOutputSlot OutGameObject = new CGModuleOutputSlot();

		[ArrayEx]
		[SerializeField]
		private List<CGGameObjectProperties> m_GameObjects = new List<CGGameObjectProperties>();
	}
}
