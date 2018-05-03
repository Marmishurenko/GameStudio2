using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBackgroundLoop : MonoBehaviour {

    [SerializeField] float SPEED;
    float startPos;
    float length;
    float posX = 0;

    void Start() {
        startPos = transform.position.x;
        length = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update() {
        posX -= SPEED * Time.deltaTime;
        if (posX < 0)
            posX += length;
        transform.position = new Vector2(startPos + posX, transform.position.y);
    }
}
