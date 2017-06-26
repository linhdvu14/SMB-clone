using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour {
	private LevelManager t_LevelManager;

	private Transform flag;
	private Transform flagStop;
	private bool moveFlag;

	private float flagVelocityY = -.08f;

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		flag = transform.FindChild ("Flag");
		flagStop = transform.FindChild ("Flag Stop");
	}

	void FixedUpdate() {
		if (moveFlag && flag.position.y > flagStop.position.y) {
			flag.position = new Vector2(flag.position.x, flag.position.y + flagVelocityY);
		}
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player" && !moveFlag) {
			moveFlag = true;
			t_LevelManager.MarioReachFlagPole ();
		}
	}
}
