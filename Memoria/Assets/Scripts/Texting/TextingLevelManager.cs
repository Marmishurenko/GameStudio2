using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextingLevelManager : MonoExtended {

    [SerializeField] string[] textingScripts;
    [SerializeField] GameObject messagePrefab;
    [SerializeField] GameObject messageWindow;
    [SerializeField] GameObject typingDots;
    [SerializeField] TypingArea typingArea;
    [SerializeField] TextingNarrative narrative;
    [SerializeField] TextingBackButton backButton;

    string[] conversationScripts;


    string messageScript;
    int messageIndex;

    void Start() {
        conversationScripts = textingScripts[gameManager.textingSceneStage].Split('/');
        messageIndex = 0;
        StartCoroutine("NextMessage");
    }

    IEnumerator NextMessage() {
        messageIndex++;
        messageScript = conversationScripts[messageIndex];
        switch (messageScript[0]) {

            case 'G':
                // Girlfriend's message
                messageScript = messageScript.Remove(0, 1);
                yield return new WaitForSeconds(1f);
                yield return new WaitForSeconds(GetWaitingTime());
                if (messageScript.Length > 0) {
                    GameObject newMessage = Instantiate(messagePrefab, messageWindow.transform);
                    if (messageScript[0] != '*')
                        newMessage.GetComponent<Message>().Init(false, 'C' - messageScript[0]);
                    else
                        newMessage.GetComponent<Message>().Init(false, 'C' - messageScript[1] + 4);
                }
                StartCoroutine("NextMessage");
                break;

            case 'Y':
                // Player's message
                messageScript = messageScript.Remove(0, 1);
                yield return new WaitForSeconds(0.7f);
                float t = GetWaitingTime();
                if (t > 0) {
                    typingDots.SetActive(true);
                    yield return new WaitForSeconds(t);
                    typingDots.SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                }
                if (messageScript.Length > 0) {
                    if (messageScript[0] != '*')
                        typingArea.ShowBubble('C' - messageScript[0]);
                    else
                        typingArea.ShowBubble('C' - messageScript[1] + 3);
                } else {
                    StartCoroutine("NextMessage");
                }
                break;

            case 'T':
                // Show narrative text
                narrative.Show();
                StartCoroutine("NextMessage");
                break;

            case 'E':
                // End of day
                yield return new WaitForSeconds(1);
                backButton.GetComponent<TextingBackButton>().flashing = true;
                break;

            default:
                // Blank time
                if (messageScript[0] < '0' || messageScript[0] > '9')
                    break;
                yield return new WaitForSeconds(GetWaitingTime());
                StartCoroutine("NextMessage");
                break;
        }
    }

    public void OnMessageSend() {
        StartCoroutine("NextMessage");
    }

    float GetWaitingTime() {
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

    protected override void GameUpdate() {
    }
}
