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
}
