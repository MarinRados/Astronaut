using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

	[SerializeField]
	WaveSpawner spawner;

	[SerializeField]
	Text waveCountText;

	private WaveSpawner.SpawnState previousState;


	// Use this for initialization
	void Start () {
		if (spawner == null) {
			Debug.Log ("No spawner referenced");
			this.enabled = false;
		}

		if (waveCountText == null) {
			Debug.Log ("No waveCountText referenced");
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (spawner.state == WaveSpawner.SpawnState.COUNTING) {
			UpdateSpawningUI ();
		}

		previousState = spawner.state;
	}

	void UpdateSpawningUI() {
		if (previousState != WaveSpawner.SpawnState.COUNTING) {

			waveCountText.text = spawner.nextWave.ToString ();
			Debug.Log ("COUNTING");
		}

	}
}
