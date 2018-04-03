using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour {

    public ParticleSystem bubbles;

	public void setPosition(Vector3 pos){
		transform.position = pos;
	}


}
