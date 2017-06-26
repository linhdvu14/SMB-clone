using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PipeWarpSide : MonoBehaviour {
	private LevelManager t_LevelManager;
	private Mario mario;
	private bool reachedPortal;

	public string sceneName;
	public int spawnPipeIdx;
	public bool leadToSameLevel = true;


	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		mario = FindObjectOfType<Mario> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			mario.AutomaticWalk (mario.levelEntryWalkSpeedX);
			reachedPortal = true;
			t_LevelManager.timerPaused = true;
			Debug.Log (this.name + " OnTriggerEnter2D: " + transform.parent.gameObject.name 
				+ " recognizes player, should automatic walk");
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player" && reachedPortal) {
			t_LevelManager.soundSource.PlayOneShot (t_LevelManager.pipePowerdownSound);

			if (leadToSameLevel) {
				Debug.Log (this.name + " OnCollisionEnter2D: " + transform.parent.gameObject.name 
					+ " teleports player to different scene same level " + sceneName
					+ ", pipe idx " + spawnPipeIdx);
				t_LevelManager.LoadSceneCurrentLevelSetSpawnPipe (sceneName, spawnPipeIdx);
			} else {
				Debug.Log (this.name + " OnCollisionEnter2D: " + transform.parent.gameObject.name
					+ " teleports player to new level " + sceneName 
					+ ", pipe idx " + spawnPipeIdx);
				t_LevelManager.LoadNewLevel (sceneName);
			}
		}
	}
}
