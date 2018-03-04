using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bottle : MonoBehaviour, IPointerClickHandler {

    [SerializeField] Vector3 HAND_POSITION;
    [SerializeField] Vector3 HAND_SCALE;
    [SerializeField] Sprite[] labels;
    public int labelIndex;

    int state = 0;
    static bool pickUpLock = false;
    Vector3 originalPos;
    Vector3 originalScale;

    void Start() {
        originalPos = transform.localPosition;
        originalScale = transform.localScale;
        labelIndex = Random.Range(1, 3);
    }

    void Update() {
        if (state == 2 && Input.GetMouseButtonDown(0)) {
            // Put back
            state = 1;
            StartCoroutine("Moving", false);
            //transform.GetChild(0).gameObject.GetComponent<Image>().sprite = labelEmpty;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (state == 0 && pickUpLock == false) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                transform.GetChild(1).gameObject.GetComponent<Image>().sprite = labels[labelIndex];
                GameObject.Find("Patience").GetComponent<Patience>().Shake();
                // Run out of patience
                if (GameObject.Find("Patience").GetComponent<Patience>().patience == 0)
                    return;
                // Take out
                state = 1;
                pickUpLock = true;
                StartCoroutine("Moving", true);
                transform.SetAsLastSibling();
                GameObject.Find("Patience").GetComponent<Patience>().Check();
            } else if (eventData.button == PointerEventData.InputButton.Right) {
                // Pick
                pickUpLock = false;
                GameObject.Find("Grandaunt").GetComponent<Grandaunt>().TurnOn(labelIndex);
            }
        } else if (state == 2) {
            if (eventData.button == PointerEventData.InputButton.Right) {
                Debug.Log("");
                // Pick
                pickUpLock = false;
                GameObject.Find("Grandaunt").GetComponent<Grandaunt>().TurnOn(labelIndex);
            }
        }
    }

    IEnumerator Moving(bool toHand) {
        float time = 0.3f;
        float startTime = Time.time;
        while (true) {
            if (toHand) {
                transform.localPosition = Vector3.Lerp(originalPos, HAND_POSITION, (Time.time - startTime) / time);
                transform.localScale = Vector3.Lerp(originalScale, HAND_SCALE, (Time.time - startTime) / time);
                SetColor((Time.time - startTime) / time);
            } else {
                transform.localPosition = Vector3.Lerp(HAND_POSITION, originalPos, (Time.time - startTime) / time);
                transform.localScale = Vector3.Lerp(HAND_SCALE, originalScale, (Time.time - startTime) / time);
                SetColor(1 - (Time.time - startTime) / time * 0.7f);
            }
            if (Time.time - startTime >= time)
                break;
            yield return null;
        }
        if (toHand) {
            state = 2;
        } else {
            state = 0;
            pickUpLock = false;
            transform.SetAsFirstSibling();
        }
    }

    void SetColor(float a) {
        Color c = transform.GetChild(1).gameObject.GetComponent<Image>().color;
        c.a = a;
        transform.GetChild(1).gameObject.GetComponent<Image>().color = c;
    }
}
