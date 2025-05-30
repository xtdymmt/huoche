// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.PaintSpline
using System;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace FluffyUnderware.Curvy.Examples
{
	public class PaintSpline : MonoBehaviour
	{
		private void Awake()
		{
			this.mSpline = base.GetComponent<CurvySpline>();
		}

		private void OnGUI()
		{
			if (this.mSpline == null || !this.mSpline.IsInitialized || !this.Controller)
			{
				return;
			}
			Event current = Event.current;
			EventType type = current.type;
			if (type != EventType.MouseDrag)
			{
				if (type == EventType.MouseUp)
				{
					this.mResetSpline = true;
				}
			}
			else if (this.mResetSpline)
			{
				this.mSpline.Clear();
				this.addCP(current.mousePosition);
				this.Controller.gameObject.SetActive(true);
				this.Controller.AbsolutePosition = 0f;
				this.mLastControlPointPos = current.mousePosition;
				this.mResetSpline = false;
			}
			else
			{
				float magnitude = (current.mousePosition - this.mLastControlPointPos).magnitude;
				if (magnitude >= this.StepDistance)
				{
					this.mLastControlPointPos = current.mousePosition;
					this.addCP(current.mousePosition);
					if (this.Controller.PlayState != CurvyController.CurvyControllerState.Playing)
					{
						this.Controller.Play();
					}
				}
			}
		}

		private CurvySplineSegment addCP(Vector3 mousePos)
		{
			Vector3 globalPosition = Camera.main.ScreenToWorldPoint(mousePos);
			globalPosition.y *= -1f;
			globalPosition.z += 100f;
			CurvySplineSegment result = this.mSpline.InsertAfter(null, globalPosition, false);
			this.InfoText.text = "Control Points: " + this.mSpline.ControlPointCount.ToString();
			return result;
		}

		public float StepDistance = 30f;

		public SplineController Controller;

		public Text InfoText;

		private CurvySpline mSpline;

		private Vector2 mLastControlPointPos;

		private bool mResetSpline = true;
	}
}
