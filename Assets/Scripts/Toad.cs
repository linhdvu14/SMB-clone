using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Toad : MonoBehaviour {
	public GameObject ThankYouMario;
	public GameObject ButOurPrincess;

	private Mario mario;
	private LevelManager t_LevelManager;


	// Use this for initialization
	void Start () {
		mario = FindObjectOfType<Mario> ();
		t_LevelManager = FindObjectOfType<LevelManager> ();
	}


	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			mario.FreezeUserInput ();
			StartCoroutine (DisplayMessageCo ());
		}
	}

	IEnumerator DisplayMessageCo() {
		ThankYouMario.SetActive (true);
		yield return new WaitForSecondsRealtime (.75f);
		ButOurPrincess.SetActive (true);
		yield return new WaitForSecondsRealtime (t_LevelManager.castleCompleteMusic.length);
		SceneManager.LoadScene ("Main Menu");
	}
}
