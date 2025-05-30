// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Utils.CurvyUtility
using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Utils
{
	public static class CurvyUtility
	{
		public static float ClampTF(float tf, CurvyClamping clamping)
		{
			switch (clamping)
			{
			case CurvyClamping.Clamp:
				return Mathf.Clamp01(tf);
			case CurvyClamping.Loop:
				return Mathf.Repeat(tf, 1f);
			case CurvyClamping.PingPong:
				return Mathf.PingPong(tf, 1f);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public static float ClampValue(float tf, CurvyClamping clamping, float minTF, float maxTF)
		{
			switch (clamping)
			{
			case CurvyClamping.Clamp:
				return Mathf.Clamp(tf, minTF, maxTF);
			case CurvyClamping.Loop:
			{
				float t = DTMath.MapValue(0f, 1f, tf, minTF, maxTF);
				return DTMath.MapValue(minTF, maxTF, Mathf.Repeat(t, 1f), 0f, 1f);
			}
			case CurvyClamping.PingPong:
			{
				float t2 = DTMath.MapValue(0f, 1f, tf, minTF, maxTF);
				return DTMath.MapValue(minTF, maxTF, Mathf.PingPong(t2, 1f), 0f, 1f);
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public static float ClampTF(float tf, ref int dir, CurvyClamping clamping)
		{
			if (clamping == CurvyClamping.Loop)
			{
				return Mathf.Repeat(tf, 1f);
			}
			if (clamping != CurvyClamping.PingPong)
			{
				return Mathf.Clamp01(tf);
			}
			if (Mathf.FloorToInt(tf) % 2 != 0)
			{
				dir *= -1;
			}
			return Mathf.PingPong(tf, 1f);
		}

		public static float ClampTF(float tf, ref int dir, CurvyClamping clamping, float minTF, float maxTF)
		{
			minTF = Mathf.Clamp01(minTF);
			maxTF = Mathf.Clamp(maxTF, minTF, 1f);
			if (clamping == CurvyClamping.Loop)
			{
				return minTF + Mathf.Repeat(tf, maxTF - minTF);
			}
			if (clamping != CurvyClamping.PingPong)
			{
				return Mathf.Clamp(tf, minTF, maxTF);
			}
			if (Mathf.FloorToInt(tf / (maxTF - minTF)) % 2 != 0)
			{
				dir *= -1;
			}
			return minTF + Mathf.PingPong(tf, maxTF - minTF);
		}

		public static float ClampDistance(float distance, CurvyClamping clamping, float length)
		{
			if (length == 0f)
			{
				return 0f;
			}
			if (clamping == CurvyClamping.Loop)
			{
				return Mathf.Repeat(distance, length);
			}
			if (clamping != CurvyClamping.PingPong)
			{
				return Mathf.Clamp(distance, 0f, length);
			}
			return Mathf.PingPong(distance, length);
		}

		public static float ClampDistance(float distance, CurvyClamping clamping, float length, float min, float max)
		{
			if (length == 0f)
			{
				return 0f;
			}
			min = Mathf.Clamp(min, 0f, length);
			max = Mathf.Clamp(max, min, length);
			if (clamping == CurvyClamping.Loop)
			{
				return min + Mathf.Repeat(distance, max - min);
			}
			if (clamping != CurvyClamping.PingPong)
			{
				return Mathf.Clamp(distance, min, max);
			}
			return min + Mathf.PingPong(distance, max - min);
		}

		public static float ClampDistance(float distance, ref int dir, CurvyClamping clamping, float length)
		{
			if (length == 0f)
			{
				return 0f;
			}
			if (clamping == CurvyClamping.Loop)
			{
				return Mathf.Repeat(distance, length);
			}
			if (clamping != CurvyClamping.PingPong)
			{
				return Mathf.Clamp(distance, 0f, length);
			}
			if (Mathf.FloorToInt(distance / length) % 2 != 0)
			{
				dir *= -1;
			}
			return Mathf.PingPong(distance, length);
		}

		public static float ClampDistance(float distance, ref int dir, CurvyClamping clamping, float length, float min, float max)
		{
			if (length == 0f)
			{
				return 0f;
			}
			min = Mathf.Clamp(min, 0f, length);
			max = Mathf.Clamp(max, min, length);
			if (clamping == CurvyClamping.Loop)
			{
				return min + Mathf.Repeat(distance, max - min);
			}
			if (clamping != CurvyClamping.PingPong)
			{
				return Mathf.Clamp(distance, min, max);
			}
			if (Mathf.FloorToInt(distance / (max - min)) % 2 != 0)
			{
				dir *= -1;
			}
			return min + Mathf.PingPong(distance, max - min);
		}

		public static Material GetDefaultMaterial()
		{
			Material material = Resources.Load("CurvyDefaultMaterial") as Material;
			if (material == null)
			{
				material = new Material(Shader.Find("Diffuse"));
			}
			return material;
		}

		public static bool Approximately(this float x, float y)
		{
			float num = Mathf.Epsilon * 8f;
			bool result;
			if (Math.Abs(x) < num)
			{
				result = (Math.Abs(y) < 1E-06f);
			}
			else if (Math.Abs(y) < num)
			{
				result = (Math.Abs(x) < 1E-06f);
			}
			else
			{
				result = Mathf.Approximately(x, y);
			}
			return result;
		}
	}
}
