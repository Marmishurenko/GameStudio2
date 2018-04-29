using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrubbingSound : MonoBehaviour {

	public AudioSource wipingSound; 
	public Rigidbody2D spongeRB; 
	public float newVolume = 0; 

	// Use this for initialization
	void Start () {

		spongeRB = GetComponent<Rigidbody2D> (); 
		wipingSound.volume = 0; 
		
	}
	
	// Update is called once per frame
	void Update () {

		wipingSound.volume = Mathf.Lerp (wipingSound.volume, newVolume, Time.deltaTime);  
	}

	void OnTriggerStay2D (Collider2D other) {

		if (other.tag == "Aunt"){

			// Clamping: means it will never go below 0 and above 1. 
			newVolume = Mathf.Clamp (spongeRB.velocity.magnitude, 0, 1); 

		}

	} 
}
