using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bottle : MonoBehaviour {

    [SerializeField] Vector3 HAND_POSITION;
    [SerializeField] Vector3 HAND_SCALE;
    [SerializeField] Vector3 CART_POSITION;
    [SerializeField] Vector3 CART_ROTATION;
    [SerializeField] Sprite[] labels;

    public static int state = 0;    // 0: shelf / 1: hand-shelf / 2: hand / 3: to cart

    public int labelIndex;
    
    static bool pickUpLock = false;
    public static int mistakeCount;
    public bool looked = false;
    Vector3 originalPos;
    Vector3 originalScale;

    void Start() {
        originalPos = transform.localPosition;
        originalScale = transform.localScale;
        labelIndex = Random.Range(1, 5);
    }

    void Update() {
        if (state == 2 && Input.GetMouseButtonDown(0)) {
            // Put back
            state = 1;
            StartCoroutine("Moving", false);
            //transform.GetChild(0).gameObject.GetComponent<Image>().sprite = labelEmpty;
        }
    }

    public void MoveToHand(bool isRight) {
        if (isRight)
            labelIndex = 0;
        transform.GetChild(1).gameObject.GetComponent<Image>().sprite = labels[labelIndex];
        state = 1;
        StartCoroutine("Moving", true);
    }

    public void MoveToShelf() {
        state = 1;
        SetColor(0);
        StartCoroutine("Moving", false);
    }

    public void MoveToCart() {
        state = 3;
        StartCoroutine("Pick");
    }

    IEnumerator Moving(bool toHand) {
        transform.SetAsLastSibling();

        float time = 0.45f;
        float startTime = Time.time;
        while (true) {
            if (toHand) {
                transform.localPosition = Vector3.Lerp(originalPos, HAND_POSITION, (Time.time - startTime) / time);
                transform.localScale = Vector3.Lerp(originalScale, HAND_SCALE, (Time.time - startTime) / time);
                //SetColor((Time.time - startTime) / time);
            } else {
                transform.localPosition = Vector3.Lerp(HAND_POSITION, originalPos, (Time.time - startTime) / time);
                transform.localScale = Vector3.Lerp(HAND_SCALE, originalScale, (Time.time - startTime) / time);
                //SetColor(1 - (Time.time - startTime) / time);
            }
            if (Time.time - startTime >= time)
                break;
            yield return null;
        }
        if (toHand) {
            state = 2;
            SetColor(1);
        } else {
            state = 0;
            pickUpLock = false;
        }
    }

    IEnumerator Pick() {
        transform.SetAsLastSibling();
        pickUpLock = false;
        originalPos = transform.localPosition;
        originalScale = transform.localScale;
        float originalAlpha = transform.GetChild(1).gameObject.GetComponent<Image>().color.a;
        float time = 0.4f;
        float startTime = Time.time;
        while (true) {
            transform.localPosition = Vector3.Lerp(originalPos, CART_POSITION, (Time.time - startTime) / time);
            transform.localScale = Vector3.Lerp(originalScale, HAND_SCALE, (Time.time - startTime) / time);
            transform.localEulerAngles = Vector3.Lerp(Vector3.zero, CART_ROTATION, (Time.time - startTime) / time);
            SetColor(Mathf.Lerp(originalAlpha, 1, (Time.time - startTime) / time));
            if (Time.time - startTime >= time)
                break;
            yield return null;
        }
        state = 0;
    }

    void SetColor(float a) {
        Color c = transform.GetChild(1).gameObject.GetComponent<Image>().color;
        c.a = a;
        transform.GetChild(1).gameObject.GetComponent<Image>().color = c;
    }

    public void Shake() {
        return;
        StartCoroutine("ShakeCoroutine");
    }

    IEnumerator ShakeCoroutine() {
        float startTime = Time.time;
        Vector3 originalPos = transform.localPosition;
        while (Time.time - startTime < 0.08) {
            float range = 6;
            transform.localPosition = originalPos + new Vector3(Random.Range(-range, range), Random.Range(-range, range));
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
