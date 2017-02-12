using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static void KillPlayer(Player player) {
		Destroy (player.gameObject);
		Debug.Log ("Rekt");
	}

	public static void KillEnemy (Enemy enemy) {
		Destroy (enemy.gameObject);
		Debug.Log ("Rekt enemy");
	}
}
