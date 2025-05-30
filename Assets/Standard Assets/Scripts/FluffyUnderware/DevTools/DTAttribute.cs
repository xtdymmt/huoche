// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.DTAttribute
using System;

namespace FluffyUnderware.DevTools
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class DTAttribute : Attribute, IComparable
	{
		public DTAttribute(int sortOrder, bool showBelow = false)
		{
			this.TypeSort = sortOrder;
			this.ShowBelowProperty = showBelow;
		}

		public int TypeSort { get; protected set; }

		public virtual int CompareTo(object obj)
		{
			DTAttribute dtattribute = (DTAttribute)obj;
			int num = this.ShowBelowProperty.CompareTo(dtattribute.ShowBelowProperty);
			if (num != 0)
			{
				return num;
			}
			int num2 = this.TypeSort.CompareTo(dtattribute.TypeSort);
			if (num2 == 0)
			{
				return this.Sort.CompareTo(dtattribute.Sort);
			}
			return num2;
		}

		public int Sort = 100;

		public bool ShowBelowProperty;

		public int Space;
	}
}
