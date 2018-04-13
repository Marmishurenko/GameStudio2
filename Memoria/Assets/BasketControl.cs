using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BasketControl : MonoExtended
{
    int itemCounter = 0;
    public Camera cam;
    public Transform target;
    public float speed=3f;
   
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (itemCounter == 5)
        {
            gameManager.LoadTransitionScene();
        }
        print(itemCounter);

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        other.GetComponent<SpriteRenderer>().enabled = false;
        LerpCam();

        itemCounter++;

    }

    protected override void GameUpdate()
    {
    }

    private void LerpCam()
    {
        
        StartCoroutine(LerpToPosition(speed, target.position));
        



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






