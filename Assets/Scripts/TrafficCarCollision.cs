// dnSpy decompiler from Assembly-CSharp.dll class: TrafficCarCollision
using System;
using UnityEngine;

public class TrafficCarCollision : MonoBehaviour
{
	private void Start()
	{
		TrafficCarCollision.MissionFailedBool = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			for (int i = 0; i < this.AICarScriptArray.Length; i++)
			{
				this.AICarScriptArray[i].enabled = false;
			}
			base.Invoke("DelayTrafficAccident", 0.2f);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			for (int i = 0; i < this.AICarScriptArray.Length; i++)
			{
				this.AICarScriptArray[i].enabled = false;
			}
			base.Invoke("DelayTrafficAccident", 0.2f);
		}
	}

	private void DelayTrafficAccident()
	{
		base.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 1000f, 1000f));
		GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.Metal_effectPrefab, this.MetalEffectPos.transform.position, this.MetalEffectPos.transform.rotation);
		UnityEngine.Object.Destroy(obj, 0.45f);
		base.GetComponent<AudioSource>().PlayOneShot(this.MetalSound, 0.5f);
	}

	private void LevelFailDelay()
	{
		TrafficCarCollision.MissionFailedBool = true;
	}

	public AICarScript[] AICarScriptArray;

	public GameObject Metal_effectPrefab;

	public GameObject MetalEffectPos;

	public AudioClip MetalSound;

	public static bool MissionFailedBool;

	private bool CarCollideBool;
}
