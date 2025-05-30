// dnSpy decompiler from Assembly-CSharp.dll class: ShipStartScript
using System;
using UnityEngine;

public class ShipStartScript : MonoBehaviour
{
	private void OnDisable()
	{
		if (this.ShipMoveScript != null)
		{
			this.ShipMoveScript.enabled = true;
		}
	}

	public ShipMove ShipMoveScript;
}
