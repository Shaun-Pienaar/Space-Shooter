using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_OutOfBounds : MonoBehaviour {

	void OnTriggerExit(Collider other){
		if(other.gameObject.CompareTag("Hazard") || other.gameObject.CompareTag("Enemy") || other.CompareTag("EnemyHard"))
			other.gameObject.GetComponent<OnDestruction>().playAnimation = false;
		Destroy(other.gameObject);
	}
}
