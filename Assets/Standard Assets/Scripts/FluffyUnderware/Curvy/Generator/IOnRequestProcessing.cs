// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.IOnRequestProcessing
using System;

namespace FluffyUnderware.Curvy.Generator
{
	public interface IOnRequestProcessing
	{
		CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests);
	}
}
