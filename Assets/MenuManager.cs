using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	string hoverOverSound = "ButtonHover";

	[SerializeField]
	string pressButtonSound = "ButtonPress";

	AudioManager audioManager;

	void Start() {
		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No AM");
		}
	}

	public void OnMouseOver () {
		audioManager.PlaySound (hoverOverSound);
	}

	public void OnMousePress () {
		audioManager.PlaySound (pressButtonSound);
	}
}
