using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimController : MonoBehaviour {

    public void Pause() {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    public void WaitForClick() {
        gameObject.GetComponent<Animator>().enabled = false;
        GameObject.Find("IntroManager").GetComponent<IntroManager>().isWaitForClick = true;
    }

    public void CityBgFadeOut() {
        GameObject.Find("IntroManager").GetComponent<IntroManager>().CityBgFadeOut();
    }

    public void PlaySound(string audio) {
        GameObject.Find(audio).GetComponent<AudioSource>().Play();
    }

    public void StopSound(string audio) {
        StartCoroutine(SoundFadeOut(GameObject.Find(audio).GetComponent<AudioSource>()));
    }

    IEnumerator SoundFadeOut(AudioSource audio) {
        float volume = 1;
        while (volume > 0) {
            volume -= 1 / 1 * Time.deltaTime;
            audio.volume = volume;
            Debug.Log(volume);
            yield return null;
        }
    }
}
