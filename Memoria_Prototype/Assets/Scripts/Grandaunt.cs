﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grandaunt : MonoBehaviour {
    
	void Start () {
        transform.GetChild(0).GetComponent<Text>().text = "";
    }

    public void TurnOn(int type) {
        gameObject.GetComponent<Image>().enabled = true;
        StartCoroutine("ShowText", type);
    }

    IEnumerator ShowText(int rightCount) {
        yield return new WaitForSeconds(1.2f);
        switch (rightCount) {
            case 3:
                transform.GetChild(0).GetComponent<Text>().text = "\"Thank you! I love you.\"";
                break;
            case 2:
            case 1:
                transform.GetChild(0).GetComponent<Text>().text = "\"Thank you, but please get me the right ones next time.\"";
                break;
            case 0:
                transform.GetChild(0).GetComponent<Text>().text = "\"Thank you, but I don't want them anymore.\"";
                break;
        }
        yield return new WaitForSeconds(2.5f);
        transform.GetChild(0).GetComponent<Text>().text = "";
        yield return new WaitForSeconds(1);
        GameObject.Find("GameManager").GetComponent<GameManager>().NextPhase();
    }
}
