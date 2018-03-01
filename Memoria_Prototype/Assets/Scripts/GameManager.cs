using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int week = 1;
    int gamePhase = 0;

    void Start() {
        DontDestroyOnLoad(gameObject);
        StartCoroutine("Pause");
    }

    public void NextPhase() {
        week++;
        SceneManager.LoadScene("WeekStart");
        StartCoroutine("Pause");
    }

    IEnumerator Pause() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Texting");
        //SceneManager.LoadScene("Shopping2");
    }
}
