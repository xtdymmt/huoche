// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGBounds
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[CGDataInfo(1f, 0.8f, 0.5f, 1f)]
	public class CGBounds : CGData
	{
		public CGBounds()
		{
		}

		public CGBounds(Bounds bounds)
		{
			this.Bounds = bounds;
		}

		public CGBounds(CGBounds source)
		{
			this.Name = source.Name;
			if (source.mBounds != null)
			{
				this.Bounds = source.Bounds;
			}
		}

		public Bounds Bounds
		{
			get
			{
				if (this.mBounds == null)
				{
					this.RecalculateBounds();
				}
				return this.mBounds.Value;
			}
			set
			{
				if (this.mBounds != value)
				{
					this.mBounds = new Bounds?(value);
				}
			}
		}

		public float Depth
		{
			get
			{
				return this.Bounds.size.z;
			}
		}

		public virtual void RecalculateBounds()
		{
			this.Bounds = default(Bounds);
		}

		public override T Clone<T>()
		{
			return new CGBounds(this) as T;
		}

		public static void Copy(CGBounds dest, CGBounds source)
		{
			if (source.mBounds != null)
			{
				dest.Bounds = source.Bounds;
			}
		}

		protected Bounds? mBounds;
	}
}
