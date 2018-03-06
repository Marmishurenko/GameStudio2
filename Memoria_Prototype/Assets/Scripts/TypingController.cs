using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingController : MonoBehaviour {

    [SerializeField] string text;
    [SerializeField] string speed;
    [SerializeField] float[] speedLevels;

    Text uiText;
    float inputCounter = 0;

    void Start() {
        uiText = gameObject.GetComponent<Text>();
        text = uiText.text;
        uiText.text = "";
    }

    void Update() {
        for (int i = 0; i < 26; i++) {
            if (text.Length == 0)
                break;
            if (Input.GetKeyDown(KeyCode.A + i)) {
                //inputCounter += speedLevels[speed[0] - '1'];
                uiText.text = uiText.text + text[0];
                text = text.Remove(0, 1);
                uiText.text = uiText.text + text[0];
                text = text.Remove(0, 1);
                continue;
                inputCounter += 0.5f;
                if (inputCounter >= 1) {
                    inputCounter -= 1;
                    uiText.text = uiText.text + text[0];
                    text = text.Remove(0, 1);
                    //speed = speed.Remove(0, 1);
                }
            }
        }
    }
}
