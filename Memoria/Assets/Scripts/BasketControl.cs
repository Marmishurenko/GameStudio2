using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BasketControl : MonoExtended {
    int itemCounter = 0;
    public Camera cam;
    public Vector3 target;
    public float speed = 3f;
    private SpriteRenderer[] srs;
    bool CRStarted = false;
    public TMP_Text txt;
    public AnimationCurve curve;
    public string[] lines;
    private float camWidth;

    void Start() {
        float height = 2 * cam.orthographicSize;
        camWidth = height * cam.aspect;
        target = new Vector3(camWidth, 0, -10);//hardcoded, haha!
    }
    protected override void GameUpdate() {
        if (itemCounter == lines.Length) {
            Debug.Log("Load transition");
            gameManager.LoadTransitionScene();
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        other.transform.SetParent(gameObject.transform);
        other.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Destroy(other.gameObject, 1.0f);
        itemCounter++;
        if (itemCounter < lines.Length)
            LerpCam();
    }
    private void LerpCam() {
        if (itemCounter > 3) {
            speed = speed / (float)1.5f;
			// Add shopping cart movment sound here / You have to lerp sound maybe change the pitch / maybe work with playback speed
        }
        StartCoroutine(LerpToPosition(speed, target));
        target = new Vector3(target.x + camWidth, target.y, target.z);
    }
    IEnumerator LerpToPosition(float lerpSpeed, Vector3 newPosition) {
        float t = 0.0f;
        Vector3 startingPos = cam.transform.position;
        while (t < 1.0f) {
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);
            float curveTime = curve.Evaluate(t);
            cam.transform.position = Vector3.Lerp(startingPos, newPosition, curveTime);
            yield return 0;
        }
    }
    public void ShowText() {
        switch (itemCounter) {
            case 6:
                txt.SetText(lines[6]);
                break;
            case 5:
                txt.SetText(lines[5]);
                break;
            case 4:
                txt.SetText(lines[4]);
                txt.fontStyle = TMPro.FontStyles.Normal;
                break;
            case 3:
                txt.SetText(lines[3]);
                txt.fontStyle = TMPro.FontStyles.Italic;
                break;
            case 2:
                txt.SetText(lines[2]);
                break;
            case 1:
                txt.SetText(lines[1]);
                break;
            default:
                txt.SetText(lines[0]);
                break;
        }
    }
}






