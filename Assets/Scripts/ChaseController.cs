using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChaseController : MonoBehaviour {
	public GameObject player;
	public float moveSpeed = 5.0f;
	public float staticAxis;
	public int gridSize = 50;

	private Queue trail;
	private Vector3 patrolPoint1;
	private Vector3 patrolPoint2;
	private Vector3 destination;
	private bool seePlayer;
	private float[] grid;
	private float scale;
	private float y;
	private bool chase;


	// Use this for initialization
	void Start () {
		scale = 2.290408f;
		setGrid ();
		y = -2.5f;
		trail = new Queue ();
		seePlayer = false;
		chase = false;
		patrolPoint1 = new Vector3(11.5f+2.3f, -2.5f, 2.3f);
		patrolPoint2 = new Vector3(38.88f, -2.5f, 2.3f);
		destination = patrolPoint2;
	}

	Vector3 findDestination(Vector3 currentDest) {
		if (currentDest == patrolPoint1) {
			return patrolPoint2;
		}
		return patrolPoint1;
	}

	void deathScene() {
	}
		
	void Update () {
		detectPlayer ();
		if (seePlayer) { //chase action
			Vector3 pos = roundPositionToGrid (player.transform.position);
			Debug.Log (pos);
//			Debug.Log (pos.z);
//			if (Vector3.Distance (pos, transform.position) <= 1) {
//				deathScene ();
//			}
			if (!(trail.Contains (pos))) {
				trail.Enqueue (pos);
			}
			if (!chase) {
				destination = (Vector3) trail.Dequeue ();
				chase = true;
			} else if (Vector3.Distance (destination, transform.position) <= 1) {
				destination = (Vector3) trail.Dequeue ();
			}
			transform.LookAt (destination);
			transform.position += transform.forward*moveSpeed*Time.deltaTime;
		} else { //basic patrol action
			if (Vector3.Distance (destination, transform.position) <= 1) {
				destination = findDestination (destination);
			}
			transform.LookAt (destination);
			transform.position += transform.forward*moveSpeed*Time.deltaTime;
		}
	}

	void detectPlayer(){
		if (Mathf.Abs(player.transform.position.x - patrolPoint1.x) <= 1) {
			seePlayer = true;
		}
	}

	Vector3 roundPositionToGrid(Vector3 pos) {
		int xInt = Mathf.RoundToInt (pos.x / scale);
		int zInt = Mathf.RoundToInt (pos.z / scale);
		float x = (float) grid [xInt];
		float z = (float) grid [zInt];
		return new Vector3 (x, y, z);
	}

	void setGrid() {
		grid = new float[gridSize];
		for (int i = 1; i <= gridSize-1; i++){
			grid [i] = scale*i;
			Debug.Log ("i: " + i);
			Debug.Log ("scale: " + scale);
			Debug.Log (grid [i]);
		}
	}

	void OnAnimatorMove() {
		Animator animator = GetComponent<Animator> ();
		if (animator) {
			Vector3 newPosition = transform.position;
			newPosition.z += animator.GetFloat ("RunSpeed") * Time.deltaTime;
			transform.position = newPosition;
		}
	}
}
	