using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public int health = 10;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			instance.health = 10;

			Destroy(gameObject); // THERE CAN BE ONLY ONE!
		}

	}

	// Update is called once per frame
	void Update () {

	}
}
