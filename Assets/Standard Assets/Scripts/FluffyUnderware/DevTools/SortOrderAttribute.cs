// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.SortOrderAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class SortOrderAttribute : DTAttribute, IDTFieldParsingAttribute
	{
		public SortOrderAttribute(int sort = 100) : base(0, false)
		{
			this.Sort = sort;
		}
	}
}
