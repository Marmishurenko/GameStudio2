using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shelves : MonoBehaviour {

    [SerializeField] float posXMin, posXMax;
    [SerializeField] float posYMin, posYMax;
    [SerializeField] float scrollRangeX, scrollRangeY;
    public float scrollSpeed;

    void Start() {

    }

    void Update() {
        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 pos = transform.localPosition;
        if (mousePos.x < scrollRangeX)
            pos.x = Mathf.MoveTowards(pos.x, posXMax, scrollSpeed);
        else if (mousePos.x > 1 - scrollRangeX)
            pos.x = Mathf.MoveTowards(pos.x, posXMin, scrollSpeed);
        else if (mousePos.y < scrollRangeY)
            pos.y = Mathf.MoveTowards(pos.y, posYMax, scrollSpeed);
        else if (mousePos.y > 1 - scrollRangeY)
            pos.y = Mathf.MoveTowards(pos.y, posYMin, scrollSpeed);
        transform.localPosition = pos;
    }

    public void Reset() {
        transform.localPosition = new Vector3(posXMax, posYMin);
    }
}
