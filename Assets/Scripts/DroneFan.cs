// dnSpy decompiler from Assembly-CSharp.dll class: DroneFan
using System;
using UnityEngine;

public class DroneFan : MonoBehaviour
{
	private void Update()
	{
		base.transform.Rotate(Vector3.forward * Time.deltaTime * 2000f);
	}
}
