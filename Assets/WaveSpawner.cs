using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState {
		SPAWNING, WAITING, COUNTING
	};


	[System.Serializable]
	public class Wave {
		public string name;
		public Transform enemy;
		public int count;
		public float rate;
	}

	public Wave[] waves;
	private int nextWave = 0;

	private int countIncrease = 0;
	private float rateIncrease = 0f;

	public Transform[] spawnPoints;

	public float timeBetweenWaves = 5f;
	private float waveCountdown;

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;

	void Start() {
		waveCountdown = timeBetweenWaves;
	}

	void Update () {

		if (state == SpawnState.WAITING) {
			if (!EnemyIsAlive ()) {
				WaveCompleted ();
				return;
			} 
			else {
				return;
			}
		}

		if (waveCountdown <= 0) {
			if (state != SpawnState.SPAWNING) {
				StartCoroutine (SpawnWave (waves [nextWave]));
			}
		} else {
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted() {
		Debug.Log ("Wave completed");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;



		if (nextWave + 1 > waves.Length - 1) {
			nextWave = 0;
			countIncrease++;
			rateIncrease += 0.5f;
			Debug.Log ("All waves complete. Looping...");
		} else {
			nextWave++;
		}
	}

	bool EnemyIsAlive() {
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f) {
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag ("Enemy") == null) {
				return false;
			}
		}

		return true;
	}

	IEnumerator SpawnWave (Wave _wave) {
		Debug.Log ("Spawning wave");
		state = SpawnState.SPAWNING;
		_wave.count += countIncrease;
		_wave.rate += rateIncrease;
		for (int i = 0; i < _wave.count; i++) {
			SpawnEnemy (_wave.enemy);
			yield return new WaitForSeconds (1f / _wave.rate);
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(Transform _enemy) {
		Debug.Log ("Spawning enemy");
		Transform _sp = spawnPoints [Random.Range (0, spawnPoints.Length)];
		Instantiate (_enemy, _sp.position, _sp.rotation);

	}
}
