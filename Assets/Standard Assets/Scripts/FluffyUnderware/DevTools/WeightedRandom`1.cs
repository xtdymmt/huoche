// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.WeightedRandom`1
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public class WeightedRandom<T>
	{
		public WeightedRandom(int initCapacity = 0)
		{
			this.mData = new List<T>(initCapacity);
		}

		public int Seed { get; set; }

		public bool RandomizeSeed { get; set; }

		private int Capacity
		{
			get
			{
				return this.mData.Capacity;
			}
		}

		public int Size
		{
			get
			{
				return this.mData.Count;
			}
		}

		public void Add(T item, int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				this.mData.Add(item);
			}
			this.mCurrentPosition = this.Size - 1;
		}

		public T Next()
		{
			if (this.mCurrentPosition < 1)
			{
				this.mCurrentPosition = this.Size - 1;
				this.mCurrentItem = this.mData[0];
				return this.mCurrentItem;
			}
			UnityEngine.Random.State state = UnityEngine.Random.state;
			if (this.RandomizeSeed)
			{
				this.Seed = UnityEngine.Random.Range(0, int.MaxValue);
			}
			UnityEngine.Random.InitState(this.Seed);
			int index = UnityEngine.Random.Range(0, this.mCurrentPosition);
			UnityEngine.Random.state = state;
			this.mCurrentItem = this.mData[index];
			this.mData[index] = this.mData[this.mCurrentPosition];
			this.mData[this.mCurrentPosition] = this.mCurrentItem;
			this.mCurrentPosition--;
			return this.mCurrentItem;
		}

		public void Reset()
		{
			this.mCurrentPosition = this.Size - 1;
		}

		public void Clear()
		{
			this.mData.Clear();
			this.mCurrentPosition = -1;
		}

		private List<T> mData;

		private int mCurrentPosition = -1;

		private T mCurrentItem;
	}
}
