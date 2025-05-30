// dnSpy decompiler from Assembly-CSharp.dll class: AnimatedProjector
using System;
using UnityEngine;

public class AnimatedProjector : MonoBehaviour
{
	private void Start()
	{
		this.projector = base.GetComponent<Projector>();
		this.NextFrame();
		base.InvokeRepeating("NextFrame", 1f / this.fps, 1f / this.fps);
	}

	private void NextFrame()
	{
		this.projector.material.SetTexture("_ShadowTex", this.frames[this.frameIndex]);
		this.frameIndex = (this.frameIndex + 1) % this.frames.Length;
	}

	public float fps = 30f;

	public Texture2D[] frames;

	private int frameIndex;

	private Projector projector;
}
