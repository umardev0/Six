using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class PolyStoreScript : MonoBehaviour 
{
	public GameObject starIMG;
	public GameObject polyIMG;
	public GameObject priceTXT;
	public GameObject lockedIMG;
	public GameObject selectedIMG;

    public Text starText;

	//public Color myColor;
//	public int myIndex;
	public int myPrice;
//    int starCount = 0;


	// Use this for initialization
	void Start () 
	{
		// selectedImg
		//myColor = GetComponent<Image>().color;
//		int.TryParse(gameObject.name, out myIndex);

		//myPrice = GameManager.Instance.getPriceOfBall(myIndex);
		// attempt to parse the value using the TryParse functionality of the integer type

//        PlayerPrefs.DeleteAll();

		starIMG = transform.Find("Image").gameObject;
		polyIMG = transform.Find("polyImg").gameObject;
		priceTXT = starIMG.transform.Find("Text").gameObject;
		lockedIMG = transform.Find("lockImage").gameObject;
		selectedIMG = transform.Find("selectedImg").gameObject;

		polyIMG.GetComponent<Image>().sprite = Resources.Load("polygons/"+gameObject.name, typeof(Sprite)) as Sprite;
		polyIMG.GetComponent<Image>().SetNativeSize();
		priceTXT.GetComponent<Text>().text = myPrice.ToString();

//        starCount = ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING);
//        starText.text = "" + starCount;
		if(PlayerPrefs.GetInt(gameObject.name, 0) == 0)
		{
			polyIMG.GetComponent<Image>().color = Color.clear;
			polyIMG.SetActive(false);
			selectedIMG.SetActive(false);
			priceTXT.SetActive(true);
			starIMG.SetActive(true);

		}
		else
		{
			polyIMG.GetComponent<Image>().color = Color.white;
            if(PlayerPrefs.GetString(GameConstants.CURRENTPOLY_STRING, "polygon1") == gameObject.name)
			{
				selectedIMG.SetActive(true);
			}
			else
			{
				selectedIMG.SetActive(false);
			}
			polyIMG.SetActive(true);
			priceTXT.SetActive(false);
			starIMG.SetActive(false);
		}

		Button clickButton = GetComponent<Button>();
		clickButton.transition = Selectable.Transition.ColorTint;
		clickButton.onClick.AddListener(delegate() {

			onBuy();

		});
	}

    void Update()
    {
        string currentPolygon = PlayerPrefs.GetString(GameConstants.CURRENTPOLY_STRING, "polygon1");

        if (currentPolygon == gameObject.name)
        {
            selectedIMG.SetActive(true);
        }
        else
        {
            selectedIMG.SetActive(false);
        }

    }

	public void onBuy()
	{
		SoundManager.Instance.PlayClickSound();
		if(PlayerPrefs.GetInt(gameObject.name, 0) == 0)
		{
            if(ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING) >= myPrice)
			{
                GameAnalytics.NewDesignEvent ("CharacterBuy");
                BuyRout();
			}
			else
			{
				SceneManager.LoadScene("StarShopScene");
			}
		}
		else
		{
            PlayerPrefs.SetString(GameConstants.CURRENTPOLY_STRING, gameObject.name);
			selectedIMG.SetActive(true);
			//GameManager.Instance.refreshStoreBtns();
		}
	}

	void BuyRout()
	{
       // starCount -= myPrice;
        PlayerPrefs.SetString(GameConstants.CURRENTPOLY_STRING, gameObject.name);

		PlayerPrefs.SetInt(gameObject.name, 1);
        GameManager.Instance.subStars(myPrice);
	//	GameManager.Instance.refreshStoreBtns();
		polyIMG.GetComponent<Image>().color = Color.white;
        polyIMG.SetActive(true);
		selectedIMG.SetActive(true);
		priceTXT.SetActive(false);
        lockedIMG.SetActive(false);
        starIMG.SetActive(false);
		starText.text = "" + ZPlayerPrefs.GetInt(GameConstants.GLOBALSTARS_STRING);
	}

	public void refreshSelected()
	{
		if(ZPlayerPrefs.GetString("CURRENTBALL","0") == gameObject.name)
		{
			selectedIMG.SetActive(true);
		}
		else
		{
			selectedIMG.SetActive(false);
		}
	}
}
