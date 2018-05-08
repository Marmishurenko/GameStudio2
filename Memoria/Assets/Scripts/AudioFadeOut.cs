using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeOut : MonoBehaviour {

    [SerializeField] float FADE_OUT_TIME;

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void FadeOut() {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine() {
        float volume = gameObject.GetComponent<AudioSource>().volume;
        while (volume > 0) {
            volume -= 1 / FADE_OUT_TIME * Time.deltaTime;
            gameObject.GetComponent<AudioSource>().volume = volume;
            yield return null;
        }
        Destroy(gameObject);
    }
}
