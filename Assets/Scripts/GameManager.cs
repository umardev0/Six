using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GooglePlayGames;

public class GameManager : MonoBehaviour 
{
    public GameObject popupPrefab;

    private static GameManager _instance;

    public static GameManager Instance
	{
		get
		{
			if(_instance == null)
			{
                _instance = GameObject.FindObjectOfType<GameManager>();

				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	
	void Awake() 
	{
		Debug.Log("Awake Called");
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(gameObject);
		}

		ZPlayerPrefs.Initialize(GameConstants.ENCRYPTION_PASSWORD, GameConstants.ENCRYPTION_SALT);

		PlayerPrefs.SetInt("polygon1",1);
		if(PlayerPrefs.GetString(GameConstants.CURRENTPOLY_STRING, "polygon1") == "polygon1")
		{
			PlayerPrefs.GetString(GameConstants.CURRENTPOLY_STRING,"polygon1");
		}
		if(ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING,0) == 0)
		{
			ZPlayerPrefs.SetInt(GameConstants.GLOBALSTARS_STRING,0);
		}
	//	ZPlayerPrefs.SetInt(GameConstants.GLOBALSTARS_STRING,5000);/// testing

		PlayerPrefs.SetInt(GameConstants.LEVEL_STRING+"1",1);
	
		//GameManager.Instance.isSoundPaused = false;
	}

    void Start()
    {
//		PlayGamesPlatform.Activate();
//
//		Social.localUser.Authenticate ((bool success) =>
//			{
//				if (success) {
//					Debug.Log ("Login Sucess on startup");
//				} else {
//					Debug.Log ("Login failed on startup");
//				}
//			});

        ZPlayerPrefs.SetInt(GameConstants.CURRENTSCORE_STRING, 0);
	//	PlayerPrefs.SetString(GameConstants.CURRENTPOLY_STRING, "polygon1");
    }

	public void addStars(int newStars)
	{
		int currStars = ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING);
		int finalStars = currStars + newStars;
		ZPlayerPrefs.SetInt(GameConstants.GLOBALSTARS_STRING, finalStars);
		Text starTxt = GameObject.FindWithTag("StarsTxt").GetComponent<Text>();
		starTxt.text = finalStars.ToString();

		if(GameObject.Find("MainMenuCanvas") != null)
		{
			GameObject.Find("MainMenuCanvas").GetComponent<MainMenuScript>().StarAnimSet();
		}
	}

    public void subStars(int newStars)
    {
        int currStars = ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING);
        int finalStars = currStars - newStars;
        ZPlayerPrefs.SetInt(GameConstants.GLOBALSTARS_STRING, finalStars);
    }

    public void createPopup(string title, string desc, string popupName, string url)
    {
        GenericPopup popup = new GenericPopup();

        if(GameObject.Find("MainMenuCanvas") != null)
        {
            popup.create(title, desc, popupPrefab, GameObject.Find("MainMenuCanvas"), popupName, url);
        }
    }
}
