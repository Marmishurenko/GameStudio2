using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fingers : MonoBehaviour {

    public GameObject[] FingerArray;
    public float Health = 10;

    public Material blue;
    public Material black;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            Health--;
            if (Health < 0)
                Health = 0;
            RefreshFingers();
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            Health++;
            if (Health > 10)
                Health = 10;
            RefreshFingers();
        }
    }

    void RefreshFingers() {
        for (int i = 0; i < 10; i++) {
            if (i < Health)
                FingerArray[i].GetComponent<MeshRenderer>().sharedMaterial = blue;
            else
                FingerArray[i].GetComponent<MeshRenderer>().sharedMaterial = black;
        }
    }
}
