// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.ConnectionHeadingEnumMethods
using System;

namespace FluffyUnderware.Curvy
{
	public static class ConnectionHeadingEnumMethods
	{
		public static ConnectionHeadingEnum ResolveAuto(this ConnectionHeadingEnum heading, CurvySplineSegment followUp)
		{
			if (heading == ConnectionHeadingEnum.Auto)
			{
				if (followUp.Spline.FirstVisibleControlPoint == followUp)
				{
					heading = ConnectionHeadingEnum.Plus;
				}
				else if (followUp.Spline.LastVisibleControlPoint == followUp)
				{
					heading = ConnectionHeadingEnum.Minus;
				}
				else
				{
					heading = ConnectionHeadingEnum.Sharp;
				}
			}
			return heading;
		}
	}
}
