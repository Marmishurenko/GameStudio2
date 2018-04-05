using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A sample of implementing MonoExtended instead of MonoBehaviour
public class MonoExtendedSample : MonoExtended {

    void Start() {

    }

    protected override void GameUpdate() {
        // Put game logic here so everything is under control of GameManager
        Debug.Log("Game is running.");
    }
}
