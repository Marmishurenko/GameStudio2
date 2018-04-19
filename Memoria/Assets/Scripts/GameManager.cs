using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// General game manager which exists through the whole life cycle of the game
public class GameManager : MonoBehaviour {

    [Tooltip("Controls the general state of the game.")]
    public GAME_STATE gameState;

    [Tooltip("Keeps track of the progress of the game.")]
    public int gameStage;

    public SceneAsset transition;

    [Tooltip("The order of execution of game scenes.")]
    [SerializeField]
    SceneAsset[] sceneArray;

    public int textingSceneStage;

    [SerializeField] GameObject cursorPrefab;

    void Awake() {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1) {
            Destroy(gameObject);
            return;
        }
        GameObject cursorObject = Instantiate(cursorPrefab);
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        gameState = GAME_STATE.RUNNING;
        gameStage = -1;
        textingSceneStage = 0;

        LoadTransitionScene();
    }

    void Update() {
    }

    // Call this to end current scene
    public virtual void LoadTransitionScene() {
        SceneManager.LoadScene(transition.name);
    }

    // Call this to start the next scene
    public void LoadNextGameScene() {
        gameStage++;
        SceneManager.LoadScene(sceneArray[gameStage].name);
    }
}


public enum GAME_STATE { RUNNING, PAUSED };