// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGSpots
using System;
using System.Collections.Generic;

namespace FluffyUnderware.Curvy.Generator
{
	[CGDataInfo(0.96f, 0.96f, 0.96f, 1f)]
	public class CGSpots : CGData
	{
		public CGSpots()
		{
			this.Points = new CGSpot[0];
		}

		public CGSpots(params CGSpot[] points)
		{
			this.Points = points;
		}

		public CGSpots(params List<CGSpot>[] lists)
		{
			int num = 0;
			for (int i = 0; i < lists.Length; i++)
			{
				num += lists[i].Count;
			}
			this.Points = new CGSpot[num];
			num = 0;
			for (int j = 0; j < lists.Length; j++)
			{
				lists[j].CopyTo(this.Points, num);
				num += lists[j].Count;
			}
		}

		public CGSpots(CGSpots source)
		{
			this.Points = source.Points;
		}

		public override int Count
		{
			get
			{
				return this.Points.Length;
			}
		}

		public override T Clone<T>()
		{
			return new CGSpots(this) as T;
		}

		public CGSpot[] Points;
	}
}
