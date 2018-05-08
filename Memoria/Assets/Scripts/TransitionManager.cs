using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransitionManager : MonoExtended {

    [SerializeField] List<string> textList;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] float HAIR_GROW_TIME;

    void Start() {
        StartCoroutine("Transition");
    }

    protected override void GameUpdate() {
    }

    IEnumerator Transition() {
        // Set text
        string text = "";
        if (gameManager.gameStage < textList.Count)
            text = textList[gameManager.gameStage].Replace("|", "\n");
        textUI.SetText(text);
        
        // Set yumi hair
        int gameCycle = -1;
        switch (gameManager.gameStage) {
            case 6:
                gameCycle = 1;
                break;
            case 10:
                gameCycle = 2;
                break;
            case 14:
                gameCycle = 3;
                break;
        }
        Transform yumiHair = GameObject.Find("Yumi").transform;
        if (gameCycle != -1) {
            foreach (Transform trans in yumiHair)
                trans.gameObject.SetActive(true);
            // Have text and hair to show
            yumiHair.GetChild(0).GetComponent<SpriteRenderer>().sprite = yumiHair.GetComponent<YumiHairs>().backHairs[gameCycle - 1];
            yumiHair.GetChild(1).GetComponent<SpriteRenderer>().sprite = yumiHair.GetComponent<YumiHairs>().backHairs[gameCycle];
            yumiHair.GetChild(3).GetComponent<SpriteRenderer>().sprite = yumiHair.GetComponent<YumiHairs>().frontHairs[gameCycle - 1];
            yumiHair.GetChild(4).GetComponent<SpriteRenderer>().sprite = yumiHair.GetComponent<YumiHairs>().frontHairs[gameCycle];
        }

        yield return new WaitForSeconds(0.5f);

        // No text to show
        if (text.Length == 0) {
            yield return new WaitForSeconds(0.5f);
            gameManager.LoadNextGameScene();
            yield break;
        }

        Color c = new Color(255, 255, 255, 0);
        while (c.a < 1) {
            c.a += 1 / HAIR_GROW_TIME * Time.deltaTime;
            yumiHair.GetChild(1).GetComponent<SpriteRenderer>().color = c;
            yumiHair.GetChild(4).GetComponent<SpriteRenderer>().color = c;
            yield return null;
        }

        while (Input.GetMouseButton(0) == false) {
            yield return null;
        }
        yield return new WaitForSeconds(1);
        gameManager.LoadNextGameScene();
    }
}
