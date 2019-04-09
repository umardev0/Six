using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Soomla.Store;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GameAnalyticsSDK;

public class MainMenuScript : MonoBehaviour 
{
	public GameObject musicOn;
	public GameObject BestScore;
	public GameObject CurrentScore;
	public GameObject Stars;
	public GameObject VidAdBtn;
	public GameObject rateUsPanel;
	public GameObject quitPanel;
    public GameObject sharePanel;
    public Text sharePanelScoreText;

	public GameObject fader;

	public ParticleSystem starPart;


	public string AndroidBundleID;
	public string IOSBundleID;
	public string WP8AppID;

    int currentScoreInt = 0;
    int bestScoreInt = 0;

	// Use this for initialization
	void Start () 
	{
//        PlayerPrefs.DeleteAll();
//        PlayerPrefs.Save();
		fader.SetActive(false);

        bestScoreInt = ZPlayerPrefs.GetInt(GameConstants.BESTSCORE_STRING, 0);

        BestScore.GetComponent<Text>().text = bestScoreInt.ToString();
        currentScoreInt = ZPlayerPrefs.GetInt(GameConstants.CURRENTSCORE_STRING, 0);
        CurrentScore.GetComponent<Text>().text = currentScoreInt.ToString();
		Stars.GetComponentInChildren<Text>().text = ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING,0).ToString();
        showRatePanel();
		InvokeRepeating("ActivateVideoAdBtn", 1f, 2f);

