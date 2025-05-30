// dnSpy decompiler from Assembly-CSharp.dll class: AnimationHelper
using System;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
	public void Play(Animation animation)
	{
		animation.Play();
	}

	public void RewindThenPlay(Animation animation)
	{
		animation.Rewind();
		animation.Play();
	}
}
