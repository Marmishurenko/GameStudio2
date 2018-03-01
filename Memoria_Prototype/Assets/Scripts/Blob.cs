using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour {

    [SerializeField] float verticalSpeed;

    bool onGround;

    public float colorRate;

    void Start() {
        colorRate = 1;
    }

    void Update() {
        colorRate -= 3 * Time.deltaTime;
        //gameObject.GetComponent<SpriteRenderer>().

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (onGround) {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, verticalSpeed);
                onGround = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {

        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        onGround = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "Pill":
                collision.gameObject.transform.position += new Vector3(0, -999);
                break;
            case "Obstacle":
                Destroy(gameObject);
                break;
        }
    }
}
