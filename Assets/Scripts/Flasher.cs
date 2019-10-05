using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour {

	Material mat;

	void Awake(){
		mat = GetComponent<Renderer> ().material;
	}

	void Start () {
		StartCoroutine (Flash ());
	}

	IEnumerator Flash(){
		while (true) {
			mat.EnableKeyword ("_EMISSION");
			yield return new WaitForSeconds (0.5f);
			mat.DisableKeyword ("_EMISSION");
			yield return new WaitForSeconds (0.3f);
		}
	}
}
