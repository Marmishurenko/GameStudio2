using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {

    [SerializeField] Animator cameraAnim;
    [SerializeField] Animator scene1Anim;
    [SerializeField] Animator scene2Anim;
    [SerializeField] Animator scene3Anim;
    [SerializeField] Animator scene4Anim;
    [SerializeField] float BUS_FADEOUT_TIME;

    int introStage = 0;
    public bool isWaitForClick = false;

    void Start() {
        cameraAnim.enabled = false;
        scene1Anim.enabled = true;
        scene2Anim.enabled = false;
        scene3Anim.enabled = false;
        scene4Anim.enabled = false;
    }

    void Update() {
        switch (introStage) {
            case 0:
                if (isWaitForClick && Input.GetMouseButtonDown(0)) {
                    StartCoroutine(BusFadeOut());
                    introStage++;
                }
                break;
            case 1:
                if (isWaitForClick && Input.GetMouseButtonDown(0)) {
                    introStage++;
                }
                break;
            case 2:
                if (isWaitForClick && Input.GetMouseButtonDown(0)) {
                    introStage++;
                }
                break;
            case 3:
                if (isWaitForClick && Input.GetMouseButtonDown(0)) {
                    introStage++;
                }
                break;
        }
    }

    IEnumerator BusFadeOut() {
        float alpha = 1;
        while (alpha > 0) {
            alpha -= 1 / BUS_FADEOUT_TIME * Time.deltaTime;
            for (int i = 0; i < 3; i++) {
                Transform sprite = scene1Anim.transform.GetChild(i);
                Color c = sprite.GetComponent<SpriteRenderer>().color;
                c.a = alpha;
                sprite.GetComponent<SpriteRenderer>().color = c;
            }
            yield return null;
        }
    }
}
