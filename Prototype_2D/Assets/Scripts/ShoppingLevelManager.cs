using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingLevelManager : MonoBehaviour {

    [SerializeField] GameObject weekScreen;
    [SerializeField] Text weekText;
    [SerializeField] Transform shelves;
    int week = 1;
    int gameState = 0;
    float timer;

    void Start() {
        TurnOnWeekScreen();
    }

    void Update() {
        if (gameState == 0) {
            // Set items on shelves
            if (Input.GetMouseButtonDown(0)) {
                gameState = 1;
                shelves.GetComponent<Shelves>().Reset();
                timer = 999;
                weekScreen.SetActive(false);
                // Set all items to the wrong type
                foreach (Transform column in shelves) {
                    foreach (Transform row in column) {
                        foreach (Transform item in row) {
                            item.GetComponent<Item>().SetItem(false);
                        }
                    }
                }
                if (week > -1)
                    shelves.GetChild(2).GetChild(4).GetChild(1).GetComponent<Item>().SetItem(true);
                else if (week == 5)
                    shelves.GetChild(3).GetChild(1).GetChild(1).GetComponent<Item>().SetItem(true);
                else
                    shelves.GetChild(Random.Range(1, 4)).GetChild(Random.Range(0, 3)).GetChild(Random.Range(0, 4)).GetComponent<Item>().SetItem(true);
            }
            return;
        }

        if (timer > 10)
            return;
        timer -= Time.deltaTime;
        if (timer <= 0) {
            gameState = 0;
            week++;
            shelves.GetComponent<Shelves>().scrollSpeed *= 0.7f;
            TurnOnWeekScreen();
        }
    }

    void TurnOnWeekScreen() {
        weekScreen.SetActive(true);
        weekText.text = "Week " + week;
    }

    public void ItemPicked() {
        timer = 0.7f;
    }
}
