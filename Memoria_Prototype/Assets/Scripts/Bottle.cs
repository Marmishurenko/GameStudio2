using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bottle : MonoBehaviour, IPointerClickHandler {

    [SerializeField] Vector3 HAND_POSITION;
    [SerializeField] Vector3 HAND_SCALE;
    [SerializeField] Vector3 CART_POSITION;
    [SerializeField] Vector3 CART_ROTATION;
    [SerializeField] Sprite[] labels;
    public int labelIndex;

    int state = 0;
    static bool pickUpLock = false;
    public static int mistakeCount;
    public bool looked = false;
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
            if (eventData.button == PointerEventData.InputButton.Right) {
                // Pick
                StartCoroutine("Pick");
            } else if (eventData.button == PointerEventData.InputButton.Left) {
                // Run out of patience
                if (GameObject.Find("Patience").GetComponent<Patience>().patience == 0) {
                    GameObject.Find("Patience").GetComponent<Patience>().Shake();
                    return;
                }

                // Take out
                // Check a new one
                if (looked == false) {
                    looked = true;
                    int count = 0;
                    foreach (Transform bottle in transform.parent)
                        if (bottle.GetComponent<Bottle>().looked)
                            count++;
                    // Already took all mistakes
                    if (count == mistakeCount) {
                        // Pick a random one from the rest
                        int index = Random.Range(0, 12 - mistakeCount);
                        foreach (Transform bottle in transform.parent) {
                            if (bottle.GetComponent<Bottle>().looked == false) {
                                if (index > 0) {
                                    index--;
                                } else {
                                    // Set the right one
                                    bottle.GetComponent<Bottle>().labelIndex = 0;
                                    break;
                                }
                            }
                        }
                    }
                }

                transform.GetChild(1).gameObject.GetComponent<Image>().sprite = labels[labelIndex];
                state = 1;
                pickUpLock = true;
                StartCoroutine("Moving", true);
                GameObject.Find("Patience").GetComponent<Patience>().Check();
            }
        } else if (state == 2) {
            if (eventData.button == PointerEventData.InputButton.Right) {
                // Pick
                StartCoroutine("Pick");
            }
        }
    }

    IEnumerator Moving(bool toHand) {
        transform.SetAsLastSibling();

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
                SetColor(1 - (Time.time - startTime) / time * 0.5f);
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
        }
    }

    void SetColor(float a) {
        Color c = transform.GetChild(1).gameObject.GetComponent<Image>().color;
        c.a = a;
        transform.GetChild(1).gameObject.GetComponent<Image>().color = c;
    }

    IEnumerator Pick() {
        transform.SetAsLastSibling();
        pickUpLock = false;
        originalPos = transform.localPosition;
        originalScale = transform.localScale;
        float originalAlpha = transform.GetChild(1).gameObject.GetComponent<Image>().color.a;
        float time = 0.7f;
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
        yield return new WaitForSeconds(0.8f);
        GameObject.Find("Grandaunt").GetComponent<Grandaunt>().TurnOn(labelIndex);
    }
}
