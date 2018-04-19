using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class garbageControl : MonoExtended {
    int garbageCounter = 0;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    protected override void GameUpdate() {
        if (garbageCounter == 5) {
            gameManager.LoadTransitionScene();
        }
        print(garbageCounter);

    }

    void OnTriggerEnter2D(Collider2D other) {

        other.GetComponent<SpriteRenderer>().enabled = false;
        if (other.GetComponentInChildren<TMP_Text>() != null) {
            other.GetComponentInChildren<TMP_Text>().enabled = false;
        }
        garbageCounter++;
    }
}
