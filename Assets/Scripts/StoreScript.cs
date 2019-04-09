using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreScript : MonoBehaviour
{
	public Text scoreTxt;

	// Use this for initialization
	void Start ()
	{
		scoreTxt.text = ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING,0).ToString();
	}
}
