// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.DTObjectDump
using System;
using System.Collections;
using System.Reflection;
using System.Text;
using FluffyUnderware.DevTools.Extensions;

namespace FluffyUnderware.DevTools
{
	public class DTObjectDump
	{
		public DTObjectDump(object o, int indent = 0)
		{
			this.mIndent = new string(' ', indent);
			this.mSB = new StringBuilder();
			this.mObject = o;
			Type type = o.GetType();
			FieldInfo[] allFields = type.GetAllFields(false, true);
			if (allFields.Length > 0)
			{
				this.AppendHeader("Fields");
			}
			foreach (FieldInfo info in allFields)
			{
				this.AppendMember(info);
			}
			PropertyInfo[] allProperties = type.GetAllProperties(false, true);
			if (allProperties.Length > 0)
			{
				this.AppendHeader("Properties");
			}
			foreach (PropertyInfo info2 in allProperties)
			{
				this.AppendMember(info2);
			}
		}

		public override string ToString()
		{
			return this.mSB.ToString();
		}

		private void AppendHeader(string name)
		{
			this.mSB.AppendLine(this.mIndent + "<b>---===| " + name + " |===---</b>\n");
		}

		private void AppendMember(MemberInfo info)
		{
			FieldInfo fieldInfo = info as FieldInfo;
			Type type;
			string arg;
			object value;
			if (fieldInfo != null)
			{
				type = fieldInfo.FieldType;
				arg = type.Name;
				value = fieldInfo.GetValue(this.mObject);
			}
			else
			{
				PropertyInfo propertyInfo = info as PropertyInfo;
				type = propertyInfo.PropertyType;
				arg = type.Name;
				value = propertyInfo.GetValue(this.mObject, null);
			}
			if (value != null)
			{
				if (typeof(IEnumerable).IsAssignableFrom(type))
				{
					string text = this.mIndent;
					int num = 0;
					IEnumerable enumerable = value as IEnumerable;
					if (enumerable != null)
					{
						if (type.GetEnumerableType().BaseType == typeof(ValueType))
						{
							IEnumerator enumerator = enumerable.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									text += string.Format("<b>{0}</b>: {1} ", num++.ToString(), obj.ToString());
								}
							}
							finally
							{
								IDisposable disposable;
								if ((disposable = (enumerator as IDisposable)) != null)
								{
									disposable.Dispose();
								}
							}
						}
						else
						{
							if (typeof(IList).IsAssignableFrom(type))
							{
								arg = "IList<" + type.GetEnumerableType() + ">";
							}
							text += "\n";
							IEnumerator enumerator2 = enumerable.GetEnumerator();
							try
							{
								while (enumerator2.MoveNext())
								{
									object o = enumerator2.Current;
									text += string.Format("<b>{0}</b>: {1} ", num++.ToString(), new DTObjectDump(o, this.mIndent.Length + 5).ToString());
								}
							}
							finally
							{
								IDisposable disposable2;
								if ((disposable2 = (enumerator2 as IDisposable)) != null)
								{
									disposable2.Dispose();
								}
							}
						}
					}
					this.mSB.Append(this.mIndent);
					this.mSB.AppendFormat("(<i>{0}</i>) <b>{1}[{2}]</b> = ", arg, info.Name, num);
					this.mSB.AppendLine(text);
				}
				else
				{
					this.mSB.Append(this.mIndent);
					this.mSB.AppendFormat("(<i>{0}</i>) <b>{1}</b> = ", arg, info.Name);
					this.mSB.AppendLine(this.mIndent + value.ToString());
				}
			}
		}

		private const int INDENTSPACES = 5;

		private string mIndent;

		private StringBuilder mSB;

		private object mObject;
	}
}
