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
    public TMP_Text txt;
    public AnimationCurve curve;

    // Use this for initialization
    void Start()
    {
        target = new Vector3(17.69f, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (itemCounter == 7)
        {
            //itemCounter = 0;
            gameManager.LoadTransitionScene();
        }
        // print(itemCounter);
       
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

       
       
        itemCounter++;
       // ShowText();
        LerpCam();

    }

    protected override void GameUpdate()
    {
    }

    private void LerpCam()
    {
        StartCoroutine(LerpToPosition(speed, target));
        target = new Vector3(target.x + 17.69f, target.y, target.z);


    }

    IEnumerator LerpToPosition(float lerpSpeed, Vector3 newPosition)
    {
        
        float t = 0.0f;
        Vector3 startingPos = cam.transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);
            float curveTime = curve.Evaluate(t);
            cam.transform.position = Vector3.Lerp(startingPos, newPosition, curveTime);
            yield return 0;

        }
    }

    public void ShowText()
    {
        switch (itemCounter)
        {
            case 6:
                txt.SetText("...");
                break;
            case 5:
                txt.SetText("I think it worked…");
                break;
            case 4:
                txt.SetText("ok…I can just pretend I’m listening to the music and don’t hear him.");
                break;
            case 3:
                txt.SetText("Is this Brian over there? — Yumi? Hi! It’s been a while!How…");
                break;
            case 2:
                txt.SetText("Uh..this is torture.");
                break;
            case 1:
                txt.SetText(" Funny, I can’t remember when or what I ate last time..");
                break;
            default:
                txt.SetText("I can’t remember if I always hated grocery stores..");
                break;

        }
    }
}






