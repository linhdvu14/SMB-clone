using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformVerticalSpawner : MonoBehaviour {
	public GameObject MovingPlatform;
	public bool isMoving;
	public float directionY = 1; // 1 for up, -1 for down 

	public Transform UpStop;
	public Transform DownStop;
	public Transform SpawnPos;

	private float WaitBetweenSpawn = 1.5f;
	private float minDistanceToMove = 40; // how close should Mario be for platforms to appear

	private GameObject mario;
	private float timer;

	// Use this for initialization
	void Start () {
		mario = FindObjectOfType<Mario> ().gameObject;
		timer = WaitBetweenSpawn / 2;
		isMoving = false;
	}


	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (mario.transform.position.x - transform.position.x) <= minDistanceToMove) {
			isMoving = true;
		} else {
			isMoving = false;
		}

		if (isMoving) {
			timer -= Time.deltaTime;

			if (timer <= 0) {
				GameObject clone = Instantiate (MovingPlatform, SpawnPos.position, Quaternion.identity);
				PatrolVertical patrolScript = clone.GetComponent<PatrolVertical> ();
				patrolScript.UpStop = UpStop;
				patrolScript.DownStop = DownStop;
				patrolScript.directionY = directionY;
				patrolScript.canMove = true;
				timer = WaitBetweenSpawn;
			}
		}
	}
}
