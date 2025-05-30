// dnSpy decompiler from Assembly-CSharp.dll class: UnityStandardAssets.Utility.SmoothFollow
using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{
		private void Start()
		{
			if (this.GameMasterScript == null)
			{
				this.GameMasterScript = (GameMaster)UnityEngine.Object.FindObjectOfType(typeof(GameMaster));
			}
			base.Invoke("DelayFindTarget_Call", 1f);
		}

		private void DelayFindTarget_Call()
		{
			this.target = this.GameMasterScript.TargetObj;
		}

		private void LateUpdate()
		{
			if (!this.target)
			{
				return;
			}
			float y = this.target.eulerAngles.y;
			float b = this.target.position.y + this.height;
			float num = base.transform.eulerAngles.y;
			float num2 = base.transform.position.y;
			num = Mathf.LerpAngle(num, y, this.rotationDamping * Time.deltaTime);
			num2 = Mathf.Lerp(num2, b, this.heightDamping * Time.deltaTime);
			Quaternion rotation = Quaternion.Euler(0f, num, 0f);
			base.transform.position = this.target.position;
			base.transform.position -= rotation * Vector3.forward * this.distance;
			base.transform.position = new Vector3(base.transform.position.x, num2, base.transform.position.z);
			base.transform.LookAt(this.target);
		}

		public Transform target;

		[SerializeField]
		private float distance = 10f;

		[SerializeField]
		private float height = 5f;

		[SerializeField]
		private float rotationDamping;

		[SerializeField]
		private float heightDamping;

		[Header("Passanger Train")]
		public Transform Target1;

		[Header("Cargo Train")]
		public Transform TargetCargoTrain;

		public GameMaster GameMasterScript;
	}
}
