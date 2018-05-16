using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

// General game manager which exists through the whole life cycle of the game
public class GameManager : MonoBehaviour {

    [Tooltip("Controls the general state of the game.")]
    public GAME_STATE gameState;

    [Tooltip("Keeps track of the progress of the game.")]
    public int gameStage;

    public string transition;

    [Tooltip("The order of execution of game scenes.")]
    [SerializeField]
    string[] sceneArray;

    public int textingSceneStage;

    [SerializeField] GameObject cursorPrefab;

    [SerializeField] SpriteRenderer blackScreen;
    [SerializeField] PostProcessingProfile bwProfile;
     float satDecrease = 0.025f;
    Camera cam;

    [SerializeField] float FADE_IN_TIME;
    [SerializeField] float FADE_OUT_TIME;

    [Header("Bg Music")]
    [SerializeField] AudioClip[] backgroundMusicList;

    CursorController cursorController = null;

    void Awake() {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1) {
            Destroy(gameObject);
            return;
        }
       
        GameObject cursor = Instantiate(cursorPrefab);
        cursorController = cursor.GetComponent<CursorController>();
        cursorController.spriteOffset = 2;
        cursorController.transform.localScale = Vector3.one * 0.6f;
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
        gameState = GAME_STATE.RUNNING;
        gameStage = 0;
        textingSceneStage = 0;

        gameObject.GetComponent<AudioSource>().clip = backgroundMusicList[0];
        gameObject.GetComponent<AudioSource>().volume = 1;
        gameObject.GetComponent<AudioSource>().Play();

        //Resetting post-processing profile
        ColorGradingModel.Settings saturationSettings = bwProfile.colorGrading.settings;
        saturationSettings.basic.saturation = 1.05f;
        bwProfile.colorGrading.settings = saturationSettings;


    }

	private void OnEnable()
	{
        SceneManager.sceneLoaded += FadeIn;
        SceneManager.sceneLoaded += AssignEffectsAndChangeAtRuntime;
	}

	private void OnDisable()
	{
        SceneManager.sceneLoaded -= FadeIn;
        SceneManager.sceneLoaded -= AssignEffectsAndChangeAtRuntime;
	}

	void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            gameStage = -1;
            LoadTransitionScene();
        }

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
        float alpha = 0;
        while (alpha < 1) {
            alpha += 1 / FADE_OUT_TIME * Time.deltaTime;
            Color c = blackScreen.color;
            c.a = alpha;
            blackScreen.color = c;
            yield return null;
        }

        // Load
        gameStage++;
       
      
        SceneManager.LoadScene(transition);
        if (cursorController != null)
            cursorController.Hide();
        gameState = GAME_STATE.RUNNING;

        // Change bg music
        switch (gameStage) {
            case 2:
                StartCoroutine(SwitchBgMusic(1));
                break;
            case 6:
                StartCoroutine(SwitchBgMusic(2));
                break;
            case 10:
                StartCoroutine(SwitchBgMusic(3));
                break;
            case 14:
                StartCoroutine(SwitchBgMusic(4));
                break;
        }
    }

    IEnumerator SwitchBgMusic(int musicIndex) {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        float volume = 1;
        while (volume > 0) {
            volume -= 1f / 2 * Time.deltaTime;
            audio.volume = volume;
            yield return null;
        }
        gameObject.GetComponent<AudioSource>().clip = backgroundMusicList[musicIndex];
        gameObject.GetComponent<AudioSource>().volume = 1;
        gameObject.GetComponent<AudioSource>().Play();
    }

    // Call this to start the next scene
    public void LoadNextGameScene() {
       
        StartCoroutine(EnterNextGameScene());

    }
   
    IEnumerator EnterNextGameScene() {
        gameState = GAME_STATE.PAUSED;

        // Fade out
        float alpha = 0;
        while (alpha < 1) {
            alpha += 1 / FADE_OUT_TIME * Time.deltaTime;
            Color c = blackScreen.color;
            c.a = alpha;
            blackScreen.color = c;
            yield return null;
        }

        // Load
        SceneManager.LoadScene(sceneArray[gameStage]);
        gameState = GAME_STATE.RUNNING;

        if (cursorController != null)
            cursorController.Show();
    }

    void FadeIn(Scene scene, LoadSceneMode mode) {
        transform.position = Vector3.zero;
        if (SceneManager.GetActiveScene().name == "LogoStart"
            || SceneManager.GetActiveScene().name == "Intro"
            || SceneManager.GetActiveScene().name == "Texting") {
            cursorController.spriteOffset = 2;
            cursorController.transform.localScale = Vector3.one * 0.6f;
        } else if (SceneManager.GetActiveScene().name == "EndCredits") {
            cursorController.Hide();
        } else {
            cursorController.spriteOffset = 0;
            cursorController.transform.localScale = Vector3.one * 0.45f;
        }
        StartCoroutine(CoroutineFadeIn());
    }

    IEnumerator CoroutineFadeIn() {
        float alpha = 1;
        while (alpha > 0) {
            alpha -= 1 / FADE_IN_TIME * Time.deltaTime;
            Color c = blackScreen.color;
            c.a = alpha;
            blackScreen.color = c;
            yield return null;
        }
    }


    public void AssignEffectsAndChangeAtRuntime(Scene scene, LoadSceneMode mode){
        //assigning post-processing effects 
        Camera cam1 = Camera.main;
        cam1.gameObject.AddComponent(typeof(PostProcessingBehaviour));
        PostProcessingBehaviour ppb = cam1.gameObject.GetComponent<PostProcessingBehaviour>();
        ppb.profile = bwProfile;

        //change the saturation in the temporary settings variable
        ColorGradingModel.Settings saturationSettings = bwProfile.colorGrading.settings;
        saturationSettings.basic.saturation -= satDecrease;

        //set the sat settings in the actual profile to the temp settings with the changed value
        bwProfile.colorGrading.settings = saturationSettings;

    }

}


public enum GAME_STATE { RUNNING, PAUSED };