using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
#if UNITY_IPHONE
using UnityEngine.SocialPlatforms.GameCenter;
#endif
#if UNITY_ANDROID
using GooglePlayGames;
#endif


public class GPSInstanceX : MonoBehaviour {

	#region Variables, Constants & Initializers

#if UNITY_ANDROID
    const string LBID = "CgkImKK5uOoSEAIQAQ";

#endif


#if UNITY_IPHONE
    const string LBID = "com.magebear.leaderboard";
#endif

	public bool isTesting;

    public bool showDebugLogs;
	
	// persistant singleton
    private static GPSInstanceX _instance;

	#endregion
	
	#region Lifecycle methods

    public static GPSInstanceX instance
	{
		get
		{
			if(_instance == null)
			{
                _instance = GameObject.FindObjectOfType<GPSInstanceX>();

				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	
	void Awake() 
	{
		this.LogDebug("Awake Called");

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
	}

	void Start ()
	{
		this.LogDebug("Start Called");

		// recommended for debugging:
	
#if UNITY_ANDROID
		PlayGamesPlatform.DebugLogEnabled = showDebugLogs;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
#endif

		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if(success) {
				this.LogDebug("GPS Authenticated SUCCESS");
			} else {
				this.LogDebug("GPS Authenticated FAILURE");
			}
		});
	}
	
	void OnEnable()
	{
		this.LogDebug("OnEnable Called");

	}
	
	void OnDisable()
	{
		this.LogDebug("OnDisable Called");

	}

	#endregion

	#region Utility Methods 

	public void LogDebug(string message) {
		if (showDebugLogs)
			Debug.Log ("GPSInstanceX >> " + message);
	}

	#endregion

	#region Callback Methods 
	public void UnlockAchievementForID(string achievementID) {
		if(!Social.localUser.authenticated) {
			return;
		}

		Social.ReportProgress(achievementID, 100.0f, (bool success) => {
			// handle success or failure
			if(success) {
				this.LogDebug("GPS Achievement Unlocked SUCCESS for " + achievementID);
			} else {
				this.LogDebug("GPS Achievement Unlocked FAILURE for " + achievementID);
			}
		});
	}

	public void IncrementAchievementForID(string achievementID, int xp) {
		if(!Social.localUser.authenticated) {
			return;
		}
	
#if UNITY_ANDROID

		PlayGamesPlatform.Instance.IncrementAchievement(achievementID, xp, (bool success) => {
			// handle success or failure
			if(success) {
				this.LogDebug("GPS Achievement Increment SUCCESS for " + achievementID);
			} else {
				this.LogDebug("GPS Achievement Increment FAILURE for " + achievementID);
			}
		});
#endif
	}

	public void UpdateLeaderboardForID( int score) {
		if(!Social.localUser.authenticated) {
			return;
		}

		Social.ReportScore(score, LBID, (bool success) => {
			// handle success or failure
			if(success) {
				this.LogDebug("GPS Leaderboard Update SUCCESS for " + LBID);
			} else {
				this.LogDebug("GPS Leaderboard Update FAILURE for " + LBID);
			}
		});
	}

//	public void UpdateLeaderboardForID(int score,string level ) {
//		if(!Social.localUser.authenticated) {
//			return;
//		}
//		if(level == "Level1")
//		{
//
//			Social.ReportScore(score, leve1LBID, (bool success) => {
//				// handle success or failure
//				if(success) {
//					this.LogDebug("GPS Leaderboard Update SUCCESS for " + leve1LBID);
//				} else {
//					this.LogDebug("GPS Leaderboard Update FAILURE for " + leve1LBID);
//				}
//			});
//		}
//		else if(level == "Level2")
//		{
//
//			Social.ReportScore(score, leve2LBID, (bool success) => {
//				// handle success or failure
//				if(success) {
//					this.LogDebug("GPS Leaderboard Update SUCCESS for " + leve2LBID);
//				} else {
//					this.LogDebug("GPS Leaderboard Update FAILURE for " + leve2LBID);
//				}
//			});
//		}
//		else if(level == "Level3")
//		{
//
//			Social.ReportScore(score, leve3LBID, (bool success) => {
//				// handle success or failure
//				if(success) {
//					this.LogDebug("GPS Leaderboard Update SUCCESS for " + leve3LBID);
//				} else {
//					this.LogDebug("GPS Leaderboard Update FAILURE for " + leve3LBID);
//				}
//			});
//		}
//		else if(level == "Level4")
//		{
//			Social.ReportScore(score, leve4LBID, (bool success) => {
//				// handle success or failure
//				if(success) {
//					this.LogDebug("GPS Leaderboard Update SUCCESS for " + leve4LBID);
//				} else {
//					this.LogDebug("GPS Leaderboard Update FAILURE for " + leve4LBID);
//				}
//			});
//		}
//
//
//	}

//	public void ShowLeaderboardForIDLevel(string level)
//	{
//
//		if(Social.localUser.authenticated) 
//		{
//			if(level == "Level1")
//			{	
//#if UNITY_ANDROID
//				PlayGamesPlatform.Instance.ShowLeaderboardUI(leve1LBID);
//#endif
//
//#if UNITY_IPHONE
//				Social.ShowLeaderboardUI();
//#endif
//			}
//			else if(level == "Level2")
//			{
//				#if UNITY_ANDROID
//				PlayGamesPlatform.Instance.ShowLeaderboardUI(leve2LBID);
//				#endif
//				#if UNITY_IPHONE
//				Social.ShowLeaderboardUI();
//				#endif
//			}
//			else if(level == "Level3")
//			{
//				#if UNITY_ANDROID
//				PlayGamesPlatform.Instance.ShowLeaderboardUI(leve3LBID);
//				#endif
//				
//				#if UNITY_IPHONE
//				Social.ShowLeaderboardUI();
//				#endif			
//			}
//			else if(level == "Level4")
//			{
//				#if UNITY_ANDROID
//				PlayGamesPlatform.Instance.ShowLeaderboardUI(leve4LBID);
//				#endif
//				
//				#if UNITY_IPHONE
//				Social.ShowLeaderboardUI();
//				#endif			
//			}
//
//		}
//		else
//		{
//			Social.localUser.Authenticate((bool success) => {
//				// handle success or failure
//				if(success) {
//					this.LogDebug("GPS Authenticated SUCCESS");
//				} else {
//					this.LogDebug("GPS Authenticated FAILURE");
//				}
//			});
//		}
//	}

	public void ShowLeaderboardForID(){
		if(Social.localUser.authenticated) 
		{
			#if UNITY_ANDROID
			PlayGamesPlatform.Instance.ShowLeaderboardUI(LBID);
			#endif
			
			#if UNITY_IPHONE
			Social.ShowLeaderboardUI();
			#endif			
		}
		else
		{
			Social.localUser.Authenticate((bool success) => {
				// handle success or failure
				if(success) {
					this.LogDebug("GPS Authenticated SUCCESS");
				} else {
					this.LogDebug("GPS Authenticated FAILURE");
				}
			});
		}
	}

	public void ShowAllLeaderboards(){
		if(Social.localUser.authenticated) {
			Social.ShowLeaderboardUI();
		}
		else
		{
			Social.localUser.Authenticate((bool success) => {
				// handle success or failure
				if(success) {
					this.LogDebug("GPS Authenticated SUCCESS");
				} else {
					this.LogDebug("GPS Authenticated FAILURE");
				}
			});
		}
	}

	public void ShowAchievements(){
		if(Social.localUser.authenticated) {
			Social.ShowAchievementsUI();
		}
		else
		{
			Social.localUser.Authenticate((bool success) => {
				// handle success or failure
				if(success) {
					this.LogDebug("GPS Authenticated SUCCESS");
				} else {
					this.LogDebug("GPS Authenticated FAILURE");
				}
			});
		}
	}

	#endregion
}
