using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPickup : MonoBehaviour {
	public Animator anim;

	// Use this for initialization
	void Start () {
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player")) {
			GameManager.instance.health++;
			anim.SetTrigger("BendOver");
			StartCoroutine (WaitForAnimation ());
			Destroy (gameObject);


		}
	}

	IEnumerator WaitForAnimation(){
		yield return new WaitForSeconds (2f);
//		gameObject.GetComponent<MeshRenderer> ().enabled = false;
//		gameObject.GetComponent<BoxCollider> ().enabled = false;
		Destroy (gameObject);

//

	}
}
