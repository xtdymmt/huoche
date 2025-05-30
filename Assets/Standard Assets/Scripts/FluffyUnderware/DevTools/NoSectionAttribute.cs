// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.NoSectionAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class NoSectionAttribute : SectionAttribute
	{
		public NoSectionAttribute() : base(string.Empty, true, false, 100)
		{
			base.TypeSort = 10;
		}
	}
}
