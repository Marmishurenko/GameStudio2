using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingBubble : MonoBehaviour {
    
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
	}
	
	void Update () {
		
	}

    public void TurnOn() {
        transform.position = new Vector3(-1.874311f, -1.558f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("MessageList").GetComponent<MessageList>().SetTargetPos(0.6f);
    }

    public void TurnOff() {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("MessageList").GetComponent<MessageList>().SetTargetPos(-0.6f);
    }
}
