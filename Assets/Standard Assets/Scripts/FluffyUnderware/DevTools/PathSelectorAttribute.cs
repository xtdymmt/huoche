// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.PathSelectorAttribute
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public class PathSelectorAttribute : DTPropertyAttribute
	{
		public PathSelectorAttribute(PathSelectorAttribute.DialogMode mode = PathSelectorAttribute.DialogMode.OpenFile) : base(string.Empty, string.Empty)
		{
			this.Mode = mode;
			this.Directory = Application.dataPath;
		}

		public readonly PathSelectorAttribute.DialogMode Mode;

		public string Title;

		public string Directory;

		public string Extension;

		public string DefaultName;

		public enum DialogMode
		{
			OpenFile,
			OpenFolder,
			CreateFile
		}
	}
}
