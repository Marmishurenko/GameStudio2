using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoExtended {

    [SerializeField] float DURATION;

    float timer;

    void Start() {
        timer = DURATION;
    }

    protected override void GameUpdate() {
        timer -= Time.deltaTime;
        if (timer <= 0)
            gameManager.LoadNextGameScene();
    }
}
