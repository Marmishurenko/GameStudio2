using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoExtended {

    [SerializeField] Animator cameraAnim;
    [SerializeField] Animator scene1Anim;
    [SerializeField] Animator scene2Anim;
    [SerializeField] Animator scene3Anim;
    [SerializeField] Animator scene4Anim;
    [SerializeField] float BUS_FADEOUT_TIME;
    [SerializeField] float CITY_BG_FADEOUT_TIME;

    int introStage = 0;
    public bool isWaitForClick = false;

    void Start() {
        cameraAnim.enabled = true;
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
                    scene2Anim.enabled = true;
                    NextStage();
                }
                break;
            case 1:
                if (isWaitForClick && Input.GetMouseButtonDown(0)) {
                    scene3Anim.enabled = true;
                    NextStage();
                }
                break;
            case 2:
            case 3:
                if (isWaitForClick && Input.GetMouseButtonDown(0)) {
                    scene4Anim.enabled = true;
                    NextStage();
                }
                break;
            case 4:
                if (isWaitForClick && Input.GetMouseButtonDown(0)) {
                    gameManager.LoadTransitionScene();
                }
                break;
            default:
                if (isWaitForClick && Input.GetMouseButtonDown(0))
                    NextStage();
                break;
        }
    }

    void NextStage() {
        isWaitForClick = false;
        introStage++;
        cameraAnim.enabled = true;
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

    public void CityBgFadeOut() {
        StartCoroutine(CityBgFadeOutCoroutine());
    }

    IEnumerator CityBgFadeOutCoroutine() {
        float alpha = 1;
        while (alpha > 0) {
            alpha -= 1 / CITY_BG_FADEOUT_TIME * Time.deltaTime;
            foreach (Transform sprite in scene1Anim.transform.GetChild(4)) {
                Color c = sprite.GetComponent<SpriteRenderer>().color;
                c.a = alpha;
                sprite.GetComponent<SpriteRenderer>().color = c;
            }
            yield return null;
        }
    }

    protected override void GameUpdate() {
    }
}
