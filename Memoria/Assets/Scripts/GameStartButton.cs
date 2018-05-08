using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoExtended {

    protected override void GameUpdate() {
        if (Input.GetMouseButtonDown(0)) {
            gameManager.LoadTransitionScene();
            GameObject.Find("StartButtonSound").GetComponent<AudioSource>().Play();
            GameObject.Find("StartButtonSound").GetComponent<AudioFadeOut>().FadeOut();
            GameObject.Find("WindEffect").GetComponent<AudioFadeOut>().FadeOut();
        }
    }
}
