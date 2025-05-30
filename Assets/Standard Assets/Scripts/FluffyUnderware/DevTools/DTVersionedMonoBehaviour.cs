// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.DTVersionedMonoBehaviour
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public class DTVersionedMonoBehaviour : MonoBehaviour
	{
		public string Version
		{
			get
			{
				return this.m_Version;
			}
		}

		protected void CheckForVersionUpgrade()
		{
		}

		protected virtual bool UpgradeVersion(string oldVersion, string newVersion)
		{
			if (string.IsNullOrEmpty(oldVersion))
			{
				UnityEngine.Debug.LogFormat("[{0}] Upgrading '{1}' to version {2}! PLEASE SAVE THE SCENE!", new object[]
				{
					base.GetType().Name,
					base.name,
					newVersion
				});
			}
			else
			{
				UnityEngine.Debug.LogFormat("[{0}] Upgrading '{1}' from version {2} to {3}! PLEASE SAVE THE SCENE!", new object[]
				{
					base.GetType().Name,
					base.name,
					oldVersion,
					newVersion
				});
			}
			return true;
		}

		public void Destroy()
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(base.gameObject);
			}
		}

		[SerializeField]
		[HideInInspector]
		private string m_Version;
	}
}
