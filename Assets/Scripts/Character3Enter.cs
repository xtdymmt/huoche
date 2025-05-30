// dnSpy decompiler from Assembly-CSharp.dll class: Character3Enter
using System;
using UnityEngine;

public class Character3Enter : MonoBehaviour
{
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		if (!this._me)
		{
			this._me = base.transform;
		}
		this.counter = 1;
		this.CurrentState = Character3Enter.PlayAnim.IdleAnim;
	}

	private void LateUpdate()
	{
		switch (this.CurrentState)
		{
		case Character3Enter.PlayAnim.IdleAnim:
			this.anim.SetBool("Idle", true);
			this.anim.SetBool("Walk", false);
			this.anim.SetBool("ChangetoSit", false);
			this.anim.SetBool("Sit", false);
			break;
		case Character3Enter.PlayAnim.WalkAnim:
			this.anim.SetBool("Idle", false);
			this.anim.SetBool("Walk", true);
			this.anim.SetBool("ChangetoSit", false);
			this.anim.SetBool("Sit", false);
			break;
		case Character3Enter.PlayAnim.ChnagetoSitAnim:
			this.anim.SetBool("Idle", false);
			this.anim.SetBool("Walk", false);
			this.anim.SetBool("ChangetoSit", true);
			this.anim.SetBool("Sit", false);
			break;
		case Character3Enter.PlayAnim.SitAnim:
			this.anim.SetBool("Idle", false);
			this.anim.SetBool("Walk", false);
			this.anim.SetBool("ChangetoSit", false);
			this.anim.SetBool("Sit", true);
			break;
		}
		if (this.counter == 1)
		{
			this.CurrentState = Character3Enter.PlayAnim.WalkAnim;
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
			this.CurrentState = Character3Enter.PlayAnim.WalkAnim;
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
			this.CurrentState = Character3Enter.PlayAnim.WalkAnim;
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
			this.CurrentState = Character3Enter.PlayAnim.ChnagetoSitAnim;
			float maxDistanceDelta4 = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target4.position, maxDistanceDelta4);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.Target4.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
		}
		if (base.transform.position == this.Target4.position)
		{
		}
		if (this.counter == 5)
		{
			this.CurrentState = Character3Enter.PlayAnim.SitAnim;
			float maxDistanceDelta5 = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.Target5.position, maxDistanceDelta5);
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.SitLook.position - this._me.position), Time.deltaTime * 9f);
			this._me.eulerAngles = new Vector3(0f, this._me.eulerAngles.y, 0f);
		}
		if (base.transform.position == this.Target5.position)
		{
		}
		if (this.counter == 6)
		{
			this.CurrentState = Character3Enter.PlayAnim.SitAnim;
			this._me.rotation = Quaternion.Slerp(this._me.rotation, Quaternion.LookRotation(this.SitLook.position - this._me.position), Time.deltaTime * 9f);
		}
	}

	public Transform Target1;

	public Transform Target2;

	public Transform Target3;

	public Transform Target4;

	public Transform Target5;

	public Transform SitLook;

	public float speed;

	public int counter;

	private Transform _me;

	private Animator anim;

	private Character3Enter.PlayAnim CurrentState;

	private enum PlayAnim
	{
		IdleAnim,
		WalkAnim,
		ChnagetoSitAnim,
		SitAnim
	}
}
