// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: RegionOptions
using System;

namespace FluffyUnderware.DevTools
{
	public struct RegionOptions<T>
	{
		public static RegionOptions<T> Default
		{
			get
			{
				return new RegionOptions<T>
				{
					OptionalTooltip = "Range",
					LabelFrom = "From",
					LabelTo = "To",
					ClampFrom = DTValueClamping.None,
					ClampTo = DTValueClamping.None
				};
			}
		}

		public static RegionOptions<T> MinMax(T min, T max)
		{
			return new RegionOptions<T>
			{
				LabelFrom = "From",
				LabelTo = "To",
				ClampFrom = DTValueClamping.Range,
				ClampTo = DTValueClamping.Range,
				FromMin = min,
				FromMax = max,
				ToMin = min,
				ToMax = max
			};
		}

		public string LabelFrom;

		public string LabelTo;

		public string OptionalTooltip;

		public DTValueClamping ClampFrom;

		public DTValueClamping ClampTo;

		public T FromMin;

		public T FromMax;

		public T ToMin;

		public T ToMax;
	}
}
