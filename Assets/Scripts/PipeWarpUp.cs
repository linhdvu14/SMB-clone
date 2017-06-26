using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeWarpUp : MonoBehaviour {
	private Mario mario;
	private Transform stop;

	private float platformVelocityY = .05f;
	public bool isTakingMarioUp;

	private LevelManager t_LevelManager;

	public bool resetSpawnPoint = false;

	void Start () {
		mario = FindObjectOfType<Mario> ();
		stop = transform.parent.transform.FindChild ("Platform Stop");
		GameStateManager t_GameStateManager = FindObjectOfType<GameStateManager> ();
		t_LevelManager = FindObjectOfType<LevelManager> ();

		Debug.Log (this.name + " Start: " + transform.parent.gameObject.name 
			+ " spawnFromPoint=" + t_GameStateManager.spawnFromPoint.ToString()
			+ " with idx=" + t_GameStateManager.spawnPipeIdx.ToString());

		if (!t_GameStateManager.spawnFromPoint && t_GameStateManager.spawnPipeIdx == transform.parent.GetSiblingIndex ()) {
			isTakingMarioUp = true;
			mario.FreezeUserInput ();
			t_LevelManager.timerPaused = true;
			Debug.Log (this.name + " Start: " + transform.parent.gameObject.name + " taking Mario up");
		} else {
			isTakingMarioUp = false;
			transform.position = stop.position;
			Debug.Log (this.name + " Start: " + transform.parent.gameObject.name + " not taking Mario up");
		}

	}

	void FixedUpdate() {
		if (isTakingMarioUp) {
			if (transform.position.y < stop.position.y) {
				transform.position = new Vector2 (transform.position.x, transform.position.y + platformVelocityY);
			} else if (t_LevelManager.timerPaused) {
				GameStateManager t_GameStateManager = FindObjectOfType<GameStateManager> ();
				t_GameStateManager.spawnFromPoint = true;
				if (resetSpawnPoint) {
					t_GameStateManager.ResetSpawnPosition ();
				}
				mario.UnfreezeUserInput ();
				t_LevelManager.timerPaused = false;
				isTakingMarioUp = false;
			}		
		}
	}
		
}
