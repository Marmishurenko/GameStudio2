using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;


	public int health = 0;
	public float time = 0;
	public int bestHealth;
	public string sceneName;



	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			instance.health = 0;
			Destroy(gameObject); // THERE CAN BE ONLY ONE!
		}
		time = Time.time;

	
		if (PlayerPrefs.HasKey("bestHealth")) {
			bestHealth = PlayerPrefs.GetInt("bestHealth");
		}


	}

	// Update is called once per frame
	void Update () {
		
		time++;
		if (time == 1000) {
			EndGame ();
		}

	}

	public void EndGame(){
		 
		if (health > bestHealth) {
				bestHealth = health;

			PlayerPrefs.SetInt ("bestHealth",bestHealth);
				PlayerPrefs.Save ();
			}
		time = 0f;
		SceneManager.LoadScene(sceneName);



	}



}
