using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Patience : MonoBehaviour {

    int[] patienceList = { 20, 16, 10, 6, 3, 0 };
    int[] resultList = { 3, 3, 1, 1, 0, 0 };

    int patience, result;
    int checkCount = 0;
    int pickCount = 0;
    int rightCount = 0;
    List<int> targetIndexes = new List<int>();

    public Color[] colors;
    public Transform bottles;

    bool done = false;

    void Start() {
        int week = 0;
        if (GameObject.Find("GameManager") != null)
            week = GameObject.Find("GameManager").GetComponent<GameManager>().week - 1;
        else
            week = 4;
        if (week < patienceList.Length) {
            patience = patienceList[week];
            result = resultList[week];
        } else {
            patience = patienceList[patienceList.Length - 1];
            result = resultList[patienceList.Length - 1];
        }

        // Generate random indexes
        for (int i = 0; i < result; i++) {
            int newIndex = 0;
            while (true) {
                newIndex = Random.Range(1, Mathf.Min(patience, 16));
                if (targetIndexes.Contains(newIndex) == false)
                    break;
                // Just in case
                if (Input.GetKeyDown(KeyCode.Q))
                    break;
            }
            targetIndexes.Add(newIndex);
        }
        if (result == 3) {
            bool bigIndex = false;
            foreach (int i in targetIndexes)
                if (i > 10) {
                    bigIndex = true;
                    break;
                }
            if (bigIndex == false)
                targetIndexes[0] = Random.Range(11, 16);
        }

        SetColor();
    }

    void Update() {
        if (done)
            return;

        switch (Bottle.state) {
            case 0:
                if (Input.GetKeyDown(KeyCode.DownArrow)) {
                    // Check a new one
                    checkCount++;
                    // Regular check
                    bottles.GetChild(0).GetComponent<Bottle>().MoveToHand(targetIndexes.Contains(checkCount - 1));
                    SetColor();
                    if (checkCount > patience)
                        StartCoroutine("ShakeCoroutine");
                }
                break;

            case 2:
                if (checkCount > patience) {
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
                        // Put to cart anyway
                        StartCoroutine("ImpatientPick");
                    }
                    break;
                }

                if (Input.GetKeyDown(KeyCode.DownArrow)) {
                    // Put to cart
                    Bottle b = bottles.GetChild(bottles.childCount - 1).GetComponent<Bottle>();
                    if (b.labelIndex != 0) {
                        b.Shake();
                        break;
                    }
                    b.MoveToCart();
                    StartCoroutine("Pick");
                    rightCount++;
                    break;
                }

                if (Input.GetKeyDown(KeyCode.UpArrow)) {
                    // Put back to shelf
                    Bottle b = bottles.GetChild(bottles.childCount - 1).GetComponent<Bottle>();
                    if (b.labelIndex == 0) {
                        b.Shake();
                        break;
                    }
                    b.MoveToShelf();
                    break;
                }
                break;

            case 1:
            case 3:
                break;
        }
    }


    IEnumerator ImpatientPick() {
        StartCoroutine("ShakeCoroutine");
        yield return new WaitForSeconds(0.3f);
        Bottle b = bottles.GetChild(bottles.childCount - 1).GetComponent<Bottle>();
        b.MoveToCart();
        StartCoroutine("Pick");
    }

    IEnumerator Pick() {
        pickCount++;
        GameObject.Find("PickCount").GetComponent<Text>().text = pickCount.ToString() + " / 3";
        GameObject.Find("PickCount").GetComponent<Animator>().Play("Pop");
        if (pickCount == 3) {
            done = true;
            yield return new WaitForSeconds(1.5f);
            GameObject.Find("Grandaunt").GetComponent<Grandaunt>().TurnOn(rightCount);
        }
    }

    void SetColor() {
        int patienceRemain = Mathf.Clamp(patience - checkCount, 0, 16);
        int index = Mathf.FloorToInt(patienceRemain / 4f);
        if (index == 4)
            transform.GetChild(0).GetComponent<Image>().color = colors[4];
        else
            transform.GetChild(0).GetComponent<Image>().color =
                Color.Lerp(colors[index], colors[index + 1], patienceRemain % 4 / 4f);
    }

    IEnumerator ShakeCoroutine() {
        float startTime = Time.time;
        Vector3 originalPos = transform.localPosition;
        while (Time.time - startTime < 0.3) {
            //float range = level * level * 0.4f + 2f;
            float range = 12;
            transform.localPosition = originalPos + new Vector3(Random.Range(-range, range), Random.Range(-range, range));
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
