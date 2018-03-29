using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageList : MonoBehaviour {

    float targetPos;

	void Start () {
		
	}
	
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, targetPos), 10 * Time.deltaTime);
	}

    public void SetTargetPos(float distance) {
        targetPos += distance;
    }
}
