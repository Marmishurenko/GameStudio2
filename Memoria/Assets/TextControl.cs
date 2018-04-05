using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextControl : MonoExtended {

    public TMP_Text yumiText;
    public string[] stringsTexts;
    private int textCounter = 0;
    public float fadeTime = 0.2f;
    float timer=1f;
    float curColor;
    float endColor = 0f;
   

	// Use this for initialization
	void Start () {
        curColor = yumiText.color.a;
       
	}
	
	// Update is called once per frame
	void Update () {
        yumiText.SetText(stringsTexts[textCounter]);
        //print(textCounter);
        if (textCounter == stringsTexts.Length-1)
        {
            Debug.Log("load");
            gameManager.LoadTransitionScene();
        }
		
	}

    public void UpdateTextCounter(){
        textCounter++;
       
    }


    public void LerpTextColor(){

        timer = Time.deltaTime / fadeTime;
            curColor = Mathf.Lerp(curColor, 0, timer);
            yumiText.color = new Color(yumiText.color.r, yumiText.color.g, yumiText.color.b, curColor);
           // print(curColor);
    }

    public void ResetTextColor(){
        timer = 1f;
        curColor = 1f;
        
    }

    protected override void GameUpdate() {
    }

    //    with care I clean auntie nena
    //I tell her about my dreams
    //I tell her about my friends
    //I tell her about my life
    //with care I wash her
    //I ask “auntie, how do you feel today”
    //in Tagalog she grumbles “be quiet”


}
