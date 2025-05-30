// dnSpy decompiler from Assembly-UnityScript.dll class: ScrollUV
using System;
using UnityEngine;

[Serializable]
public class ScrollUV : MonoBehaviour
{
	public ScrollUV()
	{
		this.scrollSpeed_X = 0.5f;
		this.scrollSpeed_Y = 0.5f;
	}

	public virtual void Update()
	{
		float x = Time.time * this.scrollSpeed_X;
		float y = Time.time * this.scrollSpeed_Y;
		this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, y);
	}

	public virtual void Main()
	{
	}

	public float scrollSpeed_X;

	public float scrollSpeed_Y;
}
