using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSControl : MonoBehaviour {
    ParticleSystem ps;
    ParticleSystem.EmissionModule emissionModule;
    public float fadeTime = 0.2f;
    float timer;
    float curRate;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();

        if(ps==null){
            print("Attach this script to object with particle system");
        }else{
            emissionModule = ps.emission;    
            curRate = emissionModule.rateOverTime.constant;
        }
	}

    public void Play(){
        ps.Play();
    }

	// Update is called once per frame
    public void DecreaseRate(){
        if (timer > 0)
        {
            timer -=  Time.deltaTime / fadeTime;
            emissionModule.rateOverTime = timer * curRate;
        }else{
            ps.Stop();
        }
    }

    public void ResetRate(){
        timer = 1;
        emissionModule.rateOverTime = curRate;
    }



}
