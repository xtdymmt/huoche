// dnSpy decompiler from Assembly-CSharp.dll class: BridgeCamAnim
using System;
using UnityEngine;

public class BridgeCamAnim : MonoBehaviour
{
	private void OnEnable()
	{
		base.gameObject.transform.position = this.StaticCam.transform.position;
		base.gameObject.transform.rotation = this.StaticCam.transform.rotation;
		this.counter = 1;
		if (!this._me)
		{
			this._me = base.transform;
		}
	}

	private void LateUpdate()
	{
		if (this.counter == 1)
		{
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target1.position, maxDistanceDelta);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.LookTarget.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
		}
		if (base.transform.position == this.Target1.position)
		{
			this.counter = 2;
		}
	}

	public Transform Target1;

	public float speed;

	private int counter;

	private Transform _me;

	public Transform LookTarget;

	public GameObject StaticCam;
}
