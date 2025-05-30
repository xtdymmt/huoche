// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.ConditionalAttribute
using System;
using System.Reflection;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public class ConditionalAttribute : ActionAttribute
	{
		protected ConditionalAttribute(string fieldOrProperty, object compareTo, bool compareFalse = false) : base(null, ActionAttribute.ActionEnum.Show)
		{
			base.TypeSort = 55;
			this.Conditions = new ConditionalAttribute.Condition[]
			{
				new ConditionalAttribute.Condition
				{
					FieldName = fieldOrProperty,
					CompareTo = compareTo,
					CompareFalse = compareFalse
				}
			};
		}

		protected ConditionalAttribute(string fieldOrProperty, object compareTo, bool compareFalse, ConditionalAttribute.OperatorEnum op, string fieldOrProperty2, object compareTo2, bool compareFalse2) : base(null, ActionAttribute.ActionEnum.Show)
		{
			base.TypeSort = 55;
			this.Conditions = new ConditionalAttribute.Condition[]
			{
				new ConditionalAttribute.Condition
				{
					FieldName = fieldOrProperty,
					CompareTo = compareTo,
					CompareFalse = compareFalse
				},
				new ConditionalAttribute.Condition
				{
					FieldName = fieldOrProperty2,
					CompareTo = compareTo2,
					CompareFalse = compareFalse2,
					Operator = op
				}
			};
		}

		protected ConditionalAttribute(string fieldOrProperty, object compareTo, bool compareFalse, ConditionalAttribute.OperatorEnum op, string fieldOrProperty2, object compareTo2, bool compareFalse2, string fieldOrProperty3, object compareTo3, bool compareFalse3) : base(null, ActionAttribute.ActionEnum.Show)
		{
			base.TypeSort = 55;
			this.Conditions = new ConditionalAttribute.Condition[]
			{
				new ConditionalAttribute.Condition
				{
					FieldName = fieldOrProperty,
					CompareTo = compareTo,
					CompareFalse = compareFalse
				},
				new ConditionalAttribute.Condition
				{
					FieldName = fieldOrProperty2,
					CompareTo = compareTo2,
					CompareFalse = compareFalse2,
					Operator = op
				},
				new ConditionalAttribute.Condition
				{
					FieldName = fieldOrProperty3,
					CompareTo = compareTo3,
					CompareFalse = compareFalse3,
					Operator = op
				}
			};
		}

		protected ConditionalAttribute(string methodToQuery) : base(null, ActionAttribute.ActionEnum.Show)
		{
			base.TypeSort = 55;
			this.Conditions = new ConditionalAttribute.Condition[]
			{
				new ConditionalAttribute.Condition
				{
					MethodName = methodToQuery,
					CompareTo = null
				}
			};
		}

		public virtual bool ConditionMet(object classInstance)
		{
			bool flag = this.evaluate(this.Conditions[0], classInstance);
			for (int i = 1; i < this.Conditions.Length; i++)
			{
				ConditionalAttribute.Condition condition = this.Conditions[i];
				ConditionalAttribute.OperatorEnum @operator = condition.Operator;
				if (@operator != ConditionalAttribute.OperatorEnum.AND)
				{
					if (@operator == ConditionalAttribute.OperatorEnum.OR)
					{
						flag = (flag || this.evaluate(condition, classInstance));
					}
				}
				else
				{
					flag = (flag && this.evaluate(condition, classInstance));
				}
			}
			return flag;
		}

		private bool evaluate(ConditionalAttribute.Condition cond, object classInstance)
		{
			if (!string.IsNullOrEmpty(cond.MethodName))
			{
				if (cond.MethodInfo == null)
				{
					cond.MethodInfo = classInstance.GetType().MethodByName(cond.MethodName, true, true);
				}
				if (cond.MethodInfo == null)
				{
					UnityEngine.Debug.LogWarningFormat("[DevTools] Unable to find method '{0}' at class '{1}' !", new object[]
					{
						cond.MethodName,
						classInstance.GetType().Name
					});
					return cond.CompareFalse;
				}
				if (cond.CompareFalse)
				{
					return !(bool)cond.MethodInfo.Invoke(classInstance, null);
				}
				return (bool)cond.MethodInfo.Invoke(classInstance, null);
			}
			else
			{
				if (cond.FieldInfo == null)
				{
					cond.FieldInfo = classInstance.GetType().FieldByName(cond.FieldName, true, true);
					if (cond.FieldInfo == null)
					{
						cond.PropertyInfo = classInstance.GetType().PropertyByName(cond.FieldName, true, true);
					}
				}
				object obj = null;
				if (cond.FieldInfo != null)
				{
					obj = cond.FieldInfo.GetValue(classInstance);
				}
				else if (cond.PropertyInfo != null)
				{
					obj = cond.PropertyInfo.GetValue(classInstance, null);
				}
				if (obj == null)
				{
					return cond.CompareTo == null && !cond.CompareFalse;
				}
				return obj.Equals(cond.CompareTo) == !cond.CompareFalse;
			}
		}

		public ConditionalAttribute.Condition[] Conditions;

		public enum OperatorEnum
		{
			AND,
			OR
		}

		public class Condition
		{
			public string FieldName;

			public FieldInfo FieldInfo;

			public PropertyInfo PropertyInfo;

			public object CompareTo;

			public bool CompareFalse;

			public ConditionalAttribute.OperatorEnum Operator;

			public MethodInfo MethodInfo;

			public string MethodName;
		}
	}
}
