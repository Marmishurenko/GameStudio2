using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFadingMusic : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeMusic (AudioSource oldSource, AudioSource newSource, float crossfadeTime) {

		StartCoroutine (changeMusicCoRoutine(oldSource, newSource, crossfadeTime)); 

	}

	public IEnumerator changeMusicCoRoutine (AudioSource oldSource, AudioSource newSource, float crossfadeTime) {
	
		float elapsedTime = 0; 
		while (elapsedTime < crossfadeTime) {

			newSource.volume = elapsedTime / crossfadeTime; 
			oldSource.volume = 1 - newSource.volume; 
			elapsedTime += Time.deltaTime; 
			yield return null; 
		} 
	
	}
		
}
