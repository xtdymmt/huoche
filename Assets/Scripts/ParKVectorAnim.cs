// dnSpy decompiler from Assembly-CSharp.dll class: ParKVectorAnim
using System;
using UnityEngine;

public class ParKVectorAnim : MonoBehaviour
{
	private void OnEnable()
	{
		this.counter = 1;
	}

	private void FixedUpdate()
	{
		if (this.counter == 1)
		{
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target1.position, maxDistanceDelta);
		}
		if (base.transform.position == this.Target1.position)
		{
			this.counter = 2;
		}
		if (this.counter == 2)
		{
			float maxDistanceDelta2 = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target2.position, maxDistanceDelta2);
		}
		if (base.transform.position == this.Target2.position)
		{
			this.counter = 1;
		}
	}

	public Transform Target1;

	public Transform Target2;

	public float speed;

	private int counter = 1;
}
