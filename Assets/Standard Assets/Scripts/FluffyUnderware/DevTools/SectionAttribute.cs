// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.SectionAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class SectionAttribute : GroupAttribute
	{
		public SectionAttribute(string name, bool expanded = true, bool fix = false, int sort = 100) : base(name)
		{
			this.Expanded = expanded;
			base.TypeSort = 10;
			this.Sort = sort;
			this.Fixed = fix;
		}

		public bool Fixed;
	}
}
