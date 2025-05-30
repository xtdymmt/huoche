// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.DuplicateEditorMesh
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	[ExecuteInEditMode]
	public class DuplicateEditorMesh : MonoBehaviour
	{
		public MeshFilter Filter
		{
			get
			{
				if (this.mFilter == null)
				{
					this.mFilter = base.GetComponent<MeshFilter>();
				}
				return this.mFilter;
			}
		}

		protected virtual void Awake()
		{
			if (!Application.isPlaying)
			{
				MeshFilter filter = this.Filter;
				if (filter && filter.sharedMesh != null)
				{
					DuplicateEditorMesh[] array = UnityEngine.Object.FindObjectsOfType<DuplicateEditorMesh>();
					foreach (DuplicateEditorMesh duplicateEditorMesh in array)
					{
						if (duplicateEditorMesh != this)
						{
							MeshFilter filter2 = duplicateEditorMesh.Filter;
							if (filter2 && filter2.sharedMesh == filter.sharedMesh)
							{
								filter.mesh = new Mesh
								{
									name = filter2.sharedMesh.name
								};
							}
						}
					}
				}
			}
		}

		private MeshFilter mFilter;
	}
}
