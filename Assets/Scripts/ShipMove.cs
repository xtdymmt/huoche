// dnSpy decompiler from Assembly-CSharp.dll class: ShipMove
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipMove : MonoBehaviour
{
	private void OnEnable()
	{
		this.counter = 1;
		if (!this._me)
		{
			this._me = base.transform;
		}
		if (this.GameMasterScript == null)
		{
			this.GameMasterScript = UnityEngine.Object.FindObjectOfType<GameMaster>();
		}
	}

	private void FixedUpdate()
	{
		if (this.counter == 1)
		{
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target1.position, maxDistanceDelta);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target1.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
		}
		if (base.transform.position == this.Target1.position)
		{
			this.counter = 2;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "ShipStart")
		{
			this.ControlsButtons.SetActive(false);
			this.BridgeCam.SetActive(true);
			this.RaceBTN.value = 0f;
			this.BridgeAnimScriptt_1.enabled = true;
			this.BridgeAnimScriptt_2.enabled = true;
			UnityEngine.Object.Destroy(other.gameObject);
		}
	}

	public Transform Target1;

	public float speed;

	private int counter;

	private Transform _me;

	public BridgeAnimScriptt BridgeAnimScriptt_1;

	public BridgeAnimScriptt2 BridgeAnimScriptt_2;

	public GameObject BridgeCam;

	public GameMaster GameMasterScript;

	public GameObject ControlsButtons;

	public Scrollbar RaceBTN;
}
