using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeekScreen : MonoBehaviour {

    // Use this for initialization
    void Start() {
        int week = GameObject.Find("GameManager").GetComponent<GameManager>().week;
        if (week <= 6)
            transform.GetChild(0).GetComponent<Text>().text = "Week " + week;
        else {
            transform.GetChild(0).GetComponent<Text>().text = "Week     ";
            transform.GetChild(1).GetComponent<Text>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
