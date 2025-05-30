// dnSpy decompiler from Assembly-CSharp.dll class: Perspective2DSortMode
using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class Perspective2DSortMode : MonoBehaviour
{
	private void Awake()
	{
		base.GetComponent<Camera>().transparencySortMode = TransparencySortMode.Orthographic;
	}
}
