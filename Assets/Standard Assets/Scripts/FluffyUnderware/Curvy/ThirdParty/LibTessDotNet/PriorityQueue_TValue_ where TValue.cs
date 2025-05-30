// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.ThirdParty.LibTessDotNet.PriorityQueue<TValue> where TValue
using System;
using System.Collections.Generic;

namespace FluffyUnderware.Curvy.ThirdParty.LibTessDotNet
{
	internal class PriorityQueue<TValue> where TValue : class
	{
		public PriorityQueue(int initialSize, PriorityHeap<TValue>.LessOrEqual leq)
		{
			this._leq = leq;
			this._heap = new PriorityHeap<TValue>(initialSize, leq);
			this._keys = new TValue[initialSize];
			this._size = 0;
			this._max = initialSize;
			this._initialized = false;
		}

		public bool Empty
		{
			get
			{
				return this._size == 0 && this._heap.Empty;
			}
		}

		private static void Swap(ref int a, ref int b)
		{
			int num = a;
			a = b;
			b = num;
		}

		public void Init()
		{
			Stack<PriorityQueue<TValue>.StackItem> stack = new Stack<PriorityQueue<TValue>.StackItem>();
			uint num = 2016473283u;
			int num2 = 0;
			int i = this._size - 1;
			this._order = new int[this._size + 1];
			int num3 = 0;
			for (int j = num2; j <= i; j++)
			{
				this._order[j] = num3;
				num3++;
			}
			stack.Push(new PriorityQueue<TValue>.StackItem
			{
				p = num2,
				r = i
			});
			while (stack.Count > 0)
			{
				PriorityQueue<TValue>.StackItem stackItem = stack.Pop();
				num2 = stackItem.p;
				i = stackItem.r;
				while (i > num2 + 10)
				{
					num = num * 1539415821u + 1u;
					int j = num2 + (int)((ulong)num % (ulong)((long)(i - num2 + 1)));
					num3 = this._order[j];
					this._order[j] = this._order[num2];
					this._order[num2] = num3;
					j = num2 - 1;
					int num4 = i + 1;
					for (;;)
					{
						j++;
						if (this._leq(this._keys[this._order[j]], this._keys[num3]))
						{
							do
							{
								num4--;
							}
							while (!this._leq(this._keys[num3], this._keys[this._order[num4]]));
							PriorityQueue<TValue>.Swap(ref this._order[j], ref this._order[num4]);
							if (j >= num4)
							{
								break;
							}
						}
					}
					PriorityQueue<TValue>.Swap(ref this._order[j], ref this._order[num4]);
					if (j - num2 < i - num4)
					{
						stack.Push(new PriorityQueue<TValue>.StackItem
						{
							p = num4 + 1,
							r = i
						});
						i = j - 1;
					}
					else
					{
						stack.Push(new PriorityQueue<TValue>.StackItem
						{
							p = num2,
							r = j - 1
						});
						num2 = num4 + 1;
					}
				}
				for (int j = num2 + 1; j <= i; j++)
				{
					num3 = this._order[j];
					int num4 = j;
					while (num4 > num2 && !this._leq(this._keys[num3], this._keys[this._order[num4 - 1]]))
					{
						this._order[num4] = this._order[num4 - 1];
						num4--;
					}
					this._order[num4] = num3;
				}
			}
			this._max = this._size;
			this._initialized = true;
			this._heap.Init();
		}

		public PQHandle Insert(TValue value)
		{
			if (this._initialized)
			{
				return this._heap.Insert(value);
			}
			int size = this._size;
			if (++this._size >= this._max)
			{
				this._max <<= 1;
				Array.Resize<TValue>(ref this._keys, this._max);
			}
			this._keys[size] = value;
			return new PQHandle
			{
				_handle = -(size + 1)
			};
		}

		public TValue ExtractMin()
		{
			if (this._size == 0)
			{
				return this._heap.ExtractMin();
			}
			TValue tvalue = this._keys[this._order[this._size - 1]];
			if (!this._heap.Empty)
			{
				TValue lhs = this._heap.Minimum();
				if (this._leq(lhs, tvalue))
				{
					return this._heap.ExtractMin();
				}
			}
			do
			{
				this._size--;
			}
			while (this._size > 0 && this._keys[this._order[this._size - 1]] == null);
			return tvalue;
		}

		public TValue Minimum()
		{
			if (this._size == 0)
			{
				return this._heap.Minimum();
			}
			TValue tvalue = this._keys[this._order[this._size - 1]];
			if (!this._heap.Empty)
			{
				TValue tvalue2 = this._heap.Minimum();
				if (this._leq(tvalue2, tvalue))
				{
					return tvalue2;
				}
			}
			return tvalue;
		}

		public void Remove(PQHandle handle)
		{
			int num = handle._handle;
			if (num >= 0)
			{
				this._heap.Remove(handle);
				return;
			}
			num = -(num + 1);
			this._keys[num] = (TValue)((object)null);
			while (this._size > 0 && this._keys[this._order[this._size - 1]] == null)
			{
				this._size--;
			}
		}

		private PriorityHeap<TValue>.LessOrEqual _leq;

		private PriorityHeap<TValue> _heap;

		private TValue[] _keys;

		private int[] _order;

		private int _size;

		private int _max;

		private bool _initialized;

		private class StackItem
		{
			internal int p;

			internal int r;
		}
	}
}
