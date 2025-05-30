// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGMeshResourceCollection
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGMeshResourceCollection : ICGResourceCollection
	{
		public int Count
		{
			get
			{
				return this.Items.Count;
			}
		}

		public Component[] ItemsArray
		{
			get
			{
				return this.Items.ToArray();
			}
		}

		public List<CGMeshResource> Items = new List<CGMeshResource>();
	}
}
