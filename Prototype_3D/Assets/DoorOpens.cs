using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorOpens : MonoBehaviour {
	Animator anim;
	public string scene; 

	// Use this for initialization
	void Start () {
		
		
	}

	// Update is called once per frame
	void Update () {
		
	}
	 
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			StartCoroutine (NextScene ());
			anim = gameObject.GetComponent<Animator> ();
			anim.Play("DoorOpens");
		}

	}


	IEnumerator NextScene(){
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene (scene);

	}


}
