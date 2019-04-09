using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GameAnalyticsSDK;
using Soomla.Store;

public class GamePlay : MonoBehaviour
{
    public GameObject polygon;
    public GameObject octagon;
    public GameObject circle;

    string currentShapeName = "";
    GameObject currentShape;

    Vector3 lastBoxPosition;
    Transform groundBox;
    public Transform starPrefab;
    public Transform starParent;

    public Text starText;
    public Text bestScoreText;
    public Text currentScoreText;
    public Text lastCallText;
    public Text gameoverScoreText;
    public GameObject musicOn;
    public GameObject pauseMenu;
    public GameObject scorePanel;
    public GameObject levelStarsPanel;
    public GameObject gameoverPanel;
    public GameObject gameoverBG;
    public GameObject particleGameObject;
    public GameObject popButtonGameObject;

    public GameObject sharePanel;
    public Text sharePanelScoreText;

    GameObject starAnimationObject;

    int nextStart = 1;
    int currentScore = 0;

    public Transform plusFivePrefab;
//    private float animationTime = 0.25f;

    private float cameraYPos = 0;
    private float polygonPreviousYPos = 0;

    bool isGameOver = false;
    public bool isPauseMenu = false;

    int boxCount = 1;
    LevelData levelData = new LevelData();
    public int starCount = 0;

    int bestScore = 0;
//    int currentScore = 0;
    int numOfStars = 0;

	// Use this for initialization
	void Start ()
    {
        polygonSetting();

        bestScore = ZPlayerPrefs.GetInt(GameConstants.BESTSCORE_STRING, 0);
        numOfStars = ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING, 0);
        currentScoreText.text = "" + currentScore;
        bestScoreText.text = "" + bestScore;
        starText.text = "" + numOfStars;

        cameraYPos = Camera.main.transform.position.y;
        Transform groundBoxPrefab = BoxPattern.getInstance().groundBoxPrefab;
        groundBox = Instantiate(groundBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Transform;
        groundBox.parent = transform;
        groundBox.localPosition = new Vector3(0, 0, 0);

        if (GameConstants.LEVEL_TO_LOAD == 0)
        {
            GameAnalytics.NewDesignEvent ("InfiniteGamePlay");
            initInfiniteGamePlay();
        }
        else
        {
            GameAnalytics.NewDesignEvent ("LevelGamePlay");
            initLevel(GameConstants.LEVEL_TO_LOAD);
        }

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

    void initInfiniteGamePlay()
    {
        scorePanel.SetActive(true);
        levelStarsPanel.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            groundBox.localPosition = new Vector3(0, -3*(i+1), 0);

            Transform box = BoxPattern.getInstance().getRandomBoxPattern();
            Transform boxObj = Instantiate(box, new Vector3(0, -3 * i, 0), Quaternion.identity) as Transform;
            boxObj.parent = transform;
            boxObj.localPosition = new Vector3(0, -3 * i, 0);
            lastBoxPosition = boxObj.localPosition;
        }
        addStarGameObject(10);
        updateScores(0, null);

        if (numOfStars >= 30)
        {
            popButtonGameObject.GetComponent<Image>().sprite = Resources.Load("PopStar", typeof(Sprite)) as Sprite;
            popButtonGameObject.SetActive(true);
        }
        else if (AdHandler.GetInstance().IsVideoAdAvailable())
        {
            popButtonGameObject.GetComponent<Image>().sprite = Resources.Load("PopVideo", typeof(Sprite)) as Sprite;
            popButtonGameObject.SetActive(true);
        }
        else
        {
            popButtonGameObject.SetActive(false);
        }
    }

