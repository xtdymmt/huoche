// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGDataRequestMetaCGOptions
using System;

namespace FluffyUnderware.Curvy.Generator
{
	public class CGDataRequestMetaCGOptions : CGDataRequestParameter
	{
		public CGDataRequestMetaCGOptions(bool checkEdges, bool checkMaterials, bool includeCP, bool extendedUV)
		{
			this.CheckHardEdges = checkEdges;
			this.CheckMaterialID = checkMaterials;
			this.IncludeControlPoints = includeCP;
			this.CheckExtendedUV = extendedUV;
		}

		public override bool Equals(object obj)
		{
			CGDataRequestMetaCGOptions cgdataRequestMetaCGOptions = obj as CGDataRequestMetaCGOptions;
			return cgdataRequestMetaCGOptions != null && (this.CheckHardEdges == cgdataRequestMetaCGOptions.CheckHardEdges && this.CheckMaterialID == cgdataRequestMetaCGOptions.CheckMaterialID && this.IncludeControlPoints == cgdataRequestMetaCGOptions.IncludeControlPoints) && this.CheckExtendedUV == cgdataRequestMetaCGOptions.CheckExtendedUV;
		}

		public override int GetHashCode()
		{
			return new
			{
				A = this.CheckHardEdges,
				B = this.CheckMaterialID,
				C = this.IncludeControlPoints,
				D = this.CheckExtendedUV
			}.GetHashCode();
		}

		public bool CheckHardEdges;

		public bool CheckMaterialID;

		public bool IncludeControlPoints;

		public bool CheckExtendedUV;
	}
}
