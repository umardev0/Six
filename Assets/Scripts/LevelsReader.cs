using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData
{
    public int levelNumber;
    public int stars;
    public List<int> boxList = new List<int>();
    public List<int> starsList = new List<int>();
}

public class LevelsReader
{
    Dictionary<string, LevelData> levelsData = new Dictionary<string, LevelData>();
    static LevelsReader m_instance = null;

    public static LevelsReader getInstance()
    {
        if (m_instance == null)
        {
            m_instance = new LevelsReader();
            m_instance.readLevelsFromJSON();
        }

        return m_instance;
    }

    public void readLevelsFromJSON()
    {
        TextAsset asset = Resources.Load("LevelData") as TextAsset;
        string datafileContent = asset.text;
        JSONObject json = new JSONObject(datafileContent);

        int count = json.Count;

        for (int i = 1; i <= json.Count; i++)
        {
            LevelData levelData = new LevelData();
            JSONObject levelDataJson = json["level" + i];
            levelData.levelNumber = (int)levelDataJson["levelNum"].n;
            List<JSONObject> boxes = levelDataJson["boxNumbers"].list;
            List<JSONObject> starsList = levelDataJson["stars"].list;
            for (int j = 0; j < boxes.Count; j++)
            {
                int boxNum = (int)boxes[j].n;
                levelData.boxList.Add(boxNum);
            }

            for (int j = 0; j < starsList.Count; j++)
            {
                int starNum = (int)starsList[j].n;
                levelData.starsList.Add(starNum);
            }

            int stars = PlayerPrefs.GetInt(GameConstants.LEVELSTARS_STRING + i, 0);
            levelData.stars = stars;
            levelsData.Add("level" + i, levelData);
        }
    }

    public LevelData getDataForLevel(int levelNum)
    {
        string key = "level" + levelNum;
        LevelData data = levelsData[key];
        return data;
    }

}
