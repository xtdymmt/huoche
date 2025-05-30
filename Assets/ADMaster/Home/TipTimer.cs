using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipTimer : MonoBehaviour {

	[SerializeField] private GameObject TipPanel;
	[SerializeField] private Button Confirm_btn;
	[SerializeField] private Text Confirm_Text;

	// Use this for initialization
	void Start () {

		TipPanel = transform.GetChild(0).gameObject;
		Confirm_Text = Confirm_btn.GetComponentInChildren<Text>();

		TipPanel.SetActive(false);
		Confirm_btn.onClick.AddListener(()=>
		{
			MF_CanvasController.ShowPausePanel("继续游戏");
			TipPanel.SetActive(false);
		});
		Invoke("ShowTip", 60);
	}

	public void ShowTip()
	{
		TipPanel.SetActive(true);
		Confirm_btn.enabled = false;
		StartCoroutine(Countdown());

		Invoke("ShowTip", 45 + 3);

	}

	private IEnumerator Countdown()
	{
		for (int i = 3; i > 0; i--)
		{
			Confirm_Text.text = i.ToString();
			yield return new WaitForSeconds(1);
		}

		Confirm_Text.text = "确认";
		Confirm_btn.enabled = true;
	}

	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR_WIN
		if (Input.GetKeyDown(KeyCode.T))
		{
			ShowTip();
		}
#endif
	}
}
