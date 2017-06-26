using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starman : MonoBehaviour {
	private LevelManager t_LevelManager;

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		t_LevelManager.soundSource.PlayOneShot (t_LevelManager.powerupAppearSound);
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			t_LevelManager.MarioInvincibleStarman ();
			t_LevelManager.soundSource.PlayOneShot (t_LevelManager.powerupSound);
			Destroy (gameObject);
		}
	}
}
