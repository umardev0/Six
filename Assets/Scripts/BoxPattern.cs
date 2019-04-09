using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxPattern : MonoBehaviour
{
    public List<Transform> boxesPrefabs;
    public Transform groundBoxPrefab;
    static BoxPattern m_instance = null;

    Color baseColor = new Color(0.243f, 0.151f, 0.0f);

    public Color[] ColourValues = new Color[] { 
        new Color(220f/255f, 190f/255f, 45.0f/255f),
        new Color(207f/255f, 135f/255f, 56.0f/255f),
        new Color(204f/255f, 111f/255f , 75f/255f),
        new Color(200f/255f, 100f/255f  , 110f/255f),
        new Color(200f/255f, 90.0f/255f, 145f/255f),
        new Color(180f/255f , 80f/255f , 140f/255f),
        new Color(150f/255f , 100f/255f , 180f/255f),
        new Color(130f/255f , 120f/255f , 200f/255f),
        new Color(120f/255f , 133f/255f, 218f/255f),
        new Color(122f/255f , 156f/255f, 230f/255f),
        new Color(120f/255f , 170f/255f, 230f/255f),
        new Color(130f/255f , 190f/255f, 230f/255f),
        new Color(110f/255f , 180f/255f, 220f/255f),
        new Color(130f/255f , 205f/255f, 230f/255f),
        new Color(96f/255f , 150f/255f, 210f/255f),
        new Color(100f/255f , 120f/255f, 230f/255f),
    };

    int count = 0;


    public static BoxPattern getInstance()
    {
        if(m_instance == null)
        {
            m_instance = GameObject.FindObjectOfType<BoxPattern>();
            DontDestroyOnLoad(m_instance.gameObject);
        }

        return m_instance;
    }

    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            m_instance.count = 0;
            Destroy(gameObject);
        }
    }

	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public Transform getRandomBoxPattern()
    {
        int randomIndex = Random.Range(0, boxesPrefabs.Count);
        Transform randomBox = boxesPrefabs[randomIndex];
        colorBoxInPatteren(randomBox);
        return randomBox;
    }

    public Transform getBoxPatternNumber(int number)
    {
//        int randomIndex = Random.Range(0, boxesPrefabs.Count);
        Transform randomBox = boxesPrefabs[number-1];
        colorBoxInPatteren(randomBox);
        return randomBox;
    }

    public Transform getGroundBox()
    {
        return groundBoxPrefab;
    }

    void colorBoxInPatteren(Transform box)
    {
        Color baseColor1 = ColourValues[count];
        Color baseColor2 = ColourValues[count+1];
        for(int i = 0; i < box.childCount; i++)
        {
            int val = Random.Range(0, 2);
            if (val == 0)
                baseColor = baseColor1;
            else
                baseColor = baseColor2;

            Transform child = box.GetChild(i);

            float offset = 0.09f;
            float random = Random.value;
            float value = (baseColor.r + baseColor.g + baseColor.b)/3.0f;
            float newValue = value + (1.0f * random * offset) - offset;
            float valueRatio = newValue / value;

            Color newColor = new Color();
            newColor.r = baseColor.r * valueRatio;// + 0.002f*i;
            newColor.g = baseColor.g * valueRatio;// + 0.002f*i;
            newColor.b = baseColor.b * valueRatio;// + 0.002f*i;
            newColor.a = 1f;



            child.GetComponent<SpriteRenderer>().color = newColor;
        }
        count++;

        if (count >= ColourValues.Length-1)
            count = 0;
    }

}
