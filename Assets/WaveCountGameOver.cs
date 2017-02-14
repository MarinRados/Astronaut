using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCountGameOver : MonoBehaviour {

	public Text wavesSurvived;

	// Use this for initialization
	void Start () {
		wavesSurvived.text = ((WaveSpawner.counter)-1).ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
