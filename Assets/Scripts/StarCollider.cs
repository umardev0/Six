using UnityEngine;
using System.Collections;

public class StarCollider : MonoBehaviour {

    GamePlay gamePlayScript;
    bool isSend = false;

	// Use this for initialization
	void Start () {
        gamePlayScript = GameObject.Find("GamePlay").GetComponent<GamePlay>();
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "polygon" || collider.gameObject.name == "octagon" || collider.gameObject.name == "circle")
        {
            if (isSend)
                return;
            isSend = true;
            gamePlayScript.updateStars(1, true, gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if ((collider.gameObject.name == "polygon" || collider.gameObject.name == "octagon" || collider.gameObject.name == "circle") && gameObject.name.Contains("groundBox"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gamePlayScript.updateStars(1, true, gameObject);
        }
    }
}
