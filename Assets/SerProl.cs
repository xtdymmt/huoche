using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QGMiniGame;
using UnityEngine.SceneManagement;

public class SerProl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if (PlayerPrefs.GetInt("Prol") == 1)
        //{
        //    this.gameObject.SetActive(false);
        //}
		//if (TimeTest.isOpen)
		//{
			ShowBannerad();
		//}
	}

	public void ShowBannerad()
	{
		// http://minigame.vivo.com.cn/documents/#/api/ad/banner-ad

		ADManagerRPK.Instance.ShowBanner();


	 
	}



    public void Btn()
    {
       // PlayerPrefs.SetInt("Prol", 1);
		this.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync("charSelect");
        //if(TimeTest.isOpen)
        //{
        //ShowBannerad();
        //}

    }
    public void OnDisAgreeBtnClick()
    {
        QG.ExitApplication();
    }
}
