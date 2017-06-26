using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioFireball : MonoBehaviour {
	public float directionX; // > 0 for right, < 0 for left
	private float explosionDuration = .25f;
	private Vector2 absVelocity = new Vector2 (20, 11);

	private LevelManager t_LevelManager;
	private Rigidbody2D m_Rigidbody2D;
	private Animator m_Animator;

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		m_Rigidbody2D = GetComponent<Rigidbody2D> ();
		m_Animator = GetComponent<Animator> ();

		// initial velocity
		m_Rigidbody2D.velocity = new Vector2(directionX * absVelocity.x, -absVelocity.y);
	}
	
	// Update is called once per frame
	void Update () {
		m_Rigidbody2D.velocity = new Vector2 (directionX * absVelocity.x, m_Rigidbody2D.velocity.y);
	}

	void Explode() {
		m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
		m_Animator.SetTrigger ("exploded");
		t_LevelManager.soundSource.PlayOneShot (t_LevelManager.bumpSound);
		Destroy (gameObject, explosionDuration);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag.Contains("Enemy")) {
			Enemy enemy = other.gameObject.GetComponent<Enemy> ();
			t_LevelManager.FireballTouchEnemy (enemy);
			Explode ();
		} else { // bounce off grounds
			Vector2 normal = other.contacts[0].normal;
			Vector2 leftSide = new Vector2 (-1f, 0f);
			Vector2 rightSide = new Vector2 (1f, 0f);
			Vector2 bottomSide = new Vector2 (0f, 1f);

			if (normal == leftSide || normal == rightSide) { // explode if side hit
				Explode ();
			} else if (normal == bottomSide) { // bounce off
				m_Rigidbody2D.velocity = new Vector2 (m_Rigidbody2D.velocity.x, absVelocity.y);
			} else {
				m_Rigidbody2D.velocity = new Vector2 (m_Rigidbody2D.velocity.x, -absVelocity.y);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag.Contains ("Enemy")) {
			Enemy enemy = other.gameObject.GetComponent<Enemy> ();
			t_LevelManager.FireballTouchEnemy (enemy);
			Explode ();
		} 
	}
}
