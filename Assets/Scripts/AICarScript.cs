// dnSpy decompiler from Assembly-CSharp.dll class: AICarScript
using System;
using System.Collections.Generic;
using UnityEngine;

public class AICarScript : MonoBehaviour
{
	private void Awake()
	{
		base.GetComponent<Rigidbody>().centerOfMass = this.centerOfmass;
	}

	private void Start()
	{
		this.requiredmaxtorque = 1500f;
		this.Increase = false;
		this.maxTorque = this.maxTorque;
		this.GetPath();
	}

	private void GetPath()
	{
		Transform[] componentsInChildren = this.PathGroup.GetComponentsInChildren<Transform>();
		this.path = new List<Transform>();
		foreach (Transform transform in componentsInChildren)
		{
			if (transform != this.PathGroup)
			{
				this.path.Add(transform);
			}
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Increase")
		{
			this.Increase = !this.Increase;
			if (this.Increase)
			{
				this.requiredmaxtorque = 2000f;
			}
			else if (!this.Increase)
			{
				this.requiredmaxtorque = 500f;
			}
		}
	}

	private void LateUpdate()
	{
		if (this.flag == 0)
		{
			this.GetSteer();
		}
		this.Move();
		this.Sensors();
		this.SetTorque();
	}

	private void SetTorque()
	{
		if (this.currentSpeed <= this.topSpeed)
		{
			this.maxTorque = this.requiredmaxtorque;
		}
		else if (this.currentSpeed > this.topSpeed)
		{
			this.maxTorque = 0f;
			this.wheelRL.brakeTorque = 200f;
			this.wheelRR.brakeTorque = 200f;
			this.wheelFL.brakeTorque = 200f;
			this.wheelFR.brakeTorque = 200f;
		}
	}

	private void GetSteer()
	{
		Vector3 vector = base.transform.InverseTransformPoint(new Vector3(this.path[this.currentPathobj].position.x, base.transform.position.y, this.path[this.currentPathobj].position.z));
		float steerAngle = this.maxSteer * (vector.x / vector.magnitude);
		this.wheelFL.steerAngle = steerAngle;
		this.wheelFR.steerAngle = steerAngle;
		if (vector.magnitude <= this.distFrompath)
		{
			this.currentPathobj++;
			if (this.currentPathobj >= this.path.Count)
			{
				this.currentPathobj = 0;
			}
		}
	}

	private void Move()
	{
		this.currentSpeed = 6f * this.wheelRL.radius * this.wheelRL.rpm * 60f / 2000f;
		this.currentSpeed = Mathf.Round(this.currentSpeed);
		if (this.currentSpeed <= this.topSpeed && !this.inSector)
		{
			if (!this.reversing)
			{
				this.wheelRL.motorTorque = this.maxTorque;
				this.wheelRR.motorTorque = this.maxTorque;
			}
			else
			{
				this.wheelRL.motorTorque = -this.maxTorque;
				this.wheelRR.motorTorque = -this.maxTorque;
			}
			this.wheelRL.brakeTorque = 0f;
			this.wheelRR.brakeTorque = 0f;
			this.wheelFL.brakeTorque = 0f;
			this.wheelFR.brakeTorque = 0f;
		}
		else if (this.currentSpeed > this.topSpeed)
		{
			this.wheelRL.motorTorque = 0f;
			this.wheelRR.motorTorque = 0f;
			this.wheelRL.brakeTorque = this.decellerationSpeed2;
			this.wheelRR.brakeTorque = this.decellerationSpeed2;
		}
	}

	private void EngineSound()
	{
		this.i = 0;
		while (this.i < this.gearRatio.Length)
		{
			if ((float)this.gearRatio[this.i] > this.currentSpeed)
			{
				break;
			}
			this.i++;
		}
		float num;
		if (this.i == 0)
		{
			num = 0f;
		}
		else
		{
			num = (float)this.gearRatio[this.i - 1];
		}
		float num2 = (float)this.gearRatio[this.i];
		float pitch = (Mathf.Abs(this.currentSpeed) - num) / (num2 - num) + 1f;
		base.GetComponent<AudioSource>().pitch = pitch;
	}

	private void Sensors()
	{
		Vector3 vector = base.transform.position;
		vector += base.transform.forward * this.frontSensorstartpoint;
		vector.y += this.sensorPlace;
		RaycastHit raycastHit;
		if (Physics.Raycast(vector, base.transform.forward, out raycastHit, this.sensorLength))
		{
			if (raycastHit.transform.tag == "Traffic")
			{
				this.wheelRL.brakeTorque = this.decellerationSpeed;
				this.wheelRR.brakeTorque = this.decellerationSpeed;
				this.wheelFL.brakeTorque = this.decellerationSpeed;
				this.wheelFR.brakeTorque = this.decellerationSpeed;
			}
			if (raycastHit.transform.tag == "StopTraffic")
			{
				this.wheelRL.brakeTorque = this.decellerationSpeed;
				this.wheelRR.brakeTorque = this.decellerationSpeed;
				this.wheelFL.brakeTorque = this.decellerationSpeed;
				this.wheelFR.brakeTorque = this.decellerationSpeed;
			}
		}
		vector += base.transform.right * this.frontSensorSideDist;
		if (Physics.Raycast(vector, base.transform.forward, out raycastHit, this.sensorLength) && (raycastHit.transform.tag == "Traffic" || raycastHit.transform.tag == "StopTraffic"))
		{
			this.wheelRL.brakeTorque = this.decellerationSpeed;
			this.wheelRR.brakeTorque = this.decellerationSpeed;
			this.wheelFL.brakeTorque = this.decellerationSpeed;
			this.wheelFR.brakeTorque = this.decellerationSpeed;
		}
		vector = base.transform.position;
		vector += base.transform.forward * this.frontSensorstartpoint;
		vector.y += this.sensorPlace;
		vector -= base.transform.right * this.frontSensorSideDist;
		if (Physics.Raycast(vector, base.transform.forward, out raycastHit, this.sensorLength) && (raycastHit.transform.tag == "Traffic" || raycastHit.transform.tag == "StopTraffic"))
		{
			this.wheelRL.brakeTorque = this.decellerationSpeed;
			this.wheelRR.brakeTorque = this.decellerationSpeed;
			this.wheelFL.brakeTorque = this.decellerationSpeed;
			this.wheelFR.brakeTorque = this.decellerationSpeed;
		}
	}

	public List<Transform> path;

	public Transform PathGroup;

	public float maxSteer = 20f;

	public int currentPathobj;

	public float distFrompath;

	public float maxTorque;

	public float currentSpeed;

	public float topSpeed;

	public float decellerationSpeed;

	public float decellerationSpeed2;

	public int[] gearRatio;

	public int i;

	private float mySidewayFriction;

	private float myForwardFriction;

	private float slipSidewayFriction;

	private float slipForwardFriction;

	public bool isBreaking;

	public bool inSector;

	public float sensorPlace;

	public float sensorLength = 5f;

	public float frontSensorstartpoint = 4.19f;

	public float frontSensorSideDist = 1.28f;

	public float frontSensorAngle = 30f;

	public float sideWaysensorlength = 5f;

	public float avoidSpeed = 12f;

	private int flag;

	public bool reversing;

	public WheelCollider wheelFL;

	public WheelCollider wheelFR;

	public WheelCollider wheelRL;

	public WheelCollider wheelRR;

	public Vector3 centerOfmass;

	private bool Increase;

	private float requiredmaxtorque;
}
