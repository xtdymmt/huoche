// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGPath
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[CGDataInfo(0.13f, 0.59f, 0.95f, 1f)]
	public class CGPath : CGShape
	{
		public CGPath()
		{
		}

		public CGPath(CGPath source) : base(source)
		{
			this.Direction = (Vector3[])source.Direction.Clone();
		}

		public override T Clone<T>()
		{
			return new CGPath(this) as T;
		}

		public static void Copy(CGPath dest, CGPath source)
		{
			CGShape.Copy(dest, source);
			Array.Resize<Vector3>(ref dest.Direction, source.Direction.Length);
			source.Direction.CopyTo(dest.Direction, 0);
		}

		public void Interpolate(float f, out Vector3 pos, out Vector3 dir, out Vector3 up)
		{
			float t;
			int findex = base.GetFIndex(f, out t);
			pos = Vector3.LerpUnclamped(this.Position[findex], this.Position[findex + 1], t);
			dir = Vector3.SlerpUnclamped(this.Direction[findex], this.Direction[findex + 1], t);
			up = Vector3.SlerpUnclamped(this.Normal[findex], this.Normal[findex + 1], t);
		}

		public void Interpolate(float f, float angleF, out Vector3 pos, out Vector3 dir, out Vector3 up)
		{
			this.Interpolate(f, out pos, out dir, out up);
			if (angleF != 0f)
			{
				Quaternion rotation = Quaternion.AngleAxis(angleF * -360f, dir);
				up = rotation * up;
			}
		}

		public Vector3 InterpolateDirection(float f)
		{
			float t;
			int findex = base.GetFIndex(f, out t);
			return Vector3.SlerpUnclamped(this.Direction[findex], this.Direction[findex + 1], t);
		}

		public override void Recalculate()
		{
			base.Recalculate();
			for (int i = 1; i < this.Count; i++)
			{
				this.Direction[i].x = this.Position[i].x - this.Position[i - 1].x;
				this.Direction[i].y = this.Position[i].y - this.Position[i - 1].y;
				this.Direction[i].z = this.Position[i].z - this.Position[i - 1].z;
				this.Direction[i] = Vector3.Normalize(this.Direction[i]);
			}
		}

		public Vector3[] Direction = new Vector3[0];
	}
}
