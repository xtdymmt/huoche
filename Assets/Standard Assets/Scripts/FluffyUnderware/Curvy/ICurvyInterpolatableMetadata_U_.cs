// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.ICurvyInterpolatableMetadata<U>
using System;

namespace FluffyUnderware.Curvy
{
	public interface ICurvyInterpolatableMetadata<U> : ICurvyInterpolatableMetadata, ICurvyMetadata
	{
		U Interpolate(ICurvyMetadata b, float f);
	}
}
