// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.ModuleInfoAttribute
using System;

namespace FluffyUnderware.Curvy.Generator
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ModuleInfoAttribute : Attribute, IComparable
	{
		public ModuleInfoAttribute(string name)
		{
			this.MenuName = name;
		}

		public int CompareTo(object obj)
		{
			return string.Compare(this.MenuName, ((ModuleInfoAttribute)obj).MenuName, StringComparison.Ordinal);
		}

		public readonly string MenuName;

		public string ModuleName;

		public string Description;

		public bool UsesRandom;
	}
}
