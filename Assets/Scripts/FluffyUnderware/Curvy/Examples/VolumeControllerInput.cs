// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.VolumeControllerInput
using System;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;

namespace FluffyUnderware.Curvy.Examples
{
	public class VolumeControllerInput : MonoBehaviour
	{
		private void Awake()
		{
			if (!this.volumeController)
			{
				this.volumeController = base.GetComponent<VolumeController>();
			}
		}

		private void Start()
		{
			if (this.volumeController.IsReady)
			{
				this.ResetController();
			}
			else
			{
				this.volumeController.OnInitialized.AddListener(delegate(CurvyController arg0)
				{
					this.ResetController();
				});
			}
		}

		private void ResetController()
		{
			this.volumeController.Speed = 0f;
			this.volumeController.RelativePosition = 0f;
			this.volumeController.CrossRelativePosition = 0f;
		}

		private void Update()
		{
			if (this.volumeController && !this.mGameOver)
			{
				if (this.volumeController.PlayState != CurvyController.CurvyControllerState.Playing)
				{
					this.volumeController.Play();
				}
				Vector2 vector = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
				Vector2 normalized = vector.normalized;
				float value = this.volumeController.Speed + normalized.y * Time.deltaTime * Mathf.Lerp(this.accelerationBackward, this.accelerationForward, (normalized.y + 1f) / 2f);
				this.volumeController.Speed = Mathf.Clamp(value, 0f, this.maxSpeed);
				this.volumeController.CrossRelativePosition += this.AngularVelocity * Mathf.Clamp(this.volumeController.Speed / 10f, 0.2f, 1f) * normalized.x * Time.deltaTime;
				if (this.rotatedTransform)
				{
					float y = Mathf.Lerp(-90f, 90f, (normalized.x + 1f) / 2f);
					this.rotatedTransform.localRotation = Quaternion.Euler(0f, y, 0f);
				}
			}
		}

		public void OnCollisionEnter(Collision collision)
		{
		}

		public void OnTriggerEnter(Collider other)
		{
			if (!this.mGameOver)
			{
				this.explosionEmitter.Emit(200);
				this.volumeController.Pause();
				this.mGameOver = true;
				base.Invoke("StartOver", 1f);
			}
		}

		private void StartOver()
		{
			this.ResetController();
			this.mGameOver = false;
		}

		public float AngularVelocity = 0.2f;

		public ParticleSystem explosionEmitter;

		public VolumeController volumeController;

		public Transform rotatedTransform;

		public float maxSpeed = 40f;

		public float accelerationForward = 20f;

		public float accelerationBackward = 40f;

		private bool mGameOver;
	}
}
