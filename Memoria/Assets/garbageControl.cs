using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class garbageControl : MonoBehaviour {
    int garbageCounter = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (garbageCounter==5){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
		
	}

 void OnTriggerEnter2D(Collider2D other)
	{
     
        other.GetComponent<SpriteRenderer>().enabled = false;
        if (other.GetComponentInChildren<TMP_Text>() != null)
        {
            other.GetComponentInChildren<TMP_Text>().enabled = false;
        }
        garbageCounter++;


	}
}
