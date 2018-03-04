using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

    [SerializeField] Sprite[] hearts;

    void Start() {
        gameObject.GetComponent<Image>().sprite = hearts[GameObject.Find("GameManager").GetComponent<GameManager>().week - 1];
    }
}
