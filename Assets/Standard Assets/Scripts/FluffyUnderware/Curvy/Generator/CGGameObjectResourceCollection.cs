// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGGameObjectResourceCollection
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGGameObjectResourceCollection : ICGResourceCollection
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

		public List<Transform> Items = new List<Transform>();

		public List<string> PoolNames = new List<string>();
	}
}
