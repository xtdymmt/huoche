// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvyEventArgs
using System;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	public class CurvyEventArgs : EventArgs
	{
		public CurvyEventArgs(MonoBehaviour sender, object data)
		{
			this.Sender = sender;
			this.Data = data;
		}

		public readonly MonoBehaviour Sender;

		public readonly object Data;
	}
}
