using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestruction : MonoBehaviour {
	public GameObject DestructionAnimation;
	public bool playAnimation = true;

	void OnDestroy(){
		if (playAnimation) {
			Instantiate (DestructionAnimation, transform.position, transform.rotation);
		}
		if(gameObject.CompareTag("Enemy")){
			if (Random.Range (0, 21) == 0) {
				int i = Random.Range (0, GameController.gameController.powerUps.Length);
				Instantiate (GameController.gameController.powerUps [i], transform.position, Quaternion.Euler(new Vector3(0f,0f,90f)));
			}
		}
		if(gameObject.CompareTag("EnemyHard")){
			if (Random.Range (0, 11) == 0) {
				int i = Random.Range (0, GameController.gameController.powerUps.Length);
				Instantiate (GameController.gameController.powerUps [i], transform.position, Quaternion.Euler(new Vector3(0f,0f,90f)));
			}
		}
	}

	void LateUpdate(){
		playAnimation = true;
	}
}