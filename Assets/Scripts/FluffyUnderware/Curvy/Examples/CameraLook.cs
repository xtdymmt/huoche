// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.CameraLook
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	public class CameraLook : MonoBehaviour
	{
		protected void Update()
		{
			if (Time.timeScale < 1.401298E-45f)
			{
				return;
			}
			float axis = UnityEngine.Input.GetAxis("Mouse X");
			float num = -Input.GetAxis("Mouse Y");
			base.transform.Rotate(num * this.m_TurnSpeed, 0f, 0f, Space.Self);
			base.transform.Rotate(0f, axis * this.m_TurnSpeed, 0f, Space.World);
		}

		[Range(0f, 10f)]
		[SerializeField]
		private float m_TurnSpeed = 1.5f;
	}
}
