// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.ICGResourceLoader
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public interface ICGResourceLoader
	{
		Component Create(CGModule cgModule, string context);

		void Destroy(CGModule cgModule, Component obj, string context, bool kill);
	}
}
