using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameAnalyticsSDK;

public class UpdateStarsInHeader : MonoBehaviour {

    public Text starText = null;

	// Use this for initialization
	void Start () {
        GameAnalytics.NewDesignEvent ("CharacterShopOpened");
        int starCount = ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING);
        starText.text = "" + starCount;
	}
	
}
