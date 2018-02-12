using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour {
	public float speed;
	public GameObject particlePrefab;
	public float timeMin=5;
	public float timeMax=10;
	private float timeSpawn;
	public GameObject boundsObj;
	private float minY,maxY,minZ,maxZ,minX,maxX;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			InstantiateAtRandomSpawnPoint ();

		}
		
	}

	public void InstantiateAtRandomSpawnPoint(){
		Instantiate (particlePrefab, GetARandomPos (), Quaternion.identity);
	}



	IEnumerator RanTime(){
		timeSpawn = Random.Range (timeMin, timeMax);
		yield return new WaitForSeconds (timeSpawn);
	}


	public Vector3 GetARandomPos(){

		Vector3 scale = boundsObj.transform.localScale;
		Vector3 pos = boundsObj.transform.position;

		float boundXFrom = pos.x - scale.x / 2f;
		float boundXTo = pos.x + scale.x / 2f;

		float boundZFrom = pos.z - scale.z / 2f;
		float boundZTo = pos.z + scale.z / 2f;

		float randomX = Random.Range (boundXFrom, boundXTo);
		float randomZ = Random.Range (boundZFrom, boundZTo);

		float ypos = transform.position.y;

		return new Vector3 (randomX, ypos, randomZ);
	}



}
