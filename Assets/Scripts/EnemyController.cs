using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float fireDelay = 0.5f;
	public float fireRate = 2f;
	public Vector2 dodgeDelay;
	public Vector2 dodgeTime;
	public Vector2 timeBetweenDodges;
	public float smoothing;
	public float tilt;

	AudioSource fireSound;
	Rigidbody rb;
	protected Transform playerTransform;

	void Start () {
		fireSound = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		currentSpeed = rb.velocity.z;
		AdjustForDifficulty ();
		InvokeRepeating("Shoot", fireDelay, fireRate);
		StartCoroutine (Evade ());
	}

	float currentSpeed;

	void FixedUpdate () {
			float newManouver = Mathf.MoveTowards (rb.velocity.x, manouverTarget, Time.deltaTime * smoothing);
			rb.velocity = new Vector3 (newManouver, 0f, currentSpeed);
			rb.position = new Vector3 (Mathf.Clamp (rb.position.x, -6f, 6f), 0f, rb.position.z);
			rb.rotation = Quaternion.Euler (new Vector3 (-3f, 180f, rb.velocity.x * tilt));
	}

	public GameObject shot;
	public Transform shotSpawn;

	void Shoot(){
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			fireSound.Play ();
	}

	protected float manouverTarget = 0;

	public virtual IEnumerator Evade(){
		yield return new WaitForSeconds (Random.Range (dodgeDelay.x, dodgeDelay.y));
		while(true){
			manouverTarget = Random.Range(0f,6f) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds (Random.Range (dodgeTime.x, dodgeTime.y));
			manouverTarget = 0f;
			yield return new WaitForSeconds (Random.Range (timeBetweenDodges.x, timeBetweenDodges.y));
		}
	}

	public virtual void AdjustForDifficulty(){
		timeBetweenDodges.y += (0.25f - (GameController.gameController.level / 10f));
		fireRate *= ((2f - (GameController.gameController.level / 10f)) + 0.9f);
	}
}
