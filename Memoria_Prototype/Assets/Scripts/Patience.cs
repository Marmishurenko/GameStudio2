using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Patience : MonoBehaviour {

    public Color[] colors;
    int[] patienceList = { 12, 12, 12, 8, 4, 2, 1, 0 };
    int totalPatience;
    public int patience;
    int previousColorIndex = -1;

    void Start() {
        int week = GameObject.Find("GameManager").GetComponent<GameManager>().week - 1;
        if (week < patienceList.Length)
            totalPatience = patienceList[week];
        else
            totalPatience = patienceList[patienceList.Length - 1];
        patience = totalPatience;
        SetColor();
        Bottle.mistakeCount = (int)((float)Mathf.Min(totalPatience, 12) * 3 / 4);
    }

    public void Check() {
        patience--;
        SetColor();
    }

    void SetColor() {
        int index = Mathf.CeilToInt(Mathf.Min(patience, 12) / 4f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[index];
        if (index != previousColorIndex && previousColorIndex != -1)
            Shake();
        previousColorIndex = index;
    }

    public void Shake() {
        StartCoroutine("ShakeCoroutine");
    }

    IEnumerator ShakeCoroutine() {
        float startTime = Time.time;
        Vector3 originalPos = transform.localPosition;
        int level = Mathf.CeilToInt(Mathf.Min(patience, 12) / 4f);
        while (Time.time - startTime < 0.25 + (3 - level) * 0.1f) {
            //float range = level * level * 0.4f + 2f;
            float range = 12;
            transform.localPosition = originalPos + new Vector3(Random.Range(-range, range), Random.Range(-range, range));
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
