using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOutro : MonoBehaviour {
    private AudioSource aus;
	// Use this for initialization
	void Start () {
        aus = gameObject.GetComponent<AudioSource>();
        aus.Play();
	}
	
	// Update is called once per frame
	void Update () {
      
		
	}
}
