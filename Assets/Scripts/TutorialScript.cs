// dnSpy decompiler from Assembly-CSharp.dll class: TutorialScript
using System;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
	private void Start()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 2)
		{
			if (PlayerPrefs.GetInt("UserTrain") == 0)
			{
				this.BtnDialogue.SetActive(true);
			}
			else
			{
				this.BtnDialogue.SetActive(false);
				this.FalseTutorial();
			}
		}
	}

	public void OkBtn_Click()
	{
		this.Obj1.SetActive(true);
		this.Obj2.SetActive(false);
		this.Obj3.SetActive(false);
		this.Obj4.SetActive(false);
		this.Obj5.SetActive(false);
		this.Obj6.SetActive(false);
		this.Obj7.SetActive(false);
		this.Obj8.SetActive(false);
		base.Invoke("obj_True2", 3f);
		this.BtnDialogue.SetActive(false);
	}

	private void obj_True2()
	{
		this.Obj1.SetActive(false);
		this.Obj2.SetActive(true);
		this.Obj3.SetActive(false);
		this.Obj4.SetActive(false);
		this.Obj5.SetActive(false);
		this.Obj6.SetActive(false);
		this.Obj7.SetActive(false);
		this.Obj8.SetActive(false);
		base.Invoke("obj_True3", 3f);
	}

	private void obj_True3()
	{
		this.Obj1.SetActive(false);
		this.Obj2.SetActive(false);
		this.Obj3.SetActive(true);
		this.Obj4.SetActive(false);
		this.Obj5.SetActive(false);
		this.Obj6.SetActive(false);
		this.Obj7.SetActive(false);
		this.Obj8.SetActive(false);
		base.Invoke("obj_True4", 3f);
	}

	private void obj_True4()
	{
		this.Obj1.SetActive(false);
		this.Obj2.SetActive(false);
		this.Obj3.SetActive(false);
		this.Obj4.SetActive(true);
		this.Obj5.SetActive(false);
		this.Obj6.SetActive(false);
		this.Obj7.SetActive(false);
		this.Obj8.SetActive(false);
		base.Invoke("obj_True5", 3f);
	}

	private void obj_True5()
	{
		this.Obj1.SetActive(false);
		this.Obj2.SetActive(false);
		this.Obj3.SetActive(false);
		this.Obj4.SetActive(false);
		this.Obj5.SetActive(true);
		this.Obj6.SetActive(false);
		this.Obj7.SetActive(false);
		this.Obj8.SetActive(false);
		base.Invoke("obj_True6", 3f);
	}

	private void obj_True6()
	{
		this.Obj1.SetActive(false);
		this.Obj2.SetActive(false);
		this.Obj3.SetActive(false);
		this.Obj4.SetActive(false);
		this.Obj5.SetActive(false);
		this.Obj6.SetActive(true);
		this.Obj7.SetActive(false);
		this.Obj8.SetActive(false);
		base.Invoke("obj_True7", 3f);
	}

	private void obj_True7()
	{
		this.Obj1.SetActive(false);
		this.Obj2.SetActive(false);
		this.Obj3.SetActive(false);
		this.Obj4.SetActive(false);
		this.Obj5.SetActive(false);
		this.Obj6.SetActive(false);
		this.Obj7.SetActive(true);
		this.Obj8.SetActive(false);
		base.Invoke("obj_True8", 3f);
	}

	private void obj_True8()
	{
		this.Obj1.SetActive(false);
		this.Obj2.SetActive(false);
		this.Obj3.SetActive(false);
		this.Obj4.SetActive(false);
		this.Obj5.SetActive(false);
		this.Obj6.SetActive(false);
		this.Obj7.SetActive(false);
		this.Obj8.SetActive(true);
		base.Invoke("FalseTutorial", 3f);
	}

	private void FalseTutorial()
	{
		this.Obj1.SetActive(false);
		this.Obj2.SetActive(false);
		this.Obj3.SetActive(false);
		this.Obj4.SetActive(false);
		this.Obj5.SetActive(false);
		this.Obj6.SetActive(false);
		this.Obj7.SetActive(false);
		this.Obj8.SetActive(false);
		this.RaceBtnPanel.SetActive(false);
		this.TutAnimatuon.SetActive(false);
		this.DoorCloseAnim.SetActive(true);
		PlayerPrefs.SetInt("UserTrain", 1);
	}

	public GameObject Obj1;

	public GameObject Obj2;

	public GameObject Obj3;

	public GameObject Obj4;

	public GameObject Obj5;

	public GameObject Obj6;

	public GameObject Obj7;

	public GameObject Obj8;

	public GameObject BtnDialogue;

	public GameObject TutAnimatuon;

	public GameObject RaceBtnPanel;

	public GameObject DoorCloseAnim;
}
