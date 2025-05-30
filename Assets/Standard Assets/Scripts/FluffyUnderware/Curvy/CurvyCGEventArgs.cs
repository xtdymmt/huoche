// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.CurvyCGEventArgs
using System;
using FluffyUnderware.Curvy.Generator;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	public class CurvyCGEventArgs : EventArgs
	{
		public CurvyCGEventArgs(CGModule module)
		{
			this.Sender = module;
			this.Generator = module.Generator;
			this.Module = module;
		}

		public CurvyCGEventArgs(CurvyGenerator generator, CGModule module)
		{
			this.Sender = generator;
			this.Generator = generator;
			this.Module = module;
		}

		public readonly MonoBehaviour Sender;

		public readonly CurvyGenerator Generator;

		public readonly CGModule Module;
	}
}
