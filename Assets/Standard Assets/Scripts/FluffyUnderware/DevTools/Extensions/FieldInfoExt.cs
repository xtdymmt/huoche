// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.Extensions.FieldInfoExt
using System;
using System.Reflection;

namespace FluffyUnderware.DevTools.Extensions
{
	public static class FieldInfoExt
	{
		public static T GetCustomAttribute<T>(this FieldInfo field) where T : Attribute
		{
			object[] customAttributes = field.GetCustomAttributes(typeof(T), true);
			return (customAttributes.Length <= 0) ? ((T)((object)null)) : ((T)((object)customAttributes[0]));
		}
	}
}
