// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.ICurvyInterpolatableMetadata
using System;

namespace FluffyUnderware.Curvy
{
	public interface ICurvyInterpolatableMetadata : ICurvyMetadata
	{
		object Value { get; }

		object InterpolateObject(ICurvyMetadata b, float f);
	}
}
