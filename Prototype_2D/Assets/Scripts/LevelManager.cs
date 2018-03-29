using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField] GameObject pill;
    [SerializeField] GameObject obstacle;
    [SerializeField] float SCROLL_SPEED;
    [SerializeField] float GENERATE_TIME_MIN;
    [SerializeField] float GENERATE_TIME_MAX;
    [SerializeField] float PILL_CHANCE;

    List<GameObject> objectList = new List<GameObject>();
    float timer = 0;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        timer -= Time.deltaTime;
        if (timer <= 0) {
            timer = Random.Range(GENERATE_TIME_MIN, GENERATE_TIME_MAX);
            
            if (Random.value < PILL_CHANCE) {
                objectList.Add(Instantiate(pill));
            } else {
                objectList.Add(Instantiate(obstacle));
            }
        }

        List<GameObject> newList = new List<GameObject>();
        foreach(GameObject obj in objectList) {
            obj.transform.position += new Vector3(-SCROLL_SPEED * Time.deltaTime, 0);
            if (obj.transform.position.x < -10)
                Destroy(obj);
            else
                newList.Add(obj);
        }
        objectList = newList;
	}
}
