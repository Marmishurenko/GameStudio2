using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingArea : MonoBehaviour {

    [SerializeField] GameObject typingBubble;
    [SerializeField] Sprite[] bubbleSprites;
    [SerializeField] GameObject messagePrefab;
    [SerializeField] TextingLevelManager levelManager;
    [SerializeField] GameObject messageWindow;

    int bubbleType;

    void Start() {

    }

    void Update() {

    }

    public void ShowBubble(int type) {
        bubbleType = type;
        typingBubble.SetActive(true);
        typingBubble.GetComponent<SpriteRenderer>().sprite = bubbleSprites[bubbleType];
    }

    public void OnClick() {
        if (typingBubble.activeSelf == false)
            return;
        GameObject newMessage = Instantiate(messagePrefab, messageWindow.transform);
        newMessage.GetComponent<Message>().Init(true, bubbleType);
        typingBubble.SetActive(false);
        levelManager.OnMessageSend();
    }
}
