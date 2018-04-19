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

    [SerializeField] SpriteRenderer blackScreen;
    [SerializeField] float FADE_IN_TIME;
    [SerializeField] float FADE_OUT_TIME;

    SpriteRenderer cursorSR;

    void Awake() {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1) {
            Destroy(gameObject);
            return;
        }
        GameObject cursor = Instantiate(cursorPrefab);
        cursorSR = cursor.GetComponent<SpriteRenderer>();
        cursorSR.enabled = false;
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        gameState = GAME_STATE.RUNNING;
        gameStage = -1;
        textingSceneStage = 0;

        SceneManager.sceneLoaded += FadeIn;

        SceneManager.LoadScene(transition.name);
    }

    // Call this to end current scene
    public virtual void LoadTransitionScene() {
        StartCoroutine("EnterTransitionScene");
    }

    IEnumerator EnterTransitionScene() {
        gameState = GAME_STATE.PAUSED;

        // Fade out
        float timer = 0;
        while (timer < FADE_OUT_TIME) {
            timer += 0.05f;
            Color c = blackScreen.color;
            c.a = timer / FADE_OUT_TIME;
            blackScreen.color = c;
            yield return new WaitForSeconds(0.05f);
        }

        // Load
        SceneManager.LoadScene(transition.name);
        cursorSR.enabled = false;
        gameState = GAME_STATE.RUNNING;
    }

    // Call this to start the next scene
    public void LoadNextGameScene() {
        StartCoroutine("EnterNextGameScene");
    }

    IEnumerator EnterNextGameScene() {
        gameState = GAME_STATE.PAUSED;

        // Fade out
        float timer = 0;
        while (timer < FADE_OUT_TIME) {
            timer += 0.05f;
            Color c = blackScreen.color;
            c.a = timer / FADE_OUT_TIME;
            blackScreen.color = c;
            yield return new WaitForSeconds(0.05f);
        }

        // Load
        gameStage++;
        SceneManager.LoadScene(sceneArray[gameStage].name);
        gameState = GAME_STATE.RUNNING;
        cursorSR.enabled = true;
    }

    void FadeIn(Scene scene, LoadSceneMode mode) {
        StartCoroutine("CoroutineFadeIn");
    }

    IEnumerator CoroutineFadeIn() {
        float timer = 0;
        while (timer < FADE_IN_TIME) {
            timer += 0.05f;
            Color c = blackScreen.color;
            c.a = 1 - timer / FADE_IN_TIME;
            blackScreen.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}


public enum GAME_STATE { RUNNING, PAUSED };