    void initLevel(int levelNum)
    {
        scorePanel.SetActive(false);
        levelStarsPanel.SetActive(true);
        levelData = LevelsReader.getInstance().getDataForLevel(levelNum);
        int boxListSize = levelData.boxList.Count;
        int numberOfBoxesToDraw = 0;

        numberOfBoxesToDraw = boxListSize > 4 ? 4 : boxListSize;

        for (int i = 0; i < numberOfBoxesToDraw; i++)
        {
            groundBox.localPosition = new Vector3(0, -3*(i+1), 0);

            Transform box = BoxPattern.getInstance().getBoxPatternNumber(levelData.boxList[i]);
            Transform boxObj = Instantiate(box, new Vector3(0, -3 * i, 0), Quaternion.identity) as Transform;
            boxObj.parent = transform;
            boxObj.localPosition = new Vector3(0, -3 * i, 0);
            lastBoxPosition = boxObj.localPosition;
            boxCount++;
        }
        starCount = levelData.stars;
        nextStart = 1;
        int nextStarBlocks = 0;
        for (int j = 0; j < levelData.starsList.Count; j++)
        {
            nextStarBlocks += levelData.starsList[j];
            if (j < levelData.stars)
            {
                continue;
            }
            addStarGameObject(nextStarBlocks*4);
            nextStart = 1;
        }

        for (int j = 0; j < levelData.stars; j++)
        {
            Image starImg = levelStarsPanel.transform.GetChild(j).GetComponent<Image>();
            starImg.sprite = Resources.Load ("GoldStar",typeof(Sprite)) as Sprite;
        }
    }

    public void polygonSetting()
    {
        string currentPolygon = PlayerPrefs.GetString(GameConstants.CURRENTPOLY_STRING, "polygon1");
        if (currentPolygon.Contains("poly"))
        {
            GameAnalytics.NewDesignEvent ("Polygon");
            currentShape = polygon;
            currentShape.GetComponent<SpriteRenderer>().sprite = Resources.Load("polygons/" + currentPolygon, typeof(Sprite)) as Sprite;
            polygon.SetActive(true);
            octagon.SetActive(false);
            circle.SetActive(false);
        }
        else if (currentPolygon.Contains("octa"))
        {
            GameAnalytics.NewDesignEvent ("octagon");
            currentShape = octagon;
            currentShape.GetComponent<SpriteRenderer>().sprite = Resources.Load("polygons/" + currentPolygon, typeof(Sprite)) as Sprite;
            polygon.SetActive(false);
            octagon.SetActive(true);
            circle.SetActive(false);
        }
        else if (currentPolygon.Contains("circle"))
        {
            GameAnalytics.NewDesignEvent ("circle");
            currentShape = circle;
            currentShape.GetComponent<SpriteRenderer>().sprite = Resources.Load("polygons/" + currentPolygon, typeof(Sprite)) as Sprite;
            polygon.SetActive(false);
            octagon.SetActive(false);
            circle.SetActive(true);
        }

        currentShapeName = currentShape.name;
    }

    public void savePolygonPosition()
    {
        if (isGameOver)
            return;
        polygonPreviousYPos = currentShape.transform.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isGameOver)
            return;

        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.childCount == 0)
            {
                Destroy(child.gameObject);
            }
        }

//        if (GameConstants.LEVEL_TO_LOAD != 0)
//        {
//            if (cameraYPos - 4*0.75 > Camera.main.transform.position.y)
//            {
//                Transform box = BoxPattern.getInstance().getRandomBoxPattern();
//                Transform boxObj = Instantiate(box, new Vector3(0, lastBoxPosition.y - 3f, 0), Quaternion.identity) as Transform;
//                boxObj.parent = transform;
//                boxObj.localPosition = new Vector3(0, lastBoxPosition.y - 3f, 0);
//                lastBoxPosition = boxObj.localPosition;
//                groundBox.localPosition = new Vector3(0, lastBoxPosition.y - 3f, 0);
//                addStarGameObject();
//                cameraYPos = Camera.main.transform.position.y;
//            }
//            return;
//        }

        //add new box below
