// dnSpy decompiler from Assembly-CSharp.dll class: PathO
using System;
using System.Collections.Generic;
using UnityEngine;

public class PathO : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		Gizmos.color = this.RayColor;
		Transform[] componentsInChildren = base.transform.GetComponentsInChildren<Transform>();
		this.path = new List<Transform>();
		foreach (Transform transform in componentsInChildren)
		{
			if (transform != base.transform)
			{
				this.path.Add(transform);
			}
		}
		for (int j = 0; j < this.path.Count; j++)
		{
			Vector3 position = this.path[j].position;
			if (j > 0)
			{
				Vector3 position2 = this.path[j - 1].position;
				Gizmos.DrawLine(position2, position);
				Gizmos.DrawWireSphere(position, 0.9f);
			}
		}
	}

	public List<Transform> path;

	public Color RayColor = Color.white;
}
