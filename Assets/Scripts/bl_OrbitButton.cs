// dnSpy decompiler from Assembly-CSharp.dll class: bl_OrbitButton
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class bl_OrbitButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IEventSystemHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.CameraOrbit == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a camera orbit target");
			return;
		}
		if (this.m_Axi == bl_OrbitButton.Axys.Horizontal)
		{
			this.CameraOrbit.Horizontal = this.RotationAmount;
		}
		else if (this.m_Axi == bl_OrbitButton.Axys.Vertical)
		{
			this.CameraOrbit.Vertical = this.RotationAmount;
		}
		this.isMaitain = true;
	}

	private void Update()
	{
		if (this.Maintain && this.isMaitain)
		{
			if (this.m_Axi == bl_OrbitButton.Axys.Horizontal)
			{
				this.CameraOrbit.Horizontal = this.RotationAmount / 5f;
			}
			else if (this.m_Axi == bl_OrbitButton.Axys.Vertical)
			{
				this.CameraOrbit.Vertical = this.RotationAmount / 5f;
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.CameraOrbit == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a camera orbit target");
			return;
		}
		this.CameraOrbit.Interact = false;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.CameraOrbit == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a camera orbit target");
			return;
		}
		this.CameraOrbit.Interact = true;
		this.isMaitain = false;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (this.CameraOrbit == null)
		{
			UnityEngine.Debug.LogWarning("Please assign a camera orbit target");
			return;
		}
		this.CameraOrbit.Interact = true;
		this.isMaitain = false;
	}

	[SerializeField]
	private bl_CameraOrbit CameraOrbit;

	[SerializeField]
	private bl_OrbitButton.Axys m_Axi;

	[SerializeField]
	[Range(-15f, 15f)]
	private float RotationAmount = 5f;

	[SerializeField]
	private bool Maintain;

	private bool isMaitain;

	[Serializable]
	public enum Axys
	{
		Horizontal,
		Vertical
	}
}
