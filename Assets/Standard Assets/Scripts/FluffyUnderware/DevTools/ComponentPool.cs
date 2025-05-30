// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.ComponentPool
using System;
using System.Collections.Generic;
using System.Linq;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FluffyUnderware.DevTools
{
	public class ComponentPool : MonoBehaviour, IPool, ISerializationCallbackReceiver
	{
		public PoolSettings Settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				if (this.m_Settings != value)
				{
					this.m_Settings = value;
				}
				if (this.m_Settings != null)
				{
					this.m_Settings.OnValidate();
				}
			}
		}

		public PoolManager Manager
		{
			get
			{
				if (this.mManager == null)
				{
					this.mManager = base.GetComponent<PoolManager>();
				}
				return this.mManager;
			}
		}

		public string Identifier
		{
			get
			{
				return this.m_Identifier;
			}
			set
			{
				throw new InvalidOperationException("Component pool's identifier should always indicate the pooled type's assembly qualified name");
			}
		}

		public Type Type
		{
			get
			{
				Type type = Type.GetType(this.Identifier);
				if (type == null)
				{
					DTLog.LogWarning("[DevTools] ComponentPool's Type is an unknown type " + this.m_Identifier);
				}
				return type;
			}
		}

		public int Count
		{
			get
			{
				return this.mObjects.Count;
			}
		}

		public void Initialize(Type type, PoolSettings settings)
		{
			this.m_Identifier = type.AssemblyQualifiedName;
			this.m_Settings = settings;
			this.mLastTime = DTTime.TimeSinceStartup + (double)UnityEngine.Random.Range(0f, this.Settings.Speed);
			if (this.Settings.Prewarm)
			{
				this.Reset();
			}
		}

		private void Start()
		{
			if (this.Settings.Prewarm)
			{
				this.Reset();
			}
		}

		private void OnEnable()
		{
			SceneManager.sceneLoaded += this.OnSceneLoaded;
		}

		private void OnDisable()
		{
		}

		public void Update()
		{
			if (Application.isPlaying)
			{
				this.mDeltaTime += DTTime.TimeSinceStartup - this.mLastTime;
				this.mLastTime = DTTime.TimeSinceStartup;
				if (this.Settings.Speed > 0f)
				{
					int num = (int)(this.mDeltaTime / (double)this.Settings.Speed);
					this.mDeltaTime -= (double)num;
					if (this.Count > this.Settings.Threshold)
					{
						num = Mathf.Min(num, this.Count - this.Settings.Threshold);
						while (num-- > 0)
						{
							if (this.Settings.Debug)
							{
								this.log("Threshold exceeded: Deleting item");
							}
							this.destroy(this.mObjects[0]);
							this.mObjects.RemoveAt(0);
						}
					}
					else if (this.Count < this.Settings.MinItems)
					{
						num = Mathf.Min(num, this.Settings.MinItems - this.Count);
						while (num-- > 0)
						{
							if (this.Settings.Debug)
							{
								this.log("Below MinItems: Adding item");
							}
							this.mObjects.Add(this.create());
						}
					}
				}
				else
				{
					this.mDeltaTime = 0.0;
				}
			}
		}

		public void Reset()
		{
			if (Application.isPlaying)
			{
				while (this.Count < this.Settings.MinItems)
				{
					this.mObjects.Add(this.create());
				}
				while (this.Count > this.Settings.Threshold)
				{
					this.destroy(this.mObjects[0]);
					this.mObjects.RemoveAt(0);
				}
				if (this.Settings.Debug)
				{
					this.log("Prewarm/Reset");
				}
			}
		}

		public void OnSceneLoaded(Scene scn, LoadSceneMode mode)
		{
			for (int i = this.mObjects.Count - 1; i >= 0; i--)
			{
				if (this.mObjects[i] == null)
				{
					this.mObjects.RemoveAt(i);
				}
			}
		}

		public void Clear()
		{
			if (this.Settings.Debug)
			{
				this.log("Clear");
			}
			for (int i = 0; i < this.Count; i++)
			{
				this.destroy(this.mObjects[i]);
			}
			this.mObjects.Clear();
		}

		public void Push(Component item)
		{
			this.sendBeforePush(item);
			if (item != null)
			{
				this.mObjects.Add(item);
				item.transform.parent = this.Manager.transform;
				item.gameObject.hideFlags = ((!this.Settings.Debug) ? HideFlags.HideAndDontSave : HideFlags.DontSave);
				if (this.Settings.AutoEnableDisable)
				{
					item.gameObject.SetActive(false);
				}
			}
		}

		public Component Pop(Transform parent = null)
		{
			Component component = null;
			if (this.Count > 0)
			{
				component = this.mObjects[0];
				this.mObjects.RemoveAt(0);
			}
			else if (this.Settings.AutoCreate || !Application.isPlaying)
			{
				if (this.Settings.Debug)
				{
					this.log("Auto create item");
				}
				component = this.create();
			}
			if (component)
			{
				component.gameObject.hideFlags = HideFlags.None;
				component.transform.parent = parent;
				if (this.Settings.AutoEnableDisable)
				{
					component.gameObject.SetActive(true);
				}
				this.sendAfterPop(component);
				if (this.Settings.Debug)
				{
					this.log("Pop " + component);
				}
			}
			return component;
		}

		public T Pop<T>(Transform parent) where T : Component
		{
			return this.Pop(parent) as T;
		}

		private Component create()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = this.Identifier;
			gameObject.transform.parent = this.Manager.transform;
			if (this.Settings.AutoEnableDisable)
			{
				gameObject.SetActive(false);
			}
			return gameObject.AddComponent(this.Type);
		}

		private void destroy(Component item)
		{
			if (item != null)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
		}

		private void setParent(Component item, Transform parent)
		{
			if (item != null)
			{
				item.transform.parent = parent;
			}
		}

		private void sendAfterPop(Component item)
		{
			GameObject gameObject = item.gameObject;
			if (gameObject.activeSelf && gameObject.activeInHierarchy)
			{
				gameObject.SendMessage("OnAfterPop", SendMessageOptions.DontRequireReceiver);
			}
			else if (item is IPoolable)
			{
				((IPoolable)item).OnAfterPop();
			}
			else
			{
				DTLog.LogWarning("[Curvy] sendAfterPop could not send message because the receiver " + item.name + " is not active");
			}
		}

		private void sendBeforePush(Component item)
		{
			GameObject gameObject = item.gameObject;
			if (gameObject.activeSelf && gameObject.activeInHierarchy)
			{
				gameObject.SendMessage("OnBeforePush", SendMessageOptions.DontRequireReceiver);
			}
			else if (item is IPoolable)
			{
				((IPoolable)item).OnBeforePush();
			}
			else
			{
				DTLog.LogWarning("[Curvy] sendBeforePush could not send message because the receiver " + item.name + " is not active");
			}
		}

		private void log(string msg)
		{
			UnityEngine.Debug.Log(string.Format("[{0}] ({1} items) {2}", this.Identifier, this.Count, msg));
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			if (Type.GetType(this.m_Identifier) == null)
			{
				string[] array = this.m_Identifier.Split(new char[]
				{
					','
				});
				if (array.Length >= 5)
				{
					string typeName = string.Join(",", array.SubArray(0, array.Length - 4));
					Type[] loadedTypes = TypeExt.GetLoadedTypes();
					Type type = loadedTypes.FirstOrDefault((Type t) => t.FullName == typeName);
					if (type != null)
					{
						this.m_Identifier = type.AssemblyQualifiedName;
					}
				}
			}
		}

		[SerializeField]
		[HideInInspector]
		private string m_Identifier;

		[Inline]
		[SerializeField]
		private PoolSettings m_Settings;

		private PoolManager mManager;

		private List<Component> mObjects = new List<Component>();

		private double mLastTime;

		private double mDeltaTime;
	}
}
