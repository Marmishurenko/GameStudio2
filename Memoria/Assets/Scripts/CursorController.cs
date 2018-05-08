using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorController : MonoExtended {

    [SerializeField] float cursorSpeedScale;
    [SerializeField] float cursorSpeedCap;
    [SerializeField] float cursorSpeedLerp;

    [SerializeField] List<Sprite> spriteList;

    Vector2 cursorPosition;
    Vector2 cursorTargetPosition;
    List<GameObject> targetList = new List<GameObject>();
    public int spriteOffset = 0;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cursorPosition = Vector2.zero;
        cursorTargetPosition = Vector2.zero;

        DontDestroyOnLoad(gameObject);
    }

    public void Hide() {
        transform.GetComponent<SpriteRenderer>().enabled = false;
        cursorPosition = Vector2.zero;
        cursorTargetPosition = Vector2.zero;
    }

    public void Show() {
        transform.GetComponent<SpriteRenderer>().enabled = true;
        cursorPosition = Vector2.zero;
        cursorTargetPosition = Vector2.zero;
    }

    protected override void GameUpdate() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Update cursor position
        Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * cursorSpeedScale;
        if (delta.magnitude > cursorSpeedCap)
            delta = delta.normalized * cursorSpeedCap;
        cursorTargetPosition += delta;
        cursorPosition = Vector3.Lerp(cursorPosition, cursorTargetPosition, cursorSpeedLerp);
        transform.position = cursorPosition + (Vector2)Camera.main.transform.position;

        if (Input.GetMouseButtonDown(0)) {
            // Fire mouse event
            if (targetList.Count > 0 && targetList[targetList.Count - 1] != null) {
                MouseEvent me = targetList[targetList.Count - 1].GetComponent<MouseEvent>();
                if (me != null)
                    me.OnDown();
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            // Fire mouse event
            if (targetList.Count > 0 && targetList[targetList.Count - 1] != null) {
                MouseEvent me = targetList[targetList.Count - 1].GetComponent<MouseEvent>();
                if (me != null)
                    me.OnUp();
            }
        }

        if (Input.GetMouseButton(0)) {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[spriteOffset + 1];
            // Fire mouse event
            if (targetList.Count > 0 && targetList[targetList.Count - 1] != null) {
                MouseEvent me = targetList[targetList.Count - 1].GetComponent<MouseEvent>();
                if (me != null)
                    me.OnDrag();
            }
        } else {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[spriteOffset];
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        SpriteRenderer sr = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (sr == null)
            return;
        // Insert the new object into the list in sorting layer order
        foreach (GameObject other in targetList) {
            if (other == null)
                continue;
            SpriteRenderer otherSR = other.GetComponentInChildren<SpriteRenderer>();
            if (sr.sortingLayerID > otherSR.sortingLayerID)
                continue;
            if (sr.sortingLayerID < otherSR.sortingLayerID) {
                targetList.Insert(targetList.IndexOf(other), collision.gameObject);
                return;
            }
            if (sr.sortingOrder <= otherSR.sortingOrder) {
                targetList.Insert(targetList.IndexOf(other), collision.gameObject);
                return;
            }
        }
        // Insert to the end of the list
        targetList.Add(collision.gameObject);
    }

    void OnTriggerExit2D(Collider2D collision) {
        targetList.Remove(collision.gameObject);
    }
}
