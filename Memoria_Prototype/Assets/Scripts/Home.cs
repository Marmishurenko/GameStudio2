using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour {

    public bool flashing = false;
    float timer = 0;

    void Start() {
    }

    void Update() {
        Color c = gameObject.GetComponent<Image>().color;
        if (flashing)
            c.a = 1;
        else
            c.a = 0;
        gameObject.GetComponent<Image>().color = c;
        return;

        if (flashing == false)
            return;

        timer -= Time.deltaTime;
        if (timer > 0)
            return;

        timer = 0.7f;
        c = gameObject.GetComponent<Image>().color;
        if (c.a > 15f / 255)
            c.a = 0;
        else
            c.a = 33f / 255;
        gameObject.GetComponent<Image>().color = c;
    }



    public void OnClick() {
        if (flashing)
            GameObject.Find("GameManager").GetComponent<GameManager>().NextPhase();
    }
}
