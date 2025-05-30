// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.Couple<T1, T2>
using System;

namespace FluffyUnderware.DevTools
{
	public class Couple<T1, T2>
	{
		public Couple(T1 first, T2 second)
		{
			this.First = first;
			this.Second = second;
		}

		public T1 First { get; set; }

		public T2 Second { get; set; }
	}
}
