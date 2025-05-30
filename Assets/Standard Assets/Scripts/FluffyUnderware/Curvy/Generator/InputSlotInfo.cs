// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.InputSlotInfo
using System;

namespace FluffyUnderware.Curvy.Generator
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class InputSlotInfo : SlotInfo
	{
		public InputSlotInfo(string name, params Type[] type) : base(name, type)
		{
		}

		public InputSlotInfo(params Type[] type) : this(null, type)
		{
		}

		public bool IsValidFrom(Type outType)
		{
			for (int i = 0; i < this.DataTypes.Length; i++)
			{
				if (outType == this.DataTypes[i] || outType.IsSubclassOf(this.DataTypes[i]))
				{
					return true;
				}
			}
			return false;
		}

		public bool RequestDataOnly;

		public bool Optional;

		public bool ModifiesData;
	}
}
