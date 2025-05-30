// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGData
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public class CGData
	{
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		public static implicit operator bool(CGData a)
		{
			return !object.ReferenceEquals(a, null);
		}

		public virtual T Clone<T>() where T : CGData
		{
			return new CGData() as T;
		}

		protected int getGenericFIndex(ref float[] FMapArray, float fValue, out float frag)
		{
			if (fValue == 1f)
			{
				frag = 1f;
				return FMapArray.Length - 2;
			}
			fValue = Mathf.Repeat(fValue, 1f);
			for (int i = 1; i < FMapArray.Length; i++)
			{
				if (FMapArray[i] > fValue)
				{
					frag = (fValue - FMapArray[i - 1]) / (FMapArray[i] - FMapArray[i - 1]);
					return i - 1;
				}
			}
			frag = 0f;
			return 0;
		}

		public string Name;
	}
}
