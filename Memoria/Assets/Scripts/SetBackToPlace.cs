using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackToPlace : MonoBehaviour {

    private Vector3 initPos;
    bool isBask = false;
    private GameObject go;
    private AudioSource aus;
   
   

    // Use this for initialization
    void Start()
    {
       
        initPos = gameObject.transform.position;
        go = GameObject.Find("SetBackAS");
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
        if(go){
            go.GetComponent<AudioSource>().Play();
        }

    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("basket"))
        {     
            isBask = true;
        }
	}

}
