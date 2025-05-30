// dnSpy decompiler from Assembly-CSharp.dll class: ToggleBehaviourByTrigger
using System;
using UnityEngine;

public class ToggleBehaviourByTrigger : MonoBehaviour
{
	private void OnTriggerEnter()
	{
		if (this.UIElement)
		{
			this.UIElement.enabled = !this.UIElement.enabled;
		}
	}

	public Behaviour UIElement;
}
