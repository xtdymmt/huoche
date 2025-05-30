// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.GroupAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class GroupAttribute : DTAttribute, IDTGroupParsingAttribute, IDTGroupRenderAttribute
	{
		public GroupAttribute(string pathAndName) : base(15, false)
		{
			this.Path = pathAndName;
		}

		public string Path
		{
			get
			{
				return this.mPath;
			}
			protected set
			{
				this.PathIsAbsolute = (!string.IsNullOrEmpty(value) && value.StartsWith("@"));
				if (this.PathIsAbsolute)
				{
					this.mPath = value.Substring(1);
					if (string.IsNullOrEmpty(this.mPath))
					{
						this.mPath = null;
					}
				}
				else
				{
					this.mPath = value;
				}
			}
		}

		public bool PathIsAbsolute { get; private set; }

		public bool Expanded = true;

		public bool Invisible;

		public string Label;

		public string Tooltip;

		public string HelpURL;

		private string mPath;
	}
}
