// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.IPool
using System;

namespace FluffyUnderware.DevTools
{
	public interface IPool
	{
		string Identifier { get; set; }

		PoolSettings Settings { get; }

		void Clear();

		void Reset();

		void Update();

		int Count { get; }
	}
}
