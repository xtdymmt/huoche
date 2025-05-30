// dnSpy decompiler from Assembly-CSharp.dll class: FollowNav
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowNav : MonoBehaviour
{
	private void Start()
	{
		if (!this._me)
		{
			this._me = base.transform;
		}
		this.GetPath();
		this.agent = base.GetComponent<NavMeshAgent>();
		this.destination = this.agent.destination;
		this.agent.speed = 0f;
		this.agent.autoBraking = false;
	}

	private void GetPath()
	{
		Transform[] componentsInChildren = this.PathGroup.GetComponentsInChildren<Transform>();
		this.target = new List<Transform>();
		foreach (Transform transform in componentsInChildren)
		{
			if (transform != this.PathGroup)
			{
				this.target.Add(transform);
			}
		}
	}

	private void LateUpdate()
	{
		if (this.i != this.target.Count && Vector3.Distance(this.destination, this.target[this.i].position) > 1f)
		{
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.target[this.i].position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
			this.destination = this.target[this.i].position;
			this.agent.destination = this.destination;
		}
		if (this.i >= this.target.Count)
		{
			this.i = 0;
			this.agent.speed = 0f;
		}
	}

	public Transform PathGroup;

	public List<Transform> target;

	private Vector3 destination;

	public NavMeshAgent agent;

	public GameObject[] Wheels;

	public int i;

	private Transform _me;
}
