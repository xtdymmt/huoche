// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ContourVertex
using System;

namespace FluffyUnderware.Curvy.ThirdParty.LibTessDotNet
{
	public struct ContourVertex
	{
		public override string ToString()
		{
			return string.Format("{0}, {1}", this.Position, this.Data);
		}

		public Vec3 Position;

		public object Data;
	}
}
