using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbler : MonoBehaviour {

	// Use this for initialization
	void Start (){
		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * 5.0f;
	}
}