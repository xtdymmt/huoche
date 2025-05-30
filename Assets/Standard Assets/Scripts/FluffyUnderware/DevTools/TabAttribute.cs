// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.TabAttribute
using System;

namespace FluffyUnderware.DevTools
{
	public class TabAttribute : GroupAttribute
	{
		public TabAttribute(string pathAndName) : base(string.Empty)
		{
			base.TypeSort = 10;
			string path;
			TabAttribute.split(pathAndName, out path, out this.TabBarName, out this.TabName);
			base.Path = path;
		}

		private static bool split(string pathAndName, out string path, out string tabBar, out string tabname)
		{
			string[] array = pathAndName.Split(new char[]
			{
				'/'
			});
			path = string.Empty;
			tabBar = string.Empty;
			tabname = pathAndName;
			if (array.Length == 0)
			{
				return false;
			}
			if (array.Length == 1)
			{
				tabname = array[0];
				tabBar = "Default";
				return true;
			}
			tabname = array[array.Length - 1];
			tabBar = array[array.Length - 2];
			path = string.Join("/", array, 0, array.Length - 2);
			return true;
		}

		public readonly string TabName;

		public readonly string TabBarName;
	}
}
