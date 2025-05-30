// dnSpy decompiler from Assembly-CSharp.dll class: HeliFollow
using System;
using UnityEngine;

public class HeliFollow : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		base.gameObject.transform.position = new Vector3(this.Enemycar.transform.position.x, 10.517f, this.Enemycar.transform.position.z);
		base.transform.eulerAngles = new Vector3(0f, this.Enemycar.transform.eulerAngles.y, 0f);
	}

	public GameObject Enemycar;
}
