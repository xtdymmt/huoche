// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.DTSingleton`1
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public class DTSingleton<T> : MonoBehaviour, IDTSingleton where T : MonoBehaviour, IDTSingleton
	{
		public static bool HasInstance
		{
			get
			{
				return DTSingleton<T>._instance != null;
			}
		}

		public static T Instance
		{
			get
			{
				if (!Application.isPlaying)
				{
					DTSingleton<T>.applicationIsQuitting = false;
				}
				if (DTSingleton<T>.applicationIsQuitting)
				{
					return (T)((object)null);
				}
				if (DTSingleton<T>._instance == null)
				{
					object @lock = DTSingleton<T>._lock;
					lock (@lock)
					{
						if (DTSingleton<T>._instance == null)
						{
							UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(T));
							DTSingleton<T>._instance = ((array.Length < 1) ? new GameObject().AddComponent<T>() : ((T)((object)array[0])));
						}
					}
				}
				return DTSingleton<T>._instance;
			}
		}

		public virtual void Awake()
		{
			T instance = DTSingleton<T>.Instance;
			object @lock = DTSingleton<T>._lock;
			lock (@lock)
			{
				if (base.GetInstanceID() != instance.GetInstanceID())
				{
					instance.MergeDoubleLoaded(this);
					this.isDuplicateInstance = true;
					base.Invoke("DestroySelf", 0f);
				}
			}
		}

		protected virtual void OnDestroy()
		{
			object @lock = DTSingleton<T>._lock;
			lock (@lock)
			{
				if (Application.isPlaying && !this.isDuplicateInstance)
				{
					DTSingleton<T>.applicationIsQuitting = true;
					DTSingleton<T>._instance = (T)((object)null);
				}
			}
		}

		public virtual void MergeDoubleLoaded(IDTSingleton newInstance)
		{
		}

		private void DestroySelf()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private static T _instance;

		private static object _lock = new object();

		private static bool applicationIsQuitting = false;

		private bool isDuplicateInstance;
	}
}
