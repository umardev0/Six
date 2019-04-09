using UnityEngine;
using System.Collections;

public class GroundCollider : MonoBehaviour {

    GamePlay gamePlayScript;
    void Start()
    {
        gamePlayScript = GameObject.Find("GamePlay").GetComponent<GamePlay>();
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
//            Destroy(collider.gameObject);
            StartCoroutine("gameOver");
            return;
        }

//        Destroy(collider.gameObject);
    }

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(1.0f);
        if(gamePlayScript.starCount == 3)
            gamePlayScript.gameOver();
    }
}
