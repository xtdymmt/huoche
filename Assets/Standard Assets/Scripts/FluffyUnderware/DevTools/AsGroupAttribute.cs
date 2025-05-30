// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.AsGroupAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class AsGroupAttribute : GroupAttribute, IDTFieldParsingAttribute, IDTFieldRenderAttribute
	{
		public AsGroupAttribute(string pathAndName = null) : base(pathAndName)
		{
			base.TypeSort = 10;
		}
	}
}
