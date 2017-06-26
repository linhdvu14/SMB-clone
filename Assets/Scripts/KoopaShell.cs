using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaShell : Enemy {
	private Animator m_Animator;
	private Rigidbody2D m_Rigidbody2D;
	private LevelManager t_LevelManager;
	private Mario mario;

	public GameObject Koopa;
	public float rollSpeedX = 7;
	private float waitTillRevive = 5;
	private float waitTillRespawn = 1.5f;

	private float currentRollVelocityX;
	private bool isReviving;
	public bool isRolling;

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		mario = FindObjectOfType<Mario> ();
		m_Animator = GetComponent<Animator> ();
		m_Rigidbody2D = GetComponent<Rigidbody2D> ();
		isReviving = false;
		isRolling = false;

		starmanBonus = 200; // ???
 		rollingShellBonus = 500; // ???
		hitByBlockBonus = 100; // ???
		fireballBonus = 100; // ???
		stompBonus = 500;
	}

	void Update() {
		if (!isReviving && !isRolling) {
			waitTillRevive -= Time.deltaTime;
			if (waitTillRevive <= 0) {
				m_Animator.SetTrigger ("revived");
				isReviving = true;
			}
		} else if (isReviving && !isRolling) {
			waitTillRespawn -= Time.deltaTime;
			if (waitTillRespawn <= 0) {
				Instantiate (Koopa, transform.position, Quaternion.identity);
				Destroy (gameObject);
			}
		} else if (isRolling) {
			m_Rigidbody2D.velocity = new Vector2 (currentRollVelocityX, m_Rigidbody2D.velocity.y);
		}

		if (hasBeenStomped) {
			stompBonus = 0;
		}
	}

	public override void TouchedByRollingShell() {
		if (!isRolling) {
			FlipAndDie ();
		} else { // change direction if touched by another rolling shell
			currentRollVelocityX = -currentRollVelocityX;
			rollingShellBonus = 0; // ???
		}
	}

	bool hasBeenStomped = false;
	public override void StompedByMario() {
		isBeingStomped = true;
		if (!isRolling) {
			// start rolling left/right depending on Mario's direction
			if (mario.transform.localScale.x == 1) {
				currentRollVelocityX = rollSpeedX;
			} else if (mario.transform.localScale.x == -1) {
				currentRollVelocityX = -rollSpeedX;
			}
			isRolling = true;
			m_Animator.SetTrigger ("rolled");
		} else {
			isRolling = false;
		}
		hasBeenStomped = true;
		isBeingStomped = false;
	}
		

	void OnCollisionEnter2D(Collision2D other) {
		if (isRolling) {
			if (other.gameObject.tag.Contains("Enemy")) { // kill off other enemies
				Enemy enemy = other.gameObject.GetComponent<Enemy>();
				t_LevelManager.RollingShellTouchEnemy (enemy);
			} else {
				currentRollVelocityX = -currentRollVelocityX;
			}
		}
	}
}
