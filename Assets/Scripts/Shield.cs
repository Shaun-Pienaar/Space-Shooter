using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public int shieldCount;

	void Awake(){
		shieldCount = 3;
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Bolt") || other.CompareTag ("Player") || other.CompareTag("Boundary") || other.CompareTag("PowerUp") || other.CompareTag("Shield"))
			return;
		Destroy (other.gameObject);
		shieldCount -= 1;
		if (shieldCount < 1)
			Destroy (gameObject);
	}
}
