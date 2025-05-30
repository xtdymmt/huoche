// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.ConformPath
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/Conform Path", ModuleName = "Conform Path", Description = "Projects a path")]
	[HelpURL("https://curvyeditor.com/doclink/cgconformpath")]
	public class ConformPath : CGModule, IOnRequestPath, IOnRequestProcessing
	{
		public Vector3 Direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (this.m_Direction != value)
				{
					this.m_Direction = value;
				}
				base.Dirty = true;
			}
		}

		public float MaxDistance
		{
			get
			{
				return this.m_MaxDistance;
			}
			set
			{
				if (this.m_MaxDistance != value)
				{
					this.m_MaxDistance = value;
				}
				base.Dirty = true;
			}
		}

		public float Offset
		{
			get
			{
				return this.m_Offset;
			}
			set
			{
				if (this.m_Offset != value)
				{
					this.m_Offset = value;
				}
				base.Dirty = true;
			}
		}

		public bool Warp
		{
			get
			{
				return this.m_Warp;
			}
			set
			{
				if (this.m_Warp != value)
				{
					this.m_Warp = value;
				}
				base.Dirty = true;
			}
		}

		public LayerMask LayerMask
		{
			get
			{
				return this.m_LayerMask;
			}
			set
			{
				if (this.m_LayerMask != value)
				{
					this.m_LayerMask = value;
				}
				base.Dirty = true;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.Properties.LabelWidth = 80f;
		}

		public override void Reset()
		{
			base.Reset();
			this.Direction = new Vector3(0f, -1f, 0f);
			this.MaxDistance = 100f;
			this.Offset = 0f;
			this.Warp = false;
			this.LayerMask = 0;
		}

		public float PathLength
		{
			get
			{
				if (this.OutPath.HasData)
				{
					return this.OutPath.GetData<CGPath>().Length;
				}
				return (!this.IsConfigured) ? 0f : this.InPath.SourceSlot(0).OnRequestPathModule.PathLength;
			}
		}

		public bool PathIsClosed
		{
			get
			{
				return this.IsConfigured && this.InPath.SourceSlot(0).OnRequestPathModule.PathIsClosed;
			}
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			CGDataRequestRasterization requestParameter = CGModule.GetRequestParameter<CGDataRequestRasterization>(ref requests);
			if (!requestParameter)
			{
				return null;
			}
			CGPath data = this.InPath.GetData<CGPath>(requests);
			return new CGData[]
			{
				ConformPath.Conform(base.Generator.transform, data, this.LayerMask, this.Direction, this.Offset, this.MaxDistance, this.Warp)
			};
		}

		public static CGPath Conform(Transform pathTransform, CGPath path, LayerMask layers, Vector3 projectionDirection, float offset, float rayLength, bool warp)
		{
			int count = path.Count;
			if (projectionDirection != Vector3.zero && rayLength > 0f && count > 0)
			{
				if (warp)
				{
					float num = float.MaxValue;
					for (int i = 0; i < count; i++)
					{
						RaycastHit raycastHit;
						if (Physics.Raycast(pathTransform.TransformPoint(path.Position[i]), projectionDirection, out raycastHit, rayLength, layers) && raycastHit.distance < num)
						{
							num = raycastHit.distance;
						}
					}
					if (num != 3.40282347E+38f)
					{
						Vector3 b = projectionDirection * (num + offset);
						for (int j = 0; j < path.Count; j++)
						{
							path.Position[j] += b;
						}
					}
				}
				else
				{
					for (int k = 0; k < count; k++)
					{
						RaycastHit raycastHit;
						if (Physics.Raycast(pathTransform.TransformPoint(path.Position[k]), projectionDirection, out raycastHit, rayLength, layers))
						{
							path.Position[k] += projectionDirection * (raycastHit.distance + offset);
						}
					}
				}
			}
			return path;
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGPath)
		}, Name = "Path", ModifiesData = true)]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath))]
		public CGModuleOutputSlot OutPath = new CGModuleOutputSlot();

		[SerializeField]
		[VectorEx("", "")]
		private Vector3 m_Direction = new Vector3(0f, -1f, 0f);

		[SerializeField]
		private float m_MaxDistance = 100f;

		[SerializeField]
		private float m_Offset;

		[SerializeField]
		private bool m_Warp;

		[SerializeField]
		private LayerMask m_LayerMask;
	}
}
