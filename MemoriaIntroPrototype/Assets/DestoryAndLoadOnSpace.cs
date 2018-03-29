using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAndLoadOnSpace : MonoBehaviour {

	public GameObject[] imageGroupPrefabs;
	private int currentGroupIndex = 0;
	private GameObject currentImageGroup;

	// Use this for initialization
	void Start () {
		currentImageGroup = Instantiate (imageGroupPrefabs [0]);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && currentGroupIndex < imageGroupPrefabs.Length-1) {
			LoadNextGroup ();
		}
	}

	void LoadNextGroup(){
		Destroy (currentImageGroup);
		currentGroupIndex += 1;
		currentImageGroup = Instantiate (imageGroupPrefabs [currentGroupIndex]);
	}
}
