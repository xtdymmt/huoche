// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.SimplePool`1 where T
using System;
using System.Collections.Generic;

namespace FluffyUnderware.DevTools
{
	internal class SimplePool<T> where T : new()
	{
		public SimplePool(int preCreatedElementsCount)
		{
			this.freeItemsBackfield = new List<T>();
			for (int i = 0; i < preCreatedElementsCount; i++)
			{
				this.freeItemsBackfield.Add(Activator.CreateInstance<T>());
			}
		}

		public T GetItem()
		{
			T result;
			if (this.freeItemsBackfield.Count == 0)
			{
				result = Activator.CreateInstance<T>();
			}
			else
			{
				int index = this.freeItemsBackfield.Count - 1;
				result = this.freeItemsBackfield[index];
				this.freeItemsBackfield.RemoveAt(index);
			}
			return result;
		}

		public void ReleaseItem(T item)
		{
			this.freeItemsBackfield.Add(item);
		}

		private readonly List<T> freeItemsBackfield;
	}
}
