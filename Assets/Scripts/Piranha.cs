using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : Enemy {
	private LevelManager t_LevelManager;
	private GameObject mario;
	private CircleCollider2D m_CircleCollider2D;
	private PatrolVertical patrolScript;

	private bool visible;
	private float maxDistanceToMove = 2; // should not emerge if Mario is within this distance of pipe

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		mario = FindObjectOfType<Mario> ().gameObject;
		m_CircleCollider2D = GetComponent<CircleCollider2D> ();
		patrolScript = GetComponent<PatrolVertical> ();
		visible = false;
		patrolScript.canMove = false;
		m_CircleCollider2D.enabled = false;

		starmanBonus = 100; // ???
		rollingShellBonus = 500; // ???
		hitByBlockBonus = 0;
		fireballBonus = 200;
		stompBonus = 0;
	}

	void OnBecameVisible() {
		visible = true;
	}

	void Update() {
		if (visible) {
			if (Mathf.Abs (mario.transform.position.x - transform.position.x) > maxDistanceToMove) {
				m_CircleCollider2D.enabled = true;
				patrolScript.canMove = true;
			} else if (patrolScript.isAtDownStop) { // do not emerge
				m_CircleCollider2D.enabled = false;
				patrolScript.canMove = false;
			}
		}
	}

	void DestroyPiranhaStruct() {
		Destroy (gameObject.transform.parent.gameObject);
	}

	public override void TouchedByStarmanMario() {
		DestroyPiranhaStruct ();
	}

	public override void TouchedByRollingShell() {
		DestroyPiranhaStruct ();
	}

	public override void HitBelowByBlock() {
	}

	public override void HitByMarioFireball() {
		DestroyPiranhaStruct ();
	}

	public override void StompedByMario() {
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			t_LevelManager.MarioPowerDown ();
		}
	}
}
