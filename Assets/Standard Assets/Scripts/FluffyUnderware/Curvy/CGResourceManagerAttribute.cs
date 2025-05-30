// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CGResourceManagerAttribute
using System;
using FluffyUnderware.DevTools;

namespace FluffyUnderware.Curvy
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class CGResourceManagerAttribute : DTPropertyAttribute
	{
		public CGResourceManagerAttribute(string resourceName) : base(string.Empty, string.Empty)
		{
			this.ResourceName = resourceName;
		}

		public readonly string ResourceName;

		public bool ReadOnly;
	}
}
