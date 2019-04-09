using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenericPopup 
{
    GameObject popup;
    string m_popupName;
    string m_url;

    public void create(string title, string desc, GameObject popupPrefab, GameObject parent, string popupName, string url)
    {
        popup = GameObject.Instantiate(popupPrefab, popupPrefab.transform.position, Quaternion.identity) as GameObject;
        popup.transform.parent = parent.transform;
        popup.transform.localScale = Vector3.one;
        popup.transform.position = Vector3.zero;

        Text titleText = popup.transform.Find("Title").GetComponent<Text>();
        Text descText  = popup.transform.Find("Description").GetComponent<Text>();
        Button yesBtn = popup.transform.Find("YesBtn").GetComponent<Button>();
        Button noBtn  = popup.transform.Find("NoBtn").GetComponent<Button>();

        titleText.text = title;
        descText.text  = desc;
        m_popupName = popupName;
        m_url = url;


        yesBtn.transition = Selectable.Transition.ColorTint;
        yesBtn.onClick.AddListener(delegate() {
            yesBtnClicked();
        });

        noBtn.transition = Selectable.Transition.ColorTint;
        noBtn.onClick.AddListener(delegate() {
            noBtnClicked();
        });

    }

    void yesBtnClicked()
    {
        if (m_popupName == "facebook")
        {
            Application.OpenURL(m_url);
            PlayerPrefs.SetInt(GameConstants.IS_FACEBOOK_DONE_STRING, 1);
        }
        else if (m_popupName == "instagram")
        {
            Application.OpenURL(m_url);
            PlayerPrefs.SetInt(GameConstants.IS_INSTAGRAM_DONE_STRING, 1);
        }

        GameObject.Destroy(popup);
    }

    void noBtnClicked()
    {
        PlayerPrefs.SetInt(GameConstants.SOCIAL_COUNT_STRING, 0);
        GameObject.Destroy(popup);
    }
}
