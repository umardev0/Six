using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;

public class AdHandler : MonoBehaviour {

	private static AdHandler instance;
    Func<bool> m_callback = null;

	[SerializeField] bool showBannerAdmob = false;
	[SerializeField] bool showInterstatialAdmob = false;
	[SerializeField] bool showInterstatialChartBoost = false;
	[SerializeField] bool showVideoVungle = false;
	[SerializeField] bool showVideoUnity = false;



	[SerializeField] string ad_mob_banner_id_ios = "";
	[SerializeField] string ad_mob_interstatial_id_ios = "";

	[SerializeField] string ad_mob_banner_id_android = "";
	[SerializeField] string ad_mob_interstatial_id_android = "";

	[SerializeField] string vungle_appid_android = "Test_Android";
	[SerializeField] string vungle_appid_ios = "Test_iOS";

	[SerializeField] string unity_gameID_android = "33675";
    [SerializeField] string unity_gameID_ios = "33675";

	public static AdHandler GetInstance()
	{
		return instance;
	}

	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
		{
			//if not, set instance to this
			instance = this;
			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
		}
		//If instance already exists and it's not this:
		else if (instance != this)
		{
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    
		}
	}

	void Start()
	{
		if(ZPlayerPrefs.GetInt("ads", 1) == 1)
		{
			InitailizeAdNetworks();
			Invoke("StartBanner", 2f);
		}
		InitailizeVideoNetworks();
	}

	void StartBanner()
	{
		AdHandler.GetInstance().showAdmobBanner();
	}

	void InitailizeVideoNetworks()
	{
		if(showVideoVungle)
		{
			Vungle.init (vungle_appid_android, vungle_appid_ios, "vungleTest");
			Vungle.onAdFinishedEvent += OnVungleAdFinished;
		}

		if(showVideoUnity)
		{
            #if UNITY_IOS
            Advertisement.Initialize (unity_gameID_ios, true);
            #endif
            #if UNITY_ANDROID
            Advertisement.Initialize (unity_gameID_android, true);
            #endif
		}
	}

	void InitailizeAdNetworks()
	{
		if(showInterstatialChartBoost)
		{
			ChartboostHandler.initialize();
		}

		if(showBannerAdmob && showInterstatialAdmob)
		{
			#if UNITY_IOS
			AdMobHandler.initialize (ad_mob_banner_id_ios, ad_mob_interstatial_id_ios);
			#endif
			#if UNITY_ANDROID
			AdMobHandler.initialize (ad_mob_banner_id_android, ad_mob_interstatial_id_android);
			#endif
		}
		else
		{
			if(showBannerAdmob)
			{
				#if UNITY_IOS
				AdMobHandler.initialize (ad_mob_banner_id_ios, "");
				#endif
				#if UNITY_ANDROID
				AdMobHandler.initialize (ad_mob_banner_id_android, "");
				#endif
			}
			else if(showInterstatialAdmob)
			{
				#if UNITY_IOS
				AdMobHandler.initialize ("", ad_mob_interstatial_id_ios);
				#endif
				#if UNITY_ANDROID
				AdMobHandler.initialize ("", ad_mob_interstatial_id_android);
				#endif
			}
		}
	}

	// Called when the player pauses
	void OnApplicationPause(bool pauseStatus) {
		if (pauseStatus)
			Vungle.onPause();
		else
			Vungle.onResume();
	}

	public void showAdmobBanner()
	{
		if(ZPlayerPrefs.GetInt("ads", 1) == 0)
			return;

		if(showBannerAdmob)
		{
			AdMobHandler.showAdmobBanner ();
		}
	}

	public void hideAdmobBanner()
	{
		if(ZPlayerPrefs.GetInt("ads", 1) == 0)
			return;

		if(showBannerAdmob)
		{
			AdMobHandler.hideAdmobBanner ();
		}
	}

	public void showInterstatial()
	{
		if(ZPlayerPrefs.GetInt("ads", 1) == 0)
			return;

		if(showInterstatialAdmob)
		{
			AdMobHandler.showAdmobInterstatial();
		}
        else if(ChartboostHandler.isCached() && showInterstatialChartBoost)
        {
            ChartboostHandler.showInterstatialAd();
        }
	}

	public void showVideoAd()
	{
		if(isUnityAdReady())
		{
			ShowUnityAd();
		}
		else if(Vungle.isAdvertAvailable())
		{
			Vungle.playAd();
		}
	}

    public void showVideoAd(Func<bool> func)
    {
        m_callback = func;
        showVideoAd();
    }

	void OnVungleAdFinished(AdFinishedEventArgs args)
	{
		VideoAdFinishAction();
	}

	void ShowUnityAd(string zone = "")
	{
		if (string.Equals (zone, ""))
			zone = null;

		ShowOptions options = new ShowOptions ();
		options.resultCallback = UnityAdFinished;

		if (Advertisement.IsReady (zone))
			Advertisement.Show (zone, options);
	}

	bool isUnityAdReady()
	{
		return Advertisement.IsReady("");
	}

	void UnityAdFinished(ShowResult result)
	{
		switch(result)
		{
		case ShowResult.Finished:
			{
				VideoAdFinishAction();
				break;
			}
		case ShowResult.Skipped:
			Debug.Log ("Unity Ad skipped. Son, I am dissapointed in you");
			break;
		case ShowResult.Failed:
			Debug.Log("Unity Ad Failed. I swear this has never happened to me before");
			break;
		}
	}

	void VideoAdFinishAction()
	{
        if (m_callback != null)
        {
            m_callback();
            m_callback = null;
            return;
        }

		GameManager.Instance.addStars(GameConstants.VIDEOAD_STARS);
		Debug.Log ("Ad Finished. Rewarding player...");
		if(GameObject.Find("MainMenuCanvas").GetComponent<MainMenuScript>() != null)
		{
			GameObject.Find("MainMenuCanvas").GetComponent<MainMenuScript>().StarAnimSet();
		}
	}

	public bool IsVideoAdAvailable()
	{
		if(Vungle.isAdvertAvailable() || isUnityAdReady())
		{
			return true;
		}
		return false;
	}
}
