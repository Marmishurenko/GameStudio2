using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextingNarrative : MonoExtended {

    [SerializeField] float FADE_IN_TIME;
    [SerializeField] float DISPLAY_TIME;
    [SerializeField] float FADE_OUT_TIME;
    [SerializeField] float MAX_ALPHA;
    [SerializeField] string[] cycleOne;
    [SerializeField] string[] cycleTwo;
    [SerializeField] string[] cycleThree;

    Text text;
    int lineIndex = -1;
    int state;
    float timer;
    string[] lines = null;

    void Start() {
        text = gameObject.GetComponent<Text>();
        text.text = null;
        state = 0;
        switch (gameManager.textingSceneStage) {
            case 0:
                lines = cycleOne;
                break;
            case 1:
                lines = cycleTwo;
                break;
            case 2:
                lines = cycleThree;
                break;
        }
    }

    void Update() {
        timer -= Time.deltaTime;

        Color c;
        switch (state) {
            case 1:
                c = text.color;
                c.a = (1 - timer / FADE_IN_TIME) * MAX_ALPHA;
                text.color = c;
                if (timer <= 0) {
                    state = 0;
                    timer = DISPLAY_TIME;
                }
                break;

            case 0:
                if (timer <= 0) {
                    state = -1;
                    timer = FADE_OUT_TIME;
                }
                break;

            case -1:
                if (timer >= 0) {
                    c = text.color;
                    c.a = timer / FADE_OUT_TIME * MAX_ALPHA;
                    text.color = c;
                }
                break;
        }
    }

    public void Show() {
        state = 1;
        timer = FADE_IN_TIME;
        lineIndex++;
        text.text = lines[lineIndex];
    }

    protected override void GameUpdate() {
    }
}