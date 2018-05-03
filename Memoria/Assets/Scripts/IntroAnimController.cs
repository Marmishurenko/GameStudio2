using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimController : MonoBehaviour {

    public void WaitForClick() {
        GameObject.Find("IntroManager").GetComponent<IntroManager>().isWaitForClick = true;
    }

    public void PauseAndWaitForClick() {
        gameObject.GetComponent<Animator>().enabled = false;
        WaitForClick();
    }
}
