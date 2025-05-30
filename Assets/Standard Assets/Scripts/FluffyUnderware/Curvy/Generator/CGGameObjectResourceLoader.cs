// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGGameObjectResourceLoader
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[ResourceLoader("GameObject")]
	public class CGGameObjectResourceLoader : ICGResourceLoader
	{
		public Component Create(CGModule cgModule, string context)
		{
			GameObject gameObject = cgModule.Generator.PoolManager.GetPrefabPool(context, new GameObject[0]).Pop(null);
			return gameObject.transform;
		}

		public void Destroy(CGModule cgModule, Component obj, string context, bool kill)
		{
			if (obj != null)
			{
				if (kill)
				{
					if (Application.isPlaying)
					{
						UnityEngine.Object.Destroy(obj.gameObject);
					}
					else
					{
						UnityEngine.Object.DestroyImmediate(obj.gameObject);
					}
				}
				else
				{
					cgModule.Generator.PoolManager.GetPrefabPool(context, new GameObject[0]).Push(obj.gameObject);
				}
			}
		}
	}
}
