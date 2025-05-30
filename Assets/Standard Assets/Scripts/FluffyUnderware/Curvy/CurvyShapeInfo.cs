// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvyShapeInfo
using System;

namespace FluffyUnderware.Curvy
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class CurvyShapeInfo : Attribute
	{
		public CurvyShapeInfo(string name, bool is2D = true)
		{
			this.Name = name;
			this.Is2D = is2D;
		}

		public readonly string Name;

		public readonly bool Is2D;
	}
}
