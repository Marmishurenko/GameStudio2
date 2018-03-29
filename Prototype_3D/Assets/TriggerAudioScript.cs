using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudioScript : MonoBehaviour {

	public AudioSource mySource;
	bool hasPlayed;

	// Use this for initialization
	void Start () {
		
		hasPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (!mySource.isPlaying) {
				mySource.Play ();
				hasPlayed = true;
			}
		}
	}
}
