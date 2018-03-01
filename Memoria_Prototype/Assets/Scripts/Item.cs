using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerClickHandler {

    [SerializeField] Sprite itemRight, itemWrong;
    bool isRight;

    void Start() {

    }

    void Update() {

    }

    public void SetItem(bool isRight) {
        this.isRight = isRight;
        gameObject.GetComponent<Image>().enabled = true;
        if (isRight)
            gameObject.GetComponent<Image>().sprite = itemRight;
        else
            gameObject.GetComponent<Image>().sprite = itemWrong;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (isRight) {
            gameObject.GetComponent<Image>().enabled = false;
            Camera.main.GetComponent<ShoppingLevelManager>().ItemPicked();
        }
    }
}
