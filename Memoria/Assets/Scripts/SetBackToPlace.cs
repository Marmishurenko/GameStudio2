using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackToPlace : MonoBehaviour {

    private Vector3 initPos;
    bool isBask = false;
   

    // Use this for initialization
    void Start()
    {
       
        initPos = gameObject.transform.position;
    }

    private void Update()
    {
        
    }

    public void SetBack(){

        if (!isBask)
        {
            gameObject.transform.position = initPos;
        }
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("basket"))
        {     
            isBask = true;
        }
	}

}
