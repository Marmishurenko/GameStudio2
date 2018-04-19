using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseEvent : MonoBehaviour {

	// create custom Event Class with Vector3 that it can pass Mouse pos as argument to another subscriber
	[System.Serializable] // Make it Serializable that it can be visible in editor
	public class Event : UnityEvent<Vector3>{};

	// Create mose events 
	public Event onMouseDownEvent;
	public Event onMouseUpEvent;
	public Event onMouseDragEvent;

    Transform cursor;

	// Use this for initialization
	Vector3 screenPoint;
	Vector3 offset;
	Vector3 scanPos;

    void Start() {
        cursor = GameObject.FindWithTag("Cursor").transform;
    }

    // When game in pause don't iteract
    bool isPause{
		get{ 
			if (Time.timeScale == 0) {
				return true;
			} else {
				return false;
			}
		}
	}

	public void OnDown()
	{
		if (!isPause) {
			if (this.enabled) {
				// Get mouse world position
				scanPos = transform.position; // used for world z position
				offset = scanPos - cursor.position; 

				// Invoke onMouseDownEvent with mouse position
				onMouseDownEvent.Invoke (cursor.position);
			}
		}
	}

	public void OnUp()
	{
		if (!isPause) {
			if (this.enabled) {
				// Invoke onMouseUpEvent with mouse position
				onMouseUpEvent.Invoke (cursor.position + offset);
			}
		}
	}

	public void OnDrag()
	{
		if (!isPause) {
			if (this.enabled) {
                // Invoke onMouseUpEvent with mouse position
                onMouseDragEvent.Invoke(cursor.position + offset);
            }
		}
	}


}
