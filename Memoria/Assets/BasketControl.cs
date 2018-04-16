using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BasketControl : MonoExtended
{
    int itemCounter = 0;
    public Camera cam;
    public Vector3 target;
    public float speed=3f;
    private SpriteRenderer[] srs;
    bool CRStarted = false;
   
    // Use this for initialization
    void Start()
    {
        target = new Vector3(17f, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (itemCounter == 5)
        {
            gameManager.LoadTransitionScene();
        }
        // print(itemCounter);
        print(CRStarted);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SpriteRenderer>() != null)
        {
            other.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            srs = other.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in srs){
                sr.enabled = false;
            }
             }

        LerpCam();
       
        itemCounter++;

    }

    protected override void GameUpdate()
    {
    }

    private void LerpCam()
    {
        StartCoroutine(LerpToPosition(speed, target));
        target = new Vector3(target.x + 17f, target.y, target.z);


    }

    IEnumerator LerpToPosition(float lerpSpeed, Vector3 newPosition)
    {
        
        float t = 0.0f;
        Vector3 startingPos = cam.transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);
            cam.transform.position = Vector3.Lerp(startingPos, newPosition, t);
            yield return 0;

        }
    }
}






