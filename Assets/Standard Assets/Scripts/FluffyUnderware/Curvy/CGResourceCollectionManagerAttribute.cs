// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CGResourceCollectionManagerAttribute
using System;

namespace FluffyUnderware.Curvy
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class CGResourceCollectionManagerAttribute : CGResourceManagerAttribute
	{
		public CGResourceCollectionManagerAttribute(string resourceName) : base(resourceName)
		{
			this.ReadOnly = true;
		}

		public bool ShowCount;
	}
}
