using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimController : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            gameObject.GetComponent<Animator>().enabled = true;
    }

    public void Pause() {
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
