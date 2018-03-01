using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevelManager : MonoBehaviour {

    [SerializeField] GameObject messagePrefab;
    [SerializeField] Sprite[] typingSprites;
    [SerializeField] SpriteRenderer typingSprite;

    [Header("/G19A/G0/Y12A/E")]
    [SerializeField]
    string[] weekScripts;

    int week;
    string[] dayScripts;
    string messageScript;
    int scriptIndex;

    void Start() {
        week = GameObject.Find("GameManager").GetComponent<GameManager>().week;
        //week = 1;
        dayScripts = weekScripts[week - 1].Split('/');
        scriptIndex = 0;
        StartCoroutine("NextMessage");
    }

    void Update() {
    }

    IEnumerator NextMessage() {
        int timer;
        scriptIndex++;
        messageScript = dayScripts[scriptIndex];
        char type = messageScript[0];
        messageScript = messageScript.Remove(0, 1);
        switch (type) {
            case 'G':
                // Girlfriend's message
                timer = messageScript[0] - '0';
                messageScript = messageScript.Remove(0, 1);
                if (messageScript[0] > '0' && messageScript[0] < '9') {
                    timer = timer * 10 + messageScript[1] - '0';
                    messageScript = messageScript.Remove(0, 1);
                }
                yield return new WaitForSeconds(timer);
                GameObject newMessage = Instantiate(messagePrefab, GameObject.Find("MessageList").transform);
                newMessage.GetComponent<Message>().Init(false, messageScript[0] - 'A');
                StartCoroutine("NextMessage");
                break;

            case 'Y':
                // Player's message
                timer = messageScript[0] - '0';
                messageScript = messageScript.Remove(0, 1);
                if (messageScript[0] > '0' && messageScript[0] < '9') {
                    timer = timer * 10 + messageScript[1] - '0';
                    messageScript = messageScript.Remove(0, 1);
                }
                yield return new WaitForSeconds(timer);
                typingSprite.enabled = true;
                typingSprite.sprite = typingSprites[messageScript[0] - 'A'];
                break;

            case 'E':
                // End of day
                yield return new WaitForSeconds(2);
                GameObject.Find("GameManager").GetComponent<GameManager>().NextPhase();
                break;
        }
    }

    public void OnClick() {
        if (typingSprite.enabled == false)
            return;
        GameObject newMessage = Instantiate(messagePrefab, GameObject.Find("MessageList").transform);
        newMessage.GetComponent<Message>().Init(true, messageScript[0] - 'A');
        typingSprite.enabled = false;
        StartCoroutine("NextMessage");
    }
}
