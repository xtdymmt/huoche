// dnSpy decompiler from Assembly-CSharp.dll class: Done_BGScroller
using System;
using UnityEngine;

public class Done_BGScroller : MonoBehaviour
{
	private void Start()
	{
		this.startPosition = base.transform.position;
	}

	private void Update()
	{
		float d = Mathf.Repeat(Time.time * this.scrollSpeed, this.tileWidth);
		base.transform.position = this.startPosition + Vector3.left * d;
	}

	public float scrollSpeed;

	public float tileWidth;

	private Vector3 startPosition;
}
