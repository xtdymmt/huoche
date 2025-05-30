// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.DTTime
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public static class DTTime
	{
		public static double TimeSinceStartup
		{
			get
			{
				return (double)Time.timeSinceLevelLoad;
			}
		}

		public static float deltaTime
		{
			get
			{
				return (!Application.isPlaying) ? DTTime._EditorDeltaTime : Time.deltaTime;
			}
		}

		public static void InitializeEditorTime()
		{
			DTTime._EditorLastTime = Time.realtimeSinceStartup;
			DTTime._EditorDeltaTime = 0f;
		}

		public static void UpdateEditorTime()
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float editorDeltaTime = realtimeSinceStartup - DTTime._EditorLastTime;
			DTTime._EditorDeltaTime = editorDeltaTime;
			DTTime._EditorLastTime = realtimeSinceStartup;
		}

		private static float _EditorDeltaTime;

		private static float _EditorLastTime;
	}
}
