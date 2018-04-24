using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransitionManager : MonoExtended {

    [SerializeField] List<string> textList;
    [SerializeField] TextMeshProUGUI textUI;

    void Start() {
        StartCoroutine("Transition");
    }

    protected override void GameUpdate() {
    }

    IEnumerator Transition() {
        string s = textList[gameManager.gameStage].Replace("|", "\n");
        textUI.SetText(s);
        yield return new WaitForSeconds(0.5f);

        // No text to show
        if (textUI.text.Length == 0) {
            yield return new WaitForSeconds(0.5f);
            gameManager.LoadNextGameScene();
            yield break;
        }

        // Show text
        while (Input.GetMouseButton(0) == false) {
            yield return null;
        }
        yield return new WaitForSeconds(1);
        gameManager.LoadNextGameScene();
    }
}
