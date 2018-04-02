using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mouse : MonoBehaviour {

	// create custom Event Class with Vector3 that it can pass Mouse pos as argument to another subscriber
	[System.Serializable] // Make it Serializable that it can be visible in editor
	public class MouseEvent : UnityEvent<Vector3>{};

	// Create mose events 
	public MouseEvent onMouseDownEvent;
	public MouseEvent onMouseUpEvent;
	public MouseEvent onMouseDragEvent;

	// Use this for initialization
	Vector3 screenPoint;
	Vector3 offset;
	Vector3 scanPos;

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

	void OnMouseDown()
	{
		if (!isPause) {
			if (this.enabled) {
				// Get mouse world position
				scanPos = transform.position; // used for world z position
				screenPoint = Camera.main.WorldToScreenPoint (scanPos);
				Vector3 m = Camera.main.ScreenToWorldPoint ( new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
				offset = scanPos - m; 

				// Invoke onMouseDownEvent with mouse position
				onMouseDownEvent.Invoke (m);
			}
		}
	}

	void OnMouseUp()
	{
		if (!isPause) {
			if (this.enabled) {
				// Get mouse world position
				Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
				Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;

				// Invoke onMouseUpEvent with mouse position
				onMouseUpEvent.Invoke (curPosition);
			}
		}
	}

	void OnMouseDrag()
	{
		if (!isPause) {
			if (this.enabled) {
				// Get mouse world position
				Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
				Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;

				// Invoke onMouseDragEvent with mouse position
				onMouseDragEvent.Invoke (curPosition);
			}
		}
	}


}
