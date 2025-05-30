// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.ResourceLoaderAttribute
using System;

namespace FluffyUnderware.Curvy.Generator
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ResourceLoaderAttribute : Attribute
	{
		public ResourceLoaderAttribute(string resName)
		{
			this.ResourceName = resName;
		}

		public readonly string ResourceName;
	}
}
