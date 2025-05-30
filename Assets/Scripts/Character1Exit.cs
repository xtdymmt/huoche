// dnSpy decompiler from Assembly-CSharp.dll class: Character1Exit
using System;
using UnityEngine;

public class Character1Exit : MonoBehaviour
{
	private void Awake()
	{
		this.anim = base.GetComponent<Animator>();
		if (!this._me)
		{
			this._me = base.transform;
		}
		this.counter = 0;
		this.CurrentState = Character1Exit.PlayAnim.SitAnim;
	}

	private void Start()
	{
		base.Invoke("ChangeStateExitToStand", 0.8f);
	}

	private void ChangeStateExitToStand()
	{
		this.CurrentState = Character1Exit.PlayAnim.ExittoStandAnim;
		base.Invoke("ChangeStateWalk", 0.8f);
	}

	private void ChangeStateWalk()
	{
		this.counter = 1;
	}

	private void LateUpdate()
	{
		switch (this.CurrentState)
		{
		case Character1Exit.PlayAnim.SitAnim:
			this.anim.SetBool("Idle", false);
			this.anim.SetBool("Walk", false);
			this.anim.SetBool("ExitToStand", false);
			this.anim.SetBool("Sit", true);
			break;
		case Character1Exit.PlayAnim.ExittoStandAnim:
			this.anim.SetBool("Idle", false);
			this.anim.SetBool("Walk", false);
			this.anim.SetBool("ExitToStand", true);
			this.anim.SetBool("Sit", false);
			break;
		case Character1Exit.PlayAnim.WalkAnim:
			this.anim.SetBool("Idle", false);
			this.anim.SetBool("Walk", true);
			this.anim.SetBool("ExitToStand", false);
			this.anim.SetBool("Sit", false);
			break;
		case Character1Exit.PlayAnim.IdleAnim:
			this.anim.SetBool("Idle", true);
			this.anim.SetBool("Walk", false);
			this.anim.SetBool("ExitToStand", false);
			this.anim.SetBool("Sit", false);
			break;
		}
		if (this.counter == 1)
		{
			this.CurrentState = Character1Exit.PlayAnim.WalkAnim;
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target1.position, maxDistanceDelta);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target1.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
		}
		if (base.transform.position == this.Target1.position)
		{
		}
		if (this.counter == 2)
		{
			this.CurrentState = Character1Exit.PlayAnim.WalkAnim;
			float maxDistanceDelta2 = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target2.position, maxDistanceDelta2);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target2.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
		}
		if (base.transform.position == this.Target2.position)
		{
		}
		if (this.counter == 3)
		{
			this.CurrentState = Character1Exit.PlayAnim.WalkAnim;
			float maxDistanceDelta3 = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target3.position, maxDistanceDelta3);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target3.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
		}
		if (base.transform.position == this.Target3.position)
		{
		}
		if (this.counter == 4)
		{
			this.CurrentState = Character1Exit.PlayAnim.IdleAnim;
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target3.position - this._me.position), Time.deltaTime * 9f);
		}
	}

	public Transform Target1;

	public Transform Target2;

	public Transform Target3;

	public float speed;

	public int counter;

	private Transform _me;

	private Animator anim;

	private Character1Exit.PlayAnim CurrentState;

	private enum PlayAnim
	{
		SitAnim,
		ExittoStandAnim,
		WalkAnim,
		IdleAnim
	}
}
