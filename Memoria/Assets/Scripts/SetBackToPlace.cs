using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackToPlace : MonoBehaviour {

    private Vector3 initPos;

    // Use this for initialization
    void Start()
    {
        initPos = gameObject.transform.position;
    }

    private void Update()
    {
        
    }

    public void SetBack(){
        gameObject.transform.position = initPos;
    }
		
}
