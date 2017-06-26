using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {
	private GameStateManager t_GameStateManager;
	public Text TopText;

	public GameObject VolumePanel;
	public GameObject SoundSlider;
	public GameObject MusicSlider;

	public bool volumePanelActive;

	// Use this for initialization
	void Start () {
		t_GameStateManager = FindObjectOfType<GameStateManager> ();
		t_GameStateManager.ConfigNewGame ();

		int currentHighScore = PlayerPrefs.GetInt ("highScore", 0);
		TopText.text = "TOP- " + currentHighScore.ToString ("D6");

		if (!PlayerPrefs.HasKey ("soundVolume")) {
			PlayerPrefs.SetFloat ("soundVolume", 1);
		}

		if (!PlayerPrefs.HasKey ("musicVolume")) {
			PlayerPrefs.SetFloat ("musicVolume", 1);
		}

		SoundSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("soundVolume");
		MusicSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("musicVolume");

		Debug.Log (this.name + " Start: Volume Setting sound=" + PlayerPrefs.GetFloat ("soundVolume")
			+ "; music=" + PlayerPrefs.GetFloat ("musicVolume"));
	}

	public void OnMouseHover(Button button) {
		if (!volumePanelActive) {
			GameObject cursor = button.transform.FindChild ("Cursor").gameObject;
			cursor.SetActive (true);
		}
	}

	public void OnMouseHoverExit(Button button) {
		if (!volumePanelActive) {
			GameObject cursor = button.transform.FindChild ("Cursor").gameObject;
			cursor.SetActive (false);
		}
	}

	public void StartNewGame() {
		if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 1-1";
			SceneManager.LoadScene ("Level Start Screen");
		}
	}

	public void StartWorld1_2() {
		if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 1-2";
			SceneManager.LoadScene ("Level Start Screen");
		}
	}
		
	public void StartWorld1_3() {
		if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 1-3";
			SceneManager.LoadScene ("Level Start Screen");
		}
	}


	public void StartWorld1_4() {
		if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 1-4";
			SceneManager.LoadScene ("Level Start Screen");
		}
	}

	public void QuitGame() {
		Application.Quit ();
	}

	public void SelectVolume() {
		VolumePanel.SetActive (true);
		volumePanelActive = true;
	}

	public void SetVolume() {
		PlayerPrefs.SetFloat ("soundVolume", SoundSlider.GetComponent<Slider> ().value);
		PlayerPrefs.SetFloat ("musicVolume", MusicSlider.GetComponent<Slider> ().value);
		VolumePanel.SetActive (false);
		volumePanelActive = false;
	}

	public void CancelSelectVolume() {
		SoundSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("soundVolume");
		MusicSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("musicVolume");
		VolumePanel.SetActive (false);
		volumePanelActive = false;
	}

}
