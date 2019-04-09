using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Soomla.Store;
using GameAnalyticsSDK;

public class ButtonClick : MonoBehaviour
{

	public GameObject homeBtn, shareBtn, VidAdBtn;

	void Start()
	{
		if(VidAdBtn != null)
			InvokeRepeating("ActivateVideoAdBtn", 0.5f, 2f);
	}

	void ActivateVideoAdBtn()
	{
		if(!AdHandler.GetInstance().IsVideoAdAvailable())
		{
			VidAdBtn.SetActive(false);
		}
		else
		{
			VidAdBtn.SetActive(true);
		}
	}

    public void homeButtonClicked()
    {
        SoundManager.Instance.PlayClickSound();
        SceneManager.LoadScene("MainMenu");
    }


    public void buyStarsButtonClick()
    {
        SoundManager.Instance.PlayClickSound();
        SceneManager.LoadScene("StarShopScene");
    }


	public void shareButtonClick()
    {
        GameAnalytics.NewDesignEvent("shareButtonClick");
        Debug.Log("share");
		SoundManager.Instance.PlayClickSound();
		homeBtn.SetActive(false);
		shareBtn.SetActive(false);
		AdHandler.GetInstance().hideAdmobBanner();
        #if UNITY_IPHONE
//        GameObject.Find("SharePanel").GetComponent<GeneralSharing>().OnShareTextWithImage();
        StartCoroutine(ShareManager.Share(homeBtn, shareBtn));
        #endif

        #if UNITY_ANDROID
		StartCoroutine(ShareManager.Share(homeBtn, shareBtn));
        #endif
    }

    public void buy100Stars()
    {
        Debug.Log("buy100Stars");
		SoundManager.Instance.PlayClickSound();
		StoreInventory.BuyItem("stars100");
    }

    public void buy500Stars()
    {
        Debug.Log("buy500Stars");
		SoundManager.Instance.PlayClickSound();
		StoreInventory.BuyItem("stars500");
    }

    public void buy1000Stars()
    {
        Debug.Log("buy1000Stars");
		SoundManager.Instance.PlayClickSound();
		StoreInventory.BuyItem("stars1000");
    }

    public void watchVideo()
    {
        Debug.Log("watchvideoad");
		SoundManager.Instance.PlayClickSound();
        GameAnalytics.NewDesignEvent ("VideoClicked");
		AdHandler.GetInstance().showVideoAd();
    }
}
