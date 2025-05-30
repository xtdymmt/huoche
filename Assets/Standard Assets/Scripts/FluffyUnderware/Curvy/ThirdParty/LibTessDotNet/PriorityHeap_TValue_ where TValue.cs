// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.ThirdParty.LibTessDotNet.PriorityHeap<TValue> where TValue
using System;

namespace FluffyUnderware.Curvy.ThirdParty.LibTessDotNet
{
	internal class PriorityHeap<TValue> where TValue : class
	{
		public PriorityHeap(int initialSize, PriorityHeap<TValue>.LessOrEqual leq)
		{
			this._leq = leq;
			this._nodes = new int[initialSize + 1];
			this._handles = new PriorityHeap<TValue>.HandleElem[initialSize + 1];
			this._size = 0;
			this._max = initialSize;
			this._freeList = 0;
			this._initialized = false;
			this._nodes[1] = 1;
			this._handles[1] = new PriorityHeap<TValue>.HandleElem
			{
				_key = (TValue)((object)null)
			};
		}

		public bool Empty
		{
			get
			{
				return this._size == 0;
			}
		}

		private void FloatDown(int curr)
		{
			int num = this._nodes[curr];
			for (;;)
			{
				int num2 = curr << 1;
				if (num2 < this._size && this._leq(this._handles[this._nodes[num2 + 1]]._key, this._handles[this._nodes[num2]]._key))
				{
					num2++;
				}
				int num3 = this._nodes[num2];
				if (num2 > this._size || this._leq(this._handles[num]._key, this._handles[num3]._key))
				{
					break;
				}
				this._nodes[curr] = num3;
				this._handles[num3]._node = curr;
				curr = num2;
			}
			this._nodes[curr] = num;
			this._handles[num]._node = curr;
		}

		private void FloatUp(int curr)
		{
			int num = this._nodes[curr];
			for (;;)
			{
				int num2 = curr >> 1;
				int num3 = this._nodes[num2];
				if (num2 == 0 || this._leq(this._handles[num3]._key, this._handles[num]._key))
				{
					break;
				}
				this._nodes[curr] = num3;
				this._handles[num3]._node = curr;
				curr = num2;
			}
			this._nodes[curr] = num;
			this._handles[num]._node = curr;
		}

		public void Init()
		{
			for (int i = this._size; i >= 1; i--)
			{
				this.FloatDown(i);
			}
			this._initialized = true;
		}

		public PQHandle Insert(TValue value)
		{
			int num = ++this._size;
			if (num * 2 > this._max)
			{
				this._max <<= 1;
				Array.Resize<int>(ref this._nodes, this._max + 1);
				Array.Resize<PriorityHeap<TValue>.HandleElem>(ref this._handles, this._max + 1);
			}
			int num2;
			if (this._freeList == 0)
			{
				num2 = num;
			}
			else
			{
				num2 = this._freeList;
				this._freeList = this._handles[num2]._node;
			}
			this._nodes[num] = num2;
			if (this._handles[num2] == null)
			{
				this._handles[num2] = new PriorityHeap<TValue>.HandleElem
				{
					_key = value,
					_node = num
				};
			}
			else
			{
				this._handles[num2]._node = num;
				this._handles[num2]._key = value;
			}
			if (this._initialized)
			{
				this.FloatUp(num);
			}
			return new PQHandle
			{
				_handle = num2
			};
		}

		public TValue ExtractMin()
		{
			int num = this._nodes[1];
			TValue key = this._handles[num]._key;
			if (this._size > 0)
			{
				this._nodes[1] = this._nodes[this._size];
				this._handles[this._nodes[1]]._node = 1;
				this._handles[num]._key = (TValue)((object)null);
				this._handles[num]._node = this._freeList;
				this._freeList = num;
				if (--this._size > 0)
				{
					this.FloatDown(1);
				}
			}
			return key;
		}

		public TValue Minimum()
		{
			return this._handles[this._nodes[1]]._key;
		}

		public void Remove(PQHandle handle)
		{
			int handle2 = handle._handle;
			int node = this._handles[handle2]._node;
			this._nodes[node] = this._nodes[this._size];
			this._handles[this._nodes[node]]._node = node;
			if (node <= --this._size)
			{
				if (node <= 1 || this._leq(this._handles[this._nodes[node >> 1]]._key, this._handles[this._nodes[node]]._key))
				{
					this.FloatDown(node);
				}
				else
				{
					this.FloatUp(node);
				}
			}
			this._handles[handle2]._key = (TValue)((object)null);
			this._handles[handle2]._node = this._freeList;
			this._freeList = handle2;
		}

		private PriorityHeap<TValue>.LessOrEqual _leq;

		private int[] _nodes;

		private PriorityHeap<TValue>.HandleElem[] _handles;

		private int _size;

		private int _max;

		private int _freeList;

		private bool _initialized;

		public delegate bool LessOrEqual(TValue lhs, TValue rhs);

		protected class HandleElem
		{
			internal TValue _key;

			internal int _node;
		}
	}
}
