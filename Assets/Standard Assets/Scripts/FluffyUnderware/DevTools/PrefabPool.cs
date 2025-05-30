// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.PrefabPool
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	[RequireComponent(typeof(PoolManager))]
	public class PrefabPool : MonoBehaviour, IPool
	{
		public string Identifier
		{
			get
			{
				return this.m_Identifier;
			}
			set
			{
				if (this.m_Identifier != value)
				{
					if (string.IsNullOrEmpty(this.m_Identifier))
					{
						string uniqueIdentifier = this.Manager.GetUniqueIdentifier(value);
					}
					this.m_Identifier = value;
				}
			}
		}

		public List<GameObject> Prefabs
		{
			get
			{
				return this.m_Prefabs;
			}
			set
			{
				if (this.m_Prefabs != value)
				{
					this.m_Prefabs = value;
				}
			}
		}

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

		private void Awake()
		{
		}

		private void Start()
		{
			if (this.Settings.Prewarm)
			{
				this.Reset();
			}
		}

		public void Initialize(string ident, PoolSettings settings, params GameObject[] prefabs)
		{
			this.Identifier = ident;
			this.m_Settings = settings;
			this.Prefabs = new List<GameObject>(prefabs);
			this.mLastTime = DTTime.TimeSinceStartup + (double)UnityEngine.Random.Range(0f, this.Settings.Speed);
			if (this.Settings.Prewarm)
			{
				this.Reset();
			}
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

		public int Count
		{
			get
			{
				return this.mObjects.Count;
			}
		}

		public GameObject Pop(Transform parent = null)
		{
			GameObject gameObject = null;
			if (this.Count > 0)
			{
				gameObject = this.mObjects[0];
				this.mObjects.RemoveAt(0);
			}
			else if (this.Settings.AutoCreate || !Application.isPlaying)
			{
				if (this.Settings.Debug)
				{
					this.log("Auto create item");
				}
				gameObject = this.create();
			}
			if (gameObject)
			{
				gameObject.gameObject.hideFlags = HideFlags.None;
				gameObject.transform.parent = parent;
				if (this.Settings.AutoEnableDisable)
				{
					gameObject.SetActive(true);
				}
				this.sendAfterPop(gameObject);
				if (this.Settings.Debug)
				{
					this.log("Pop " + gameObject);
				}
			}
			return gameObject;
		}

		public virtual void Push(GameObject item)
		{
			if (this.Settings.Debug)
			{
				this.log("Push " + item);
			}
			if (item != null)
			{
				this.sendBeforePush(item);
				this.mObjects.Add(item);
				item.transform.parent = base.transform;
				item.gameObject.hideFlags = ((!this.Settings.Debug) ? HideFlags.HideAndDontSave : HideFlags.DontSave);
				if (this.Settings.AutoEnableDisable)
				{
					item.SetActive(false);
				}
			}
		}

		private GameObject create()
		{
			GameObject gameObject = null;
			if (this.Prefabs.Count > 0)
			{
				GameObject gameObject2 = this.Prefabs[UnityEngine.Random.Range(0, this.Prefabs.Count)];
				gameObject = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
				gameObject.name = gameObject2.name;
				gameObject.transform.parent = base.transform;
				if (this.Settings.AutoEnableDisable)
				{
					gameObject.SetActive(false);
				}
			}
			return gameObject;
		}

		private void destroy(GameObject go)
		{
			UnityEngine.Object.Destroy(go);
		}

		private void log(string msg)
		{
			UnityEngine.Debug.Log(string.Format("[{0}] ({1} items) {2}", this.Identifier, this.Count, msg));
		}

		private void setParent(Transform item, Transform parent)
		{
			if (item != null)
			{
				item.parent = parent;
			}
		}

		private void sendAfterPop(GameObject item)
		{
			item.SendMessage("OnAfterPop", SendMessageOptions.DontRequireReceiver);
		}

		private void sendBeforePush(GameObject item)
		{
			item.SendMessage("OnBeforePush", SendMessageOptions.DontRequireReceiver);
		}

		[FieldCondition("m_Identifier", "", false, ActionAttribute.ActionEnum.ShowWarning, "Please enter an identifier! (Select a prefab to set automatically)", ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private string m_Identifier;

		[SerializeField]
		private List<GameObject> m_Prefabs = new List<GameObject>();

		[Inline]
		[SerializeField]
		private PoolSettings m_Settings;

		private PoolManager mManager;

		private List<GameObject> mObjects = new List<GameObject>();

		private double mLastTime;

		private double mDeltaTime;
	}
}
