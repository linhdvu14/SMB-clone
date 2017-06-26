using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebar : Enemy {
	public Transform pivot;
	public float rotateSpeed = 75;
	private LevelManager t_LevelManager;
	private GameObject mario;
	public bool canMove;
	private bool canMoveAutomatic = true;
	private float minDistanceToMove = 14f;

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		mario = FindObjectOfType<Mario> ().gameObject;

		starmanBonus = 0;
		rollingShellBonus = 0;
		hitByBlockBonus = 0;
		fireballBonus = 0;
		stompBonus = 0;
	}


	void Update() {
		if (!canMove & Mathf.Abs (mario.transform.position.x - transform.position.x) <= minDistanceToMove && canMoveAutomatic) {
			canMove = true;
		} else if (canMove) {
			transform.RotateAround(pivot.position, Vector3.forward, rotateSpeed * Time.deltaTime);
		}
	}

	public override void TouchedByStarmanMario() {
	}

	public override void TouchedByRollingShell() {
	}

	public override void HitBelowByBlock() {
	}

	public override void HitByMarioFireball() {
	}

	public override void StompedByMario() {
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			t_LevelManager.MarioPowerDown ();
		}
	}

}
