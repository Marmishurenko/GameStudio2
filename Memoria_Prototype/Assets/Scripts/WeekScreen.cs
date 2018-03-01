using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeekScreen : MonoBehaviour {

    // Use this for initialization
    void Start() {
        int week = GameObject.Find("GameManager").GetComponent<GameManager>().week;
        transform.GetChild(0).GetComponent<Text>().text = "Week " + week;
    }

    // Update is called once per frame
    void Update() {

    }
}
