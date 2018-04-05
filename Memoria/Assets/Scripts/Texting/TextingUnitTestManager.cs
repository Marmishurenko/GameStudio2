using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextingUnitTestManager : GameManager {

    void Start() {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1) {
            Destroy(gameObject);
            return;
        }
        textingSceneStage = 0;
        DontDestroyOnLoad(gameObject);
    }

    public override void LoadTransitionScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
