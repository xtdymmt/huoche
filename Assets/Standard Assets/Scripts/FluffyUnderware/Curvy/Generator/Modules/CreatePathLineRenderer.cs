// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.Modules.CreatePathLineRenderer
using System;
using FluffyUnderware.Curvy.Utils;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Create/Path Line Renderer", ModuleName = "Create Path Line Renderer", Description = "Feeds a Line Renderer with a Path")]
	public class CreatePathLineRenderer : CGModule
	{
		public LineRenderer LineRenderer
		{
			get
			{
				if (this.mLineRenderer == null)
				{
					this.mLineRenderer = base.GetComponent<LineRenderer>();
				}
				return this.mLineRenderer;
			}
		}

		public override bool IsConfigured
		{
			get
			{
				return base.IsConfigured;
			}
		}

		public override bool IsInitialized
		{
			get
			{
				return base.IsInitialized;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			this.createLR();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
		}

		public override void Reset()
		{
			base.Reset();
		}

		public override void Refresh()
		{
			base.Refresh();
			CGPath data = this.InPath.GetData<CGPath>(new CGDataRequestParameter[0]);
			if (data != null)
			{
				this.LineRenderer.positionCount = data.Position.Length;
				this.LineRenderer.SetPositions(data.Position);
			}
			else
			{
				this.LineRenderer.positionCount = 0;
			}
		}

		private void createLR()
		{
			if (this.LineRenderer == null)
			{
				this.mLineRenderer = base.gameObject.AddComponent<LineRenderer>();
				this.mLineRenderer.useWorldSpace = false;
				this.mLineRenderer.textureMode = LineTextureMode.Tile;
				this.mLineRenderer.sharedMaterial = CurvyUtility.GetDefaultMaterial();
			}
		}

		[HideInInspector]
		[InputSlotInfo(new Type[]
		{
			typeof(CGPath)
		}, DisplayName = "Rasterized Path")]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		private LineRenderer mLineRenderer;
	}
}
