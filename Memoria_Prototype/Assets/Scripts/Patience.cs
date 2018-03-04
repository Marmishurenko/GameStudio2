using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Patience : MonoBehaviour {

    public Color[] colors;
    int[] patienceList = { 12, 10, 8, 4, 2, 2, 1, 0 };
    int totalPatience;
    public int patience;

    void Start() {
        int week = GameObject.Find("GameManager").GetComponent<GameManager>().week - 1;
        if (week < patienceList.Length)
            totalPatience = patienceList[week];
        else
            totalPatience = patienceList[patienceList.Length - 1];
        patience = totalPatience;
        SetColor();
        GameObject.Find("Bottles").transform.GetChild(Random.Range(0, 10)).GetComponent<Bottle>().labelIndex = 0;
    }

    public void Check() {
        patience--;
        SetColor();
    }

    void SetColor() {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[Mathf.CeilToInt((float)patience / totalPatience * 3)];
    }

    public void Shake() {
        StartCoroutine("ShakeCoroutine");
    }

    IEnumerator ShakeCoroutine() {
        float startTime = Time.time;
        Vector3 originalPos = transform.localPosition;
        while (Time.time - startTime < 0.2) {
            int level = (4 - Mathf.CeilToInt((float)patience / totalPatience * 3));     // 1-4
            float range = level * level * 0.4f + 2f;
            transform.localPosition = originalPos + new Vector3(Random.Range(-range, range), Random.Range(-range, range));
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
