// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.Note
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Note", ModuleName = "Note", Description = "Creates a note")]
	[HelpURL("https://curvyeditor.com/doclink/cgnote")]
	public class Note : CGModule, INoProcessing
	{
		public string NoteText
		{
			get
			{
				return this.m_Note;
			}
			set
			{
				if (this.m_Note != value)
				{
					this.m_Note = value;
				}
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.Properties.MinWidth = 200f;
			this.Properties.LabelWidth = 50f;
		}

		[SerializeField]
		[TextArea(3, 10)]
		private string m_Note;
	}
}
