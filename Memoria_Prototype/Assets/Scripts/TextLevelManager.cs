using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevelManager : MonoBehaviour {

    [SerializeField] GameObject messagePrefab;
    [SerializeField] Sprite[] typingSprites;
    [SerializeField] SpriteRenderer typingSprite;
    [SerializeField] GameObject dots;

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
            week =3;
        dayScripts = weekScripts[week - 1].Split('/');
        scriptIndex = 0;
        StartCoroutine("NextMessage");
    }

    void Update() {
    }

    IEnumerator NextMessage() {
        scriptIndex++;
        messageScript = dayScripts[scriptIndex];
        char type = messageScript[0];
        messageScript = messageScript.Remove(0, 1);
        switch (type) {
            case 'G':
                // Girlfriend's message
                yield return new WaitForSeconds(0.7f);
                yield return new WaitForSeconds(GetTime());
                GameObject newMessage = Instantiate(messagePrefab, GameObject.Find("MessageList").transform);
                newMessage.GetComponent<Message>().Init(false, messageScript[0] - 'A');
                StartCoroutine("NextMessage");
                break;

            case 'Y':
                // Player's message
                yield return new WaitForSeconds(0.7f);
                float t = GetTime();
                if (t > 0) {
                    dots.SetActive(true);
                    yield return new WaitForSeconds(t);
                    dots.SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                }
                typingSprite.enabled = true;
                typingSprite.sprite = typingSprites[messageScript[0] - 'A'];
                break;

            case 'E':
                // End of day
                yield return new WaitForSeconds(1);
                GameObject.Find("Back").GetComponent<Home>().flashing = true;
                break;
        }
    }

    float GetTime() {
        float timer = 0;
        while (true) {
            if (messageScript[0] > '0' && messageScript[0] < '9') {
                timer = timer * 10 + messageScript[0] - '0';
                messageScript = messageScript.Remove(0, 1);
            } else if (messageScript[0] == '.') {
                timer += 0.5f;
                messageScript = messageScript.Remove(0, 1);
            } else {
                break;
            }
        }
        return timer;
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
