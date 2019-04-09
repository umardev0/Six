using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelStarsScript : MonoBehaviour 
{
	public List<GameObject> starsList = new List<GameObject>();

	void Awake()
	{
		int childStarsCount = transform.childCount;
		for (int i = 0; i < childStarsCount; ++i)
		{
			starsList.Add(transform.GetChild(i).gameObject);
		}

		foreach(GameObject st in starsList)
		{
			st.GetComponent<Image>().sprite = Resources.Load ("GreyStar",typeof(Sprite)) as Sprite;
		}
	}

//	void Start () 
//	{
//		
//	
//	}

	public void setStars(int si)
	{
		for( int i = 0; i < si ; i++ )
		{
			starsList[i].GetComponent<Image>().sprite = Resources.Load ("GoldStar",typeof(Sprite)) as Sprite;
		}
	}

}
