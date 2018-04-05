using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextingBackButton : MonoExtended {

    public bool flashing = false;
    float timer = 0;

    void Start() {
    }

    protected override void GameUpdate() {
        if (flashing == false)
            return;

        timer -= Time.deltaTime;
        float period = 2.5f;
        Color c = gameObject.GetComponent<Image>().color;
        c.a = (Mathf.Sin((period - timer) / period * Mathf.PI * 2) + 1) * 0.5f * 0.8f + 0.2f;
        gameObject.GetComponent<Image>().color = c;

        if (timer <= 0)
            timer = period;
    }

    public void OnClick() {
        if (flashing) {
            gameManager.textingSceneStage++;
            gameManager.LoadTransitionScene();
        }
    }
}
