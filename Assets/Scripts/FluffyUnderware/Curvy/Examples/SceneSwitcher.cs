// dnSpy decompiler from Assembly-CSharp.dll class: FluffyUnderware.Curvy.Examples.SceneSwitcher
using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FluffyUnderware.Curvy.Examples
{
	public class SceneSwitcher : MonoBehaviour
	{
		private void Start()
		{
			List<string> scenes = this.getScenes();
			if (scenes.Count == 0 || this.CurrentLevel < 0)
			{
				this.Text.text = "Add scenes to the build settings to enable the scene switcher!";
			}
			else
			{
				this.Text.text = "Scene selector";
				this.DropDown.ClearOptions();
				this.DropDown.AddOptions(scenes);
			}
			this.DropDown.value = this.CurrentLevel;
			this.DropDown.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChanged));
		}

		private int CurrentLevel
		{
			get
			{
				return SceneManager.GetActiveScene().buildIndex;
			}
			set
			{
				if (this.CurrentLevel != value)
				{
					SceneManager.LoadScene(value, LoadSceneMode.Single);
				}
			}
		}

		private List<string> getScenes()
		{
			int sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;
			List<string> list = new List<string>(sceneCountInBuildSettings);
			for (int i = 0; i < sceneCountInBuildSettings; i++)
			{
				string[] array = SceneUtility.GetScenePathByBuildIndex(i).Split(new char[]
				{
					'/'
				});
				string text = array[array.Length - 1].TrimEnd(".unity", StringComparison.CurrentCultureIgnoreCase);
				string item;
				if (this.scenesAlternativeNames.ContainsKey(text))
				{
					item = this.scenesAlternativeNames[text];
				}
				else
				{
					item = text;
				}
				list.Add(item);
			}
			return list;
		}

		private void OnValueChanged(int value)
		{
			this.CurrentLevel = this.DropDown.value;
		}

		public Text Text;

		public Dropdown DropDown;

		private Dictionary<string, string> scenesAlternativeNames = new Dictionary<string, string>
		{
			{
				"00_SplineController",
				"Move object : Follow a static spline"
			},
			{
				"04_PaintSpline",
				"Move object : Follow a dynamic spline"
			},
			{
				"20_CGPaths",
				"Move object : Follow a blended spline"
			},
			{
				"21_CGExtrusion",
				"Move object : Follow a Curvy generated volume"
			},
			{
				"06_Orientation",
				"Basics: Store orientation data in a spline"
			},
			{
				"01_MetaData",
				"Basics: Store custom data in a spline"
			},
			{
				"05_NearestPoint",
				"Basics: Find nearest point on spline"
			},
			{
				"03_Connections",
				"Basics: Connections and events"
			},
			{
				"10_RBSplineController",
				"Physics: Interaction with a Curvy spline"
			},
			{
				"11_Rigidbody",
				"Physics: Interaction with a Curvy generated mesh"
			},
			{
				"02_GUI",
				"Splines and UI"
			},
			{
				"22_CGClonePrefabs",
				"Advanced : Clone objects along a spline"
			},
			{
				"26_CGExtrusionExtendedUV",
				"Advanced: Extended UV functionality"
			},
			{
				"12_Train",
				"Train: Railway junction"
			},
			{
				"13_TrainMultiTrackDrifting",
				"Train: Multi tracks drifting!"
			},
			{
				"25_CGExtrusionAdvanced",
				"Train: Advanced scene"
			},
			{
				"23_CGTube",
				"Space tube"
			},
			{
				"50_EndlessRunner",
				"Space Runner"
			}
		};
	}
}
