using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// General game manager which exists through the whole life cycle of the game
public class GameManager : MonoBehaviour {

    // TODO tooltip
    // TODO Text mesh pro
    public GAME_STATE gameState;    // Controls the general state of the game, like running, paused, etc.

    void Start() {
        DontDestroyOnLoad(gameObject);
        gameState = GAME_STATE.RUNNING;
    }

    void Update() {

    }
}


public enum GAME_STATE { RUNNING, PAUSED };