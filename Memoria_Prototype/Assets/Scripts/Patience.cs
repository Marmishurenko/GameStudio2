using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Patience : MonoBehaviour {

    public int patience;
    int[] patienceList = { 15, 10, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

    void Start() {
        int week = GameObject.Find("GameManager").GetComponent<GameManager>().week - 1;
        if (week < patienceList.Length)
            patience = patienceList[week];
        else
            patience = patienceList[patienceList.Length - 1];
        GameObject.Find("Bottles").transform.GetChild(Random.Range(1, 14)).GetComponent<Bottle>().labelIndex = 0;
        gameObject.GetComponent<Text>().text = "Patience " + patience.ToString();
    }

    void Update() {
    }

    public void Check() {
        patience--;
        gameObject.GetComponent<Text>().text = "Patience " + patience.ToString();
    }

    public void Shake() {
        StartCoroutine("ShakeCoroutine");
    }
    IEnumerator ShakeCoroutine() {
        float startTime = Time.time;
        Vector3 originalPos = transform.localPosition;
        while (Time.time - startTime < 0.2) {
            float range = 6;
            transform.localPosition = originalPos + new Vector3(Random.Range(-range, range), Random.Range(-range, range));
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
