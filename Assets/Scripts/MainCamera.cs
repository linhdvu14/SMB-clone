using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
	public GameObject target;
	public float followAhead = 2.6f;
	public float smoothing = 5;
	public bool canMove;
	public bool canMoveBackward = false;

	private Transform leftEdge;
	private Transform rightEdge;
	private float cameraWidth;
	private Vector3 targetPosition;


	// Use this for initialization
	void Start () {
		Mario mario = FindObjectOfType<Mario> ();
		target = mario.gameObject;

		GameObject boundary = GameObject.Find ("Level Boundary");
		leftEdge = boundary.transform.FindChild ("Left Boundary").transform;
		rightEdge = boundary.transform.FindChild ("Right Boundary").transform;
		float aspectRatio = GetComponent<MainCameraAspectRatio> ().targetAspects.x /
		                    GetComponent<MainCameraAspectRatio> ().targetAspects.y;
		cameraWidth = Camera.main.orthographicSize * aspectRatio;

		// Initialize camera's position
		Vector3 spawnPosition = FindObjectOfType<LevelManager>().FindSpawnPosition();
		targetPosition = new Vector3 (spawnPosition.x, transform.position.y, transform.position.z);

		bool passedLeftEdge = targetPosition.x < leftEdge.position.x + cameraWidth;

		if (rightEdge.position.x - leftEdge.position.x <= cameraWidth * 2) {  // center camera if already within boundaries
			transform.position = new Vector3 ((leftEdge.position.x + rightEdge.position.x) / 2f, targetPosition.y, targetPosition.z);
			canMove = false;
		} else if (passedLeftEdge) { // do not let camera shoot pass left edge
			transform.position = new Vector3 (leftEdge.position.x + cameraWidth, targetPosition.y, targetPosition.z);
			canMove = true;
		} else {
			transform.position = new Vector3 (targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
			canMove = true;
		}
	}


	// Update is called once per frame
	void Update () {
		if (canMove) {
			bool passedLeftEdge = transform.position.x < leftEdge.position.x + cameraWidth;
			bool passedRightEdge = transform.position.x > rightEdge.position.x - cameraWidth;

			targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

			// move target of camera ahead of Player, but do not let camera shoot pass
			// level boundaries
			if (target.transform.localScale.x > 0f && !passedRightEdge &&
			    targetPosition.x - leftEdge.position.x >= cameraWidth - followAhead) {
				if (canMoveBackward || target.transform.position.x + followAhead >= transform.position.x) {
					targetPosition = new Vector3 (targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
					transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);
				}

			} else if (target.transform.localScale.x < 0f && canMoveBackward && !passedLeftEdge 
				&& rightEdge.position.x - targetPosition.x >= cameraWidth - followAhead) {
				targetPosition = new Vector3 (targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
				transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);
			}
		}
			



//		void Update () { // can move camera both left and right
//			if (canMove) {
//				bool passedLeftEdge = transform.position.x < leftEdge.position.x + cameraWidth;
//				bool passedRightEdge = transform.position.x > rightEdge.position.x - cameraWidth;
//
//				targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
//
//				// move target of camera ahead of Player, but do not let camera shoot pass
//				// level boundaries
//				if (target.transform.localScale.x > 0f && !passedRightEdge && 
//					targetPosition.x - leftEdge.position.x >= cameraWidth - followAhead) {
//					targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
//					transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
//				} else if (target.transform.localScale.x < 0f && !passedLeftEdge && 
//					rightEdge.position.x - targetPosition.x >= cameraWidth - followAhead) {
//					targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
//					transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
//				}
//			}
//		}
	}
}