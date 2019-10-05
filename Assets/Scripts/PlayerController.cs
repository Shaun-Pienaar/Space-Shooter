using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController playerController;

	public GameObject shot;
	public Transform shotSpawn;

	//Rigidbody rb;
	AudioSource sound;

	static float fireRate = 0.5f;
	float nextFire;

	void Start () {
		if (playerController == null) {
			playerController = this;
		}
		//rb = GetComponent<Rigidbody> ();
		sound = GetComponent<AudioSource> ();
	}

	void Update(){
		if (Input.GetButton ("Jump") && Time.time > nextFire) {
			Quaternion tempRotation = Quaternion.Euler(new Vector3(shotSpawn.rotation.x, shotSpawn.rotation.y, 0f));
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, tempRotation);
			if (GameController.gameController.multiShot) {
				Instantiate (shot, shotSpawn.position, tempRotation * Quaternion.Euler (new Vector3 (0f, -20f, 0f)));
				Instantiate (shot, shotSpawn.position, tempRotation * Quaternion.Euler (new Vector3 (0f, 20f, 0f)));
			}
			sound.Play ();
		}
	}

	/*void FixedUpdate () {
		float inputHorizontal = Input.GetAxis ("Horizontal");
		float inputVertical = Input.GetAxis ("Vertical");
		Vector3 inputTotal = new Vector3 (inputHorizontal, 0.0f, inputVertical);

		rb.velocity = inputTotal.normalized * 9.0f;
		rb.position = new Vector3 (Mathf.Clamp (rb.position.x, -6.0f, 6.0f), 0.0f, Mathf.Clamp (rb.position.z, -4.0f, 8.0f));
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -4.0f);
	}*/

	void OnDestroy(){
		MainController.rbs.Remove (this.GetComponent<Rigidbody> ());
		MainController.childCount--;
		if (GameController.gameController.isSplit > 1) {
			GameController.gameController.isSplit--;
			return;
		}
		fireRate = 0.5f;
		if (GameController.gameController != null) {
			GameController.gameController.multiShot = false;
			if (GameController.gameController.LoseLife ())
				GameController.gameController.Respawn ();
			else
				GameController.gameController.GameOver ();
		}
		else
			Debug.Log ("Can't find game controller");
	}

	public static void ChangeFireRate(float newFireRate){
		fireRate = newFireRate;
	}
}
