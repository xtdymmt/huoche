// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.OutputSlotInfo
using System;

namespace FluffyUnderware.Curvy.Generator
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class OutputSlotInfo : SlotInfo
	{
		public OutputSlotInfo(Type type) : this(null, type)
		{
		}

		public OutputSlotInfo(string name, Type type) : base(name, new Type[]
		{
			type
		})
		{
		}

		public Type DataType
		{
			get
			{
				return this.DataTypes[0];
			}
		}
	}
}
