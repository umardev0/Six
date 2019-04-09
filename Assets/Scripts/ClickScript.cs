using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ClickScript : MonoBehaviour 
{
    GamePlay gamePlayScript;
	void Start()
    {
        gamePlayScript = GameObject.Find("GamePlay").GetComponent<GamePlay>();
    }
	
	void Update()
    {
        if (gamePlayScript.isPauseMenu)
        {
            return;
        }

	
        if (Input.GetMouseButtonDown (0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);

//            Debug.Log ("mouse pos " + mousePosition.x + " y " + mousePosition.y + " ");    

            if (hitCollider) 
            {
                if (hitCollider.gameObject == gameObject)
                {
                    gamePlayScript.savePolygonPosition();
                    gamePlayScript.updateScores(5, gameObject);
                    Destroy(gameObject);
                }
            }
        }
	}


    void FixedUpdate()
    {
//        var currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
//
//        if (currentVelocity.y <= 0f) 
//            return;
//
//        currentVelocity.y = 0f;
//
//        gameObject.GetComponent<Rigidbody2D>().velocity = currentVelocity;
    }

}
