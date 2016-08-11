using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class randomSpawn : MonoBehaviour {
	public List<Vector3> spawnPoints;


	// Use this for initialization
	void Start () {
		int r = Random.Range (0, spawnPoints.Count);
		Vector3 spawnPoint = spawnPoints [r];
		gameObject.transform.position = spawnPoint;
	}

}
