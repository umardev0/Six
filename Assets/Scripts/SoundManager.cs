using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

	public AudioClip clickSound;
	public AudioClip failSound;
	public AudioClip popSound;
	public AudioClip starSound;
    public AudioClip levelCompleteSound;
	

    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake() 
    {
        Debug.Log("Awake Called");
        if(_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if(this != _instance)
                Destroy(gameObject);
        }
    }


	//public AudioClip grinderOffSound;

//	public void PlaySwooshSound()
//	{
//		AudioSource.PlayClipAtPoint(swoosh ,transform.position);
//	}
//
//	public void PlayMegaWinSound()
//	{
//		AudioSource.PlayClipAtPoint(megWin ,transform.position);
//	}
//
	public void PlayFailSound()
	{
		AudioSource.PlayClipAtPoint(failSound ,transform.position);
	}

	public void PlayClickSound()
	{
        AudioSource.PlayClipAtPoint(clickSound ,transform.position);
	}

	public void PlayStarSound()
	{
		AudioSource.PlayClipAtPoint(starSound ,transform.position);
	}

    public void PlayLevelCompleteSound()
    {
        AudioSource.PlayClipAtPoint(levelCompleteSound ,transform.position);
    }

	public void PlayPopSound()
	{
        AudioSource.PlayClipAtPoint(popSound ,transform.position);
	}

//	public void PlayPointDiceSound()
//	{
//		AudioSource.PlayClipAtPoint(pointDiceSound ,transform.position);
//	}
//
//
//	public void PlayShowStepSound()
//	{
//		AudioSource.PlayClipAtPoint(showStepSound ,transform.position);
//	}
//
//	public void PlayTriplesSound()
//	{
//		AudioSource.PlayClipAtPoint(triplesSound ,transform.position);
//	}
//
//	public void PlayTrifectaSound()
//	{
//		AudioSource.PlayClipAtPoint(trifectaWin ,transform.position);
//	}
//
//	public void PlayLoseSound()
//	{
//		AudioSource.PlayClipAtPoint(trifectaWin ,transform.position);		
//	}
//
//	public void PlayStartGameSound()
//	{
//		AudioSource.PlayClipAtPoint(startGame ,transform.position);		
//	}

}
