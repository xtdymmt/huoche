// dnSpy decompiler from Assembly-CSharp.dll class: DG.Tweening.DOTweenAnimationExtensions
using System;
using UnityEngine;

namespace DG.Tweening
{
	public static class DOTweenAnimationExtensions
	{
		public static bool IsSameOrSubclassOf<T>(this Component t)
		{
			return t is T;
		}
	}
}
