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

    [Header("Bg Music")]
    [SerializeField] AudioClip[] backgroundMusicList;

    CursorController cursorController;

    void Awake() {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1) {
            Destroy(gameObject);
            return;
        }
        GameObject cursor = Instantiate(cursorPrefab);
        cursorController = cursor.GetComponent<CursorController>();
        cursorController.Hide();
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        gameState = GAME_STATE.RUNNING;
        gameStage = -1;
        textingSceneStage = 0;

        SceneManager.sceneLoaded += FadeIn;

        LoadTransitionScene();
    }

    void Update() {
        // Debug
        if (Input.GetKeyDown(KeyCode.N))
            LoadTransitionScene();
    }

    // Call this to end current scene
    public virtual void LoadTransitionScene() {
        StartCoroutine(EnterTransitionScene());
    }

    IEnumerator EnterTransitionScene() {
        gameState = GAME_STATE.PAUSED;

        // Fade out
        transform.position = (Vector2)Camera.main.transform.position;
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
        SceneManager.LoadScene(transition.name);
        cursorController.Hide();
        gameState = GAME_STATE.RUNNING;

        // Change bg music
        switch (gameStage) {
            case 0:
                gameObject.GetComponent<AudioSource>().clip = backgroundMusicList[0];
                gameObject.GetComponent<AudioSource>().Play();
                break;
            case 4:
                gameObject.GetComponent<AudioSource>().clip = backgroundMusicList[1];
                gameObject.GetComponent<AudioSource>().Play();
                break;
            case 8:
                gameObject.GetComponent<AudioSource>().clip = backgroundMusicList[2];
                gameObject.GetComponent<AudioSource>().Play();
                break;
            case 12:
                gameObject.GetComponent<AudioSource>().clip = backgroundMusicList[3];
                gameObject.GetComponent<AudioSource>().Play();
                break;
        }
    }

    // Call this to start the next scene
    public void LoadNextGameScene() {
        StartCoroutine(EnterNextGameScene());
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
        SceneManager.LoadScene(sceneArray[gameStage].name);
        gameState = GAME_STATE.RUNNING;
        cursorController.Show();
    }

    void FadeIn(Scene scene, LoadSceneMode mode) {
        if (SceneManager.GetActiveScene().name == "Texting") {
            cursorController.spriteOffset = 2;
            cursorController.transform.localScale = Vector3.one * 0.6f;
        } else {
            cursorController.spriteOffset = 0;
            cursorController.transform.localScale = Vector3.one * 0.45f;
        }
        StartCoroutine(CoroutineFadeIn());
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