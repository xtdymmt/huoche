// dnSpy decompiler from Assembly-CSharp.dll class: BuggyFollow
using System;
using UnityEngine;

public class BuggyFollow : MonoBehaviour
{
	private void Start()
	{
		this._followOffset = base.transform.position - this.leader.position;
	}

	private void LateUpdate()
	{
		Vector3 vector = this.leader.position + this._followOffset;
	}

	public Transform leader;

	public float followSharpness = 0.1f;

	private Vector3 _followOffset;
}
