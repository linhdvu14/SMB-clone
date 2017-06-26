using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserFire : Enemy {
	private LevelManager t_LevelManager;
	private Rigidbody2D m_Rigidbody2D;

	private float absSpeedX = 18;
	public float directionX = -1; // 1 for right, -1 for left

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		m_Rigidbody2D = FindObjectOfType<Rigidbody2D> ();
		transform.localScale = new Vector3 (directionX, 1, 1); // orient sprite

		starmanBonus = 0;
		rollingShellBonus = 0;
		hitByBlockBonus = 0;
		fireballBonus = 0;
		stompBonus = 0;
	}

	void Update() {
		m_Rigidbody2D.velocity = new Vector2 (absSpeedX * directionX, m_Rigidbody2D.velocity.y);
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
