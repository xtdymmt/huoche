// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.Curvy.Generator.CGDataInfoAttribute
using System;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public sealed class CGDataInfoAttribute : Attribute
	{
		public CGDataInfoAttribute(Color color)
		{
			this.Color = color;
		}

		public CGDataInfoAttribute(float r, float g, float b, float a = 1f)
		{
			this.Color = new Color(r, g, b, a);
		}

		public CGDataInfoAttribute(string htmlColor)
		{
			this.Color = htmlColor.ColorFromHtml();
		}

		public readonly Color Color;
	}
}
