using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour {

	public float moveSpeed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate( 0,-moveSpeed*Time.deltaTime , 0);

		
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "Arm") {
			print ("catch!");
			Destroy (gameObject);
			GameManager.instance.health++;
		}
//		} else {
//			Destroy (gameObject);
//		}
	}
}
