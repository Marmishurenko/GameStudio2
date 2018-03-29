using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour {

    [SerializeField] Sprite[] girlMessages;
    [SerializeField] Sprite[] yomiMessages;
    [SerializeField] float girlPos;
    [SerializeField] float yomiPos;

    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public void Init(bool isYomi, int type) {
        Vector2 frame = girlMessages[type].bounds.extents;
        frame = frame * 2 * 0.22f + new Vector2(0.4f, 0.2f);
        if (isYomi) {
            gameObject.GetComponent<SpriteRenderer>().sprite = yomiMessages[type];
            transform.position = new Vector3(yomiPos - frame.x / 2, -1.46f - frame.y / 2);
        } else {
            gameObject.GetComponent<SpriteRenderer>().sprite = girlMessages[type];
            transform.position = new Vector3(girlPos + frame.x / 2, -1.46f - frame.y / 2);
        }
        transform.parent.GetComponent<MessageList>().SetTargetPos(frame.y);
    }
}
