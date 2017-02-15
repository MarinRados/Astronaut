using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;
	private static LevelManager levelManager;

	private AudioManager audioManager;

	void Awake () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ();
		}
	}

	void Start(){
		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No AM");
		}
	}

	public static void KillPlayer(Player player) {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		print("Trigger");
		levelManager.LoadLevel("GameOver");
	}

	public static void KillEnemy (Enemy enemy) {
		Destroy (enemy.gameObject);
	}
}
