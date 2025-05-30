// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGModule
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[ExecuteInEditMode]
	public class CGModule : DTVersionedMonoBehaviour
	{
		public CurvyCGEvent OnBeforeRefresh
		{
			get
			{
				return this.m_OnBeforeRefresh;
			}
			set
			{
				if (this.m_OnBeforeRefresh != value)
				{
					this.m_OnBeforeRefresh = value;
				}
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

		protected CurvyCGEventArgs OnBeforeRefreshEvent(CurvyCGEventArgs e)
		{
			if (this.OnBeforeRefresh != null)
			{
				this.OnBeforeRefresh.Invoke(e);
			}
			return e;
		}

		protected CurvyCGEventArgs OnRefreshEvent(CurvyCGEventArgs e)
		{
			if (this.OnRefresh != null)
			{
				this.OnRefresh.Invoke(e);
			}
			return e;
		}

		public string ModuleName
		{
			get
			{
				return base.name;
			}
			set
			{
				if (base.name != value)
				{
					base.name = value;
					this.renameManagedResourcesINTERNAL();
				}
			}
		}

		public bool Active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				if (this.m_Active != value)
				{
					this.m_Active = value;
					this.Dirty = true;
					this.Generator.sortModulesINTERNAL();
				}
			}
		}

		public int Seed
		{
			get
			{
				return this.m_Seed;
			}
			set
			{
				if (this.m_Seed != value)
				{
					this.m_Seed = value;
				}
				this.Dirty = true;
			}
		}

		public bool RandomizeSeed
		{
			get
			{
				return this.m_RandomizeSeed;
			}
			set
			{
				if (this.m_RandomizeSeed != value)
				{
					this.m_RandomizeSeed = value;
				}
			}
		}

		public CurvyGenerator Generator
		{
			get
			{
				return this.mGenerator;
			}
		}

		public int UniqueID
		{
			get
			{
				return this.m_UniqueID;
			}
		}

		public bool CircularReferenceError { get; set; }

		public Dictionary<string, CGModuleInputSlot> InputByName { get; private set; }

		public Dictionary<string, CGModuleOutputSlot> OutputByName { get; private set; }

		public List<CGModuleInputSlot> Input { get; private set; }

		public List<CGModuleOutputSlot> Output { get; private set; }

		public ModuleInfoAttribute Info
		{
			get
			{
				if (this.mInfo == null)
				{
					this.mInfo = this.getInfo();
				}
				return this.mInfo;
			}
		}

		public bool Dirty
		{
			get
			{
				return this.mDirty;
			}
			set
			{
				if (this.mDirty != value)
				{
					this.mDirty = value;
				}
				if (this.mDirty)
				{
					this.setTreeDirtyState();
				}
				if (this is IOnRequestProcessing || this is INoProcessing)
				{
					this.mDirty = false;
					if (this.Output != null)
					{
						for (int i = 0; i < this.Output.Count; i++)
						{
							this.Output[i].LastRequestParameters = null;
						}
					}
				}
			}
		}

		protected virtual void Awake()
		{
			this.mGenerator = base.GetComponentInParent<CurvyGenerator>();
		}

		protected virtual void OnEnable()
		{
			if (this.mGenerator)
			{
				this.Initialize();
				this.Generator.sortModulesINTERNAL();
			}
		}

		public void Initialize()
		{
			if (!this.mGenerator)
			{
				this.mGenerator = base.GetComponentInParent<CurvyGenerator>();
			}
			if (!this.mGenerator)
			{
				base.Invoke("Delete", 0f);
			}
			else
			{
				this.mInfo = this.getInfo();
				base.CheckForVersionUpgrade();
				if (string.IsNullOrEmpty(this.ModuleName))
				{
					if (string.IsNullOrEmpty(this.Info.ModuleName))
					{
						this.ModuleName = this.Generator.getUniqueModuleNameINTERNAL(this.Info.MenuName.Substring(this.Info.MenuName.LastIndexOf("/", StringComparison.Ordinal) + 1));
					}
					else
					{
						this.ModuleName = this.Generator.getUniqueModuleNameINTERNAL(this.Info.ModuleName);
					}
				}
				this.loadSlots();
				this.mInitialized = true;
			}
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnDestroy()
		{
			bool flag = true;
			this.setTreeDirtyStateChange();
			List<Component> list;
			List<string> list2;
			if (flag && this.GetManagedResources(out list, out list2))
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					this.DeleteManagedResource(list2[i], list[i], string.Empty, true);
				}
			}
			List<CGModuleInputSlot> inputSlots = this.GetInputSlots(null);
			List<CGModuleOutputSlot> outputSlots = this.GetOutputSlots(null);
			foreach (CGModuleInputSlot cgmoduleInputSlot in inputSlots)
			{
				cgmoduleInputSlot.ReInitializeLinkedTargetModules();
			}
			foreach (CGModuleOutputSlot cgmoduleOutputSlot in outputSlots)
			{
				cgmoduleOutputSlot.ReInitializeLinkedTargetModules();
			}
			if (this.Generator)
			{
				this.Generator.ModulesByID.Remove(this.UniqueID);
				this.Generator.Modules.Remove(this);
				this.Generator.sortModulesINTERNAL();
			}
			this.mInitialized = false;
		}

		private void OnDidApplyAnimationProperties()
		{
			this.Dirty = true;
		}

		public virtual bool IsConfigured
		{
			get
			{
				if (!this.IsInitialized || this.CircularReferenceError || !this.Active)
				{
					this.mIsConfiguredInternal = false;
					return false;
				}
				int num = 0;
				for (int i = 0; i < this.Input.Count; i++)
				{
					InputSlotInfo inputInfo = this.Input[i].InputInfo;
					if (this.Input[i].IsLinked)
					{
						for (int j = 0; j < this.Input[i].Count; j++)
						{
							if (this.Input[i].SourceSlot(j) != null)
							{
								if (this.Input[i].SourceSlot(j).Module.IsConfigured)
								{
									num++;
								}
								else if (!inputInfo.Optional)
								{
									this.mIsConfiguredInternal = false;
									return false;
								}
							}
						}
					}
					else if (inputInfo == null || !inputInfo.Optional)
					{
						this.mIsConfiguredInternal = false;
						return false;
					}
				}
				this.mIsConfiguredInternal = (num > 0 || this.Input.Count == 0);
				return this.mIsConfiguredInternal;
			}
		}

		public virtual bool IsInitialized
		{
			get
			{
				return this.mInitialized;
			}
		}

		public virtual void Refresh()
		{
			this.UIMessages.Clear();
		}

		public virtual void Reset()
		{
			this.ModuleName = ((!string.IsNullOrEmpty(this.Info.ModuleName)) ? this.Info.ModuleName : base.GetType().Name);
		}

		public void ReInitializeLinkedSlots()
		{
			List<CGModuleInputSlot> inputSlots = this.GetInputSlots(null);
			List<CGModuleOutputSlot> outputSlots = this.GetOutputSlots(null);
			for (int i = 0; i < inputSlots.Count; i++)
			{
				inputSlots[i].ReInitializeLinkedSlots();
			}
			for (int j = 0; j < outputSlots.Count; j++)
			{
				outputSlots[j].ReInitializeLinkedSlots();
			}
		}

		public virtual void OnStateChange()
		{
			this.Dirty = true;
			if (this.Output != null)
			{
				for (int i = 0; i < this.Output.Count; i++)
				{
					this.Output[i].ClearData();
				}
			}
		}

		public virtual void OnTemplateCreated()
		{
		}

		protected static T GetRequestParameter<T>(ref CGDataRequestParameter[] requests) where T : CGDataRequestParameter
		{
			for (int i = 0; i < requests.Length; i++)
			{
				if (requests[i] is T)
				{
					return (T)((object)requests[i]);
				}
			}
			return (T)((object)null);
		}

		protected static void RemoveRequestParameter(ref CGDataRequestParameter[] requests, CGDataRequestParameter request)
		{
			for (int i = 0; i < requests.Length; i++)
			{
				if (requests[i] == request)
				{
					requests = requests.RemoveAt(i);
					return;
				}
			}
		}

		public CGModuleLink GetOutputLink(CGModuleOutputSlot outSlot, CGModuleInputSlot inSlot)
		{
			return CGModule.GetLink(this.OutputLinks, outSlot, inSlot);
		}

		public List<CGModuleLink> GetOutputLinks(CGModuleOutputSlot outSlot)
		{
			return CGModule.GetLinks(this.OutputLinks, outSlot);
		}

		public CGModuleLink GetInputLink(CGModuleInputSlot inSlot, CGModuleOutputSlot outSlot)
		{
			return CGModule.GetLink(this.InputLinks, inSlot, outSlot);
		}

		public List<CGModuleLink> GetInputLinks(CGModuleInputSlot inSlot)
		{
			return CGModule.GetLinks(this.InputLinks, inSlot);
		}

		private static CGModuleLink GetLink(List<CGModuleLink> lst, CGModuleSlot source, CGModuleSlot target)
		{
			for (int i = 0; i < lst.Count; i++)
			{
				if (lst[i].IsSame(source, target))
				{
					return lst[i];
				}
			}
			return null;
		}

		private static List<CGModuleLink> GetLinks(List<CGModuleLink> lst, CGModuleSlot source)
		{
			List<CGModuleLink> list = new List<CGModuleLink>();
			for (int i = 0; i < lst.Count; i++)
			{
				if (lst[i].IsFrom(source))
				{
					list.Add(lst[i]);
				}
			}
			return list;
		}

		public CGModule CopyTo(CurvyGenerator targetGenerator)
		{
            CGModule cGModule = this.DuplicateGameObject<CGModule>(targetGenerator.transform);
            cGModule.mGenerator = targetGenerator;
            cGModule.Initialize();
            cGModule.ModuleName = ModuleName;
            cGModule.ModuleName = targetGenerator.getUniqueModuleNameINTERNAL(cGModule.ModuleName);
            cGModule.SetUniqueIdINTERNAL();
            cGModule.renameManagedResourcesINTERNAL();
            return cGModule;
        }

		public Component AddManagedResource(string resourceName, string context = "", int index = -1)
		{
			Component component = CGResourceHandler.CreateResource(this, resourceName, context);
			this.RenameResource(resourceName + context, component, index);
			component.transform.SetParent(base.transform);
			return component;
		}

		public void DeleteManagedResource(string resourceName, Component res, string context = "", bool dontUsePool = false)
		{
			if (res)
			{
				CGResourceHandler.DestroyResource(this, resourceName, res, context, dontUsePool);
			}
		}

		public bool IsManagedResource(Component res)
		{
			return res && res.transform.parent == base.transform;
		}

		protected void RenameResource(string resourceName, Component resource, int index = -1)
		{
			resource.name = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", new object[]
			{
				this.ModuleName,
				this.UniqueID,
				resourceName
			});
			if (index > -1)
			{
				resource.name += string.Format(CultureInfo.InvariantCulture, "{0:000}", new object[]
				{
					index
				});
			}
		}

		protected PrefabPool GetPrefabPool(GameObject prefab)
		{
			return this.Generator.PoolManager.GetPrefabPool(this.UniqueID.ToString(CultureInfo.InvariantCulture) + "_" + prefab.name, new GameObject[]
			{
				prefab
			});
		}

		public List<IPool> GetAllPrefabPools()
		{
			return this.Generator.PoolManager.FindPools(this.UniqueID.ToString(CultureInfo.InvariantCulture) + "_");
		}

		public void DeleteAllPrefabPools()
		{
			this.Generator.PoolManager.DeletePools(this.UniqueID.ToString(CultureInfo.InvariantCulture) + "_");
		}

		public void Delete()
		{
			this.OnStateChange();
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		public CGModuleInputSlot GetInputSlot(string name)
		{
			return (this.InputByName == null || !this.InputByName.ContainsKey(name)) ? null : this.InputByName[name];
		}

		public List<CGModuleInputSlot> GetInputSlots(Type filterType = null)
		{
			if (filterType == null)
			{
				return new List<CGModuleInputSlot>(this.Input);
			}
			List<CGModuleInputSlot> list = new List<CGModuleInputSlot>();
			for (int i = 0; i < this.Output.Count; i++)
			{
				if (this.Output[i].Info.DataTypes[0] == filterType || this.Output[i].Info.DataTypes[0].IsSubclassOf(filterType))
				{
					list.Add(this.Input[i]);
				}
			}
			return list;
		}

		public CGModuleOutputSlot GetOutputSlot(string name)
		{
			return (this.OutputByName == null || !this.OutputByName.ContainsKey(name)) ? null : this.OutputByName[name];
		}

		public List<CGModuleOutputSlot> GetOutputSlots(Type filterType = null)
		{
			if (filterType == null)
			{
				return new List<CGModuleOutputSlot>(this.Output);
			}
			List<CGModuleOutputSlot> list = new List<CGModuleOutputSlot>();
			for (int i = 0; i < this.Output.Count; i++)
			{
				if (this.Output[i].Info.DataTypes[0] == filterType || this.Output[i].Info.DataTypes[0].IsSubclassOf(filterType))
				{
					list.Add(this.Output[i]);
				}
			}
			return list;
		}

		public bool GetManagedResources(out List<Component> components, out List<string> componentNames)
		{
			components = new List<Component>();
			componentNames = new List<string>();
			FieldInfo[] allFields = base.GetType().GetAllFields(false, true);
			foreach (FieldInfo fieldInfo in allFields)
			{
				CGResourceManagerAttribute customAttribute = fieldInfo.GetCustomAttribute<CGResourceManagerAttribute>();
				if (customAttribute != null)
				{
					if (typeof(ICGResourceCollection).IsAssignableFrom(fieldInfo.FieldType))
					{
						ICGResourceCollection icgresourceCollection = fieldInfo.GetValue(this) as ICGResourceCollection;
						if (icgresourceCollection != null)
						{
							Component[] itemsArray = icgresourceCollection.ItemsArray;
							foreach (Component component in itemsArray)
							{
								if (component.transform.parent == base.transform)
								{
									components.Add(component);
									componentNames.Add(customAttribute.ResourceName);
								}
							}
						}
					}
					else
					{
						Component component2 = fieldInfo.GetValue(this) as Component;
						if (component2 && component2.transform.parent == base.transform)
						{
							components.Add(component2);
							componentNames.Add(customAttribute.ResourceName);
						}
					}
				}
			}
			return components.Count > 0;
		}

		public int SetUniqueIdINTERNAL()
		{
			this.m_UniqueID = ++this.Generator.m_LastModuleID;
			return this.m_UniqueID;
		}

		internal void initializeSort()
		{
			this.SortAncestors = 0;
			this.CircularReferenceError = false;
			for (int i = 0; i < this.Input.Count; i++)
			{
				if (this.Input[i].IsLinked)
				{
					this.SortAncestors++;
				}
			}
		}

		internal List<CGModule> decrementChilds()
		{
			List<CGModule> list = new List<CGModule>();
			for (int i = 0; i < this.Output.Count; i++)
			{
				for (int j = 0; j < this.Output[i].LinkedSlots.Count; j++)
				{
					if (--this.Output[i].LinkedSlots[j].Module.SortAncestors == 0)
					{
						list.Add(this.Output[i].LinkedSlots[j].Module);
					}
				}
			}
			return list;
		}

		internal void doRefresh()
		{
			if (this.RandomizeSeed)
			{
				CGModule.setSeed((int)DateTime.Now.Ticks);
			}
			else
			{
				CGModule.setSeed(this.Seed);
			}
			this.OnBeforeRefreshEvent(new CurvyCGEventArgs(this));
			this.Refresh();
			CGModule.setSeed((int)DateTime.Now.Ticks);
			this.OnRefreshEvent(new CurvyCGEventArgs(this));
			this.mDirty = false;
		}

		private static void setSeed(int seed)
		{
			UnityEngine.Random.InitState(seed);
		}

		internal ModuleInfoAttribute getInfo()
		{
			object[] customAttributes = base.GetType().GetCustomAttributes(typeof(ModuleInfoAttribute), true);
			return (customAttributes.Length <= 0) ? null : ((ModuleInfoAttribute)customAttributes[0]);
		}

		private bool usesRandom()
		{
			return this.Info != null & this.Info.UsesRandom;
		}

		private void loadSlots()
		{
			this.InputByName = new Dictionary<string, CGModuleInputSlot>();
			this.OutputByName = new Dictionary<string, CGModuleOutputSlot>();
			this.Input = new List<CGModuleInputSlot>();
			this.Output = new List<CGModuleOutputSlot>();
			FieldInfo[] allFields = base.GetType().GetAllFields(false, false);
			foreach (FieldInfo fieldInfo in allFields)
			{
				if (fieldInfo.FieldType == typeof(CGModuleInputSlot))
				{
					CGModuleInputSlot cgmoduleInputSlot = (CGModuleInputSlot)fieldInfo.GetValue(this);
					cgmoduleInputSlot.Module = this;
					cgmoduleInputSlot.Info = this.getSlotInfo(fieldInfo);
					cgmoduleInputSlot.ReInitializeLinkedSlots();
					this.InputByName.Add(cgmoduleInputSlot.Info.Name, cgmoduleInputSlot);
					this.Input.Add(cgmoduleInputSlot);
				}
				else if (fieldInfo.FieldType == typeof(CGModuleOutputSlot))
				{
					CGModuleOutputSlot cgmoduleOutputSlot = (CGModuleOutputSlot)fieldInfo.GetValue(this);
					cgmoduleOutputSlot.Module = this;
					cgmoduleOutputSlot.Info = this.getSlotInfo(fieldInfo);
					cgmoduleOutputSlot.ReInitializeLinkedSlots();
					this.OutputByName.Add(cgmoduleOutputSlot.Info.Name, cgmoduleOutputSlot);
					this.Output.Add(cgmoduleOutputSlot);
				}
			}
		}

		private SlotInfo getSlotInfo(FieldInfo f)
		{
			SlotInfo customAttribute = f.GetCustomAttribute<SlotInfo>();
			if (customAttribute != null)
			{
				if (string.IsNullOrEmpty(customAttribute.Name))
				{
					customAttribute.Name = f.Name.TrimStart("In", StringComparison.CurrentCultureIgnoreCase).TrimStart("Out", StringComparison.CurrentCultureIgnoreCase);
				}
				for (int i = 0; i < customAttribute.DataTypes.Length; i++)
				{
					if (!customAttribute.DataTypes[i].IsSubclassOf(typeof(CGData)))
					{
						UnityEngine.Debug.LogError(string.Format(CultureInfo.InvariantCulture, "{0}, Slot '{1}': Data type needs to be subclass of CGData!", new object[]
						{
							base.GetType().Name,
							customAttribute.Name
						}));
					}
				}
				return customAttribute;
			}
			UnityEngine.Debug.LogError(string.Concat(new string[]
			{
				"The Slot '",
				f.Name,
				"' of type '",
				f.DeclaringType.Name,
				"' needs a SlotInfo attribute!"
			}));
			return null;
		}

		private void setTreeDirtyStateChange()
		{
			this.mStateChangeDirty = true;
			if (this.Output != null)
			{
				for (int i = 0; i < this.Output.Count; i++)
				{
					if (this.Output[i].IsLinked)
					{
						List<CGModule> linkedModules = this.Output[i].GetLinkedModules();
						for (int j = 0; j < linkedModules.Count; j++)
						{
							if (linkedModules[j] != this || linkedModules[j].CircularReferenceError)
							{
								linkedModules[j].setTreeDirtyStateChange();
							}
						}
					}
				}
			}
		}

		private void setTreeDirtyState()
		{
			bool isConfigured = this.IsConfigured;
			if (this.mLastIsConfiguredState != isConfigured)
			{
				this.mStateChangeDirty = true;
			}
			this.mLastIsConfiguredState = isConfigured;
			if (this.Output != null)
			{
				for (int i = 0; i < this.Output.Count; i++)
				{
					if (this.Output[i].IsLinked)
					{
						List<CGModule> linkedModules = this.Output[i].GetLinkedModules();
						for (int j = 0; j < linkedModules.Count; j++)
						{
							if (linkedModules[j] != this || linkedModules[j].CircularReferenceError)
							{
								linkedModules[j].Dirty = true;
							}
						}
					}
				}
			}
		}

		public void checkOnStateChangedINTERNAL()
		{
			if (this.mStateChangeDirty)
			{
				this.OnStateChange();
			}
			this.mStateChangeDirty = false;
		}

		public void renameManagedResourcesINTERNAL()
		{
			FieldInfo[] allFields = base.GetType().GetAllFields(false, true);
			foreach (FieldInfo fieldInfo in allFields)
			{
				CGResourceManagerAttribute customAttribute = fieldInfo.GetCustomAttribute<CGResourceManagerAttribute>();
				if (customAttribute != null)
				{
					Component component = fieldInfo.GetValue(this) as Component;
					if (component && component.transform.parent == base.transform)
					{
						this.RenameResource(customAttribute.ResourceName, component, -1);
					}
				}
			}
		}

		[Group("Events", Expanded = false, Sort = 1000)]
		[SerializeField]
		private CurvyCGEvent m_OnBeforeRefresh = new CurvyCGEvent();

		[Group("Events")]
		[SerializeField]
		private CurvyCGEvent m_OnRefresh = new CurvyCGEvent();

		[SerializeField]
		[HideInInspector]
		private string m_ModuleName;

		[SerializeField]
		[HideInInspector]
		private bool m_Active = true;

		[Group("Seed Options", Expanded = false, Sort = 1001)]
		[GroupCondition("usesRandom")]
		[FieldAction("CBSeedOptions", ActionAttribute.ActionEnum.Callback, ShowBelowProperty = true)]
		[SerializeField]
		private bool m_RandomizeSeed;

		[SerializeField]
		[HideInInspector]
		private int m_Seed = (int)DateTime.Now.Ticks;

		[NonSerialized]
		public List<string> UIMessages = new List<string>();

		private CurvyGenerator mGenerator;

		[SerializeField]
		[HideInInspector]
		private int m_UniqueID;

		internal int SortAncestors;

		[HideInInspector]
		public CGModuleProperties Properties = new CGModuleProperties();

		[HideInInspector]
		public List<CGModuleLink> InputLinks = new List<CGModuleLink>();

		[HideInInspector]
		public List<CGModuleLink> OutputLinks = new List<CGModuleLink>();

		private ModuleInfoAttribute mInfo;

		private bool mDirty = true;

		private bool mInitialized;

		private bool mIsConfiguredInternal;

		private bool mStateChangeDirty;

		private bool mLastIsConfiguredState;
	}
}
