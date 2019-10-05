using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyByBolt : MonoBehaviour {

	GameObject self;

	void Start(){
		self = gameObject;
	}

	void OnTriggerEnter(Collider other){
		float delay = 0f;
		try{
			if (other.CompareTag("Boundary") || other.CompareTag("Player") || other.CompareTag("Bolt") || (self.CompareTag("EnemyBolt")&&other.CompareTag("Enemy")) || (self.CompareTag("EnemyBolt")&&other.CompareTag("EnemyHard")) || other.CompareTag("PowerUp") || other.CompareTag("Shield"))
				return;
		}
		catch(NullReferenceException){
			return;
		}
		if (other.CompareTag ("EnemyBolt"))
			delay = 0.01f;
		Destroy (other.gameObject, delay);
		Destroy (this.gameObject, delay);
		delay = 0f;
		if (!self.CompareTag ("EnemyBolt")) {
			if (GameController.gameController == null)
				Debug.Log ("Can't find game controller");
			else {
				switch (other.tag) {
                case "Enemy":
                    GameController.gameController.AddScore(10);
                    break;
                case "EnemyHard":
                    GameController.gameController.AddScore (15);
                    break;
				case "Hazard":
					GameController.gameController.AddScore (5);
					break;
				}
			}
		}
	}
}