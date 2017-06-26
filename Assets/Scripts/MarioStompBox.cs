using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioStompBox : MonoBehaviour {
	private LevelManager t_LevelManager;

	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Contains("Enemy") && other.gameObject.tag != "Enemy/Piranha"
			&& other.gameObject.tag != "Enemy/Bowser") {
			Debug.Log (this.name + " OnTriggerEnter2D: recognizes " + other.gameObject.name);
			Enemy enemy = other.gameObject.GetComponent<Enemy> ();
			t_LevelManager.MarioStompEnemy (enemy);
			Debug.Log (this.name + " OnTriggerEnter2D: finishes calling stomp method on " + other.gameObject.name);
		}
	}
}