//        if (transform.childCount <= 3)
//        {
//            Transform box = BoxPattern.getInstance().getRandomBoxPattern();
//            Transform boxObj = Instantiate(box, new Vector3(0, lastBoxPosition.y - 3f, 0), Quaternion.identity) as Transform;
//            boxObj.parent = transform;
//            boxObj.localPosition = new Vector3(0, lastBoxPosition.y - 3f, 0);
//            lastBoxPosition = boxObj.localPosition;
//            groundBox.localPosition = new Vector3(0, lastBoxPosition.y - 3f, 0);
//            addStarGameObject();
//        }

        if (cameraYPos - 4*0.75 > Camera.main.transform.position.y)// || transform.childCount <= 4)
        {
            if (GameConstants.LEVEL_TO_LOAD != 0 && boxCount <= levelData.boxList.Count)
            {
                Transform box = BoxPattern.getInstance().getBoxPatternNumber(boxCount);
                addNewBoxAtBottom(box);
                boxCount++;
            }
            else if(GameConstants.LEVEL_TO_LOAD == 0)
            {
                Transform box = BoxPattern.getInstance().getRandomBoxPattern();
                addNewBoxAtBottom(box);
                addStarGameObject(10);
            }
        }
	}

    void addNewBoxAtBottom(Transform box)
    {
        Transform boxObj = Instantiate(box, new Vector3(0, lastBoxPosition.y - 3f, 0), Quaternion.identity) as Transform;
        boxObj.parent = transform;
        boxObj.localPosition = new Vector3(0, lastBoxPosition.y - 3f, 0);
        lastBoxPosition = boxObj.localPosition;
        groundBox.localPosition = new Vector3(0, lastBoxPosition.y - 3f, 0);
        cameraYPos = Camera.main.transform.position.y;
    }

    void addStarGameObject(int blocks)
    {
        Transform starObj = Instantiate(starPrefab, new Vector3(starPrefab.localPosition.x, 
            starPrefab.localPosition.y - (blocks * 0.75f * nextStart), starPrefab.localPosition.z), Quaternion.identity) as Transform;
        starObj.parent = starParent;
        nextStart++;
    }

    public void updateScores(int plusScore, GameObject box)
    {
        popButtonGameObject.SetActive(false);

        SoundManager.Instance.PlayPopSound();
        if (box != null)
        {
            particleGameObject = Instantiate(Resources.Load("collideParitcle", typeof(GameObject)) as GameObject);
            particleGameObject.transform.position = box.transform.position;
            particleGameObject.GetComponent<ParticleSystem>().startColor = box.GetComponent<SpriteRenderer>().color;
            particleGameObject.GetComponent<ParticleSystem>().Play();
        }

        if (GameConstants.LEVEL_TO_LOAD != 0)
        {
            return;
        }

        currentScore += plusScore;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
        }

        currentScoreText.text = "" + currentScore;
        bestScoreText.text = "" + bestScore;
        starText.text = "" + numOfStars;

        if (box != null)
        {
            Transform plusFiveGO = Instantiate(plusFivePrefab, box.transform.position, Quaternion.identity) as Transform;
//            plusFiveGO.parent = box.transform.parent;

            iTween.MoveAdd(plusFiveGO.gameObject, iTween.Hash(
                    "amount", Vector3.up * 1.5f,
                    "easeType", iTween.EaseType.linear,
                    "loopType", iTween.LoopType.none,
                    "time", 1.0f,
                    "oncomplete", "itweenCallback",
                    "oncompletetarget", gameObject,
                    "oncompleteparams", plusFiveGO.gameObject));
            
            iTween.ScaleTo(plusFiveGO.gameObject, Vector3.one*2, 1.0f);
        }
    }

    public void itweenCallback(GameObject obj)
    {
//        Debug.Log("itweenCallback");
        Destroy(obj);
    }

    public void itweenStarCallback(GameObject obj)
    {
        Debug.Log("itweenCallback");
        Destroy(obj.transform.parent.gameObject);
    }

    public void updateStars(int stars, bool isAnimation, GameObject starObj)
    {
        StartCoroutine(updateStars1(stars, isAnimation, starObj));
    }

    public IEnumerator updateStars1(int stars, bool isAnimation, GameObject starObj)
    {
        if (GameConstants.LEVEL_TO_LOAD != 0)
        {
            if (starCount == 2)
            {
                yield return new WaitForSeconds(2.0f);
                if (GameObject.Find(currentShapeName) == null)
                {
                    gameOver();
                    yield return null;
                }
            }
        }

        SoundManager.Instance.PlayStarSound();
//        int numOfStars = ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING, 0);
        numOfStars += stars;
        starText.text = "" + numOfStars;

        if (isAnimation)
        {
            Vector3 moveToPos = new Vector3(starText.transform.position.x, 
                starText.transform.position.y - (polygonPreviousYPos - currentShape.transform.position.y),
                                            starText.transform.position.z);

            iTween.MoveTo(starObj.gameObject, iTween.Hash(
                                "position", moveToPos,
                                "easeType", iTween.EaseType.linear,
                                "loopType", iTween.LoopType.none,
                                "time", 1.0f,
                                "oncomplete", "itweenStarCallback",
                                "oncompletetarget", gameObject,
                                "oncompleteparams", starObj.gameObject));

            
            iTween.ScaleTo(starObj, Vector3.one*2, 1.0f);
            iTween.RotateAdd(starObj, iTween.Hash(
                "amount", Vector3.up * 360,
                "easeType", iTween.EaseType.linear,
                "loopType", iTween.LoopType.none,
                "time", 1.0f));
        }

        if (GameConstants.LEVEL_TO_LOAD != 0)
        {
            starCount++;
            levelData.stars = starCount;
            Image StarImg = levelStarsPanel.transform.GetChild(starCount - 1).GetComponent<Image>();
            StarImg.sprite = Resources.Load ("GoldStar",typeof(Sprite)) as Sprite;
        }

        if (starCount == 3)
            gameOver();
    }

    public void gameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        if (GameConstants.LEVEL_TO_LOAD == 0)
        {
            Camera.main.backgroundColor = Color.black;
            currentScoreText.color = Color.white;
            bestScoreText.color = Color.white;
            lastCallText.enabled = true;
			SoundManager.Instance.PlayFailSound();
        }
        else
        {
            PlayerPrefs.SetInt(GameConstants.LEVELSTARS_STRING + levelData.levelNumber, starCount);
            lastCallText.enabled = true;
            lastCallText.color = Color.black;

            if (starCount == 3 && GameObject.Find(currentShapeName) != null)
            {
                SoundManager.Instance.PlayLevelCompleteSound();
                lastCallText.text = "Level Sucess!";
            }
            else
            {
                lastCallText.text = "Level Failed!";
                SoundManager.Instance.PlayFailSound();
            }
        }

		StartCoroutine("showGameOver");
    }

	void SendScore()
	{
//		Social.ReportScore (currentScore, GameConstants.LEADERBOARD_ID, (bool success) =>
//			{
//				if (success) {
//					Debug.Log ("Update Score Success");
//
//				} else {
//					Debug.Log ("Update Score Fail");
//				}
//			});

        GPSInstanceX.instance.UpdateLeaderboardForID(currentScore);
	}

    IEnumerator successFailText()
    {
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator showGameOver()
    {
        yield return new WaitForSeconds(3.0f);

        int gameOverCount = ZPlayerPrefs.GetInt(GameConstants.GAMEOVER_COUNT_STRING, 0);
        ZPlayerPrefs.SetInt(GameConstants.GAMEOVER_COUNT_STRING, gameOverCount + 1);
		GameAnalytics.NewDesignEvent ("GameOver", (float)gameOverCount);
        GameAnalytics.NewDesignEvent ("GameOverNew");
        ZPlayerPrefs.SetInt(GameConstants.CURRENTSCORE_STRING, currentScore);
        ZPlayerPrefs.SetInt(GameConstants.BESTSCORE_STRING, bestScore);
        ZPlayerPrefs.SetInt(GameConstants.GLOBALSTARS_STRING, numOfStars);

        int socialCount = PlayerPrefs.GetInt(GameConstants.SOCIAL_COUNT_STRING, 0);
        PlayerPrefs.SetInt(GameConstants.SOCIAL_COUNT_STRING, socialCount+1);

        if (gameOverCount % 3 == 0 && GameConstants.LEVEL_TO_LOAD == 0)
        {
//            AdHandler.GetInstance().showInterstatial();
            StartCoroutine("showAd");
        }
        else if(gameOverCount % 3 == 0 && GameConstants.LEVEL_TO_LOAD != 0)
        {
            AdHandler.GetInstance().showInterstatial();
        }


        if (GameConstants.LEVEL_TO_LOAD == 0)
        {
            isPauseMenu = true;
            gameoverPanel.SetActive(true);
            gameoverScoreText.text = "" + currentScore;

            iTween.ScaleTo(gameoverBG.gameObject, iTween.Hash(
                "scale", Vector3.one,
                "easeType", iTween.EaseType.easeInOutBack,
                "loopType", iTween.LoopType.none,
                "time", 0.75f,
                "oncomplete", "gameoverAnimCompleteCB",
                "oncompletetarget", gameObject));

			if (Social.localUser.authenticated)
			{
				SendScore();
			}
			else
			{
				Debug.Log ("User not logged in. Score not sent to leaderboard");
			}
        }
        else
        {
            if (starCount == 3)
            {
                PlayerPrefs.SetInt(GameConstants.LEVEL_STRING + (levelData.levelNumber+1), 1);

                if (levelData.levelNumber + 1 >= 99)
                {
                    GameAnalytics.NewDesignEvent("EndLevel99");
                }

            }
            SceneManager.LoadScene("LevelScene");
        }
    }

    public void pauseButtonClicked()
    {
        SoundManager.Instance.PlayClickSound();
        isPauseMenu = true;
        pauseMenu.SetActive(true);
    }

    public void playButtonClicked()
    {
        SoundManager.Instance.PlayClickSound();
        isPauseMenu = false;
        pauseMenu.SetActive(false);
    }

    public void homeButtonClicked()
    {
        SoundManager.Instance.PlayClickSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void replayButtonClicked()
    {
        SoundManager.Instance.PlayClickSound();
        GameConstants.LEVEL_TO_LOAD = 0;
        SceneManager.LoadScene("GamePlay");
    }

    public void shareButtonClicked()
    {
        SoundManager.Instance.PlayClickSound();
        sharePanelScoreText.text = currentScore.ToString();
        sharePanel.SetActive(true);
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

	public void OnRemoveAdClick()
	{
		SoundManager.Instance.PlayClickSound();
		StoreInventory.BuyItem("removead");
	}


    public void popButtonClicked()
    {
        if (numOfStars < 30)
        {
            AdHandler.GetInstance().showVideoAd(videoActionComplete);
        }
        else
        {
            numOfStars -= 30;
            popButtonGameObject.SetActive(false);
            StartCoroutine("popBlocks1");
        }
    }

    public bool videoActionComplete()
    {
        popButtonGameObject.SetActive(false);
        StartCoroutine("popBlocks1");
        return true;
    }

    IEnumerator popBlocks1()
    {
        yield return new WaitForSeconds(0f);

        PhysicsMaterial2D material = currentShape.GetComponent<Collider2D>().sharedMaterial;
        if (material != null)
        {
            material.bounciness = 0;
            currentShape.GetComponent<Collider2D>().enabled = false;
            currentShape.GetComponent<Collider2D>().enabled = true;
        }

//        for (int i = 0; i < 3; i++)
        {
            StartCoroutine("popBlocks");
        }
    }

    IEnumerator popBlocks()
    {
        //        yield return new WaitForSeconds(0.2f);
        int popedBlocked = 0;

        for (int i = 0; popedBlocked < 9; i++)
        {
            if (gameObject.transform.childCount <= 1)
            {
                Debug.Log("test");
            }

            Transform box = gameObject.transform.GetChild(1);

            if (box.name.Contains("box") || box.name.Contains("Box"))
            {
                for (int j = 0; j < box.childCount; j++)
                {
                    Transform child = box.GetChild(j);

                    if (!child.name.Contains("shape"))
                    {
                        continue;
                    }
                    updateScores(10, child.gameObject);
//                    Destroy(child.gameObject);
                }

                Destroy(box.gameObject);

                popedBlocked++;
                yield return new WaitForSeconds(0.259f);
                //                Destroy(box.gameObject);
            }

        }

//        PhysicsMaterial2D material = currentShape.GetComponent<Collider2D>().sharedMaterial;
//        if(material != null)
//        {
//            material.bounciness = 0.0f;
//        }

        StartCoroutine("enableBounce");
    }

    IEnumerator enableBounce()
    {
        yield return new WaitForSeconds(1.5f);
        PhysicsMaterial2D material = currentShape.GetComponent<Collider2D>().sharedMaterial;
        if(material != null)
        {
            material.bounciness = 0.3f;
            currentShape.GetComponent<Collider2D>().enabled = false;
            currentShape.GetComponent<Collider2D>().enabled = true;
        }
    }

    IEnumerator showAd()
    {
        yield return new WaitForSeconds(0.7f);
        Debug.Log("Arslan::gameoverAnimCompleteCB");
        AdHandler.GetInstance().showInterstatial();
    }

    public void gameoverAnimCompleteCB()
    {
//        Debug.Log("Arslan::gameoverAnimCompleteCB");
//        AdHandler.GetInstance().showInterstatial();

//        if(gameOverCount%3 == 0)
//        {
//            //            AdHandler.GetInstance().showInterstatial();
//            //            StartCoroutine("showAd");
//        }

    }

}
