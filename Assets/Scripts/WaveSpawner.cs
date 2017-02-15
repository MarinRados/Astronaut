using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState {
		SPAWNING, WAITING, COUNTING
	};


	[System.Serializable]
	public class Wave {
		public string name;
		//public Transform enemy;
		public Transform[] enemies;
		public int count;
		public float rate;
	}

	public Wave[] waves;
	public int nextWave = 0;
	public static int counter;

	public Text wavesSurvived;
	public Text waveCountText;

	public Transform[] spawnPoints;

	public float timeBetweenWaves = 5f;
	private float waveCountdown;

	private float searchCountdown = 1f;

	public SpawnState state = SpawnState.COUNTING;

	void Start() {
		counter = 1;
		waveCountdown = timeBetweenWaves;
		waveCountText.text = counter.ToString();
	}

	void Update () {

		if (state == SpawnState.WAITING) {
			if (!EnemyIsAlive ()) {
				WaveCompleted ();
				UpdateScore ();
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

		for (int i = 0; i < _wave.count; i++) {
			var randomEnemy = _wave.enemies [Random.Range (0, 3)];
			SpawnEnemy (randomEnemy);
			yield return new WaitForSeconds (1f / _wave.rate);
		}
		_wave.count++;
		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(Transform _enemy) {
		Debug.Log ("Spawning enemy");
		Transform _sp = spawnPoints [Random.Range (0, spawnPoints.Length)];
		Instantiate (_enemy, _sp.position, _sp.rotation);

	}

	void UpdateScore ()
	{
		counter++;
		waveCountText.text = counter.ToString();
	}
}
