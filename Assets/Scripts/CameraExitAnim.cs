// dnSpy decompiler from Assembly-CSharp.dll class: CameraExitAnim
using System;
using UnityEngine;

public class CameraExitAnim : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.Invoke("CamPos2", 4.5f);
		}
	}

	private void CamPos2()
	{
		this.CamAnimExit.transform.position = this.CamTarget2.transform.position;
		this.CamAnimExit.transform.rotation = this.CamTarget2.transform.rotation;
	}

	public GameObject CamAnimExit;

	public GameObject CamTarget2;
}
