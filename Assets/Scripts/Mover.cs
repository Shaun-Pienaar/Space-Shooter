using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
	public float speed = 1f;

	void Awake () {
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}
}
