// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.Ring`1
using System;
using System.Collections;
using System.Collections.Generic;

namespace FluffyUnderware.DevTools
{
	public class Ring<T> : IList<T>, IEnumerable, ICollection<T>, IEnumerable<T>
	{
		public Ring(int size)
		{
			this.mList = new List<T>(size);
			this.Size = size;
		}

		public int Size { get; private set; }

		public void Add(T item)
		{
			if (this.mList.Count == this.Size)
			{
				this.mList[this.mIndex++] = item;
				if (this.mIndex == this.mList.Count)
				{
					this.mIndex = 0;
				}
			}
			else
			{
				this.mList.Add(item);
			}
		}

		public void Clear()
		{
			this.mList.Clear();
			this.mIndex = 0;
		}

		public int IndexOf(T item)
		{
			return this.mList.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public T this[int index]
		{
			get
			{
				return this.mList[index];
			}
			set
			{
				this.mList[index] = value;
			}
		}

		public IEnumerator GetEnumerator()
		{
			return this.mList.GetEnumerator();
		}

		public bool Contains(T item)
		{
			return this.mList.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			this.mList.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				return this.mList.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public bool Remove(T item)
		{
			return this.mList.Remove(item);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		private List<T> mList;

		private int mIndex;
	}
}
