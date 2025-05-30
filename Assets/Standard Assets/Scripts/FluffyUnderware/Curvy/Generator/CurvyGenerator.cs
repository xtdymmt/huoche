// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CurvyGenerator
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[ExecuteInEditMode]
	[HelpURL("https://curvyeditor.com/doclink/generator")]
	[AddComponentMenu("Curvy/Generator", 3)]
	[RequireComponent(typeof(PoolManager))]
	public class CurvyGenerator : DTVersionedMonoBehaviour
	{
		public bool ShowDebug
		{
			get
			{
				return this.m_ShowDebug;
			}
			set
			{
				if (this.m_ShowDebug != value)
				{
					this.m_ShowDebug = value;
				}
			}
		}

		public bool AutoRefresh
		{
			get
			{
				return this.m_AutoRefresh;
			}
			set
			{
				if (this.m_AutoRefresh != value)
				{
					this.m_AutoRefresh = value;
				}
			}
		}

		public int RefreshDelay
		{
			get
			{
				return this.m_RefreshDelay;
			}
			set
			{
				int num = Mathf.Max(0, value);
				if (this.m_RefreshDelay != num)
				{
					this.m_RefreshDelay = num;
				}
			}
		}

		public int RefreshDelayEditor
		{
			get
			{
				return this.m_RefreshDelayEditor;
			}
			set
			{
				int num = Mathf.Max(0, value);
				if (this.m_RefreshDelayEditor != num)
				{
					this.m_RefreshDelayEditor = num;
				}
			}
		}

		public PoolManager PoolManager
		{
			get
			{
				if (this.mPoolManager == null)
				{
					this.mPoolManager = base.GetComponent<PoolManager>();
				}
				return this.mPoolManager;
			}
		}

		public CurvyCGEvent OnRefresh
		{
			get
			{
				return this.m_OnRefresh;
			}
			set
			{
				if (this.m_OnRefresh != value)
				{
					this.m_OnRefresh = value;
				}
			}
		}

		public bool IsInitialized
		{
			get
			{
				return this.mInitialized;
			}
		}

		public bool Destroying { get; private set; }

		private void Awake()
		{
		}

		private void OnEnable()
		{
			this.PoolManager.AutoCreatePools = true;
		}

		private void OnDisable()
		{
			this.mInitialized = false;
			this.mInitializedPhaseOne = false;
			this.mNeedSort = true;
		}

		private void OnDestroy()
		{
			this.Destroying = true;
		}

		private void Update()
		{
			if (!this.IsInitialized)
			{
				this.Initialize(false);
			}
			else if (Application.isPlaying && this.AutoRefresh && DTTime.TimeSinceStartup - this.mLastUpdateTime > (double)((float)this.RefreshDelay * 0.001f))
			{
				this.mLastUpdateTime = DTTime.TimeSinceStartup;
				this.Refresh(false);
			}
		}

		public static CurvyGenerator Create()
		{
			GameObject gameObject = new GameObject("Curvy Generator", new Type[]
			{
				typeof(CurvyGenerator)
			});
			return gameObject.GetComponent<CurvyGenerator>();
		}

		public T AddModule<T>() where T : CGModule
		{
			return (T)((object)this.AddModule(typeof(T)));
		}

		public CGModule AddModule(Type type)
		{
			GameObject gameObject = new GameObject(string.Empty);
			gameObject.transform.SetParent(base.transform, false);
			CGModule cgmodule = (CGModule)gameObject.AddComponent(type);
			cgmodule.SetUniqueIdINTERNAL();
			this.Modules.Add(cgmodule);
			this.ModulesByID.Add(cgmodule.UniqueID, cgmodule);
			return cgmodule;
		}

		public void ArrangeModules()
		{
			Vector2 a = new Vector2(float.MaxValue, float.MaxValue);
			foreach (CGModule cgmodule in this.Modules)
			{
				a.x = Mathf.Min(cgmodule.Properties.Dimensions.x, a.x);
				a.y = Mathf.Min(cgmodule.Properties.Dimensions.y, a.y);
			}
			a -= new Vector2(10f, 10f);
			foreach (CGModule cgmodule2 in this.Modules)
			{
				CGModuleProperties properties = cgmodule2.Properties;
				properties.Dimensions.x = properties.Dimensions.x - a.x;
				CGModuleProperties properties2 = cgmodule2.Properties;
				properties2.Dimensions.y = properties2.Dimensions.y - a.y;
			}
		}

		public void ReorderModules()
		{
			Dictionary<CGModule, Rect> dictionary = new Dictionary<CGModule, Rect>(this.Modules.Count);
			foreach (CGModule cgmodule in this.Modules)
			{
				dictionary[cgmodule] = cgmodule.Properties.Dimensions;
			}
			List<CGModule> list = (from m in this.Modules
			where !m.OutputLinks.Any<CGModuleLink>()
			select m).ToList<CGModule>();
			Dictionary<CGModule, HashSet<CGModule>> dictionary2 = new Dictionary<CGModule, HashSet<CGModule>>(this.Modules.Count);
			foreach (CGModule moduleToAdd in list)
			{
				CurvyGenerator.UpdateModulesRecursiveInputs(dictionary2, moduleToAdd);
			}
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < list.Count; i++)
			{
				float num;
				if (i == 0)
				{
					num = 0f;
				}
				else
				{
					num = dictionary2[list[i - 1]].Max((CGModule m) => m.Properties.Dimensions.yMax) + 20f;
				}
				float y = num;
				CGModule cgmodule2 = list[i];
				cgmodule2.Properties.Dimensions.position = new Vector2(0f, y);
				hashSet.Add(cgmodule2.UniqueID);
				CurvyGenerator.ReorderEndpointRecursiveInputs(cgmodule2, hashSet, dictionary2);
			}
			this.ArrangeModules();
		}

		public void Clear()
		{
			this.clearModules();
		}

		public void DeleteModule(CGModule module)
		{
			if (module)
			{
				module.Delete();
			}
		}

		public List<T> FindModules<T>(bool includeOnRequestProcessing = false) where T : CGModule
		{
			List<T> list = new List<T>();
			for (int i = 0; i < this.Modules.Count; i++)
			{
				if (this.Modules[i] is T && (includeOnRequestProcessing || !(this.Modules[i] is IOnRequestProcessing)))
				{
					list.Add((T)((object)this.Modules[i]));
				}
			}
			return list;
		}

		public List<CGModule> GetModules(bool includeOnRequestProcessing = false)
		{
			if (includeOnRequestProcessing)
			{
				return new List<CGModule>(this.Modules);
			}
			List<CGModule> list = new List<CGModule>();
			for (int i = 0; i < this.Modules.Count; i++)
			{
				if (!(this.Modules[i] is IOnRequestProcessing))
				{
					list.Add(this.Modules[i]);
				}
			}
			return list;
		}

		public CGModule GetModule(int moduleID, bool includeOnRequestProcessing = false)
		{
			CGModule cgmodule;
			if (this.ModulesByID.TryGetValue(moduleID, out cgmodule) && (includeOnRequestProcessing || !(cgmodule is IOnRequestProcessing)))
			{
				return cgmodule;
			}
			return null;
		}

		public T GetModule<T>(int moduleID, bool includeOnRequestProcessing = false) where T : CGModule
		{
			return this.GetModule(moduleID, includeOnRequestProcessing) as T;
		}

		public CGModule GetModule(string moduleName, bool includeOnRequestProcessing = false)
		{
			for (int i = 0; i < this.Modules.Count; i++)
			{
				if (this.Modules[i].ModuleName.Equals(moduleName, StringComparison.CurrentCultureIgnoreCase) && (includeOnRequestProcessing || !(this.Modules[i] is IOnRequestProcessing)))
				{
					return this.Modules[i];
				}
			}
			return null;
		}

		public T GetModule<T>(string moduleName, bool includeOnRequestProcessing = false) where T : CGModule
		{
			return this.GetModule(moduleName, includeOnRequestProcessing) as T;
		}

		public CGModuleOutputSlot GetModuleOutputSlot(int moduleId, string slotName)
		{
			CGModule module = this.GetModule(moduleId, false);
			if (module)
			{
				return module.GetOutputSlot(slotName);
			}
			return null;
		}

		public CGModuleOutputSlot GetModuleOutputSlot(string moduleName, string slotName)
		{
			CGModule module = this.GetModule(moduleName, false);
			if (module)
			{
				return module.GetOutputSlot(slotName);
			}
			return null;
		}

		public void Initialize(bool force = false)
		{
			if (!this.mInitializedPhaseOne || force)
			{
				this.Modules = new List<CGModule>(base.GetComponentsInChildren<CGModule>());
				this.ModulesByID.Clear();
				for (int i = 0; i < this.Modules.Count; i++)
				{
					if (!this.Modules[i].IsInitialized || force)
					{
						this.Modules[i].Initialize();
					}
					if (this.ModulesByID.ContainsKey(this.Modules[i].UniqueID))
					{
						UnityEngine.Debug.LogError("ID of '" + this.Modules[i].ModuleName + "' isn't unique!");
						return;
					}
					this.ModulesByID.Add(this.Modules[i].UniqueID, this.Modules[i]);
				}
				if (this.Modules.Count > 0)
				{
					this.sortModulesINTERNAL();
				}
				this.mInitializedPhaseOne = true;
			}
			for (int j = 0; j < this.Modules.Count; j++)
			{
				if (this.Modules[j] is IExternalInput && !this.Modules[j].IsInitialized)
				{
					return;
				}
			}
			this.mInitialized = true;
			this.mInitializedPhaseOne = false;
			this.mNeedSort = (this.mNeedSort || force);
			this.Refresh(true);
		}

		public void Refresh(bool forceUpdate = false)
		{
			if (!this.IsInitialized)
			{
				return;
			}
			if (this.mNeedSort)
			{
				this.doSortModules();
			}
			CGModule cgmodule = null;
			for (int i = 0; i < this.Modules.Count; i++)
			{
				if (forceUpdate && this.Modules[i] is IOnRequestProcessing)
				{
					this.Modules[i].Dirty = true;
				}
				if (!(this.Modules[i] is INoProcessing) && (this.Modules[i].Dirty || (forceUpdate && !(this.Modules[i] is IOnRequestProcessing))))
				{
					this.Modules[i].checkOnStateChangedINTERNAL();
					if (this.Modules[i].IsInitialized && this.Modules[i].IsConfigured)
					{
						if (cgmodule == null)
						{
							cgmodule = this.Modules[i];
						}
						this.Modules[i].doRefresh();
					}
				}
			}
			if (cgmodule != null)
			{
				this.OnRefreshEvent(new CurvyCGEventArgs(this, cgmodule));
			}
		}

		protected CurvyCGEventArgs OnRefreshEvent(CurvyCGEventArgs e)
		{
			if (this.OnRefresh != null)
			{
				this.OnRefresh.Invoke(e);
			}
			return e;
		}

		private void clearModules()
		{
			for (int i = this.Modules.Count - 1; i >= 0; i--)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(this.Modules[i].gameObject);
				}
			}
			this.Modules.Clear();
			this.ModulesByID.Clear();
			this.m_LastModuleID = 0;
		}

		public string getUniqueModuleNameINTERNAL(string name)
		{
			string text = name;
			int num = 1;
			bool flag;
			do
			{
				flag = true;
				foreach (CGModule cgmodule in this.Modules)
				{
					if (cgmodule.ModuleName.Equals(text, StringComparison.CurrentCultureIgnoreCase))
					{
						text = name + num++.ToString(CultureInfo.InvariantCulture);
						flag = false;
						break;
					}
				}
			}
			while (!flag);
			return text;
		}

		internal void sortModulesINTERNAL()
		{
			this.mNeedSort = true;
		}

		private bool doSortModules()
		{
			List<CGModule> list = new List<CGModule>(this.Modules);
			List<CGModule> list2 = new List<CGModule>();
			List<CGModule> list3 = new List<CGModule>();
			for (int i = list.Count - 1; i >= 0; i--)
			{
				list[i].initializeSort();
				if (list[i] is INoProcessing)
				{
					list3.Add(list[i]);
					list.RemoveAt(i);
				}
				else if (list[i].SortAncestors == 0)
				{
					list2.Add(list[i]);
					list.RemoveAt(i);
				}
			}
			this.Modules.Clear();
			int num = 0;
			while (list2.Count > 0)
			{
				CGModule cgmodule = list2[0];
				list2.RemoveAt(0);
				List<CGModule> list4 = cgmodule.decrementChilds();
				list2.AddRange(list4);
				for (int j = 0; j < list4.Count; j++)
				{
					list.Remove(list4[j]);
				}
				this.Modules.Add(cgmodule);
				cgmodule.transform.SetSiblingIndex(num++);
			}
			for (int k = 0; k < list.Count; k++)
			{
				list[k].CircularReferenceError = true;
			}
			this.Modules.AddRange(list);
			this.Modules.AddRange(list3);
			this.mNeedSort = false;
			return list.Count > 0;
		}

		private static void ReorderEndpointRecursiveInputs(CGModule endPoint, HashSet<int> reordredModuleIds, Dictionary<CGModule, HashSet<CGModule>> modulesRecursiveInputs)
		{
			float num = endPoint.Properties.Dimensions.xMin - 50f;
			float num2 = endPoint.Properties.Dimensions.yMin;
			List<CGModule> list = endPoint.Input.SelectMany((CGModuleInputSlot i) => i.GetLinkedModules()).ToList<CGModule>();
			foreach (CGModule cgmodule in list)
			{
				float num3 = num - cgmodule.Properties.Dimensions.width;
				if (!reordredModuleIds.Contains(cgmodule.UniqueID))
				{
					cgmodule.Properties.Dimensions.position = new Vector2(num3, num2);
					reordredModuleIds.Add(cgmodule.UniqueID);
					CurvyGenerator.ReorderEndpointRecursiveInputs(cgmodule, reordredModuleIds, modulesRecursiveInputs);
				}
				else if (num3 < cgmodule.Properties.Dimensions.xMin)
				{
					cgmodule.Properties.Dimensions.position = new Vector2(num3, cgmodule.Properties.Dimensions.yMin);
					CurvyGenerator.ReorderEndpointRecursiveInputs(cgmodule, reordredModuleIds, modulesRecursiveInputs);
				}
				num2 = Math.Max(num2, modulesRecursiveInputs[cgmodule].Max((CGModule m) => m.Properties.Dimensions.yMax) + 20f);
			}
		}

		private static HashSet<CGModule> UpdateModulesRecursiveInputs(Dictionary<CGModule, HashSet<CGModule>> modulesRecursiveInputs, CGModule moduleToAdd)
		{
			if (modulesRecursiveInputs.ContainsKey(moduleToAdd))
			{
				return modulesRecursiveInputs[moduleToAdd];
			}
			List<CGModule> source = moduleToAdd.Input.SelectMany((CGModuleInputSlot i) => i.GetLinkedModules()).ToList<CGModule>();
			HashSet<CGModule> hashSet = new HashSet<CGModule>();
			hashSet.Add(moduleToAdd);
			hashSet.UnionWith(source.SelectMany((CGModule i) => CurvyGenerator.UpdateModulesRecursiveInputs(modulesRecursiveInputs, i)));
			modulesRecursiveInputs[moduleToAdd] = hashSet;
			return hashSet;
		}

		[Tooltip("Show Debug Output?")]
		[SerializeField]
		private bool m_ShowDebug;

		[Tooltip("Whether to automatically refresh the generator's output when necessary")]
		[SerializeField]
		private bool m_AutoRefresh = true;

		[FieldCondition("m_AutoRefresh", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[Positive(Tooltip = "The minimum delay between two automatic generator's refreshing while in Play mode, in milliseconds")]
		[SerializeField]
		private int m_RefreshDelay;

		[FieldCondition("m_AutoRefresh", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[Positive(Tooltip = "The minimum delay between two automatic generator's refreshing while in Edit mode, in milliseconds")]
		[SerializeField]
		private int m_RefreshDelayEditor = 10;

		[Section("Events", false, false, 1000, HelpURL = "https://curvyeditor.com/doclink/generator_events")]
		[SerializeField]
		private CurvyCGEvent m_OnRefresh = new CurvyCGEvent();

		[HideInInspector]
		public List<CGModule> Modules = new List<CGModule>();

		[SerializeField]
		[HideInInspector]
		internal int m_LastModuleID;

		public Dictionary<int, CGModule> ModulesByID = new Dictionary<int, CGModule>();

		private bool mInitialized;

		private bool mInitializedPhaseOne;

		private bool mNeedSort = true;

		private double mLastUpdateTime;

		private PoolManager mPoolManager;

		private const int ModulesReorderingDeltaX = 50;

		private const int ModulesReorderingDeltaY = 20;
	}
}
