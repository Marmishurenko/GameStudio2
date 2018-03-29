using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extended MonoBehaviour class. Should be used instead
// Check MonoExtendExample class for example
public abstract class MonoExtended : MonoBehaviour {

    protected GameManager gameManager;    // Every MonoExtended class has direct access to GamaManager

    protected void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        // Game state like running or paused
        switch (gameManager.gameState) {
            case GAME_STATE.RUNNING:
                GameUpdate();
                break;

            case GAME_STATE.PAUSED:
                break;
        }
    }

    protected abstract void GameUpdate();
}
