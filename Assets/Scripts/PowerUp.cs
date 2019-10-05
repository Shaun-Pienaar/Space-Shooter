using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public int shieldCount;
	public GameObject shield;
	public GameObject extraPlayer;

	List<Transform> playerShips = new List<Transform>();
	Transform playerTransform;

	AudioSource sound;

	void Start(){
		playerTransform = GameObject.FindGameObjectWithTag ("PlayerMain").GetComponent<Transform>();
		//foreach(Rigidbody rb in MainController.rbs){
		//	playerShips.Add (rb.transform);
		//}
		//playerShips.Add (playerTransform.GetChild (0));
		sound = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			switch(name){
			case "ExtraLife(Clone)":
				GameController.gameController.AddLife ();
				break;
			case "MultiShot(Clone)":
				GameController.gameController.multiShot = true;
				break;
			case "Shields(Clone)":
				foreach (Rigidbody rb in MainController.rbs) {
					playerShips.Add (rb.transform);
				}
				foreach (Transform player in playerShips) {
					Shield currentShield = player.gameObject.GetComponentInChildren<Shield> ();
					if (currentShield == null) {
						Instantiate (shield, player.transform.position, Quaternion.identity, player);
					}
					else{
						currentShield.shieldCount = 3;
					}
				}
				break;
			case "RapidFire(Clone)":
				PlayerController.ChangeFireRate (0.25f);
				break;
			case "Split(Clone)":
				Vector3 pos = new Vector3 ();
				foreach (Rigidbody rb in MainController.rbs) {
					playerShips.Add (rb.transform);
				}
				foreach (Transform player in playerShips) {
					player.position = new Vector3 (player.position.x - 1.5f, player.position.y, player.position.z);
					pos = new Vector3 (player.position.x + 1.5f, player.position.y, player.position.z);
				}
				GameObject temp = (GameObject)Instantiate (extraPlayer, pos, Quaternion.identity, playerTransform);
				playerShips.Add (temp.transform);
				GameController.gameController.isSplit++;
				break;
			}
			GetComponent<Renderer> ().enabled = false;
			GetComponent<Collider> ().enabled = false;
			sound.Play ();
			Destroy (gameObject, 1f);
		}
	}
}
