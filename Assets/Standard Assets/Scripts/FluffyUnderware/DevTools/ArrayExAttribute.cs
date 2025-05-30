// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.ArrayExAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class ArrayExAttribute : DTAttribute, IDTFieldParsingAttribute
	{
		public ArrayExAttribute() : base(35, false)
		{
		}

		public bool Draggable = true;

		public bool ShowHeader = true;

		public bool ShowAdd = true;

		public bool ShowDelete = true;

		public bool DropTarget = true;
	}
}
