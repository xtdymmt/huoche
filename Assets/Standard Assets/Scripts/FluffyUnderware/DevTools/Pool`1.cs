// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.Pool`1
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public class Pool<T> : IPool
	{
		public Pool(PoolSettings settings = null)
		{
			this.Settings = (settings ?? new PoolSettings());
			this.Identifier = typeof(T).FullName;
			this.mLastTime = DTTime.TimeSinceStartup + (double)UnityEngine.Random.Range(0f, this.Settings.Speed);
			if (this.Settings.Prewarm)
			{
				this.Reset();
			}
		}

		public string Identifier { get; set; }

		public PoolSettings Settings { get; protected set; }

		public Type Type
		{
			get
			{
				return typeof(T);
			}
		}

		public void Update()
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
						this.destroy(this.mObjects[0]);
						this.mObjects.RemoveAt(0);
						this.log("Threshold exceeded: Deleting item");
					}
				}
				else if (this.Count < this.Settings.MinItems)
				{
					num = Mathf.Min(num, this.Settings.MinItems - this.Count);
					while (num-- > 0)
					{
						this.mObjects.Add(this.create());
						this.log("Below MinItems: Adding item");
					}
				}
			}
			else
			{
				this.mDeltaTime = 0.0;
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
				this.log("Prewarm/Reset");
			}
		}

		public void Clear()
		{
			this.log("Clear");
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

		public virtual T Pop(Transform parent = null)
		{
			T t = default(T);
			if (this.Count > 0)
			{
				t = this.mObjects[0];
				this.mObjects.RemoveAt(0);
			}
			else if (this.Settings.AutoCreate || !Application.isPlaying)
			{
				this.log("Auto create item");
				t = this.create();
			}
			if (t != null)
			{
				this.sendAfterPop(t);
				this.setParent(t, parent);
				this.log("Pop " + t);
			}
			return t;
		}

		public virtual void Push(T item)
		{
			this.log("Push " + item);
			if (Application.isPlaying && item != null)
			{
				this.sendBeforePush(item);
				this.mObjects.Add(item);
			}
		}

		protected virtual void sendBeforePush(T item)
		{
			if (item is IPoolable)
			{
				((IPoolable)((object)item)).OnBeforePush();
			}
		}

		protected virtual void sendAfterPop(T item)
		{
			if (item is IPoolable)
			{
				((IPoolable)((object)item)).OnAfterPop();
			}
		}

		protected virtual void setParent(T item, Transform parent)
		{
		}

		protected virtual T create()
		{
			return Activator.CreateInstance<T>();
		}

		protected virtual void destroy(T item)
		{
		}

		private void log(string msg)
		{
			if (this.Settings.Debug)
			{
				UnityEngine.Debug.Log(string.Format("[{0}] ({1} items) {2}", this.Identifier, this.Count, msg));
			}
		}

		private List<T> mObjects = new List<T>();

		private double mLastTime;

		private double mDeltaTime;
	}
}
