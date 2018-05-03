using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimController : MonoBehaviour {

    public void WaitForClick() {
        gameObject.GetComponent<Animator>().enabled = false;
        GameObject.Find("IntroManager").GetComponent<IntroManager>().isWaitForClick = true;
    }
}
