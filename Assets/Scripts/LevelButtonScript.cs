using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtonScript : MonoBehaviour 
{

	public GameObject stars;

	public GameObject openLevelImg;
	public GameObject closeLevelImg;
	public GameObject levelTxt;

	//public Color myColor;
	public int myIndex;

	// Use this for initialization
	void Start () 
	{
		int.TryParse(gameObject.name, out myIndex);

		stars = transform.Find("Stars").gameObject;
		openLevelImg = transform.Find("OpenLevel").gameObject;
		levelTxt = transform.Find("LevelNo").gameObject;
		closeLevelImg = transform.Find("LockedLevel").gameObject;

		levelTxt.GetComponent<Text>().text = gameObject.name;
		levelTxt.GetComponent<Text>().resizeTextMaxSize = 25;
		stars.AddComponent<LevelStarsScript>();

		if(PlayerPrefs.GetInt(GameConstants.LEVEL_STRING+gameObject.name,0) == 0)
		{
			levelTxt.GetComponent<Text>().color = Color.white;
			openLevelImg.SetActive(false);
			closeLevelImg.SetActive(true);
			stars.GetComponent<LevelStarsScript>().setStars(PlayerPrefs.GetInt(GameConstants.LEVELSTARS_STRING+gameObject.name,0));
		}
		else
		{
			levelTxt.GetComponent<Text>().color = Color.grey;
			openLevelImg.SetActive(true);
			closeLevelImg.SetActive(false);
			stars.GetComponent<LevelStarsScript>().setStars(PlayerPrefs.GetInt(GameConstants.LEVELSTARS_STRING+gameObject.name,0));
		}

		Button clickButton = GetComponent<Button>();
		clickButton.transition = Selectable.Transition.ColorTint;
		clickButton.onClick.AddListener(delegate() {

			onOpenLevel();

		});
	}

	public void onOpenLevel()
	{

		// open level
		SoundManager.Instance.PlayClickSound();

        if (closeLevelImg.activeSelf)
        {
            return;
        }

        GameConstants.LEVEL_TO_LOAD = myIndex;
        SceneManager.LoadScene("GamePlay");
	}



	public void refreshStar()
	{
		stars.GetComponent<LevelStarsScript>().setStars(ZPlayerPrefs.GetInt("LEVELSTARS"+gameObject.name,0));
	}
}
