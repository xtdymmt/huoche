// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.PoolManager
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public class PoolManager : MonoBehaviour
	{
		public bool AutoCreatePools
		{
			get
			{
				return this.m_AutoCreatePools;
			}
			set
			{
				if (this.m_AutoCreatePools != value)
				{
					this.m_AutoCreatePools = value;
				}
			}
		}

		public PoolSettings DefaultSettings
		{
			get
			{
				return this.m_DefaultSettings;
			}
			set
			{
				if (this.m_DefaultSettings != value)
				{
					this.m_DefaultSettings = value;
				}
				if (this.m_DefaultSettings != null)
				{
					this.m_DefaultSettings.OnValidate();
				}
			}
		}

		public bool IsInitialized { get; private set; }

		public int Count
		{
			get
			{
				return this.Pools.Count + this.TypePools.Count;
			}
		}

		private void OnDisable()
		{
			this.IsInitialized = false;
		}

		private void Update()
		{
			if (!this.IsInitialized)
			{
				this.Initialize();
			}
			if (this.mPools.Length != this.TypePools.Count)
			{
				Array.Resize<IPool>(ref this.mPools, this.TypePools.Count);
				this.TypePools.Values.CopyTo(this.mPools, 0);
			}
			for (int i = 0; i < this.mPools.Length; i++)
			{
				this.mPools[i].Update();
			}
		}

		private void Initialize()
		{
			this.Pools.Clear();
			IPool[] components = base.GetComponents<IPool>();
			foreach (IPool pool in components)
			{
				if (pool is ComponentPool)
				{
					if (!this.Pools.ContainsKey(pool.Identifier))
					{
						this.Pools.Add(pool.Identifier, pool);
					}
					else
					{
						DTLog.Log("[DevTools] Found a duplicated ComponentPool for type " + pool.Identifier + ". The duplicated pool will be destroyed");
						UnityEngine.Object.Destroy(pool as ComponentPool);
					}
				}
				else
				{
					pool.Identifier = this.GetUniqueIdentifier(pool.Identifier);
					this.Pools.Add(pool.Identifier, pool);
				}
			}
			this.IsInitialized = true;
		}

		public string GetUniqueIdentifier(string ident)
		{
			int num = 0;
			string text = ident;
			while (this.Pools.ContainsKey(text))
			{
				int num2;
				num = (num2 = num + 1);
				text = ident + num2.ToString();
			}
			return text;
		}

		public Pool<T> GetTypePool<T>()
		{
			IPool pool = null;
			if (!this.TypePools.TryGetValue(typeof(T), out pool) && this.AutoCreatePools)
			{
				pool = this.CreateTypePool<T>(null);
			}
			return (Pool<T>)pool;
		}

		public ComponentPool GetComponentPool<T>() where T : Component
		{
			if (!this.IsInitialized)
			{
				this.Initialize();
			}
			IPool pool = null;
			if (!this.Pools.TryGetValue(typeof(T).AssemblyQualifiedName, out pool) && this.AutoCreatePools)
			{
				pool = this.CreateComponentPool<T>(null);
			}
			return (ComponentPool)pool;
		}

		public PrefabPool GetPrefabPool(string identifier, params GameObject[] prefabs)
		{
			if (!this.IsInitialized)
			{
				this.Initialize();
			}
			IPool pool;
			if (!this.Pools.TryGetValue(identifier, out pool) && this.AutoCreatePools)
			{
				pool = this.CreatePrefabPool(identifier, null, prefabs);
			}
			return (PrefabPool)pool;
		}

		public Pool<T> CreateTypePool<T>(PoolSettings settings = null)
		{
			PoolSettings settings2 = settings ?? new PoolSettings(this.DefaultSettings);
			IPool pool = null;
			if (!this.TypePools.TryGetValue(typeof(T), out pool))
			{
				pool = new Pool<T>(settings2);
				this.TypePools.Add(typeof(T), pool);
			}
			return (Pool<T>)pool;
		}

		public ComponentPool CreateComponentPool<T>(PoolSettings settings = null) where T : Component
		{
			if (!this.IsInitialized)
			{
				this.Initialize();
			}
			PoolSettings settings2 = settings ?? new PoolSettings(this.DefaultSettings);
			IPool pool = null;
			if (!this.Pools.TryGetValue(typeof(T).AssemblyQualifiedName, out pool))
			{
				pool = base.gameObject.AddComponent<ComponentPool>();
				((ComponentPool)pool).Initialize(typeof(T), settings2);
				this.Pools.Add(pool.Identifier, pool);
			}
			return (ComponentPool)pool;
		}

		public PrefabPool CreatePrefabPool(string name, PoolSettings settings = null, params GameObject[] prefabs)
		{
			if (!this.IsInitialized)
			{
				this.Initialize();
			}
			PoolSettings settings2 = settings ?? new PoolSettings(this.DefaultSettings);
			IPool pool = null;
			if (!this.Pools.TryGetValue(name, out pool))
			{
				PrefabPool prefabPool = base.gameObject.AddComponent<PrefabPool>();
				prefabPool.Initialize(name, settings2, prefabs);
				this.Pools.Add(name, prefabPool);
				return prefabPool;
			}
			return (PrefabPool)pool;
		}

		public List<IPool> FindPools(string identifierStartsWith)
		{
			List<IPool> list = new List<IPool>();
			foreach (KeyValuePair<string, IPool> keyValuePair in this.Pools)
			{
				if (keyValuePair.Key.StartsWith(identifierStartsWith))
				{
					list.Add(keyValuePair.Value);
				}
			}
			return list;
		}

		public void DeletePools(string startsWith)
		{
			List<IPool> list = this.FindPools(startsWith);
			for (int i = list.Count - 1; i >= 0; i--)
			{
				this.DeletePool(list[i]);
			}
		}

		public void DeletePool(IPool pool)
		{
			if (pool is PrefabPool || pool is ComponentPool)
			{
				UnityEngine.Object.Destroy((MonoBehaviour)pool);
				this.Pools.Remove(pool.Identifier);
			}
		}

		public void DeletePool<T>()
		{
			this.TypePools.Remove(typeof(T));
		}

		[Section("General", true, false, 100)]
		[SerializeField]
		private bool m_AutoCreatePools = true;

		[AsGroup(null, Expanded = false)]
		[SerializeField]
		private PoolSettings m_DefaultSettings = new PoolSettings();

		public Dictionary<string, IPool> Pools = new Dictionary<string, IPool>();

		public Dictionary<Type, IPool> TypePools = new Dictionary<Type, IPool>();

		private IPool[] mPools = new IPool[0];
	}
}
