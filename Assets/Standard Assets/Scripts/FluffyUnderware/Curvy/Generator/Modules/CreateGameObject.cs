// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.CreateGameObject
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Create/GameObject", ModuleName = "Create GameObject")]
	[HelpURL("https://curvyeditor.com/doclink/cgcreategameobject")]
	public class CreateGameObject : CGModule
	{
		public int Layer
		{
			get
			{
				return this.m_Layer;
			}
			set
			{
				int num = Mathf.Clamp(value, 0, 32);
				if (this.m_Layer != num)
				{
					this.m_Layer = num;
				}
				base.Dirty = true;
			}
		}

		public bool MakeStatic
		{
			get
			{
				return this.m_MakeStatic;
			}
			set
			{
				if (this.m_MakeStatic != value)
				{
					this.m_MakeStatic = value;
				}
				base.Dirty = true;
			}
		}

		public CGGameObjectResourceCollection GameObjects
		{
			get
			{
				return this.m_Resources;
			}
		}

		public int GameObjectCount
		{
			get
			{
				return this.GameObjects.Count;
			}
		}

		public override void Reset()
		{
			base.Reset();
			this.MakeStatic = false;
			this.Layer = 0;
			this.Clear();
		}

		protected override void OnDestroy()
		{
			if (!base.Generator.Destroying)
			{
				base.DeleteAllPrefabPools();
			}
			base.OnDestroy();
		}

		public override void OnTemplateCreated()
		{
			this.Clear();
		}

		public void Clear()
		{
			for (int i = 0; i < this.GameObjects.Count; i++)
			{
				base.DeleteManagedResource("GameObject", this.GameObjects.Items[i], this.GameObjects.PoolNames[i], false);
			}
			this.GameObjects.Items.Clear();
			this.GameObjects.PoolNames.Clear();
		}

		public override void OnStateChange()
		{
			base.OnStateChange();
			if (!this.IsConfigured)
			{
				this.Clear();
			}
		}

		public override void Refresh()
		{
			base.Refresh();
			List<CGGameObject> allData = this.InGameObjectArray.GetAllData<CGGameObject>(new CGDataRequestParameter[0]);
			CGSpots data = this.InSpots.GetData<CGSpots>(new CGDataRequestParameter[0]);
			this.Clear();
			List<IPool> allPrefabPools = base.GetAllPrefabPools();
			HashSet<string> hashSet = new HashSet<string>();
			if (allData.Count > 0 && data.Count > 0)
			{
				for (int i = 0; i < data.Count; i++)
				{
					CGSpot cgspot = data.Points[i];
					int index = cgspot.Index;
					if (index >= 0 && index < allData.Count && allData[index].Object != null)
					{
						string identifier = base.GetPrefabPool(allData[index].Object).Identifier;
						hashSet.Add(identifier);
						Transform transform = (Transform)base.AddManagedResource("GameObject", identifier, i);
						transform.gameObject.isStatic = this.MakeStatic;
						transform.gameObject.layer = this.Layer;
						transform.localPosition = cgspot.Position;
						transform.localRotation = cgspot.Rotation;
						transform.localScale = new Vector3(transform.localScale.x * cgspot.Scale.x * allData[index].Scale.x, transform.localScale.y * cgspot.Scale.y * allData[index].Scale.y, transform.localScale.z * cgspot.Scale.z * allData[index].Scale.z);
						if (allData[index].Matrix != Matrix4x4.identity)
						{
							transform.Translate(allData[index].Translate);
							transform.Rotate(allData[index].Rotate);
						}
						this.GameObjects.Items.Add(transform);
						this.GameObjects.PoolNames.Add(identifier);
					}
				}
			}
			foreach (IPool pool in allPrefabPools)
			{
				if (!hashSet.Contains(pool.Identifier))
				{
					base.Generator.PoolManager.DeletePool(pool);
				}
			}
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGGameObject)
		}, Array = true, Name = "GameObject")]
		public CGModuleInputSlot InGameObjectArray = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGSpots)
		}, Name = "Spots")]
		public CGModuleInputSlot InSpots = new CGModuleInputSlot();

		[SerializeField]
		[CGResourceCollectionManager("GameObject", ShowCount = true)]
		private CGGameObjectResourceCollection m_Resources = new CGGameObjectResourceCollection();

		[Tab("General")]
		[SerializeField]
		private bool m_MakeStatic;

		[SerializeField]
		[Layer("", "")]
		private int m_Layer;
	}
}
