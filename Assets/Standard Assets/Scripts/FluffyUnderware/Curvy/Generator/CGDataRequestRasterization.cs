// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGDataRequestRasterization
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public class CGDataRequestRasterization : CGDataRequestParameter
	{
		public CGDataRequestRasterization(float start, float rasterizedRelativeLength, int resolution, float splineAbsoluteLength, float angle, CGDataRequestRasterization.ModeEnum mode = CGDataRequestRasterization.ModeEnum.Even)
		{
			this.Start = Mathf.Repeat(start, 1f);
			this.RasterizedRelativeLength = Mathf.Clamp01(rasterizedRelativeLength);
			this.Resolution = resolution;
			this.SplineAbsoluteLength = splineAbsoluteLength;
			this.AngleThreshold = angle;
			this.Mode = mode;
		}

		public CGDataRequestRasterization(CGDataRequestRasterization source) : this(source.Start, source.RasterizedRelativeLength, source.Resolution, source.SplineAbsoluteLength, source.AngleThreshold, source.Mode)
		{
		}

		public override bool Equals(object obj)
		{
			CGDataRequestRasterization cgdataRequestRasterization = obj as CGDataRequestRasterization;
			return cgdataRequestRasterization != null && (this.Start == cgdataRequestRasterization.Start && this.RasterizedRelativeLength == cgdataRequestRasterization.RasterizedRelativeLength && this.Resolution == cgdataRequestRasterization.Resolution && this.SplineAbsoluteLength == cgdataRequestRasterization.SplineAbsoluteLength && this.AngleThreshold == cgdataRequestRasterization.AngleThreshold) && this.Mode == cgdataRequestRasterization.Mode;
		}

		public override int GetHashCode()
		{
			return new
			{
				A = this.Start,
				B = this.RasterizedRelativeLength,
				C = this.Resolution,
				D = this.AngleThreshold,
				E = this.Mode,
				F = this.SplineAbsoluteLength
			}.GetHashCode();
		}

		public float Start;

		public float RasterizedRelativeLength;

		public int Resolution;

		public float SplineAbsoluteLength;

		public float AngleThreshold;

		public CGDataRequestRasterization.ModeEnum Mode;

		public enum ModeEnum
		{
			Even,
			Optimized
		}
	}
}
