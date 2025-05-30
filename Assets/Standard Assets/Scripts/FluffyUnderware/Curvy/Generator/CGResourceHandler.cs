// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGResourceHandler
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public static class CGResourceHandler
	{
		public static Component CreateResource(CGModule module, string resName, string context)
		{
			if (CGResourceHandler.Loader.Count == 0)
			{
				CGResourceHandler.getLoaders();
			}
			if (CGResourceHandler.Loader.ContainsKey(resName))
			{
				ICGResourceLoader icgresourceLoader = CGResourceHandler.Loader[resName];
				return icgresourceLoader.Create(module, context);
			}
			UnityEngine.Debug.LogError("CGResourceHandler: Missing Loader for resource '" + resName + "'");
			return null;
		}

		public static void DestroyResource(CGModule module, string resName, Component obj, string context, bool kill)
		{
			if (CGResourceHandler.Loader.Count == 0)
			{
				CGResourceHandler.getLoaders();
			}
			if (CGResourceHandler.Loader.ContainsKey(resName))
			{
				ICGResourceLoader icgresourceLoader = CGResourceHandler.Loader[resName];
				icgresourceLoader.Destroy(module, obj, context, kill);
			}
			else
			{
				UnityEngine.Debug.LogError("CGResourceHandler: Missing Loader for resource '" + resName + "'");
			}
		}

		private static void getLoaders()
		{
			Type[] loadedTypes = TypeExt.GetLoadedTypes();
			foreach (Type type in loadedTypes)
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(ResourceLoaderAttribute), true);
				if (customAttributes.Length > 0)
				{
					ICGResourceLoader icgresourceLoader = (ICGResourceLoader)Activator.CreateInstance(type);
					if (icgresourceLoader != null)
					{
						CGResourceHandler.Loader.Add(((ResourceLoaderAttribute)customAttributes[0]).ResourceName, icgresourceLoader);
					}
				}
			}
		}

		private static Dictionary<string, ICGResourceLoader> Loader = new Dictionary<string, ICGResourceLoader>();
	}
}
