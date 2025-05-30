// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Controllers.CurvyControllerEventArgs
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Controllers
{
	public class CurvyControllerEventArgs : CurvyEventArgs
	{
		public CurvyControllerEventArgs(MonoBehaviour sender, CurvyController controller) : base(sender, null)
		{
			this.Controller = controller;
		}

		public readonly CurvyController Controller;
	}
}
