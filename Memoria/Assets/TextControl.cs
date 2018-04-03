using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextControl : MonoBehaviour {

    public TMP_Text yumiText;
    public string[] stringsTexts;
    private int textCounter = 0;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
        yumiText.SetText(stringsTexts[textCounter]);
		
	}

    public void UpdateTextCounter(){
        textCounter++;
    }
}
