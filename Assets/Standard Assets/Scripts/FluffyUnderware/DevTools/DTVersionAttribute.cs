// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.DTVersionAttribute
using System;

namespace FluffyUnderware.DevTools
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class DTVersionAttribute : Attribute
	{
		public DTVersionAttribute(string version)
		{
			this.Version = version;
		}

		public readonly string Version;
	}
}
