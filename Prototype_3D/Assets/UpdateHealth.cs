using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateHealth : MonoBehaviour {

	Text myText;


	// Use this for initialization
	void Start () {
		myText = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		myText.text = "Health " + GameManager.instance.health + "Best Health " + GameManager.instance.bestHealth + " Time: " + GameManager.instance.time.ToString() ;
		
	}


}
