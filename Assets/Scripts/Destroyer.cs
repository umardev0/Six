using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    GamePlay gamePlayScript;
    void Start()
    {
        gamePlayScript = GameObject.Find("GamePlay").GetComponent<GamePlay>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
//        Debug.Log("collison");

        if (collider.gameObject.name == "StarOn@2x")
        {
            return;
        }

        if (collider.gameObject.name == "polygon" || collider.gameObject.name == "octagon" || collider.gameObject.name == "circle")
        {
            Destroy(collider.gameObject);
            StartCoroutine("gameOver");
            return;
        }

        Destroy(collider.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collison");

        if (collision.gameObject.name.Contains("groundBox"))
        {
            return;
        }

        if (collision.gameObject.name == "polygon" || collision.gameObject.name == "octagon" || collision.gameObject.name == "circle")
        {
            Destroy(collision.gameObject.GetComponent<Collider2D>().gameObject);
            StartCoroutine("gameOver");
            return;
        }

        Destroy(collision.gameObject);
    }

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(1.0f);
        gamePlayScript.gameOver();
    }

//    void OnCollisionStay2D(Collision2D collision)
//    {
//        Debug.Log("collison");
//        Destroy(collision.gameObject);
//    }
//
//    void OnCollisionExit2D(Collision2D collision)
//    {
//        Debug.Log("collison");
//        Destroy(collision.gameObject);
//    }
}
