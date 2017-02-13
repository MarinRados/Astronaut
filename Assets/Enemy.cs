using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[System.Serializable]
	public class EnemyStats {
		public int Health = 100;
	}

	public EnemyStats stats = new EnemyStats();

	public void DamageEnemy (int damage) {
		stats.Health -= damage;
		if (stats.Health <= 0) {
			GameMaster.KillEnemy (this);
		}
	}

	void OnCollisionEnter2D(Collision2D _colInfo) {
		Player _player = _colInfo.collider.GetComponent<Player> ();
		if (_player != null) {
			_player.DamagePlayer (1000);
			DamageEnemy (1000);
		}
	}
}
