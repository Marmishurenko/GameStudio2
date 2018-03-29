using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTJ.Alembic;

public class ObjAnimation : MonoBehaviour {

	private Vector3 initScale;
	private Vector3 updatedScale;
	public float scaleMultiplier = 10f;

	// Use this for initialization
	void Start () {
		//initScale = gameObject.transform.localScale;

	}

	
	// Update is called once per frame
	void Update () {

		
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Player")) {
			print (GameManager.instance.health--);
			transform.localScale+= new Vector3 (0.1f,0.1f,0.1f);
			GameManager.instance.health--;
		}
	}
}
