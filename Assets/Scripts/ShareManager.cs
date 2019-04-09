using UnityEngine;
using System.Collections;
using System.IO;

public class ShareManager : MonoBehaviour {

	public static IEnumerator Share(GameObject homeBtn, GameObject shareBtn)
	{
		string dateAndTime = System.DateTime.Now.ToString(); 
		dateAndTime = dateAndTime.Replace("/", "-"); 

		string screenshotName = "ScreenShot" + "_" + dateAndTime + ".png"; 
		string screenshotPath = Path.Combine( Application.persistentDataPath, screenshotName );
		Debug.Log("Arslan:: imagePath = " + screenshotPath);
		//      string filename = "ScreenN123.png";
		Application.CaptureScreenshot(screenshotName);

		while(!System.IO.File.Exists(screenshotPath)){
			Debug.Log("Arslan:: waiting ");
			yield return null;    
		}

		homeBtn.SetActive(true);
		shareBtn.SetActive(true);

		AdHandler.GetInstance().showAdmobBanner();
        #if UNITY_ANDROID
		//instantiate the class Intent
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");

		//instantiate the object Intent
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

		//call setAction setting ACTION_SEND as parameter
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

		//instantiate the class Uri
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");

		//instantiate the object Uri with the parse of the url's file
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + screenshotPath);

		//call putExtra with the uri object of the file
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		//call putExtra with the uri object of the file
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), GameConstants.SHARE_SUBJECT);
		//call putExtra with the uri object of the file
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), GameConstants.SHARE_TITLE);
		//call putExtra with the uri object of the file
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), GameConstants.SHARE_TEXT);

		//set the type of file
		intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");

		//instantiate the class UnityPlayer
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

		//instantiate the object currentActivity
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

		// option one WITHOUT chooser:
		//currentActivity.Call("startActivity", intentObject);

		// option two WITH chooser:
		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, GameConstants.SHARE_SUBJECT);
		currentActivity.Call("startActivity", jChooser);
        #endif

        #if UNITY_IOS

        GeneralSharingiOSBridge.ShareTextWithImage (screenshotPath, GameConstants.SHARE_TEXT_IOS);

        #endif
	}
}
