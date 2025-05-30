// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.DTTween
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public static class DTTween
	{
		public static float Ease(DTTween.EasingMethod method, float t, float b, float c)
		{
			switch (method)
			{
			case DTTween.EasingMethod.ExponentialIn:
				return DTTween.ExpoIn(t, b, c);
			case DTTween.EasingMethod.ExponentialOut:
				return DTTween.ExpoOut(t, b, c);
			case DTTween.EasingMethod.ExponentialInOut:
				return DTTween.ExpoInOut(t, b, c);
			case DTTween.EasingMethod.ExponentialOutIn:
				return DTTween.ExpoOutIn(t, b, c);
			case DTTween.EasingMethod.CircularIn:
				return DTTween.CircIn(t, b, c);
			case DTTween.EasingMethod.CircularOut:
				return DTTween.CircOut(t, b, c);
			case DTTween.EasingMethod.CircularInOut:
				return DTTween.CircInOut(t, b, c);
			case DTTween.EasingMethod.CircularOutIn:
				return DTTween.CircOutIn(t, b, c);
			case DTTween.EasingMethod.QuadraticIn:
				return DTTween.QuadIn(t, b, c);
			case DTTween.EasingMethod.QuadraticOut:
				return DTTween.QuadOut(t, b, c);
			case DTTween.EasingMethod.QuadraticInOut:
				return DTTween.QuadInOut(t, b, c);
			case DTTween.EasingMethod.QuadraticOutIn:
				return DTTween.QuadOutIn(t, b, c);
			case DTTween.EasingMethod.SinusIn:
				return DTTween.SineIn(t, b, c);
			case DTTween.EasingMethod.SinusOut:
				return DTTween.SineOut(t, b, c);
			case DTTween.EasingMethod.SinusInOut:
				return DTTween.SineInOut(t, b, c);
			case DTTween.EasingMethod.SinusOutIn:
				return DTTween.SineOutIn(t, b, c);
			case DTTween.EasingMethod.CubicIn:
				return DTTween.CubicIn(t, b, c);
			case DTTween.EasingMethod.CubicOut:
				return DTTween.CubicOut(t, b, c);
			case DTTween.EasingMethod.CubicInOut:
				return DTTween.CubicInOut(t, b, c);
			case DTTween.EasingMethod.CubicOutIn:
				return DTTween.CubicOutIn(t, b, c);
			case DTTween.EasingMethod.QuarticIn:
				return DTTween.QuartIn(t, b, c);
			case DTTween.EasingMethod.QuarticOut:
				return DTTween.QuartOut(t, b, c);
			case DTTween.EasingMethod.QuarticInOut:
				return DTTween.QuartInOut(t, b, c);
			case DTTween.EasingMethod.QuarticOutIn:
				return DTTween.QuartOutIn(t, b, c);
			case DTTween.EasingMethod.QuinticIn:
				return DTTween.QuintIn(t, b, c);
			case DTTween.EasingMethod.QuinticOut:
				return DTTween.QuintOut(t, b, c);
			case DTTween.EasingMethod.QuinticInOut:
				return DTTween.QuintInOut(t, b, c);
			case DTTween.EasingMethod.QuinticOutIn:
				return DTTween.QuintOutIn(t, b, c);
			default:
				return DTTween.Linear(t, b, c);
			}
		}

		public static float Ease(DTTween.EasingMethod method, float t, float b, float c, float d)
		{
			switch (method)
			{
			case DTTween.EasingMethod.ExponentialIn:
				return DTTween.ExpoIn(t, b, c, d);
			case DTTween.EasingMethod.ExponentialOut:
				return DTTween.ExpoOut(t, b, c, d);
			case DTTween.EasingMethod.ExponentialInOut:
				return DTTween.ExpoInOut(t, b, c, d);
			case DTTween.EasingMethod.ExponentialOutIn:
				return DTTween.ExpoOutIn(t, b, c, d);
			case DTTween.EasingMethod.CircularIn:
				return DTTween.CircIn(t, b, c, d);
			case DTTween.EasingMethod.CircularOut:
				return DTTween.CircOut(t, b, c, d);
			case DTTween.EasingMethod.CircularInOut:
				return DTTween.CircInOut(t, b, c, d);
			case DTTween.EasingMethod.CircularOutIn:
				return DTTween.CircOutIn(t, b, c, d);
			case DTTween.EasingMethod.QuadraticIn:
				return DTTween.QuadIn(t, b, c, d);
			case DTTween.EasingMethod.QuadraticOut:
				return DTTween.QuadOut(t, b, c, d);
			case DTTween.EasingMethod.QuadraticInOut:
				return DTTween.QuadInOut(t, b, c, d);
			case DTTween.EasingMethod.QuadraticOutIn:
				return DTTween.QuadOutIn(t, b, c, d);
			case DTTween.EasingMethod.SinusIn:
				return DTTween.SineIn(t, b, c, d);
			case DTTween.EasingMethod.SinusOut:
				return DTTween.SineOut(t, b, c, d);
			case DTTween.EasingMethod.SinusInOut:
				return DTTween.SineInOut(t, b, c, d);
			case DTTween.EasingMethod.SinusOutIn:
				return DTTween.SineOutIn(t, b, c, d);
			case DTTween.EasingMethod.CubicIn:
				return DTTween.CubicIn(t, b, c, d);
			case DTTween.EasingMethod.CubicOut:
				return DTTween.CubicOut(t, b, c, d);
			case DTTween.EasingMethod.CubicInOut:
				return DTTween.CubicInOut(t, b, c, d);
			case DTTween.EasingMethod.CubicOutIn:
				return DTTween.CubicOutIn(t, b, c, d);
			case DTTween.EasingMethod.QuarticIn:
				return DTTween.QuartIn(t, b, c, d);
			case DTTween.EasingMethod.QuarticOut:
				return DTTween.QuartOut(t, b, c, d);
			case DTTween.EasingMethod.QuarticInOut:
				return DTTween.QuartInOut(t, b, c, d);
			case DTTween.EasingMethod.QuarticOutIn:
				return DTTween.QuartOutIn(t, b, c, d);
			case DTTween.EasingMethod.QuinticIn:
				return DTTween.QuintIn(t, b, c, d);
			case DTTween.EasingMethod.QuinticOut:
				return DTTween.QuintOut(t, b, c, d);
			case DTTween.EasingMethod.QuinticInOut:
				return DTTween.QuintInOut(t, b, c, d);
			case DTTween.EasingMethod.QuinticOutIn:
				return DTTween.QuintOutIn(t, b, c, d);
			default:
				return DTTween.Linear(t, b, c, d);
			}
		}

		public static float Linear(float t, float b, float c)
		{
			return c * Mathf.Clamp01(t) + b;
		}

		public static float Linear(float t, float b, float c, float d)
		{
			return c * t / d + b;
		}

		public static float ExpoOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return (t != 1f) ? (c * (-Mathf.Pow(2f, -10f * t) + 1f) + b) : (b + c);
		}

		public static float ExpoOut(float t, float b, float c, float d)
		{
			return (t != d) ? (c * (-Mathf.Pow(2f, -10f * t / d) + 1f) + b) : (b + c);
		}

		public static float ExpoIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return (t != 0f) ? (c * Mathf.Pow(2f, 10f * (t - 1f)) + b) : b;
		}

		public static float ExpoIn(float t, float b, float c, float d)
		{
			return (t != 0f) ? (c * Mathf.Pow(2f, 10f * (t / d - 1f)) + b) : b;
		}

		public static float ExpoInOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if (t == 0f)
			{
				return b;
			}
			if (t == 1f)
			{
				return b + c;
			}
			if ((t /= 0.5f) < 1f)
			{
				return c / 2f * Mathf.Pow(2f, 10f * (t - 1f)) + b;
			}
			return c / 2f * (-Mathf.Pow(2f, -10f * (t -= 1f)) + 2f) + b;
		}

		public static float ExpoInOut(float t, float b, float c, float d)
		{
			if (t == 0f)
			{
				return b;
			}
			if (t == d)
			{
				return b + c;
			}
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * Mathf.Pow(2f, 10f * (t - 1f)) + b;
			}
			return c / 2f * (-Mathf.Pow(2f, -10f * (t -= 1f)) + 2f) + b;
		}

		public static float ExpoOutIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if (t < 0.5f)
			{
				return DTTween.ExpoOut(t * 2f, b, c / 2f);
			}
			return DTTween.ExpoIn(t * 2f - 1f, b + c / 2f, c / 2f);
		}

		public static float ExpoOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return DTTween.ExpoOut(t * 2f, b, c / 2f, d);
			}
			return DTTween.ExpoIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float CircOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return c * Mathf.Sqrt(1f - (t -= 1f) * t) + b;
		}

		public static float CircOut(float t, float b, float c, float d)
		{
			return c * Mathf.Sqrt(1f - (t = t / d - 1f) * t) + b;
		}

		public static float CircIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return -c * (Mathf.Sqrt(1f - t * t) - 1f) + b;
		}

		public static float CircIn(float t, float b, float c, float d)
		{
			return -c * (Mathf.Sqrt(1f - (t /= d) * t) - 1f) + b;
		}

		public static float CircInOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if ((t /= 0.5f) < 1f)
			{
				return -c / 2f * (Mathf.Sqrt(1f - t * t) - 1f) + b;
			}
			return c / 2f * (Mathf.Sqrt(1f - (t -= 2f) * t) + 1f) + b;
		}

		public static float CircInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return -c / 2f * (Mathf.Sqrt(1f - t * t) - 1f) + b;
			}
			return c / 2f * (Mathf.Sqrt(1f - (t -= 2f) * t) + 1f) + b;
		}

		public static float CircOutIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if (t < 0.5f)
			{
				return DTTween.CircOut(t * 2f, b, c / 2f);
			}
			return DTTween.CircIn(t * 2f - 1f, b + c / 2f, c / 2f);
		}

		public static float CircOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return DTTween.CircOut(t * 2f, b, c / 2f, d);
			}
			return DTTween.CircIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float QuadOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return -c * t * (t - 2f) + b;
		}

		public static float QuadOut(float t, float b, float c, float d)
		{
			return -c * (t /= d) * (t - 2f) + b;
		}

		public static float QuadIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return c * t * t + b;
		}

		public static float QuadIn(float t, float b, float c, float d)
		{
			return c * (t /= d) * t + b;
		}

		public static float QuadInOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if ((t /= 0.5f) < 1f)
			{
				return -c / 2f * (Mathf.Sqrt(1f - t * t) - 1f) + b;
			}
			return c / 2f * (Mathf.Sqrt(1f - (t -= 2f) * t) + 1f) + b;
		}

		public static float QuadInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return -c / 2f * (Mathf.Sqrt(1f - t * t) - 1f) + b;
			}
			return c / 2f * (Mathf.Sqrt(1f - (t -= 2f) * t) + 1f) + b;
		}

		public static float QuadOutIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if (t < 0.5f)
			{
				return DTTween.QuadOut(t * 2f, b, c / 2f);
			}
			return DTTween.QuadIn(t * 2f - 1f, b + c / 2f, c / 2f);
		}

		public static float QuadOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return DTTween.QuadOut(t * 2f, b, c / 2f, d);
			}
			return DTTween.QuadIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float SineOut(float t, float b, float c)
		{
			return c * Mathf.Sin(Mathf.Clamp01(t) * 1.57079637f) + b;
		}

		public static float SineOut(float t, float b, float c, float d)
		{
			return c * Mathf.Sin(t / d * 1.57079637f) + b;
		}

		public static float SineIn(float t, float b, float c)
		{
			return -c * Mathf.Cos(Mathf.Clamp01(t) * 1.57079637f) + c + b;
		}

		public static float SineIn(float t, float b, float c, float d)
		{
			return -c * Mathf.Cos(t / d * 1.57079637f) + c + b;
		}

		public static float SineInOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if ((t /= 0.5f) < 1f)
			{
				return c / 2f * Mathf.Sin(3.14159274f * t / 2f) + b;
			}
			return -c / 2f * (Mathf.Cos(3.14159274f * (t -= 1f) / 2f) - 2f) + b;
		}

		public static float SineInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * Mathf.Sin(3.14159274f * t / 2f) + b;
			}
			return -c / 2f * (Mathf.Cos(3.14159274f * (t -= 1f) / 2f) - 2f) + b;
		}

		public static float SineOutIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if (t < 0.5f)
			{
				return DTTween.SineOut(t * 2f, b, c / 2f);
			}
			return DTTween.SineIn(t * 2f - 1f, b + c / 2f, c / 2f);
		}

		public static float SineOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return DTTween.SineOut(t * 2f, b, c / 2f, d);
			}
			return DTTween.SineIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float CubicOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return c * ((t -= 1f) * t * t + 1f) + b;
		}

		public static float CubicOut(float t, float b, float c, float d)
		{
			return c * ((t = t / d - 1f) * t * t + 1f) + b;
		}

		public static float CubicIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return c * t * t * t + b;
		}

		public static float CubicIn(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * t + b;
		}

		public static float CubicInOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if ((t /= 0.5f) < 1f)
			{
				return c / 2f * t * t * t + b;
			}
			return c / 2f * ((t -= 2f) * t * t + 2f) + b;
		}

		public static float CubicInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * t * t * t + b;
			}
			return c / 2f * ((t -= 2f) * t * t + 2f) + b;
		}

		public static float CubicOutIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if (t < 0.5f)
			{
				return DTTween.CubicOut(t * 2f, b, c / 2f);
			}
			return DTTween.CubicIn(t * 2f - 1f, b + c / 2f, c / 2f);
		}

		public static float CubicOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return DTTween.CubicOut(t * 2f, b, c / 2f, d);
			}
			return DTTween.CubicIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float QuartOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return -c * ((t -= 1f) * t * t * t - 1f) + b;
		}

		public static float QuartOut(float t, float b, float c, float d)
		{
			return -c * ((t = t / d - 1f) * t * t * t - 1f) + b;
		}

		public static float QuartIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return c * t * t * t * t + b;
		}

		public static float QuartIn(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * t * t + b;
		}

		public static float QuartInOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if ((t /= 0.5f) < 1f)
			{
				return c / 2f * t * t * t * t + b;
			}
			return -c / 2f * ((t -= 2f) * t * t * t - 2f) + b;
		}

		public static float QuartInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * t * t * t * t + b;
			}
			return -c / 2f * ((t -= 2f) * t * t * t - 2f) + b;
		}

		public static float QuartOutIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if (t < 0.5f)
			{
				return DTTween.QuartOut(t * 2f, b, c / 2f);
			}
			return DTTween.QuartIn(t * 2f - 1f, b + c / 2f, c / 2f);
		}

		public static float QuartOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return DTTween.QuartOut(t * 2f, b, c / 2f, d);
			}
			return DTTween.QuartIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public static float QuintOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return c * ((t -= 1f) * t * t * t * t + 1f) + b;
		}

		public static float QuintOut(float t, float b, float c, float d)
		{
			return c * ((t = t / d - 1f) * t * t * t * t + 1f) + b;
		}

		public static float QuintIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			return c * t * t * t * t * t + b;
		}

		public static float QuintIn(float t, float b, float c, float d)
		{
			return c * (t /= d) * t * t * t * t + b;
		}

		public static float QuintInOut(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if ((t /= 0.5f) < 1f)
			{
				return c / 2f * t * t * t * t * t + b;
			}
			return c / 2f * ((t -= 2f) * t * t * t * t + 2f) + b;
		}

		public static float QuintInOut(float t, float b, float c, float d)
		{
			if ((t /= d / 2f) < 1f)
			{
				return c / 2f * t * t * t * t * t + b;
			}
			return c / 2f * ((t -= 2f) * t * t * t * t + 2f) + b;
		}

		public static float QuintOutIn(float t, float b, float c)
		{
			t = Mathf.Clamp01(t);
			if (t < 0.5f)
			{
				return DTTween.QuintOut(t * 2f, b, c / 2f);
			}
			return DTTween.QuintIn(t * 2f - 1f, b + c / 2f, c / 2f);
		}

		public static float QuintOutIn(float t, float b, float c, float d)
		{
			if (t < d / 2f)
			{
				return DTTween.QuintOut(t * 2f, b, c / 2f, d);
			}
			return DTTween.QuintIn(t * 2f - d, b + c / 2f, c / 2f, d);
		}

		public enum EasingMethod
		{
			Linear,
			ExponentialIn,
			ExponentialOut,
			ExponentialInOut,
			ExponentialOutIn,
			CircularIn,
			CircularOut,
			CircularInOut,
			CircularOutIn,
			QuadraticIn,
			QuadraticOut,
			QuadraticInOut,
			QuadraticOutIn,
			SinusIn,
			SinusOut,
			SinusInOut,
			SinusOutIn,
			CubicIn,
			CubicOut,
			CubicInOut,
			CubicOutIn,
			QuarticIn,
			QuarticOut,
			QuarticInOut,
			QuarticOutIn,
			QuinticIn,
			QuinticOut,
			QuinticInOut,
			QuinticOutIn
		}
	}
}
