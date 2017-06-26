using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Powerup collectible behaviors
 * Applicable to: Big Mushroom, Fireflower
 */

public class PowerupObject : MonoBehaviour {
	private LevelManager t_LevelManager;
	private Rigidbody2D m_Rigidbody2D;

	public Vector2 initialVelocity;

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		m_Rigidbody2D = GetComponent<Rigidbody2D> ();
		m_Rigidbody2D.velocity = initialVelocity;
		t_LevelManager.soundSource.PlayOneShot (t_LevelManager.powerupAppearSound);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			t_LevelManager.MarioPowerUp ();
			Destroy (gameObject);
		}
	}
}
