using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoExtended {

    [SerializeField] Sprite[] girlMessages;
    [SerializeField] Sprite[] yomiMessages;
    [SerializeField] float girlPos;
    [SerializeField] float yomiPos;

    public void Init(bool isYomi, int size) {
        Vector2 frame = girlMessages[size].bounds.extents;
        frame = frame * 2 * 0.22f + new Vector2(0.4f, 0.2f);
        if (isYomi) {
            gameObject.GetComponent<SpriteRenderer>().sprite = yomiMessages[size];
            transform.position = new Vector3(yomiPos - frame.x / 2, -1.46f - frame.y / 2);
        } else {
            gameObject.GetComponent<SpriteRenderer>().sprite = girlMessages[size];
            transform.position = new Vector3(girlPos + frame.x / 2, -1.46f - frame.y / 2);
        }
        transform.parent.GetComponent<MessageWindow>().SetTargetPos(frame.y);
    }

    protected override void GameUpdate() {
    }
}
