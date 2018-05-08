using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBackButton : MonoExtended {

    [SerializeField] AudioClip backSoundFour;

    bool isPlayingPrev = false;

    void Start () {
        DontDestroyOnLoad(gameObject);
        if (gameManager.textingSceneStage == 3)
            GameObject.Find("BackButtonSound").GetComponent<AudioSource>().clip = backSoundFour;
    }
	
	void Update () {
        if (isPlayingPrev && gameObject.GetComponent<AudioSource>().isPlaying == false)
            Destroy(gameObject);
        isPlayingPrev = gameObject.GetComponent<AudioSource>().isPlaying;
    }

    protected override void GameUpdate() {
    }
}
