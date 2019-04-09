using UnityEngine;
using System.Collections;

public class FollowScript : MonoBehaviour
{
    private Vector3 initialPosition;
    public GameObject destroyer;
    public GameObject destroyerLeft;
    public GameObject destroyerRight;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x <= -2 || transform.position.x >= 2)
        {
            return;
        }
        float previousPos = Camera.main.transform.position.y;
        Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, transform.position.y - initialPosition.y,Camera.main.transform.position.z);

        float deltaPos = previousPos - Camera.main.transform.position.y;
        destroyer.transform.position = new Vector3 (destroyer.transform.position.x, destroyer.transform.position.y - deltaPos, destroyer.transform.position.z);
        destroyerLeft.transform.position = new Vector3 (destroyerLeft.transform.position.x, destroyerLeft.transform.position.y - deltaPos, destroyerLeft.transform.position.z);
        destroyerRight.transform.position = new Vector3 (destroyerRight.transform.position.x, destroyerRight.transform.position.y - deltaPos, destroyerRight.transform.position.z);
	}
}