		if (AudioListener.volume == 0.0f)
		{
			Sprite temp = Resources.Load ("sound_off",typeof(Sprite)) as Sprite;
			musicOn.GetComponent<Image>().overrideSprite  =  temp;//Resources.Load ("DressingScene/Body/body"+id.ToString()+".png",typeof(Sprite)) as Sprite;
			//musicOn.GetComponent<UISprite>().spriteName = "SoundOn-button";
		}
		else
		{
			Sprite temp = Resources.Load ("sound_on",typeof(Sprite)) as Sprite;
			musicOn.GetComponent<Image>().overrideSprite  =  temp;
			//musicOn.GetComponent<UISprite>().spriteName = "SoundOff-button";
		}
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

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{ 
			OnBackKeyPressed ();
		}
	}

	public void onPlay()
	{
		SoundManager.Instance.PlayClickSound();
        GameConstants.LEVEL_TO_LOAD = 0;
		SceneManager.LoadScene("GamePlay");
	}

	public void onChallenge()
	{
		SoundManager.Instance.PlayClickSound();
		SceneManager.LoadScene("LevelScene");
	}

	public void onSound()
	{
		SoundManager.Instance.PlayClickSound();

		if (AudioListener.volume == 0.0f)
		{
			AudioListener.volume = 1.0f;
			Sprite temp = Resources.Load ("sound_on",typeof(Sprite)) as Sprite;
			musicOn.GetComponent<Image>().overrideSprite  =  temp;//Resources.Load ("DressingScene/Body/body"+id.ToString()+".png",typeof(Sprite)) as Sprite;
			//musicOn.GetComponent<UISprite>().spriteName = "SoundOn-button";
		}
		else
		{
			AudioListener.volume = 0.0f;
			Sprite temp = Resources.Load ("sound_off",typeof(Sprite)) as Sprite;
			musicOn.GetComponent<Image>().overrideSprite  =  temp;
			//musicOn.GetComponent<UISprite>().spriteName = "SoundOff-button";

		}
	}

	public void onVideoAd()
	{
		SoundManager.Instance.PlayClickSound();
        GameAnalytics.NewDesignEvent ("VideoClicked");
		AdHandler.GetInstance().showVideoAd();
	}

	public void onLeaderBoard()
	{
		if (Social.localUser.authenticated)
		{
//			((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (GameConstants.LEADERBOARD_ID);
            GameAnalytics.NewDesignEvent ("OpenLeaderboard");
            GPSInstanceX.instance.ShowLeaderboardForID();
		}
		else
		{
			Social.localUser.Authenticate ((bool success) =>
				{
					if (success) {
//						((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (GameConstants.LEADERBOARD_ID);
                        GPSInstanceX.instance.ShowLeaderboardForID();
						Debug.Log ("Login Sucess");
					} else {
						Debug.Log ("Login failed");
					}
				});
		}
	}

	public void onRateUs()
	{
		SoundManager.Instance.PlayClickSound();
//		if(PlayerPrefs.GetInt("RATEDONE",0)==0)
//		{
//			PlayerPrefs.SetInt("RATEDONE",1);
//			GameManager.Instance.addCoins(10);
//		}
		#if UNITY_ANDROID
		if(AndroidBundleID != null) {
			Application.OpenURL("market://details?id=" + AndroidBundleID);
		}
		#elif UNITY_IPHONE
		if(IOSBundleID != null) {
            Application.OpenURL("http://apple.co/2cH8dYH");
		}
		#elif UNITY_WP8
		if(WP8AppID != null) {
		Application.OpenURL("zune:reviewapp?appid=app" + WP8AppID);
		}
		#endif

		if(PlayerPrefs.GetInt("RateCoins", 0) == 0)
		{
			PlayerPrefs.SetInt("RateCoins", 1);
			GameManager.Instance.addStars(100);
		}

	}

	public void onStore()
	{
		SoundManager.Instance.PlayClickSound();
		SceneManager.LoadScene("StoreScene");
	}

	public void onRestoreInapp()
	{
		SoundManager.Instance.PlayClickSound();
		SoomlaStore.RestoreTransactions();
	}

	public void onRemoveAds()
	{
		SoundManager.Instance.PlayClickSound();
		StoreInventory.BuyItem("removead");
	}

	public void StarAnimSet()
	{
		//iTween.PunchScale (Stars,new Vector3 (2, 2, 0), 1);
		Stars.GetComponentInChildren<Text>().text = ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING,0).ToString();

		starPart.Play();
	}

	public void setStars(int sta)
	{
		Stars.GetComponentInChildren<Text>().text = sta.ToString();
	}


	public void showRatePanel()
	{
        int gameCount = ZPlayerPrefs.GetInt(GameConstants.GAMEOVER_COUNT_STRING, 0);
        int socialCount = PlayerPrefs.GetInt(GameConstants.SOCIAL_COUNT_STRING, 0);

		if(gameCount >= GameConstants.RATEUS_COUNT && PlayerPrefs.GetInt(GameConstants.RATE_SHOW_STRING,0) == 0)
		{
            ZPlayerPrefs.SetInt(GameConstants.GAMEOVER_COUNT_STRING, 0);
			fader.SetActive(true);
			iTween.MoveTo(rateUsPanel,Vector3.zero,0.1f);
		}
        else if(socialCount >= GameConstants.INSTAGRAM_COUNT && PlayerPrefs.GetInt(GameConstants.IS_INSTAGRAM_DONE_STRING,0) == 0)
        {
            PlayerPrefs.SetInt(GameConstants.SOCIAL_COUNT_STRING, socialCount + 1);
            GameManager.Instance.createPopup("Follow Us!", "Will you follow us @MageBearStudio on Instagram for 500 stars?", "instagram", "https://www.instagram.com/magebearstudio/");
        }
        else if(socialCount >= GameConstants.FACEBOOK_COUNT && PlayerPrefs.GetInt(GameConstants.IS_FACEBOOK_DONE_STRING,0) == 0)
        {
            PlayerPrefs.SetInt(GameConstants.SOCIAL_COUNT_STRING, 0);
            GameManager.Instance.createPopup("Follow Us!", "Will you follow us @MageBearStudio on facebook for 500 stars?", "facebook", "https://www.facebook.com/magebearstudio");
        }
	}

	public void hideRatePanel()
	{
		fader.SetActive(false);
		iTween.MoveAdd(rateUsPanel, Vector3.down * 100, 0.1f);
	}

	public void showQuitPanel()
	{
		fader.SetActive(true);
		iTween.MoveTo(quitPanel,Vector3.zero,0.1f);
	}

	public void hideQuitPanel()
	{
		fader.SetActive(false);
		iTween.MoveAdd(quitPanel, Vector3.down * 100, 0.1f);
	}

	public void onRateIt()
	{
        GameAnalytics.NewDesignEvent ("RateIt");
		fader.SetActive(false);
		iTween.MoveAdd(rateUsPanel, Vector3.down * 100, 0.1f);
		onRateUs();
		PlayerPrefs.SetInt(GameConstants.RATE_SHOW_STRING,1);
	}

	public void onNoThanksRate()
	{
		fader.SetActive(false);
		iTween.MoveAdd(rateUsPanel, Vector3.down * 100, 0.1f);
		PlayerPrefs.SetInt(GameConstants.RATE_SHOW_STRING,1);

	}

	public void onRemindRate()
	{
        GameAnalytics.NewDesignEvent ("RemindMeLater");
		fader.SetActive(false);
		iTween.MoveAdd(rateUsPanel, Vector3.down * 100, 0.1f);
	}

	public void onYesQuit()
	{
		fader.SetActive(false);
		iTween.MoveAdd(quitPanel, Vector3.down * 100, 0.1f);
		Application.Quit();
	}

	public void onNoQuit()
	{
		fader.SetActive(false);
		iTween.MoveAdd(quitPanel, Vector3.down * 100, 0.1f);
	}

	private void OnBackKeyPressed() 
	{
        showQuitPanel();
	}

    public void bestShareClick()
    {
        sharePanelScoreText.text = bestScoreInt.ToString();
        openSharePanel();
    }

    public void currentShareClick()
    {
        sharePanelScoreText.text = currentScoreInt.ToString();
        openSharePanel();
    }

    void openSharePanel()
    {
        sharePanel.SetActive(true);
    }

    public void openStarShopPanel()
    {
        SoundManager.Instance.PlayClickSound();
        SceneManager.LoadScene("StarShopScene");
    }
	
	public void OnInfoClick()
	{
		Application.OpenURL("http://magebear.com/privacypolicy.html");
	}
}
