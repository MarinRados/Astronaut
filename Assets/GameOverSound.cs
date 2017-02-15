using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSound : MonoBehaviour {

	[SerializeField]
	string gameOver = "GameOver";

	AudioManager audioManager;

	void Start() {
		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No AM");
		}

		GameOver ();

	}

	public void GameOver () {
		audioManager.PlaySound (gameOver);
	}
}
