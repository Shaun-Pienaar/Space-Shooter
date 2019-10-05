using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByShip : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Boundary") || other.CompareTag("PowerUp") || other.CompareTag("Shield") || other.CompareTag("Bolt") || other.CompareTag("Player"))
			return;
		Destroy (other.gameObject);
		Destroy (this.gameObject);
	}
}
