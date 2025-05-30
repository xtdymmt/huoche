// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.SlotInfo
using System;

namespace FluffyUnderware.Curvy.Generator
{
	public class SlotInfo : Attribute, IComparable
	{
		protected SlotInfo(string name, params Type[] type)
		{
			this.DataTypes = type;
			this.Name = name;
		}

		protected SlotInfo(params Type[] type) : this(null, type)
		{
		}

		public string DisplayName
		{
			get
			{
				return this.displayName ?? this.Name;
			}
			set
			{
				this.displayName = value;
			}
		}

		public int CompareTo(object obj)
		{
			return string.Compare(((SlotInfo)obj).Name, this.Name, StringComparison.Ordinal);
		}

		public readonly Type[] DataTypes;

		public string Name;

		private string displayName;

		public string Tooltip;

		public bool Array;
	}
}
