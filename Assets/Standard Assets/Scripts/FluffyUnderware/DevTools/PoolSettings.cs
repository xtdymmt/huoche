// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.PoolSettings
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	[Serializable]
	public class PoolSettings
	{
		public PoolSettings()
		{
		}

		public PoolSettings(PoolSettings src)
		{
			this.Prewarm = src.Prewarm;
			this.AutoCreate = src.AutoCreate;
			this.MinItems = src.MinItems;
			this.Threshold = src.Threshold;
			this.Speed = src.Speed;
			this.Debug = src.Debug;
		}

		public bool Prewarm
		{
			get
			{
				return this.m_Prewarm;
			}
			set
			{
				if (this.m_Prewarm != value)
				{
					this.m_Prewarm = value;
				}
			}
		}

		public bool AutoCreate
		{
			get
			{
				return this.m_AutoCreate;
			}
			set
			{
				if (this.m_AutoCreate != value)
				{
					this.m_AutoCreate = value;
				}
			}
		}

		public bool AutoEnableDisable
		{
			get
			{
				return this.m_AutoEnableDisable;
			}
			set
			{
				if (this.m_AutoEnableDisable != value)
				{
					this.m_AutoEnableDisable = value;
				}
			}
		}

		public int MinItems
		{
			get
			{
				return this.m_MinItems;
			}
			set
			{
				int num = Mathf.Max(0, value);
				if (this.m_MinItems != num)
				{
					this.m_MinItems = num;
				}
			}
		}

		public int Threshold
		{
			get
			{
				return this.m_Threshold;
			}
			set
			{
				int num = Mathf.Max(this.MinItems, value);
				if (this.m_Threshold != num)
				{
					this.m_Threshold = num;
				}
			}
		}

		public float Speed
		{
			get
			{
				return this.m_Speed;
			}
			set
			{
				float num = Mathf.Max(0f, value);
				if (this.m_Speed != num)
				{
					this.m_Speed = num;
				}
			}
		}

		public void OnValidate()
		{
			this.MinItems = this.m_MinItems;
			this.Threshold = this.m_Threshold;
			this.Speed = this.m_Speed;
		}

		[SerializeField]
		private bool m_Prewarm;

		[SerializeField]
		private bool m_AutoCreate = true;

		[SerializeField]
		private bool m_AutoEnableDisable = true;

		[Positive]
		[SerializeField]
		private int m_MinItems;

		[Positive]
		[SerializeField]
		private int m_Threshold;

		[Positive]
		[SerializeField]
		private float m_Speed = 1f;

		public bool Debug;
	}
}
