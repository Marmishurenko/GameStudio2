using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SpongeControl : MonoBehaviour {
    string curTag;
    bool isAunt{ get { return curTag == "Aunt" ? true :false; }}
    public UnityEvent OnAuntEnter;
    public UnityEvent OnAuntExit;
    public UnityEvent OnAuntRub;
    public UnityEvent OnBucketEnter;
    public UnityEvent OnBucketExit;


	
    // Use this for initialization
	void Start () {
		
	}

    public void Rub(){
        if(isAunt){
            OnAuntRub.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
	{
        curTag = other.tag;
        print("enter");
        if (other.tag == "Aunt"){
            OnAuntEnter.Invoke();
        }
        if (other.tag == "Bucket")
        {
            OnBucketEnter.Invoke();
        }
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        curTag = "";
        print("exit");
        if (other.tag == "Aunt")
        {
            OnAuntExit.Invoke();
        }
        if (other.tag == "Bucket")
        {
            OnBucketExit.Invoke();
        }
    }
}
