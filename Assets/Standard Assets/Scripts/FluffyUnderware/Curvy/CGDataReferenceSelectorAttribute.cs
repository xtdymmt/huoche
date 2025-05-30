// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CGDataReferenceSelectorAttribute
using System;
using FluffyUnderware.DevTools;

namespace FluffyUnderware.Curvy
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class CGDataReferenceSelectorAttribute : DTPropertyAttribute
	{
		public CGDataReferenceSelectorAttribute(Type dataType) : base(string.Empty, string.Empty)
		{
			this.DataType = dataType;
		}

		public readonly Type DataType;
	}
}
