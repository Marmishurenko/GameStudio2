using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnCubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//check if mouse button is down
		if (Input.GetMouseButtonDown (0)) {
			//create a ray from us through the direction the camera is facing
			Ray ray = new Ray (transform.position, Camera.main.transform.forward);
			RaycastHit hit;

			//trigger the sound on the cube if the ray hits it
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.tag == "Audio Cube") {
					hit.collider.gameObject.GetComponent<AudioSource> ().Play ();
				}
			}
		}
	}
			
}