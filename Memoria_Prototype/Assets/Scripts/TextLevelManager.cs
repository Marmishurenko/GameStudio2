using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevelManager : MonoBehaviour {

    [SerializeField] GameObject messagePrefab;
    [SerializeField] Sprite[] typingSprites;
    [SerializeField] SpriteRenderer typingSprite;
    [SerializeField] GameObject playerDots;
    [SerializeField] TypingBubble girlTypingBubble;

    [Header("/G19A/G0/Y12A/E")]
    [SerializeField]
    string[] weekScripts;

    int week;
    string[] dayScripts;
    string messageScript;
    int scriptIndex;

    void Start() {
        if (GameObject.Find("GameManager") != null)
            week = GameObject.Find("GameManager").GetComponent<GameManager>().week;
        else
            week = 2;
        dayScripts = weekScripts[week - 1].Split('/');
        scriptIndex = 0;
        StartCoroutine("NextMessage");
    }

    IEnumerator NextMessage() {
        scriptIndex++;
        messageScript = dayScripts[scriptIndex];
        switch (messageScript[0]) {

            case 'G':
                // Girlfriend's message
                messageScript = messageScript.Remove(0, 1);
                yield return new WaitForSeconds(1f);
                yield return new WaitForSeconds(ReadTime());
                /*
                yield return new WaitForSeconds(1.5f);
                float t = ReadTime();
                if (t > 0) {
                    girlTypingBubble.TurnOn();
                    yield return new WaitForSeconds(t);
                    girlTypingBubble.TurnOff();
                    yield return new WaitForSeconds(0.3f);
                }
                */
                if (messageScript.Length > 0) {
                    GameObject newMessage = Instantiate(messagePrefab, GameObject.Find("MessageList").transform);
                    newMessage.GetComponent<Message>().Init(false, 'D' - messageScript[0]);
                }
                StartCoroutine("NextMessage");
                break;

            case 'Y':
                // Player's message
                messageScript = messageScript.Remove(0, 1);
                yield return new WaitForSeconds(0.7f);
                float t = ReadTime();
                if (t > 0) {
                    playerDots.SetActive(true);
                    yield return new WaitForSeconds(t);
                    playerDots.SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                }
                if (messageScript.Length > 0) {
                    typingSprite.enabled = true;
                    typingSprite.sprite = typingSprites['D' - messageScript[0]];
                } else {
                    StartCoroutine("NextMessage");
                }
                break;

            case 'E':
                // End of day
                yield return new WaitForSeconds(1);
                GameObject.Find("Back").GetComponent<Home>().flashing = true;
                break;

            default:
                // Blank time
                if (messageScript[0] < '0' || messageScript[0] > '9')
                    break;
                yield return new WaitForSeconds(ReadTime());
                StartCoroutine("NextMessage");
                break;
        }
    }

    float ReadTime() {
        float timer = 0;
        while (true) {
            if (messageScript.Length == 0)
                break;
            if (messageScript[0] < '0' || messageScript[0] > '9')
                break;
            timer = timer * 10 + messageScript[0] - '0';
            messageScript = messageScript.Remove(0, 1);
        }
        return timer;
    }

    public void OnClick() {
        if (typingSprite.enabled == false)
            return;
        GameObject newMessage = Instantiate(messagePrefab, GameObject.Find("MessageList").transform);
        newMessage.GetComponent<Message>().Init(true, 'D' - messageScript[0]);
        typingSprite.enabled = false;
        StartCoroutine("NextMessage");
    }
}
