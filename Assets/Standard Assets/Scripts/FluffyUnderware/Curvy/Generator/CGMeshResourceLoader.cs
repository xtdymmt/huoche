// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGMeshResourceLoader
using System;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[ResourceLoader("Mesh")]
	public class CGMeshResourceLoader : ICGResourceLoader
	{
		public Component Create(CGModule cgModule, string context)
		{
			return cgModule.Generator.PoolManager.GetComponentPool<CGMeshResource>().Pop(null);
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
					obj.StripComponents(new Type[]
					{
						typeof(CGMeshResource),
						typeof(MeshFilter),
						typeof(MeshRenderer)
					});
					cgModule.Generator.PoolManager.GetComponentPool<CGMeshResource>().Push(obj);
				}
			}
		}
	}
}
