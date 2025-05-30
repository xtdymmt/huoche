// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.IOnRequestPath
using System;

namespace FluffyUnderware.Curvy.Generator
{
	public interface IOnRequestPath : IOnRequestProcessing
	{
		float PathLength { get; }

		bool PathIsClosed { get; }
	}
}
