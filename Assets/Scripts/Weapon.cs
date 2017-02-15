using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public int Damage = 10;
	public LayerMask whatToHit;

	public Transform bulletTrailPrefab;
	public Transform hitPrefab;

	public float effectSpawnRate = 10;

	float timeToSpawnEffect = 0;
	Transform firePoint;


	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No fire point");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
		}
	}

	void Shoot () {
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
			Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100, whatToHit);

		if (hit.collider != null) {
			Enemy enemy = hit.collider.GetComponent<Enemy> ();
			if (enemy != null) {
				enemy.DamageEnemy (Damage);
			}
		}

		if (Time.time >= timeToSpawnEffect) {
			Vector3 hitPos;

			if (hit.collider == null)
				hitPos = (mousePosition - firePointPosition) * 1000;
			else {
				hitPos = hit.point;
				Instantiate (hitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitPos));
			}

			Effect (hitPos);
			timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
		}
	}

	void Effect(Vector3 hitPos) {
		Transform trail = Instantiate (bulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
		LineRenderer lr = trail.GetComponent<LineRenderer> (); 

		if (lr != null) {
			lr.SetPosition (0, firePoint.position);
			lr.SetPosition (1, hitPos);
		}

		Destroy (trail.gameObject, 0.06f);
	}
}
