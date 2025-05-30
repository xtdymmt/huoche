// dnSpy decompiler from Assembly-CSharp.dll class: PassangerBuggy1Move
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PassangerBuggy1Move : MonoBehaviour
{
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.destination = this.agent.destination;
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

	private void Update()
	{
		if (this.i != this.target.Count && Vector3.Distance(this.destination, this.target[this.i].position) > 1f)
		{
			this.destination = this.target[this.i].position;
			this.agent.destination = this.destination;
		}
		if (this.i >= this.target.Count)
		{
			this.i = 0;
		}
		if (this.agent.speed > 0f)
		{
			this.Wheel_RR.gameObject.transform.Rotate(Time.deltaTime * 500f, 0f, 0f);
			this.Wheel_RL.gameObject.transform.Rotate(Time.deltaTime * 500f, 0f, 0f);
			this.Wheel_FR.gameObject.transform.Rotate(Time.deltaTime * 500f, 0f, 0f);
			this.Wheel_FL.gameObject.transform.Rotate(Time.deltaTime * 500f, 0f, 0f);
		}
		else
		{
			this.Wheel_RR.gameObject.transform.Rotate(0f, 0f, 0f);
			this.Wheel_RL.gameObject.transform.Rotate(0f, 0f, 0f);
			this.Wheel_FR.gameObject.transform.Rotate(0f, 0f, 0f);
			this.Wheel_FL.gameObject.transform.Rotate(0f, 0f, 0f);
		}
	}

	private void CarSpeed()
	{
		this.agent.speed = 12f;
	}

	public Transform PathGroup;

	public List<Transform> target;

	private Vector3 destination;

	private NavMeshAgent agent;

	public int i;

	public GameObject Wheel_RR;

	public GameObject Wheel_RL;

	public GameObject Wheel_FR;

	public GameObject Wheel_FL;
}
